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
        public double Level1 { get; set; }
        public double Level2 { get; set; }
        public double Level3 { get; set; }
        public double Level4 { get; set; }

        public double CMP { get; set; }
    }
}
