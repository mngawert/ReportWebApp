using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.ViewModels
{
    public class DashboardReport1ViewModel
    {
        public string DestinationAddress { get; set; }
        public long TotalCount { get; set; }
    }
    public class DashboardReport1Request
    {
        public string Year { get; set; }
        public string DestinationAddress { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
