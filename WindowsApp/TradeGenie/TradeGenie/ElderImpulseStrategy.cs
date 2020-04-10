using KiteConnect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradeGenie.Repository;

namespace TradeGenie
{
    public partial class ElderImpulseStrategy : TradeGenieForm
    {
        /// <summary>
        /// 1. Loop thru all the stocks that are configured in the MasterStockList and pick the stocks that are not in position
        /// 2. Take the last 3 records from the Elder Data of 5 min Timeperiod (Based on the time passed as input param)
        /// 3. Check the EMA2Dev (configure) of all the 3 records
        /// 
        /// For Long Order & Short Order Scan
        /// 4. Please refer the logic in the code. 
        /// 
        /// Place Order
        /// 5. Calculate the Quantity
        /// 6. Calculate the Stop loss for the Cover order
        /// 7. Place the Cover Order and record the Price and Order Id
        /// 
        /// Monitor Positions and Modify Orders
        /// 8. Load OTMP table.
        /// 9. Pick all the SL-M Orders which are in "Trigger Pending" status
        /// 10. Get the LTP of all the SL-M instruments
        /// 11. Calculate the profit percentage. 
        /// 12. If the Profit is between 0.3% and 0.6%, then modify the SL-M order trigger to Buy Price
        /// 13. If the profit is between 0.6% and 0.9%, then modify the SL-M order trigger to 0.3% Profit 
        /// 14. Do the same continously, till the SL-M order gets executed.
        /// 
        /// Exit Orders
        /// 15. Exit all the "Trigger Pending" SL-M orders @ 15:15 pm
        /// 
        /// </summary>

        /*
         *Things to Do
            1. First SL to be Break Even, after that previous 15 min candle Low/High * 0.1 (little higher than the indicator, to have leverage) based on the position.
            2. Order Status tobe checked before updating the order.
            3. If the SL order status is completed, it has to be updated in the Elder table with exit price
            4. Elder Table has to be updated once the order is placed and Entry/SL to be synced with the market.
            5. Update the real time position Labels
            6. Duplicate orders to be eliminated when the order is in position
         * **/

        List<MasterStockList> mslList = new List<MasterStockList>();

        private Object thisLock = new Object();
        private static bool placeOrders = true;
        private static bool getOrders = true;
        private int currentIndex = 1;
        private int previousIndex = 0;

        //double percentageDeviationLongBottom = 0.25, percentageDeviationLongTop = 1;
        //double percentageDeviationShortBottom = -1, percentageDeviationShortTop = -0.25;
        DateTime tradeDate = DateTime.Today;
        TimeSpan CurrentTime = new TimeSpan(DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, 0);
        TimeSpan startTime = new TimeSpan(9, 15, 0);
        TimeSpan endTime = new TimeSpan(15, 15, 0);
        DateTime fromDateTime;
        DateTime toDateTime;
        DateTime endDateTime;
        DateTime lastRunTimeLong;
        DateTime lastRunTimeShort;

        Thread thElderStrategy;
        Thread thRealTimePTMFetch;
        private volatile bool mStopThread;
        private string sortDirection;

        List<ElderStrategyOrderVM> currentPosition = new List<ElderStrategyOrderVM>();

        public ElderImpulseStrategy()
        {
            InitializeComponent();

            ToggleRealTimeOrderPlacement();

            InitializeThreads();
        }

        public void MainBusinessLogic()
        {
            string longTimerToRun = string.Empty;
            string shortTimerToRun = string.Empty;

            List<ElderStrategyOrder> longOrders = new List<ElderStrategyOrder>();
            List<ElderStrategyOrder> shortOrders = new List<ElderStrategyOrder>();


            longTimerToRun = TimerToRun(UserConfiguration.TimePeriodLong);
            shortTimerToRun = TimerToRun(UserConfiguration.TimePeriodShort);

            mslList = dbMethods.GetMasterStockList();

            while (!mStopThread)
            {
                try
                {

                    CurrentTime = new TimeSpan(DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, 0);

                    fromDateTime = DateTime.Today + startTime;
                    toDateTime = DateTime.Today + CurrentTime;
                    endDateTime = DateTime.Today + endTime;


                    if (Utilities.MinuteTimer(longTimerToRun).Contains(CurrentTime))
                    {
                        if (lastRunTimeLong != DateTime.Today + CurrentTime)
                        {
                            longOrders = new List<ElderStrategyOrder>();
                            shortOrders = new List<ElderStrategyOrder>();

                            List<TickerElderIndicatorsVM> elderListBig =
                            GetTickerElderForTimePeriod(UserConfiguration.TimePeriodLong.ToString(), fromDateTime, toDateTime);

                            longOrders = ScanForLong(elderListBig);

                            shortOrders = ScanForShort(elderListBig);
                        }

                        lastRunTimeLong = DateTime.Today + CurrentTime;
                    }

                    if (Utilities.MinuteTimer(shortTimerToRun).Contains(CurrentTime))
                    {
                        if (lastRunTimeShort != DateTime.Today + CurrentTime)
                        {
                            //For Long
                            PlaceLongOrders(longOrders);

                            //For Short
                            PlaceShortOrders(shortOrders);
                        }

                        lastRunTimeShort = DateTime.Today + CurrentTime;
                    }


                    Thread.Sleep(UserConfiguration.DelayInSec * 1000);

                    //Referesh the order table here, by getting the orders from Kite
                    //GetPositions();

                    //For all the ElderStrategyOrders, which doesnt have SLOrderId, load it from Order table
                    AssignSLOrderIds();

                    //Monitor and change the SL
                    MonitorPositions(fromDateTime, toDateTime);

                }
                catch (Exception ex)
                {
                    Logger.ErrorLogToFile(ex);
                }

            }


        }

