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
    
    public partial class TickerMinElderIndicator
    {
        public string StockCode { get; set; }
        public System.DateTime TickerDateTime { get; set; }
        public int TimePeriod { get; set; }
        public Nullable<double> PriceOpen { get; set; }
        public Nullable<double> PriceHigh { get; set; }
        public Nullable<double> PriceLow { get; set; }
        public Nullable<double> PriceClose { get; set; }
        public Nullable<int> Volume { get; set; }
        public Nullable<double> EMA1 { get; set; }
        public Nullable<double> EMA2 { get; set; }
        public Nullable<double> EMA3 { get; set; }
        public Nullable<double> EMA4 { get; set; }
        public Nullable<double> MACD { get; set; }
        public Nullable<double> Signal { get; set; }
        public Nullable<double> Histogram { get; set; }
        public string HistIncDec { get; set; }
        public string Impulse { get; set; }
        public Nullable<double> ForceIndex1 { get; set; }
        public Nullable<double> ForceIndex2 { get; set; }
        public Nullable<double> EMA1Dev { get; set; }
        public Nullable<double> EMA2Dev { get; set; }
        public Nullable<double> AG1 { get; set; }
        public Nullable<double> AL1 { get; set; }
        public Nullable<double> RSI1 { get; set; }
        public Nullable<double> AG2 { get; set; }
        public Nullable<double> AL2 { get; set; }
        public Nullable<double> RSI2 { get; set; }
        public Nullable<double> Change { get; set; }
        public Nullable<double> ChangePercent { get; set; }
        public Nullable<decimal> TradedValue { get; set; }
    }
}
