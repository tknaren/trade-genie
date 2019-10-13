using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Models;
using Utilities;
using Serilog;
using Serilog.Exceptions;

namespace BusinessLogicLayer
{
    public interface IIndicatorEngine
    {
        void IndicatorEngineLogic();
    }

    public class IndicatorEngine : IIndicatorEngine
    {
        private readonly IConfigSettings _settings;
        private readonly IDBMethods _dBMethods;

        List<MasterStockList> mslList = new List<MasterStockList>();
        List<TickerElderIndicatorsModel> tickerListMaster = new List<TickerElderIndicatorsModel>();

        public IndicatorEngine(IConfigSettings settings, IDBMethods dBMethods)
        {
            _settings = settings;
            _dBMethods = dBMethods;
        }

        public void IndicatorEngineLogic()
        {
            //string[] timePeriodsToCalculate = _settings.TimePeriodsToCalculate.Split(',');
            string[] timePeriodsToCalculate = GetTimePeriodsToCalculate();

            if (timePeriodsToCalculate.Length > 0)
            {
                Log.Information("Time Periods to Calculate - " + string.Join(",", timePeriodsToCalculate));
            }
            else
            {
                Log.Information("No TimePeriods - Exiting");

                return;
            }

            List<Task> taskList = new List<Task>();
            List<TickerMin> tkrDataForConsol = new List<TickerMin>();
            List<TickerMin> tkrAllData = new List<TickerMin>();
            List<TickerElderIndicatorsModel> tickerListElder = new List<TickerElderIndicatorsModel>();

            mslList = _dBMethods.GetMasterStockList();

            string instrumentList = string.Join(",", from item in mslList select item.TradingSymbol);

            Log.Information("Indicators - Get Time Periods");
            Log.Information("Indicators - Get All Ticker Data");

            Task<List<TickerElderIndicatorsModel>> tickerListMasterTask = 
                Task.Run(() => _dBMethods.GetTickerDataForIndicators(instrumentList, _settings.TimePeriodsToCalculate));
            //tickerListMaster = _dBMethods.GetTickerDataForIndicators(instrumentList, _settings.TimePeriodsToCalculate);

            taskList.Add(tickerListMasterTask);

            Task<List<TickerMin>> tkrAllDataTask =
                Task.Run(() => _dBMethods.GetTickerDataForConsolidation());

            taskList.Add(tkrAllDataTask);

            Task.WaitAll(taskList.ToArray());

            tickerListMaster = tickerListMasterTask.Result;
            tkrAllData = tkrAllDataTask.Result;

            // tkrAllData = _dBMethods.GetTickerDataForConsolidation(msl.TradingSymbol, DateTime.Today);
            // tkrAllData = _dBMethods.GetTickerDataForConsolidation();

            Log.Information("Indicators - Start Calculation");

            // Get the data from TickerMin
            // If last record is present, get records greater than that time,
            // else get entire records greater than fromDate configuration
            // Once after the OHLC, Volume, Change and TradedValue calculation is done, load the data to tickerListMaster 
            //  to facilitate the further calculation of the indicators
            foreach (MasterStockList msl in mslList)
            {
                for (int iTimePeriod = 0; iTimePeriod < timePeriodsToCalculate.Length; iTimePeriod++)
                {
                    int timePeriod = Convert.ToInt32(timePeriodsToCalculate[iTimePeriod]);

                    DateTime tickerDateFrom = DateTime.MinValue;

                    TickerElderIndicatorsModel tickeLastRecordedData = 
                                                (from tle in tickerListMaster
                                                 where tle.StockCode == msl.TradingSymbol
                                                    && tle.TimePeriod == timePeriod
                                                 select tle).FirstOrDefault();

                    if (tickeLastRecordedData != null)
                    {
                        // Pull the data from TickerMin greater than the last datetime
                        tickerDateFrom = tickeLastRecordedData.TickerDateTime.AddMinutes(timePeriod - 1);

                        tickerListElder.Add(tickeLastRecordedData);
                    }
                    else
                    {
                        // Pull the data from the TickerMin greater than the DateFrom date
                         tickerDateFrom = _settings.IndicatorLoadDateFrom;
                        // tickerDateFrom = DateTime.Today;
                    }

                    tkrDataForConsol = (from tkr in tkrAllData
                                        where tkr.TradingSymbol == msl.TradingSymbol
                                            && tkr.DateTime > tickerDateFrom
                                        select tkr).ToList<TickerMin>();

                    //OHLC Calculation
                    CalculateOHLC(tkrDataForConsol, timePeriod, tickerListElder);

                    //EMA Calculation
                    AllEMACalculation(tickerListElder);

                    //RSI Calculation
                    AllRSICalculation(tickerListElder);

                    //MACD Calculation
                    MACDCalculation(tickerListElder);

                    //Force Index Calculation
                    AllForceIndexCalculation(tickerListElder);

                    //True Range
                    AllTrueRangeCalculation(tickerListElder);

                    //Super Trend
                    AllSuperTrendCalculation(tickerListElder);

                    //Impulse Indicator
                    ImpulseIndicator(tickerListElder);

                    //EMA Deviation
                    CalculateEMADeviation(tickerListElder);

                    //HeikinAshi 
                    HeikinAshiCalculation(tickerListElder);

                    //EMA Variance
                    EMAVarianceCalculation(tickerListElder);

                    //Price Variance
                    PriceVarianceCalculation(tickerListElder);

                    //HA with EMA
                    CompareHAWithEMA(tickerListElder);

                    //OC with EMA
                    CompareOCWithEMA(tickerListElder);

                    //EMA with EMA
                    CompareEMAWithEMA(tickerListElder);

                    //AllVWMACalculation(tickerListElder);
                    tickerListElder.Remove(tickeLastRecordedData);
                }
            }

            Log.Information("Indicators - Insert into DB");
            //DataTable masterTable = tickerListElder.ToDataTable();
            //_dBMethods.UpdateTickerElderDataTable(masterTable);
            UploadToIndicatorTables(tickerListElder);
        }

        private string[] GetTimePeriodsToCalculate()
        {
            string[] timePeriods = { };

            TimeSpan currentTime = new TimeSpan(AuxiliaryMethods.GetCurrentIndianTimeStamp().TimeOfDay.Hours,
                                        AuxiliaryMethods.GetCurrentIndianTimeStamp().TimeOfDay.Minutes, 0);

            string timePeriodCSV = string.Empty;

            DateTime startDateTime = DateTime.Today + _settings.StartingTime;

            if (AuxiliaryMethods.MinuteTimer(_settings.Min60Timer).Contains(currentTime))
            {
                timePeriodCSV = string.IsNullOrEmpty(timePeriodCSV) == true ? "60" : ",60";
            }

            if (AuxiliaryMethods.MinuteTimer(_settings.Min30Timer).Contains(currentTime))
            {
                timePeriodCSV = string.IsNullOrEmpty(timePeriodCSV) == true ? "30" : ",30";
            }

            if (AuxiliaryMethods.MinuteTimer(_settings.Min25Timer).Contains(currentTime))
            {
                timePeriodCSV = string.IsNullOrEmpty(timePeriodCSV) == true ? "25" : ",25";
            }

            if (AuxiliaryMethods.MinuteTimer(_settings.Min15Timer).Contains(currentTime))
            {
                timePeriodCSV = string.IsNullOrEmpty(timePeriodCSV) == true ? "15" : ",15";
            }

            if (AuxiliaryMethods.MinuteTimer(_settings.Min10Timer).Contains(currentTime))
            {
                timePeriodCSV = string.IsNullOrEmpty(timePeriodCSV) == true ? "10" : ",10";
            }

            if (AuxiliaryMethods.MinuteTimer(_settings.Min5Timer).Contains(currentTime))
            {
                timePeriodCSV = string.IsNullOrEmpty(timePeriodCSV) == true ? "5" : ",5";
            }

            if (AuxiliaryMethods.MinuteTimer(_settings.Min3Timer).Contains(currentTime))
            {
                timePeriodCSV = string.IsNullOrEmpty(timePeriodCSV) == true ? "3" : ",3";
            }

            if (timePeriodCSV.Length > 0)
                timePeriods = timePeriodCSV.Split(',');

            return timePeriods;
        }

