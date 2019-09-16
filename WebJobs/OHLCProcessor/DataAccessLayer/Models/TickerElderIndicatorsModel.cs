using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class TickerElderIndicatorsModel
    {
        public string StockCode { get; set; }
        public DateTime TickerDateTime { get; set; }
        public int? TimePeriod { get; set; }

        public double? PriceOpen { get; set; }
        public double? PriceHigh { get; set; }
        public double? PriceLow { get; set; }
        public double? PriceClose { get; set; }

        public int? Volume { get; set; }

        public double? Change { get; set; }
        public double? ChangePercent { get; set; }
        public decimal? TradedValue { get; set; }

        public double? EMA1 { get; set; }
        public double? EMA2 { get; set; }
        public double? EMA3 { get; set; }
        public double? EMA4 { get; set; }

        public double? MACD { get; set; }
        public double? Signal { get; set; }

        public double? Histogram { get; set; }
        public string HistIncDec { get; set; }

        public string Impulse { get; set; }

        public double? ForceIndex1 { get; set; }
        public double? ForceIndex2 { get; set; }

        public double? EMA1Dev { get; set; }
        public double? EMA2Dev { get; set; }

        public double? AG1 { get; set; }
        public double? AL1 { get; set; }
        public double? RSI1 { get; set; }
        public double? AG2 { get; set; }
        public double? AL2 { get; set; }
        public double? RSI2 { get; set; }

        public double? TrueRange { get; set; }
        public double? ATR1 { get; set; }
        public double? ATR2 { get; set; }
        public double? ATR3 { get; set; }
        public double? BUB1 { get; set; }
        public double? BUB2 { get; set; }
        public double? BUB3 { get; set; }
        public double? BLB1 { get; set; }
        public double? BLB2 { get; set; }
        public double? BLB3 { get; set; }
        public double? FUB1 { get; set; }
        public double? FUB2 { get; set; }
        public double? FUB3 { get; set; }
        public double? FLB1 { get; set; }
        public double? FLB2 { get; set; }
        public double? FLB3 { get; set; }
        public double? ST1 { get; set; }
        public double? ST2 { get; set; }
        public double? ST3 { get; set; }
        public string Trend1 { get; set; }
        public string Trend2 { get; set; }
        public string Trend3 { get; set; }

        public double? EHEMA1 { get; set; }
        public double? EHEMA2 { get; set; }
        public double? EHEMA3 { get; set; }
        public double? EHEMA4 { get; set; }
        public double? EHEMA5 { get; set; }
        public double? VWMA1 { get; set; }
        public double? VWMA2 { get; set; }
        public double? HAOpen { get; set; }
        public double? HAHigh { get; set; }
        public double? HALow { get; set; }
        public double? HAClose { get; set; }
        public double? varEMA1v2 { get; set; }
        public double? varEMA1v3 { get; set; }
        public double? varEMA1v4 { get; set; }
        public double? varEMA2v3 { get; set; }
        public double? varEMA2v4 { get; set; }
        public double? varEMA3v4 { get; set; }
        public double? varEMA4v5 { get; set; }
        public double? varVWMA1vVWMA2 { get; set; }
        public double? varVWMA1vPriceClose { get; set; }
        public double? varVWMA2vPriceClose { get; set; }
        public double? varVWMA1vEMA1 { get; set; }
        public double? varHAOvHAC { get; set; }
        public double? varHAOvHAPO { get; set; }
        public double? varHACvHAPC { get; set; }
        public double? varOvC { get; set; }
        public double? varOvPO { get; set; }
        public double? varCvPC { get; set; }
        public string HAOCwEMA1 { get; set; }
        public string OCwEMA1 { get; set; }
        public int? AllEMAsInNum { get; set; }

    }
}
