using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Collections;
using TradeGenie.Repository;
using TradeGenie;

namespace TradeGenie
{
    //https://www.tradinformed.com/2013/02/17/use-excel-to-backtest-a-trading-strategy-using-an-atr-stop-loss/

    public class ElderBusinessLogic
    {

        List<MasterStockList> mslList = new List<MasterStockList>();

        public void MainBusinessLogic(int timePeriod)
        {
            //Get the list of stocks from the master stock list in DB    
            mslList = TradeGenieForm.dbMethods.GetMasterStockList();

            foreach (MasterStockList stock in mslList)
            {
                try
                {
                    List<TickerElderIndicatorsVM> tickerListElder = new List<TickerElderIndicatorsVM>();

                    if (UserConfiguration.CalculateElderIndicators)
                    {
                        double prevEMA = 0.0;
                        bool recExists = false;
                        string[] emasToCalculate = UserConfiguration.EMAsToCalculate.Split(',');
                        string[] rsisToCalculate = UserConfiguration.RSIsToCalculate.Split(',');

                        tickerListElder = TradeGenieForm.dbMethods.GetTickerDataForElder(stock, timePeriod);

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

                        for (int i = 0; i < rsisToCalculate.Length; i++)
                        {
                            switch (i)
                            {
                                case 0:

                                    //1. If none of the RSI is calculated, take the whole list, and run for the RSI periods

                                    RSICalculation(tickerListElder, Convert.ToInt32(rsisToCalculate[i]), i + 1); break;
                                case 1:
                                    RSICalculation(tickerListElder, Convert.ToInt32(rsisToCalculate[i]), i + 1); break;
                            }
                        }

                        MACDCalculation(tickerListElder);

                        ForceIndexCalculation(tickerListElder);

                        ImpulseIndicator(tickerListElder);

                        TradeGenieForm.dbMethods.InsertTickerElderIndicators(tickerListElder, timePeriod);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ErrorLogToFile(ex);
                }
            }
        }

        /// <summary>
        /// This process will be called when the time interval call is made. 
        /// Same process will be run for the multiple time periods based on the config
        /// </summary>
        public void DownloadAndLoadData(int timePeriod)
        {
            //Get the list of stocks from the master stock list in DB    
            mslList = TradeGenieForm.dbMethods.GetMasterStockList();

            foreach (MasterStockList stock in mslList)
            {
                List<TickerElderIndicatorsVM> listStockTicker = new List<TickerElderIndicatorsVM>();

                if (UserConfiguration.DownloadData)
                {
                    try
                    {
                        string tickerRawData = DownloadData(stock.TradingSymbol, timePeriod);

                        if (tickerRawData != string.Empty)
                        {
                            listStockTicker = ParseDownloadData(stock.TradingSymbol, tickerRawData, timePeriod);

                            if (listStockTicker.Count > 0)
                            {
                                TradeGenieForm.dbMethods.InsertInitialElderData(listStockTicker);
                            }

                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorLogToFile(ex);
                    }
                }
            }
        }

        private void EMACalculation(List<TickerElderIndicatorsVM> tickerList, int period, int emaCnt, double? prevEma = 0.0)
        {

            int emaCounter = 0;
            double? smaSum = 0.0;
            double? sma = 0.0;
            double? ema = 0.0;
            double? currentPrice = 0.0;

            foreach (TickerElderIndicatorsVM ticker in tickerList)
            {
                switch (emaCnt)
                {
                    case 1: ema = ticker.EMA1; break;
                    case 2: ema = ticker.EMA2; break;
                    case 3: ema = ticker.EMA3; break;
                    case 4: ema = ticker.EMA4; break;
                }

                if (ema == null && emaCounter < period && prevEma == 0.0)
                {
                    smaSum += ticker.PriceClose;
                    emaCounter++;

                    if (period == emaCounter)
                    {
                        sma = smaSum / period;
                        prevEma = ema = Math.Round((double)sma, 2, MidpointRounding.AwayFromZero); ;
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

                    prevEma = ema = Math.Round((double)ema, 2, MidpointRounding.AwayFromZero);
                }

                switch (emaCnt)
                {
                    case 1: ticker.EMA1 = ema; break;
                    case 2: ticker.EMA2 = ema; break;
                    case 3: ticker.EMA3 = ema; break;
                    case 4: ticker.EMA4 = ema; break;
                }
            }
        }

        private void RSICalculation(List<TickerElderIndicatorsVM> tickerList, int period, int rsiCnt = 0)
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

            switch (rsiCnt) {
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

                        foreach (TickerElderIndicatorsVM tickRSI1 in tickRSICalc1)
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

                        foreach (TickerElderIndicatorsVM tickRSI2 in tickRSICalc2)
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
                    List<TickerElderIndicatorsVM> tickerListRSI = tickerList.Skip(recCount).Take(period + 1).ToList();

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
                foreach(TickerElderIndicatorsVM tickNull in tickerList)
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

        private void MACDCalculation(List<TickerElderIndicatorsVM> tickerList)
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

            List<TickerElderIndicatorsVM> currentList = new List<TickerElderIndicatorsVM>();

            var recExists = tickerList.Any(a => a.MACD != null);

            if (recExists)
            {
                var tickPrevVal = (from li in tickerList
                                   where li.MACD != null 
                                   orderby li.TickerDateTime descending
                                   select li).FirstOrDefault();

                prevSignal = tickPrevVal.Signal;

                var tickCurrVal = from cur in tickerList
                                  where cur.MACD == null
                                  orderby cur.TickerDateTime
                                  select cur;

                currentList = tickCurrVal.ToList<TickerElderIndicatorsVM>();

                foreach (TickerElderIndicatorsVM curr in currentList)
                {
                    macd = curr.EMA3 - curr.EMA4;

                    signal = (macd * (double)(2 / (signalPeriod + 1)) + prevSignal * (1 - (double)(2 / (signalPeriod + 1))));

                    histogram = macd - signal;

                    prevSignal = signal;

                    curr.MACD = Math.Round((double)macd, 2, MidpointRounding.AwayFromZero);
                    curr.Histogram = Math.Round((double)histogram, 2, MidpointRounding.AwayFromZero);
                    curr.Signal = Math.Round((double)signal, 2, MidpointRounding.AwayFromZero);
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

                    prevSignal = signal;

                    tickerList[i].MACD = Math.Round((double)macd, 2, MidpointRounding.AwayFromZero); 
                    tickerList[i].Histogram = Math.Round((double)histogram, 2, MidpointRounding.AwayFromZero); 
                    tickerList[i].Signal = Math.Round((double)signal, 2, MidpointRounding.AwayFromZero);
                }
            }

        }

        private void ImpulseIndicator(List<TickerElderIndicatorsVM> tickerList)
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
                            impulseIndicator = null;
                        }
                    }
                }

