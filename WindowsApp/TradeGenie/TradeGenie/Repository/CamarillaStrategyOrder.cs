//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TradeGenie.Repository
{
    using System;
    using System.Collections.Generic;
    
    public partial class CamarillaStrategyOrder
    {
        public int ID { get; set; }
        public int InstrumentToken { get; set; }
        public string TradingSymbol { get; set; }
        public System.DateTime TradeDate { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> EntryPrice { get; set; }
        public Nullable<decimal> ExitPrice { get; set; }
        public Nullable<decimal> SLPrice { get; set; }
        public Nullable<decimal> BOTargetValue { get; set; }
        public Nullable<decimal> BOSLValue { get; set; }
        public Nullable<decimal> BOTSLValue { get; set; }
        public Nullable<System.DateTime> EntryTime { get; set; }
        public Nullable<System.DateTime> ExitTime { get; set; }
        public Nullable<System.DateTime> SLHitTime { get; set; }
        public string PositionType { get; set; }
        public string OrderVariety { get; set; }
        public Nullable<bool> isRealOrderPlaced { get; set; }
        public string PositionStatus { get; set; }
        public string SLOrderStatus { get; set; }
        public string TargetOrderStatus { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<int> SLOrderId { get; set; }
        public Nullable<int> TargetOrderId { get; set; }
        public Nullable<decimal> ProfitLoss { get; set; }
        public Nullable<decimal> PLAfterBrok { get; set; }
        public string Camarilla { get; set; }
        public Nullable<decimal> H1 { get; set; }
        public Nullable<decimal> H2 { get; set; }
        public Nullable<decimal> H3 { get; set; }
        public Nullable<decimal> H4 { get; set; }
        public Nullable<decimal> H5 { get; set; }
        public Nullable<decimal> H6 { get; set; }
        public Nullable<decimal> L1 { get; set; }
        public Nullable<decimal> L2 { get; set; }
        public Nullable<decimal> L3 { get; set; }
        public Nullable<decimal> L4 { get; set; }
        public Nullable<decimal> L5 { get; set; }
        public Nullable<decimal> L6 { get; set; }
        public Nullable<decimal> PDClose { get; set; }
        public Nullable<decimal> PDHigh { get; set; }
        public Nullable<decimal> PDLow { get; set; }
        public Nullable<decimal> CDOpen { get; set; }
    }
}
