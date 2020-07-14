using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportWebApp.Helper;
using ReportWebApp.TOTVASModels;

namespace ReportWebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly TOTVASContext _context;

        public UserController(TOTVASContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            var q = _context.User.Where(a => a.UserName == model.UserName && a.Password == Encrypt(model.Password) && a.UserStatus == 1).FirstOrDefault();

            if (q == null)
                return NotFound();

            q.Password = "";

            return Ok(q);
        }

        [HttpPost]
        public ActionResult GetUsers(UserViewModel model)
        {
            var q = _context.User.Select(a => new User { UserId = a.UserId, UserName = a.UserName, UserGroup = a.UserGroup, UserStatus = a.UserStatus  } ) as IQueryable<User>;

            if (!string.IsNullOrEmpty(model.UserName))
            {
                q = q.Where(a => a.UserName.Contains(model.UserName));
            }
            if (!string.IsNullOrEmpty(model.UserGroup))
            {
                q = q.Where(a => a.UserGroup == model.UserGroup);
            }
            if (model.UserStatus != null)
            {
                q = q.Where(a => a.UserStatus == model.UserStatus);
            }

            var qq = PaginatedList<User>.Create(q, model.PageNumber ?? 1, model.PageSize ?? 10).GetPaginatedData();

            return Ok(qq);
        }

        [HttpGet("{id}")]
        public ActionResult GetUser(long id)
        {
            var q = _context.User.Find(id);

            q.Password = "";

            return Ok(q);
        }

        [HttpPost]
        public ActionResult Create(User model)
        {
            var q = new User
            {
                UserName = model.UserName,
                Password = Encrypt(model.Password),
                UserGroup = model.UserGroup,
                UserStatus = model.UserStatus
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
            //q.Password = Encrypt(model.Password);
            q.UserGroup = model.UserGroup;
            q.UserStatus = model.UserStatus;

            _context.Update(q).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        [HttpPost]
        public ActionResult ChangePassword(User model)
        {
            var q = _context.User.Where(a => a.UserId == model.UserId).FirstOrDefault();
            if (q == null)
                return NotFound();

            q.Password = Encrypt(model.Password);

            _context.Update(q).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete("{id}")]
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