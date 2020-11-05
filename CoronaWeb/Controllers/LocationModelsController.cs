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
    public class LocationModelsController : ControllerBase
    {
        private readonly CoronaDbContext _context;

        public LocationModelsController(CoronaDbContext context)
        {
            _context = context;
        }

        // GET: api/LocationModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LocationModel>>> GetLocationModels()
        {
            return await _context.LocationModels.ToListAsync();
        }

        // GET: api/LocationModels/5
        [HttpGet]
        [Route("UserLocations")]
        public  Task<List<UserModel>> GetLocationModel(int id)
        {
            var locationModel = _context.UserModels.Where(c => c.Id == id)
                .Include(obj => obj.LocationModels)
                
               .ToListAsync();
                

            if (locationModel == null)
            {
              
            }

            return locationModel;
        }

        /*  [HttpGet]
          [Route("UserSharedLocation")]
          public Task<List<LocationModel>> GetLocationSharedModel(int lat , int longa, DateTime dateTime)
          {
              var locationModel = _context.LocationModels.Where(c => c.Latitude == lat &&
              c.Longitude == longa && c.Date == dateTime)
                  .Include(obj => obj.UserModel)

                 .ToListAsync();


              if (locationModel == null)
              {

              }

              return locationModel;
          }*/

        [HttpGet]
        [Route("UserSharedLocation")]
        public Task<List<LocationModel>> GetLocationSharedModel(int userid)
        {
            var locationModel = _context.LocationModels.Where(c => c.UserModelId == userid).FirstOrDefault();
            var locatsharreduser = _context.LocationModels.Where(b => b.Longitude == locationModel.Longitude
            && b.Latitude == locationModel.Latitude)
                .Include(obj=>obj.UserModel).ToListAsync();

            return locatsharreduser;
        }
      /*  [HttpGet]
        [Route("UserSharedLocationCount")]
        public Task<int> UserSharedLocationCount(int userid)
        {
            var locationModel = _context.LocationModels.Where(c => c.UserModelId == userid).FirstOrDefault();
          
            var locatsharreduser = (_context.LocationModels.Where(b => b.Longitude == locationModel.Longitude && b.Latitude == locationModel.Latitude)
                .Include(obj => obj.UserModel)).CountAsync();

         
            return locatsharreduser;
        }
        */

        // PUT: api/LocationModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocationModel(int id, LocationModel locationModel)
        {
            if (id != locationModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(locationModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocationModelExists(id))
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

        // POST: api/LocationModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<LocationModel>> PostLocationModel(LocationModel locationModel)
        {
            locationModel.UserModel = null;
            
            _context.LocationModels.Add(locationModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLocationModel", new { id = locationModel.Id }, locationModel);
        }

        // DELETE: api/LocationModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<LocationModel>> DeleteLocationModel(int id)
        {
            var locationModel = await _context.LocationModels.FindAsync(id);
            if (locationModel == null)
            {
                return NotFound();
            }

            _context.LocationModels.Remove(locationModel);
            await _context.SaveChangesAsync();

            return locationModel;
        }

        private bool LocationModelExists(int id)
        {
            return _context.LocationModels.Any(e => e.Id == id);
        }
    }
}
