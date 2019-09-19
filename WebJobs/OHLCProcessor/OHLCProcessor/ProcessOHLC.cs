using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using APIInterfaceLayer;
using BusinessLogicLayer;
using DataAccessLayer;
using Microsoft.Azure.WebJobs;
using Utilities;
using Serilog;
using Serilog.Exceptions;
using System.Threading;

namespace OHLCProcessor
{
    public class ProcessOHLC : IDisposable
    {
        private readonly IConfigSettings _configSettings;
        private readonly IUpstoxInterface _upstoxInterface;
        private readonly IDBMethods _dBMethods;
        private readonly IHistoryLoaderEngine _historyLoaderEngine;
        private readonly IConsolidatorEngine _consolidatorEngine;
        private readonly IIndicatorEngine _indicatorEngine;

        public ProcessOHLC()
        {
            _configSettings = new ConfigSettings();
            _upstoxInterface = new UpstoxInterface(_configSettings);
            _dBMethods = new DBMethods(_configSettings);
            _historyLoaderEngine = new HistoryLoaderEngine(_configSettings, _upstoxInterface, _dBMethods);
            _consolidatorEngine = new ConsolidatorEngine(_configSettings, _dBMethods);
            _indicatorEngine = new IndicatorEngine(_configSettings, _dBMethods);
        }

        public void Dispose()
        {

        }

        private void ProcessOHLCMain()
        {
            //Call History Loader Engine
            //Call Consolidator Engine
            //Call Indicator Engine

            bool loadIndicators = false;

            try
            {
                if (HistoryFetchTime())
                {
                    Log.Information("History Fetch Start");
                    _historyLoaderEngine.LoadHistory();
                    Log.Information("History Fetch End");

                    Log.Information("Consolidator Start");
                    loadIndicators = _consolidatorEngine.ConsolidateTickerEntries();
                    Log.Information("Consolidator End");
                }

                if (loadIndicators)
                {
                    Log.Information("Indicators Start");
                    _indicatorEngine.IndicatorEngineLogic();
                    Log.Information("Indicators End");
                }

            }
            catch(Exception ex)
            {
                Log.Error(ex, "ProcessOHLCMain Exception");
            }
        }

        private bool TradingTime()
        {
            TimeSpan CurrentTime = AuxiliaryMethods.GetCurrentIndianTimeStamp().TimeOfDay;

            return ((CurrentTime > _configSettings.StartingTime) && (CurrentTime < _configSettings.EndingTime));
        }

        private bool HistoryFetchTime()
        {
            TimeSpan CurrentTime = AuxiliaryMethods.GetCurrentIndianTimeStamp().TimeOfDay;

            return ((CurrentTime > _configSettings.StartingTime) && (CurrentTime < _configSettings.HistoryEndTime));
        }

        public void StartEngine()
        {
            try
            {
                while (true)
                {
                    if (TradingTime())
                        ProcessOHLCMain();

                    Thread.Sleep(Convert.ToInt32(_configSettings.DelayInMin) * 60 * 1000);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Start Engine Exception");
            }
        }

    }
}
