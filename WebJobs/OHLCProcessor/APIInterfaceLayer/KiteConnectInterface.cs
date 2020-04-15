using KiteConnect;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace APIInterfaceLayer
{

    public interface IKiteConnectInterface
    {
        bool InitializeKiteConnect(string requestToken, string accessToken);

        List<Historical> GetHistory(int instrumentToken, DateTime fromWhen);
    }

    public class KiteConnectInterface : IKiteConnectInterface
    {
        private Kite _kite;
        private readonly IConfigSettings _configSettings;

        public KiteConnectInterface(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
            
        }

        public bool InitializeKiteConnect(string requestToken, string accessToken)
        {
            try
            {
                _kite = new Kite(_configSettings.KiteAPIKey, Debug: true);

                _kite.SetAccessToken(accessToken);

                _kite.SetSessionExpiryHook(OnTokenExpire);

                //var profile = _kite.GetProfile();

                //string[] instruments = new string[] { "7456769" };

                //Dictionary<string, OHLC> ohlc = _kite.GetOHLC(instruments);

                //List<Historical> history = _kite.GetHistoricalData("7456769", DateTime.Now.AddDays(-10), DateTime.Now, "5minute");

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "Kite Connect Login Error");
                return false;
            }

            return true;
            
        }

        public List<Historical> GetHistory(int instrumentToken, DateTime fromWhen)
        {
            List<Historical> history = _kite.GetHistoricalData(instrumentToken.ToString(), fromWhen, DateTime.Now.ToIndianTimeStamp(), _configSettings.KiteInterval);

            return history;
        }

        //private static void initSession()
        //{
        //    Console.WriteLine("Goto " + kite.GetLoginURL());
        //    Console.WriteLine("Enter request token: ");
        //    string requestToken = Console.ReadLine();
        //    User user = kite.GenerateSession(requestToken, MySecret);

        //    Console.WriteLine(Utils.JsonSerialize(user));

        //    MyAccessToken = user.AccessToken;
        //    MyPublicToken = user.PublicToken;
        //}

        //private static void initTicker()
        //{
        //    ticker = new Ticker(MyAPIKey, MyAccessToken);

        //    ticker.OnTick += OnTick;
        //    ticker.OnReconnect += OnReconnect;
        //    ticker.OnNoReconnect += OnNoReconnect;
        //    ticker.OnError += OnError;
        //    ticker.OnClose += OnClose;
        //    ticker.OnConnect += OnConnect;
        //    ticker.OnOrderUpdate += OnOrderUpdate;

        //    ticker.EnableReconnect(Interval: 5, Retries: 50);
        //    ticker.Connect();

        //    // Subscribing to NIFTY50 and setting mode to LTP
        //    ticker.Subscribe(Tokens: new UInt32[] { 256265 });
        //    ticker.SetMode(Tokens: new UInt32[] { 256265 }, Mode: Constants.MODE_LTP);
        //}

        private static void OnTokenExpire()
        {
            Log.Error("Need to login again");
        }
    }
}
