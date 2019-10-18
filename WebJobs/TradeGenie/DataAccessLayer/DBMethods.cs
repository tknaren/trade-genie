using DataAccessLayer.Model;
using Serilog;
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
        string GetLatestAccessToken();

        List<GapOpenedScript> GetRealTimeGapOpenedScripts();
    }

    public class DBMethods : IDBMethods
    {
        private readonly IConfigSettings _configSettings;

        public DBMethods(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        public string GetLatestAccessToken()
        {
            string accessToken = string.Empty;
            DateTime logInDateTime = new DateTime();
            DateTime currentDateTime = AuxiliaryMethods.GetCurrentIndianTimeStamp().Date;
            string status = string.Empty;

            using (SQLAZURECONNSTR_aztgsqldbEntities db = new SQLAZURECONNSTR_aztgsqldbEntities())
            {
                var latestLogin = db.UserLogins.Where(a => a.Status == "IN")
                    .Where(b => b.LoginDateTime >= currentDateTime)
                    .FirstOrDefault();

                if (latestLogin != null)
                {
                    accessToken = latestLogin.AccessToken;
                    logInDateTime = latestLogin.LoginDateTime;
                    status = latestLogin.Status;

                    Log.Information("Access Token: {0}, Logged In Time: {1}, Status: {2}", accessToken, logInDateTime, status);
                }
                else
                {
                    Log.Information("Not Logged In for Today - Please Login.");
                }
            }

            return accessToken;
        }

        public List<MasterStockList> GetMasterStockList()
        {
            List<MasterStockList> mslist = null;

            using (SQLAZURECONNSTR_aztgsqldbEntities db = new SQLAZURECONNSTR_aztgsqldbEntities())
            {
                var masterStockList = from msl in db.MasterStockLists
                                      where msl.IsIncluded == true
                                      select msl;

                mslist = masterStockList.ToList();
            }

            return mslist;
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

            using (SQLAZURECONNSTR_aztgsqldbEntities db = new SQLAZURECONNSTR_aztgsqldbEntities())
            {
                var result = db.RealTimeGapOpenedScripts(yesterday, today, targetPercentage, gapPercentage, priceRangeHigh, priceRangeLow);

                //foreach(RealTimeGapOpenedScripts_Result item in result)
                //{
                //    currentGapOpenedStocks.Add(new GapOpenedScript
                //    {
                //        TradingSymbol = item.TradingSymbol,
                //        Index = item.NiftyIndex,
                //        GapPer = (double)item.GapPer,
                //        TradedValue = (decimal)item.TradedValue,
                //        Yesterday = (DateTime)item.Yesterday,
                //        YesterdayClose = (double)item.YesterdayClose,
                //        YesterdayHL = (double)item.YesterdayHL,
                //        Today = (DateTime)item.Today,
                //        TodayOpen = (double)item.TodayOpen,
                //        TodayHL = (double)item.TodayHL,
                //        OrderType = item.OrderType
                //    });
                //}
            }

            return currentGapOpenedStocks;
        }
    }
}
