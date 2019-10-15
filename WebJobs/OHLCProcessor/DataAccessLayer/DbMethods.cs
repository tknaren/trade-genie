using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Utilities;
using Serilog;
using Newtonsoft;
using Newtonsoft.Json;

namespace DataAccessLayer
{
    public interface IDBMethods
    {
        string GetLatestAccessToken();

        List<MasterStockList> GetMasterStockList();

        void InsertTickerDataTable(TickerMinDataTable dtTicker);

        void InsertTickerDataTable(TickerMinDataTable dtTicker, bool downloadDayHistory);

        void GenerateOHLC(string dateTime, int minuteBar);

        List<TickerElderIndicatorsModel> GetTickerDataForIndicators(string instrumentList, string timePeriodList);

        void UpdateTickerElderDataTable(DataTable dtTickerElderData);

        void BulkUploadHistoryToDB(IList<TickerMin> tickerData);

        void BulkUploadElderDataToDB(IList<TickerMinElderIndicator> tickerElderData);

        void BulkUploadSuperTrendDataToDB(IList<TickerMinSuperTrend> tickerSuperTrendData);

        void BulkUploadEMAHADataToDB(IList<TickerMinEMAHA> tickerEMAHAData);

        void MergeTickerData();

        List<TickerMin> GetLatestTickerData(string instrumentList, DateTime today);

        List<TickerMin> GetTickerDataForConsolidation();
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

            using (aztgsqldbEntities db = new aztgsqldbEntities())
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

            using (aztgsqldbEntities db = new aztgsqldbEntities())
            {
                var masterStockList = from msl in db.MasterStockLists
                                      where msl.IsIncluded == true
                                      select msl;

                mslist = masterStockList.ToList();
            }

            return mslist;
        }

