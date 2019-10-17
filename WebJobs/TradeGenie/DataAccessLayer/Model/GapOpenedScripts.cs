using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Model
{
    public class GapOpenedScripts
    {
        public string TradingSymbol { get; set; }
        public string Collection { get; set; }
        public DateTime Yesterday { get; set; }
        public double PriceClose { get; set; }
        public DateTime Today { get; set; }
        public double Open { get; set; }
        public double GapPer { get; set; }
    }
}
