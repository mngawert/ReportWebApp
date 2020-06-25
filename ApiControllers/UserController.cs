using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportWebApp.TOTVASModels;

namespace ReportWebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TOT_VASContext _context;

        public UserController(TOT_VASContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            var q = _context.User.Where(a => a.UserName == model.UserName && a.Password == Encrypt(model.Password)).FirstOrDefault();

            if (q == null)
                return NotFound();

            q.Password = "";

            return Ok(q);
        }

        [HttpPost]
        public ActionResult Create(User model)
        {
            var q = new User
            {
                UserName = model.UserName,
                Password = Encrypt(model.Password)
            };

            _context.Add(q).State = Microsoft.EntityFrameworkCore.EntityState.Added;
            _context.SaveChanges();

            return Ok(q);
        }

        [HttpPost]
        public ActionResult Update(User model)
        {
            var q = _context.User.Where(a => a.UserId == model.UserId).FirstOrDefault();
            if (q == null)
                return NotFound();

            q.UserName = model.UserName;
            q.Password = Encrypt(model.Password);

            _context.Update(q).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            var q = _context.User.Where(a => a.UserId == id).FirstOrDefault();

            if (q == null)
                return NotFound();

            _context.Remove(q).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            _context.SaveChanges();

            return Ok();
        }

        public string Encrypt(string inputString)
        {
            byte[] data = System.Text.Encoding.ASCII.GetBytes(inputString);
            data = new System.Security.Cryptography.SHA256Managed().ComputeHash(data);
            String hash = System.Text.Encoding.ASCII.GetString(data);

            return hash;
        }


    }
}