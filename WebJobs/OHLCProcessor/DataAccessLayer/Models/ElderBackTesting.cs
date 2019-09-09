using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class ElderBackTesting
    {
        public int InstrumentToken { get; set; }
        public string TradingSymbol { get; set; }
        public DateTime TradeDate { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public double? EntryPrice { get; set; }
        public double? ExitPrice { get; set; }
        public string OrderType { get; set; }
        public int? Quantity { get; set; }
        public double? Ema1 { get; set; }
        public double? Ema2 { get; set; }
        public double? Ema1dev { get; set; }
        public double? Ema2dev { get; set; }
        public double? ForceIndex { get; set; }
        public double? Histogram { get; set; }
        public string HistogramMovement { get; set; }
        public string Impulse { get; set; }
    }
}