                tickerList[i].Impulse = impulseIndicator;

            }
        }

        private void ForceIndexCalculation(List<TickerElderIndicatorsVM> tickerList)
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

            double? prevFI = 0.0;
            double? currFI = 0.0;
            double? prevClose = 0.0;
            double? fiPeriod = 13;
            double? currFISingle = 0.0;
            double? sumOfFI = 0.0;

            List<TickerElderIndicatorsVM> currentList = new List<TickerElderIndicatorsVM>();

            var recExists = tickerList.Any(a => a.ForceIndex1 != null);

            if (recExists)
            {
                var tickPrevVal = (from li in tickerList
                                   where li.ForceIndex1 != null
                                   orderby li.TickerDateTime descending
                                   select li).FirstOrDefault();

                prevFI = tickPrevVal.ForceIndex1;
                prevClose = tickPrevVal.PriceClose;

                var tickCurrVal = from cur in tickerList
                                  where cur.ForceIndex1 == null
                                  orderby cur.TickerDateTime
                                  select cur;

                currentList = tickCurrVal.ToList<TickerElderIndicatorsVM>();

                foreach (TickerElderIndicatorsVM curr in currentList)
                {
                    currFISingle = (curr.PriceClose - prevClose) * curr.Volume;

                    currFI = (currFISingle * (double)(2 / (fiPeriod + 1)) + prevFI * (1 - (double)(2 / (fiPeriod + 1))));

                    prevFI = currFI;
                    prevClose = curr.PriceClose;

                    curr.ForceIndex1 = Math.Round((double)currFI, 2, MidpointRounding.AwayFromZero);
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

                    tickerList[i].ForceIndex1 = Math.Round((double)currFI, 2, MidpointRounding.AwayFromZero);
                }
            }

        }

        private string GetDownloadUrl(string stockTicker, int timePeriod)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(UserConfiguration.GooglePriceUrl);
            sb.Append("?");
            sb.Append("f=" + UserConfiguration.ValuesToFetch);
            sb.Append("&");
            sb.Append("p=" + UserConfiguration.Period);
            sb.Append("&");
            sb.Append("q=" + stockTicker.Trim());
            sb.Append("&");
            sb.Append("x=" + UserConfiguration.Exchange);
            sb.Append("&");
            //sb.Append("i=" + uc.Interval);
            sb.Append("i=" + (timePeriod * 60).ToString());
            

            return sb.ToString();
        }

        private string DownloadData(string stockTicker, int timePeriod)
        {
            string uri = GetDownloadUrl(stockTicker, timePeriod);
            string downloadedData = string.Empty;

            if (String.IsNullOrEmpty(uri))
                return string.Empty;

            using (WebClient wClient = new WebClient())
            {
                wClient.Headers.Add("user-agent", "Microsoft Visual Studio");

                downloadedData = wClient.DownloadString(uri);
            }

            return downloadedData;
        }

        private List<TickerElderIndicatorsVM> ParseDownloadData(string stockTicker, string downloadedData, int timePeriod)
        {
            string errorMessage;
            int instrumentToken = 0;
            List<TickerElderIndicatorsVM> tickerElderData = new List<TickerElderIndicatorsVM>();
            DataProcessor dp = new DataProcessor();

            if (downloadedData == string.Empty)
            {
                errorMessage = "No data in download data";
            }

            using (MemoryStream ms = new MemoryStream(System.Text.Encoding.Default.GetBytes(downloadedData)))
            {
                instrumentToken = TradeGenieForm.dbMethods.GetInstrumentToken(stockTicker);

                tickerElderData = dp.extractHistoricalData(ms, instrumentToken, stockTicker, timePeriod, out errorMessage);
            }

            return tickerElderData;
        }

    }
}
