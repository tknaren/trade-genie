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
        void LoadHistory(bool downloadDayHistory = false);
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
        private readonly object tickerLock = new object();
        private readonly IList<TickerMin> tickerData; 
        private static string accessToken;

        public bool IsUserLoggedIn { get { if (!string.IsNullOrEmpty(accessToken)) return true; else return false; } }

        public HistoryLoaderEngine(IConfigSettings settings, IUpstoxInterface upstoxInterface, IDBMethods dBMethods)
        {
            _settings = settings;
            _upstoxInterface = upstoxInterface;
            _dBMethods = dBMethods;
            dtHistoryData = new TickerMinDataTable();
            tickerData = new List<TickerMin>();
        }

        public void LoadHistory(bool downloadDayHistory = false)
        {
            DownloadHistory(downloadDayHistory);

            //UploadHistoryToDB(downloadDayHistory);
        }

        private void DownloadHistory(bool downloadDayHistory = false)
        {
            int iCtr = 0;

            List<MasterStockList> masterStockLists = null;

            if (string.IsNullOrEmpty(accessToken))
            {
                accessToken = _dBMethods.GetLatestAccessToken();
            }

            if (!string.IsNullOrEmpty(accessToken))
            {
                masterStockLists = _dBMethods.GetMasterStockList();

                #region Current Logic of getting the history sequentially

                //foreach (MasterStockList stock in masterStockLists)
                //{
                //    try
                //    {
                //        string uri = _upstoxInterface.BuildHistoryUri(stock.TradingSymbol, downloadDayHistory);

                //        Historical historial = _upstoxInterface.GetHistory(accessToken, uri);

                //        AddToTickerDataTable(stock.InstrumentToken, stock.TradingSymbol, historial);

                //        if (iCtr >= 20)
                //        {
                //            UploadHistoryToDB(downloadDayHistory);

                //            iCtr = 0;
                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        Log.Error(ex, "Download History Exception " + stock.TradingSymbol);
                //    }

                //    iCtr++;
                //}

                //if (iCtr > 1 && dtHistoryData.Rows.Count > 1)
                //{
                //    UploadHistoryToDB(downloadDayHistory);
                //}

                #endregion

                #region New Logic for getting the history simultaneously and loading to DB

                List<Task> taskList = new List<Task>();

                foreach (MasterStockList stock in masterStockLists)
                {
                    Task stockHistoryTask = Task.Run(() => GetIndividualStockHistory(stock.InstrumentToken, stock.TradingSymbol));
                    taskList.Add(stockHistoryTask);
                }

                Log.Information("Waiting for all tasks to complete");

                Task.WaitAll(taskList.ToArray());

                Log.Information("Load To DB START");

                UploadHistoryToDB();

                Log.Information("Load To DB END");

                #endregion

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

        private void UploadHistoryToDB(bool downloadDayHistory = false)
        {
            if (!downloadDayHistory)
            {
                _dBMethods.InsertTickerDataTable(dtHistoryData);
            }
            else
            {
                _dBMethods.InsertTickerDataTable(dtHistoryData, downloadDayHistory);
            }

            dtHistoryData.Clear();
        }

        #region Section for handling simultaneuous threads

        private void GetIndividualStockHistory(int instrumentToken, string tradingSymbol)
        {
            string uri = _upstoxInterface.BuildHistoryUri(tradingSymbol);

            Historical historial = _upstoxInterface.GetHistory(accessToken, uri);

            Log.Information("History retreived for " + tradingSymbol);

            AddToTickerObject(instrumentToken, tradingSymbol, historial);
        }

        private void AddToTickerObject(int instrumentToken, string tradingSymbol, Historical historical)
        {
            decimal open, high, low, close;
            int volume;
            DateTime ohlcDateTime;
            //TickerMinDataTable tickerMinDataTable = new TickerMinDataTable();

            if (historical != null && historical.data != null)
            {
                lock (tickerLock)
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

                        tickerData.Add(new TickerMin
                        {
                            InstrumentToken = instrumentToken,
                            TradingSymbol = tradingSymbol,
                            DateTime = ohlcDateTime,
                            Open = open,
                            High = high,
                            Low = low,
                            Close = close,
                            Volume = volume
                        });

                        //tickerMinDataTable.AddRow(
                        //        instrumentToken,
                        //        tradingSymbol,
                        //        ohlcDateTime,
                        //        open,
                        //        high,
                        //        low,
                        //        close,
                        //        volume);

                    }
                }
            }
            else
            {
                throw new Exception("History not fetched for " + tradingSymbol);
            }
        }

        private void UploadHistoryToDB()
        {
            _dBMethods.BulkUploadHistoryToDB(tickerData);

            _dBMethods.MergeTickerData();
        }

        #endregion
    }
}
