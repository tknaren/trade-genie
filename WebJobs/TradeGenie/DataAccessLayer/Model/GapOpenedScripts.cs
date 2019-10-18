using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public enum LevelType
    {
        Support,
        Resistance
    }

    public class Level
    {
        public string LevelName { get; set; }
        public double Price { get; set; }
    }

    public class GapOpenedScript
    {
        public string TradingSymbol { get; set; }
        public string Index { get; set; }
        public DateTime Yesterday { get; set; }
        public double YesterdayClose { get; set; }
        public double YesterdayHL { get; set; }
        public decimal TradedValue { get; set; }
        public DateTime Today { get; set; }
        public double TodayOpen { get; set; }
        public double TodayHL { get; set; }
        public double GapPer { get; set; }
        public string OrderType { get; set; }

        public LevelType Leveltype { get; set; }
        public List<Level> PriceLevels { get; set; }

        public double CMP { get; set; }
    }
}
