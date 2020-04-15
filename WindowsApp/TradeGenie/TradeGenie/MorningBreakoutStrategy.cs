using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KiteConnect;
using System.Threading;
using TradeGenie.Repository;

/* On Clicking the start trading button
 * 1. Thread 1 
 *      Create a thread that will subscribe all the stocks that are in the pick list. 
 *      Delegate one more method for the onTick Event and name it as ltp populate
 *      On every tick, find the appropriate instrument and update the db
 * 2. Thread 2
 *      Create a thread that will fetch the data from the morning strategy table
 *      delay it for every 30 sec
 *      Based on the LTP, determine the entry exit and other parameters.
 *      Simulate the BO order placement
 * **/

namespace TradeGenie
{
    public partial class MorningBreakoutStrategy : TradeGenieForm
    {
        DataView dvOHLCDataView;
        DataTable dtOHLCDataTable = new DataTable();
        Thread morningBKTicker;
        List<MorningBreakOutStrategyDM> morningBreakouts;
        List<MorningBreakOutStrategyDM> morningBreakoutsForSimulation;
        string[] insListForSubscription;
        private Object thisLock = new Object();
        private string sortDirection;
        private static bool placeOrders = true;
        private static decimal stopLossCandleSize = 0.0M;

        public MorningBreakoutStrategy()
        {
            InitializeComponent();

            ToggleRealTimeOrderPlacement();
        }

        //private void btnGetOHLCData_Click(object sender, EventArgs e)
        //{
        //    List<InstrumentVM> instrumentList = new List<InstrumentVM>();

        //    if (!string.IsNullOrEmpty(cmbCollection.SelectedItem.ToString()))
        //    {
        //        instrumentList = dbMethods.GetStockList(cmbCollection.SelectedItem.ToString());

        //        var stockArray = from ins in instrumentList
        //                         select ins.Exchange + ":" + ins.TradingSymbol;

        //        string[] stockList = stockArray.ToArray<string>();

        //        Dictionary<string, OHLC> ohlcs = kite.GetOHLC(stockList);

        //        var gridOHLC = from data in ohlcs
        //                       select new InstrumentOHLCVM
        //                       {
        //                           TradingSymbol = data.Key,
        //                           Open = data.Value.Open,
        //                           High = data.Value.High,
        //                           Low = data.Value.Low,
        //                           PreviousClose = data.Value.Close,
        //                           LastPrice = data.Value.LastPrice,
        //                           InstrumentToken = (int)data.Value.InstrumentToken,
        //                           OHLCDateTime = DateTime.Now
        //                       };


        //        dtOHLCDataTable = Utilities.ToDataTable<InstrumentOHLCVM>(gridOHLC.ToList());

        //        dgvOHLCData.DataSource = dtOHLCDataTable;

        //    }
        //}

        private void txtFilterBySymbol_TextChanged(object sender, EventArgs e)
        {
            dgvMorningBreakout.DataSource = (from filter in morningBreakoutsForSimulation
                                             where filter.TradingSymbol.Contains(txtFilterBySymbol.Text.ToUpper())
                                             select filter).ToList();

        }

        //1. Load OHLC Data  Step 1
        private void btnLoadOHLC_Click(object sender, EventArgs e)
        {
            GetOHLCFromKite();
        }

        //2. Load Potential trading scripts for the morning breakout strategy
        private void btnTradingScripts_Click(object sender, EventArgs e)
        {
            //Get OHLC Data from DB
            //Loop thru and fill the MorningBKstrategyDM with OHLC and Trading sysmbol
            //Subsequently fill the Candle Height and Price bracket

            LoadInitialMorningBreakoutData();
        }

        //3. Start the trading loop
        private void btnStartTrading_Click(object sender, EventArgs e)
        {
            //SubscribeAndLoadTickers();

            CalculateStopLossCandleSize();

            morningBKTicker = new Thread(new ThreadStart(MonitorMorningBreakouts));
            morningBKTicker.Start();
            MessageBox.Show("Trading Started. ");
            //MonitorMorningBreakouts();
        }

        //Refresh the grid to see the real time
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            lock (thisLock)
            {
                morningBreakoutsForSimulation = dbMethods.GetAllMorningBreakouts();
            }

