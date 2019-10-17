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
        List<RealTimeGapOpenedScripts_Result> GetRealTimeGapOpenedScripts();
    }

    public class DBMethods : IDBMethods
    {
        private readonly IConfigSettings _configSettings;

        public DBMethods(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        public List<RealTimeGapOpenedScripts_Result> GetRealTimeGapOpenedScripts()
        {
            List<RealTimeGapOpenedScripts_Result> currentGapOpenedStocks = new List<RealTimeGapOpenedScripts_Result>();

            DateTime yesterday = _configSettings.PreviousTradeDay;
            DateTime today = DateTime.Today.AddHours(9).AddMinutes(15);
            double targetPercentage = _configSettings.TargetPercentage;
            double gapPercentage = _configSettings.GapPercentage;
            int priceRangeHigh = _configSettings.PriceRangeHigh;
            int priceRangeLow = _configSettings.PriceRangeLow;

            using (aztgsqldbEntities db = new aztgsqldbEntities())
            {
                var result = db.RealTimeGapOpenedScripts()
            }

            return currentGapOpenedStocks;
        }
    }
}
