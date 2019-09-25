using APIInterfaceLayer;
using APIInterfaceLayer.Models;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using DataAccessLayer.Models;
using Serilog;
using Serilog.Exceptions;

namespace BusinessLogicLayer
{

    public interface IHistoryLoaderEngine
    {
        bool IsUserLoggedIn { get; }
        void LoadHistory();
    }

    /// <summary>
    /// Get Latest Access Token from DB
    /// Set Access Token and login to Upstox
    /// Get Master Stock Info
    /// Get Config info for downlading history
    /// Get History from Upstox
    /// Format and store in DB
    /// </summary>
    public class HistoryLoaderEngine : IHistoryLoaderEngine
    {
        private readonly IConfigSettings _settings;
        private readonly IUpstoxInterface _upstoxInterface;
        private readonly IDBMethods _dBMethods;
        private readonly TickerMinDataTable dtHistoryData;
        private static string accessToken;

        public bool IsUserLoggedIn { get { if (string.IsNullOrEmpty(accessToken)) return true; else return false; } }

        public HistoryLoaderEngine(IConfigSettings settings, IUpstoxInterface upstoxInterface, IDBMethods dBMethods)
        {
            _settings = settings;
            _upstoxInterface = upstoxInterface;
            _dBMethods = dBMethods;
            dtHistoryData = new TickerMinDataTable();
        }

        public void LoadHistory()
        {
            DownloadHistory();

            UploadHistoryToDB();
        }

        private void DownloadHistory()
        {
            
            //bool isSuccessfulLogin = false;
            //bool isAccessTokenSuccessfullySet = false;
            List<MasterStockList> masterStockLists = null;

            //if (!_upstoxInterface.IsLoggedIn)
            //{
            //    accessToken = _dBMethods.GetLatestAccessToken();

            //    if (!string.IsNullOrEmpty(accessToken))
            //    {
            //        isSuccessfulLogin = _upstoxInterface.InitializeUpstox(_settings.APIKey, _settings.APISecret, _settings.RedirectUrl);

            //        if (isSuccessfulLogin)
            //            isAccessTokenSuccessfullySet = _upstoxInterface.SetUpstoxAccessToken(accessToken);

            //        Log.Information("Is Login Successful: {0}", isAccessTokenSuccessfullySet.ToString());
            //    }
            //}
            //else
            //{
            //    Log.Information("Access Token: {0}", _upstoxInterface.AccessToken);
            //    Log.Information("Is Logged In: {0}", _upstoxInterface.IsLoggedIn.ToString());
            //}

            if (string.IsNullOrEmpty(accessToken))
            {
                accessToken = _dBMethods.GetLatestAccessToken();
            }

            if (!string.IsNullOrEmpty(accessToken))
            {
                masterStockLists = _dBMethods.GetMasterStockList();

                foreach (MasterStockList stock in masterStockLists)
                {
                    try
                    {
                        string uri = _upstoxInterface.BuildHistoryUri(stock.TradingSymbol);

                        Historical historial = _upstoxInterface.GetHistory(accessToken, uri);

                        AddToTickerDataTable(stock.InstrumentToken, stock.TradingSymbol, historial);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Download History Exception " + stock.TradingSymbol);
                    }
                }
            }
            else
            {
                Log.Information("Access token empty - Please login");
            }
        }

        private void AddToTickerDataTable(int instrumentToken, string tradingSymbol, Historical historical)
        {
            decimal open, high, low, close;
            int volume;
            DateTime ohlcDateTime;

            if (historical != null && historical.data != null)
            {
                foreach (string historyItem in historical.data)
                {
                    string[] ohlcArray = historyItem.Split(',');

                    ohlcDateTime = AuxiliaryMethods.ConvertUnixTimeStampToWindows(ohlcArray[0]);
                    open = Convert.ToDecimal(ohlcArray[1]);
                    high = Convert.ToDecimal(ohlcArray[2]);
                    low = Convert.ToDecimal(ohlcArray[3]);
                    close = Convert.ToDecimal(ohlcArray[4]);
                    volume = Convert.ToInt32(ohlcArray[5]);

                    dtHistoryData.AddRow(
                            instrumentToken,
                            tradingSymbol,
                            ohlcDateTime,
                            open,
                            high,
                            low,
                            close,
                            volume);

                }
            }
            else
            {
                throw new Exception("History not fetched for " + tradingSymbol);
            }
        }

        private void UploadHistoryToDB()
        {
            _dBMethods.InsertTickerDataTable(dtHistoryData);
        }
    }
}
