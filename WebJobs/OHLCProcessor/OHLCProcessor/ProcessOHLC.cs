using System;
using System.Collections.Generic;
using APIInterfaceLayer;
using DataAccessLayer;
using Utilities;

namespace OHLCProcessor
{
    public class ProcessOHLC
    {
        //Get Latest Access Token from DB
        //Set Access Token and login to Upstox
        //Get Master Stock Info
        //Get Config info for downlading history
        //Get History from Upstox
        //Format and store in DB

        //Call Consolidator Engine
        //Call Indicator Engine

        private readonly IConfigSettings _settings;
        private readonly IUpstoxInterface _upstoxInterface;
        private readonly IDBMethods _dBMethods;

        public ProcessOHLC(IConfigSettings settings, IUpstoxInterface upstoxInterface, IDBMethods dBMethods)
        {
            _settings = settings;
            _upstoxInterface = upstoxInterface;
            _dBMethods = dBMethods;
        }

        public void ProcessOHLCMain()
        {
            string accessToken = string.Empty;
            bool isSuccessfulLogin = false;
            bool isAccessTokenSuccessfullySet = false;
            List<MasterStockList> masterStockLists = null;

            try
            {
                var iInMin = _settings.IntervalInMin;

                accessToken = _dBMethods.GetLatestAccessToken();

                if (!string.IsNullOrEmpty(accessToken))
                {
                    isSuccessfulLogin = _upstoxInterface.InitializeUpstox(_settings.APIKey, _settings.APISecret, _settings.RedirectUrl);

                    if (isSuccessfulLogin)
                        isAccessTokenSuccessfullySet = _upstoxInterface.SetUpstoxAccessToken(accessToken);
                }

                masterStockLists = _dBMethods.GetMasterStockList();

                foreach (MasterStockList stock in masterStockLists)
                {
                    string uri = _upstoxInterface.BuildHistoryUri(stock.TradingSymbol);

                    _upstoxInterface.GetHistory(accessToken, uri);
                }

                Console.WriteLine("Is Login Successful: {0}", isAccessTokenSuccessfullySet.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // Log the exception as this is the front interface
            }
        }
    }
}
