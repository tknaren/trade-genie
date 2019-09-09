using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class TickerMinElderIndicators
    {
        public string StockCode { get; set; }
        public DateTime TickerDateTime { get; set; }
        public int TimePeriod { get; set; }
        public double? PriceOpen { get; set; }
        public double? PriceHigh { get; set; }
        public double? PriceLow { get; set; }
        public double? PriceClose { get; set; }
        public int? Volume { get; set; }
        public double? Change { get; set; }
        public double? ChangePercent { get; set; }
        public decimal? TradedValue { get; set; }
        public double? Ema1 { get; set; }
        public double? Ema2 { get; set; }
        public double? Ema3 { get; set; }
        public double? Ema4 { get; set; }
        public double? Macd { get; set; }
        public double? Signal { get; set; }
        public double? Histogram { get; set; }
        public string HistIncDec { get; set; }
        public string Impulse { get; set; }
        public double? ForceIndex1 { get; set; }
        public double? ForceIndex2 { get; set; }
        public double? Ema1dev { get; set; }
        public double? Ema2dev { get; set; }
        public double? Ag1 { get; set; }
        public double? Al1 { get; set; }
        public double? Rsi1 { get; set; }
        public double? Ag2 { get; set; }
        public double? Al2 { get; set; }
        public double? Rsi2 { get; set; }
    }
}
