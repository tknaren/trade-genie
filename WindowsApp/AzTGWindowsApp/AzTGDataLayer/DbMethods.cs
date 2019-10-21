using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using AzTGDataLayer.Models;
using System.Linq;

namespace AzTGDataLayer
{
    public class DbMethods
    {
        public bool InsertUserLoginInfo(string clientId, string accessCode, string accessToken)
        {
            int recCount = 0;
            int recCountStage = 0;
            bool retStatus = false;

            using (var db = new aztgsqldbContext())
            {
                db.UserLogins.Add(new UserLogins {
                    ClientId = clientId,
                    Broker = "Upstox",
                    AccessToken = accessToken,
                    RequestToken = accessCode,
                    LoginDateTime = DateTime.Now,
                    LogoutDateTime = DateTime.Now,
                    Status = "IN"
                });

                recCount = db.SaveChanges();
            }

            using (var db = new aztgsqldbContextStage())
            {
                db.UserLogins.Add(new UserLogins
                {
                    ClientId = clientId,
                    Broker = "Upstox",
                    AccessToken = accessToken,
                    RequestToken = accessCode,
                    LoginDateTime = DateTime.Now,
                    LogoutDateTime = DateTime.Now,
                    Status = "IN"
                });

                recCountStage = db.SaveChanges();
            }

            if (recCount > 0 && recCountStage > 0)
                retStatus = true;

            return retStatus;
        }

        public bool InsertUserLoggedOutInfo(string accessToken)
        {
            int recCount = 0;
            int recCountStage = 0;
            bool retStatus = false;

            using (var db = new aztgsqldbContext())
            {
                var query = db.UserLogins.Where(a => a.AccessToken == accessToken).FirstOrDefault();
                query.LogoutDateTime = DateTime.Now;
                query.Status = "OUT";

                recCount = db.SaveChanges();
            }

            using (var db = new aztgsqldbContextStage())
            {
                var query = db.UserLogins.Where(a => a.AccessToken == accessToken).FirstOrDefault();
                query.LogoutDateTime = DateTime.Now;
                query.Status = "OUT";

                recCountStage = db.SaveChanges();
            }

            if (recCount > 0 && recCountStage > 0)
                retStatus = true;

            return retStatus;
        }

    }
}