        public void InsertTickerDataTable(TickerMinDataTable dtTicker)
        {
            string commandName = "spUpdateTicker";

            using (SqlConnection sqlConn = new SqlConnection(_configSettings.AzSQLConString))
            {
                using (SqlCommand sqlComm = new SqlCommand(commandName))
                {
                    sqlComm.Connection = sqlConn;
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.CommandTimeout = 240;

                    SqlParameter tblParam = new SqlParameter("@tblTicker", SqlDbType.Structured);
                    tblParam.Value = dtTicker;

                    sqlComm.Parameters.Add(tblParam);

                    sqlConn.Open();
                    int ret = sqlComm.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
        }

        public void InsertTickerDataTable(TickerMinDataTable dtTicker, bool downloadDayHistory)
        {
            string commandName = "spUpdateTickerElder";

            using (SqlConnection sqlConn = new SqlConnection(_configSettings.AzSQLConString))
            {
                using (SqlCommand sqlComm = new SqlCommand(commandName))
                {
                    sqlComm.Connection = sqlConn;
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.CommandTimeout = 240;

                    SqlParameter tblParam = new SqlParameter("@tblTicker", SqlDbType.Structured);
                    tblParam.Value = dtTicker;

                    SqlParameter tpParam = new SqlParameter("@timePeriod", SqlDbType.Int);
                    tpParam.Value = 375;

                    sqlComm.Parameters.Add(tblParam);
                    sqlComm.Parameters.Add(tpParam);

                    sqlConn.Open();
                    int ret = sqlComm.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
        }

        public void MergeTickerData()
        {
            string commandName = "spMergeTicker";

            using (SqlConnection sqlConn = new SqlConnection(_configSettings.AzSQLConString))
            {
                using (SqlCommand sqlComm = new SqlCommand(commandName))
                {
                    sqlComm.Connection = sqlConn;
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.CommandTimeout = 600;

                    sqlConn.Open();
                    int ret = sqlComm.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
        }

        public void BulkUploadHistoryToDB(IList<TickerMin> tickerData)
        {
            //string output = JsonConvert.SerializeObject(tickerData);

            //Log.Information("TickerData {@tickerData}", tickerData);

            var objBulk = new SQLBulkUpload<TickerMin>()
            {
                InternalStore = tickerData,
                TableName = "TickerMin",
                CommitBatchSize = _configSettings.BulkCommitBatchSize,
                ConnectionString = _configSettings.AzSQLConString
            };

            objBulk.Commit();
        }

        public void BulkUploadElderDataToDB(IList<TickerMinElderIndicator> tickerElderData)
        {
            //string output = JsonConvert.SerializeObject(tickerElderData);

            //Log.Information("TickerData - {output}", output);

            //Log.Verbose<IList<TickerMinElderIndicator>>("{tickerElderData}", tickerElderData);

            var objBulk = new SQLBulkUpload<TickerMinElderIndicator>()
            {
                InternalStore = tickerElderData,
                TableName = "TickerMinElderIndicators",
                CommitBatchSize = _configSettings.BulkCommitBatchSize,
                ConnectionString = _configSettings.AzSQLConString
            };

            objBulk.Commit();
        }

        public void BulkUploadSuperTrendDataToDB(IList<TickerMinSuperTrend> tickerSuperTrendData)
        {
            var objBulk = new SQLBulkUpload<TickerMinSuperTrend>()
            {
                InternalStore = tickerSuperTrendData,
                TableName = "TickerMinSuperTrend",
                CommitBatchSize = _configSettings.BulkCommitBatchSize,
                ConnectionString = _configSettings.AzSQLConString
            };

            objBulk.Commit();
        }

        public void BulkUploadEMAHADataToDB(IList<TickerMinEMAHA> tickerEMAHAData)
        {
            var objBulk = new SQLBulkUpload<TickerMinEMAHA>()
            {
                InternalStore = tickerEMAHAData,
                TableName = "TickerMinEMAHA",
                CommitBatchSize = _configSettings.BulkCommitBatchSize,
                ConnectionString = _configSettings.AzSQLConString
            };

            objBulk.Commit();
        }

        public void GenerateOHLC(string dateTime, int minuteBar)
        {
            string commandText = "EXEC spGenerateOHLC '" + dateTime + "', " + minuteBar.ToString();

            using (SqlConnection sqlConn = new SqlConnection(_configSettings.AzSQLConString))
            {
                using (SqlCommand sqlComm = new SqlCommand(commandText))
                {
                    sqlComm.Connection = sqlConn;

                    sqlConn.Open();

                    int ret = sqlComm.ExecuteNonQuery();

                    sqlConn.Close();
                }
            }
        }

        public List<TickerMin> GetLatestTickerData(string instrumentList, DateTime today)
        {
            List<TickerMin> latestTickerData = new List<TickerMin>();

            using (aztgsqldbEntities db = new aztgsqldbEntities())
            {
                var result = db.spGetTickerLatestData(instrumentList, today);

                foreach(spGetTickerLatestData_Result singleItem in result)
                {
                    TickerMin tickerMin = new TickerMin {
                        InstrumentToken = singleItem.InstrumentToken,
                        TradingSymbol = singleItem.TradingSymbol,
                        DateTime = singleItem.DateTime,
                        Open = singleItem.Open,
                        High = singleItem.High,
                        Low = singleItem.Low,
                        Close = singleItem.Close,
                        Volume = singleItem.Volume
                    };

                    latestTickerData.Add(tickerMin);
                }
            }

            return latestTickerData;
        }

        public List<TickerElderIndicatorsModel> GetTickerDataForIndicators(string instrumentList, string timePeriodList)
        {
            List<spGetTickerDataForIndicators_Result> tickerResult = new List<spGetTickerDataForIndicators_Result>();

            List<TickerElderIndicatorsModel> tickerElderData = new List<TickerElderIndicatorsModel>();

            using (aztgsqldbEntities db = new aztgsqldbEntities())
            {
                tickerResult = db.spGetTickerDataForIndicators(instrumentList, timePeriodList, DateTime.Today).ToList();
                //tickerResult = db.spGetTickerDataForIndicators(instrumentList, timePeriodList, _configSettings.IndicatorLoadDateFrom).ToList();

                foreach (spGetTickerDataForIndicators_Result tickItem in tickerResult)
                {
                    TickerElderIndicatorsModel model = new TickerElderIndicatorsModel();

                    model.AG1 = tickItem.AG1;
                    model.AG2 = tickItem.AG2;
                    model.AL1 = tickItem.AL1;
                    model.AL2 = tickItem.AL2;
                    model.EMA1 = tickItem.EMA1;
                    model.EMA2 = tickItem.EMA2;
                    model.EMA3 = tickItem.EMA3;
                    model.EMA4 = tickItem.EMA4;
                    model.EMA1Dev = tickItem.EMA1Dev;
                    model.EMA2Dev = tickItem.EMA2Dev;
                    model.ForceIndex1 = tickItem.ForceIndex1;
                    model.ForceIndex2 = tickItem.ForceIndex2;
                    model.Histogram = tickItem.Histogram;
                    model.HistIncDec = tickItem.HistIncDec;
                    model.Impulse = tickItem.Impulse;
                    model.MACD = tickItem.MACD;
                    model.PriceClose = tickItem.PriceClose;
                    model.PriceHigh = tickItem.PriceHigh;
                    model.PriceLow = tickItem.PriceLow;
                    model.PriceOpen = tickItem.PriceOpen;
                    model.RSI1 = tickItem.RSI1;
                    model.RSI2 = tickItem.RSI2;
                    model.Signal = tickItem.Signal;
                    model.StockCode = tickItem.StockCode;
                    model.TickerDateTime = tickItem.TickerDateTime;
                    model.TimePeriod = tickItem.TimePeriod;
                    model.Volume = tickItem.Volume;
                    model.Change = tickItem.Change;
                    model.ChangePercent = tickItem.ChangePercent;
                    model.TradedValue = tickItem.TradedValue;
                    model.TrueRange = tickItem.TrueRange;
                    model.ATR1 = tickItem.ATR1;
                    model.ATR2 = tickItem.ATR2;
                    model.ATR3 = tickItem.ATR3;
                    model.BUB1 = tickItem.BUB1;
                    model.BUB2 = tickItem.BUB2;
                    model.BUB3 = tickItem.BUB3;
                    model.BLB1 = tickItem.BLB1;
                    model.BLB2 = tickItem.BLB2;
                    model.BLB3 = tickItem.BLB3;
                    model.FUB1 = tickItem.FUB1;
                    model.FUB2 = tickItem.FUB2;
                    model.FUB3 = tickItem.FUB3;
                    model.FLB1 = tickItem.FLB1;
                    model.FLB2 = tickItem.FLB2;
                    model.FLB3 = tickItem.FLB3;
                    model.ST1 = tickItem.ST1;
                    model.ST2 = tickItem.ST2;
                    model.ST3 = tickItem.ST3;
                    model.Trend1 = tickItem.Trend1;
                    model.Trend2 = tickItem.Trend2;
                    model.Trend3 = tickItem.Trend3;

                    model.EHEMA1 = tickItem.EHEMA1;
                    model.EHEMA2 = tickItem.EHEMA2;
                    model.EHEMA3 = tickItem.EHEMA3;
                    model.EHEMA4 = tickItem.EHEMA4;
                    model.EHEMA5 = tickItem.EHEMA5;
                    model.VWMA1 = tickItem.VWMA1;
                    model.VWMA2 = tickItem.VWMA2;
                    model.HAOpen = tickItem.HAOpen;
                    model.HAHigh = tickItem.HAHigh;
                    model.HALow = tickItem.HALow;
                    model.HAClose = tickItem.HAClose;
                    model.varEMA1v2 = tickItem.varEMA1v2;
                    model.varEMA1v3 = tickItem.varEMA1v3;
                    model.varEMA1v4 = tickItem.varEMA1v4;
                    model.varEMA2v3 = tickItem.varEMA2v3;
                    model.varEMA2v4 = tickItem.varEMA2v4;
                    model.varEMA3v4 = tickItem.varEMA3v4;
                    model.varEMA4v5 = tickItem.varEMA4v5;
                    model.varVWMA1vVWMA2 = tickItem.varVWMA1vVWMA2;
                    model.varVWMA1vPriceClose = tickItem.varVWMA1vPriceClose;
                    model.varVWMA2vPriceClose = tickItem.varVWMA2vPriceClose;
                    model.varVWMA1vEMA1 = tickItem.varVWMA1vEMA1;
                    model.varHAOvHAC = tickItem.varHAOvHAC;
                    model.varHAOvHAPO = tickItem.varHAOvHAPO;
                    model.varHACvHAPC = tickItem.varHACvHAPC;
                    model.varOvC = tickItem.varOvC;
                    model.varOvPO = tickItem.varOvPO;
                    model.varCvPC = tickItem.varCvPC;
                    model.HAOCwEMA1 = tickItem.HAOCwEMA1;
                    model.OCwEMA1 = tickItem.OCwEMA1;
                    model.AllEMAsInNum = tickItem.AllEMAsInNum;


                    tickerElderData.Add(model);
                }
            }

            return tickerElderData;
        }

        public void UpdateTickerElderDataTable(DataTable dtTickerElderData)
        {
            using (SqlConnection sqlConn = new SqlConnection(_configSettings.AzSQLConString))
            {
                using (SqlCommand sqlComm = new SqlCommand("spUpdateTickerElderIndicators"))
                {
                    sqlComm.Connection = sqlConn;
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.CommandTimeout = 600;

                    SqlParameter tblParam = new SqlParameter("@tblTickerElder", SqlDbType.Structured);
                    tblParam.Value = dtTickerElderData;

                    sqlComm.Parameters.Add(tblParam);

                    sqlConn.Open();
                    int ret = sqlComm.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
        }

        public List<TickerMin> GetTickerDataForConsolidation()
        {
            List<TickerMin> tickerDataForConsolidation = new List<TickerMin>();

            using (aztgsqldbEntities db = new aztgsqldbEntities())
            {
                //tickerDataForConsolidation = (from tkr in db.TickerMins
                //                             where tkr.TradingSymbol == tradingSymbol
                //                                && tkr.DateTime > DateTime.Today
                //                             select tkr).ToList<TickerMin>();

                tickerDataForConsolidation = (from tkr in db.TickerMins
                                                  where tkr.DateTime > DateTime.Today
                                              //where tkr.DateTime > _configSettings.IndicatorLoadDateFrom
                                              select tkr).ToList<TickerMin>();
            }

            return tickerDataForConsolidation;
        }
    }
}
