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
    public class StatisticsModelsController : ControllerBase
    {
        private readonly CoronaDbContext _context;

        public StatisticsModelsController(CoronaDbContext context)
        {
            _context = context;
        }

        // GET: api/StatisticsModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatisticsModel>>> GetStatisticsModels()
        {
            var CountryStat = await _context.StatisticsModels.ToListAsync();

            return CountryStat;
        }

        // GET: api/StatisticsModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatisticsModel>> GetStatisticsModel(int id)
        {
            var statisticsModel = await _context.StatisticsModels.FindAsync(id);

            if (statisticsModel == null)
            {
                return NotFound();
            }

            return statisticsModel;
        }

        // PUT: api/StatisticsModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatisticsModel(int id, StatisticsModel statisticsModel)
        {
            if (id != statisticsModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(statisticsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatisticsModelExists(id))
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

        // POST: api/StatisticsModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<StatisticsModel>> PostStatisticsModel(StatisticsModel statisticsModel)
        {
            _context.StatisticsModels.Add(statisticsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatisticsModel", new { id = statisticsModel.Id }, statisticsModel);
        }

        // DELETE: api/StatisticsModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<StatisticsModel>> DeleteStatisticsModel(int id)
        {
            var statisticsModel = await _context.StatisticsModels.FindAsync(id);
            if (statisticsModel == null)
            {
                return NotFound();
            }

            _context.StatisticsModels.Remove(statisticsModel);
            await _context.SaveChangesAsync();

            return statisticsModel;
        }

        private bool StatisticsModelExists(int id)
        {
            return _context.StatisticsModels.Any(e => e.Id == id);
        }
    }
}