        private void UploadToIndicatorTables(List<TickerElderIndicatorsModel> tickerListElder)
        {
            List<TickerMinElderIndicator> elderItems = new List<TickerMinElderIndicator>();
            List<TickerMinSuperTrend> stItems = new List<TickerMinSuperTrend>();
            List<TickerMinEMAHA> emahaItems = new List<TickerMinEMAHA>();

            foreach (TickerElderIndicatorsModel tickerItem in tickerListElder)
            {
                elderItems.Add(new TickerMinElderIndicator
                {
                    StockCode = tickerItem.StockCode,
                    TickerDateTime = tickerItem.TickerDateTime,
                    TimePeriod = (int)tickerItem.TimePeriod,
                    PriceOpen = tickerItem.PriceOpen,
                    PriceHigh = tickerItem.PriceHigh,
                    PriceLow = tickerItem.PriceLow,
                    PriceClose = tickerItem.PriceClose,
                    Volume = tickerItem.Volume,
                    Change = tickerItem.Change,
                    ChangePercent = tickerItem.ChangePercent,
                    TradedValue = tickerItem.TradedValue,
                    EMA1 = tickerItem.EMA1,
                    EMA2 = tickerItem.EMA2,
                    EMA3 = tickerItem.EMA3,
                    EMA4 = tickerItem.EMA4,
                    MACD = tickerItem.MACD,
                    Signal = tickerItem.Signal,
                    Histogram = tickerItem.Histogram,
                    HistIncDec = tickerItem.HistIncDec,
                    Impulse = tickerItem.Impulse,
                    ForceIndex1 = tickerItem.ForceIndex1,
                    ForceIndex2 = tickerItem.ForceIndex2,
                    EMA1Dev = tickerItem.EMA1Dev,
                    EMA2Dev = tickerItem.EMA2Dev,
                    AG1 = tickerItem.AG1,
                    AL1 = tickerItem.AL1,
                    RSI1 = tickerItem.RSI1,
                    AG2 = tickerItem.AG2,
                    AL2 = tickerItem.AL2,
                    RSI2 = tickerItem.RSI2
                });

                stItems.Add(new TickerMinSuperTrend
                {
                    StockCode = tickerItem.StockCode,
                    TickerDateTime = tickerItem.TickerDateTime,
                    TimePeriod = (int)tickerItem.TimePeriod,
                    PriceOpen = tickerItem.PriceOpen,
                    PriceHigh = tickerItem.PriceHigh,
                    PriceLow = tickerItem.PriceLow,
                    PriceClose = tickerItem.PriceClose,
                    Volume = tickerItem.Volume,
                    TrueRange = tickerItem.TrueRange,
                    ATR1 = tickerItem.ATR1,
                    ATR2 = tickerItem.ATR2,
                    ATR3 = tickerItem.ATR3,
                    BUB1 = tickerItem.BUB1,
                    BUB2 = tickerItem.BUB2,
                    BUB3 = tickerItem.BUB3,
                    BLB1 = tickerItem.BLB1,
                    BLB2 = tickerItem.BLB2,
                    BLB3 = tickerItem.BLB3,
                    FLB1 = tickerItem.FLB1,
                    FLB2 = tickerItem.FLB2,
                    FLB3 = tickerItem.FLB3,
                    FUB1 = tickerItem.FUB1,
                    FUB2 = tickerItem.FUB2,
                    FUB3 = tickerItem.FUB3,
                    ST1 = tickerItem.ST1,
                    ST2 = tickerItem.ST2,
                    ST3 = tickerItem.ST3,
                    Trend1 = tickerItem.Trend1,
                    Trend2 = tickerItem.Trend2,
                    Trend3 = tickerItem.Trend3
                });

                emahaItems.Add(new TickerMinEMAHA
                {
                    StockCode = tickerItem.StockCode,
                    TickerDateTime = tickerItem.TickerDateTime,
                    TimePeriod = (int)tickerItem.TimePeriod,
                    PriceOpen = tickerItem.PriceOpen,
                    PriceHigh = tickerItem.PriceHigh,
                    PriceLow = tickerItem.PriceLow,
                    PriceClose = tickerItem.PriceClose,
                    Volume = tickerItem.Volume,
                    EHEMA1 = tickerItem.EHEMA1,
                    EHEMA2 = tickerItem.EHEMA2,
                    EHEMA3 = tickerItem.EHEMA3,
                    EHEMA4 = tickerItem.EHEMA4,
                    EHEMA5 = tickerItem.EHEMA5,
                    HAOpen = tickerItem.HAOpen,
                    HAHigh = tickerItem.HAHigh,
                    HALow = tickerItem.HALow,
                    HAClose = tickerItem.HAClose,
                    varEMA1v2 = tickerItem.varEMA1v2,
                    varEMA1v3 = tickerItem.varEMA1v3,
                    varEMA1v4 = tickerItem.varEMA1v4,
                    varEMA2v3 = tickerItem.varEMA2v3,
                    varEMA2v4 = tickerItem.varEMA2v4,
                    varEMA3v4 = tickerItem.varEMA3v4,
                    varEMA4v5 = tickerItem.varEMA4v5,
                    varHAOvHAC = tickerItem.varHAOvHAC,
                    varHAOvHAPO = tickerItem.varHAOvHAPO,
                    varHACvHAPC = tickerItem.varHACvHAPC,
                    varOvC = tickerItem.varOvC,
                    varOvPO = tickerItem.varOvPO,
                    varCvPC = tickerItem.varCvPC,
                    HAOCwEMA1 = tickerItem.HAOCwEMA1,
                    OCwEMA1 = tickerItem.OCwEMA1,
                    AllEMAsInNum = tickerItem.AllEMAsInNum
                });
            }

            _dBMethods.BulkUploadElderDataToDB(elderItems);

            _dBMethods.BulkUploadEMAHADataToDB(emahaItems);

            _dBMethods.BulkUploadSuperTrendDataToDB(stItems);
        }

        private void CalculateOHLC(List<TickerMin> tkrDataForConsol, int timePeriod, List<TickerElderIndicatorsModel> tickerList)
        {
            //OHLC Calculation
            int numberOfSets = (tkrDataForConsol.Count / timePeriod) + (tkrDataForConsol.Count % timePeriod == 0 ? 0 : 1);

            for (int setIndex = 0; setIndex < numberOfSets; setIndex++)
            {
                var tickerBatchList = tkrDataForConsol.Skip(setIndex * timePeriod).Take(timePeriod);

                double open = (double)tickerBatchList.First().Open;
                double high = (double)tickerBatchList.Max(mx => mx.High);
                double low = (double)tickerBatchList.Min(mn => mn.Low);
                double close = (double)tickerBatchList.Last().Close;
                int volume = (int)tickerBatchList.Sum(sm => sm.Volume);
                double change = Math.Round(close - open, 2);
                double changePercent = Math.Round(((close - open) / open) * 100, 2);
                decimal tradedValue = (decimal)Math.Round((((open + high + low + close) / 4) * volume), 2);

                tickerList.Add(new TickerElderIndicatorsModel
                {
                    StockCode = tickerBatchList.First().TradingSymbol,
                    TickerDateTime = tickerBatchList.First().DateTime,
                    TimePeriod = timePeriod,
                    PriceOpen = open,
                    PriceHigh = high,
                    PriceLow = low,
                    PriceClose = close,
                    Volume = volume,
                    Change = change,
                    ChangePercent = changePercent,
                    TradedValue = tradedValue
                });
            }
        }

        private void HeikinAshiCalculation(List<TickerElderIndicatorsModel> tickerList)
        {
            List<TickerElderIndicatorsModel> currentList = new List<TickerElderIndicatorsModel>();
            double? haPrevOpen = 0.0;
            double? haPrevClose = 0.0;

            var recExists = tickerList.Any(a => a.HAOpen != null);

            if (recExists)
            {
                var tickPrevVal = (from li in tickerList
                                   where li.HAOpen != null
                                   orderby li.TickerDateTime descending
                                   select li).FirstOrDefault();

                haPrevOpen = tickPrevVal.HAOpen;
                haPrevClose = tickPrevVal.HAClose;

                var tickCurrVal = from cur in tickerList
                                  where cur.HAOpen == null
                                  orderby cur.TickerDateTime
                                  select cur;

                currentList = tickCurrVal.ToList<TickerElderIndicatorsModel>();
            }
            else
            {
                currentList = tickerList;
            }

            foreach (TickerElderIndicatorsModel ticker in currentList)
            {
                if (haPrevOpen == 0.0 && haPrevClose == 0.0)
                {
                    haPrevOpen = ticker.PriceOpen;
                    haPrevClose = ticker.PriceClose;

                    ticker.HAClose = ticker.HAOpen = ticker.HAHigh = ticker.HALow = 0.0;
                }
                else
                {
                    ticker.HAClose = (ticker.PriceOpen + ticker.PriceHigh + ticker.PriceLow + ticker.PriceClose) / 4;
                    ticker.HAOpen = (haPrevOpen + haPrevClose) / 2;

                    ticker.HAClose = Math.Round((double)ticker.HAClose, 2, MidpointRounding.ToEven);
                    ticker.HAOpen = Math.Round((double)ticker.HAOpen, 2, MidpointRounding.ToEven);

                    ticker.HAHigh = Math.Max(Math.Round((double)ticker.PriceHigh, 2, MidpointRounding.ToEven),
                            (double)Math.Max((double)ticker.HAOpen, (double)ticker.HAClose));

                    ticker.HALow = Math.Min(Math.Round((double)ticker.PriceLow, 2, MidpointRounding.ToEven),
                            (double)Math.Min((double)ticker.HAOpen, (double)ticker.HAClose));

                    ticker.varHAOvHAC = Math.Round((double)((ticker.HAClose - ticker.HAOpen) / ticker.HAOpen) * 100, 4, MidpointRounding.ToEven);
                    ticker.varHAOvHAPO = Math.Round((double)((ticker.HAOpen - haPrevOpen) / haPrevOpen) * 100, 4, MidpointRounding.ToEven);
                    ticker.varHACvHAPC = Math.Round((double)((ticker.HAClose - haPrevClose) / haPrevClose) * 100, 4, MidpointRounding.ToEven);

                    haPrevOpen = ticker.HAOpen;
                    haPrevClose = ticker.HAClose;
                }
            }
        }