            morningBreakoutsForSimulation.OrderBy(col1 => col1.ExitTime).ThenBy(col2 => col2.StopLossHitTime);

            dgvMorningBreakout.DataSource = morningBreakoutsForSimulation;

            LoadLabels();
        }

        //Subscription ticker event that loads the Quote. In future make this to load only the LTP
        public void onSubscriptionTick(Tick TickData)
        {
            //Load the ticker data to DB
            lock (thisLock)
            {
                //dbMethods.SetLTPHighLow(TickData);
                dbMethods.LoadQuote(TickData);
            }
        }

        private void dgvMorningBreakout_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (sortDirection == "ASC") sortDirection = "DESC";
            else sortDirection = "ASC";

            var orderByExpression = Utilities.GetOrderByExpression<MorningBreakOutStrategyDM>(dgvMorningBreakout.Columns[e.ColumnIndex].Name);

            var data = Utilities.OrderByDir<MorningBreakOutStrategyDM>(morningBreakoutsForSimulation, sortDirection, orderByExpression);

            dgvMorningBreakout.DataSource = data.ToList();

        }

        private void btnToggleOrderPlacement_Click(object sender, EventArgs e)
        {
            ToggleRealTimeOrderPlacement();
        }

        private void btnGetOTP_Click(object sender, EventArgs e)
        {
            try
            {
                List<KiteConnect.Order> orders = kite.GetOrders();

                lock (thisLock)
                {
                    dbMethods.LoadOrders(orders);
                }

                PositionResponse pr = kite.GetPositions();

                lock (thisLock)
                {
                    dbMethods.LoadPositions(pr.Net);
                }

                List<KiteConnect.Trade> trades = kite.GetOrderTrades();

                lock (thisLock)
                {
                    dbMethods.LoadTrades(trades);
                }

                //KiteConnect.Margin margin = kite.Margins("equity");

                //lock (thisLock)
                //{
                //    dbMethods.LoadMargins(margin);
                //}

            }
            catch (Exception ex)
            {
                Logger.ErrorLogToFile(ex);
            }
        }

        private void GetOHLCFromKite()
        {
            List<InstrumentVM> instrumentList = new List<InstrumentVM>();
            List<InstrumentOHLCVM> ohlcList = new List<InstrumentOHLCVM>();

            string retMessage = string.Empty;
            int retVal = 0;

            string[] niftyIndexStocks = { "Nifty 50", "Nifty Next 50", "Nifty Midcap 50" };

            foreach (string niftyIndexItem in niftyIndexStocks)
            {
                instrumentList = dbMethods.GetStockList(niftyIndexItem);

                var stockArray = from ins in instrumentList
                                 select ins.Exchange + ":" + ins.TradingSymbol;

                string[] stockList = stockArray.ToArray<string>();

                Dictionary<string, OHLC> ohlcs = kite.GetOHLC(stockList);

                Logger.GenericLog("Kite returned data," + ohlcs.Count);

                var varOHLC = from data in ohlcs
                              select new InstrumentOHLCVM
                              {
                                  TradingSymbol = data.Key,
                                  Open = data.Value.Open,
                                  High = data.Value.High,
                                  Low = data.Value.Low,
                                  PreviousClose = data.Value.Close,
                                  LastPrice = data.Value.LastPrice,
                                  InstrumentToken = (int)data.Value.InstrumentToken,
                                  OHLCDateTime = DateTime.Now
                              };

                ohlcList = varOHLC.ToList();

                lock (thisLock)
                {
                    retVal = dbMethods.LoadOHLCData(ohlcList);
                }

                if (retVal == 1)
                {
                    retMessage = "Exception while uploading " + niftyIndexItem + " instruments OHLC Data to DB";
                }
                else
                {
                    retMessage = niftyIndexItem + " OHLC Data loaded successfully";
                }

                MessageBox.Show(retMessage);

                Thread.Sleep(1000);
            }
        }

        //Not required for the new Elder Logic
        private void LoadInitialMorningBreakoutData()
        {
            //Get OHLC Data from DB
            //Loop thru and fill the MorningBKstrategyDM with OHLC and Trading sysmbol
            //Subsequently fill the Candle Height and Price bracket

            List<MorningBreakOutStrategyDM> mbksList = new List<MorningBreakOutStrategyDM>();
            List<OHLCData> ohlcAll = new List<OHLCData>();
            int retval = 0;

            lock (thisLock)
            {
                ohlcAll = dbMethods.GetOHLCData();
            }

            foreach (OHLCData ohlc in ohlcAll)
            {
                decimal dCandleSize = 0.0M;

                try
                {
                    dCandleSize = Math.Round((decimal)(((ohlc.High - ohlc.Low) / ohlc.Open) * 100), 2, MidpointRounding.AwayFromZero);
                }
                catch { }

                if (dCandleSize > UserConfiguration.CandleLengthPercentageLow && dCandleSize < UserConfiguration.CandleLengthPercentageHigh)
                {
                    if (ohlc.LastPrice > UserConfiguration.PriceLowerBoundary && ohlc.LastPrice < UserConfiguration.PriceUpperBoundary)
                    {
                        MorningBreakOutStrategyDM mbks = new MorningBreakOutStrategyDM();

                        mbks.DateTimePeriod = ohlc.LastUpdatedTime;
                        mbks.InstrumentToken = ohlc.InstrumentToken;
                        mbks.TradingSymbol = ohlc.TradingSymbol;
                        mbks.Open = ohlc.Open;
                        mbks.High = ohlc.High;
                        mbks.Low = ohlc.Low;
                        mbks.Close = ohlc.LastPrice;
                        mbks.CandleSize = dCandleSize;

                        mbksList.Add(mbks);
                    }
                }

            }

            lock (thisLock)
            {
                retval = dbMethods.LoadMorningBreakoutData(mbksList);
            }

            if (retval == 0)
                MessageBox.Show("Initial Morning Breakout data is loaded.");

        }

        private void MonitorMorningBreakouts()
        {
            while (true)
            {
                try
                {
                    lock (thisLock)
                    {
                        morningBreakoutsForSimulation = dbMethods.GetAllMorningBreakouts();
                    }

                    foreach (MorningBreakOutStrategyDM mbk in morningBreakoutsForSimulation)
                    {

                        UpwardMovement(mbk);

                        DownwardMovement(mbk);
                    }

                    //dgvMorningBreakout.Invoke((Action)(() => dgvMorningBreakout.DataSource = morningBreakoutsForSimulation));

                    lock (thisLock)
                    {
                        dbMethods.UpdateMorningBreakoutData(morningBreakoutsForSimulation);
                    }

                    //Sleep for configured seconds
                    Thread.Sleep(UserConfiguration.MorningBreakoutInterval);
                }
                catch (Exception ex)
                {
                    Logger.ErrorLogToFile(ex);

                    Thread.Sleep(UserConfiguration.MorningBreakoutInterval);
                }
            }
        }

        private void UpwardMovement(MorningBreakOutStrategyDM mbk)
        {
            if ((mbk.Movement != null && mbk.Movement == Utilities.DownwardMovement)) // || rbDownTrend.Checked)
                return;

            if (mbk.LTP != null && mbk.LTP >= Math.Round((decimal)(mbk.High * UserConfiguration.EntryPercentage), 1) && mbk.Entry == null && mbk.Movement == null)
            {
                mbk.Entry = Math.Round((decimal)(mbk.High * UserConfiguration.EntryPercentage), 1, MidpointRounding.ToEven);

                mbk.Quantity = Convert.ToInt32(Math.Round((decimal)(UserConfiguration.TotalPurchaseValue / mbk.Entry), 0));

                mbk.Movement = Utilities.UpwardMovement;

                Logger.LogToFile("Upward," + mbk.TradingSymbol + "," + mbk.High + "," + mbk.Entry + "," + mbk.LTP + "," + mbk.Exit + "," + mbk.StopLoss);

            }

            if (mbk.Movement == Utilities.UpwardMovement)
            {
                if (mbk.Entry != null && mbk.Exit == null)
                {
                    decimal secondEntryUpperLimit = Math.Round((decimal)(mbk.Entry * UserConfiguration.EntryPercentage), 1, MidpointRounding.ToEven);

                    if (mbk.LTP >= mbk.Entry && mbk.LTP <= secondEntryUpperLimit)
                    {
                        mbk.EntryTime = DateTime.Now;

                        mbk.Exit = Math.Round((decimal)(mbk.Entry * UserConfiguration.ExitPercentage), 1, MidpointRounding.ToEven);

                        if (mbk.CandleSize > stopLossCandleSize)
                        {
                            mbk.StopLoss = Math.Round((decimal)(mbk.Entry / UserConfiguration.StopLossPercentage), 1, MidpointRounding.ToEven);
                        }
                        else
                        {
                            mbk.StopLoss = Math.Round((decimal)(mbk.Low), 1, MidpointRounding.ToEven);
                        }

                        Logger.LogToFile("Upward-EXSL," + mbk.TradingSymbol + "," + mbk.High + "," + mbk.Entry + "," + mbk.LTP + "," + mbk.Exit + "," + mbk.StopLoss);

                        PlaceOrder(mbk);

                    }
                }

                if (mbk.Exit != null && mbk.ExitTime == null && mbk.StopLossHitTime == null)
                {
                    if (mbk.LTP >= mbk.Exit)
                    {
                        mbk.ExitTime = DateTime.Now;

                        mbk.ProfitLoss = ((mbk.Exit - mbk.Entry) * mbk.Quantity);

                        Logger.LogToFile("Upward-Profit," + mbk.TradingSymbol + "," + mbk.High + "," + mbk.Entry + "," + mbk.LTP + "," + mbk.Exit + "," + mbk.StopLoss + "," + mbk.ProfitLoss);
                    }

                    if (mbk.LTP <= mbk.StopLoss)
                    {
                        mbk.StopLossHitTime = DateTime.Now;

                        mbk.ProfitLoss = ((mbk.StopLoss - mbk.Entry) * mbk.Quantity);

                        Logger.LogToFile("Upward-Loss," + mbk.TradingSymbol + "," + mbk.High + "," + mbk.Entry + "," + mbk.LTP + "," + mbk.Exit + "," + mbk.StopLoss + "," + mbk.ProfitLoss);
                    }
                }

                if (mbk.ExitTime != null || mbk.StopLossHitTime != null)
                {
                    //unsubscribe the token
                    uint[] unsubscribeTokens = { Convert.ToUInt32(mbk.InstrumentToken) };

                    ticker.UnSubscribe(unsubscribeTokens);
                }
            }
        }

        private void DownwardMovement(MorningBreakOutStrategyDM mbk)
        {
            if ((mbk.Movement != null && mbk.Movement == Utilities.UpwardMovement)) /*|| rbUpTrend.Checked || rbSideways.Checked*/
                return;

            if (mbk.LTP != null && mbk.LTP <= Math.Round((decimal)(mbk.Low / UserConfiguration.EntryPercentage), 1) && mbk.Entry == null && mbk.Movement == null)
            {
                mbk.Entry = Math.Round((decimal)(mbk.Low / UserConfiguration.EntryPercentage), 1, MidpointRounding.ToEven);

                mbk.Quantity = Convert.ToInt32(Math.Round((decimal)(UserConfiguration.TotalPurchaseValue / mbk.Entry), 0));

                mbk.Movement = Utilities.DownwardMovement;

                Logger.LogToFile("Downward," + mbk.TradingSymbol + "," + mbk.Low + "," + mbk.Entry + "," + mbk.LTP + "," + mbk.Exit + "," + mbk.StopLoss);
            }

            if (mbk.Movement == Utilities.DownwardMovement)
            {
                if (mbk.Entry != null && mbk.Exit == null)
                {
                    decimal secondEntryLimit = Math.Round((decimal)(mbk.Entry / UserConfiguration.EntryPercentage), 1, MidpointRounding.ToEven);

                    if (mbk.LTP <= mbk.Entry && mbk.LTP >= secondEntryLimit)
                    {
                        mbk.EntryTime = DateTime.Now;

                        mbk.Exit = Math.Round((decimal)(mbk.Entry / UserConfiguration.ExitPercentage), 1, MidpointRounding.ToEven);

                        if (mbk.CandleSize > stopLossCandleSize)
                        {
                            mbk.StopLoss = Math.Round((decimal)(mbk.Entry * UserConfiguration.StopLossPercentage), 1, MidpointRounding.ToEven);
                        }
                        else
                        {
                            mbk.StopLoss = Math.Round((decimal)(mbk.High), 1, MidpointRounding.ToEven);
                        }


                        Logger.LogToFile("Downward-EXSL," + mbk.TradingSymbol + "," + mbk.Low + "," + mbk.Entry + "," + mbk.LTP + "," + mbk.Exit + "," + mbk.StopLoss);

                        PlaceOrder(mbk);
                    }
                }

                if (mbk.Exit != null && mbk.ExitTime == null && mbk.StopLossHitTime == null)
                {
                    if (mbk.LTP <= mbk.Exit)
                    {
                        mbk.ExitTime = DateTime.Now;

                        mbk.ProfitLoss = ((mbk.Entry - mbk.Exit) * mbk.Quantity);

                        Logger.LogToFile("Downward-Profit," + mbk.TradingSymbol + "," + mbk.Low + "," + mbk.Entry + "," + mbk.LTP + "," + mbk.Exit + "," + mbk.StopLoss + "," + mbk.ProfitLoss);
                    }

                    if (mbk.LTP >= mbk.StopLoss)
                    {
                        mbk.StopLossHitTime = DateTime.Now;

                        mbk.ProfitLoss = ((mbk.Entry - mbk.StopLoss) * mbk.Quantity);

                        Logger.LogToFile("Downward-Loss," + mbk.TradingSymbol + "," + mbk.Low + "," + mbk.Entry + "," + mbk.LTP + "," + mbk.Exit + "," + mbk.StopLoss + "," + mbk.ProfitLoss);
                    }
                }

                if (mbk.ExitTime != null || mbk.StopLossHitTime != null)
                {
                    //unsubscribe the token
                    uint[] unsubscribeTokens = { Convert.ToUInt32(mbk.InstrumentToken) };

                    ticker.UnSubscribe(unsubscribeTokens);
                }
            }
        }

        private void CalculateTargetSLTSL(MorningBreakOutStrategyDM mbk, out decimal entry, out decimal target, out decimal stopLoss, out int trailingStopLoss)
        {
            decimal originalTarget = 0.0M;
            decimal originalStopLoss = 0.0M;

            entry = 0.0M;
            target = 0;
            stopLoss = 0;
            trailingStopLoss = 0;

            if (mbk.Movement == Utilities.UpwardMovement)
            {
                if (mbk.Entry != null)
                    entry = (decimal)mbk.LTP;
                else
                    entry = Math.Round((decimal)(mbk.LTP * UserConfiguration.EntryPercentage * UserConfiguration.EntryPercentage), 1);

                originalTarget = Math.Round((decimal)(entry * UserConfiguration.ExitPercentage), 1);

                if (mbk.CandleSize > stopLossCandleSize)
                    originalStopLoss = Math.Round((decimal)(entry / UserConfiguration.StopLossPercentage), 1);
                else
                    originalStopLoss = Math.Round((decimal)(mbk.Low), 1);

                target = Math.Round(originalTarget - entry, 1);
                stopLoss = Math.Round(entry - originalStopLoss, 1);
                trailingStopLoss = Int32.Parse(Math.Round((originalTarget - entry) / 3, 0).ToString());

            }
            else if (mbk.Movement == Utilities.DownwardMovement)
            {
                if (mbk.Entry != null)
                    entry = (decimal)mbk.LTP;
                else
                    entry = Math.Round((decimal)(mbk.LTP / UserConfiguration.EntryPercentage / UserConfiguration.EntryPercentage), 1);

                originalTarget = Math.Round((decimal)(entry / UserConfiguration.ExitPercentage), 1);

                if (mbk.CandleSize > stopLossCandleSize)
                    originalStopLoss = Math.Round((decimal)(entry * UserConfiguration.StopLossPercentage), 1);
                else
                    originalStopLoss = Math.Round((decimal)(mbk.High), 1);

                target = Math.Round((entry - originalTarget), 1);
                stopLoss = Math.Round((originalStopLoss - entry), 1);
                trailingStopLoss = Int32.Parse(Math.Round((entry - originalTarget) / 3, 0).ToString());

            }
        }

        private int CalculateQuantity(MorningBreakOutStrategyDM mbk)
        {
            int quantity = 0;

            int capital = UserConfiguration.Capital;
            int maxOrders = UserConfiguration.MaxOrders;
            int bracketMargin = UserConfiguration.BracketMargin;

            double marginPerOrder = Math.Round((double)(capital / maxOrders), 0);
            double allocationPerOrder = marginPerOrder * bracketMargin;

            quantity = Convert.ToInt32(Math.Round((decimal)(Convert.ToDecimal(allocationPerOrder) / mbk.LTP), 0));

            return quantity;
        }

        private bool isMarginAvailable()
        {
            bool isMarginAvailable = true;

            // Margin Logic should be written here. Right now handling manually

            //int maxOrders = uc.MaxOrders;

            //int cntOpenOrders = dbMethods.GetOrderCount(Utilities.OS_Open);

            //if ((maxOrders - cntOpenOrders) >= 1)
            //{
            //    isMarginAvailable = true;
            //}

            return isMarginAvailable;
        }

        private void PlaceOrder(MorningBreakOutStrategyDM mbk)
        {
            decimal entry = 0.0M;
            decimal target = 0;
            decimal stopLoss = 0;
            int trailingStopLoss = 0;
            int quantity = 0;
            string BuyOrSell = string.Empty;

            CalculateTargetSLTSL(mbk, out entry, out target, out stopLoss, out trailingStopLoss);

            quantity = CalculateQuantity(mbk);

            if (mbk.Movement == Utilities.UpwardMovement)
                BuyOrSell = Utilities.BuyOrder;
            else if (mbk.Movement == Utilities.DownwardMovement)
                BuyOrSell = Utilities.SellOrder;

            if (!string.IsNullOrEmpty(BuyOrSell) && isMarginAvailable())
            {
                Logger.LogToFile("PlaceBO," + mbk.TradingSymbol + "," + BuyOrSell + "," + entry.ToString() + "," + target.ToString() + "," + stopLoss.ToString() + "," + trailingStopLoss.ToString() + "," + quantity.ToString());

                if (placeOrders)
                {
                    try
                    {
                        if (mbk.isRealOrderPlaced == null || (bool)!mbk.isRealOrderPlaced)
                        {
                            //Dictionary<string, dynamic> response = kite.PlaceOrder(mbk.TradingSymbol.Split(':')[0], mbk.TradingSymbol.Split(':')[1],
                            //        BuyOrSell, quantity.ToString(), Price: entry.ToString(), OrderType: Utilities.LimitOrder, Product: Utilities.NRMLProduct, Variety: "bo",
                            //        SquareOffValue: target.ToString(), StoplossValue: stopLoss.ToString(), TrailingStoploss: trailingStopLoss.ToString(),
                            //        Validity: Utilities.OrderValidity);

                            Dictionary<string, dynamic> response = kite.PlaceOrder(
                                        Exchange: mbk.TradingSymbol.Split(':')[0], 
                                        TradingSymbol: mbk.TradingSymbol.Split(':')[1],
                                        TransactionType: BuyOrSell, 
                                        Quantity: quantity, 
                                        Price: entry, 
                                        OrderType: Constants.ORDER_TYPE_LIMIT, 
                                        Product: Constants.PRODUCT_NRML, 
                                        Variety: Constants.VARIETY_BO, 
                                        SquareOffValue: target, 
                                        StoplossValue: stopLoss, 
                                        TrailingStoploss: trailingStopLoss,
                                        Validity: Constants.VALIDITY_DAY);

                            if (response != null && response["status"] == "success")
                            {
                                Logger.LogToFile(response["data"]["order_id"]);
                            }

                            mbk.isRealOrderPlaced = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.ErrorLogToFile(ex);
                    }

                    Thread.Sleep(500);
                }
            }

        }

        private string[] PickInstrumentListForSubscription()
        {
            string[] insListForMBKs = (from mbk in morningBreakouts
                                       select mbk.InstrumentToken.ToString()).ToArray();

            return insListForMBKs;
        }

        private void CalculateStopLossCandleSize()
        {
            stopLossCandleSize = (UserConfiguration.StopLossPercentage - 1) * 100;
        }

        private void SubscribeAndLoadTickers()
        {
            //morningBreakouts = dbMethods.GetAllMorningBreakouts();

            insListForSubscription = PickInstrumentListForSubscription();

            //ticker.OnTick += this.onSubscriptionTick;

            //ticker.Subscribe(Tokens: insListForSubscription);
            //ticker.SetMode(Tokens: insListForSubscription, Mode: "ltp");

        }

        private void LoadLabels()
        {
            string profit = string.Empty;
            string loss = string.Empty;
            string profitloss = string.Empty;

            int totalStocks = 0;
            int executedStocks = 0;
            int enteredStocks = 0;


            lblProfit.Text = profit = morningBreakoutsForSimulation.Where(script => script.ExitTime != null).Sum(script => script.ProfitLoss).ToString();
            lblLoss.Text = loss = morningBreakoutsForSimulation.Where(script => script.StopLossHitTime != null).Sum(script => script.ProfitLoss).ToString();
            lblProfitLoss.Text = (Convert.ToDecimal(profit) + Convert.ToDecimal(loss)).ToString();

            totalStocks = morningBreakoutsForSimulation.Count();
            executedStocks = morningBreakoutsForSimulation.Where(script => script.ExitTime != null || script.StopLossHitTime != null).Count();
            enteredStocks = morningBreakoutsForSimulation.Where(script => script.EntryTime != null).Count();

            lblExecuted.Text = executedStocks.ToString();
            lblPending.Text = (enteredStocks - executedStocks).ToString();
            lblScriptNotEntered.Text = (totalStocks - enteredStocks).ToString();

            int cntOpenOrders = dbMethods.GetOrderCount(Utilities.OS_Open);
            int cntCompletedOrders = dbMethods.GetOrderCount(Utilities.OS_Complete);
            int cntOpenParentOrders = dbMethods.GetOrderCount(Utilities.OS_Open, true);
            int cntCompletedParentOrders = dbMethods.GetOrderCount(Utilities.OS_Complete, true);
            int cntOpenChildOrders = dbMethods.GetOrderCount(Utilities.OS_Open, false, true);
            int cntCompletedChildOrders = dbMethods.GetOrderCount(Utilities.OS_Complete, false, true);

            lblOpenOrders.Text = cntOpenOrders.ToString();
            lblOpenEntries.Text = cntOpenParentOrders.ToString();
            lblOpenPositions.Text = cntOpenChildOrders.ToString();

            lblExecutedOrders.Text = cntCompletedOrders.ToString();
            lblEntriesExecuted.Text = cntCompletedParentOrders.ToString();
            lblClosedPositions.Text = cntCompletedChildOrders.ToString();

        }

        private void ToggleRealTimeOrderPlacement()
        {
            placeOrders = !placeOrders;

            if (placeOrders)
                btnToggleOrderPlacement.Text = Utilities.RealTimeOrderPlacementON;
            else
                btnToggleOrderPlacement.Text = Utilities.RealTimeOrderPlacementOFF;
        }

        private void lblCoverOrder_Click(object sender, EventArgs e)
        {
            List<KiteConnect.Order> orders = kite.GetOrders();

            List<KiteConnect.Order> childOrders = orders.Where(ord => ord.ParentOrderId != null && ord.Status == Utilities.OS_TriggerPending).ToList();

            foreach (KiteConnect.Order order in childOrders)
            {
                Dictionary<string, LTP> ltp = kite.GetLTP(new string[] { order.Tradingsymbol });

                Dictionary<string, dynamic> response = kite.ModifyOrder(
                        OrderId: order.OrderId, 
                        ParentOrderId: order.ParentOrderId,
                        TradingSymbol: order.Tradingsymbol, 
                        TriggerPrice: Math.Round((order.TriggerPrice * 1.0005M), 1),
                        Product: Constants.PRODUCT_NRML, 
                        Variety: Constants.VARIETY_CO);

                if (response != null && response["status"] == "success")
                {
                    MessageBox.Show(response["data"]["order_id"]);
                }
            }


        }

        private void btnSubscribeAll_Click(object sender, EventArgs e)
        {
            uint[] insList = dbMethods.GetAllInstrumentTokenForSubscription().ToArray();

            ticker.OnTick += this.onSubscriptionTick;

            ticker.Subscribe(Tokens: insList);
            ticker.SetMode(Tokens: insList, Mode: Constants.MODE_LTP);
        }
    }

}
