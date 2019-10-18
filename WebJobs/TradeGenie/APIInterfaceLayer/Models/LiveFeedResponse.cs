using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIInterfaceLayer.Models
{
    public class LTPData
    {
        public long timestamp { get; set; }
        public string exchange { get; set; }
        public string symbol { get; set; }
        public double ltp { get; set; }
        public double close { get; set; }
    }

    public class LiveFeedLTP
    {
        public int code { get; set; }
        public string status { get; set; }
        public DateTime timestamp { get; set; }
        public string message { get; set; }
        public LTPData data { get; set; }
        public Error error { get; set; }
    }


    public class FullData
    {
        public long timestamp { get; set; }
        public string exchange { get; set; }
        public string symbol { get; set; }
        public double ltp { get; set; }
        public int open { get; set; }
        public double high { get; set; }
        public int low { get; set; }
        public double close { get; set; }
        public int vtt { get; set; }
        public double atp { get; set; }
        public int oi { get; set; }
        public double spot_price { get; set; }
        public int total_buy_qty { get; set; }
        public int total_sell_qty { get; set; }
        public double lower_circuit { get; set; }
        public double upper_circuit { get; set; }
        public List<Bid> bids { get; set; }
        public List<Ask> asks { get; set; }
        public long ltt { get; set; }
    }

    public class Bid
    {
        public int quantity { get; set; }
        public double price { get; set; }
        public int orders { get; set; }
    }

    public class Ask
    {
        public int quantity { get; set; }
        public double price { get; set; }
        public int orders { get; set; }
    }


    public class LiveFeedFull
    {
        public int code { get; set; }
        public string status { get; set; }
        public DateTime timestamp { get; set; }
        public string message { get; set; }
        public FullData data { get; set; }
    }
}
