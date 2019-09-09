using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class Quotes
    {
        public int InstrumentToken { get; set; }
        public DateTime LastUpdatedTime { get; set; }
        public int? Volume { get; set; }
        public int? LastQuantity { get; set; }
        public decimal? Change { get; set; }
        public decimal? ChangePercent { get; set; }
        public decimal? OpenInterest { get; set; }
        public int? BuyQuantity { get; set; }
        public int? SellQuantity { get; set; }
        public decimal? LastPrice { get; set; }
        public decimal? Open { get; set; }
        public decimal? Close { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
    }
}
