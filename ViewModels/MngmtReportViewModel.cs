using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.ViewModels
{
    public class MngmtReportViewModel
    {
        public string Id { get; set; }
        public string Month { get; set; }
        public long TotalCount { get; set; }
        public long SuccessCount { get; set; }
        public long FailCount { get; set; }
    }
    public class MngmtReportRequest
    {
        public string Year { get; set; }
        public string DestinationAddress { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
