using System;
using System.Collections.Generic;

namespace ReportWebApp.IVRModels
{
    public partial class TransCdr01
    {
        public long SessionId { get; set; }
        public long TransactionId { get; set; }
        public DateTimeOffset ReceiptTimestamp { get; set; }
        public DateTimeOffset DeliveryTime { get; set; }
        public string SubscriberType { get; set; }
        public int? MessageStatus { get; set; }
        public int? MessageType { get; set; }
        public string ChargeStatus { get; set; }
        public string AmountDeducted { get; set; }
        public string Taskid { get; set; }
        public string TraversalPath { get; set; }
        public int? NodeId { get; set; }
        public DateTimeOffset AppRequest { get; set; }
        public DateTimeOffset AppResponse { get; set; }
        public string AppCause { get; set; }
        public string AppInfo { get; set; }
        public string AppKey { get; set; }
        public int? DataLength { get; set; }
        public string UserData { get; set; }
        public int? DataCodingScheme { get; set; }
        public int? ServiceId { get; set; }
        public string InitiationDirection { get; set; }
        public string ServerId { get; set; }
        public int? SequenceNo { get; set; }
        public int MenuLevel { get; set; }
        public int InterfaceId { get; set; }
        public string OriginationAddress { get; set; }
        public string DestinationAddress { get; set; }
        public string Vlr { get; set; }
        public string FeatureId { get; set; }
        public long? Cgi { get; set; }
        public int? MonthIndex { get; set; }
    }
}
