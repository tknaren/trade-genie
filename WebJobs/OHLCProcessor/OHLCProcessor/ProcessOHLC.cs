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
            //Just for testing

            bool loadIndicators = false;

            try
            {
                if (!Environment.UserInteractive)
                {
                    if (HistoryFetchTime())
                    {
                        Log.Information("History Fetch Start");
                        _historyLoaderEngine.LoadHistory();
                        Log.Information("History Fetch End");
                    }

                    if (_historyLoaderEngine.IsUserLoggedIn)
                    {
                        Log.Information("Consolidator Start");
                        loadIndicators = _consolidatorEngine.ConsolidateTickerEntries();
                        Log.Information("Consolidator End");
                    }

                    if (IsDayHistoryTime())
                    {
                        Log.Information("Day History Fetch Start");
                        _historyLoaderEngine.LoadHistory(true);
                        Log.Information("Day History Fetch End");
                        loadIndicators = true;
                    }

                    if (loadIndicators)
                    {
                        Log.Information("Indicators Start");
                        _indicatorEngine.IndicatorEngineLogic();
                        Log.Information("Indicators End");
                    }
                }
                else
                {
                    _historyLoaderEngine.LoadHistory(true);
                    _indicatorEngine.IndicatorEngineLogic();
                }

            }
            catch(Exception ex)
            {
                Log.Error(ex, "ProcessOHLC Main Exception");
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

        private bool IsDayHistoryTime()
        {
            bool isDayHistoryTime = false;

            TimeSpan timeSpan = AuxiliaryMethods.GetCurrentIndianTimeStamp().TimeOfDay;

            TimeSpan eodTime = new TimeSpan(Convert.ToInt32(_configSettings.EODTimer.Split(':')[0]),
                                            Convert.ToInt32(_configSettings.EODTimer.Split(':')[1]), 0);

            TimeSpan CurrentTime = new TimeSpan(timeSpan.Hours, timeSpan.Minutes, 00);

            if (CurrentTime == eodTime)
                isDayHistoryTime = true;

            return isDayHistoryTime;
        }

        private int GetSleepTime()
        {
            int maxSeconds = Convert.ToInt32(_configSettings.DelayInSec);
            int seconds = AuxiliaryMethods.GetCurrentIndianTimeStamp().TimeOfDay.Seconds;

            int timeToSleep = maxSeconds - seconds;

            return timeToSleep * 1000;
        }

        public void StartEngine()
        {
            try
            {
                if (Environment.UserInteractive)
                {
                    while (true)
                    {
                        if (TradingTime())
                        {
                            ProcessOHLCMain();
                            Thread.Sleep(GetSleepTime());
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                else
                {
                    ProcessOHLCMain();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Start Engine Exception");
            }
        }

    }
}
