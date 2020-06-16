using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportWebApp.Helper;
using ReportWebApp.Models;
using ReportWebApp.IVRModels;
using ReportWebApp.ViewModels;

namespace ReportWebApp.ApiControllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IVRController : ControllerBase
    {
        private readonly TOT_IVR_CDRContext _IVRcontext;

        public IVRController(TOT_IVR_CDRContext IVRcontext)
        {
            _IVRcontext = IVRcontext;
        }

        [HttpPost]
        public IActionResult GetIVRLogs(TransCdr01RequestViewModel model)
        {

            var q = _IVRcontext.TransCdr01
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

            var qq = PaginatedList<TransCdr01ViewModel>.Create(q, model.PageNumber ?? 1, model.PageSize ?? 10).GetPaginatedData();

            return Ok(qq);
        }

    }
}