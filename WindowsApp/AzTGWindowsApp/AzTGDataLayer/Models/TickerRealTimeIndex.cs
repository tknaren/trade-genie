using System;
using System.Collections.Generic;

namespace AzTGDataLayer.Models
{
    public partial class TickerRealTimeIndex
    {
        public string IndexName { get; set; }
        public DateTime LastTickerTime { get; set; }
        public double? Open { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public double? Ltp { get; set; }
        public double? Change { get; set; }
        public double? ChgPercentage { get; set; }
        public double? YearlyChange { get; set; }
        public double? MonthlyChange { get; set; }
        public double? YearlyHigh { get; set; }
        public double? YearlyLow { get; set; }
        public int? Advances { get; set; }
        public int? Declines { get; set; }
        public int? Unchanged { get; set; }
    }
}
