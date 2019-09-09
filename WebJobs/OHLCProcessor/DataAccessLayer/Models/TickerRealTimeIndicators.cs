using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class TickerRealTimeIndicators
    {
        public int TimePeriod { get; set; }
        public string StockCode { get; set; }
        public DateTime PeriodDateTime { get; set; }
        public double? Prie { get; set; }
        public double? ChangePercentage { get; set; }
        public double? TradedVolume { get; set; }
        public double? Ema1 { get; set; }
        public double? Ema2 { get; set; }
        public double? Ema3 { get; set; }
        public double? Ema4 { get; set; }
        public double? Macd { get; set; }
        public double? Signal { get; set; }
        public double? Histogram { get; set; }
        public double? Rsi1 { get; set; }
        public double? Rsi2 { get; set; }
        public double? ForceIndex { get; set; }
        public double? Obv { get; set; }
        public double? Atr { get; set; }
        public string Impulse { get; set; }
    }
}
