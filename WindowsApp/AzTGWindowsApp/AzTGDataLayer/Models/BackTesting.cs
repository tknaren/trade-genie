using System;
using System.Collections.Generic;

namespace AzTGDataLayer.Models
{
    public partial class BackTesting
    {
        public int TransactionId { get; set; }
        public string StockCode { get; set; }
        public DateTime? BuyDateTime { get; set; }
        public DateTime? SellDateTime { get; set; }
        public string BuyType { get; set; }
        public string SellType { get; set; }
        public double? BuyPrice { get; set; }
        public double? SellPrice { get; set; }
        public int? Qty { get; set; }
        public double? BuyAmount { get; set; }
        public double? SellAmount { get; set; }
        public double? ProfitLoss { get; set; }
    }
}