        private void AllEMACalculation(List<TickerElderIndicatorsModel> tickerListElder)
        {
            double prevEMA = 0.0;
            double prevEHEMA = 0.0;
            bool recExists = false;
            string[] emasToCalculate = _settings.EMAsToCalculate.Split(',');
            string[] ehemasToCalculate = _settings.EHEMAsToCalculate.Split(',');

            for (int i = 0; i < emasToCalculate.Length; i++)
            {
                switch (i)
                {
                    #region EMA1
                    case 0:

                        prevEMA = 0.0;

                        var tickerListEMA1 = (from li in tickerListElder
                                              where li.EMA1 == null
                                              select li).ToList();

                        recExists = tickerListElder.Any(a => a.EMA1 != null);

                        if (recExists)
                        {
                            var prevEmaVar = (from li in tickerListElder
                                              where li.EMA1 != null
                                              orderby li.TickerDateTime descending
                                              select li).FirstOrDefault().EMA1;

                            prevEMA = Convert.ToDouble(prevEmaVar);
                        }


                        EMACalculation(tickerListEMA1, Convert.ToInt32(emasToCalculate[i]), i + 1, prevEMA);

                        break;
                    #endregion

                    #region EMA2
                    case 1:

                        prevEMA = 0.0;

                        var tickerListEMA2 = (from li in tickerListElder
                                              where li.EMA2 == null
                                              select li).ToList();

                        recExists = tickerListElder.Any(a => a.EMA2 != null);

                        if (recExists)
                        {
                            var prevEmaVar = (from li in tickerListElder
                                              where li.EMA2 != null
                                              orderby li.TickerDateTime descending
                                              select li).FirstOrDefault().EMA2;

                            prevEMA = Convert.ToDouble(prevEmaVar);
                        }

                        EMACalculation(tickerListEMA2, Convert.ToInt32(emasToCalculate[i]), i + 1, prevEMA);

                        break;
                    #endregion

                    #region EMA3
                    case 2:

                        prevEMA = 0.0;

                        var tickerListEMA3 = (from li in tickerListElder
                                              where li.EMA3 == null
                                              select li).ToList();

                        recExists = tickerListElder.Any(a => a.EMA3 != null);

                        if (recExists)
                        {
                            var prevEmaVar = (from li in tickerListElder
                                              where li.EMA3 != null
                                              orderby li.TickerDateTime descending
                                              select li).FirstOrDefault().EMA3;

                            prevEMA = Convert.ToDouble(prevEmaVar);
                        }

                        EMACalculation(tickerListEMA3, Convert.ToInt32(emasToCalculate[i]), i + 1, prevEMA);

                        break;
                    #endregion

                    #region EMA4
                    case 3:

                        prevEMA = 0.0;

                        var tickerListEMA4 = (from li in tickerListElder
                                              where li.EMA4 == null
                                              select li).ToList();

                        recExists = tickerListElder.Any(a => a.EMA4 != null);

                        if (recExists)
                        {
                            var prevEmaVar = (from li in tickerListElder
                                              where li.EMA4 != null
                                              orderby li.TickerDateTime descending
                                              select li).FirstOrDefault().EMA4;

                            prevEMA = Convert.ToDouble(prevEmaVar);
                        }

                        EMACalculation(tickerListEMA4, Convert.ToInt32(emasToCalculate[i]), i + 1, prevEMA);

                        break;
                        #endregion
                }
            }


            for (int i = 0; i < ehemasToCalculate.Length; i++)
            {
                switch (i)
                {
                    #region EHEMA1
                    case 0:

                        prevEHEMA = 0.0;

                        var tickerListEHEMA1 = (from li in tickerListElder
                                                where li.EHEMA1 == null
                                                select li).ToList();

                        recExists = tickerListElder.Any(a => a.EHEMA1 != null);

                        if (recExists)
                        {
                            var prevEmaVar = (from li in tickerListElder
                                              where li.EHEMA1 != null
                                              orderby li.TickerDateTime descending
                                              select li).FirstOrDefault().EHEMA1;

                            prevEHEMA = Convert.ToDouble(prevEmaVar);
                        }


                        EMACalculation(tickerListEHEMA1, Convert.ToInt32(ehemasToCalculate[i]), i + 1, prevEHEMA, true);

                        break;
                    #endregion

                    #region EHEMA2
                    case 1:

                        prevEHEMA = 0.0;

                        var tickerListEHEMA2 = (from li in tickerListElder
                                                where li.EHEMA2 == null
                                                select li).ToList();

                        recExists = tickerListElder.Any(a => a.EHEMA2 != null);

                        if (recExists)
                        {
                            var prevEmaVar = (from li in tickerListElder
                                              where li.EHEMA2 != null
                                              orderby li.TickerDateTime descending
                                              select li).FirstOrDefault().EHEMA2;

                            prevEHEMA = Convert.ToDouble(prevEmaVar);
                        }

                        EMACalculation(tickerListEHEMA2, Convert.ToInt32(ehemasToCalculate[i]), i + 1, prevEHEMA, true);

                        break;
                    #endregion

                    #region EHEMA3
                    case 2:

                        prevEHEMA = 0.0;

                        var tickerListEHEMA3 = (from li in tickerListElder
                                                where li.EHEMA3 == null
                                                select li).ToList();

                        recExists = tickerListElder.Any(a => a.EHEMA3 != null);

                        if (recExists)
                        {
                            var prevEmaVar = (from li in tickerListElder
                                              where li.EHEMA3 != null
                                              orderby li.TickerDateTime descending
                                              select li).FirstOrDefault().EHEMA3;

                            prevEHEMA = Convert.ToDouble(prevEmaVar);
                        }

                        EMACalculation(tickerListEHEMA3, Convert.ToInt32(ehemasToCalculate[i]), i + 1, prevEHEMA, true);

                        break;
                    #endregion

                    #region EHEMA4
                    case 3:

                        prevEHEMA = 0.0;

                        var tickerListEHEMA4 = (from li in tickerListElder
                                                where li.EHEMA4 == null
                                                select li).ToList();

                        recExists = tickerListElder.Any(a => a.EHEMA4 != null);

                        if (recExists)
                        {
                            var prevEmaVar = (from li in tickerListElder
                                              where li.EHEMA4 != null
                                              orderby li.TickerDateTime descending
                                              select li).FirstOrDefault().EHEMA4;

                            prevEHEMA = Convert.ToDouble(prevEmaVar);
                        }

                        EMACalculation(tickerListEHEMA4, Convert.ToInt32(ehemasToCalculate[i]), i + 1, prevEHEMA, true);

                        break;
                    #endregion

                    #region EHEMA5
                    case 4:

                        prevEHEMA = 0.0;

                        var tickerListEHEMA5 = (from li in tickerListElder
                                                where li.EHEMA5 == null
                                                select li).ToList();

                        recExists = tickerListElder.Any(a => a.EHEMA5 != null);

                        if (recExists)
                        {
                            var prevEmaVar = (from li in tickerListElder
                                              where li.EHEMA5 != null
                                              orderby li.TickerDateTime descending
                                              select li).FirstOrDefault().EHEMA5;

                            prevEHEMA = Convert.ToDouble(prevEmaVar);
                        }

                        EMACalculation(tickerListEHEMA5, Convert.ToInt32(ehemasToCalculate[i]), i + 1, prevEHEMA, true);

                        break;
                        #endregion
                }
            }
        }

        private void EMAVarianceCalculation(List<TickerElderIndicatorsModel> tickerList)
        {
            foreach (TickerElderIndicatorsModel ticker in tickerList)
            {
                if (ticker.EHEMA2 > 0.0 && ticker.EHEMA1 > 0.0)
                {
                    ticker.varEMA1v2 = Math.Round((double)((ticker.EHEMA2 - ticker.EHEMA1) / ticker.EHEMA1) * 100, 4, MidpointRounding.ToEven);
                }

                if (ticker.EHEMA3 > 0.0 && ticker.EHEMA1 > 0.0)
                {
                    ticker.varEMA1v3 = Math.Round((double)((ticker.EHEMA3 - ticker.EHEMA1) / ticker.EHEMA1) * 100, 4, MidpointRounding.ToEven);
                }

                if (ticker.EHEMA4 > 0.0 && ticker.EHEMA1 > 0.0)
                {
                    ticker.varEMA1v4 = Math.Round((double)((ticker.EHEMA4 - ticker.EHEMA1) / ticker.EHEMA1) * 100, 4, MidpointRounding.ToEven);
                }

                if (ticker.EHEMA3 > 0.0 && ticker.EHEMA2 > 0.0)
                {
                    ticker.varEMA2v3 = Math.Round((double)((ticker.EHEMA3 - ticker.EHEMA2) / ticker.EHEMA2) * 100, 4, MidpointRounding.ToEven);
                }

                if (ticker.EHEMA4 > 0.0 && ticker.EHEMA2 > 0.0)
                {
                    ticker.varEMA2v4 = Math.Round((double)((ticker.EHEMA4 - ticker.EHEMA2) / ticker.EHEMA2) * 100, 4, MidpointRounding.ToEven);
                }

                if (ticker.EHEMA4 > 0.0 && ticker.EHEMA3 > 0.0)
                {
                    ticker.varEMA3v4 = Math.Round((double)((ticker.EHEMA4 - ticker.EHEMA3) / ticker.EHEMA3) * 100, 4, MidpointRounding.ToEven);
                }

                if (ticker.EHEMA5 > 0.0 && ticker.EHEMA4 > 0.0)
                {
                    ticker.varEMA4v5 = Math.Round((double)((ticker.EHEMA5 - ticker.EHEMA4) / ticker.EHEMA4) * 100, 4, MidpointRounding.ToEven);
                }
            }
        }

        private void PriceVarianceCalculation(List<TickerElderIndicatorsModel> tickerList, double? prevOpen = 0.0, double? prevClose = 0.0)
        {
            foreach (TickerElderIndicatorsModel ticker in tickerList)
            {
                if (prevOpen == 0.0 && prevClose == 0.0)
                {
                    prevOpen = ticker.PriceOpen;
                    prevClose = ticker.PriceClose;
                }
                else
                {
                    ticker.varOvC = Math.Round((double)((ticker.PriceClose - ticker.PriceOpen) / ticker.PriceOpen) * 100, 4, MidpointRounding.ToEven);
                    ticker.varOvPO = Math.Round((double)((ticker.PriceOpen - prevOpen) / prevOpen) * 100, 4, MidpointRounding.ToEven);
                    ticker.varCvPC = Math.Round((double)((ticker.PriceClose - prevClose) / prevClose) * 100, 4, MidpointRounding.ToEven);

                    prevOpen = ticker.PriceOpen;
                    prevClose = ticker.PriceClose;
                }
            }
        }

        private void CompareHAWithEMA(List<TickerElderIndicatorsModel> tickerList)
        {
            //G = GREEN
            //Y = AMBUR - BUY
            //A = AMBER - SELL
            //R = RED
            foreach (TickerElderIndicatorsModel ticker in tickerList)
            {
                if (ticker.EMA1 > 0.0)
                {
                    if (ticker.HAClose > ticker.EMA1 && ticker.HAOpen > ticker.EMA1)
                    {
                        ticker.HAOCwEMA1 = "G";
                    }
                    else if (ticker.HAClose >= ticker.EMA1 && ticker.HAOpen <= ticker.EMA1)
                    {
                        ticker.HAOCwEMA1 = "Y";
                    }
                    else if (ticker.HAClose <= ticker.EMA1 && ticker.HAOpen >= ticker.EMA1)
                    {
                        ticker.HAOCwEMA1 = "A";
                    }
                    else if (ticker.HAClose < ticker.EMA1 && ticker.HAOpen < ticker.EMA1)
                    {
                        ticker.HAOCwEMA1 = "R";
                    }
                }
            }
        }

