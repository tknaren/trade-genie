using APIInterfaceLayer.Models;
using System;
using System.Text;
using UpstoxNet;
using Utilities;
using Serilog;
using Serilog.Exceptions;

namespace APIInterfaceLayer
{
    public interface IUpstoxInterface
    {
        string AccessToken { get; }
        bool IsLoggedIn { get; }
        bool InitializeUpstox(string apiKey, string apiSecret, string redirectUrl);
        bool SetUpstoxAccessToken(string accesToken);
        string BuildHistoryUri(string stockCode);
        Historical GetHistory(string accesToken, string uri);
    }

    public class UpstoxInterface : IUpstoxInterface
    {
        private readonly Upstox _upstox;
        private readonly IConfigSettings _configSettings;
        private readonly WebClientInterface _webClient;

        string _separator = "/";
        string _param = "?";
        string _paramSeparator = "&";


        public UpstoxInterface(IConfigSettings configSettings)
        {
            _upstox = new Upstox();
            _webClient = new WebClientInterface(configSettings);
            _configSettings = configSettings;
        }

        public string AccessToken { get { return _upstox.Access_Token; } }
        public bool IsLoggedIn { get { return _upstox.Login_Status; } }

        public bool InitializeUpstox(string apiKey, string apiSecret, string redirectUrl)
        {
            bool isInitialized = false;

            try
            {
                _upstox.Api_Key = apiKey;
                _upstox.Api_Secret = apiSecret;
                _upstox.Redirect_Url = redirectUrl;

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

        public string BuildHistoryUri(string stockCode)
        {
            StringBuilder uri = new StringBuilder();
            uri.Append(_configSettings.HistoricalAPI);
            uri.Append(_separator);
            uri.Append(_configSettings.Exchange);
            uri.Append(_separator);
            uri.Append(stockCode);
            uri.Append(_separator);
            uri.Append(_configSettings.IntervalInMin);
            uri.Append(_param);
            uri.Append("start_date=" + DateTime.Today.AddDays(-5).ToString("dd-MM-yyyy"));
            uri.Append(_paramSeparator);
            uri.Append("end_date=" + DateTime.Today.ToString("dd-MM-yyyy"));

            return uri.ToString();
        }

        public Historical GetHistory(string accesToken, string uri)
        {
            Historical historical = _webClient.CallHistoricalAPI(accesToken, uri);

            return historical;
        }
    }
}
