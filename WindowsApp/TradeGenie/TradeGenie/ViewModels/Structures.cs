using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeGenie
{
    public class UserLoginVM
    {
        public int ID { get; set; }
        public DateTime LoginDate { get; set; }
        public string ClientId { get; set; }
        public string AccessToken { get; set; }
        public string PublicToken { get; set; }
        public string RequestToken { get; set; }
    }

    public class InstrumentOHLCVM
    {
        public string TradingSymbol { get; set; }
        public int InstrumentToken { get; set; }
        //public string InstrumentName { get; set; }
        public decimal PreviousClose { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal LastPrice { get; set; }
        public DateTime OHLCDateTime { get; set; }
    }

    public class HistoricalVM
    {
        public int InstrumentToken { get; set; }
        public string TradingSymbol { get; set; }
        public string TimeStamp { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public uint Volume { get; set; }
    }

    public class InstrumentVM
    {
        public int InstrumentToken { get; set; }
        public int ExchangeToken { get; set; }
        public string TradingSymbol { get; set; }
        public string Name { get; set; }
        public decimal LastPrice { get; set; }
        public decimal TickSize { get; set; }
        public string Expiry { get; set; }
        public string InstrumentType { get; set; }
        public string Segment { get; set; }
        public string Exchange { get; set; }
        public decimal Strike { get; set; }
        public int LotSize { get; set; }
        public decimal? Margin { get; set; }
        public decimal? COLower { get; set; }
        public decimal? MISMultiplier { get; set; }
        public decimal? COUpper { get; set; }
        public decimal? NRMLMargin { get; set; }
        public decimal? MISMargin { get; set; }
    }

    public class MorningBreakOutStrategyDM
    {
        public string TradingSymbol { get; set; }
        public int InstrumentToken { get; set; }
        public DateTime DateTimePeriod { get; set; }
        public decimal? Open { get; set; }
        public decimal? High { get; set; }
        public decimal? Low { get; set; }
        public decimal? Close { get; set; }
        public decimal? LTP { get; set; }
        public decimal? CandleSize { get; set; }
        public decimal? DayHigh { get; set; }
        public decimal? DayLow { get; set; }
        public int? Quantity { get; set; }
        public decimal? Entry { get; set; }
        public DateTime? EntryTime { get; set; }
        public decimal? Exit { get; set; }
        public DateTime? ExitTime { get; set; }
        public decimal? StopLoss { get; set; }
        public DateTime? StopLossHitTime { get; set; }
        public decimal? ProfitLoss { get; set; }
        public string RAGStatus { get; set; }
        public string Movement { get; set; }
        public bool? isRealOrderPlaced { get; set; }
    }

    public class ElderStrategyOrderVM
    {
        public string TradingSymbol { get; set; }
        public System.DateTime? EntryTime { get; set; }
        public decimal? EntryPrice { get; set; }
        public decimal? ExitPrice { get; set; }
        public decimal? StopLoss { get; set; }
        public decimal? Ltp{ get; set; }
        public int? Quantity { get; set; }
        public decimal? ProfitLoss { get; set; }
        public System.DateTime? ExitTime { get; set; }
        public System.DateTime? StopLossHitTime { get; set; }
        public string PositionType { get; set; }
        public string PositionStatus { get; set; }
        public string Variety { get; set; }
        public string SLOrderStatus { get; set; }
        public string TargetOrderStatus { get; set; }
        public string OrderId { get; set; }
        public string SLOrderId { get; set; }
        public string StockTrend { get; set; }
        public bool? isRealOrderPlaced { get; set; }
        public int InstrumentToken { get; set; }
        public System.DateTime TradeDate { get; set; }
    }

    public class CamarillaStrategyOrderVM
    {
        public string TradingSymbol { get; set; }
        public System.DateTime? EntryTime { get; set; }
        public decimal? EntryPrice { get; set; }
        public decimal? ExitPrice { get; set; }
        public decimal? SLPrice { get; set; }
        public decimal? Ltp { get; set; }
        public int? Quantity { get; set; }
        public decimal? ProfitLoss { get; set; }
        public System.DateTime? ExitTime { get; set; }
        public System.DateTime? SLHitTime { get; set; }
        public string PositionType { get; set; }
        public string PositionStatus { get; set; }
        public string Variety { get; set; }
        public string SLOrderStatus { get; set; }
        public string TargetOrderStatus { get; set; }
        public string OrderId { get; set; }
        public string SLOrderId { get; set; }
        public bool? isRealOrderPlaced { get; set; }
        public int InstrumentToken { get; set; }
        public System.DateTime TradeDate { get; set; }

    }

    public class DayOHLC
    {
        public DateTime OHLCDate { get; set; }
        public string TradingSymbol { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
    }

    public class CurrentDayOHLC
    {
        public DateTime OHLCDate { get; set; }
        public string TradingSymbol { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
    }

    public enum CamarillaScenario
    {
        NA,
        SCENARIO_1,
        SCENARIO_2,
        SCENARIO_3,
        SCENARIO_4,
        SCENARIO_5
    }

    public class Camarilla
    {
        public DateTime CamarillaDate { get; set; }
        public DateTime CurrentDate { get; set; }
        public string TradingSymbol { get; set; }
        public CamarillaLevels CamarillaLevel { get; set; }
        public CamarillaScenario Scenario { get; set; }
        public bool IsTriggered { get; set; }
        public Signal CamarillaSignal { get; set; }

        public double BuyPrice { get; set; }
        public double SellPrice { get; set; }
        public double StopLoss { get; set; }
        public double Target { get; set; }
    }

    public enum Signal
    {
        NA,
        LONG,
        SHORT
    }

    public class CamarillaLevels
    {
        public double Close { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double H6 { get; set; }
        public double H5 { get; set; }
        public double H4 { get; set; }
        public double H3 { get; set; }
        public double H2 { get; set; }
        public double H1 { get; set; }
        public double L6 { get; set; }
        public double L5 { get; set; }
        public double L4 { get; set; }
        public double L3 { get; set; }
        public double L2 { get; set; }
        public double L1 { get; set; }
        public double Pivot { get; set; }
        public double Range { get; set; }
    }

    public class TickerElderIndicatorsVM
    {
        public int InstrumentToken { get; set; }
        public string TradingSymbol { get; set; }
        public DateTime TickerDateTime { get; set; }
        public int TimePeriod { get; set; }

        public double? PriceOpen { get; set; }
        public double? PriceHigh { get; set; }
        public double? PriceLow { get; set; }
        public double? PriceClose { get; set; }

        public int? Volume { get; set; }

        public double? EMA1 { get; set; }
        public double? EMA2 { get; set; }
        public double? EMA3 { get; set; }
        public double? EMA4 { get; set; }

        public double? MACD { get; set; }
        public double? Signal { get; set; }

        public double? Histogram { get; set; }
        public string HistMovement { get; set; }

        public double? AG1 { get; set; }
        public double? AL1 { get; set; }
        public double? RSI1 { get; set; }
        public double? AG2 { get; set; }
        public double? AL2 { get; set; }
        public double? RSI2 { get; set; }

        public double? ForceIndex1 { get; set; }
        public double? ForceIndex2 { get; set; }

        public string Impulse { get; set; }

        public double? EMA1Dev { get; set; }
        public double? EMA2Dev { get; set; }

    }


}
