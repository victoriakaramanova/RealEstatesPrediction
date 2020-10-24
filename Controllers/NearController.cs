using ImotiPrediction.Data;
using ImotiPrediction.Models;
using ImotiPrediction.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImotiPrediction.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class NearController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public NearController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet, Route("Post")]
        public async Task<ActionResult<IEnumerable<RawProperty>>> Post(string id)
        {
            var prediction = await this.context.Searches
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            var actualOffers = await this.context.Offers
                .Where(x => x.District.Contains(prediction.District)
                && x.Type == prediction.Type
                //&& x.Price < prediction.Score * 2
                && x.Size < prediction.Size + 5 
                && x.Size > prediction.Size - 5)
                .Take(6)
                .OrderBy(x => x.Price)
                .ToListAsync();
            actualOffers.Dump();
            return actualOffers;
        }
    }
}
