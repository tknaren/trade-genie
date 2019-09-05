using System;
using System.Collections.Generic;

namespace AzTGDataLayer.Models
{
    public partial class TickerRealTimeFo
    {
        public string StockCode { get; set; }
        public DateTime TickerDate { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public double? Open { get; set; }
        public double? High { get; set; }
        public double? Low { get; set; }
        public double? Ltp { get; set; }
        public double? Change { get; set; }
        public double? ChangePercentage { get; set; }
        public double? PreviousClose { get; set; }
        public double? DayEndClose { get; set; }
        public double? NetTurnOverInCr { get; set; }
        public double? TradedVolume { get; set; }
        public double? YearlyPercentageChange { get; set; }
        public double? MonthlyPercentageChange { get; set; }
    }
}
