using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportWebApp.Helper;
using ReportWebApp.Models;
using ReportWebApp.ViewModels;
using ReportWebApp.VOICEModels;

namespace ReportWebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MCNController : ControllerBase
    {
        private readonly TOT_VOICE_CDRContext _context;

        public MCNController(TOT_VOICE_CDRContext context)
        {
            _context = context;
        }


        public IQueryable<Report1ViewModel> QueryReport(TransCdr01RequestViewModel model, string id)
        {
            string sql = System.IO.File.ReadAllText(@".\SQL\MCN_GETREPORT.sql");
            sql = sql.Replace("[ID]", id);

            var q = _context.Report1ViewModel.FromSqlRaw(sql);

            if (model.MessageType != null)
            {
                q = q.Where(a => a.MessageType == model.MessageType);
            }
            if (!string.IsNullOrEmpty(model.OriginationAddress))
            {
                q = q.Where(a => a.OriginationAddress.Contains(model.OriginationAddress));
            }
            if (!string.IsNullOrEmpty(model.DestinationAddress))
            {
                q = q.Where(a => a.DestinationAddress.Contains(model.DestinationAddress));
            }
            if (!string.IsNullOrEmpty(model.StartDate))
            {
                var d = DateTime.Parse(model.StartDate);
                q = q.Where(a => a.DeliveryTime >= d);
            }
            if (!string.IsNullOrEmpty(model.EndDate))
            {
                var d = DateTime.Parse(model.EndDate);
                q = q.Where(a => a.DeliveryTime < d.AddDays(1));
            }
            if (model.MessageStatus != null)
            {
                q = q.Where(a => a.MessageStatus == model.MessageStatus);
            }

            return q;
        }

        [HttpPost]
        public IActionResult GetReport(TransCdr01RequestViewModel model)
        {
            var q = QueryReport(model, "01");
            for (int i = 2; i <= 12; i++)
            {
                q = q.Concat(QueryReport(model, i.ToString("D2")));
            }

            q = q.OrderByDescending(a => a.DeliveryTime);

            var qq = PaginatedList<Report1ViewModel>.Create(q, model.PageNumber ?? 1, model.PageSize ?? 10).GetPaginatedData();

            return Ok(qq);
        }

        public IQueryable<MngmtReportViewModel> QueryMngmtReport(MngmtReportRequest model, string id)
        {
            string sql = System.IO.File.ReadAllText(@".\SQL\MCN_GETMNGMTREPORT.sql");
            sql = sql.Replace("[ID]", id);

            var q = _context.MngmtReportViewModel.FromSqlRaw(sql, model.Year, model.DestinationAddress);

            return q;
        }

        [HttpPost]
        public IActionResult GetMngmtReport(MngmtReportRequest model)
        {
            var q = QueryMngmtReport(model, "01");
            for (int i = 2; i <= 12; i++)
            {
                q = q.Concat(QueryMngmtReport(model, i.ToString("D2")));
            }

            var qq = q
                .GroupBy(a => new { a.Id, a.Month })
                .Select(g => new MngmtReportViewModel
                {
                    Id = g.Key.Id,
                    Month = g.Key.Month,
                    FailCount = g.Sum(b => b.FailCount),
                    SuccessCount = g.Sum(b => b.SuccessCount),
                    TotalCount = g.Sum(b => b.TotalCount)
                });

            qq = qq.OrderBy(a => a.Id);

            return Ok(qq);
        }

        private IQueryable<DashboardReport1ViewModel> QueryDashboardReport1(DashboardReport1Request model, string id)
        {
            string sql = System.IO.File.ReadAllText(@".\SQL\MCN_GETDASHBOARDREPORT.sql");
            sql = sql.Replace("[ID]", id);

            var q = _context.DashboardReport1ViewModel.FromSqlRaw(sql, model.Year, model.Month, model.MessageStatus).Take(10);

            return q;
        }

        [HttpPost]
        public IActionResult GetDashboardReport1(DashboardReport1Request model)
        {
            var q = QueryDashboardReport1(model, "01");
            for (int i = 2; i <= 12; i++)
            {
                q = q.Concat(QueryDashboardReport1(model, i.ToString("D2")));
            }

            var qq = q
                .GroupBy(a => new { a.DestinationAddress })
                .Select(g => new DashboardReport1ViewModel
                {
                    DestinationAddress = g.Key.DestinationAddress,
                    TotalCount = g.Sum(b => b.TotalCount)
                });

            qq = qq.OrderByDescending(a => a.TotalCount);

            return Ok(qq);
        }

    }
}