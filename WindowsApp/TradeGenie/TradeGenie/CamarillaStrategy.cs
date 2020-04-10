using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using TradeGenie.Repository;
using KiteConnect;

namespace TradeGenie
{
    //public partial class CamarillaStrategy : TradeGenieForm
    //{
    //    List<MasterStockList> mslList = new List<MasterStockList>();
    //    RepositoryMethods dbMethods = new RepositoryMethods();
    //    List<Camarilla> camarillaAllStocks = new List<Camarilla>();

    //    private Object thisLock = new Object();
    //    private static bool placeOrders = true;
    //    private static bool getOrders = true;
    //    private int currentIndex = 1;
    //    private int previousIndex = 0;

    //    DateTime tradeDate = DateTime.Today;
    //    TimeSpan CurrentTime = new TimeSpan(DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, 0);
    //    TimeSpan startTime = new TimeSpan(9, 15, 0);
    //    TimeSpan endTime = new TimeSpan(15, 15, 0);
    //    DateTime fromDateTime;
    //    DateTime toDateTime;
    //    DateTime endDateTime;
    //    DateTime lastRunTimeLong;
    //    DateTime lastRunTimeShort;

    //    Thread thCamarillaStrategy;
    //    Thread thRealTimePTMFetch;
    //    private volatile bool mStopThread;
    //    private string sortDirection;

    //    List<CamarillaStrategyOrderVM> currentPosition = new List<CamarillaStrategyOrderVM>();

    //    public CamarillaStrategy()
    //    {
    //        InitializeComponent();

    //        ToggleRealTimeOrderPlacement();

    //        InitializeThreads();
    //    }

    //    private void InitializeThreads()
    //    {
    //        thCamarillaStrategy = new Thread(new ThreadStart(MainBusinessLogic));
    //        thCamarillaStrategy.IsBackground = true;

    //        //thRealTimePTMFetch = new Thread(new ThreadStart(GetPositions));
    //        //thRealTimePTMFetch.IsBackground = true;
    //    }

    //    private void ToggleRealTimeOrderPlacement()
    //    {
    //        placeOrders = !placeOrders;

    //        if (placeOrders)
    //        {
    //            btnToggleOrderPlacement.Text = Utilities.RealTimeOrderPlacementON;
    //            btnToggleOrderPlacement.BackColor = Color.Green;
    //        }
    //        else
    //        {
    //            btnToggleOrderPlacement.Text = Utilities.RealTimeOrderPlacementOFF;
    //            btnToggleOrderPlacement.BackColor = Color.Silver;
    //        }
    //    }

    //    public void MainBusinessLogic()
    //    {
    //        string timerToRun = string.Empty;

    //        timerToRun = TimerToRun(UserConfiguration.TimePeriodShort);

    //        List<CamarillaStrategyOrderVM> orders = new List<CamarillaStrategyOrderVM>();

    //        mslList = dbMethods.GetMasterStockList();

    //        while (!mStopThread)
    //        {
    //            try
    //            {
    //                CurrentTime = new TimeSpan(DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, 0);

    //                fromDateTime = DateTime.Today + startTime;
    //                toDateTime = DateTime.Today + CurrentTime;
    //                endDateTime = DateTime.Today + endTime;

    //                if (Utilities.MinuteTimer(timerToRun).Contains(CurrentTime))
    //                {
    //                    if (lastRunTimeLong != DateTime.Today + CurrentTime)
    //                    {
    //                        orders = new List<CamarillaStrategyOrderVM>();

    //                        List<TickerElderIndicatorsVM> elderList =
    //                            GetTickerElderForTimePeriod(UserConfiguration.TimePeriodLong.ToString(), fromDateTime, toDateTime);

    //                        foreach (MasterStockList item in mslList)
    //                        {
    //                            Camarilla camarilla = ScanForCamarillaTrigger(elderList[previousIndex], elderList[currentIndex]);


    //                        }
                            
    //                    }

    //                    lastRunTimeLong = DateTime.Today + CurrentTime;
    //                }

