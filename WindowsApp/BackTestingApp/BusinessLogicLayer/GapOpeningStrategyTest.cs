using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Models;
using Utilities;
using Serilog;

namespace BusinessLogicLayer
{
    public enum GapType
    {
        GapUp,
        GapDown
    }

    public class GapOpeningStrategyTest
    {
        IDBMethods _dbmethods;
        IConfigSettings _config;

        List<MasterStockList> mslList = new List<MasterStockList>();
        List<GapStrategyPotentialOrder> orderBook = new List<GapStrategyPotentialOrder>();

        public GapOpeningStrategyTest(IDBMethods dbmethods, IConfigSettings config)
        {
            _dbmethods = dbmethods;
            _config = config;
        }

        public List<BackTestDate> GetBackTestDates()
        {
            List<BackTestDate> backTestDateCollection = new List<BackTestDate>()
            {
                new BackTestDate { Yesterday = DateTime.Parse("2019-09-20"), Today = DateTime.Parse("2019-09-23") },
                new BackTestDate { Yesterday = DateTime.Parse("2019-09-23"), Today = DateTime.Parse("2019-09-24") },
                new BackTestDate { Yesterday = DateTime.Parse("2019-09-24"), Today = DateTime.Parse("2019-09-25") },
                new BackTestDate { Yesterday = DateTime.Parse("2019-09-25"), Today = DateTime.Parse("2019-09-26") },
                new BackTestDate { Yesterday = DateTime.Parse("2019-09-26"), Today = DateTime.Parse("2019-09-27") }
                
            };

            return backTestDateCollection;
        }

        public void RunGapOpeningStrategyTest()
        {
            try
            {
                List<BackTestDate> backTestDateCol = GetBackTestDates();

                Log.Information("started");

                foreach (BackTestDate backTestDate in backTestDateCol)
                {
                    List<spGetGapOpenedScripts_Result> gapOpenedScripts =
                        _dbmethods.GetStocksWithGapOpening(backTestDate.Yesterday, backTestDate.Today, 
                        _config.TargetPercentage, _config.GapPercentage, _config.PriceRangeHigh, _config.PriceRangeLow);

                    foreach (spGetGapOpenedScripts_Result potentialScript in gapOpenedScripts)
                    {
                        //Write the strategy for the checking if the order can be placed 
                        //1.  Determine if it is Gap-Up or Gap-Down
                        //2.  Check if the 1st candle has not closed the gap
                        //3.  Check if the Open of 2nd candle is less/greater than based on the GapUp / GapDown 
                        //4.  If Gapup - the Open of 2nd candle should not be greater than the 1st candle.
                        //5.  If GapDown - the Open of 2nd candle should not be lesser than the 1st candle.
                        //6.  If condition satisfies, place the order in the 3rd candle open
                        //7.  Stop Loss has to be placed @0.5% of the buy price
                        //8.  Target has to be @1% of the buy price
                        //9.  Loop thru the Min data and check if the order has hit Target or SL
                        //10. If the time breaches 12 pm, close the position.

                        GapStrategyPotentialOrder potentialOrder = new GapStrategyPotentialOrder();

                        potentialOrder.StockCode = potentialScript.StockCode;
                        potentialOrder.Collection = potentialScript.Collection;
                        potentialOrder.Yesterday = potentialScript.Yesterday;
                        potentialOrder.YesterdayClose = potentialScript.PriceClose;
                        potentialOrder.Today = potentialScript.Today;
                        potentialOrder.TodayOpen = potentialScript.PriceOpen;
                        potentialOrder.GapPer = potentialScript.GapPer;

                        if (potentialScript.GapPer > 0)
                        {
                            HandleGapUpScripts(backTestDate, potentialScript, potentialOrder);
                        }
                        else
                        {
                            HandleGapDownScripts(backTestDate, potentialScript, potentialOrder);
                        }

                        orderBook.Add(potentialOrder);

                    }

                }

                _dbmethods.InsertGapStrategyOrders(orderBook);
            }
            catch(Exception ex)
            {
                Log.Error(ex, "GapOpeningStrategyTest Error");
            }
        }

