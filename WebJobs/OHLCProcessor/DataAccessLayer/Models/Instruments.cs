using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Instruments
    {
        public int InstrumentToken { get; set; }
        public int? ExchangeToken { get; set; }
        public string TradingSymbol { get; set; }
        public string Name { get; set; }
        public decimal? LastPrice { get; set; }
        public decimal? TickSize { get; set; }
        public string Expiry { get; set; }
        public string InstrumentType { get; set; }
        public string Segment { get; set; }
        public string Exchange { get; set; }
        public decimal? Strike { get; set; }
        public int? LotSize { get; set; }
        public decimal? Margin { get; set; }
        public decimal? Colower { get; set; }
        public decimal? Mismultiplier { get; set; }
        public decimal? Coupper { get; set; }
        public decimal? Nrmlmargin { get; set; }
        public decimal? Mismargin { get; set; }
    }
}
