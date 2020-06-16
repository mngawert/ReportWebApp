using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReportWebApp.Models;
using ReportWebApp.ViewModels;

namespace ReportWebApp.Controllers
{
    public class USSDController : Controller
    {
        private readonly TOT_USSD_CDRContext _USSDcontext;

        public USSDController(TOT_USSD_CDRContext uSSDcontext)
        {
            _USSDcontext = uSSDcontext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logs()
        {
            //var q = _USSDcontext.TransCdr01.ToList();
            //var q = _USSDcontext.TransCdr01.Select(a => new TransCdr01ViewModel { SessionId = a.SessionId, DeliveryTime = a.DeliveryTime }).ToList();

            return View();
        }

        public IActionResult Summary()
        {
            return View();
        }

    }
}