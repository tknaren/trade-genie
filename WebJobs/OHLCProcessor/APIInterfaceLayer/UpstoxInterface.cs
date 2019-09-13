using APIInterfaceLayer.Models;
using System;
using System.Text;
using UpstoxNet;
using Utilities;

namespace APIInterfaceLayer
{
    public interface IUpstoxInterface
    {
        bool InitializeUpstox(string apiKey, string apiSecret, string redirectUrl);
        bool SetUpstoxAccessToken(string accesToken);
        string BuildHistoryUri(string stockCode);
        Historical GetHistory(string accesToken, string uri);
    }

    public class UpstoxInterface : IUpstoxInterface
    {
        Upstox _upstox;
        IConfigSettings _configSettings;
        WebClientInterface _webClient;

        string _separator = "/";
        string _param = "?";
        string _paramSeparator = "&";


        public UpstoxInterface(IConfigSettings configSettings)
        {
            _upstox = new Upstox();
            _webClient = new WebClientInterface(configSettings);
            _configSettings = configSettings;
        }

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
                Console.WriteLine("Error while initializing Upstox: {0}", ex.Message);

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
                Console.WriteLine("Error while setting Access Token: {0}", ex.Message);

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
            uri.Append("start_date=" + _configSettings.StartDate);
            uri.Append(_paramSeparator);
            uri.Append("end_date=" + _configSettings.EndDate);

            return uri.ToString();
        }

        public Historical GetHistory(string accesToken, string uri)
        {
            Historical historical = _webClient.CallHistoricalAPI(accesToken, uri);

            return historical;
        }
    }
}