    //            }
    //            catch (Exception ex)
    //            {

    //            }
    //        }
    //    }

    //    private void PlaceOrders(Camarilla camarilla)
    //    {
    //        try
    //        {
    //            CamarillaStrategyOrder order = new CamarillaStrategyOrder();

    //            //Calculate Quantity
    //            //Calculate Stop Loss
    //            //Calculate Entry
    //            order.EntryPrice = dbMethods.GetLTP(camarilla.TradingSymbol);
    //            order.SLPrice = GetStopLossValue((decimal)longItem.EntryPrice, Utilities.LongPosition);
    //            order.Quantity = GetQuantity(longItem);
    //            order.PositionStatus = "OPEN";

    //            #region Long Bracket Order

    //            bool isPostionExists = dbMethods.IsPositionExists(longItem.TradingSymbol, longItem.PositionType,
    //                                longItem.TradeDate, Constants.VARIETY_BO, Constants.TRANSACTION_TYPE_BUY, placeOrders);

    //            if (!isPostionExists)
    //            {
    //                if (placeOrders)
    //                {
    //                    longItem.OrderId = null;
    //                    longItem.isRealOrderPlaced = null;
    //                    longItem.OrderVariety = Constants.VARIETY_BO;
    //                    longItem.EntryTime = DateTime.Now;

    //                    //Calculate Target
    //                    decimal targetValue = GetTarget((decimal)longItem.EntryPrice, Utilities.LongPosition);
    //                    decimal boTarget, boStopLoss, boTrailingStopLoss;

    //                    CalculateTargetSLTSL(Utilities.LongPosition, (decimal)longItem.EntryPrice, targetValue, (decimal)longItem.StopLoss,
    //                                    out boTarget, out boStopLoss, out boTrailingStopLoss);

    //                    //Place Bracket Order
    //                    Dictionary<string, dynamic> response = kite.PlaceOrder(
    //                                Exchange: Constants.EXCHANGE_NSE,
    //                                TradingSymbol: longItem.TradingSymbol,
    //                                TransactionType: Constants.TRANSACTION_TYPE_BUY,
    //                                Quantity: (int)longItem.Quantity,
    //                                Price: longItem.EntryPrice,
    //                                OrderType: Constants.ORDER_TYPE_LIMIT,
    //                                Product: Constants.PRODUCT_MIS,
    //                                Variety: Constants.VARIETY_BO,
    //                                SquareOffValue: boTarget,
    //                                StoplossValue: boStopLoss,
    //                                Validity: Constants.VALIDITY_DAY);

    //                    if (response != null && response["status"] == "success")
    //                    {
    //                        longItem.OrderId = response["data"]["order_id"];
    //                    }

    //                    longItem.isRealOrderPlaced = true;

    //                    Logger.LogToFile(string.Format("{0},{1},{2},{3},{4},{5},{6},{7}",
    //                        "BO Placed", longItem.TradingSymbol, Utilities.LongPosition,
    //                        "Entry Price:" + longItem.EntryPrice.ToString(),
    //                        "Target Price:" + boTarget.ToString(),
    //                        "Stop Loss Price:" + boStopLoss.ToString(),
    //                        "Trailing Stop Loss Price:" + boTrailingStopLoss.ToString(),
    //                        "Quantity:" + longItem.Quantity.ToString()));
    //                }
    //                else
    //                {
    //                    longItem.OrderId = null;
    //                    longItem.isRealOrderPlaced = null;
    //                }

    //                dbMethods.InsertElderStrategyOrder(longItem);


    //            }
    //        }

    //        #endregion

    //        catch (Exception ex)
    //        {
    //            Logger.LogToFile(string.Format("{0},{1}", "Exception", longItem.TradingSymbol));
    //            Logger.ErrorLogToFile(ex);
    //        }

    //    }

