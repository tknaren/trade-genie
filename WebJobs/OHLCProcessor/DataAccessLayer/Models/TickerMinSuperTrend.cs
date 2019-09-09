using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class TickerMinSuperTrend
    {
        public string StockCode { get; set; }
        public DateTime TickerDateTime { get; set; }
        public int TimePeriod { get; set; }
        public double? PriceOpen { get; set; }
        public double? PriceHigh { get; set; }
        public double? PriceLow { get; set; }
        public double? PriceClose { get; set; }
        public int? Volume { get; set; }
        public double? TrueRange { get; set; }
        public double? Atr1 { get; set; }
        public double? Atr2 { get; set; }
        public double? Atr3 { get; set; }
        public double? Bub1 { get; set; }
        public double? Bub2 { get; set; }
        public double? Bub3 { get; set; }
        public double? Blb1 { get; set; }
        public double? Blb2 { get; set; }
        public double? Blb3 { get; set; }
        public double? Fub1 { get; set; }
        public double? Fub2 { get; set; }
        public double? Fub3 { get; set; }
        public double? Flb1 { get; set; }
        public double? Flb2 { get; set; }
        public double? Flb3 { get; set; }
        public double? St1 { get; set; }
        public double? St2 { get; set; }
        public double? St3 { get; set; }
        public string Trend1 { get; set; }
        public string Trend2 { get; set; }
        public string Trend3 { get; set; }
    }
}
