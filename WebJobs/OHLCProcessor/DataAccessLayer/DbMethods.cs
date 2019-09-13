using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Utilities;

namespace DataAccessLayer
{
    public interface IDBMethods
    {
        string GetLatestAccessToken();

        List<MasterStockList> GetMasterStockList();

        void InsertTickerDataTable(TickerMinDataTable dtTicker);
    }

    public class DBMethods : IDBMethods
    {
        IConfigSettings _configSettings;
        public DBMethods(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }

        public string GetLatestAccessToken()
        {
            string accessToken = string.Empty;
            DateTime logInDateTime = new DateTime();
            string status = string.Empty;

            try
            {
                using (aztgsqldbEntities db = new aztgsqldbEntities())
                {
                    var latestLogin = db.UserLogins.Where(a => a.Status == "IN").OrderByDescending(b => b.LoginDateTime).FirstOrDefault();

                    accessToken = latestLogin.AccessToken;
                    logInDateTime = latestLogin.LoginDateTime;
                    status = latestLogin.Status;
                }

                Console.WriteLine("Access Token: {0}, Logged In Time: {1}, Status: {2}", accessToken, logInDateTime, status);

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: {0}, InnerException: {1}, StackTrace: {2}",
                    ex.Message, ex.InnerException?.Message, ex.StackTrace);

                throw ex;
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
            using (SqlConnection sqlConn = new SqlConnection(_configSettings.AzSQLConString))
            {
                using (SqlCommand sqlComm = new SqlCommand("spUpdateTicker"))
                {
                    sqlComm.Connection = sqlConn;
                    sqlComm.CommandType = CommandType.StoredProcedure;
                    sqlComm.CommandTimeout = 240;
                    //sqlComm.Parameters.AddWithValue("@tblTicker", dtTicker);

                    SqlParameter tblParam = new SqlParameter("@tblTicker", SqlDbType.Structured);
                    tblParam.Value = dtTicker;

                    sqlComm.Parameters.Add(tblParam);

                    sqlConn.Open();
                    int ret = sqlComm.ExecuteNonQuery();
                    sqlConn.Close();
                }
            }
        }
    }
}