        private void CompareOCWithEMA(List<TickerElderIndicatorsModel> tickerList)
        {
            //G = GREEN
            //Y = AMBUR - BUY
            //A = AMBER - SELL
            //R = RED
            foreach (TickerElderIndicatorsModel ticker in tickerList)
            {
                if (ticker.EMA1 > 0.0)
                {
                    if (ticker.PriceClose > ticker.EMA1 && ticker.PriceOpen > ticker.EMA1)
                    {
                        ticker.OCwEMA1 = "G";
                    }
                    else if (ticker.PriceClose >= ticker.EMA1 && ticker.PriceOpen <= ticker.EMA1)
                    {
                        ticker.OCwEMA1 = "Y";
                    }
                    else if (ticker.PriceClose <= ticker.EMA1 && ticker.PriceOpen >= ticker.EMA1)
                    {
                        ticker.OCwEMA1 = "A";
                    }
                    else if (ticker.PriceClose < ticker.EMA1 && ticker.PriceOpen < ticker.EMA1)
                    {
                        ticker.OCwEMA1 = "R";
                    }
                }
            }
        }

        private void CompareEMAWithEMA(List<TickerElderIndicatorsModel> tickerList)
        {
            //A = WELL ABOVE
            //B = ABOVE
            //C = NARROW ABOVE
            //D = NARROW BELOW
            //E = BELOW
            //F = WELL BELOW

            foreach (TickerElderIndicatorsModel ticker in tickerList)
            {
                StringBuilder sbAllEMA = new StringBuilder();

                if (ticker.varEMA1v2 != null && ticker.varEMA1v3 != null && ticker.varEMA1v4 != null)
                {
                    StringBuilder sbEMA1v234 = new StringBuilder();

                    sbAllEMA.Append(EMARangeDeterminationInNum(ticker.varEMA1v2));
                    sbAllEMA.Append(EMARangeDeterminationInNum(ticker.varEMA1v3));
                    sbAllEMA.Append(EMARangeDeterminationInNum(ticker.varEMA1v4));

                }

                if (ticker.varEMA2v3 != null && ticker.varEMA2v4 != null)
                {
                    StringBuilder sbEMA2v34 = new StringBuilder();

                    sbAllEMA.Append(EMARangeDeterminationInNum(ticker.varEMA2v3));
                    sbAllEMA.Append(EMARangeDeterminationInNum(ticker.varEMA2v4));

                }

                if (ticker.varEMA3v4 != null)
                {
                    StringBuilder sbEMA3v4 = new StringBuilder();

                    sbAllEMA.Append(EMARangeDeterminationInNum(ticker.varEMA3v4));
                }

                if (sbAllEMA.Length == 6)
                {
                    ticker.AllEMAsInNum = Convert.ToInt32(sbAllEMA.ToString());
                }
            }
        }

        private string EMARangeDetermination(double? emaValue)
        {
            string rangeChar = string.Empty;

            if (emaValue < -0.5)
            {
                rangeChar = "A";
            }
            else if (emaValue < -0.25 && emaValue > -0.5)
            {
                rangeChar = "B";
            }
            else if (emaValue < 0 && emaValue > -0.25)
            {
                rangeChar = "C";
            }
            else if (emaValue > 0 && emaValue < 0.25)
            {
                rangeChar = "D";
            }
            else if (emaValue > 0.25 && emaValue < 0.5)
            {
                rangeChar = "E";
            }
            else
            {
                rangeChar = "F";
            }

            return rangeChar;
        }

        private string EMARangeDeterminationInNum(double? emaValue)
        {
            string rangeChar = string.Empty;

            if (emaValue < -0.5)
            {
                rangeChar = "6";
            }
            else if (emaValue < -0.25 && emaValue >= -0.5)
            {
                rangeChar = "5";
            }
            else if (emaValue < 0 && emaValue >= -0.25)
            {
                rangeChar = "4";
            }
            else if (emaValue >= 0 && emaValue < 0.25)
            {
                rangeChar = "3";
            }
            else if (emaValue >= 0.25 && emaValue < 0.5)
            {
                rangeChar = "2";
            }
            else if (emaValue >= 0.5)
            {
                rangeChar = "1";
            }

            return rangeChar;
        }


        //private void AllVWMACalculation(List<TickerElderIndicatorsModel> tickerListElder)
        //{
        //    string[] vwmasToCalculate = _settings.VWMAsToCalculate.Split(',');

        //    for (int i = 0; i < vwmasToCalculate.Length; i++)
        //    {
        //        VWMACalculation(tickerListElder, Convert.ToInt32(vwmasToCalculate[i]), false, i + 1);
        //    }
        //}

        //private void VWMACalculation(List<TickerElderIndicatorsModel> tickerList, int period, bool isLastCalculated = false, int vmmaCnt = 0)
        //{
        //    double? vmma = 0.0;

        //    if (!isLastCalculated)
        //    {
        //        if (tickerList.Count > period)
        //        {
        //            for (int i = 0; i < tickerList.Count; i++)
        //            {
        //                if (i >= period - 1)
        //                {
        //                    double? vwmaTop = 0.0;
        //                    double? vwmaBottom = 0.0;

        //                    for (int j = i; j >= i - (period - 1); j--)
        //                    {
        //                        vwmaTop += (tickerList[j].PriceClose * tickerList[j].Volume);
        //                        vwmaBottom += tickerList[j].Volume;
        //                    }

        //                    vmma = vwmaTop / vwmaBottom;

        //                    vmma = Math.Round((double)vmma, 2, MidpointRounding.AwayFromZero);

        //                    switch (vmmaCnt)
        //                    {
        //                        case 1: tickerList[i].VWMA1 = vmma; break;
        //                        case 2: tickerList[i].VWMA2 = vmma; break;
        //                    }

        //                }
        //            }
        //        }
        //    }
        //    else
        //    {
        //        foreach (TickerElderIndicatorsModel tickerRec in tickerList)
        //        {
        //            //Get the last periods PriceClose and Volume
        //            List<TickerMin> priceVolumeForPeriod = GetVolumeAndPriceForPeriod(tickerRec, period);

        //            double sumPriceVolume = 0.0;
        //            double sumVolume = 0.0;

        //            foreach (TickerMin single in priceVolumeForPeriod)
        //            {
        //                sumPriceVolume += (double)(single.PriceClose * single.Volume);
        //                sumVolume += (double)single.Volume;
        //            }

        //            vmma = sumPriceVolume / sumVolume;

        //            vmma = Math.Round((double)vmma, 2, MidpointRounding.AwayFromZero);

        //            switch (vmmaCnt)
        //            {
        //                case 1: tickerRec.VWMA1 = vmma; break;
        //                case 2: tickerRec.VWMA2 = vmma; break;
        //            }
        //        }
        //    }
        //}

        private void EMACalculation(List<TickerElderIndicatorsModel> tickerList, int period, int emaCnt, double? prevEma = 0.0, bool isEH = false)
        {

            int emaCounter = 0;
            double? smaSum = 0.0;
            double? sma = 0.0;
            double? ema = 0.0;
            double? currentPrice = 0.0;

            foreach (TickerElderIndicatorsModel ticker in tickerList)
            {
                if (!isEH)
                {
                    switch (emaCnt)
                    {
                        case 1: ema = ticker.EMA1; break;
                        case 2: ema = ticker.EMA2; break;
                        case 3: ema = ticker.EMA3; break;
                        case 4: ema = ticker.EMA4; break;
                    }
                }
                else
                {
                    switch (emaCnt)
                    {
                        case 1: ema = ticker.EHEMA1; break;
                        case 2: ema = ticker.EHEMA2; break;
                        case 3: ema = ticker.EHEMA3; break;
                        case 4: ema = ticker.EHEMA4; break;
                        case 5: ema = ticker.EHEMA5; break;
                    }
                }

                if (ema == null && emaCounter < period && prevEma == 0.0)
                {
                    smaSum += ticker.PriceClose;
                    emaCounter++;

                    if (period == emaCounter)
                    {
                        sma = smaSum / period;
                        prevEma = ema = Math.Round((double)sma, 2);
                    }
                    else
                    {
                        prevEma = ema = 0.0;
                    }
                }
                else if (ema == null && prevEma != 0.0)
                {
                    currentPrice = ticker.PriceClose;

                    ema = currentPrice * (double)(2.0 / (period + 1)) + prevEma * (1 - (double)(2.0 / (period + 1)));

                    emaCounter++;

                    prevEma = ema = Math.Round((double)ema, 2);
                }

                if (!isEH)
                {
                    switch (emaCnt)
                    {
                        case 1: ticker.EMA1 = ema; break;
                        case 2: ticker.EMA2 = ema; break;
                        case 3: ticker.EMA3 = ema; break;
                        case 4: ticker.EMA4 = ema; break;
                    }
                }
                else
                {
                    switch (emaCnt)
                    {
                        case 1: ticker.EHEMA1 = ema; break;
                        case 2: ticker.EHEMA2 = ema; break;
                        case 3: ticker.EHEMA3 = ema; break;
                        case 4: ticker.EHEMA4 = ema; break;
                        case 5: ticker.EHEMA5 = ema; break;
                    }
                }
            }
        }

        private void AllRSICalculation(List<TickerElderIndicatorsModel> tickerListElder)
        {
            string[] rsisToCalculate = _settings.RSIsToCalculate.Split(',');

            for (int i = 0; i < rsisToCalculate.Length; i++)
            {
                RSICalculation(tickerListElder, Convert.ToInt32(rsisToCalculate[i]), i + 1);
            }
        }

