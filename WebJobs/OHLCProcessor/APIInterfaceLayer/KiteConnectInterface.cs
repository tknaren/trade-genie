using KiteConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APIInterfaceLayer
{

    interface IKiteConnectInterface
    {
        bool InitializeKiteConnect(string apiKey, string apiSecret, string userId);
    }

    public class KiteConnectInterface : IKiteConnectInterface
    {
        static Ticker ticker;
        static Kite kite;

        public bool InitializeKiteConnect(string apiKey, string apiSecret, string userId)
        {
            kite = new Kite(apiKey, Debug: true);

            kite.SetSessionExpiryHook(OnTokenExpire);

            // Initializes the login flow

            try
            {
                initSession();
            }
            catch (Exception e)
            {
                // Cannot continue without proper authentication
                Console.WriteLine(e.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }

            kite.SetAccessToken(MyAccessToken);

            // Initialize ticker

            initTicker();

            return false;
        }
    }
}
