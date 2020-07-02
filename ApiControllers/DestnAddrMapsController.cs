using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportWebApp.Helper;
using ReportWebApp.Models;
using ReportWebApp.TOTVASModels;
using ReportWebApp.ViewModels;

namespace ReportWebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DestnAddrMapsController : ControllerBase
    {
        private readonly TOTVASContext _context;

        public DestnAddrMapsController(TOTVASContext context)
        {
            _context = context;
        }

        // GET: api/DestnAddrMaps
        [HttpPost]
        public ActionResult GetDestnAddrMap(DestnAddrMapViewModel model)
        {
            var q = _context.DestnAddrMap as IQueryable<DestnAddrMap>;

            if (!string.IsNullOrEmpty(model.DestnAddrName))
            {
                q = q.Where(a => a.DestnAddrName.Contains(model.DestnAddrName));
            }
            if (!string.IsNullOrEmpty(model.DestnAddrValue))
            {
                q = q.Where(a => a.DestnAddrValue.Contains(model.DestnAddrValue));
            }
            if (model.DestnAddrStatus != null)
            {
                q = q.Where(a => a.DestnAddrStatus == model.DestnAddrStatus);
            }
            if (!string.IsNullOrEmpty(model.DestnAddrType))
            {
                q = q.Where(a => a.DestnAddrType.Contains(model.DestnAddrType));
            }

            var qq = PaginatedList<DestnAddrMap>.Create(q, model.PageNumber ?? 1, model.PageSize ?? 10).GetPaginatedData();

            return Ok(qq);
        }

        // GET: api/DestnAddrMaps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DestnAddrMap>> GetDestnAddrMap(long id)
        {
            var destnAddrMap = await _context.DestnAddrMap.FindAsync(id);

            if (destnAddrMap == null)
            {
                return NotFound();
            }

            return destnAddrMap;
        }

        // PUT: api/DestnAddrMaps/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<IActionResult> UpdateDestnAddrMap(DestnAddrMap model)
        {
            var q = _context.DestnAddrMap.Where(a => a.DestnAddrId == model.DestnAddrId).FirstOrDefault();
            if (q != null)
            {
                q.DestnAddrName = model.DestnAddrName;
                q.DestnAddrValue = model.DestnAddrValue;
                q.DestnAddrStatus = model.DestnAddrStatus;
                q.DestnAddrType = model.DestnAddrType;

                _context.Entry(q).State = EntityState.Modified;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestnAddrMapExists(model.DestnAddrId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        // POST: api/DestnAddrMaps
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<DestnAddrMap>> CreateDestnAddrMap(DestnAddrMapViewModel model)
        {

            var q = new DestnAddrMap
            {
                DestnAddrName = model.DestnAddrName,
                DestnAddrValue = model.DestnAddrValue,
                DestnAddrStatus = model.DestnAddrStatus.Value,
                DestnAddrType = model.DestnAddrType,
            };

            _context.DestnAddrMap.Add(q);
            await _context.SaveChangesAsync();

            return Ok(q);
        }

        // DELETE: api/DestnAddrMaps/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<DestnAddrMap>> DeleteDestnAddrMap(long id)
        {
            var destnAddrMap = await _context.DestnAddrMap.FindAsync(id);
            if (destnAddrMap == null)
            {
                return NotFound();
            }

            _context.DestnAddrMap.Remove(destnAddrMap);
            await _context.SaveChangesAsync();

            return destnAddrMap;
        }

        private bool DestnAddrMapExists(long id)
        {
            return _context.DestnAddrMap.Any(e => e.DestnAddrId == id);
        }
    }
}
