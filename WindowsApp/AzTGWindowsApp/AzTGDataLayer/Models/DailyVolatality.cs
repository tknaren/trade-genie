using System;
using System.Collections.Generic;

namespace AzTGDataLayer.Models
{
    public partial class DailyVolatality
    {
        public DateTime RunDate { get; set; }
        public string Symbol { get; set; }
        public double? ClosePrice { get; set; }
        public double? PrevClosePrice { get; set; }
        public double? LogReturns { get; set; }
        public double? PrevVolatality { get; set; }
        public double? CurrVolatality { get; set; }
        public double? AnnualVolatality { get; set; }
    }
}
