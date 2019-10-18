//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Collections.Generic;
    
    public partial class ElderStrategyOrder
    {
        public int InstrumentToken { get; set; }
        public string TradingSymbol { get; set; }
        public System.DateTime TradeDate { get; set; }
        public Nullable<decimal> EntryPrice { get; set; }
        public Nullable<decimal> ExitPrice { get; set; }
        public Nullable<decimal> StopLoss { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> ProfitLoss { get; set; }
        public System.DateTime EntryTime { get; set; }
        public Nullable<System.DateTime> ExitTime { get; set; }
        public Nullable<System.DateTime> StopLossHitTime { get; set; }
        public Nullable<decimal> CurrOpen { get; set; }
        public Nullable<decimal> CurrHigh { get; set; }
        public Nullable<decimal> CurrLow { get; set; }
        public Nullable<decimal> CurrClose { get; set; }
        public Nullable<decimal> PrevOpen { get; set; }
        public Nullable<decimal> PrevHigh { get; set; }
        public Nullable<decimal> PrevLow { get; set; }
        public Nullable<decimal> PrevClose { get; set; }
        public Nullable<double> ShortEMA { get; set; }
        public Nullable<double> LongEMA { get; set; }
        public Nullable<double> ShortEMADev { get; set; }
        public Nullable<double> LongEMADev { get; set; }
        public Nullable<double> ForceIndex { get; set; }
        public string CurrHistogramMovement { get; set; }
        public string PrevHistogramMovement { get; set; }
        public string CurrImpulse { get; set; }
        public string PrevImpulse { get; set; }
        public string StockTrend { get; set; }
        public string PositionType { get; set; }
        public string PositionStatus { get; set; }
        public Nullable<bool> isRealOrderPlaced { get; set; }
        public string OrderId { get; set; }
        public string SLOrderId { get; set; }
    }
}