        private void RSICalculation(List<TickerElderIndicatorsModel> tickerList, int period, int rsiCnt = 0)
        {
            double? rsi = 0.0;
            double? change = 0.0;
            double? advance = 0.0;
            double? decline = 0.0;
            double? averageGain = 0.0;
            double? averageLoss = 0.0;
            double? rs = 0.0;
            double? prevClose = 0.0;
            int recCount = 0;
            double? prevAG = 0.0;
            double? prevAL = 0.0;

            //1. Check for the prev AG and AL
            //2. if present, Loop thru the not null records to calculate RSI
            //3. calculate the current advance and decline with the prevAG and prevAL and using that calculate RSI

            //1. If prevAG and prevAL not present
            //2. calculate the advance and decline for the period 
            //3. calculate the average gain and average loss and set it to the list
            //4. calcualte the RSI using the AG and AL and set it to the list

            switch (rsiCnt)
            {
                case 1:
                    var recExists1 = tickerList.Any(a => a.AG1 != null && a.AL1 != null);

                    if (recExists1)
                    {
                        var tickPrevAvg = (from li in tickerList
                                           where li.AG1 != null && li.AL1 != null
                                           orderby li.TickerDateTime descending
                                           select li).FirstOrDefault();

                        prevAG = tickPrevAvg.AG1;
                        prevAL = tickPrevAvg.AL1;
                        prevClose = tickPrevAvg.PriceClose;


                        var tickRSICalc1 = (from li in tickerList
                                            where li.AG1 == null && li.AL1 == null
                                            orderby li.TickerDateTime
                                            select li).ToList();

                        foreach (TickerElderIndicatorsModel tickRSI1 in tickRSICalc1)
                        {
                            change = tickRSI1.PriceClose - prevClose;

                            advance = change >= 0 ? change : 0;

                            decline = change <= 0 ? -(change) : 0;

                            averageGain = ((prevAG * (period - 1)) + advance) / period;

                            averageLoss = ((prevAL * (period - 1)) + decline) / period;

                            rs = averageGain / averageLoss;

                            rsi = (100 - (100 / (1 + rs)));


                            if (Double.IsNaN((double)rsi))
                            {
                                rsi = 0.0;
                            }

                            rsi = Math.Round((double)rsi, 2, MidpointRounding.AwayFromZero);

                            prevAG = averageGain = Math.Round((double)averageGain, 2, MidpointRounding.AwayFromZero);
                            prevAL = averageLoss = Math.Round((double)averageLoss, 2, MidpointRounding.AwayFromZero);

                            prevClose = tickRSI1.PriceClose;

                            tickRSI1.AG1 = averageGain;
                            tickRSI1.AL1 = averageLoss;
                            tickRSI1.RSI1 = rsi;
                        }
                    }

                    break;
                case 2:
                    var recExists2 = tickerList.Any(a => a.AG2 != null && a.AL2 != null);

                    if (recExists2)
                    {
                        var tickPrevAvg = (from li in tickerList
                                           where li.AG2 != null && li.AL2 != null
                                           orderby li.TickerDateTime descending
                                           select li).FirstOrDefault();

                        prevAG = tickPrevAvg.AG2;
                        prevAL = tickPrevAvg.AL2;
                        prevClose = tickPrevAvg.PriceClose;


                        var tickRSICalc2 = (from li in tickerList
                                            where li.AG2 == null && li.AL2 == null
                                            orderby li.TickerDateTime
                                            select li).ToList();

                        foreach (TickerElderIndicatorsModel tickRSI2 in tickRSICalc2)
                        {
                            change = tickRSI2.PriceClose - prevClose;

                            advance = change >= 0 ? change : 0;

                            decline = change <= 0 ? -(change) : 0;

                            averageGain = ((prevAG * (period - 1)) + advance) / period;

                            averageLoss = ((prevAL * (period - 1)) + decline) / period;

                            rs = averageGain / averageLoss;

                            rsi = (100 - (100 / (1 + rs)));


                            if (Double.IsNaN((double)rsi))
                            {
                                rsi = 0.0;
                            }

                            rsi = Math.Round((double)rsi, 2, MidpointRounding.AwayFromZero);

                            prevAG = averageGain = Math.Round((double)averageGain, 2, MidpointRounding.AwayFromZero);
                            prevAL = averageLoss = Math.Round((double)averageLoss, 2, MidpointRounding.AwayFromZero);

                            prevClose = tickRSI2.PriceClose;

                            tickRSI2.AG2 = averageGain;
                            tickRSI2.AL2 = averageLoss;
                            tickRSI2.RSI2 = rsi;
                        }
                    }

                    break;
            }


            if (prevAG != 0.0 && prevAL != 0.0)
            {


            }
            else
            {

                while (recCount + period < tickerList.Count)
                {
                    List<TickerElderIndicatorsModel> tickerListRSI = tickerList.Skip(recCount).Take(period + 1).ToList();

                    for (int i = 1; i <= period; i++)
                    {
                        change = tickerListRSI[i].PriceClose - tickerListRSI[i - 1].PriceClose;

                        if (i <= period && prevAG == 0.0 && prevAL == 0.0)
                        {
                            if (change < 0)
                            {
                                decline = decline + -(change);
                            }
                            else
                            {
                                advance = advance + change;
                            }
                        }

                        if (i == period)
                        {
                            if (prevAG == 0.0 && prevAL == 0.0)
                            {
                                averageGain = advance / period;
                                averageLoss = decline / period;
                            }
                            else
                            {
                                advance = change >= 0 ? change : 0;

                                decline = change <= 0 ? -(change) : 0;

                                averageGain = ((prevAG * (period - 1)) + advance) / period;

                                averageLoss = ((prevAL * (period - 1)) + decline) / period;
                            }

                            rs = averageGain / averageLoss;

                            rsi = (100 - (100 / (1 + rs)));

                            if (Double.IsNaN((double)rsi))
                            {
                                rsi = 0.0;
                            }

                            rsi = Math.Round((double)rsi, 2, MidpointRounding.AwayFromZero);

                            prevAG = averageGain = Math.Round((double)averageGain, 2, MidpointRounding.AwayFromZero);
                            prevAL = averageLoss = Math.Round((double)averageLoss, 2, MidpointRounding.AwayFromZero);

                            switch (rsiCnt)
                            {
                                case 1:
                                    tickerListRSI[i].AG1 = averageGain;
                                    tickerListRSI[i].AL1 = averageLoss;
                                    tickerListRSI[i].RSI1 = rsi;
                                    break;
                                case 2:
                                    tickerListRSI[i].AG2 = averageGain;
                                    tickerListRSI[i].AL2 = averageLoss;
                                    tickerListRSI[i].RSI2 = rsi;
                                    break;
                            }

                        }

                    }

                    recCount++;
                }

                //make all the remainng NULLs to 0s
                foreach (TickerElderIndicatorsModel tickNull in tickerList)
                {
                    if (tickNull.AG1 == null) { tickNull.AG1 = 0; }
                    if (tickNull.AL1 == null) { tickNull.AL1 = 0; }
                    if (tickNull.AG2 == null) { tickNull.AG2 = 0; }
                    if (tickNull.AL2 == null) { tickNull.AL2 = 0; }

                    if (tickNull.RSI1 == null) { tickNull.RSI1 = 0; }
                    if (tickNull.RSI2 == null) { tickNull.RSI2 = 0; }

                }

            }

        }

        private void MACDCalculation(List<TickerElderIndicatorsModel> tickerList)
        {
            //1. Calculate as a whole in a single shot
            //  a. Traverse to 26th period and calculate the MACD
            //  b. EMA 12 - EMA 26 = MACD
            //  c. Do this for all periods
            //  d. Calculate 1st Signal (Average of the MACD for the 9 period)
            //  e. Calculate 2nd Signal onwards (using formula)
            //      I. Signal = MACD * (2/(period + 1)) + Signal (n-1) * (1-(2/period + 1))
            //      II. Parameters 
            //          a. MACD - Current Row
            //          b. Signal Period - 9
            //          c. Signal - Current Row
            //          d. Signal (n-1) - Previous Row Signal
            //2. Calculate one by one keeping old records
            //  a. Start the process only when there are null records
            //  b. Pick the previous values

            int macdPeriod = 26;
            double? signalPeriod = 9;
            int macdCnt = 0;
            double? macd = 0.0;
            double? macdSum = 0.0;
            double? signal = 0.0;
            double? prevSignal = 0.0;
            double? histogram = 0.0;
            double? prevHistogram = 0.0;
            string histogramMovement = string.Empty;

            List<TickerElderIndicatorsModel> currentList = new List<TickerElderIndicatorsModel>();

            var recExists = tickerList.Any(a => a.MACD != null);

            if (recExists)
            {
                var tickPrevVal = (from li in tickerList
                                   where li.MACD != null
                                   orderby li.TickerDateTime descending
                                   select li).FirstOrDefault();

                prevSignal = tickPrevVal.Signal;
                prevHistogram = tickPrevVal.Histogram;

                var tickCurrVal = from cur in tickerList
                                  where cur.MACD == null
                                  orderby cur.TickerDateTime
                                  select cur;

                currentList = tickCurrVal.ToList<TickerElderIndicatorsModel>();

                foreach (TickerElderIndicatorsModel curr in currentList)
                {
                    macd = curr.EMA3 - curr.EMA4;

                    signal = (macd * (double)(2 / (signalPeriod + 1)) + prevSignal * (1 - (double)(2 / (signalPeriod + 1))));

                    histogram = macd - signal;

                    if (histogram > prevHistogram)
                        histogramMovement = "I";
                    else if (histogram < prevHistogram)
                        histogramMovement = "D";
                    else
                        histogramMovement = "N";

                    prevHistogram = histogram;
                    prevSignal = signal;

                    curr.MACD = Math.Round((double)macd, 2, MidpointRounding.AwayFromZero);
                    curr.Histogram = Math.Round((double)histogram, 2, MidpointRounding.AwayFromZero);
                    curr.Signal = Math.Round((double)signal, 2, MidpointRounding.AwayFromZero);
                    curr.HistIncDec = histogramMovement;
                }

            }
            else
            {
                for (int i = 0; i < tickerList.Count; i++)
                {

                    if (i >= macdPeriod)
                    {
                        macd = tickerList[i].EMA3 - tickerList[i].EMA4;

                        macdSum += macd;

                        macdCnt++;
                    }

                    if (macdCnt >= signalPeriod)
                    {
                        if (macdCnt == signalPeriod)
                        {
                            signal = macdSum / signalPeriod;
                        }

                        if (macdCnt > signalPeriod)
                        {
                            signal = (macd * (double)(2 / (signalPeriod + 1)) + prevSignal * (1 - (double)(2 / (signalPeriod + 1))));
                        }

                        histogram = macd - signal;
                    }

                    if (histogram > prevHistogram)
                        histogramMovement = "I";
                    else if (histogram < prevHistogram)
                        histogramMovement = "D";
                    else
                        histogramMovement = "N";

                    prevHistogram = histogram;
                    prevSignal = signal;

                    tickerList[i].MACD = Math.Round((double)macd, 2, MidpointRounding.AwayFromZero);
                    tickerList[i].Histogram = Math.Round((double)histogram, 2, MidpointRounding.AwayFromZero);
                    tickerList[i].Signal = Math.Round((double)signal, 2, MidpointRounding.AwayFromZero);
                    tickerList[i].HistIncDec = histogramMovement;

                }
            }

        }

