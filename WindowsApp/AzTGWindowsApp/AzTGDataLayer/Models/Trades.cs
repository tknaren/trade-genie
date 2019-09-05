using System;
using System.Collections.Generic;

namespace AzTGDataLayer.Models
{
    public partial class Trades
    {
        public string TradeId { get; set; }
        public string OrderId { get; set; }
        public string ExchangeOrderId { get; set; }
        public string TradingSymbol { get; set; }
        public string Exchange { get; set; }
        public int InstrumentToken { get; set; }
        public string TransactionType { get; set; }
        public string Product { get; set; }
        public decimal AveragePrice { get; set; }
        public int Quantity { get; set; }
        public DateTime? OrderTimestamp { get; set; }
        public DateTime? ExchangeTimestamp { get; set; }
    }
}
