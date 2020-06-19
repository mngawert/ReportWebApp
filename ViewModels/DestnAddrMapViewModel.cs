using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.ViewModels
{
    public class DestnAddrMapViewModel
    {
        public long DestnAddrId { get; set; }
        public string DestnAddrName { get; set; }
        public string DestnAddrValue { get; set; }
        public int? DestnAddrStatus { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }
    }
}
