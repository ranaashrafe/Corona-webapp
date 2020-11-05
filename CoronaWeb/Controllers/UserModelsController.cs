using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoronaWeb.CoronaDb;
using CoronaWeb.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

using Microsoft.AspNetCore.Authorization;
using System.Net.Mail;

namespace CoronaWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserModelsController : ControllerBase
    {
        private readonly CoronaDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;
        IHostingEnvironment envmail = null;

        public UserModelsController(CoronaDbContext context, IHostingEnvironment env, IHostingEnvironment envmail)
        {
            _context = context;
            hostingEnvironment = env;
            this.envmail = envmail;
        }

        // GET: api/UserModels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserModel>>> GetUserModels()
        {
            return await _context.UserModels.ToListAsync();
        }

        // GET: api/UserModels/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUserModel(int id)
        {
            var userModel = await _context.UserModels.FindAsync(id);

            if (userModel == null)
            {
                return NotFound();
            }
        
            var LoggedUserLocation = _context.UserModels.Where(x => x.Email == userModel.Email && x.Password == userModel.Password)
                 .Include(obj => obj.LocationModels)
                 .Include(obj => obj.MedicalStateModels)
                 .FirstOrDefault();
            if (LoggedUserLocation == null)
            {
                return userModel;
            }


            return LoggedUserLocation;
        }

        // PUT: api/UserModels/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<UserModel> PutUserModel(int id, UserModel userModel)
        {
            if (id != userModel.Id)
            {
                
            }

            _context.Entry(userModel).State = EntityState.Modified;

         
                await _context.SaveChangesAsync();
           

            return userModel;
        }

        // POST: api/UserModels
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        [Route("SignUp")]
        public async Task<ActionResult<UserModel>> PostUserModel(UserModel userModel)
        {
            userModel.Email = userModel.Email.ToLower();
            userModel.MedicalStateModels = null;
            userModel.LocationModels = null;
            
            _context.UserModels.Add(userModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserModel", new { id = userModel.Id }, userModel);
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<UserModel>> LoginUserModel(UserModel userModel)
        {
            userModel.Email = userModel.Email.ToLower();
        
            var LoggedUser = _context.UserModels.Where(x => x.Email == userModel.Email && x.Password == userModel.Password)
               .FirstOrDefault();
            if (LoggedUser == null)
            {
                return NotFound();
            }
         var LoggedUserLocation = _context.UserModels.Where(x => x.Email == userModel.Email && x.Password == userModel.Password)
              .Include(obj=>obj.LocationModels)
              .Include(obj=>obj.MedicalStateModels)
              .FirstOrDefault();
            if (LoggedUserLocation == null)
            {
                return LoggedUser;
            }
          
     
            return LoggedUserLocation;
        }
        // DELETE: api/UserModels/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserModel>> DeleteUserModel(int id)
        {
            var userModel = await _context.UserModels.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }

            _context.UserModels.Remove(userModel);
            await _context.SaveChangesAsync();

            return userModel;
        }

        private bool UserModelExists(int id)
        {
            return _context.UserModels.Any(e => e.Id == id);
        }
        [HttpPost("UploadImage")]
        public async Task<string> UploadImage(IFormFile file)
        {
            string output = "";
            try
            {
                List<string> types = new List<string>();
                if (ModelState.IsValid)
                {
                    // Code to upload image if not null
                    if (file != null && file.Length != 0)
                    {
                        // Create a File Info
                        FileInfo fi = new FileInfo(file.FileName);

                        // This code creates a unique file name to prevent duplications
                        // stored at the file location
                        var newFilename = String.Format("{0:d}",
                                              (DateTime.Now.Ticks / 10) % 100000000) + fi.Extension;
                        var webPath = hostingEnvironment.WebRootPath;
                        var path = Path.Combine("", webPath + @"\uploads\" + newFilename);

                        // IMPORTANT: The pathToSave variable will be save on the column in the database
                        var pathToSave = @"/uploads/" + newFilename;

                        // This stream the physical file to the allocate wwwroot/ImageFiles folder
                        using (var stream = new FileStream(path, FileMode.CreateNew))
                        {
                            await file.CopyToAsync(stream);

                        }

                        // This save the path to the record
                        output = pathToSave;

                    }
                }

            }
            catch (Exception ex)
            {
                output = ex.Message;
            }
            return output;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("send")]
        public string PostSendGmail(MessageModel messageModel)
        {
            SmtpClient client = new SmtpClient();
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = true;
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            System.Net.NetworkCredential credentials =
                new System.Net.NetworkCredential("ranaaliashraf55789@gmail.com", "rana123456");
            client.UseDefaultCredentials = false;
            client.Credentials = credentials;
            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("ranaaliashraf55789@gmail.com");
            msg.To.Add(new MailAddress(messageModel.Email));
            msg.Subject = messageModel.Body;
            msg.IsBodyHtml = true;
            msg.Body = string.Format("<html><head></head><body><b>Message Email</b></body>");
            try
            {
                client.Send(msg);
                return "OK";
            }
            catch (Exception ex)
            {

                return "error:" + ex.ToString();
            }
        }


    }
}
