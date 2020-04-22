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

        List<Historical> GetHistory(int instrumentToken, DateTime fromWhen, int timePeriod);
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
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "Kite Connect Login Error");
                return false;
            }

            return true;
            
        }

        public List<Historical> GetHistory(int instrumentToken, DateTime fromWhen, int timePeriod)
        {
            List<Historical> history = _kite.GetHistoricalData(instrumentToken.ToString(), fromWhen, 
                DateTime.Now.ToIndianTimeStamp(), timePeriod.ToKiteInternal());

            return history;
        }

        private static void OnTokenExpire()
        {
            Log.Error("Need to login again");
        }
    }
}