        private string TimerToRun(int timePeriod)
        {
            string retValue = string.Empty;

            switch (timePeriod)
            {
                case 5:
                    retValue = UserConfiguration.Min5Timer;
                    break;
                case 15:
                    retValue = UserConfiguration.Min15Timer;
                    break;
                case 30:
                    retValue = UserConfiguration.Min30Timer;
                    break;
            }

            return retValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public List<TickerElderIndicatorsVM> GetTickerElderForTimePeriod(string timePeriod, DateTime startDateTime, DateTime endDateTime)
        {
            string instrumentList = string.Join(",", from item in mslList select item.TradingSymbol);

            List<TickerElderIndicatorsVM> tikcerElderList = dbMethods.GetTickerDataForIndicators
                                                (instrumentList, timePeriod, startDateTime, endDateTime);

            return tikcerElderList;

        }

        public List<TickerElderIndicatorsVM> GetTickerTimePeriodForOrders(string tradingSymbol, string timePeriod, DateTime startDateTime, DateTime endDateTime)
        {
            List<TickerElderIndicatorsVM> tikcerElderList = dbMethods.GetTickerDataForIndicators
                                                (tradingSymbol, timePeriod, startDateTime, endDateTime);

            return tikcerElderList;

        }

        private List<ElderStrategyOrder> ScanForLong(List<TickerElderIndicatorsVM> elderListBig)
        {
            List<ElderStrategyOrder> longList = new List<ElderStrategyOrder>();

            foreach (MasterStockList item in mslList)
            {
                try
                {

                    List<TickerElderIndicatorsVM> selStock = elderListBig.Where(sel => sel.TradingSymbol == item.TradingSymbol).
                                                                OrderBy(ord => ord.TickerDateTime).ToList();

                    //List<TickerElderIndicatorsVM> selStockSmall = elderListSmall.Where(sel => sel.TradingSymbol == item.TradingSymbol).
                    //                                            OrderBy(ord => ord.TickerDateTime).ToList();

                    if (selStock.Count != 2)
                    {
                        continue;
                    }

                    if (IsCurrentClosePriceGreaterThanShortEMA(selStock)
                        && IsCurrentOpenClosePriceGreaterThanMediumEMA(selStock)
                        && IsCurrentOpenClosePriceGreaterThanLongEMA(selStock)
                        && IsCurrentLowPriceGreaterThanLongEMA(selStock)
                        //&& IsPreviousOpenPriceGreaterThanLongEMA(selStock)
                        && IsPreviousClosePriceGreaterThanShortEMA(selStock)
                        && CurrentImpulseColor(selStock) == Utilities.ImpulseColor.GREEN
                        && (PreviousImpulseColor(selStock) == Utilities.ImpulseColor.GREEN
                            || PreviousImpulseColor(selStock) == Utilities.ImpulseColor.BLUE)
                        && (CurrentHistogramMovement(selStock) == Utilities.HistogramMovement.INCREASING
                            && PreviousHistogramMovement(selStock) == Utilities.HistogramMovement.INCREASING)
                        //&& IsCurrentShortEMADevGreaterThanPreviousShortEMADev(selStock)
                        //&& IsPreviousShortEMADevGreaterThanPreviousToPreviousShortEMADev(selStock)
                        && IsCurrentAndPreviousShortEMADevGreaterThanPercentageRange(selStock, UserConfiguration.EMAPerDevBottom, UserConfiguration.EMAPerDevTop)
                        && IsPreviousShortEMAGreaterThanPreviousLongEMAByPercentage(selStock, Convert.ToDouble(UserConfiguration.EMAPercentageIncreaseLow))
                        && IsCurrentShortEMAGreaterThanCurrentLongEMAByPercentage(selStock, Convert.ToDouble(UserConfiguration.EMAPercentageIncreaseHigh))
                        && IsCurrentShortEMAGreaterThanCurrentMediumEMAByPercentage(selStock, Convert.ToDouble(UserConfiguration.EMAPercentageIncreaseLow))
                        //&& TrendOfTheStock(selStock) == Utilities.Trend.UP
                        && (PriceRangeOfTheStock(selStock) == Utilities.PriceRange.LOW
                            || PriceRangeOfTheStock(selStock) == Utilities.PriceRange.MEDIUM
                            || PriceRangeOfTheStock(selStock) == Utilities.PriceRange.HIGH))
                    {

                        ElderStrategyOrder longRec = new ElderStrategyOrder();

                        longRec.InstrumentToken = selStock[currentIndex].InstrumentToken;
                        longRec.TradingSymbol = selStock[currentIndex].TradingSymbol;
                        longRec.TradeDate = tradeDate;// DateTime.Today;
                        longRec.PositionType = Utilities.LongPosition;
                        //longRec.EntryPrice = dbMethods.GetLTP(selStock[currentIndex].TradingSymbol);

                        longRec.PrevClose = Convert.ToDecimal(selStock[previousIndex].PriceClose);
                        longRec.PrevHigh = Convert.ToDecimal(selStock[previousIndex].PriceHigh);
                        longRec.PrevLow = Convert.ToDecimal(selStock[previousIndex].PriceLow);
                        longRec.PrevOpen = Convert.ToDecimal(selStock[previousIndex].PriceOpen);

                        longRec.CurrClose = Convert.ToDecimal(selStock[currentIndex].PriceClose);
                        longRec.CurrHigh = Convert.ToDecimal(selStock[currentIndex].PriceHigh);
                        longRec.CurrLow = Convert.ToDecimal(selStock[currentIndex].PriceLow);
                        longRec.CurrOpen = Convert.ToDecimal(selStock[currentIndex].PriceOpen);

                        //longRec.EntryTime = DateTime.Now;
                        longRec.ShortEMA = selStock[currentIndex].EMA1;
                        longRec.LongEMA = selStock[currentIndex].EMA2;
                        longRec.ShortEMADev = selStock[currentIndex].EMA1Dev;
                        longRec.LongEMADev = selStock[currentIndex].EMA2Dev;
                        longRec.ForceIndex = selStock[currentIndex].ForceIndex1;
                        longRec.CurrHistogramMovement = selStock[currentIndex].HistMovement;
                        longRec.PrevHistogramMovement = selStock[previousIndex].HistMovement;
                        longRec.PrevImpulse = selStock[previousIndex].Impulse.Trim();
                        longRec.CurrImpulse = selStock[currentIndex].Impulse.Trim();
                        longRec.StockTrend = TrendOfTheStock(selStock).ToString();

                        //longRec.StopLoss = GetStopLossValue(selStock, longRec.PositionType);

                        longList.Add(longRec);

                        Logger.LogToFile(string.Format("{0},{1},{2}", "SHORTLISTED", longRec.TradingSymbol, longRec.PositionType));
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogToFile("Exception on scanning : " + item.TradingSymbol);
                    Logger.ErrorLogToFile(ex);
                }

            }

            return longList;
        }

        private List<ElderStrategyOrder> ScanForShort(List<TickerElderIndicatorsVM> elderListBig)
        {
            List<ElderStrategyOrder> shortList = new List<ElderStrategyOrder>();

            foreach (MasterStockList item in mslList)
            {
                try
                {

                    List<TickerElderIndicatorsVM> selStock = elderListBig.Where(sel => sel.TradingSymbol == item.TradingSymbol).
                                                                OrderBy(ord => ord.TickerDateTime).ToList();

                    if (selStock.Count != 2)
                    {
                        continue;
                    }

                    if (IsCurrentClosePriceLesserThanShortEMA(selStock)
                        && IsCurrentOpenClosePriceLesserThanMediumEMA(selStock)
                        && IsCurrentOpenClosePriceLesserThanLongEMA(selStock)
                        && IsCurrentLowPriceLesserThanLongEMA(selStock)
                        //&& IsPreviousOpenPriceLesserThanLongEMA(selStock)
                        && IsPreviousOpenClosePriceLesserThanShortEMA(selStock)
                        && CurrentImpulseColor(selStock) == Utilities.ImpulseColor.RED
                        && (PreviousImpulseColor(selStock) == Utilities.ImpulseColor.RED
                            || PreviousImpulseColor(selStock) == Utilities.ImpulseColor.BLUE)
                        && (CurrentHistogramMovement(selStock) == Utilities.HistogramMovement.DECREASING
                            && PreviousHistogramMovement(selStock) == Utilities.HistogramMovement.DECREASING)
                        //&& IsCurrentShortEMADevLesserThanPreviousShortEMADev(selStock)
                        //&& IsPreviousShortEMADevLesserThanPreviousToPreviousShortEMADev(selStock)
                        && IsCurrentAndPreviousShortEMADevLesserThanPercentageRange(selStock, UserConfiguration.EMAPerDevBottom, UserConfiguration.EMAPerDevTop)
                        && IsPreviousShortEMALesserThanPreviousLongEMAByPercentage(selStock, Convert.ToDouble(UserConfiguration.EMAPercentageIncreaseLow))
                        && IsCurrentShortEMALesserThanCurrentLongEMAByPercentage(selStock, Convert.ToDouble(UserConfiguration.EMAPercentageIncreaseHigh))
                        && IsCurrentShortEMALesserThanCurrentMediumEMAByPercentage(selStock, Convert.ToDouble(UserConfiguration.EMAPercentageIncreaseLow))
                        //&& TrendOfTheStock(selStock) == Utilities.Trend.DOWN
                        && (PriceRangeOfTheStock(selStock) == Utilities.PriceRange.LOW
                            || PriceRangeOfTheStock(selStock) == Utilities.PriceRange.MEDIUM
                            || PriceRangeOfTheStock(selStock) == Utilities.PriceRange.HIGH))

                    {
                        ElderStrategyOrder shortRec = new ElderStrategyOrder();

                        shortRec.InstrumentToken = selStock[currentIndex].InstrumentToken;
                        shortRec.TradingSymbol = selStock[currentIndex].TradingSymbol;
                        shortRec.TradeDate = tradeDate;// DateTime.Today;
                        shortRec.PositionType = Utilities.ShortPosition;

                        shortRec.PrevClose = Convert.ToDecimal(selStock[previousIndex].PriceClose);
                        shortRec.PrevHigh = Convert.ToDecimal(selStock[previousIndex].PriceHigh);
                        shortRec.PrevLow = Convert.ToDecimal(selStock[previousIndex].PriceLow);
                        shortRec.PrevOpen = Convert.ToDecimal(selStock[previousIndex].PriceOpen);

                        shortRec.CurrClose = Convert.ToDecimal(selStock[currentIndex].PriceClose);
                        shortRec.CurrHigh = Convert.ToDecimal(selStock[currentIndex].PriceHigh);
                        shortRec.CurrLow = Convert.ToDecimal(selStock[currentIndex].PriceLow);
                        shortRec.CurrOpen = Convert.ToDecimal(selStock[currentIndex].PriceOpen);

                        shortRec.ShortEMA = selStock[currentIndex].EMA1;
                        shortRec.LongEMA = selStock[currentIndex].EMA2;
                        shortRec.ShortEMADev = selStock[currentIndex].EMA1Dev;
                        shortRec.LongEMADev = selStock[currentIndex].EMA2Dev;
                        shortRec.ForceIndex = selStock[currentIndex].ForceIndex1;
                        shortRec.CurrHistogramMovement = selStock[currentIndex].HistMovement;
                        shortRec.PrevHistogramMovement = selStock[previousIndex].HistMovement;
                        shortRec.PrevImpulse = selStock[previousIndex].Impulse.Trim();
                        shortRec.CurrImpulse = selStock[currentIndex].Impulse.Trim();
                        shortRec.StockTrend = TrendOfTheStock(selStock).ToString();

                        //shortRec.StopLoss = GetStopLossValue(selStock, shortRec.PositionType);

                        shortList.Add(shortRec);

                        Logger.LogToFile(string.Format("{0},{1},{2}", "SHORTLISTED", shortRec.TradingSymbol, shortRec.PositionType));
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogToFile("Exception on scanning : " + item.TradingSymbol);
                    Logger.ErrorLogToFile(ex);
                }
            }

            return shortList;
        }

        private void AssignSLOrderIds()
        {
            string slOrderId = string.Empty;
            string targetOrderId = string.Empty;
            string slOrderStatus = string.Empty;
            string targetOrderStatus = string.Empty;

            decimal entryPrice = 0.0M;
            decimal exitPrice = 0.0M;
            decimal slPrice = 0.0M;

            //Get all the pending elder orders. means position status is not complete
            List<ElderStrategyOrder> elderOrders = dbMethods.GetPendingOrders(tradeDate);

            //Using the orderId as parentOrderId, fetch the stoploss order id
            foreach (ElderStrategyOrder elderOrder in elderOrders)
            {
                dbMethods.GetOrderInfo(tradeDate, elderOrder.OrderId, out slOrderId, out entryPrice, out exitPrice, out slPrice,
                                    out slOrderStatus, out targetOrderStatus, out targetOrderId);

                if (!string.IsNullOrEmpty(slOrderId))
                    elderOrder.SLOrderId = slOrderId;

                if (entryPrice != 0.0M)
                    elderOrder.EntryPrice = entryPrice;

                if (slPrice != 0.0M)
                    elderOrder.StopLoss = slPrice;

                //elderOrder.TargetOrderId = targetOrderId;
                //elderOrder.TargetOrderStatus = targetOrderStatus;
                //elderOrder.SLOrderStatus = slOrderStatus;

                if (targetOrderStatus == Constants.ORDER_STATUS_COMPLETE)
                {
                    if (exitPrice != 0.0M)
                    {
                        elderOrder.ExitPrice = exitPrice;
                        elderOrder.StopLossHitTime = DateTime.Now;
                    }
                }
                else if (slOrderStatus == Constants.ORDER_STATUS_COMPLETE)
                {
                    if (exitPrice != 0.0M)
                    {
                        elderOrder.ExitPrice = exitPrice;
                        elderOrder.StopLossHitTime = DateTime.Now;
                    }
                }

                

                //if (elderOrder.TargetOrderStatus == Constants.ORDER_STATUS_COMPLETE || elderOrder.SLOrderStatus == Constants.ORDER_STATUS_COMPLETE)
                //{
                //    elderOrder.PositionStatus = Constants.ORDER_STATUS_COMPLETE;
                //}
                //else
                //{
                //    elderOrder.PositionStatus = slOrderStatus;
                //}

            }

            //update the elder table
            dbMethods.UpdateElderStrategyOrders(elderOrders);
        }

        private decimal GetStopLossValue(decimal ltp, string positionType)
        {
            decimal stopLossPrice = 0.0M;

            if (positionType == Utilities.LongPosition)
            {
                stopLossPrice = Math.Round((decimal)(ltp / UserConfiguration.StopLossPercentageHigh), 1);
            }
            else
            {
                stopLossPrice = Math.Round((decimal)(ltp * UserConfiguration.StopLossPercentageHigh), 1);
            }

            return stopLossPrice;
        }

        private decimal GetTarget(decimal ltp, string positionType)
        {
            decimal targetPrice = 0.0M;

            if (positionType == Utilities.LongPosition)
            {
                targetPrice = Math.Round((decimal)(ltp * UserConfiguration.InitialExitPercentage), 1);
            }
            else
            {
                targetPrice = Math.Round((decimal)(ltp / UserConfiguration.InitialExitPercentage), 1);
            }

            return targetPrice;
        }

        private int GetQuantity(ElderStrategyOrder elderOrder)
        {
            int quantity = 0;

            quantity = 1;

            quantity = Convert.ToInt32(Math.Round((decimal)(UserConfiguration.TotalPurchaseValue / elderOrder.EntryPrice), 0));

            return quantity;
        }

        private bool IsShortTermTrendIncreasing(string tradingSymbol)
        {
            //Check if the trend is increasing in 5 min as well.
            List<TickerElderIndicatorsVM> elderIndicatorsForShortTermTrend =
                GetTickerTimePeriodForOrders(tradingSymbol, UserConfiguration.TimePeriodShort.ToString(), fromDateTime, toDateTime);

            return (CurrentImpulseColor(elderIndicatorsForShortTermTrend) == Utilities.ImpulseColor.GREEN
                && CurrentHistogramMovement(elderIndicatorsForShortTermTrend) == Utilities.HistogramMovement.INCREASING)
                && IsCurrentHistogramValuePositive(elderIndicatorsForShortTermTrend)
                && IsCurrentClosePriceGreaterThanCurrentOpenPrice(elderIndicatorsForShortTermTrend);

        }

        private bool IsShortTermTrendDecreasing(string tradingSymbol)
        {
            //Check if the trend is increasing in 5 min as well.
            List<TickerElderIndicatorsVM> elderIndicatorsForShortTermTrend =
                GetTickerTimePeriodForOrders(tradingSymbol, UserConfiguration.TimePeriodShort.ToString(), fromDateTime, toDateTime);

            return (CurrentImpulseColor(elderIndicatorsForShortTermTrend) == Utilities.ImpulseColor.RED
                && CurrentHistogramMovement(elderIndicatorsForShortTermTrend) == Utilities.HistogramMovement.DECREASING)
                && IsCurrentHistogramValueNegative(elderIndicatorsForShortTermTrend)
                && IsCurrentClosePriceLesserThanCurrentOpenPrice(elderIndicatorsForShortTermTrend);
        }

        private void PlaceLongOrders(List<ElderStrategyOrder> longList)
        {
            foreach (ElderStrategyOrder longItem in longList)
            {
                try
                {
                    if (IsShortTermTrendIncreasing(longItem.TradingSymbol))
                    {
                        //Calculate Quantity
                        //Calculate Stop Loss
                        //Calculate Entry
                        longItem.EntryPrice = dbMethods.GetLTP(longItem.TradingSymbol);
                        longItem.StopLoss = GetStopLossValue((decimal)longItem.EntryPrice, Utilities.LongPosition);
                        longItem.Quantity = GetQuantity(longItem);
                        longItem.PositionStatus = "OPEN";

                        #region Long Bracket Order

                        if (chkBracketOrder.Checked)
                        {
                            bool isPostionExists = dbMethods.IsPositionExists(longItem.TradingSymbol, longItem.PositionType,
                                                longItem.TradeDate, Constants.VARIETY_BO, Constants.TRANSACTION_TYPE_BUY, placeOrders);

                            if (!isPostionExists)
                            {
                                if (placeOrders)
                                {
                                    longItem.OrderId = null;
                                    longItem.isRealOrderPlaced = null;
                                    longItem.OrderVariety = Constants.VARIETY_BO;
                                    longItem.EntryTime = DateTime.Now;

                                    //Calculate Target
                                    decimal targetValue = GetTarget((decimal)longItem.EntryPrice, Utilities.LongPosition);
                                    decimal boTarget, boStopLoss, boTrailingStopLoss;

                                    CalculateTargetSLTSL(Utilities.LongPosition, (decimal)longItem.EntryPrice, targetValue, (decimal)longItem.StopLoss,
                                                    out boTarget, out boStopLoss, out boTrailingStopLoss);

                                    //Place Bracket Order
                                    Dictionary<string, dynamic> response = kite.PlaceOrder(
                                                Exchange: Constants.EXCHANGE_NSE,
                                                TradingSymbol: longItem.TradingSymbol,
                                                TransactionType: Constants.TRANSACTION_TYPE_BUY,
                                                Quantity: (int)longItem.Quantity,
                                                Price: longItem.EntryPrice,
                                                OrderType: Constants.ORDER_TYPE_LIMIT,
                                                Product: Constants.PRODUCT_MIS,
                                                Variety: Constants.VARIETY_BO,
                                                SquareOffValue: boTarget,
                                                StoplossValue: boStopLoss,
                                                Validity: Constants.VALIDITY_DAY);

                                    if (response != null && response["status"] == "success")
                                    {
                                        longItem.OrderId = response["data"]["order_id"];
                                    }

                                    longItem.isRealOrderPlaced = true;

                                    Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                                        "BO Placed", longItem.TradingSymbol, Utilities.LongPosition,
                                        "Entry Price:" + longItem.EntryPrice.ToString(),
                                        "Target Price:" + boTarget.ToString(),
                                        "Stop Loss Price:" + boStopLoss.ToString(),
                                        "Trailing Stop Loss Price:" + boTrailingStopLoss.ToString(),
                                        "Quantity:" + longItem.Quantity.ToString()));
                                }
                                else
                                {
                                    longItem.OrderId = null;
                                    longItem.isRealOrderPlaced = null;
                                }

                                dbMethods.InsertElderStrategyOrder(longItem);


                            }
                        }

                        #endregion

                        #region Long Cover Order
                        if (chkCoverOrder.Checked)
                        {
                            //check if item is already long in the position table
                            bool isPostionExists = dbMethods.IsPositionExists(longItem.TradingSymbol, longItem.PositionType,
                                                longItem.TradeDate, Constants.VARIETY_CO, Constants.TRANSACTION_TYPE_BUY, placeOrders);

                            //if not, get the LTP of the instrument and place the order in the table
                            if (!isPostionExists)
                            {
                                longItem.OrderId = null;
                                longItem.isRealOrderPlaced = null;
                                longItem.OrderVariety = Constants.VARIETY_CO;
                                longItem.EntryTime = DateTime.Now;

                                if (placeOrders)
                                {
                                    //Place Cover Order 
                                    Dictionary<string, dynamic> retOrdStatus =
                                                        kite.PlaceOrder(
                                                                Exchange: Constants.EXCHANGE_NSE,
                                                                TradingSymbol: longItem.TradingSymbol,
                                                                TransactionType: Constants.TRANSACTION_TYPE_BUY,
                                                                Quantity: Convert.ToInt32(longItem.Quantity),
                                                                Product: Constants.PRODUCT_MIS,
                                                                OrderType: Constants.ORDER_TYPE_LIMIT,
                                                                Price: longItem.EntryPrice,
                                                                Validity: Constants.VALIDITY_DAY,
                                                                TriggerPrice: longItem.StopLoss,
                                                                Variety: Constants.VARIETY_CO
                                                            );

                                    if (retOrdStatus != null && retOrdStatus["status"] == "success")
                                    {
                                        longItem.OrderId = retOrdStatus["data"]["order_id"];
                                    }

                                    longItem.isRealOrderPlaced = true;

                                    Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5}",
                                        "CO Placed", longItem.TradingSymbol, Utilities.LongPosition,
                                        "Entry Price:" + longItem.EntryPrice.ToString(),
                                        "Stop Loss Price:" + longItem.StopLoss.ToString(),
                                        "Quantity:" + longItem.Quantity.ToString()));
                                }
                                else
                                {
                                    longItem.OrderId = null;
                                    longItem.isRealOrderPlaced = null;
                                }

                                dbMethods.InsertElderStrategyOrder(longItem);
                            }
                        }
                        #endregion
                    }
                }
                catch (Exception ex)
                {
                    Logger.LogToFile(string.Format("{0},{1}", "Exception", longItem.TradingSymbol));
                    Logger.ErrorLogToFile(ex);
                }
            }

        }

        private void PlaceShortOrders(List<ElderStrategyOrder> shortList)
        {
            foreach (ElderStrategyOrder shortItem in shortList)
            {
                try
                {
                    if (IsShortTermTrendDecreasing(shortItem.TradingSymbol))
                    {
                        //Calculate Quantity
                        //Calculate Stop Loss
                        //Calculate Entry
                        shortItem.EntryPrice = dbMethods.GetLTP(shortItem.TradingSymbol);
                        shortItem.StopLoss = GetStopLossValue((decimal)shortItem.EntryPrice, Utilities.ShortPosition);
                        shortItem.Quantity = GetQuantity(shortItem);
                        shortItem.PositionStatus = "OPEN";

                        if (chkBracketOrder.Checked)
                        {
                            bool isPostionExists = dbMethods.IsPositionExists(shortItem.TradingSymbol, shortItem.PositionType,
                                                shortItem.TradeDate, Constants.VARIETY_BO, Constants.TRANSACTION_TYPE_SELL, placeOrders);

                            if (!isPostionExists)
                            {
                                if (placeOrders)
                                {
                                    shortItem.OrderId = null;
                                    shortItem.isRealOrderPlaced = null;
                                    shortItem.OrderVariety = Constants.VARIETY_BO;
                                    shortItem.EntryTime = DateTime.Now;

                                    //Calculate Target
                                    decimal targetValue = GetTarget((decimal)shortItem.EntryPrice, Utilities.ShortPosition);
                                    decimal boTarget, boStopLoss, boTrailingStopLoss;

                                    CalculateTargetSLTSL(Utilities.ShortPosition, (decimal)shortItem.EntryPrice, targetValue, (decimal)shortItem.StopLoss,
                                                    out boTarget, out boStopLoss, out boTrailingStopLoss);

                                    //Place Bracket Order
                                    Dictionary<string, dynamic> response = kite.PlaceOrder(
                                                Exchange: Constants.EXCHANGE_NSE,
                                                TradingSymbol: shortItem.TradingSymbol,
                                                TransactionType: Constants.TRANSACTION_TYPE_SELL,
                                                Quantity: (int)shortItem.Quantity,
                                                Price: shortItem.EntryPrice,
                                                OrderType: Constants.ORDER_TYPE_LIMIT,
                                                Product: Constants.PRODUCT_MIS,
                                                Variety: Constants.VARIETY_BO,
                                                SquareOffValue: boTarget,
                                                StoplossValue: boStopLoss,
                                                Validity: Constants.VALIDITY_DAY);

                                    if (response != null && response["status"] == "success")
                                    {
                                        shortItem.OrderId = response["data"]["order_id"];
                                    }

                                    shortItem.isRealOrderPlaced = true;

                                    Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
                                        "BO Placed", shortItem.TradingSymbol, Utilities.ShortPosition,
                                        "Entry Price:" + shortItem.EntryPrice.ToString(),
                                        "Target Price:" + boTarget.ToString(),
                                        "Stop Loss Price:" + boStopLoss.ToString(),
                                        "Trailing Stop Loss Price:" + boTrailingStopLoss.ToString(),
                                        "Quantity:" + shortItem.Quantity.ToString()));
                                }
                                else
                                {
                                    shortItem.OrderId = null;
                                    shortItem.isRealOrderPlaced = null;
                                }

                                dbMethods.InsertElderStrategyOrder(shortItem);
                            }
                        }

                        if (chkCoverOrder.Checked)
                        {

                            //check if item is already short in the position table
                            //if not, get the LTP of the instrument and place the order in the table
                            bool isPostionExists = dbMethods.IsPositionExists(shortItem.TradingSymbol, shortItem.PositionType,
                                                    shortItem.TradeDate, Constants.VARIETY_CO, Constants.TRANSACTION_TYPE_SELL, placeOrders);

                            if (!isPostionExists)
                            {
                                if (placeOrders)
                                {
                                    shortItem.OrderId = null;
                                    shortItem.isRealOrderPlaced = null;
                                    shortItem.OrderVariety = Constants.VARIETY_CO;
                                    shortItem.EntryTime = DateTime.Now;

                                    //Place Cover Order 
                                    Dictionary<string, dynamic> retOrdStatus =
                                                       kite.PlaceOrder(
                                                               Exchange: Constants.EXCHANGE_NSE,
                                                               TradingSymbol: shortItem.TradingSymbol,
                                                               TransactionType: Constants.TRANSACTION_TYPE_SELL,
                                                               Quantity: Convert.ToInt32(shortItem.Quantity),
                                                               Product: Constants.PRODUCT_MIS,
                                                               OrderType: Constants.ORDER_TYPE_LIMIT,
                                                               Validity: Constants.VALIDITY_DAY,
                                                               TriggerPrice: shortItem.StopLoss,
                                                               Variety: Constants.VARIETY_CO
                                                           );

                                    shortItem.isRealOrderPlaced = true;

                                    if (retOrdStatus != null && retOrdStatus["status"] == "success")
                                    {
                                        shortItem.OrderId = retOrdStatus["data"]["order_id"];
                                    }

                                    Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5}",
                                        "CO Placed", shortItem.TradingSymbol, Utilities.LongPosition,
                                        "Entry Price:" + shortItem.EntryPrice.ToString(),
                                        "Stop Loss Price:" + shortItem.StopLoss.ToString(),
                                        "Quantity:" + shortItem.Quantity.ToString()));
                                }
                                else
                                {
                                    shortItem.OrderId = null;
                                    shortItem.isRealOrderPlaced = null;
                                }

                                dbMethods.InsertElderStrategyOrder(shortItem);

                            }
                        }

                    }
                }
                catch (Exception ex)
                {
                    Logger.LogToFile(string.Format("{0},{1}", "Exception", shortItem.TradingSymbol));
                    Logger.ErrorLogToFile(ex);
                }
            }
        }

        private void CalculateTargetSLTSL(string positionType, decimal entryValue, decimal exitValue, decimal stopLossValue,
            out decimal boTarget, out decimal boStopLoss, out decimal boTrailingStopLoss)
        {
            boTarget = 0.0M;
            boStopLoss = 0.0M;
            boTrailingStopLoss = 0.0M;

            if (Utilities.LongPosition == positionType)
            {
                boTarget = Math.Round(exitValue - entryValue, 1);
                boStopLoss = Math.Round(entryValue - stopLossValue, 1);
                boTrailingStopLoss = Int32.Parse(Math.Round((exitValue - entryValue) / 3, 0).ToString());
            }

            if (Utilities.ShortPosition == positionType)
            {
                boTarget = Math.Round((entryValue - exitValue), 1);
                boStopLoss = Math.Round((stopLossValue - entryValue), 1);
                boTrailingStopLoss = Int32.Parse(Math.Round((entryValue - exitValue) / 3, 0).ToString());
            }
        }

        public void MonitorPositions(DateTime fromDateTime, DateTime toDateTime)
        {
            try
            {
                //Run the thread in 1 min interval
                //Get the Open Elder Strategy Orders for today  
                //Get the LTP for all the stocks
                //If LONG
                //if the LTP > Entry * 1.003 and SL < Entry, then SL = Entry
                //else if LTP > SL * 1.006, then SL = SL * 1.003
                //else if LTP < SL, then Exit = LTP and ExitTime and SLHitTime = Current Time
                //        ProfitLoss = (Exit - Entry) * Qty
                //IF Short
                //if the LTP < Entry / 1.003 and SL > Entry, then SL = Entry
                //else if LTP < SL / 1.006, then SL = SL / 1.003
                //else if LTP > SL, then Exit = LTP and ExitTime and SLHitTime = Current Time
                //        ProfitLoss = (Entry - Exit) * Qty

                decimal entryPercentage = UserConfiguration.InitialExitPercentage;
                decimal multiplyPercentage = UserConfiguration.SubsequentExitPercentage;

                var allElderOrders = dbMethods.GetElderStrategyOrders(tradeDate);

                List<ElderStrategyOrder> elderOrders = allElderOrders.Where(con => con.PositionStatus == Utilities.OS_TriggerPending).ToList();

                foreach (ElderStrategyOrder elderOrder in elderOrders)
                {
                    List<TickerElderIndicatorsVM> elderIndicatorsForOrder =
                        GetTickerTimePeriodForOrders(elderOrder.TradingSymbol, UserConfiguration.TimePeriodLong.ToString(), fromDateTime, toDateTime);

                    decimal ltp = dbMethods.GetLTP(elderOrder.TradingSymbol);

                    if (elderOrder.PositionType == Utilities.LongPosition && elderOrder.PositionStatus == Utilities.OS_TriggerPending
                        && elderOrder.EntryPrice != 0 && elderOrder.Quantity != 0)
                    {
                        decimal entryPointInitialPercentIncrease = Math.Round(((decimal)elderOrder.EntryPrice * entryPercentage), 1);

                        elderOrder.ProfitLoss = (ltp - elderOrder.EntryPrice) * elderOrder.Quantity;

                        if (ltp >= entryPointInitialPercentIncrease)
                        {
                            if (elderOrder.ExitPrice == null || elderOrder.ExitPrice == 0.0M)
                            {
                                elderOrder.ExitPrice = elderOrder.EntryPrice;
                                //elderOrder.PositionStatus = "BREAKEVEN";

                                //update the cover order here - SL = EntryPrice
                                if (placeOrders)
                                {
                                    Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                                                    "entryPointInitialPercentIncrease",
                                                    elderOrder.TradingSymbol,
                                                    elderOrder.PositionType,
                                                    "EntryPrice:" + elderOrder.EntryPrice.ToString(),
                                                    "ExitPrice:" + elderOrder.ExitPrice.ToString()));

                                    kite.ModifyOrder(OrderId: elderOrder.SLOrderId,
                                                    ParentOrderId: elderOrder.OrderId,
                                                    TriggerPrice: elderOrder.ExitPrice);
                                }

                            }
                            else
                            {
                                decimal entryPointPercentIncrease = Math.Round(((decimal)elderOrder.ExitPrice * multiplyPercentage), 1);

                                if (ltp > entryPointPercentIncrease)
                                {
                                    elderOrder.ExitPrice = Math.Round(((decimal)elderOrder.ExitPrice * entryPercentage), 1);
                                    //elderOrder.PositionStatus = "PROFIT";

                                    //update the cover order here
                                    if (placeOrders)
                                    {
                                        Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                                                        "entryPointPercentIncrease",
                                                        elderOrder.TradingSymbol,
                                                        elderOrder.PositionType,
                                                        "EntryPrice:" + elderOrder.EntryPrice.ToString(),
                                                        "ExitPrice:" + elderOrder.ExitPrice.ToString()));

                                        kite.ModifyOrder(OrderId: elderOrder.SLOrderId,
                                                        ParentOrderId: elderOrder.OrderId,
                                                        TriggerPrice: elderOrder.ExitPrice);
                                    }
                                }
                                else if (ltp <= elderOrder.ExitPrice)
                                {
                                    elderOrder.ExitPrice = ltp;
                                    elderOrder.ExitTime = DateTime.Now;
                                    elderOrder.ProfitLoss = (elderOrder.ExitPrice - elderOrder.EntryPrice) * elderOrder.Quantity;
                                }
                            }
                        }
                        else if ((ltp < elderOrder.StopLoss)) //|| CurrentImpulseColor(elderIndicatorsForOrder) == Utilities.ImpulseColor.RED)
                        {
                            elderOrder.ExitPrice = ltp;
                            elderOrder.ExitTime = elderOrder.StopLossHitTime = DateTime.Now;
                            elderOrder.ProfitLoss = (elderOrder.ExitPrice - elderOrder.EntryPrice) * elderOrder.Quantity;

                            if (elderOrder.PositionStatus == Utilities.OS_TriggerPending)
                            {
                                Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                                                "ExitCoverOrder",
                                                elderOrder.TradingSymbol,
                                                elderOrder.PositionType,
                                                "EntryPrice:" + elderOrder.EntryPrice.ToString(),
                                                "ExitPrice:" + elderOrder.ExitPrice.ToString()));

                                // kite.CancelOrder(
                                //    OrderId: elderOrder.SLOrderId,
                                //    Variety: Constants.VARIETY_CO,
                                //    ParentOrderId: elderOrder.OrderId
                                //);
                            }
                        }
                    }
                    else if (elderOrder.PositionType == Utilities.ShortPosition && elderOrder.PositionStatus == Utilities.OS_TriggerPending
                        && elderOrder.EntryPrice != 0 && elderOrder.Quantity != 0)
                    {
                        decimal entryPointInitialPercentDecrease = Math.Round(((decimal)elderOrder.EntryPrice / entryPercentage), 1);

                        elderOrder.ProfitLoss = (elderOrder.EntryPrice - ltp) * elderOrder.Quantity;

                        if (ltp < entryPointInitialPercentDecrease)
                        {
                            if (elderOrder.ExitPrice == null || elderOrder.ExitPrice == 0.0M)
                            {
                                elderOrder.ExitPrice = elderOrder.EntryPrice;
                                //elderOrder.PositionStatus = "BREAKEVEN";

                                if (placeOrders && elderOrder.PositionStatus == Utilities.OS_TriggerPending)
                                {
                                    Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                                                     "entryPointInitialPercentDecrease",
                                                     elderOrder.TradingSymbol,
                                                     elderOrder.PositionType,
                                                     "EntryPrice:" + elderOrder.EntryPrice.ToString(),
                                                     "ExitPrice:" + elderOrder.ExitPrice.ToString()));

                                    kite.ModifyOrder(OrderId: elderOrder.SLOrderId,
                                                    ParentOrderId: elderOrder.OrderId,
                                                    TriggerPrice: elderOrder.ExitPrice);
                                }
                            }
                            else
                            {
                                decimal entryPointPercentDecrease = Math.Round(((decimal)elderOrder.ExitPrice / multiplyPercentage), 1);

                                if (ltp < entryPointPercentDecrease)
                                {
                                    elderOrder.ExitPrice = Math.Round(((decimal)elderOrder.ExitPrice / entryPercentage), 1);
                                    //elderOrder.PositionStatus = "PROFIT";

                                    if (placeOrders && elderOrder.PositionStatus == Utilities.OS_TriggerPending)
                                    {

                                        Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                                                         "entryPointPercentDecrease",
                                                         elderOrder.TradingSymbol,
                                                         elderOrder.PositionType,
                                                         "EntryPrice:" + elderOrder.EntryPrice.ToString(),
                                                         "ExitPrice:" + elderOrder.ExitPrice.ToString()));

                                        kite.ModifyOrder(OrderId: elderOrder.SLOrderId,
                                                        ParentOrderId: elderOrder.OrderId,
                                                        TriggerPrice: elderOrder.ExitPrice);
                                    }
                                }
                                else if (ltp >= elderOrder.ExitPrice)
                                {
                                    elderOrder.ExitPrice = ltp;
                                    elderOrder.ExitTime = DateTime.Now;
                                    elderOrder.ProfitLoss = (elderOrder.EntryPrice - elderOrder.ExitPrice) * elderOrder.Quantity;
                                }
                            }
                        }
                        else if ((ltp > elderOrder.StopLoss))// || CurrentImpulseColor(elderIndicatorsForOrder) == Utilities.ImpulseColor.GREEN)
                        {
                            elderOrder.ExitPrice = ltp;
                            elderOrder.ExitTime = elderOrder.StopLossHitTime = DateTime.Now;
                            elderOrder.ProfitLoss = (elderOrder.EntryPrice - elderOrder.ExitPrice) * elderOrder.Quantity;

                            if (elderOrder.PositionStatus == Utilities.OS_TriggerPending)
                            {
                                Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                                                "ExitCoverOrder",
                                                elderOrder.TradingSymbol,
                                                elderOrder.PositionType,
                                                "EntryPrice:" + elderOrder.EntryPrice.ToString(),
                                                "ExitPrice:" + elderOrder.ExitPrice.ToString()));

                                //kite.CancelOrder(
                                //   OrderId: elderOrder.SLOrderId,
                                //   Variety: Constants.VARIETY_CO,
                                //   ParentOrderId: elderOrder.OrderId
                                //);
                            }

                            //elderOrder.PositionStatus = "LOSS";
                        }
                    }
                }

                lock (this)
                {
                    if (elderOrders.Count > 0)
                    {
                        dbMethods.UpdateElderStrategyOrders(elderOrders);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.ErrorLogToFile(ex);
            }
        }

        public void GetPositions()
        {
            while (!mStopThread)
            {
                Thread.Sleep(UserConfiguration.DelayInSec * 1000);

                try
                {
                    if (placeOrders)
                    {
                        List<KiteConnect.Order> orders = kite.GetOrders();

                        List<Repository.Order> localOrders = new List<Repository.Order>();

                        foreach (KiteConnect.Order order in orders)
                        {
                            //this condition is to avoid entering REJECTED orders
                            if (order.Status == Utilities.OS_Complete || order.Status == Utilities.OS_Open ||
                                order.Status == Utilities.OS_TriggerPending || order.Status == Utilities.OS_Cancelled)
                            {
                                Repository.Order localOrder = new Repository.Order();

                                localOrder.OrderId = order.OrderId;
                                localOrder.ExchangeOrderId = order.ExchangeOrderId;
                                localOrder.ParentOrderId = order.ParentOrderId;
                                localOrder.InstrumentToken = Convert.ToInt32(order.InstrumentToken);
                                localOrder.Exchange = order.Exchange;
                                localOrder.OrderTimestamp = order.OrderTimestamp;
                                localOrder.ExchangeTimestamp = order.ExchangeTimestamp;
                                localOrder.OrderType = order.OrderType;
                                localOrder.Tradingsymbol = order.Tradingsymbol;
                                localOrder.TransactionType = order.TransactionType;
                                localOrder.Quantity = order.Quantity;
                                localOrder.CancelledQuantity = order.CancelledQuantity;
                                localOrder.DisclosedQuantity = order.DisclosedQuantity;
                                localOrder.FilledQuantity = order.FilledQuantity;
                                localOrder.PendingQuantity = order.PendingQuantity;
                                localOrder.Price = order.Price;
                                localOrder.TriggerPrice = order.TriggerPrice;
                                localOrder.AveragePrice = order.AveragePrice;
                                localOrder.Product = order.Product;
                                localOrder.PlacedBy = order.PlacedBy;
                                localOrder.Validity = order.Validity;
                                localOrder.Variety = order.Variety;
                                localOrder.Tag = order.Tag;
                                localOrder.Status = order.Status;
                                localOrder.StatusMessage = order.StatusMessage;

                                localOrders.Add(localOrder);
                            }
                        }

                        if (localOrders.Count > 0)
                        {
                            DataTable dtOrder = localOrders.ToDataTable();

                            dbMethods.RefreshOrders(dtOrder);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Logger.ErrorLogToFile(ex);
                }
            }
        }

        private Utilities.PriceRange PriceRangeOfTheStock(List<TickerElderIndicatorsVM> selStock)
        {
            Utilities.PriceRange retValue = Utilities.PriceRange.NA;

            double currentPrice = (double)selStock[currentIndex].PriceClose;

            if (currentPrice < 75)
            {
                retValue = Utilities.PriceRange.VERYLOW;
            }
            else if (currentPrice >= 75 && currentPrice < 250)
            {
                retValue = Utilities.PriceRange.LOW;
            }
            else if (currentPrice >= 250 && currentPrice < 750)
            {
                retValue = Utilities.PriceRange.MEDIUM;
            }
            else if (currentPrice >= 750 && currentPrice < 1500)
            {
                retValue = Utilities.PriceRange.HIGH;
            }
            else if (currentPrice >= 1500)
            {
                retValue = Utilities.PriceRange.VERYHIGH;
            }

            return retValue;
        }

        private Utilities.Trend TrendOfTheStock(List<TickerElderIndicatorsVM> selStock)
        {
            Utilities.Trend retValue = Utilities.Trend.NA;

            OHLCData ohlc = dbMethods.GetOHLC(selStock[currentIndex].TradingSymbol);

            decimal percentageChange = CalculateChangePercentage((decimal)ohlc.PreviousClose, (decimal)ohlc.LastPrice);

            if (percentageChange > -0.3M && percentageChange < 0.3M)
            {
                retValue = Utilities.Trend.FLAT;
            }
            else if (percentageChange > 0.3M)
            {
                retValue = Utilities.Trend.UP;
            }
            else if (percentageChange < -0.3M)
            {
                retValue = Utilities.Trend.DOWN;
            }

            return retValue;
        }

        private decimal CalculateChangePercentage(decimal previousClose, decimal ltp)
        {
            return Math.Round(((ltp - previousClose) / previousClose) * 100, 2);
        }

        /****************************************************/

        private bool IsCurrentClosePriceGreaterThanCurrentOpenPrice(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].PriceClose > selStock[currentIndex].PriceOpen)
            {
                retValue = true;
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentClosePriceGreaterThanCurrentOpenPrice",
                    selStock[currentIndex].TradingSymbol,
                    "PriceClose:" + selStock[currentIndex].PriceClose.ToString(),
                    "PriceOpen:" + selStock[currentIndex].PriceOpen.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentClosePriceGreaterThanShortEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA1 != null)
            {
                if (selStock[currentIndex].PriceClose > selStock[currentIndex].EMA1)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentClosePriceGreaterThanShortEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceClose:" + selStock[currentIndex].PriceClose.ToString(),
                    "EMAShort:" + selStock[currentIndex].EMA1.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentOpenClosePriceGreaterThanMediumEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA2 != null)
            {
                if (selStock[currentIndex].PriceOpen > selStock[currentIndex].EMA2 && selStock[currentIndex].PriceClose > selStock[currentIndex].EMA2)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5}",
                    "IsCurrentOpenClosePriceGreaterThanMediumEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceOpen:" + selStock[currentIndex].PriceOpen.ToString(),
                    "PriceClose:" + selStock[currentIndex].PriceClose.ToString(),
                    "EMAMedium:" + selStock[currentIndex].EMA2.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentLowPriceGreaterThanLongEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA4 != null)
            {
                if (selStock[currentIndex].PriceLow > selStock[currentIndex].EMA4)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentLowPriceGreaterThanLongEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceLow:" + selStock[currentIndex].PriceLow.ToString(),
                    "EMALong:" + selStock[currentIndex].EMA4.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsPreviousOpenPriceGreaterThanLongEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[previousIndex].EMA4 != null)
            {
                if (selStock[previousIndex].PriceOpen > selStock[previousIndex].EMA4)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsPreviousOpenPriceGreaterThanLongEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceOpen:" + selStock[currentIndex].PriceOpen.ToString(),
                    "EMALong:" + selStock[currentIndex].EMA4.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsPreviousClosePriceGreaterThanShortEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[previousIndex].EMA1 != null)
            {
                if (selStock[previousIndex].PriceClose > selStock[previousIndex].EMA1)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsPreviousClosePriceGreaterThanShortEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceClose:" + selStock[previousIndex].PriceClose.ToString(),
                    "EMAShort:" + selStock[previousIndex].EMA1.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentShortEMADevGreaterThanPreviousShortEMADev(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA1Dev != null && selStock[previousIndex].EMA1Dev != null)
            {
                if (selStock[currentIndex].EMA1Dev > selStock[previousIndex].EMA1Dev)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentShortEMADevGreaterThanPreviousShortEMADev",
                    selStock[currentIndex].TradingSymbol,
                    "PreviousEMADev:" + selStock[previousIndex].EMA1Dev.ToString(),
                    "CurrentEMADev:" + selStock[currentIndex].EMA1Dev.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        //private bool IsPreviousShortEMADevGreaterThanPreviousToPreviousShortEMADev(List<TickerElderIndicatorsVM> selStock)
        //{
        //    bool retValue = false;

        //    if (selStock[previousIndex].EMA1Dev != null && selStock[0].EMA1Dev != null)
        //    {
        //        if (selStock[previousIndex].EMA1Dev > selStock[0].EMA1Dev)
        //        {
        //            retValue = true;
        //        }
        //    }

        //    Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
        //            "IsPreviousShortEMADevGreaterThanPreviousToPreviousShortEMADev",
        //            selStock[currentIndex].TradingSymbol,
        //            "PreviousToPreviousEMADev:" + selStock[0].EMA1Dev.ToString(),
        //            "PreviousEMADev:" + selStock[previousIndex].EMA1Dev.ToString(),
        //            retValue.ToString()
        //            ));

        //    return retValue;
        //}

        private bool IsPreviousShortEMAGreaterThanPreviousLongEMAByPercentage(List<TickerElderIndicatorsVM> selStock, double percentage)
        {
            bool retValue = false;

            if (selStock[previousIndex].EMA1 != null && selStock[previousIndex].EMA4 != null)
            {
                if (selStock[previousIndex].EMA1 > (selStock[previousIndex].EMA4 * percentage))
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsPreviousShortEMAGreaterThanPreviousLongEMAByPercentage",
                    selStock[currentIndex].TradingSymbol,
                    "PreviousShortEMA:" + selStock[previousIndex].EMA1.ToString(),
                    "PreviousLongEMA Per Dev:" + (selStock[previousIndex].EMA4 * percentage).ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentShortEMAGreaterThanCurrentLongEMAByPercentage(List<TickerElderIndicatorsVM> selStock, double percentage)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA1 != null && selStock[currentIndex].EMA4 != null)
            {
                if (selStock[currentIndex].EMA1 > (selStock[currentIndex].EMA4 * percentage))
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentShortEMAGreaterThanCurrentLongEMAByPercentage",
                    selStock[currentIndex].TradingSymbol,
                    "CurrentShortEMA:" + selStock[currentIndex].EMA1.ToString(),
                    "CurrentLongEMA Per Dev:" + (selStock[currentIndex].EMA4 * percentage).ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }


        private bool IsCurrentShortEMAGreaterThanCurrentMediumEMAByPercentage(List<TickerElderIndicatorsVM> selStock, double percentage)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA1 != null && selStock[currentIndex].EMA2 != null)
            {
                if (selStock[currentIndex].EMA1 > (selStock[currentIndex].EMA2 * percentage))
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentShortEMAGreaterThanCurrentMediumEMAByPercentage",
                    selStock[currentIndex].TradingSymbol,
                    "CurrentShortEMA:" + selStock[currentIndex].EMA1.ToString(),
                    "CurrentMediumEMA Per Dev:" + (selStock[currentIndex].EMA2 * percentage).ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }


        /****************************************************/

        private bool IsCurrentClosePriceLesserThanCurrentOpenPrice(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].PriceClose < selStock[currentIndex].PriceOpen)
            {
                retValue = true;
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentClosePriceLesserThanCurrentOpenPrice",
                    selStock[currentIndex].TradingSymbol,
                    "PriceClose:" + selStock[currentIndex].PriceClose.ToString(),
                    "PriceOpen:" + selStock[currentIndex].PriceOpen.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentClosePriceLesserThanShortEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA1 != null)
            {
                if (selStock[currentIndex].PriceClose < selStock[currentIndex].EMA1)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentClosePriceLesserThanShortEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceClose:" + selStock[currentIndex].PriceClose.ToString(),
                    "EMAShort:" + selStock[currentIndex].EMA1.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentOpenClosePriceLesserThanMediumEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA2 != null)
            {
                if (selStock[currentIndex].PriceOpen < selStock[currentIndex].EMA2 && selStock[currentIndex].PriceClose < selStock[currentIndex].EMA2)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5}",
                    "IsCurrentOpenClosePriceLesserThanMediumEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceOpen:" + selStock[currentIndex].PriceOpen.ToString(),
                    "PriceClose:" + selStock[currentIndex].PriceClose.ToString(),
                    "EMAMedium:" + selStock[currentIndex].EMA2.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentLowPriceLesserThanLongEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA4 != null)
            {
                if (selStock[currentIndex].PriceLow < selStock[currentIndex].EMA4)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentLowPriceLesserThanLongEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceLow:" + selStock[currentIndex].PriceLow.ToString(),
                    "EMALong:" + selStock[currentIndex].EMA4.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsPreviousOpenPriceLesserThanLongEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[previousIndex].EMA4 != null)
            {
                if (selStock[previousIndex].PriceOpen < selStock[previousIndex].EMA4)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsPreviousOpenPriceLesserThanLongEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceOpen:" + selStock[previousIndex].PriceOpen.ToString(),
                    "EMALong:" + selStock[previousIndex].EMA4.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsPreviousClosePriceLesserThanShortEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[previousIndex].EMA1 != null)
            {
                if (selStock[previousIndex].PriceClose < selStock[previousIndex].EMA1)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsPreviousClosePriceLesserThanShortEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceOpen:" + selStock[previousIndex].PriceClose.ToString(),
                    "EMALong:" + selStock[previousIndex].EMA1.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentShortEMADevLesserThanPreviousShortEMADev(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA1Dev != null && selStock[previousIndex].EMA1Dev != null)
            {
                if (selStock[currentIndex].EMA1Dev < selStock[previousIndex].EMA1Dev)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentShortEMADevLesserThanPreviousShortEMADev",
                    selStock[currentIndex].TradingSymbol,
                    "PreviousEMADev:" + selStock[previousIndex].EMA1Dev.ToString(),
                    "CurrentEMADev:" + selStock[currentIndex].EMA1Dev.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        //private bool IsPreviousShortEMADevLesserThanPreviousToPreviousShortEMADev(List<TickerElderIndicatorsVM> selStock)
        //{
        //    bool retValue = false;

        //    if (selStock[previousIndex].EMA1Dev != null && selStock[0].EMA1Dev != null)
        //    {
        //        if (selStock[previousIndex].EMA1Dev < selStock[0].EMA1Dev)
        //        {
        //            retValue = true;
        //        }
        //    }

        //    Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
        //            "IsPreviousShortEMADevLesserThanPreviousToPreviousShortEMADev",
        //            selStock[currentIndex].TradingSymbol,
        //            "PreviousToPreviousEMADev:" + selStock[0].EMA1Dev.ToString(),
        //            "PreviousEMADev:" + selStock[previousIndex].EMA1Dev.ToString(),
        //            retValue.ToString()
        //            ));

        //    return retValue;
        //}

        private bool IsPreviousShortEMALesserThanPreviousLongEMAByPercentage(List<TickerElderIndicatorsVM> selStock, double percentage)
        {
            bool retValue = false;

            if (selStock[previousIndex].EMA1 != null && selStock[previousIndex].EMA4 != null)
            {
                if (selStock[previousIndex].EMA1 < (selStock[previousIndex].EMA4 / percentage))
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsPreviousShortEMALesserThanPreviousLongEMAByPercentage",
                    selStock[currentIndex].TradingSymbol,
                    "PreviousShortEMA:" + selStock[previousIndex].EMA1.ToString(),
                    "PreviousLongEMA Per Dev:" + (selStock[previousIndex].EMA4 / percentage).ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentShortEMALesserThanCurrentLongEMAByPercentage(List<TickerElderIndicatorsVM> selStock, double percentage)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA1 != null && selStock[currentIndex].EMA4 != null)
            {
                if (selStock[currentIndex].EMA1 < (selStock[previousIndex].EMA4 / percentage))
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentShortEMALesserThanCurrentLongEMAByPercentage",
                    selStock[currentIndex].TradingSymbol,
                    "CurrentShortEMA:" + selStock[currentIndex].EMA1.ToString(),
                    "CurrentLongEMA Per Dev:" + (selStock[previousIndex].EMA4 / percentage).ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentShortEMALesserThanCurrentMediumEMAByPercentage(List<TickerElderIndicatorsVM> selStock, double percentage)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA1 != null && selStock[currentIndex].EMA2 != null)
            {
                if (selStock[currentIndex].EMA1 < (selStock[previousIndex].EMA2 / percentage))
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentShortEMALesserThanCurrentMediumEMAByPercentage",
                    selStock[currentIndex].TradingSymbol,
                    "CurrentShortEMA:" + selStock[currentIndex].EMA1.ToString(),
                    "CurrentMediumEMA Per Dev:" + (selStock[previousIndex].EMA2 / percentage).ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        /****************************************************/

        private bool IsCurrentOpenClosePriceGreaterThanLongEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA4 != null)
            {
                if (selStock[currentIndex].PriceOpen > selStock[currentIndex].EMA4 && selStock[currentIndex].PriceClose > selStock[currentIndex].EMA4)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5}",
                    "IsCurrentOpenClosePriceGreaterThanLongEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceOpen:" + selStock[currentIndex].PriceOpen.ToString(),
                    "PriceClose:" + selStock[currentIndex].PriceClose.ToString(),
                    "EMALong:" + selStock[currentIndex].EMA4.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentOpenClosePriceLesserThanLongEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA4 != null)
            {
                if (selStock[currentIndex].PriceOpen < selStock[currentIndex].EMA4 && selStock[currentIndex].PriceClose < selStock[currentIndex].EMA4)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5}",
                    "IsCurrentOpenClosePriceLesserThanLongEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceOpen:" + selStock[currentIndex].PriceOpen.ToString(),
                    "PriceClose:" + selStock[currentIndex].PriceClose.ToString(),
                    "EMALong:" + selStock[currentIndex].EMA4.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsPreviousOpenClosePriceGreaterThanLongEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[previousIndex].EMA4 != null)
            {
                if (selStock[previousIndex].PriceOpen > selStock[previousIndex].EMA4 && selStock[previousIndex].PriceClose > selStock[previousIndex].EMA4)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5}",
                    "IsPreviousOpenClosePriceGreaterThanLongEMA",
                    selStock[previousIndex].TradingSymbol,
                    "PriceOpen:" + selStock[previousIndex].PriceOpen.ToString(),
                    "PriceClose:" + selStock[previousIndex].PriceClose.ToString(),
                    "EMALong:" + selStock[previousIndex].EMA4.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsPreviousClosePriceGreaterThanLongEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[previousIndex].EMA4 != null)
            {
                if (selStock[previousIndex].PriceClose > selStock[previousIndex].EMA4)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsPreviousClosePriceGreaterThanLongEMA",
                    selStock[previousIndex].TradingSymbol,
                    "PriceClose:" + selStock[previousIndex].PriceClose.ToString(),
                    "EMALong:" + selStock[previousIndex].EMA4.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsPreviousClosePriceLesserThanLongEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[previousIndex].EMA4 != null)
            {
                if (selStock[previousIndex].PriceClose < selStock[previousIndex].EMA4)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsPreviousClosePriceLesserThanLongEMA",
                    selStock[previousIndex].TradingSymbol,
                    "PriceClose:" + selStock[previousIndex].PriceClose.ToString(),
                    "EMALong:" + selStock[previousIndex].EMA4.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsPreviousOpenClosePriceLesserThanLongEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[previousIndex].EMA4 != null)
            {
                if (selStock[previousIndex].PriceOpen < selStock[previousIndex].EMA4 && selStock[previousIndex].PriceClose < selStock[previousIndex].EMA4)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5}",
                    "IsPreviousOpenClosePriceLesserThanLongEMA",
                    selStock[previousIndex].TradingSymbol,
                    "PriceOpen:" + selStock[previousIndex].PriceOpen.ToString(),
                    "PriceClose:" + selStock[previousIndex].PriceClose.ToString(),
                    "EMALong:" + selStock[previousIndex].EMA4.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentOpenClosePriceGreaterThanShortEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA1 != null)
            {
                if (selStock[currentIndex].PriceOpen > selStock[currentIndex].EMA1 && selStock[currentIndex].PriceClose > selStock[currentIndex].EMA1)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5}",
                    "IsCurrentOpenClosePriceGreaterThanShortEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceOpen:" + selStock[currentIndex].PriceOpen.ToString(),
                    "PriceClose:" + selStock[currentIndex].PriceClose.ToString(),
                    "EMAShort:" + selStock[currentIndex].EMA1.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentOpenClosePriceLesserThanShortEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[currentIndex].EMA1 != null)
            {
                if (selStock[currentIndex].PriceOpen < selStock[currentIndex].EMA1 && selStock[currentIndex].PriceClose < selStock[currentIndex].EMA1)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5}",
                    "IsCurrentOpenClosePriceLesserThanShortEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceOpen:" + selStock[currentIndex].PriceOpen.ToString(),
                    "PriceClose:" + selStock[currentIndex].PriceClose.ToString(),
                    "EMAShort:" + selStock[currentIndex].EMA1.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsPreviousOpenClosePriceGreaterThanShortEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[previousIndex].EMA1 != null)
            {
                if (selStock[previousIndex].PriceOpen > selStock[previousIndex].EMA1 && selStock[previousIndex].PriceClose > selStock[previousIndex].EMA1)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5}",
                    "IsPreviousOpenClosePriceGreaterThanShortEMA",
                    selStock[currentIndex].TradingSymbol,
                    "PriceOpen:" + selStock[previousIndex].PriceOpen.ToString(),
                    "PriceClose:" + selStock[previousIndex].PriceClose.ToString(),
                    "EMAShort:" + selStock[previousIndex].EMA1.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsPreviousOpenClosePriceLesserThanShortEMA(List<TickerElderIndicatorsVM> selStock)
        {
            bool retValue = false;

            if (selStock[previousIndex].EMA1 != null)
            {
                if (selStock[previousIndex].PriceOpen < selStock[previousIndex].EMA1 && selStock[previousIndex].PriceClose < selStock[previousIndex].EMA1)
                {
                    retValue = true;
                }
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5}",
                    "IsPreviousOpenClosePriceLesserThanShortEMA",
                    selStock[previousIndex].TradingSymbol,
                    "PriceOpen:" + selStock[previousIndex].PriceOpen.ToString(),
                    "PriceClose:" + selStock[previousIndex].PriceClose.ToString(),
                    "EMAShort:" + selStock[previousIndex].EMA1.ToString(),
                    retValue.ToString()
                    ));

            return retValue;
        }

        private bool IsCurrentForceIndexPositive(List<TickerElderIndicatorsVM> selStock)
        {
            bool isCurrentForceIndexPositive = false;

            if (selStock[currentIndex].ForceIndex1 != null && selStock[currentIndex].ForceIndex1 > 0)
            {
                isCurrentForceIndexPositive = true;
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3}",
                    "IsCurrentForceIndexPositive",
                    selStock[currentIndex].TradingSymbol,
                    "ForceIndex:" + selStock[currentIndex].ForceIndex1.ToString(),
                    isCurrentForceIndexPositive.ToString()
                    ));

            return isCurrentForceIndexPositive;
        }

        private bool IsCurrentForceIndexNegative(List<TickerElderIndicatorsVM> selStock)
        {
            bool isCurrentForceIndexNegative = false;

            if (selStock[currentIndex].ForceIndex1 != null && selStock[currentIndex].ForceIndex1 < 0)
            {
                isCurrentForceIndexNegative = true;
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3}",
                    "IsCurrentForceIndexNegative",
                    selStock[currentIndex].TradingSymbol,
                    "ForceIndex:" + selStock[currentIndex].ForceIndex1.ToString(),
                    isCurrentForceIndexNegative.ToString()
                    ));

            return isCurrentForceIndexNegative;
        }

        //private bool IsShortEMAGreaterThanLongEMA(List<TickerElderIndicatorsVM> selStock)
        //{
        //    bool isShortEMAGreaterThanLongEMA = false;

        //    if (selStock[0].EMA1 > selStock[0].EMA4 && selStock[previousIndex].EMA1 > selStock[previousIndex].EMA4 && selStock[currentIndex].EMA1 > selStock[currentIndex].EMA4)
        //    {
        //        isShortEMAGreaterThanLongEMA = true;
        //    }

        //    return isShortEMAGreaterThanLongEMA;
        //}

        //private bool IsShortEMADeviationPositive(List<TickerElderIndicatorsVM> selStock)
        //{
        //    bool isShortEMADeviationPositive = false;

        //    if (selStock[0].EMA1Dev > 0 && selStock[previousIndex].EMA1Dev > 0 && selStock[currentIndex].EMA1Dev > 0)
        //    {
        //        isShortEMADeviationPositive = true;
        //    }

        //    return isShortEMADeviationPositive;
        //}

        private bool IsCurrentAndPreviousShortEMADevGreaterThanPercentageRange(List<TickerElderIndicatorsVM> selStock,
            double percentageDeviationBottom, double percentageDeviationTop)
        {
            bool retVal = false;

            if (selStock[currentIndex].EMA1Dev > percentageDeviationTop && selStock[previousIndex].EMA1Dev > percentageDeviationBottom)
            {
                retVal = true;
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentAndPreviousShortEMADevGreaterThanPercentageRange",
                    selStock[currentIndex].TradingSymbol,
                    "CurrentShortEMADev:" + selStock[currentIndex].EMA1Dev,
                    "PreviousShortEMADev:" + selStock[previousIndex].EMA1Dev,
                    retVal.ToString()
                    ));

            return retVal;
        }

        private bool IsCurrentAndPreviousShortEMADevLesserThanPercentageRange(List<TickerElderIndicatorsVM> selStock,
                    double percentageDeviationBottom, double percentageDeviationTop)
        {
            bool retVal = false;

            if (selStock[currentIndex].EMA1Dev < -(percentageDeviationTop) && selStock[previousIndex].EMA1Dev < -(percentageDeviationBottom))
            {
                retVal = true;
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3},{4}",
                    "IsCurrentAndPreviousShortEMADevLesserThanPercentageRange",
                    selStock[currentIndex].TradingSymbol,
                    "CurrentShortEMADev:" + selStock[currentIndex].EMA1Dev,
                    "PreviousShortEMADev:" + selStock[previousIndex].EMA1Dev,
                    retVal.ToString()
                    ));

            return retVal;
        }

        //private bool IsShortEMADeviationNegative(List<TickerElderIndicatorsVM> selStock)
        //{
        //    bool isShortEMADeviationNegative = false;

        //    if (selStock[0].EMA1Dev < 0 && selStock[previousIndex].EMA1Dev < 0 && selStock[currentIndex].EMA1Dev < 0)
        //    {
        //        isShortEMADeviationNegative = true;
        //    }

        //    return isShortEMADeviationNegative;
        //}

        //private bool IsLongEMADeviationPositive(List<TickerElderIndicatorsVM> selStock)
        //{
        //    bool isLongEMADeviationPositive = false;

        //    if (selStock[0].EMA2Dev > 0 && selStock[previousIndex].EMA2Dev > 0 && selStock[currentIndex].EMA2Dev > 0)
        //    {
        //        isLongEMADeviationPositive = true;
        //    }

        //    return isLongEMADeviationPositive;
        //}

        //private bool IsLongEMADeviationNegative(List<TickerElderIndicatorsVM> selStock)
        //{
        //    bool isLongEMADeviationNegative = false;

        //    if (selStock[0].EMA2Dev < 0 && selStock[previousIndex].EMA2Dev < 0 && selStock[currentIndex].EMA2Dev < 0)
        //    {
        //        isLongEMADeviationNegative = true;
        //    }

        //    return isLongEMADeviationNegative;
        //}

        /****************************************************/

        private Utilities.ImpulseColor CurrentImpulseColor(List<TickerElderIndicatorsVM> selStock)
        {
            Utilities.ImpulseColor impulse = Utilities.ImpulseColor.NA;

            if (selStock[currentIndex].Impulse != null)
            {
                if (selStock[currentIndex].Impulse.Trim() == "B")
                {
                    impulse = Utilities.ImpulseColor.BLUE;
                }
                else if (selStock[currentIndex].Impulse.Trim() == "G")
                {
                    impulse = Utilities.ImpulseColor.GREEN;
                }
                else if (selStock[currentIndex].Impulse.Trim() == "R")
                {
                    impulse = Utilities.ImpulseColor.RED;
                }

                Logger.LogToFile(string.Format("{0},{1},{2}",
                        "CurrentImpulseColor",
                        selStock[currentIndex].TradingSymbol,
                        "Impulse Color:" + impulse.ToString()
                        ));

            }

            return impulse;
        }

        private Utilities.ImpulseColor PreviousImpulseColor(List<TickerElderIndicatorsVM> selStock)
        {
            Utilities.ImpulseColor impulse = Utilities.ImpulseColor.NA;

            if (selStock[previousIndex].Impulse != null)
            {
                if (selStock[previousIndex].Impulse.Trim() == "B")
                {
                    impulse = Utilities.ImpulseColor.BLUE;
                }
                else if (selStock[previousIndex].Impulse.Trim() == "G")
                {
                    impulse = Utilities.ImpulseColor.GREEN;
                }
                else if (selStock[previousIndex].Impulse.Trim() == "R")
                {
                    impulse = Utilities.ImpulseColor.RED;
                }

                Logger.LogToFile(string.Format("{0},{1},{2}",
                        "PreviousImpulseColor",
                        selStock[previousIndex].TradingSymbol,
                        "Impulse Color:" + impulse.ToString()
                        ));

            }

            return impulse;
        }

        //private Utilities.ImpulseColor PreviousToPreviousImpulseColor(List<TickerElderIndicatorsVM> selStock)
        //{
        //    Utilities.ImpulseColor impulse = Utilities.ImpulseColor.NA;

        //    if (selStock[0].Impulse != null)
        //    {
        //        if (selStock[0].Impulse.Trim() == "B")
        //        {
        //            impulse = Utilities.ImpulseColor.BLUE;
        //        }
        //        else if (selStock[0].Impulse.Trim() == "G")
        //        {
        //            impulse = Utilities.ImpulseColor.GREEN;
        //        }
        //        else if (selStock[0].Impulse.Trim() == "R")
        //        {
        //            impulse = Utilities.ImpulseColor.RED;
        //        }

        //        Logger.LogToFile(string.Format("{0},{1},{2}",
        //                "PreviousToPreviousImpulseColor",
        //                selStock[0].TradingSymbol,
        //                "Impulse Color:" + impulse.ToString()
        //                ));
        //    }

        //    return impulse;
        //}

        private Utilities.HistogramMovement CurrentHistogramMovement(List<TickerElderIndicatorsVM> selStock)
        {
            Utilities.HistogramMovement histMovement = Utilities.HistogramMovement.NA;

            if (selStock[currentIndex].HistMovement != null)
            {
                if (selStock[currentIndex].HistMovement.Trim() == "I")
                {
                    histMovement = Utilities.HistogramMovement.INCREASING;
                }
                else if (selStock[currentIndex].HistMovement.Trim() == "D")
                {
                    histMovement = Utilities.HistogramMovement.DECREASING;
                }

                Logger.LogToFile(string.Format("{0},{1},{2}",
                        "CurrentHistogramMovement",
                        selStock[currentIndex].TradingSymbol,
                        "Hist Movement:" + histMovement.ToString()
                        ));
            }

            return histMovement;
        }

        private Utilities.HistogramMovement PreviousHistogramMovement(List<TickerElderIndicatorsVM> selStock)
        {
            Utilities.HistogramMovement histMovement = Utilities.HistogramMovement.NA;

            if (selStock[previousIndex].HistMovement != null)
            {
                if (selStock[previousIndex].HistMovement.Trim() == "I")
                {
                    histMovement = Utilities.HistogramMovement.INCREASING;
                }
                else if (selStock[previousIndex].HistMovement.Trim() == "D")
                {
                    histMovement = Utilities.HistogramMovement.DECREASING;
                }

                Logger.LogToFile(string.Format("{0},{1},{2}",
                        "CurrentHistogramMovement",
                        selStock[previousIndex].TradingSymbol,
                        "Prev Hist Movement:" + histMovement.ToString()
                        ));
            }

            return histMovement;
        }

        //private Utilities.HistogramMovement PreviousToPreviousHistogramMovement(List<TickerElderIndicatorsVM> selStock)
        //{
        //    Utilities.HistogramMovement histMovement = Utilities.HistogramMovement.NA;

        //    if (selStock[0].HistMovement != null)
        //    {
        //        if (selStock[0].HistMovement.Trim() == "I")
        //        {
        //            histMovement = Utilities.HistogramMovement.INCREASING;
        //        }
        //        else if (selStock[0].HistMovement.Trim() == "D")
        //        {
        //            histMovement = Utilities.HistogramMovement.DECREASING;
        //        }

        //        Logger.LogToFile(string.Format("{0},{1},{2}",
        //                "PreviousToPreviousHistogramMovement",
        //                selStock[0].TradingSymbol,
        //                "PrevToPrev Hist Movement:" + histMovement.ToString()
        //                ));
        //    }

        //    return histMovement;
        //}

        private bool IsCurrentHistogramValuePositive(List<TickerElderIndicatorsVM> selStock)
        {
            bool retVal = false;

            if (selStock[currentIndex].Histogram != null)
            {
                if (selStock[currentIndex].Histogram > 0)
                {
                    retVal = true;
                }
            }

            return retVal;
        }

        private bool IsCurrentHistogramValueNegative(List<TickerElderIndicatorsVM> selStock)
        {
            bool retVal = false;

            if (selStock[currentIndex].Histogram != null)
            {
                if (selStock[currentIndex].Histogram < 0)
                {
                    retVal = true;
                }
            }

            return retVal;
        }


        /****************************************************/

        /// <summary>
        /// 
        /// </summary>
        private void GetPTM()
        {

        }

        private void UpdateNetPositions()
        {
            PositionResponse pr = kite.GetPositions();

            List<Repository.NetPosition> netPositions = new List<NetPosition>();

            foreach (Position position in pr.Net)
            {
                NetPosition newPosition = new NetPosition();

                newPosition.PositionDate = DateTime.Today;
                newPosition.InstrumentToken = (int)position.InstrumentToken;
                newPosition.TradingSymbol = position.TradingSymbol;

                newPosition.AveragePrice = position.AveragePrice;
                newPosition.BuyM2M = position.BuyM2M;
                newPosition.BuyPrice = position.BuyPrice;
                newPosition.BuyQuantity = position.BuyQuantity;
                newPosition.BuyValue = position.BuyValue;
                newPosition.ClosePrice = position.ClosePrice;
                newPosition.Exchange = position.Exchange;
                newPosition.LastPrice = position.LastPrice;
                newPosition.M2M = position.M2M;
                newPosition.Multiplier = position.Multiplier;
                newPosition.OvernightQuantity = position.OvernightQuantity;
                newPosition.PNL = position.PNL;
                newPosition.Product = position.Product;
                newPosition.Quantity = position.Quantity;
                newPosition.Realised = position.Realised;
                newPosition.SellM2M = position.SellM2M;
                newPosition.SellPrice = position.SellPrice;
                newPosition.SellQuantity = position.SellQuantity;
                newPosition.SellValue = position.SellValue;
                newPosition.Unrealised = position.Unrealised;
                newPosition.Value = position.Value;

                netPositions.Add(newPosition);
            }

            if (netPositions.Count > 0)
            {
                DataTable dtPositions = netPositions.ToDataTable();

                dbMethods.RefreshPositions(dtPositions);
            }

        }

        private void UpdateTrades()
        {
            List<KiteConnect.Trade> trades = kite.GetOrderTrades();

            List<Repository.Trade> tradeList = new List<Repository.Trade>();

            foreach (KiteConnect.Trade trade in trades)
            {
                Repository.Trade tradeRep = new Repository.Trade();
                tradeRep.AveragePrice = trade.AveragePrice;
                tradeRep.Exchange = trade.Exchange;
                tradeRep.ExchangeOrderId = trade.ExchangeOrderId;
                tradeRep.ExchangeTimestamp = trade.ExchangeTimestamp;
                tradeRep.InstrumentToken = Convert.ToInt32(trade.InstrumentToken);
                tradeRep.OrderId = trade.OrderId;
                tradeRep.OrderTimestamp = trade.FillTimestamp;
                tradeRep.Product = trade.Product;
                tradeRep.Quantity = trade.Quantity;
                tradeRep.TradeId = trade.TradeId;
                tradeRep.TradingSymbol = trade.Tradingsymbol;
                tradeRep.TransactionType = trade.TransactionType;

                tradeList.Add(tradeRep);
            }

            if (tradeList.Count > 0)
            {
                DataTable dtTrades = tradeList.ToDataTable();

                dbMethods.RefreshTrades(dtTrades);
            }

        }

        private void UpdateMargins()
        {
            UserMargin margin = kite.GetMargins(Constants.MARGIN_EQUITY);

            dbMethods.LoadMargins(margin);
        }

        private void UpdateLabels()
        {
            decimal profit, loss, profitLoss;
            int boCompleted, boPending, boTotal;
            int coCompleted, coPending, coTotal;

            dbMethods.GetPLStatus(tradeDate, out profit, out loss, out profitLoss, out boCompleted, out boPending, out boTotal, out coCompleted, out coPending, out coTotal);

            lblProfit.Text = profit.ToString();
            lblLoss.Text = loss.ToString();
            lblProfitLoss.Text = profitLoss.ToString();

            lblBOCompleted.Text = boCompleted.ToString();
            lblBOPending.Text = boPending.ToString();
            lblBOTotal.Text = boTotal.ToString();

            lblCOCompleted.Text = coCompleted.ToString();
            lblCOPending.Text = coPending.ToString();
            lblCOTotal.Text = coTotal.ToString();
        }


        private void ToggleRealTimeOrderPlacement()
        {
            placeOrders = !placeOrders;

            if (placeOrders)
            {
                btnToggleOrderPlacement.Text = Utilities.RealTimeOrderPlacementON;
                btnToggleOrderPlacement.BackColor = Color.Green;
            }
            else
            {
                btnToggleOrderPlacement.Text = Utilities.RealTimeOrderPlacementOFF;
                btnToggleOrderPlacement.BackColor = Color.Silver;
            }
        }

        private void InitializeThreads()
        {
            thElderStrategy = new Thread(new ThreadStart(MainBusinessLogic));
            thElderStrategy.IsBackground = true;

            thRealTimePTMFetch = new Thread(new ThreadStart(GetPositions));
            thRealTimePTMFetch.IsBackground = true;

            //thMonitorPostions = new Thread(new ThreadStart(MonitorPositions));
        }

        private void btnToggleOrderPlacement_Click(object sender, EventArgs e)
        {
            ToggleRealTimeOrderPlacement();
        }


        private void btnGetOTP_Click(object sender, EventArgs e)
        {
            //Get Margins
            //Get Trades
            //Get Positions

            //This has to be done using Stored procs only. Entity Framework should not be used.
            //thRealTimePTMFetch.Start();
        }

        private void btnMonitorPositions_Click(object sender, EventArgs e)
        {
            //thMonitorPostions.Start();

            //btnMonitorPositions.Enabled = false;
        }

        private void btnStartTrading_Click(object sender, EventArgs e)
        {
            if (thElderStrategy.ThreadState != ThreadState.Running || thElderStrategy.ThreadState != ThreadState.Suspended)
            {
                thElderStrategy.Start();
            }

            if (thRealTimePTMFetch.ThreadState != ThreadState.Running || thRealTimePTMFetch.ThreadState != ThreadState.Suspended)
            {
                thRealTimePTMFetch.Start();
            }

            mStopThread = false;

            btnStartTrading.Enabled = false;
            btnStartTrading.BackColor = Color.Green;

            btnStopTrading.Enabled = true;
            btnStopTrading.BackColor = Color.LightCoral;
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                currentPosition = dbMethods.GetElderStrategyOrdersForView(tradeDate);

                currentPosition.OrderByDescending(col1 => col1.EntryTime).ThenByDescending(col2 => col2.ExitTime).ThenByDescending(col3 => col3.StopLossHitTime);

                dgvCurrentPositions.DataSource = currentPosition;

            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR in Refresh!!" + ex.Message + ". Check ErrorLog for more info.");
                Logger.ErrorLogToFile(ex);
            }
        }

        private void btnStopTrading_Click(object sender, EventArgs e)
        {
            mStopThread = true;

            btnStartTrading.Enabled = true;
            btnStartTrading.BackColor = Color.Silver;

            btnStopTrading.Enabled = false;
            btnStopTrading.BackColor = Color.Silver;
        }

        private void btnMTNRefresh_Click(object sender, EventArgs e)
        {
            //Refresh Trades / Positions / Margins
            try
            {
                UpdateNetPositions();

                UpdateTrades();

                UpdateMargins();

                UpdateLabels();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR in MTNRefresh!!" + ex.Message + ". Check ErrorLog for more info.");
                Logger.ErrorLogToFile(ex);
            }
        }

        private void dgvCurrentPositions_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sortDirection == "ASC") sortDirection = "DESC";
            else sortDirection = "ASC";

            var orderByExpression = Utilities.GetOrderByExpression<ElderStrategyOrderVM>(dgvCurrentPositions.Columns[e.ColumnIndex].Name);

            var data = Utilities.OrderByDir<ElderStrategyOrderVM>(currentPosition, sortDirection, orderByExpression);

            dgvCurrentPositions.DataSource = data.ToList();
        }

        private void txtSearchBySymbol_TextChanged(object sender, EventArgs e)
        {
            dgvCurrentPositions.DataSource = (from filter in currentPosition
                                             where filter.TradingSymbol.Contains(txtSearchBySymbol.Text.ToUpper())
                                             select filter).ToList();
        }

        private void rbAllOrders_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
