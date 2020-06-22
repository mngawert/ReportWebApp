using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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

            var q = _USSDcontext.TransCdr01
                .Select(a => new TransCdr01ViewModel
                {
                    SessionId = a.SessionId,
                    TransactionId = a.TransactionId,
                    DeliveryTime = a.DeliveryTime,
                    OriginationAddress = a.OriginationAddress,
                    DestinationAddress = a.DestinationAddress,
                    MessageStatus = a.MessageStatus
                });

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

            var qq = PaginatedList<TransCdr01ViewModel>.Create(q, model.PageNumber ?? 1, model.PageSize ?? 10).GetPaginatedData();

            return Ok(qq);
        }

    }
}