        private void ImpulseIndicator(List<TickerElderIndicatorsModel> tickerList)
        {
            double? prevEma1 = 0.0;
            double? prevEma2 = 0.0;
            double? prevHistogram = 0.0;

            double? currEma1 = 0.0;
            double? currEma2 = 0.0;
            double? currHistogram = 0.0;

            string impulseIndicator = string.Empty;

            for (int i = 1; i < tickerList.Count; i++)
            {
                prevEma1 = tickerList[i - 1].EMA3;
                prevEma2 = tickerList[i - 1].EMA4;
                prevHistogram = tickerList[i - 1].Histogram;

                currEma1 = tickerList[i].EMA3;
                currEma2 = tickerList[i].EMA4;
                currHistogram = tickerList[i].Histogram;

                if (prevEma1 != null && prevEma2 != null && prevHistogram != null)
                {
                    if ((currEma1 > prevEma1) && (currEma2 > prevEma2) && (currHistogram > prevHistogram))
                    {
                        impulseIndicator = "G";
                    }
                    else if ((currEma1 < prevEma1) && (currEma2 < prevEma2) && (currHistogram < prevHistogram))
                    {
                        impulseIndicator = "R";
                    }
                    else
                    {
                        if (!(currEma1 == 0.0 || prevEma1 == 0.0 || currEma2 == 0.0 || prevEma2 == 0.0 || currHistogram == 0.0 || prevHistogram == 0.0))
                        {
                            impulseIndicator = "B";
                        }
                        else
                        {
                            impulseIndicator = "B";
                        }
                    }
                }

                tickerList[i].Impulse = impulseIndicator;

            }
        }

        private void AllForceIndexCalculation(List<TickerElderIndicatorsModel> tickerListElder)
        {
            string[] forceIndexesToCalculate = _settings.ForceIndexesToCalculate.Split(',');

            for (int i = 0; i < forceIndexesToCalculate.Length; i++)
            {
                ForceIndexCalculation(tickerListElder, Convert.ToInt32(forceIndexesToCalculate[i]), i + 1);
            }
        }

        private void ForceIndexCalculation(List<TickerElderIndicatorsModel> tickerList, double? fiPeriod, int forceIndexCnt = 0)
        {
            //1. Calculate as a whole in a single shot
            //  a. Calculate the ForceIndex for a period
            //  b. for index > 0, Calculate ForceIndex by Subtract the currect close from the previous close and multiply by current volume
            //  c. for index > force Index period 13, calculate the ema of the ForceIndex using formula
            //      I. First value to be the average of the 13 period FI
            //      II. From second value, use the EMA formula to determine forceIndex
            //  d. Save the value in the list
            //2. Calculate one by one keeping old records
            //  a. Start the process only when there are null records
            //  b. Pick the previous values and calculate using the same formula

            //switch (forceIndexCnt)
            //{
            //    case 1: { break; }
            //    case 2: { break; }
            //    default: break;
            //}

            double? prevFI = 0.0;
            double? currFI = 0.0;
            double? prevClose = 0.0;
            //double? fiPeriod = 13;
            double? currFISingle = 0.0;
            double? sumOfFI = 0.0;

            List<TickerElderIndicatorsModel> currentList = new List<TickerElderIndicatorsModel>();

            //var recExists = tickerList.Any(a => a.ForceIndex != null);
            bool recExists = false;

            switch (forceIndexCnt)
            {
                case 1: { recExists = tickerList.Any(a => a.ForceIndex1 != null); break; }
                case 2: { recExists = tickerList.Any(a => a.ForceIndex2 != null); break; }
                default: break;
            }

            if (recExists)
            {
                TickerElderIndicatorsModel tickPrevVal = new TickerElderIndicatorsModel();
                //TickerElderIndicatorsModel tickCurrVal;

                switch (forceIndexCnt)
                {
                    case 1:
                        {
                            tickPrevVal = (from li in tickerList
                                           where li.ForceIndex1 != null
                                           orderby li.TickerDateTime descending
                                           select li).FirstOrDefault();

                            prevFI = tickPrevVal.ForceIndex1;

                            currentList = (from cur in tickerList
                                           where cur.ForceIndex1 == null
                                           orderby cur.TickerDateTime
                                           select cur).ToList<TickerElderIndicatorsModel>();

                            break;
                        }
                    case 2:
                        {
                            tickPrevVal = (from li in tickerList
                                           where li.ForceIndex2 != null
                                           orderby li.TickerDateTime descending
                                           select li).FirstOrDefault();

                            prevFI = tickPrevVal.ForceIndex2;

                            currentList = (from cur in tickerList
                                           where cur.ForceIndex2 == null
                                           orderby cur.TickerDateTime
                                           select cur).ToList<TickerElderIndicatorsModel>();
                            break;
                        }
                    default: break;
                }

                prevClose = tickPrevVal.PriceClose;


                foreach (TickerElderIndicatorsModel curr in currentList)
                {
                    currFISingle = (curr.PriceClose - prevClose) * curr.Volume;

                    currFI = (currFISingle * (double)(2 / (fiPeriod + 1)) + prevFI * (1 - (double)(2 / (fiPeriod + 1))));

                    prevFI = currFI;
                    prevClose = curr.PriceClose;

                    switch (forceIndexCnt)
                    {
                        case 1: { curr.ForceIndex1 = Math.Round((double)currFI, 2, MidpointRounding.AwayFromZero); break; }
                        case 2: { curr.ForceIndex2 = Math.Round((double)currFI, 2, MidpointRounding.AwayFromZero); break; }
                        default: break;
                    }

                }

            }
            else
            {
                for (int i = 0; i < tickerList.Count; i++)
                {
                    if (i > 0)
                    {
                        currFISingle = (tickerList[i].PriceClose - tickerList[i - 1].PriceClose) * tickerList[i].Volume;

                        sumOfFI += currFISingle;
                    }

                    if (i == fiPeriod)
                    {
                        currFI = (double)(sumOfFI / fiPeriod);

                        prevFI = currFI;
                    }

                    if (i > fiPeriod)
                    {
                        currFI = (currFISingle * (double)(2 / (fiPeriod + 1)) + prevFI * (1 - (double)(2 / (fiPeriod + 1))));

                        prevFI = currFI;
                    }

                    switch (forceIndexCnt)
                    {
                        case 1: { tickerList[i].ForceIndex1 = Math.Round((double)currFI, 2, MidpointRounding.AwayFromZero); break; }
                        case 2: { tickerList[i].ForceIndex2 = Math.Round((double)currFI, 2, MidpointRounding.AwayFromZero); break; }
                        default: break;
                    }

                }
            }

        }

        private void AllTrueRangeCalculation(List<TickerElderIndicatorsModel> tickerListElder)
        {
            string[] atrsToCalculate = _settings.ATRsToCalculate.Split(',');

            TrueRangeCalculation(tickerListElder);

            for (int i = 0; i < atrsToCalculate.Length; i++)
            {
                AverageTrueRangeCalculation(tickerListElder, Convert.ToInt32(atrsToCalculate[i]), i + 1);
            }
        }

        private void TrueRangeCalculation(List<TickerElderIndicatorsModel> tickerList)
        {
            double? trueRange = 0.0;

            for (int i = 0; i < tickerList.Count; i++)
            {
                if (i == 0)
                {
                    double? currHigh = tickerList[i].PriceHigh;
                    double? currLow = tickerList[i].PriceLow;

                    trueRange = (double)(currHigh - currLow);
                }
                else
                {
                    double? currHigh = tickerList[i].PriceHigh;
                    double? currLow = tickerList[i].PriceLow;
                    double? prevClose = tickerList[i - 1].PriceClose;

                    trueRange = Math.Max(Math.Abs((double)(currHigh - currLow)), (double)Math.Max(Math.Abs((double)(currHigh - prevClose)), Math.Abs((double)(prevClose - currLow))));
                }

                tickerList[i].TrueRange = Math.Round((double)trueRange, 2, MidpointRounding.AwayFromZero);
            }
        }

