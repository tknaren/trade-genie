using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreLayer;

namespace DataAccessLayer
{
    public class DBMethods : IDBMethods
    {
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
    }
}
