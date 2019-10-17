using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataAccessLayer
{
    public interface IDBMethods
    {
        List<GapOpenedScript> GetRealTimeGapOpenedScripts();
    }

    public class DBMethods : IDBMethods
    {
        private readonly IConfigSettings _configSettings;

        public DBMethods(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        public List<GapOpenedScript> GetRealTimeGapOpenedScripts()
        {
            List<GapOpenedScript> currentGapOpenedStocks = new List<GapOpenedScript>();

            DateTime yesterday = _configSettings.PreviousTradeDay;
            DateTime today = DateTime.Today.AddHours(9).AddMinutes(15);
            double targetPercentage = _configSettings.TargetPercentage;
            double gapPercentage = _configSettings.GapPercentage;
            int priceRangeHigh = _configSettings.PriceRangeHigh;
            int priceRangeLow = _configSettings.PriceRangeLow;

            using (aztgsqldbEntities db = new aztgsqldbEntities())
            {
                var result = db.RealTimeGapOpenedScripts(yesterday, today, targetPercentage, gapPercentage, priceRangeHigh, priceRangeLow);

                foreach(RealTimeGapOpenedScripts_Result item in result)
                {
                    currentGapOpenedStocks.Add(new GapOpenedScript
                    {
                        TradingSymbol = item.TradingSymbol,
                        Index = item.NiftyIndex,
                        GapPer = (double)item.GapPer,
                        TradedValue = (decimal)item.TradedValue,
                        Yesterday = (DateTime)item.Yesterday,
                        YesterdayClose = (double)item.YesterdayClose,
                        YesterdayHL = (double)item.YesterdayHL,
                        Today = (DateTime)item.Today,
                        TodayOpen = (double)item.TodayOpen,
                        TodayHL = (double)item.TodayHL,
                        OrderType = item.OrderType
                    });
                }
            }

            return currentGapOpenedStocks;
        }
    }
}
