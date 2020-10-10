using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportWebApp.ViewModels
{
    public class Report1ViewModel
    {
        //public long SessionId { get; set; }
        public string TransactionId { get; set; }
        public DateTimeOffset? DeliveryTime { get; set; }
        public string DeliveryTimeText { get; set; }
        public int? MessageStatus { get; set; }
        public string OriginationAddress { get; set; }
        public string DestinationAddress { get; set; }
    }

}
