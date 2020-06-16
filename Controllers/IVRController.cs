using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ReportWebApp.Controllers
{
    public class IVRController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Logs()
        {
            return View();
        }

        public IActionResult Summary()
        {
            return View();
        }

    }
}