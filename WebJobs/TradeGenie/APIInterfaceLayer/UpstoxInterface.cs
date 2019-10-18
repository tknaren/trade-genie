using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpstoxNet;
using Utilities;

namespace APIInterfaceLayer
{
    public interface IUpstoxInterface
    {
        string AccessToken { get; }
        bool IsLoggedIn { get; }
        bool InitializeUpstox();
        bool SetUpstoxAccessToken(string accesToken);
        double GetCurrentMarketPrice(string tradingSymbol);
    }

    public class UpstoxInterface : IUpstoxInterface
    {
        private readonly Upstox _upstox;
        private readonly IConfigSettings _configSettings;

        public UpstoxInterface(IConfigSettings configSettings)
        {
            _upstox = new Upstox();
            _configSettings = configSettings;
        }

        public string AccessToken { get { return _upstox.Access_Token; } }
        public bool IsLoggedIn { get { return _upstox.Login_Status; } }

        public bool InitializeUpstox()
        {
            bool isInitialized = false;

            try
            {
                _upstox.Api_Key = _configSettings.ApiKey;
                _upstox.Api_Secret = _configSettings.ApiSecret;
                _upstox.Redirect_Url = _configSettings.RedirectUrl;

                bool loginStatus = _upstox.Login_Status;

                _upstox.Login();

                isInitialized = true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Initialize Upstox Exception");

                throw ex;
            }

            return isInitialized;
        }

        public bool SetUpstoxAccessToken(string accesToken)
        {
            bool isSuccessful = false;

            try
            {
                isSuccessful = _upstox.SetAccessToken(accesToken);
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Set Upstox AccessToken Exception");

                throw ex;
            }

            return isSuccessful;
        }

        public double GetCurrentMarketPrice(string tradingSymbol)
        {
            double cmp = 0.0;

            try
            {
                cmp = _upstox.GetLtp(_configSettings.Exchange, tradingSymbol);
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Error - GET CMP " + tradingSymbol);
            }

            return cmp;
        }
    }
}
