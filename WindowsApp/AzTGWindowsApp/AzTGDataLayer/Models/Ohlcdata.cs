using System;
using System.Collections.Generic;

namespace AzTGDataLayer.Models
{
    public partial class Ohlcdata
    {
        public int InstrumentToken { get; set; }
        public DateTime Ohlcdate { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public string TradingSymbol { get; set; }
        public decimal? PreviousClose { get; set; }
        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? LastPrice { get; set; }
    }
}
