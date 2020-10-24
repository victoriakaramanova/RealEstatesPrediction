using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ImotiPrediction.Data;
using ImotiPrediction.Models;

namespace ImotiPrediction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OffersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OffersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Offers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RawProperty>>> GetOffers()
        {
            if (_context.Offers.Count() == 0)
            {
                Redirect("GatherData");
            }

            return await _context.Offers
                //.Where(x => x.Pic.Contains("cloudinary"))
                
                .OrderBy(x => Guid.NewGuid())
                .Take(5)
                .ToListAsync();
        }
        // GET: api/Offers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RawProperty>> GetOffer(string id)
        {
            var offer = await _context.Offers.FindAsync(id);

            if (offer == null)
            {
                return NotFound();
            }

            return offer;
        }

        // PUT: api/Offers/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOffer(string id, RawProperty offer)
        {
            if (id != offer.Id)
            {
                return BadRequest();
            }

            _context.Entry(offer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Offers
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
       // [HttpPost]
        //public async Task<ActionResult<RawProperty>> PostOffer()
       // {
         //   ImotBgDataGatherer t = new ImotBgDataGatherer(_context);
           // var properties = await t.GatherData(30, 32);

            /*_context.Offers.Add(offer);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (OfferExists(offer.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }*/

            //return CreatedAtAction("GetOffers", new { properties }); //, new { id = offer.Id }, offer);
       // }

        // DELETE: api/Offers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<RawProperty>> DeleteOffer(string id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }

            _context.Offers.Remove(offer);
            await _context.SaveChangesAsync();

            return offer;
        }

        private bool OfferExists(string id)
        {
            return _context.Offers.Any(e => e.Id == id);
        }
    }
}
