using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IDBMethods
    {
        List<TickerMinElderIndicator> GetConsolidatedData(DateTime startDate, DateTime endDate, string stockCode, int timePeriod);
        List<MasterStockList> GetMasterStockList();
        List<TickerMin> GetMinuteData(DateTime startDate, DateTime endDate, string stockCode);
        List<spGetGapOpenedScripts_Result> GetStocksWithGapOpening(DateTime yesterday, DateTime today, int targetPercentage, int gapPercentage, int priceRangeHigh, int priceRangeLow);
        void InsertGapStrategyOrders(List<GapStrategyPotentialOrder> potentialOrders);
    }

    public class DBMethods : IDBMethods
    {
        public List<MasterStockList> GetMasterStockList()
        {
            List<MasterStockList> masterStocks = new List<MasterStockList>();

            using (TGEntities tgEntities = new TGEntities())
            {

                var masterStockList = from msl in tgEntities.MasterStockLists
                                      where msl.IsIncluded == true
                                      select msl;

                masterStocks = masterStockList.ToList();
            }

            return masterStocks;
        }

        public List<TickerMin> GetMinuteData(DateTime startDate, DateTime endDate, string stockCode)
        {
            List<TickerMin> minDatalist = new List<TickerMin>();

            using (TGEntities tgEntities = new TGEntities())
            {
                var tickerMinList = from tm in tgEntities.TickerMins
                                    where tm.TradingSymbol == stockCode
                                          && tm.DateTime >= startDate
                                          && tm.DateTime <= endDate
                                    orderby tm.DateTime ascending
                                    select tm;

                minDatalist = tickerMinList.ToList();
            }

            return minDatalist;
        }

        public List<TickerMinElderIndicator> GetConsolidatedData(DateTime startDate, DateTime endDate, string stockCode, int timePeriod)
        {
            List<TickerMinElderIndicator> elderDatalist = new List<TickerMinElderIndicator>();

            using (TGEntities tgEntities = new TGEntities())
            {
                var tickerMinList = from tm in tgEntities.TickerMinElderIndicators
                                    where tm.StockCode == stockCode
                                          && tm.TickerDateTime >= startDate
                                          && tm.TickerDateTime <= endDate
                                          && tm.TimePeriod == timePeriod
                                    orderby tm.TickerDateTime ascending
                                    select tm;

                elderDatalist = tickerMinList.ToList();
            }

            return elderDatalist;
        }

        public List<spGetGapOpenedScripts_Result> GetStocksWithGapOpening(DateTime yesterday, DateTime today, 
                                        int targetPercentage, int gapPercentage, int priceRangeHigh, int priceRangeLow)
        {
            List<spGetGapOpenedScripts_Result> listOfStocks = new List<spGetGapOpenedScripts_Result>();

            using (TGEntities tgEntities = new TGEntities())
            {
                listOfStocks = tgEntities.spGetGapOpenedScripts(yesterday, today, targetPercentage, gapPercentage, priceRangeHigh, priceRangeLow).ToList<spGetGapOpenedScripts_Result>();
            }

            return listOfStocks;
        }

        public void InsertGapStrategyOrders(List<GapStrategyPotentialOrder> potentialOrders)
        {
            using (TGEntities tGEntities = new TGEntities())
            {
                foreach(GapStrategyPotentialOrder potentialOrder in  potentialOrders)
                {
                    tGEntities.GapStrategyPotentialOrders.Add(potentialOrder);
                }

                tGEntities.SaveChanges();
            }
        }
    }
}
