using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportWebApp.Helper;
using ReportWebApp.Models;
using ReportWebApp.USSDModels;
using ReportWebApp.ViewModels;

namespace ReportWebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class USSDController : ControllerBase
    {
        private readonly TOT_USSD_CDRContext _USSDcontext;

        public USSDController(TOT_USSD_CDRContext uSSDcontext)
        {
            _USSDcontext = uSSDcontext;
        }

        [HttpPost]
        public IActionResult GetUSSDLogs(TransCdr01RequestViewModel model)
        {

            string sql = @"
                            SELECT  Session_Id as SessionId,
                                    Transaction_Id as TransactionId,
                                    Delivery_Time as DeliveryTime,
                                    Origination_Address as OriginationAddress,
                                    Destination_Address as DestinationAddress, 
                                    Message_Status as MessageStatus 
                            FROM TRANS_CDR_01";
            
            var q = _USSDcontext.Report1ViewModel.FromSqlRaw(sql);

            //var q = _USSDcontext.TransCdr01
            //    .Select(a => new TransCdr01ViewModel
            //    {
            //        SessionId = a.SessionId,
            //        TransactionId = a.TransactionId,
            //        DeliveryTime = a.DeliveryTime,
            //        OriginationAddress = a.OriginationAddress,
            //        DestinationAddress = a.DestinationAddress,
            //        MessageStatus = a.MessageStatus
            //    });

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
                q = q.Where(a => (a.MessageStatus == 1 ? 1 : 2) == model.MessageStatus);
            }

            var qq = PaginatedList<Report1ViewModel>.Create(q, model.PageNumber ?? 1, model.PageSize ?? 10).GetPaginatedData();

            return Ok(qq);
        }



        [HttpPost]
        public IActionResult GetMngmtReport(MngmtReportRequest model)
        {

            string sql = @"
                            select date_format(a.delivery_time, '%Y-%m') as Id, date_format(a.delivery_time, '%M') as Month, count(1) as TotalCount, sum( case when message_status = 1 then 1 else 0 end) as SuccessCount, sum( case when message_status = 1 then 0 else 1 end) as FailCount
                            from trans_cdr_01 a
                            where date_format(a.delivery_time, '%Y') = {0}
                            and destination_address = {1}
                            group by date_format(a.delivery_time, '%Y-%m'), date_format(a.delivery_time, '%M')
                            order by 1
                            ";

            var q = _USSDcontext.MngmtReportViewModel.FromSqlRaw(sql, model.Year, model.DestinationAddress);

            return Ok(q);
        }

    }
}