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
        List<GapOpenedScripts> GetRealTimeGapOpenedScripts();
    }

    public class DBMethods : IDBMethods
    {
        private readonly IConfigSettings _configSettings;

        public DBMethods(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        public List<GapOpenedScripts> GetRealTimeGapOpenedScripts()
        {
            List<GapOpenedScripts> currentGapOpenedStocks = new List<GapOpenedScripts>();

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
                    currentGapOpenedStocks.Add(new GapOpenedScripts
                    {
                        TradingSymbol = item.TradingSymbol,
                        Collection = item.Collection,
                        GapPer = (double)item.GapPer,
                        Open = (double)item.Open,
                        PriceClose = (double)item.PriceClose,
                        Today = (DateTime)item.Today,
                        Yesterday = (DateTime)item.Yesterday

                    });
                }
            }

            return currentGapOpenedStocks;
        }
    }
}
