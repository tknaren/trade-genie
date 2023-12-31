﻿using System;
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
        private readonly IKiteConnectInterface _kiteConnectInterface;
        private readonly IDBMethods _dBMethods;
        private readonly IHistoryLoaderEngine _historyLoaderEngine;
        private readonly IConsolidatorEngine _consolidatorEngine;
        private readonly IIndicatorEngine _indicatorEngine;

        public ProcessOHLC()
        {
            _configSettings = new ConfigSettings();
            _upstoxInterface = new UpstoxInterface(_configSettings);
            _kiteConnectInterface = new KiteConnectInterface(_configSettings);
            _dBMethods = new DBMethods(_configSettings);
            _historyLoaderEngine = new HistoryLoaderEngine(_configSettings, _upstoxInterface, _kiteConnectInterface, _dBMethods);
            _consolidatorEngine = new ConsolidatorEngine(_configSettings, _dBMethods);
            _indicatorEngine = new IndicatorEngine(_configSettings, _dBMethods, _kiteConnectInterface);
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
                #region commented history loader and consolidator
                //if (HistoryFetchTime() && _configSettings.IsHistoryFetchReq)
                //{
                //    Log.Information("History Fetch Start");
                //    _historyLoaderEngine.LoadHistory();
                //    Log.Information("History Fetch End");
                //}

                //if (_historyLoaderEngine.IsUserLoggedIn && _configSettings.IsConsolidatorReq)
                //{
                //    Log.Information("Consolidator Start");
                //    loadIndicators = _consolidatorEngine.ConsolidateTickerEntries();
                //    Log.Information("Consolidator End");
                //}

                //if (IsDayHistoryTime() && _configSettings.IsDayHistoryFetchReq)
                //{
                //    Log.Information("Day History Fetch Start");
                //    _historyLoaderEngine.LoadHistory(true);
                //    Log.Information("Day History Fetch End");
                //    loadIndicators = true;
                //}

                //if (loadIndicators && _configSettings.IsIndicatorReq)
                #endregion

                //New Logic - 22-Apr-2020, This part calls the history, calculates the indicators, and loads the data to DB.
                //                          History and consolidator logic are not required now.

                if (_configSettings.IsIndicatorReq)
                {
                    Log.Information("Indicators Start");
                    _indicatorEngine.IndicatorEngineLogic(loadIndicators);
                    Log.Information("Indicators End");
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

            if (DateTime.Today.DayOfWeek == DayOfWeek.Saturday || DateTime.Today.DayOfWeek == DayOfWeek.Sunday)
            {
                return false;
            }
            else
            {
                return ((CurrentTime > _configSettings.StartingTime) && (CurrentTime < _configSettings.EndingTime));
            }
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
                while (true)
                {
                    if (TradingTime())
                    //if (true)
                    {
                        ProcessOHLCMain();
                        Thread.Sleep(GetSleepTime());
                    }
                    else
                    {
                        break;
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Start Engine Exception");
            }
        }

    }
}
