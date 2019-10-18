using APIInterfaceLayer;
using BusinessLogicLayer;
using DataAccessLayer;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace TradeGenie
{
    public class AlgoTrade
    {
        private readonly IConfigSettings _configSettings;
        private readonly IUpstoxInterface _upstoxInterface;
        private readonly IDBMethods _dBMethods;
        private readonly IGapStrategyEngine _gapStrategyEngine;
        private string _accessToken;

        public AlgoTrade()
        {
            _configSettings = new ConfigSettings();
            _upstoxInterface = new UpstoxInterface(_configSettings);
            _dBMethods = new DBMethods(_configSettings);
            _gapStrategyEngine = new GapStrategyEngine(_configSettings, _upstoxInterface, _dBMethods);
        }

        public void StartTrading()
        {
            try
            {
                _accessToken = _dBMethods.GetLatestAccessToken();

                _upstoxInterface.InitializeUpstox();

                AlgoTradingMain();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Start Trading Method Exception");
            }
        }

        private void AlgoTradingMain()
        {
            _gapStrategyEngine.RunGapStrategy();
        }
        
    }
}