        private void AverageTrueRangeCalculation(List<TickerElderIndicatorsModel> tickerList, double? atrPeriod, int atrCnt = 0)
        {
            double? prevATR = 0.0;
            double? currATR = 0.0;
            double? prevTrueRange = 0.0;
            //double? fiPeriod = 13;
            double? currTrueRange = 0.0;
            double? sumOfATR = 0.0;

            List<TickerElderIndicatorsModel> currentList = new List<TickerElderIndicatorsModel>();

            bool recExists = false;

            switch (atrCnt)
            {
                case 1: { recExists = tickerList.Any(a => a.ATR1 != null); break; }
                case 2: { recExists = tickerList.Any(a => a.ATR2 != null); break; }
                case 3: { recExists = tickerList.Any(a => a.ATR3 != null); break; }
                default: break;
            }

            if (recExists)
            {
                TickerElderIndicatorsModel tickPrevVal = new TickerElderIndicatorsModel();

                switch (atrCnt)
                {
                    case 1:
                        {
                            tickPrevVal = (from li in tickerList
                                           where li.ATR1 != null
                                           orderby li.TickerDateTime descending
                                           select li).FirstOrDefault();

                            prevATR = tickPrevVal.ATR1;

                            currentList = (from cur in tickerList
                                           where cur.ATR1 == null
                                           orderby cur.TickerDateTime
                                           select cur).ToList<TickerElderIndicatorsModel>();

                            break;
                        }
                    case 2:
                        {
                            tickPrevVal = (from li in tickerList
                                           where li.ATR2 != null
                                           orderby li.TickerDateTime descending
                                           select li).FirstOrDefault();

                            prevATR = tickPrevVal.ATR2;

                            currentList = (from cur in tickerList
                                           where cur.ATR2 == null
                                           orderby cur.TickerDateTime
                                           select cur).ToList<TickerElderIndicatorsModel>();
                            break;
                        }
                    case 3:
                        {
                            tickPrevVal = (from li in tickerList
                                           where li.ATR3 != null
                                           orderby li.TickerDateTime descending
                                           select li).FirstOrDefault();

                            prevATR = tickPrevVal.ATR3;

                            currentList = (from cur in tickerList
                                           where cur.ATR3 == null
                                           orderby cur.TickerDateTime
                                           select cur).ToList<TickerElderIndicatorsModel>();
                            break;
                        }
                    default: break;
                }

                prevTrueRange = tickPrevVal.TrueRange;


                foreach (TickerElderIndicatorsModel curr in currentList)
                {
                    currTrueRange = curr.TrueRange;

                    currATR = ((prevATR * (double)(atrPeriod - 1) + currTrueRange) / atrPeriod);

                    prevATR = currATR;
                    prevTrueRange = curr.TrueRange;

                    switch (atrCnt)
                    {
                        case 1: { curr.ATR1 = Math.Round((double)currATR, 2, MidpointRounding.AwayFromZero); break; }
                        case 2: { curr.ATR2 = Math.Round((double)currATR, 2, MidpointRounding.AwayFromZero); break; }
                        case 3: { curr.ATR3 = Math.Round((double)currATR, 2, MidpointRounding.AwayFromZero); break; }
                        default: break;
                    }

                }

            }
            else
            {
                for (int i = 0; i < tickerList.Count; i++)
                {
                    if (i >= 0)
                    {
                        currTrueRange = tickerList[i].TrueRange;

                        sumOfATR += currTrueRange;
                    }

                    if (i == atrPeriod - 1)
                    {
                        currATR = (double)(sumOfATR / atrPeriod);

                        prevATR = currATR;
                    }

                    if (i >= atrPeriod)
                    {
                        currATR = (((prevATR * (double)(atrPeriod - 1)) + currTrueRange) / atrPeriod);

                        prevATR = currATR;
                    }

                    switch (atrCnt)
                    {
                        case 1: { tickerList[i].ATR1 = Math.Round((double)currATR, 2, MidpointRounding.AwayFromZero); break; }
                        case 2: { tickerList[i].ATR2 = Math.Round((double)currATR, 2, MidpointRounding.AwayFromZero); break; }
                        case 3: { tickerList[i].ATR3 = Math.Round((double)currATR, 2, MidpointRounding.AwayFromZero); break; }
                        default: break;
                    }

                }
            }
        }

        private void AllSuperTrendCalculation(List<TickerElderIndicatorsModel> tickerListElder)
        {
            string[] superTrendMultipliers = _settings.SuperTrendMultipliers.Split(',');
            string[] atrsToCalculate = _settings.ATRsToCalculate.Split(',');

            for (int i = 0; i < superTrendMultipliers.Length; i++)
            {
                SuperTrendCalculation(tickerListElder, Convert.ToDouble(atrsToCalculate[i]), Convert.ToDouble(superTrendMultipliers[i]), i + 1);
            }
        }

