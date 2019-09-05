using System;
using System.Collections.Generic;

namespace AzTGDataLayer.Models
{
    public partial class MasterStockList
    {
        public int InstrumentToken { get; set; }
        public string TradingSymbol { get; set; }
        public string Name { get; set; }
        public string Collection { get; set; }
        public bool? IsIncluded { get; set; }
    }
}
