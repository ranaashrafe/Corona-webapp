using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoronaWeb.CoronaDb;
using CoronaWeb.Models;

namespace CoronaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryModelsController : ControllerBase
    {
        private readonly CoronaDbContext _context;

        public CountryModelsController(CoronaDbContext context)
        {
            _context = context;
        }

        // GET: api/CountryModels
        [HttpGet]
        public async Task<ActionResult<List<CountryModel>>> GetCountryModels()
        {
            var countries = await _context.CountryModels
                .Include(obj => obj.StatisticsModel)
                .Include(obj=>obj.NewsSourceModels)
                .Include(obj=>obj.SOSModels)
                .ToListAsync() ;

            return countries.Take(5).ToList();
        }
        [HttpGet]
        [Route("AllCountries")]
        public async Task<ActionResult<List<CountryModel>>> GetCountryMoreModels()
        {
            var countries = await _context.CountryModels
               .Include(obj => obj.StatisticsModel)
               .Include(obj => obj.NewsSourceModels)
               .Include(obj => obj.SOSModels)
               .ToListAsync();

            return countries;
        }
        // GET: api/CountryModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CountryModel>> GetCountryModel(int id)
        {
            var countryModel = await _context.CountryModels.FindAsync(id);
           
            if (countryModel == null)
            {
                return NotFound();
            }

            return countryModel;
        }

        // PUT: api/CountryModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountryModel(int id, CountryModel countryModel)
        {
            if (id != countryModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(countryModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CountryModelExists(id))
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

        // POST: api/CountryModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<CountryModel>> PostCountryModel(CountryModel countryModel)
        {
            countryModel.UserModels = null;

        
            
            _context.CountryModels.Add(countryModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCountryModel", new { id = countryModel.Id }, countryModel);
        }

        // DELETE: api/CountryModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<CountryModel>> DeleteCountryModel(int id)
        {
            var countryModel = await _context.CountryModels.FindAsync(id);
            if (countryModel == null)
            {
                return NotFound();
            }

            _context.CountryModels.Remove(countryModel);
            await _context.SaveChangesAsync();

            return countryModel;
        }

        private bool CountryModelExists(int id)
        {
            return _context.CountryModels.Any(e => e.Id == id);
        }
    }
}
