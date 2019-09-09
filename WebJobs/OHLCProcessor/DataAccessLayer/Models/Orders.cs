using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Orders
    {
        public string OrderId { get; set; }
        public string ExchangeOrderId { get; set; }
        public string ParentOrderId { get; set; }
        public int? InstrumentToken { get; set; }
        public string Exchange { get; set; }
        public DateTime? OrderTimestamp { get; set; }
        public DateTime? ExchangeTimestamp { get; set; }
        public string OrderType { get; set; }
        public string Tradingsymbol { get; set; }
        public string TransactionType { get; set; }
        public int? Quantity { get; set; }
        public int? CancelledQuantity { get; set; }
        public int? DisclosedQuantity { get; set; }
        public int? FilledQuantity { get; set; }
        public int? PendingQuantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? TriggerPrice { get; set; }
        public decimal? AveragePrice { get; set; }
        public string Product { get; set; }
        public string PlacedBy { get; set; }
        public string Validity { get; set; }
        public string Variety { get; set; }
        public string Tag { get; set; }
        public string Status { get; set; }
        public string StatusMessage { get; set; }
    }
}
