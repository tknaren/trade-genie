using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class GapStrategyOrder
    {
        public double AfterBrok { get; set; }
        public double? PNL { get; set; }
        public string StockCode { get; set; }
        public string Collection { get; set; }

        public string Position { get; set; }
        public DateTime BuyTime { get; set; }
        public DateTime SellTime { get; set; }
        public DateTime SLTime { get; set; }
        public double? BuyPrice { get; set; }
        public double? SellPrice { get; set; }
        public double? StopLoss { get; set; }
        public double? Target { get; set; }
        public int Quantity { get; set; }
        public double? MaxProfitAchieved { get; set; }
        public double? MaxProfitCanBeAchieved { get; set; }

        public double Open { get; set; }
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }

        public DateTime? Yesterday { get; set; }
        public double? YesterdayClose { get; set; }
        public DateTime? Today { get; set; }
        public double? TodayOpen { get; set; }
        public double? GapPer { get; set; }
    }
}