        public void HandleGapUpScripts(BackTestDate backTestDate, spGetGapOpenedScripts_Result potentialScript, GapStrategyPotentialOrder potentialOrder)
        {
            List<TickerMin> todayMinuteData = _dbmethods.GetMinuteData(backTestDate.Today, backTestDate.Today.AddDays(1), potentialScript.StockCode);
            //List<TickerMin> yesterdayMinuteData = _dbmethods.GetMinuteData(backTestDate.Yesterday, backTestDate.Today, potentialScript.StockCode);

            bool isEligibleScript = false;

            //Process only if the entire data for the day is present
            bool isInHold = orderBook.Any(ord => ord.StockCode == potentialScript.StockCode &&
                    (((DateTime)ord.BuyTime).Day == backTestDate.Today.Day || ((DateTime)ord.SellTime).Day == backTestDate.Today.Day));

            if (!isInHold)
            {
                if (todayMinuteData.Count > 350)
                {
                    if (!DidFirstCandleCloseTheGap(todayMinuteData.First<TickerMin>(), potentialScript, GapType.GapUp))
                    {
                        isEligibleScript = true;
                    }
                    else
                    {
                        potentialOrder.Position = "FAILED-DidFirstCandleCloseTheGap";
                    }
                }
                else
                {
                    potentialOrder.Position = "FAILED-NoMinuteData";
                }

                if (isEligibleScript)
                {
                    PlaceAndTrackOrder(backTestDate.Today, todayMinuteData, potentialScript, potentialOrder, GapType.GapUp);
                }
            }

        }

        public void HandleGapDownScripts(BackTestDate backTestDate, spGetGapOpenedScripts_Result potentialScript, GapStrategyPotentialOrder potentialOrder)
        {
            List<TickerMin> todayMinuteData = _dbmethods.GetMinuteData(backTestDate.Today, backTestDate.Today.AddDays(1), potentialScript.StockCode);
            //List<TickerMin> yesterdayMinuteData = _dbmethods.GetMinuteData(backTestDate.Yesterday, backTestDate.Today, potentialScript.StockCode);

            bool isEligibleScript = false;

            //Process only if the entire data for the day is present
            bool isInHold = orderBook.Any(ord => ord.StockCode == potentialScript.StockCode &&
                    (((DateTime)ord.BuyTime).Day == backTestDate.Today.Day || ((DateTime)ord.SellTime).Day == backTestDate.Today.Day));

            if (!isInHold)
            {
                if (todayMinuteData.Count > 350)
                {
                    if (!DidFirstCandleCloseTheGap(todayMinuteData.First<TickerMin>(), potentialScript, GapType.GapDown))
                    {
                        isEligibleScript = true;
                    }
                    else
                    {
                        potentialOrder.Position = "FAILED-DidFirstCandleCloseTheGap";
                    }
                }
                else
                {
                    potentialOrder.Position = "FAILED-NoMinuteData";
                }

                if (isEligibleScript)
                {
                    PlaceAndTrackOrder(backTestDate.Today, todayMinuteData, potentialScript, potentialOrder, GapType.GapDown);
                }
            }
        }

        private bool DidFirstCandleCloseTheGap(TickerMin firstItem, spGetGapOpenedScripts_Result potentialScript, GapType gapType)
        {
            bool isGapClosed = false;

            decimal priceClose = Convert.ToDecimal(potentialScript.PriceClose);

            //Calculate the fill percentage of First Candle from previous day close
            if (gapType == GapType.GapUp)
            {
                if (priceClose > firstItem.Low)
                {
                    isGapClosed = true;
                }
            }
            else
            {
                if (firstItem.High > priceClose)
                {
                    isGapClosed = true;
                }
            }

            return isGapClosed;

        }

        private bool DoesSubsequentCandleFollowTheTrend(TickerMin firstItem, TickerMin secondItem, GapType gapType)
        {
            bool isTrendFollowed = false;

            if (gapType == GapType.GapUp)
            {
                if (secondItem.Close < firstItem.Close)
                {
                    isTrendFollowed = true;
                }
            }
            else
            {
                if (secondItem.Close > firstItem.Close)
                {
                    isTrendFollowed = true;
                }
            }

            return isTrendFollowed;
        }

