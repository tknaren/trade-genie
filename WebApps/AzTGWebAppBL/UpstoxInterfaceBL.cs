using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using UpstoxNet;
using AzTGWebAppEntities;

namespace AzTGWebAppBL
{
    public class UpstoxInterfaceBL
    {
        Upstox upstox;
        UpstoxLoginLogoutStatus loginLogoutStatus;

        public UpstoxInterfaceBL()
        {
            upstox = new Upstox();
            loginLogoutStatus = new UpstoxLoginLogoutStatus();
        }

        private void SetAPIKeySecret()
        {
            upstox.Api_Key = "ar8VJeMwIW54Ya4cbvTr48KEP9obrSBd993kvaaC";
            upstox.Api_Secret = "jb5m80ild3";
        }

        public void LoginToUpstox()
        {
            upstox.Login();

            loginLogoutStatus.LoginResponse = upstox.Login_Response;
            loginLogoutStatus.LoginStatus = upstox.Login_Status;
            loginLogoutStatus.LoginURL = upstox.Login_Url;
        }

        public void Logout()
        {
            upstox.Logout();

            loginLogoutStatus.LogoutStatus = upstox.Logout_Status;
        }
    }
}