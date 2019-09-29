using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DBMethods
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
    }
}
