using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ReportWebApp.Controllers
{
    public class WebLogsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}