    //    private void CalculateTargetSLTSL(string positionType, decimal entryValue, decimal exitValue, decimal stopLossValue,
    //        out decimal boTarget, out decimal boStopLoss, out decimal boTrailingStopLoss)
    //    {
    //        boTarget = 0.0M;
    //        boStopLoss = 0.0M;
    //        boTrailingStopLoss = 0.0M;

    //        if (Utilities.LongPosition == positionType)
    //        {
    //            boTarget = Math.Round(exitValue - entryValue, 1);
    //            boStopLoss = Math.Round(entryValue - stopLossValue, 1);
    //            boTrailingStopLoss = Int32.Parse(Math.Round((exitValue - entryValue) / 3, 0).ToString());
    //        }

    //        if (Utilities.ShortPosition == positionType)
    //        {
    //            boTarget = Math.Round((entryValue - exitValue), 1);
    //            boStopLoss = Math.Round((stopLossValue - entryValue), 1);
    //            boTrailingStopLoss = Int32.Parse(Math.Round((entryValue - exitValue) / 3, 0).ToString());
    //        }
    //    }

    //    private decimal GetStopLossValue(decimal ltp, string positionType)
    //    {
    //        decimal stopLossPrice = 0.0M;

    //        if (positionType == Utilities.LongPosition)
    //        {
    //            stopLossPrice = Math.Round((decimal)(ltp / UserConfiguration.StopLossPercentageHigh), 1);
    //        }
    //        else
    //        {
    //            stopLossPrice = Math.Round((decimal)(ltp * UserConfiguration.StopLossPercentageHigh), 1);
    //        }

    //        return stopLossPrice;
    //    }

    //    private decimal GetTarget(decimal ltp, string positionType)
    //    {
    //        decimal targetPrice = 0.0M;

    //        if (positionType == Utilities.LongPosition)
    //        {
    //            targetPrice = Math.Round((decimal)(ltp * UserConfiguration.InitialExitPercentage), 1);
    //        }
    //        else
    //        {
    //            targetPrice = Math.Round((decimal)(ltp / UserConfiguration.InitialExitPercentage), 1);
    //        }

    //        return targetPrice;
    //    }

    //    private int GetQuantity(ElderStrategyOrder elderOrder)
    //    {
    //        int quantity = 0;

    //        quantity = 1;

    //        quantity = Convert.ToInt32(Math.Round((decimal)(UserConfiguration.TotalPurchaseValue / elderOrder.EntryPrice), 0));

    //        return quantity;
    //    }

    //    private Camarilla ScanForCamarillaTrigger(TickerElderIndicatorsVM prevItem, TickerElderIndicatorsVM curItem)
    //    {
    //        Camarilla selStock = camarillaAllStocks.Where(a => a.TradingSymbol == curItem.TradingSymbol
    //                                                    && a.CamarillaDate.Day == curItem.TickerDateTime.Day
    //                                                    && a.CamarillaDate.Month == curItem.TickerDateTime.Month).Single();

    //        if (prevItem.PriceClose != null)
    //        {

    //            switch (selStock.Scenario)
    //            {
    //                case CamarillaScenario.SCENARIO_1:
    //                    {
    //                        if (!selStock.IsTriggered)
    //                        {
    //                            if (prevItem.PriceClose <= selStock.CamarillaLevel.L3 && curItem.PriceClose > selStock.CamarillaLevel.L3)
    //                            {
    //                                selStock.IsTriggered = true;
    //                                selStock.CamarillaSignal = Signal.LONG;

    //                                selStock.BuyPrice = (double)curItem.PriceClose;
    //                                selStock.StopLoss = Math.Round((selStock.CamarillaLevel.L3 + selStock.CamarillaLevel.L4) / 2, 2);
    //                                selStock.Target = (double)selStock.CamarillaLevel.H1;

    //                            }

    //                            if (prevItem.PriceClose >= selStock.CamarillaLevel.H3 && curItem.PriceClose < selStock.CamarillaLevel.L3)
    //                            {
    //                                selStock.IsTriggered = true;
    //                                selStock.CamarillaSignal = Signal.SHORT;

