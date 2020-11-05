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
    public class SOSModelsController : ControllerBase
    {
        private readonly CoronaDbContext _context;

        public SOSModelsController(CoronaDbContext context)
        {
            _context = context;
        }

        // GET: api/SOSModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SOSModel>>> GetSOSModels()
        {
            return await _context.SOSModels.ToListAsync();
        }

        // GET: api/SOSModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SOSModel>> GetSOSModel(int id)
        {
            var sOSModel = await _context.SOSModels.FindAsync(id);

            if (sOSModel == null)
            {
                return NotFound();
            }

            return sOSModel;
        }

        // PUT: api/SOSModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSOSModel(int id, SOSModel sOSModel)
        {
            if (id != sOSModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(sOSModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SOSModelExists(id))
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

        // POST: api/SOSModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<SOSModel>> PostSOSModel(SOSModel sOSModel)
        {
            _context.SOSModels.Add(sOSModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSOSModel", new { id = sOSModel.Id }, sOSModel);
        }

        // DELETE: api/SOSModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<SOSModel>> DeleteSOSModel(int id)
        {
            var sOSModel = await _context.SOSModels.FindAsync(id);
            if (sOSModel == null)
            {
                return NotFound();
            }

            _context.SOSModels.Remove(sOSModel);
            await _context.SaveChangesAsync();

            return sOSModel;
        }

        private bool SOSModelExists(int id)
        {
            return _context.SOSModels.Any(e => e.Id == id);
        }
    }
}
