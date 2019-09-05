using System;
using System.Collections.Generic;

namespace AzTGDataLayer.Models
{
    public partial class TickerMin
    {
        public int InstrumentToken { get; set; }
        public string TradingSymbol { get; set; }
        public DateTime DateTime { get; set; }
        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Close { get; set; }
        public int? Volume { get; set; }
    }
}