    //                                selStock.SellPrice = (double)curItem.PriceClose;
    //                                selStock.StopLoss = Math.Round((selStock.CamarillaLevel.H3 + selStock.CamarillaLevel.H4) / 2, 2);
    //                                selStock.Target = (double)selStock.CamarillaLevel.L1;
    //                            }
    //                        }

    //                        break;
    //                    }
    //                case CamarillaScenario.SCENARIO_2:
    //                    {
    //                        if (!selStock.IsTriggered)
    //                        {
    //                            if (prevItem.PriceClose <= selStock.CamarillaLevel.H4 && curItem.PriceClose > selStock.CamarillaLevel.H4)
    //                            {
    //                                selStock.IsTriggered = true;
    //                                selStock.CamarillaSignal = Signal.LONG;

    //                                selStock.BuyPrice = (double)curItem.PriceClose;
    //                                selStock.StopLoss = Math.Round((selStock.CamarillaLevel.H3 + selStock.CamarillaLevel.H4) / 2, 2);
    //                                //selStock.Target = Math.Round((double)curItem.PriceClose * 1.005,2);
    //                                selStock.Target = (double)selStock.CamarillaLevel.H5;
    //                            }

    //                            if (prevItem.PriceClose >= selStock.CamarillaLevel.H3 && curItem.PriceClose < selStock.CamarillaLevel.H3)
    //                            {
    //                                selStock.IsTriggered = true;
    //                                selStock.CamarillaSignal = Signal.SHORT;

    //                                selStock.SellPrice = (double)curItem.PriceClose;
    //                                selStock.StopLoss = Math.Round((selStock.CamarillaLevel.H3 + selStock.CamarillaLevel.H4) / 2, 2);
    //                                selStock.Target = (double)selStock.CamarillaLevel.L1;
    //                            }
    //                        }

    //                        break;
    //                    }
    //                case CamarillaScenario.SCENARIO_3:
    //                    {
    //                        if (!selStock.IsTriggered)
    //                        {
    //                            if (prevItem.PriceClose <= selStock.CamarillaLevel.L3 && curItem.PriceClose > selStock.CamarillaLevel.L3)
    //                            {
    //                                selStock.IsTriggered = true;
    //                                selStock.CamarillaSignal = Signal.LONG;

    //                                selStock.BuyPrice = (double)curItem.PriceClose;
    //                                selStock.StopLoss = Math.Round((selStock.CamarillaLevel.L3 + selStock.CamarillaLevel.L4) / 2, 2);
    //                                selStock.Target = (double)selStock.CamarillaLevel.H1;
    //                            }

    //                            if (prevItem.PriceClose >= selStock.CamarillaLevel.L4 && curItem.PriceClose < selStock.CamarillaLevel.L4)
    //                            {
    //                                selStock.IsTriggered = true;
    //                                selStock.CamarillaSignal = Signal.SHORT;

    //                                selStock.SellPrice = (double)curItem.PriceClose;
    //                                selStock.StopLoss = Math.Round((selStock.CamarillaLevel.L3 + selStock.CamarillaLevel.L4) / 2, 2);
    //                                //selStock.Target = Math.Round((double)curItem.PriceClose / 1.005, 2);
    //                                selStock.Target = (double)selStock.CamarillaLevel.L5;
    //                            }
    //                        }

    //                        break;
    //                    }
    //                case CamarillaScenario.SCENARIO_4:
    //                    {
    //                        if (!selStock.IsTriggered)
    //                        {
    //                            if (prevItem.PriceClose >= selStock.CamarillaLevel.H3 && curItem.PriceClose < selStock.CamarillaLevel.H3)
    //                            {
    //                                selStock.IsTriggered = true;
    //                                selStock.CamarillaSignal = Signal.SHORT;

