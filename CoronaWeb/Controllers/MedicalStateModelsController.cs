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
    public class MedicalStateModelsController : ControllerBase
    {
        private readonly CoronaDbContext _context;

        public MedicalStateModelsController(CoronaDbContext context)
        {
            _context = context;
        }

        // GET: api/MedicalStateModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MedicalStateModel>>> GetMedicalStateModels()
        {
            return await _context.MedicalStateModels.ToListAsync();
        }

        // GET: api/MedicalStateModels/5
        [HttpGet("{id}")]
        public  Task<List<DiseaseModel>> GetMedicalStateModel(int id)
        {
         //   var medicalStateModel =  _context.MedicalStateModels.Where(c=>c.UserModelId==id).ToListAsync();
            var medname = (from b in _context.MedicalStateModels.Where(c => c.UserModelId == id)
                           join x in _context.DiseaseModels
                           on b.DiseaseModelId equals x.Id
                           select x).ToListAsync();
           

            return medname;
        }

        // PUT: api/MedicalStateModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
     /*   [HttpPut("{id}")]
        public async Task<IActionResult> PutMedicalStateModel(int id, MedicalStateModel medicalStateModel)
        {
            if (id != medicalStateModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(medicalStateModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicalStateModelExists(id))
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
        */
        // POST: api/MedicalStateModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<MedicalStateModel>> PostMedicalStateModel(MedicalStateModel medicalStateModel)
        {
         

            medicalStateModel.DiseaseModel = null;
            medicalStateModel.UserModel = null;
            if (!MedicalStateModelExists(medicalStateModel.UserModelId, medicalStateModel.DiseaseModelId))
            {
                _context.MedicalStateModels.Add(medicalStateModel);
            }
            else
            {
                
                var medicalStateModeldelete =  _context.MedicalStateModels.Where(c=>c.UserModelId==medicalStateModel.UserModelId&&c.DiseaseModelId==medicalStateModel.DiseaseModelId).FirstOrDefault();

                _context.MedicalStateModels.Remove(medicalStateModeldelete);
                await _context.SaveChangesAsync();
            }
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedicalStateModel", new { id = medicalStateModel.Id }, medicalStateModel);
        }

        // DELETE: api/MedicalStateModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MedicalStateModel>> DeleteMedicalStateModel(int id)
        {
            var medicalStateModel = await _context.MedicalStateModels.FindAsync(id);
            if (medicalStateModel == null)
            {
                return NotFound();
            }

            _context.MedicalStateModels.Remove(medicalStateModel);
            await _context.SaveChangesAsync();

            return medicalStateModel;
        }

        private bool MedicalStateModelExists(int id,int Did)
        {
            return _context.MedicalStateModels.Any(e => e.UserModelId == id && e.DiseaseModelId==Did);
        }
    }
}