        private void CalculateTargetAndSL(GapType gapType, decimal close, decimal open, out decimal target, out decimal stopLoss)
        {
            //calculate the target and stop loss based on the Risk Reward Ratio
            target = 0;
            stopLoss = 0;

            if (gapType == GapType.GapUp)
            {
                decimal percentDiff = ((open - close) / close) * 100;

                //Calculate 80% / 85% of the difference and set as target
                decimal targetPercentage = (decimal)(Convert.ToInt32(percentDiff) * 0.85);

                target = open - (open * (targetPercentage / 100));

                stopLoss = open + (open * ((targetPercentage / 2) / 100));

            }
            else
            {
                decimal percentDiff = ((close - open) / close) * 100;

                //Calculate 80% / 85% of the difference and set as target
                decimal targetPercentage = (decimal)(Convert.ToInt32(percentDiff) * 0.85);

                target = open + (open * (targetPercentage / 100));

                stopLoss = open - (open * ((targetPercentage / 2) / 100));
            }
        }

        private void PlaceAndTrackOrder(DateTime today, List<TickerMin> todayMinuteData, 
                                        spGetGapOpenedScripts_Result potentialScript, GapStrategyPotentialOrder order,
                                        GapType gapType)
        {
            int iCtr = 0;
            int iCapitalAmount = 100000;
            decimal iTarget = 0;
            decimal iStopLoss = 0;
            //int iMISBrok = 100;

            foreach (TickerMin item in todayMinuteData)
            {
                //Place the order in candle 4 open

                bool isInHold = orderBook.Any(ord => ord.StockCode == potentialScript.StockCode &&
                     (((DateTime)ord.BuyTime).Day == today.Day || ((DateTime)ord.SellTime).Day == today.Day));

                if (!isInHold)
                {
                    if (iCtr == 2)
                    {
                        if (gapType == GapType.GapUp)
                        {
                            //GapStrategyOrder order = new GapStrategyOrder();

                            order.StockCode = item.TradingSymbol;
                            order.Position = "SHORT";

                            order.BuyPrice = (double)item.Open;
                            order.BuyTime = item.DateTime;
                            //order.StopLoss = Math.Round((double)order.BuyPrice * iStopLoss, 1);
                            //order.Target = Math.Round((double)order.BuyPrice / iTarget, 1);

                            CalculateTargetAndSL(GapType.GapUp, 
                                (decimal)potentialScript.PriceClose, (decimal)potentialScript.PriceOpen, 
                                out iTarget, out iStopLoss);

                            order.StopLoss = (double)iStopLoss;
                            order.Target = (double)iTarget;

                            order.Quantity = Convert.ToInt32(Math.Round(Convert.ToDouble(iCapitalAmount / order.BuyPrice), 0));
                            order.MaxProfitAchieved = 0;
                            order.MaxProfitCanBeAchieved = 0;

                            orderBook.Add(order);
                        }
                        else
                        {
                            //GapStrategyOrder order = new GapStrategyOrder();

                            order.StockCode = item.TradingSymbol;
                            order.Position = "LONG";

                            order.BuyPrice = (double)item.Open;
                            order.BuyTime = item.DateTime;
                            //order.StopLoss = Math.Round((double)order.BuyPrice / (double)iStopLoss, 1);
                            //order.Target = Math.Round((double)order.BuyPrice * (double)iTarget, 1);

                            CalculateTargetAndSL(GapType.GapDown,
                                (decimal)potentialScript.PriceClose, (decimal)potentialScript.PriceOpen,
                                out iTarget, out iStopLoss);

                            order.StopLoss = (double)iStopLoss;
                            order.Target = (double)iTarget;

                            order.Quantity = Convert.ToInt32(Math.Round(Convert.ToDouble(iCapitalAmount / order.BuyPrice), 0));
                            order.MaxProfitAchieved = 0;
                            order.MaxProfitCanBeAchieved = 0;

                            orderBook.Add(order);
                        }
                    }
                }
                else
                {
                    GapStrategyPotentialOrder orderInPosition =
                        orderBook.Where(con => con.SellPrice == null 
                        && con.StockCode == item.TradingSymbol).SingleOrDefault();

                    double buyPrice = orderInPosition.BuyPrice != null ? (double)orderInPosition.BuyPrice : 0;
                    double sellPrice = orderInPosition.SellPrice != null ? (double)orderInPosition.SellPrice : 0;
                    double open = item.Open != null ? (double)item.Open : 0;
                    double high = item.High != null ? (double)item.High : 0;
                    double low = item.Low != null ? (double)item.Low : 0;
                    double close = item.Close != null ? (double)item.Close : 0;
                    int quantity = orderInPosition.Quantity != null ? (int)orderInPosition.Quantity : 0;

                    if (orderInPosition != null)
                    {
                        if (gapType == GapType.GapUp)
                        {
                            if (item.High >= (decimal)orderInPosition.StopLoss)
                            {
                                orderInPosition.SellPrice = orderInPosition.StopLoss;
                                sellPrice = (double)orderInPosition.StopLoss;
                                orderInPosition.StopLossTime = item.DateTime;
                                orderInPosition.SellTime = item.DateTime;
                                orderInPosition.PNL = Math.Round((buyPrice - sellPrice) * quantity, 2);

                                break;
                            }
                            else if (item.Low <= (decimal)orderInPosition.Target)
                            {
                                orderInPosition.SellPrice = orderInPosition.Target;
                                sellPrice = (double)orderInPosition.Target;
                                orderInPosition.SellTime = item.DateTime;
                                orderInPosition.PNL = Math.Round((buyPrice - sellPrice) * quantity, 2);

                                break;
                            }
                            else if (item.DateTime.Hour == 12 &&
                                (item.DateTime.Minute >= 00 && item.DateTime.Minute <= 5))
                            {
                                orderInPosition.SellPrice = (double)item.Close;
                                sellPrice = (double)item.Close;
                                orderInPosition.SellTime = item.DateTime;
                                orderInPosition.PNL = Math.Round((buyPrice - sellPrice) * quantity, 2);

                                break;
                            }
                            else
                            {
                                orderInPosition.PNL = Math.Round((close - buyPrice) * quantity, 2);

                                double tmpMaxProfitAchived = Math.Round((low - buyPrice) * quantity, 2);

                                if (orderInPosition.MaxProfitAchieved != null && orderInPosition.PNL > orderInPosition.MaxProfitAchieved)
                                {
                                    orderInPosition.MaxProfitAchieved = orderInPosition.PNL;
                                }

                                if (orderInPosition.MaxProfitCanBeAchieved != null && tmpMaxProfitAchived > orderInPosition.MaxProfitCanBeAchieved)
                                {
                                    orderInPosition.MaxProfitCanBeAchieved = tmpMaxProfitAchived;
                                }
                            }
                        }
                        else
                        {
                            if (item.Low <= (decimal)orderInPosition.StopLoss)
                            {
                                orderInPosition.SellPrice = orderInPosition.StopLoss;
                                sellPrice = (double)orderInPosition.StopLoss;
                                orderInPosition.StopLossTime = item.DateTime;
                                orderInPosition.SellTime = item.DateTime;
                                orderInPosition.PNL = Math.Round((sellPrice - buyPrice) * quantity, 2);

                                break;
                            }
                            else if (item.High >= (decimal)orderInPosition.Target)
                            {
                                orderInPosition.SellPrice = orderInPosition.Target;
                                sellPrice = (double)orderInPosition.Target;
                                orderInPosition.SellTime = item.DateTime;
                                orderInPosition.PNL = Math.Round((sellPrice - buyPrice) * quantity, 2);

                                break;
                            }
                            else if (item.DateTime.Hour == 12 &&
                                (item.DateTime.Minute >= 00 && item.DateTime.Minute <= 5))
                            {
                                orderInPosition.SellPrice = (double)item.Close;
                                sellPrice = (double)item.Close;
                                orderInPosition.SellTime = item.DateTime;
                                orderInPosition.PNL = Math.Round((sellPrice - buyPrice) * quantity, 2);

                                break;
                            }
                            else
                            {
                                orderInPosition.PNL = Math.Round((close - buyPrice) * quantity, 2);

                                double tmpMaxProfitAchived = Math.Round((high - buyPrice) * quantity, 2);

                                if (orderInPosition.MaxProfitAchieved != null && orderInPosition.PNL > orderInPosition.MaxProfitAchieved)
                                {
                                    orderInPosition.MaxProfitAchieved = orderInPosition.PNL;
                                }

                                if (orderInPosition.MaxProfitCanBeAchieved != null && tmpMaxProfitAchived > orderInPosition.MaxProfitCanBeAchieved)
                                {
                                    orderInPosition.MaxProfitCanBeAchieved = tmpMaxProfitAchived;
                                }
                            }
                        }
                    }
                }

                iCtr++;
            }

            
        }

    }
}