    //                                selStock.SellPrice = (double)curItem.PriceClose;
    //                                selStock.StopLoss = Math.Round((selStock.CamarillaLevel.H3 + selStock.CamarillaLevel.H4) / 2, 2);
    //                                selStock.Target = (double)selStock.CamarillaLevel.L1;
    //                            }
    //                        }
    //                        break;
    //                    }
    //                case CamarillaScenario.SCENARIO_5:
    //                    {
    //                        if (!selStock.IsTriggered)
    //                        {
    //                            if (prevItem.PriceClose <= selStock.CamarillaLevel.L3 && curItem.PriceClose > selStock.CamarillaLevel.L3)
    //                            {
    //                                selStock.IsTriggered = true;
    //                                selStock.CamarillaSignal = Signal.LONG;

    //                                selStock.BuyPrice = (double)curItem.PriceClose;
    //                                selStock.StopLoss = Math.Round((selStock.CamarillaLevel.L3 + selStock.CamarillaLevel.L4) / 2, 2);
    //                                selStock.Target = (double)selStock.CamarillaLevel.H1;
    //                            }
    //                        }

    //                        break;
    //                    }

    //            }
    //        }

    //        //if (selStock.CamarillaSignal )
    //        //{
    //        //    selStock.CamarillaSignal = Signal.NA;
    //        //}

    //        return selStock;
    //    }


    //    private void ScanForCamarillaScenario(DayOHLC dayOpenHighLow)
    //    {
    //        Camarilla selStock = camarillaAllStocks.Where(a => a.TradingSymbol == dayOpenHighLow.TradingSymbol
    //                                                    && a.CamarillaDate.Day == dayOpenHighLow.OHLCDate.Day
    //                                                    && a.CamarillaDate.Month == dayOpenHighLow.OHLCDate.Month).Single();

    //        //Scenario 1 - Open Price between H3 and L3
    //        if (dayOpenHighLow.Open < selStock.CamarillaLevel.H3 && dayOpenHighLow.Open > selStock.CamarillaLevel.L3)
    //        {
    //            selStock.Scenario = CamarillaScenario.SCENARIO_1;
    //        }
    //        //Scenario 2 - Open Price between H3 and H4
    //        else if (dayOpenHighLow.Open < selStock.CamarillaLevel.H4 && dayOpenHighLow.Open > selStock.CamarillaLevel.H3)
    //        {
    //            selStock.Scenario = CamarillaScenario.SCENARIO_2;
    //        }
    //        //Scenario 3 - Open Price between L3 and L4
    //        else if (dayOpenHighLow.Open < selStock.CamarillaLevel.L3 && dayOpenHighLow.Open > selStock.CamarillaLevel.L4)
    //        {
    //            selStock.Scenario = CamarillaScenario.SCENARIO_3;
    //        }
    //        //Scenario 4 - Open Price above H4
    //        else if (dayOpenHighLow.Open > selStock.CamarillaLevel.H4)
    //        {
    //            selStock.Scenario = CamarillaScenario.SCENARIO_4;
    //        }
    //        //Scenario 5 - Open Price below L4
    //        else if (dayOpenHighLow.Open < selStock.CamarillaLevel.L4)
    //        {
    //            selStock.Scenario = CamarillaScenario.SCENARIO_5;
    //        }
    //        else
    //        {
    //            selStock.Scenario = CamarillaScenario.NA;
    //        }

    //        selStock.IsTriggered = false;
    //    }

    //    public List<TickerElderIndicatorsVM> GetTickerElderForTimePeriod(string timePeriod, DateTime startDateTime, DateTime endDateTime)
    //    {
    //        string instrumentList = string.Join(",", from item in mslList select item.TradingSymbol);

    //        List<TickerElderIndicatorsVM> tikcerElderList = dbMethods.GetTickerDataForIndicators
    //                                            (instrumentList, timePeriod, startDateTime, endDateTime);

    //        return tikcerElderList;

    //    }

    //    private void CalculateCamarillaPoints()
    //    {
    //        //1. Loop thru the date list
    //        //2. Loop thru the master stock list
    //        //2. Take the previous days Close, High and Low
    //        //3. Calculate Camarilla levels

