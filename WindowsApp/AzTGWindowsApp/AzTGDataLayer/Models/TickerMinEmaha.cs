using System;
using System.Collections.Generic;

namespace AzTGDataLayer.Models
{
    public partial class TickerMinEmaha
    {
        public string StockCode { get; set; }
        public DateTime TickerDateTime { get; set; }
        public int TimePeriod { get; set; }
        public double? PriceOpen { get; set; }
        public double? PriceHigh { get; set; }
        public double? PriceLow { get; set; }
        public double? PriceClose { get; set; }
        public int? Volume { get; set; }
        public double? Ehema1 { get; set; }
        public double? Ehema2 { get; set; }
        public double? Ehema3 { get; set; }
        public double? Ehema4 { get; set; }
        public double? Ehema5 { get; set; }
        public double? Vwma1 { get; set; }
        public double? Vwma2 { get; set; }
        public double? Haopen { get; set; }
        public double? Hahigh { get; set; }
        public double? Halow { get; set; }
        public double? Haclose { get; set; }
        public double? VarEma1v2 { get; set; }
        public double? VarEma1v3 { get; set; }
        public double? VarEma1v4 { get; set; }
        public double? VarEma2v3 { get; set; }
        public double? VarEma2v4 { get; set; }
        public double? VarEma3v4 { get; set; }
        public double? VarEma4v5 { get; set; }
        public double? VarVwma1vVwma2 { get; set; }
        public double? VarVwma1vPriceClose { get; set; }
        public double? VarVwma2vPriceClose { get; set; }
        public double? VarVwma1vEma1 { get; set; }
        public double? VarHaovHac { get; set; }
        public double? VarHaovHapo { get; set; }
        public double? VarHacvHapc { get; set; }
        public double? VarOvC { get; set; }
        public double? VarOvPo { get; set; }
        public double? VarCvPc { get; set; }
        public string HaocwEma1 { get; set; }
        public string OcwEma1 { get; set; }
        public string Ema2v34 { get; set; }
        public int? AllEmasInNum { get; set; }
    }
}
