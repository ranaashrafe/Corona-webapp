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
    public class NewsSourceModelsController : ControllerBase
    {
        private readonly CoronaDbContext _context;

        public NewsSourceModelsController(CoronaDbContext context)
        {
            _context = context;
        }

        // GET: api/NewsSourceModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NewsSourceModel>>> GetNewsSourceModels()
        {
            return await _context.NewsSourceModels.Include(obj=>obj.CountryModel).ToListAsync();
        }

        // GET: api/NewsSourceModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NewsSourceModel>> GetNewsSourceModel(int id)
        {
            var newsSourceModel = await _context.NewsSourceModels.FindAsync(id);

            if (newsSourceModel == null)
            {
                return NotFound();
            }

            return newsSourceModel;
        }

        // PUT: api/NewsSourceModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNewsSourceModel(int id, NewsSourceModel newsSourceModel)
        {
            if (id != newsSourceModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(newsSourceModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsSourceModelExists(id))
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

        // POST: api/NewsSourceModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<NewsSourceModel>> PostNewsSourceModel(NewsSourceModel newsSourceModel)
        {
            _context.NewsSourceModels.Add(newsSourceModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNewsSourceModel", new { id = newsSourceModel.Id }, newsSourceModel);
        }

        // DELETE: api/NewsSourceModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<NewsSourceModel>> DeleteNewsSourceModel(int id)
        {
            var newsSourceModel = await _context.NewsSourceModels.FindAsync(id);
            if (newsSourceModel == null)
            {
                return NotFound();
            }

            _context.NewsSourceModels.Remove(newsSourceModel);
            await _context.SaveChangesAsync();

            return newsSourceModel;
        }

        private bool NewsSourceModelExists(int id)
        {
            return _context.NewsSourceModels.Any(e => e.Id == id);
        }
    }
}