        private void SuperTrendCalculation(List<TickerElderIndicatorsModel> tickerList, double? atrPeriod, double? stMultiplier, int stCnt = 0)
        {
            double? currATR = 0.0;
            double? currBUB = 0.0;
            double? currBLB = 0.0;
            double? currFUB = 0.0;
            double? currFLB = 0.0;
            double? currST = 0.0;
            string currTrend = string.Empty;

            double? prevATR = 0.0;
            double? prevBUB = 0.0;
            double? prevBLB = 0.0;
            double? prevFUB = 0.0;
            double? prevFLB = 0.0;
            double? prevST = 0.0;
            double? prevClose = 0.0;
            string prevTrend = string.Empty;

            #region Super Trend Algorithm

            // Basic Upper Band Calculation = (High + Low) / 2 + Multiplier * ATR
            // Basic Lower Band Calculation = (High + Low) / 2 – Multiplier * ATR

            // Final Upper Band Calculation = 
            // IF( (Current BasicUpperband  < Previous Final Upperband) OR (Previous Close > Previous Final Upperband)) THEN 
            //      Current Basic Upperband 
            // ELSE 
            //      Previous FinalUpperband

            // Final Lowerband Calculation = 
            // IF((Current Basic Lowerband > Previous Final Lowerband) OR (Previous Close < Previous Final Lowerband)) THEN
            //      Current Basic Lowerband 
            // ELSE 
            //      Previous Final Lowerband

            // SUPERTREND = 
            //      IF (Previous Super Trend = Previous Final Upper Band) AND (Current Close < Current Final Upper Band) THEN
            //          Current Final Upperband 
            //      ELSE IF (Previous Super Trend = Previous Final Upper Band) AND (Current Close > Current Final Upper Band) THEN
            //          Current Final Lowerband
            //      ELSE IF (Previous Super Trend = Previous Final Lower Band) AND (Current Close > Current Final Lower Band) THEN
            //          Current Final Lowerband
            //      ELSE IF (Previous Super Trend = Previous Final Lower Band) AND (Current Close < Current Final Lower Band) THEN
            //          Current Final Upperband 
            //      ELSE
            //          0

            #endregion

            List<TickerElderIndicatorsModel> currentList = new List<TickerElderIndicatorsModel>();

            bool recExists = false;

            switch (stCnt)
            {
                case 1: { recExists = tickerList.Any(a => a.ST1 != null); break; }
                case 2: { recExists = tickerList.Any(a => a.ST2 != null); break; }
                case 3: { recExists = tickerList.Any(a => a.ST3 != null); break; }
                default: break;
            }

            if (recExists)
            {
                TickerElderIndicatorsModel tickPrevVal = new TickerElderIndicatorsModel();

                switch (stCnt)
                {
                    case 1:
                        {
                            tickPrevVal = (from li in tickerList
                                           where li.ST1 != null
                                           orderby li.TickerDateTime descending
                                           select li).FirstOrDefault();

                            prevClose = tickPrevVal.PriceClose;
                            prevATR = tickPrevVal.ATR1;
                            prevBUB = tickPrevVal.BUB1;
                            prevBLB = tickPrevVal.BLB1;
                            prevFUB = tickPrevVal.FUB1;
                            prevFLB = tickPrevVal.FLB1;
                            prevST = tickPrevVal.ST1;
                            prevTrend = tickPrevVal.Trend1;

                            currentList = (from cur in tickerList
                                           where cur.ST1 == null
                                           orderby cur.TickerDateTime
                                           select cur).ToList<TickerElderIndicatorsModel>();

                            break;
                        }
                    case 2:
                        {
                            tickPrevVal = (from li in tickerList
                                           where li.ST2 != null
                                           orderby li.TickerDateTime descending
                                           select li).FirstOrDefault();

                            prevClose = tickPrevVal.PriceClose;
                            prevATR = tickPrevVal.ATR2;
                            prevBUB = tickPrevVal.BUB2;
                            prevBLB = tickPrevVal.BLB2;
                            prevFUB = tickPrevVal.FUB2;
                            prevFLB = tickPrevVal.FLB2;
                            prevST = tickPrevVal.ST2;
                            prevTrend = tickPrevVal.Trend2;

                            currentList = (from cur in tickerList
                                           where cur.ST2 == null
                                           orderby cur.TickerDateTime
                                           select cur).ToList<TickerElderIndicatorsModel>();
                            break;
                        }
                    case 3:
                        {
                            tickPrevVal = (from li in tickerList
                                           where li.ST3 != null
                                           orderby li.TickerDateTime descending
                                           select li).FirstOrDefault();

                            prevClose = tickPrevVal.PriceClose;
                            prevATR = tickPrevVal.ATR3;
                            prevBUB = tickPrevVal.BUB3;
                            prevBLB = tickPrevVal.BLB3;
                            prevFUB = tickPrevVal.FUB3;
                            prevFLB = tickPrevVal.FLB3;
                            prevST = tickPrevVal.ST3;
                            prevTrend = tickPrevVal.Trend3;

                            currentList = (from cur in tickerList
                                           where cur.ST3 == null
                                           orderby cur.TickerDateTime
                                           select cur).ToList<TickerElderIndicatorsModel>();
                            break;
                        }
                    default: break;
                }

                foreach (TickerElderIndicatorsModel curr in currentList)
                {
                    switch (stCnt)
                    {
                        case 1: currATR = curr.ATR1; break;
                        case 2: currATR = curr.ATR2; break;
                        case 3: currATR = curr.ATR3; break;
                        default: break;
                    }

                    currBUB = Math.Round((double)(((curr.PriceHigh + curr.PriceLow) / 2) + (stMultiplier * currATR)), 1, MidpointRounding.ToEven);

                    currBLB = Math.Round((double)(((curr.PriceHigh + curr.PriceLow) / 2) - (stMultiplier * currATR)), 1, MidpointRounding.ToEven);

                    if ((currBUB < prevFUB) || (prevClose > prevFUB))
                    {
                        currFUB = currBUB;
                    }
                    else
                    {
                        currFUB = prevFUB;
                    }

                    if ((currBLB > prevFLB) || (prevClose < prevFLB))
                    {
                        currFLB = currBLB;
                    }
                    else
                    {
                        currFLB = prevFLB;
                    }

                    if ((prevST == prevFUB) && (curr.PriceClose < currFUB))
                    {
                        currST = currFUB;

                        if (prevTrend == AuxiliaryMethods.TREND_UP)
                            currTrend = AuxiliaryMethods.TREND_CHANGE_DOWN;
                        else
                            currTrend = AuxiliaryMethods.TREND_DOWN;
                    }
                    else if ((prevST == prevFUB) && (curr.PriceClose > currFUB))
                    {
                        currST = currFLB;

                        if (prevTrend == AuxiliaryMethods.TREND_DOWN)
                            currTrend = AuxiliaryMethods.TREND_CHANGE_UP;
                        else
                            currTrend = AuxiliaryMethods.TREND_UP;
                    }
                    else if ((prevST == prevFLB) && (curr.PriceClose > currFLB))
                    {
                        currST = currFLB;

                        if (prevTrend == AuxiliaryMethods.TREND_DOWN)
                            currTrend = AuxiliaryMethods.TREND_CHANGE_UP;
                        else
                            currTrend = AuxiliaryMethods.TREND_UP;
                    }
                    else if ((prevST == prevFLB) && (curr.PriceClose < currFLB))
                    {
                        currST = currFUB;

                        if (prevTrend == AuxiliaryMethods.TREND_UP)
                            currTrend = AuxiliaryMethods.TREND_CHANGE_DOWN;
                        else
                            currTrend = AuxiliaryMethods.TREND_DOWN;
                    }
                    else
                    {
                        currST = prevST;
                        currTrend = prevTrend;
                    }

                    prevBUB = currBUB;
                    prevBLB = currBLB;
                    prevFUB = currFUB;
                    prevFLB = currFLB;
                    prevST = currST;
                    prevTrend = currTrend;
                    prevClose = curr.PriceClose;

                    switch (stCnt)
                    {
                        case 1:
                            {
                                curr.BUB1 = currBUB;
                                curr.BLB1 = currBLB;
                                curr.FUB1 = currFUB;
                                curr.FLB1 = currFLB;
                                curr.ST1 = currST;
                                curr.Trend1 = currTrend;
                                break;
                            }
                        case 2:
                            {
                                curr.BUB2 = currBUB;
                                curr.BLB2 = currBLB;
                                curr.FUB2 = currFUB;
                                curr.FLB2 = currFLB;
                                curr.ST2 = currST;
                                curr.Trend2 = currTrend;
                                break;
                            }
                        case 3:
                            {
                                curr.BUB3 = currBUB;
                                curr.BLB3 = currBLB;
                                curr.FUB3 = currFUB;
                                curr.FLB3 = currFLB;
                                curr.ST3 = currST;
                                curr.Trend3 = currTrend;
                                break;
                            }
                        default: break;
                    }

                }
            }
            else
            {
                for (int i = 0; i < tickerList.Count; i++)
                {
                    if (i == atrPeriod - 1)
                    {
                        switch (stCnt)
                        {
                            case 1:
                                {
                                    prevATR = tickerList[i].ATR1 == null ? 0.0 : tickerList[i].ATR1;
                                    prevBUB = tickerList[i].BUB1 == null ? 0.0 : tickerList[i].BUB1;
                                    prevBLB = tickerList[i].BLB1 == null ? 0.0 : tickerList[i].BLB1;
                                    prevFUB = tickerList[i].FUB1 == null ? 0.0 : tickerList[i].FUB1;
                                    prevFLB = tickerList[i].FLB1 == null ? 0.0 : tickerList[i].FLB1;
                                    prevST = tickerList[i].ST1 == null ? 0.0 : tickerList[i].ST1;
                                    prevTrend = tickerList[i].Trend1 == null ? string.Empty : tickerList[i].Trend1;
                                    break;
                                }
                            case 2:
                                {
                                    prevATR = tickerList[i].ATR2 == null ? 0.0 : tickerList[i].ATR2;
                                    prevBUB = tickerList[i].BUB2 == null ? 0.0 : tickerList[i].BUB2;
                                    prevBLB = tickerList[i].BLB2 == null ? 0.0 : tickerList[i].BLB2;
                                    prevFUB = tickerList[i].FUB2 == null ? 0.0 : tickerList[i].FUB2;
                                    prevFLB = tickerList[i].FLB2 == null ? 0.0 : tickerList[i].FLB2;
                                    prevST = tickerList[i].ST2 == null ? 0.0 : tickerList[i].ST2;
                                    prevTrend = tickerList[i].Trend2 == null ? string.Empty : tickerList[i].Trend2;
                                    break;
                                }
                            case 3:
                                {
                                    prevATR = tickerList[i].ATR3 == null ? 0.0 : tickerList[i].ATR3;
                                    prevBUB = tickerList[i].BUB3 == null ? 0.0 : tickerList[i].BUB3;
                                    prevBLB = tickerList[i].BLB3 == null ? 0.0 : tickerList[i].BLB3;
                                    prevFUB = tickerList[i].FUB3 == null ? 0.0 : tickerList[i].FUB3;
                                    prevFLB = tickerList[i].FLB3 == null ? 0.0 : tickerList[i].FLB3;
                                    prevST = tickerList[i].ST3 == null ? 0.0 : tickerList[i].ST3;
                                    prevTrend = tickerList[i].Trend3 == null ? string.Empty : tickerList[i].Trend3;
                                    break;
                                }
                            default: break;
                        }

                        prevClose = tickerList[i].PriceClose;
                    }

                    if (i >= atrPeriod)
                    {
                        switch (stCnt)
                        {
                            case 1: { currATR = tickerList[i].ATR1; break; }
                            case 2: { currATR = tickerList[i].ATR2; break; }
                            case 3: { currATR = tickerList[i].ATR3; break; }
                            default: break;
                        }

                        currBUB = Math.Round((double)(((tickerList[i].PriceHigh + tickerList[i].PriceLow) / 2) + (stMultiplier * currATR)), 1, MidpointRounding.ToEven);

                        currBLB = Math.Round((double)(((tickerList[i].PriceHigh + tickerList[i].PriceLow) / 2) - (stMultiplier * currATR)), 1, MidpointRounding.ToEven);

                        if ((currBUB < prevFUB) || (prevClose > prevFUB))
                        {
                            currFUB = currBUB;
                        }
                        else
                        {
                            currFUB = prevFUB;
                        }

                        if ((currBLB > prevFLB) || (prevClose < prevFLB))
                        {
                            currFLB = currBLB;
                        }
                        else
                        {
                            currFLB = prevFLB;
                        }

                        if ((prevST == prevFUB) && (tickerList[i].PriceClose < currFUB))
                        {
                            currST = currFUB;

                            if (prevTrend == AuxiliaryMethods.TREND_UP)
                                currTrend = AuxiliaryMethods.TREND_CHANGE_DOWN;
                            else
                                currTrend = AuxiliaryMethods.TREND_DOWN;
                        }
                        else if ((prevST == prevFUB) && (tickerList[i].PriceClose > currFUB))
                        {
                            currST = currFLB;

                            if (prevTrend == AuxiliaryMethods.TREND_DOWN)
                                currTrend = AuxiliaryMethods.TREND_CHANGE_UP;
                            else
                                currTrend = AuxiliaryMethods.TREND_UP;
                        }
                        else if ((prevST == prevFLB) && (tickerList[i].PriceClose > currFLB))
                        {
                            currST = currFLB;

                            if (prevTrend == AuxiliaryMethods.TREND_DOWN)
                                currTrend = AuxiliaryMethods.TREND_CHANGE_UP;
                            else
                                currTrend = AuxiliaryMethods.TREND_UP;
                        }
                        else if ((prevST == prevFLB) && (tickerList[i].PriceClose < currFLB))
                        {
                            currST = currFUB;

                            if (prevTrend == AuxiliaryMethods.TREND_UP)
                                currTrend = AuxiliaryMethods.TREND_CHANGE_DOWN;
                            else
                                currTrend = AuxiliaryMethods.TREND_DOWN;
                        }
                        else
                        {
                            currST = prevST;
                            currTrend = prevTrend;
                        }

                        prevBUB = currBUB;
                        prevBLB = currBLB;
                        prevFUB = currFUB;
                        prevFLB = currFLB;
                        prevST = currST;
                        prevTrend = currTrend;
                        prevClose = tickerList[i].PriceClose;
                    }

                    switch (stCnt)
                    {
                        case 1:
                            {
                                tickerList[i].BUB1 = currBUB;
                                tickerList[i].BLB1 = currBLB;
                                tickerList[i].FUB1 = currFUB;
                                tickerList[i].FLB1 = currFLB;
                                tickerList[i].ST1 = currST;
                                tickerList[i].Trend1 = currTrend;
                                break;
                            }
                        case 2:
                            {
                                tickerList[i].BUB2 = currBUB;
                                tickerList[i].BLB2 = currBLB;
                                tickerList[i].FUB2 = currFUB;
                                tickerList[i].FLB2 = currFLB;
                                tickerList[i].ST2 = currST;
                                tickerList[i].Trend2 = currTrend;
                                break;
                            }
                        case 3:
                            {
                                tickerList[i].BUB3 = currBUB;
                                tickerList[i].BLB3 = currBLB;
                                tickerList[i].FUB3 = currFUB;
                                tickerList[i].FLB3 = currFLB;
                                tickerList[i].ST3 = currST;
                                tickerList[i].Trend3 = currTrend;
                                break;
                            }
                        default: break;
                    }

                }
            }
        }

        private void CalculateEMADeviation(List<TickerElderIndicatorsModel> tickerListElder)
        {
            //Calculation is done for MACD periods 13 and 26
            //Fields used for this calculation is EMA1 and EMA4

            foreach (TickerElderIndicatorsModel curr in tickerListElder)
            {
                if (curr.EMA1 != 0)
                    curr.EMA1Dev = Math.Round((double)((curr.PriceClose - curr.EMA1) / curr.EMA1 * 100), 2);

                if (curr.EMA4 != 0)
                    curr.EMA2Dev = Math.Round((double)((curr.PriceClose - curr.EMA4) / curr.EMA4 * 100), 2);
            }
        }

    }
}
