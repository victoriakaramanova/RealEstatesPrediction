using ImotiPrediction.Data;
using ImotiPrediction.Models;
using ImotiPrediction.Services;
using ImotiPrediction.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Services.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImotiPrediction.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext context;

        public SearchController(UserManager<ApplicationUser> userManager,
            ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        //[HttpGet("{userId}")]
        [HttpGet, Route("Get")]
        public async Task<ActionResult<IEnumerable<Search>>> Get(string userId) //[FromQuery(Name = "userId")] 
        {
            var lastSearches = await this.context.Searches
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.PredictedAt)
                .Take(5)
                .ToListAsync();
            lastSearches.Dump();
            return lastSearches;
        }
    }
}
