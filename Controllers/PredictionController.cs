using ImotiPrediction.Data;
using ImotiPrediction.Models;
using ImotiPrediction.Services;
using ImotiPrediction.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ImotiPrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PredictionController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;
        private readonly IHttpContextAccessor contextAccessor;

        public PredictionController(UserManager<ApplicationUser> userManager, 
            ApplicationDbContext context,
            IHttpContextAccessor contextAccessor)
        {
            this.userManager = userManager;
            this.context = context;
            this.contextAccessor = contextAccessor;
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<ModelOutput>> Post(FormInput input)
        {
            //var userId = this.userManager.GetUserId(this.User);
            //var user = await this.userManager.GetUserAsync(this.User);
            //var userId = this.contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            //var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

             var castInput = new ModelInput
            {
                BuildingType = input.BuildingType,
                District = input.District,
                Floor = input.Floor,
                TotalFloors = input.TotalFloors,
                Size = input.Size,
                Year = input.Year,
                Type = input.Type,
            };

            var score = Prediction.TestModel(castInput) < 0 ? 0 : Prediction.TestModel(castInput);
            var search = new Search
            {
                Id = Guid.NewGuid().ToString(),
                BuildingType = input.BuildingType,
                District = input.District,
                Floor = (input.Floor),
                TotalFloors = (input.TotalFloors),
                Size = (input.Size),
                Year = (input.Year),
                Type = input.Type,
                UserId = input.UserId,
                Score = score,
                PredictedAt = DateTime.UtcNow,
            };

            this.context.Add(search);
            await this.context.SaveChangesAsync();

            /*var lastSearches = await this.context.Searches
                .Where(x => x.UserId == input.UserId)
                .OrderByDescending(x => x.PredictedAt)
                .Take(5)
                .ToListAsync();*/
            return new ModelOutput { Score = score };
        }

        
    }
}
