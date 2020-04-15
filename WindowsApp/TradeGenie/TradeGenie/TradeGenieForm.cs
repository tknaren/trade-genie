using KiteConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TradeGenie.Repository;

namespace TradeGenie
{
    public class TradeGenieForm : Form
    {
        public static string requestToken;
        //public static UserConfiguration uc;
        public static Ticker ticker;
        public static Kite kite;
        public static User user;
        public static string StatusMessage;
        public static RepositoryMethods dbMethods;
        public static UserLoginVM loginInfo;

        public static string Nifty50Value;
        public static string NiftyBankValue;
        public static string NiftyMidcapValue;

        public TradeGenieForm()
        {
            if (UserConfiguration.APIKey == null)
                UserConfiguration.LoadUserConfiguration();

            if (loginInfo == null)
                loginInfo = new UserLoginVM();

            if (kite == null)
                kite = new Kite(UserConfiguration.APIKey, Debug: true);    

            if (dbMethods == null)
                dbMethods = new RepositoryMethods();
        }

        //public UserConfiguration UserConfig { get; set; }
        //public string RequestToken { get; set; }

        protected void LoginToKite()
        {
            try
            {
                //user = kite.RequestAccessToken(loginInfo.RequestToken, uc.APISecret);

                kite.SetSessionExpiryHook(onTokenExpire);

                kite.SetAccessToken(UserConfiguration.AccessToken);

                InitializeTicker();
            }
            catch (Exception ex)
            {
                Logger.ErrorLogToFile(ex);
                MessageBox.Show(ex.Message);
            }
        }

        protected void InitializeTicker()
        {
            try
            {
                ticker = new Ticker(UserConfiguration.APIKey, UserConfiguration.UserId, UserConfiguration.PublicToken);

                ticker.OnReconnect += onReconnect;
                ticker.OnNoReconnect += oNoReconnect;
                ticker.OnError += onError;
                ticker.OnClose += onClose;
                ticker.OnConnect += onConnect;
                ticker.OnTick += onTick;

                ticker.EnableReconnect(Interval: 5, Retries: 50);
                ticker.Connect();

            }
            catch (Exception ex)
            {
                Logger.ErrorLogToFile(ex);
                MessageBox.Show(ex.Message);
            }
        }

        protected static void onTick(Tick TickData)
        {
            StatusMessage = "Token Expire event. Need to login again.";

            Logger.GenericLog(StatusMessage + " - Time - " + user.LoginTime);
        }

        protected static void onTokenExpire()
        {
            StatusMessage = "Token Expire event. Need to login again.";

            Logger.GenericLog(StatusMessage + " - Time - " + user.LoginTime);
        }

        private static void onConnect()
        {
            StatusMessage = "Connected ticker";

            Logger.GenericLog(StatusMessage + " - Time - " + user.LoginTime);
        }

        private static void onClose()
        {
            StatusMessage = "Closed ticker";

            Logger.GenericLog(StatusMessage);
        }

        private static void onError(string Message)
        {
            StatusMessage = "Error: " + Message;

            Logger.GenericLog(StatusMessage);
        }

        private static void oNoReconnect()
        {
            StatusMessage = "Not reconnecting";

            Logger.GenericLog(StatusMessage);
        }

        private static void onReconnect()
        {
            StatusMessage = "Reconnecting";

            Logger.GenericLog(StatusMessage);
        }
    }
}
