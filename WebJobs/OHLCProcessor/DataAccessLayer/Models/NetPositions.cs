using System;
using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public partial class NetPositions
    {
        public int InstrumentToken { get; set; }
        public DateTime PositionDate { get; set; }
        public string TradingSymbol { get; set; }
        public decimal? AveragePrice { get; set; }
        public decimal? BuyM2m { get; set; }
        public decimal? BuyPrice { get; set; }
        public int? BuyQuantity { get; set; }
        public decimal? BuyValue { get; set; }
        public decimal? ClosePrice { get; set; }
        public string Exchange { get; set; }
        public decimal? LastPrice { get; set; }
        public decimal? M2m { get; set; }
        public decimal? Multiplier { get; set; }
        public decimal? NetBuyAmountM2m { get; set; }
        public decimal? NetSellAmountM2m { get; set; }
        public int? OvernightQuantity { get; set; }
        public decimal? Pnl { get; set; }
        public string Product { get; set; }
        public int? Quantity { get; set; }
        public decimal? Realised { get; set; }
        public decimal? SellM2m { get; set; }
        public decimal? SellPrice { get; set; }
        public int? SellQuantity { get; set; }
        public decimal? SellValue { get; set; }
        public decimal? Unrealised { get; set; }
        public decimal? Value { get; set; }
    }
}