    //        //pivot = (high + low + close) / 3.0
    //        //range = high - low
    //        //h5 = (high / low) * close
    //        //h4 = close + (high - low) * 1.1 / 2.0
    //        //h3 = close + (high - low) * 1.1 / 4.0
    //        //h2 = close + (high - low) * 1.1 / 6.0
    //        //h1 = close + (high - low) * 1.1 / 12.0
    //        //l1 = close - (high - low) * 1.1 / 12.0
    //        //l2 = close - (high - low) * 1.1 / 6.0
    //        //l3 = close - (high - low) * 1.1 / 4.0
    //        //l4 = close - (high - low) * 1.1 / 2.0
    //        //h6 = h5 + 1.168 * (h5 - h4)
    //        //l5 = close - (h5 - close)
    //        //l6 = close - (h6 - close)

    //        try
    //        {
    //            foreach (MasterStockList msl in mslList)
    //            {
    //                Camarilla camarilla = new Camarilla();

    //                DayOHLC previousDay = dbMethods.GetPreviousDayOHLC(msl.TradingSymbol, dtPreviousTradeDate.Value);

    //                CamarillaLevels camLevels = new CamarillaLevels();

    //                camarilla.CamarillaDate = dtPreviousTradeDate.Value;
    //                camarilla.TradingSymbol = msl.TradingSymbol;

    //                camLevels.Close = previousDay.Close;
    //                camLevels.High = previousDay.High;
    //                camLevels.Low = previousDay.Low;

    //                camLevels.Pivot = Math.Round((previousDay.High + previousDay.Low + previousDay.Close) / 3, 2);
    //                camLevels.Range = Math.Round(previousDay.High - previousDay.Low, 2);

    //                camLevels.H5 = Math.Round((previousDay.High / previousDay.Low) * previousDay.Close, 2);
    //                camLevels.H4 = Math.Round((previousDay.Close + (previousDay.High - previousDay.Low) * 1.1 / 2), 2);
    //                camLevels.H3 = Math.Round((previousDay.Close + (previousDay.High - previousDay.Low) * 1.1 / 4), 2);
    //                camLevels.H2 = Math.Round((previousDay.Close + (previousDay.High - previousDay.Low) * 1.1 / 6), 2);
    //                camLevels.H1 = Math.Round((previousDay.Close + (previousDay.High - previousDay.Low) * 1.1 / 12), 2);
    //                camLevels.L1 = Math.Round((previousDay.Close - (previousDay.High - previousDay.Low) * 1.1 / 12), 2);
    //                camLevels.L2 = Math.Round((previousDay.Close - (previousDay.High - previousDay.Low) * 1.1 / 6), 2);
    //                camLevels.L3 = Math.Round((previousDay.Close - (previousDay.High - previousDay.Low) * 1.1 / 4), 2);
    //                camLevels.L4 = Math.Round((previousDay.Close - (previousDay.High - previousDay.Low) * 1.1 / 2), 2);

    //                camLevels.H6 = Math.Round((camLevels.H5 + 1.168 * (camLevels.H5 - camLevels.H4)), 2);
    //                camLevels.L5 = Math.Round(previousDay.Close - (camLevels.H5 - previousDay.Close), 2);
    //                camLevels.L6 = Math.Round(previousDay.Close - (camLevels.H6 - previousDay.Close), 2);

    //                camarilla.CamarillaLevel = camLevels;

    //                camarillaAllStocks.Add(camarilla);
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Logger.ErrorLogToFile(ex);
    //        }
    //    }

    //    private string TimerToRun(int timePeriod)
    //    {
    //        string retValue = string.Empty;

    //        switch (timePeriod)
    //        {
    //            case 5:
    //                retValue = UserConfiguration.Min5Timer;
    //                break;
    //            case 15:
    //                retValue = UserConfiguration.Min15Timer;
    //                break;
    //            case 30:
    //                retValue = UserConfiguration.Min30Timer;
    //                break;
    //        }

    //        return retValue;
    //    }

    //    private void label3_Click(object sender, EventArgs e)
    //    {

    //    }
    //}
}