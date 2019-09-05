using System;
using System.Collections.Generic;

namespace AzTGDataLayer.Models
{
    public partial class MorningBreakouts
    {
        public int InstrumentToken { get; set; }
        public DateTime DateTimePeriod { get; set; }
        public string TradingSymbol { get; set; }
        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Close { get; set; }
        public decimal? Ltp { get; set; }
        public decimal? CandleSize { get; set; }
        public decimal? DayHigh { get; set; }
        public decimal? DayLow { get; set; }
        public int? Quantity { get; set; }
        public decimal? Entry { get; set; }
        public DateTime? EntryTime { get; set; }
        public decimal? Exit { get; set; }
        public DateTime? ExitTime { get; set; }
        public decimal? StopLoss { get; set; }
        public DateTime? StopLossHitTime { get; set; }
        public decimal? ProfitLoss { get; set; }
        public string Ragstatus { get; set; }
        public string Movement { get; set; }
        public bool? IsRealOrderPlaced { get; set; }
    }
}
