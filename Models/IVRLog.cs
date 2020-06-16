using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.Models
{
    public class IVRLog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }

    }


    public class IVRLogViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

    }

}
