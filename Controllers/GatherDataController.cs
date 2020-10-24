using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImotiPrediction.Data;
using ImotiPrediction.Models;
using AngleSharp.Html.Parser;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using AngleSharp.Dom;

namespace ImotiPrediction.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class GatherDataController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public GatherDataController(ApplicationDbContext context)
        {
            this.context = context;
        }

        // GET: api/GatherData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RawProperty>>> Get() //(int fromSize, int toSize)
        {
            int fromSize = 10;
            int toSize = 1000;

            var properties = new List<RawProperty>();

            var parser = new HtmlParser();
            var handler = new HttpClientHandler { AllowAutoRedirect = false, };
            var client = new HttpClient(handler);
            var floorsRegex = new Regex(@"(?<floor>[0-9]+)[^\d]+(?<all>[0-9]+)", RegexOptions.Compiled);
            var typeAndInfoRegex = new Regex(@"^(?<type>[^,]+)([,\s]+(?<year>[0-9]+))?", RegexOptions.Compiled);

            // 10 => 1000
            for (var size = fromSize; size <= toSize; size++)
            {
                Console.Write($"Area {size}: ");
                var formData =
                    $"act=3&rub=1&rub_pub_save=1&topmenu=2&actions=1&f0=127.0.0.1&f1=1&f2=&f3=&f4=1&f7=1%7E2%7E3%7E4%7E5%7E6%7E8%7E&f28=&f29=&f43=&f44=&f30=EUR&f26={size}&f27={size}&f41=1&f31=&f32=&f38=%E3%F0%E0%E4+%D1%EE%F4%E8%FF&f42=&f39=&f40=&fe3=&fe4=&f45=&f46=&f51=&f52=&f33=&f34=&f35=&f36=&f37=&fe2=1";
                //$"act=3&rub=1&rub_pub_save=1&topmenu=2&actions=1&f0=127.0.0.1&f1=1&f2=&f3=&f4=1&f7=1%7E2%7E3%7E4%7E5%7E6%7E8%7E&f28=&f29=&f43=&f44=&f30=EUR&f26={size}&f27={size}&f41=1&f31=&f32=&f38=%E3%F0%E0%E4+{town}&f42=&f39=&f40=&fe3=&fe4=&f45=&f46=&f51=&f52=&f33=&f34=&f35=&f36=&f37=&fe2=1";
                var response = await client.PostAsync(
                                   "https://www.imot.bg/pcgi/imot.cgi",
                                   new StringContent(formData, Encoding.UTF8, "application/x-www-form-urlencoded"));
                var firstPageUrl = response.Headers.Location;

                for (var page = 1; page <= 26; page++)
                {
                    var pageUrl = firstPageUrl.ToString().Replace("&f1=1", $"&f1={page}");
                    var pageHtml = await GetHtml(pageUrl);
                    var pageDocument = await parser.ParseDocumentAsync(pageHtml);
                    var listItems = pageDocument.QuerySelectorAll("a.photoLink").Where(
                        x => x.Attributes["href"]?.Value?.Contains("pcgi/imot.cgi?act=5&adv=") == true).ToList();

                    if (!listItems.Any())
                    {
                        break;
                    }

                    foreach (var listItem in listItems)
                    {
                        var url = "https:" + listItem.Attributes["href"].Value;
                        var html = await GetHtml(url);
                        var document = await parser.ParseDocumentAsync(html);
                        var district = document.QuerySelectorAll("table")[2].QuerySelectorAll("tr td div")[1].TextContent;
                        if (district?.Contains("<br>") == true)
                        {
                            var indexOfBr = district.IndexOf("<br>", StringComparison.InvariantCulture);
                            district = district.Substring(0, indexOfBr).Trim();
                        }

                        var pic = document.QuerySelector("img.big").Attributes["src"].Value;
                        //Console.WriteLine(pic);
                        var floorInfoString = document.QuerySelectorAll("ul.imotData li")[3].TextContent; ;
                        var floorMatch = floorsRegex.Match(floorInfoString);
                        var typeAndInfoString = document.QuerySelector("ul.imotData").Children.Last().TextContent;
                        var typeAndInfoMatch = typeAndInfoRegex.Match(typeAndInfoString);
                        
                        
                        var property = new RawProperty
                        {
                            Url = url,
                            Size = size,
                            District = district,
                            Type = typeAndInfoString.Split(",")[0],
                            Floor =
                                               floorMatch.Success ? floorMatch.Groups["floor"].Value.ToInteger() : 0,
                            TotalFloors =
                                               floorMatch.Success ? floorMatch.Groups["all"].Value.ToInteger() : 0,
                            Price =
                                               document.QuerySelector("span#cena")?.TextContent
                                                   ?.Replace(" EUR", string.Empty)?.ToInteger() ?? 0,
                            Year = typeAndInfoMatch.Success && typeAndInfoMatch.Groups["year"].Success
                                                      ? typeAndInfoMatch.Groups["year"].Value.ToInteger()
                                                      : 0,
                            BuildingType =
                                                document.QuerySelectorAll("table")[2].QuerySelectorAll("tr td div h1")[0].TextContent.Replace("Продава ", string.Empty),
                            CreatedAt = DateTime.UtcNow,
                            Pic = pic,
                        };

                         if (!OfferExists(property.Url) && property.TotalFloors != 0 && property.Year != 0 && property.Price != 0) 
                        //if (property.TotalFloors != 0 && property.Year != 0 && property.Price != 0)
                        { 
                            properties.Add(property);
                            await context.AddAsync(property);
                            await context.SaveChangesAsync();
                        }
                    }

                    Console.Write($"{page}({properties.Count}), ");
                }
                //await context.AddRangeAsync(properties);
                //await context.SaveChangesAsync();

                Console.WriteLine($" => Total: {properties.Count}");
            }

            //await context.AddRangeAsync(properties);
           // var result = await context.SaveChangesAsync();
            return properties.OrderByDescending(x=>x.CreatedAt)
                .Take(5).ToList();


            async Task<string> GetHtml(string pageUrl)
            {
                var pageResponse = await client.GetAsync(pageUrl);
                var byteContent = await pageResponse.Content.ReadAsByteArrayAsync();
                var html = Encoding.GetEncoding("windows-1251").GetString(byteContent);
                return html;
            }
        }

        

        private bool OfferExists(string url)
        {
            return this.context.Offers.Any(e => e.Url == url);
        }
    }
}
