using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Models;
using Utilities;

namespace BusinessLogicLayer
{
    public class GapOpeningStrategyTest
    {
        IDBMethods _dbmethods;
        IConfigSettings _config;

        List<MasterStockList> mslList = new List<MasterStockList>();
        List<GapStrategyOrder> orderBook = new List<GapStrategyOrder>();

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
            };

            return backTestDateCollection;
        }

        public void RunGapOpeningStrategyTest()
        {
            try
            {
                List<BackTestDate> backTestDateCol = GetBackTestDates();

                foreach(BackTestDate backTestDate in backTestDateCol)
                {
                    List<spGetGapOpenedScripts_Result> gapOpenedScripts =
                        _dbmethods.GetStocksWithGapOpening(backTestDate.Yesterday, backTestDate.Today, _config.TargetPercentage, _config.GapPercentage, _config.PriceRangeHigh, _config.PriceRangeLow);

                    foreach(spGetGapOpenedScripts_Result potentialScript in gapOpenedScripts)
                    {
                        List<TickerMin> minuteData = _dbmethods.GetMinuteData(backTestDate.Today, backTestDate.Today.AddDays(1), potentialScript.StockCode);

                        //Process only if the entire data for the day is present
                        if (minuteData.Count == 375)
                        {
                            foreach (TickerMin item in minuteData)
                            {
                                GapStrategyOrder order = new GapStrategyOrder();

                                bool isInHold = orderBook.Any(ord => ord.StockCode == item.TradingSymbol &&
                                     (ord.BuyTime.Day == backTestDate.Today.Day || ord.SellTime.Day == backTestDate.Today.Day));

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


                                orderBook.Add(order);

                            }
                        }
                    }
                }
            }
            catch
            {

            }
        }

    }
}
