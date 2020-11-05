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
    public class DiseaseModelsController : ControllerBase
    {
        private readonly CoronaDbContext _context;

        public DiseaseModelsController(CoronaDbContext context)
        {
            _context = context;
        }

        // GET: api/DiseaseModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DiseaseModel>>> GetDiseaseModels()
        {
            return await _context.DiseaseModels.ToListAsync();
        }

        // GET: api/DiseaseModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DiseaseModel>> GetDiseaseModel(int id)
        {
            var diseaseModel = await _context.DiseaseModels.FindAsync(id);

            if (diseaseModel == null)
            {
                return NotFound();
            }

            return diseaseModel;
        }

        // PUT: api/DiseaseModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDiseaseModel(int id, DiseaseModel diseaseModel)
        {
            if (id != diseaseModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(diseaseModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DiseaseModelExists(id))
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

        // POST: api/DiseaseModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DiseaseModel>> PostDiseaseModel(DiseaseModel diseaseModel)
        {
            _context.DiseaseModels.Add(diseaseModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDiseaseModel", new { id = diseaseModel.Id }, diseaseModel);
        }

        // DELETE: api/DiseaseModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DiseaseModel>> DeleteDiseaseModel(int id)
        {
            var diseaseModel = await _context.DiseaseModels.FindAsync(id);
            if (diseaseModel == null)
            {
                return NotFound();
            }

            _context.DiseaseModels.Remove(diseaseModel);
            await _context.SaveChangesAsync();

            return diseaseModel;
        }

        private bool DiseaseModelExists(int id)
        {
            return _context.DiseaseModels.Any(e => e.Id == id);
        }
    }
}
