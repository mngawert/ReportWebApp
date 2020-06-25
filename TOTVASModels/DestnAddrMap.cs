using System;
using System.Collections.Generic;

namespace ReportWebApp.TOTVASModels
{
    public partial class DestnAddrMap
    {
        public long DestnAddrId { get; set; }
        public string DestnAddrName { get; set; }
        public string DestnAddrValue { get; set; }
        public int DestnAddrStatus { get; set; }
        public string DestnAddrType { get; set; }
        public DateTimeOffset? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTimeOffset? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
