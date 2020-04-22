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
        private readonly IKiteConnectInterface _kiteConnectInterface;
        private readonly IDBMethods _dBMethods;
        private readonly object tickerLock = new object();

        private static string accessToken;
        private static string requestToken;

        private TickerMinDataTable dtHistoryData;
        private IList<TickerMin> tickerData;
        private IDictionary<string, Historical> histories;
        private IList<TickerMin> tickerLatestMins;

        public bool IsUserLoggedIn { get { if (!string.IsNullOrEmpty(accessToken)) return true; else return false; } }

        public HistoryLoaderEngine(IConfigSettings settings, IUpstoxInterface upstoxInterface, IKiteConnectInterface kiteConnectInterface, IDBMethods dBMethods)
        {
            _settings = settings;
            _upstoxInterface = upstoxInterface;
            _kiteConnectInterface = kiteConnectInterface;
            _dBMethods = dBMethods;
            dtHistoryData = new TickerMinDataTable();
            tickerData = new List<TickerMin>();
            histories = new Dictionary<string, Historical>();
        }

        public void LoadHistory(bool downloadDayHistory = false)
        {
            //Kite or upstox switch
            if (_settings.Platform.Equals(PLATFORM.KITE))
            {
                DownloadHistoryFromKite(downloadDayHistory);
            }
            else if (_settings.Platform.Equals(PLATFORM.UPSTOX))
            {
                DownloadHistoryFromUpstox(downloadDayHistory);
            }

            if (downloadDayHistory)
            {

            }

            //UploadHistoryToDB(downloadDayHistory);
        }

        private void DownloadHistoryFromKite(bool downloadDayHistory = false)
        {
            //Get the history from the Kite using history download
            //download the 1 minute data and the 

            List<MasterStockList> masterStockLists = null;

            if (string.IsNullOrEmpty(accessToken))
            {
                dynamic response = _dBMethods.GetLatestAccessToken();

                accessToken = response.GetType().GetProperty("AccessToken").GetValue(response, null);
                requestToken = response.GetType().GetProperty("RequestToken").GetValue(response, null);
            }

            _kiteConnectInterface.InitializeKiteConnect(requestToken, accessToken);

            if (!string.IsNullOrEmpty(accessToken))
            {
                masterStockLists = _dBMethods.GetMasterStockList();

                string instrumentList = string.Join(",", from item in masterStockLists select item.TradingSymbol);

                List<Task> taskList = new List<Task>();

                Log.Information("Parallel threads started to get Stock History");

                //Task lastHistoryEntry = Task.Run(() => GetLatestTickerMins(instrumentList));
                //taskList.Add(lastHistoryEntry);

                GetLatestTickerMins(instrumentList);

                int batchSize = _settings.HistoryAPICallBatchSize;
                int numberOfPages = (masterStockLists.Count / batchSize) + (masterStockLists.Count % batchSize == 0 ? 0 : 1);

                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    foreach (MasterStockList stock in masterStockLists.Skip(pageIndex * batchSize).Take(batchSize))
                    {
                        //Task stockHistoryTask = Task.Run(() => GetIndividualStockHistory(stock.InstrumentToken, stock.TradingSymbol));
                        //taskList.Add(stockHistoryTask);
                        GetIndividualStockHistory(stock.InstrumentToken, stock.TradingSymbol);
                    }

                    Log.Information("Waiting for batch-" + (pageIndex + 1).ToString() + " tasks to complete");

                    Task.WaitAll(taskList.ToArray());

                    Log.Information("Batch-" + (pageIndex + 1).ToString() + " tasks completed");

                    taskList.Clear();
                }
                
            }

            Log.Information("History fetch completed");

            UploadHistoryToDB();

            Log.Information("History Load completed");

        }

        private void DownloadHistoryFromUpstox(bool downloadDayHistory = false)
        {
            List<MasterStockList> masterStockLists = null;

            if (string.IsNullOrEmpty(accessToken))
            {
                accessToken = _dBMethods.GetLatestAccessToken();
            }

            if (!string.IsNullOrEmpty(accessToken))
            {
                masterStockLists = _dBMethods.GetMasterStockList();

                string instrumentList = string.Join(",", from item in masterStockLists select item.TradingSymbol);

                #region Current Logic of getting the history sequentially

                //_dBMethods.GetLatestTickerData(instrumentList, DateTime.Today));

                //foreach (MasterStockList stock in masterStockLists)
                //{
                //    try
                //    {
                //        string uri = _upstoxInterface.BuildHistoryUri(stock.TradingSymbol, downloadDayHistory);

                //        Historical historial = _upstoxInterface.GetHistory(accessToken, uri);

                //        AddToTickerDataTable(stock.InstrumentToken, stock.TradingSymbol, historial);

                //        UploadHistoryToDB(downloadDayHistory);

                //        //if (iCtr >= 20)
                //        //{
                //        //    UploadHistoryToDB(downloadDayHistory);

                //        //    iCtr = 0;
                //        //}
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

                //Task retLatestTickDataTask = Task.Run(() => GetLatestTickerMins(instrumentList));
                //taskList.Add(retLatestTickDataTask);

                Log.Information("Parallel threads started to get Stock History");

                Task lastHistoryEntry = Task.Run(() => GetLatestTickerMins(instrumentList));
                taskList.Add(lastHistoryEntry);

                int batchSize = _settings.HistoryAPICallBatchSize;
                int numberOfPages = (masterStockLists.Count / batchSize) + (masterStockLists.Count % batchSize == 0 ? 0 : 1);

                for (int pageIndex = 0; pageIndex < numberOfPages; pageIndex++)
                {
                    foreach (MasterStockList stock in masterStockLists.Skip(pageIndex * batchSize).Take(batchSize))
                    {
                        Task stockHistoryTask = Task.Run(() => GetIndividualStockHistory(stock.InstrumentToken, stock.TradingSymbol));
                        taskList.Add(stockHistoryTask);
                    }

                    //Log.Information("Waiting for batch-" + (pageIndex + 1).ToString() + " tasks to complete");

                    Task.WaitAll(taskList.ToArray());

                    Log.Information("Batch-" + (pageIndex + 1).ToString() + " tasks completed");

                    taskList.Clear();
                }

                //enumerate history dictionary
                foreach (KeyValuePair<string, Historical> entry in histories)
                {
                    int instrumentToken = Convert.ToInt32(entry.Key.Split(',')[0]);
                    string tradingSymbol = entry.Key.Split(',')[1].ToString();

                    AddToTickerDataTable(instrumentToken, tradingSymbol, entry.Value);
                }

                //Log.Information("Load To DB START");

                UploadHistoryToDB();

                Log.Information("Data loaded To DB");

                histories.Clear();

                #endregion

            }
            else
            {
                Log.Information("Access token empty - Please login");
            }
        }

        private void GetLatestTickerMins(string instrumentList)
        {
            tickerLatestMins = _dBMethods.GetLatestTickerData(instrumentList, DateTime.Today);
        }

        private void GetIndividualStockHistory(int instrumentToken, string tradingSymbol)
        {
            try
            {
                if (_settings.Platform.Equals(PLATFORM.UPSTOX))
                {

                    string historyKey = string.Join(",", instrumentToken.ToString(), tradingSymbol);

                    string uri = _upstoxInterface.BuildHistoryUri(tradingSymbol);

                    Historical historial = _upstoxInterface.GetHistory(accessToken, uri);

                    histories.Add(new KeyValuePair<string, Historical>(historyKey, historial));

                }
                else if(_settings.Platform.Equals(PLATFORM.KITE))
                {
                    TickerMin tickerMin = null;
                    //DateTime fromDateTime = DateTime.Today.AddDays(-1);
                    DateTime fromDateTime = DateTime.Today;

                    if (tickerLatestMins.Count > 0)
                    {
                        tickerMin = (from tick in tickerLatestMins
                                 where tick.TradingSymbol == tradingSymbol
                                 select tick).FirstOrDefault();

                        if (tickerMin != null && tickerMin.DateTime != null)
                        {
                            fromDateTime = tickerMin.DateTime.AddMinutes(_settings.KiteInterval.ToMinute());
                        }
                    }

                    List<KiteConnect.Historical> historical = _kiteConnectInterface.GetHistory(instrumentToken, fromDateTime, 1);

                    foreach (KiteConnect.Historical history in historical)
                    {
                        tickerData.Add(new TickerMin
                        {
                            InstrumentToken = instrumentToken,
                            TradingSymbol = tradingSymbol,
                            DateTime = history.TimeStamp.ToIndianTimeStamp(),
                            Open = history.Open,
                            High = history.High,
                            Low = history.Low,
                            Close = history.Close,
                            Volume = Int32.Parse(history.Volume.ToString())
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Download History Exception " + tradingSymbol);
            }
        }

        private void AddToTickerDataTable(int instrumentToken, string tradingSymbol, Historical historical)
        {
            decimal open, high, low, close;
            int volume;
            DateTime ohlcDateTime;

            if (historical != null && historical.data != null)
            {
                var query = (from tick in tickerLatestMins
                             where tick.TradingSymbol == tradingSymbol
                             select tick).FirstOrDefault();

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

                        if (query == null || (query != null && ohlcDateTime > query.DateTime))
                        {
                            //dtHistoryData.AddRow(
                            //        instrumentToken,
                            //        tradingSymbol,
                            //        ohlcDateTime,
                            //        open,
                            //        high,
                            //        low,
                            //        close,
                            //        volume);


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
                        }
                    }
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

            //_dBMethods.MergeTickerData();
            tickerData.Clear();
        }

        #endregion
    }
}
