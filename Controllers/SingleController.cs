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
    public class SingleController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public SingleController(
            ApplicationDbContext context)
        {
            this.context = context;
        }

        //[HttpGet("{userId}")]
        [HttpGet, Route("Get")]
        public async Task<ActionResult<Search>> Get(string searchId) //[FromQuery(Name = "userId")] 
        {
            var search = await this.context.Searches
                .Where(x => x.Id == searchId)
                .FirstOrDefaultAsync();
            search.Dump();
            return search;
        }
    }
}
