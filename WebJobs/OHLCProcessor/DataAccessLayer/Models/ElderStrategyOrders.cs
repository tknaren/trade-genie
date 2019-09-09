using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class ElderStrategyOrders
    {
        public int InstrumentToken { get; set; }
        public string TradingSymbol { get; set; }
        public DateTime TradeDate { get; set; }
        public decimal? EntryPrice { get; set; }
        public decimal? ExitPrice { get; set; }
        public decimal? StopLoss { get; set; }
        public int? Quantity { get; set; }
        public decimal? ProfitLoss { get; set; }
        public DateTime EntryTime { get; set; }
        public DateTime? ExitTime { get; set; }
        public DateTime? StopLossHitTime { get; set; }
        public decimal? CurrOpen { get; set; }
        public decimal? CurrHigh { get; set; }
        public decimal? CurrLow { get; set; }
        public decimal? CurrClose { get; set; }
        public decimal? PrevOpen { get; set; }
        public decimal? PrevHigh { get; set; }
        public decimal? PrevLow { get; set; }
        public decimal? PrevClose { get; set; }
        public double? ShortEma { get; set; }
        public double? LongEma { get; set; }
        public double? ShortEmadev { get; set; }
        public double? LongEmadev { get; set; }
        public double? ForceIndex { get; set; }
        public string CurrHistogramMovement { get; set; }
        public string PrevHistogramMovement { get; set; }
        public string CurrImpulse { get; set; }
        public string PrevImpulse { get; set; }
        public string StockTrend { get; set; }
        public string PositionType { get; set; }
        public string PositionStatus { get; set; }
        public bool? IsRealOrderPlaced { get; set; }
        public string OrderId { get; set; }
        public string SlorderId { get; set; }
    }
}
