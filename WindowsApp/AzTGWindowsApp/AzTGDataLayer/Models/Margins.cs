using System;
using System.Collections.Generic;

namespace AzTGDataLayer.Models
{
    public partial class Margins
    {
        public DateTime MarginDate { get; set; }
        public bool? Enabled { get; set; }
        public decimal? Net { get; set; }
        public decimal? AdhocMargin { get; set; }
        public decimal? Collateral { get; set; }
        public decimal? IntradayPayin { get; set; }
        public decimal? Cash { get; set; }
        public decimal? M2munrealised { get; set; }
        public decimal? M2mrealised { get; set; }
        public decimal? Debits { get; set; }
        public decimal? Span { get; set; }
        public decimal? OptionPremium { get; set; }
        public decimal? HoldingSales { get; set; }
        public decimal? Exposure { get; set; }
        public decimal? Turnover { get; set; }
    }
}
