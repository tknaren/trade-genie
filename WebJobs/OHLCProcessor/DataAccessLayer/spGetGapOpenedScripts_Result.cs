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
    
    public partial class spGetGapOpenedScripts_Result
    {
        public string StockCode { get; set; }
        public string Collection { get; set; }
        public Nullable<System.DateTime> Yesterday { get; set; }
        public Nullable<double> PriceClose { get; set; }
        public Nullable<System.DateTime> Today { get; set; }
        public Nullable<double> PriceOpen { get; set; }
        public Nullable<double> GapPer { get; set; }
        public Nullable<double> Target { get; set; }
        public Nullable<double> Final { get; set; }
        public string IsProfit { get; set; }
    }
}