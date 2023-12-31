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
    
    public partial class Instrument
    {
        public int InstrumentToken { get; set; }
        public Nullable<int> ExchangeToken { get; set; }
        public string TradingSymbol { get; set; }
        public string Name { get; set; }
        public Nullable<decimal> LastPrice { get; set; }
        public Nullable<decimal> TickSize { get; set; }
        public string Expiry { get; set; }
        public string InstrumentType { get; set; }
        public string Segment { get; set; }
        public string Exchange { get; set; }
        public Nullable<decimal> Strike { get; set; }
        public Nullable<int> LotSize { get; set; }
        public Nullable<decimal> Margin { get; set; }
        public Nullable<decimal> COLower { get; set; }
        public Nullable<decimal> MISMultiplier { get; set; }
        public Nullable<decimal> COUpper { get; set; }
        public Nullable<decimal> NRMLMargin { get; set; }
        public Nullable<decimal> MISMargin { get; set; }
    
        public virtual Instrument Instruments1 { get; set; }
        public virtual Instrument Instrument1 { get; set; }
    }
}
