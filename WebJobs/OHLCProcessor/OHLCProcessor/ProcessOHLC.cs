using System;
using System.Collections.Generic;
using System.IO;
using System.Timers;
using APIInterfaceLayer;
using BusinessLogicLayer;
using DataAccessLayer;
using Microsoft.Azure.WebJobs;
using Utilities;

namespace OHLCProcessor
{
    public class ProcessOHLC : IDisposable
    {
        IConfigSettings _configSettings;
        IUpstoxInterface _upstoxInterface;
        IDBMethods _dBMethods;
        IHistoryLoaderEngine _historyLoaderEngine;
        IConsolidatorEngine _consolidatorEngine;
        TextWriter log;

        //Timer tickerTimer = null;

        public ProcessOHLC()
        {
            _configSettings = new ConfigSettings();
            _upstoxInterface = new UpstoxInterface(_configSettings);
            _dBMethods = new DBMethods(_configSettings);
            _historyLoaderEngine = new HistoryLoaderEngine(_configSettings, _upstoxInterface, _dBMethods);
            _consolidatorEngine = new ConsolidatorEngine(_configSettings, _dBMethods);
            //tickerTimer = new Timer();
        }

        public void Dispose()
        {
            _configSettings = null;
            _upstoxInterface = null;
            _dBMethods = null;
            _historyLoaderEngine = null;
        }

        public void ProcessOHLCMain()
        {
            //Call History Loader Engine
            //Call Consolidator Engine
            //Call Indicator Engine

            try
            {
                Console.WriteLine("History Fetch Start @ " + DateTime.Now.ToString());

                _historyLoaderEngine.LoadHistory();

                Console.WriteLine("History Fetch End @ " + DateTime.Now.ToString());

                bool loadIndicators = _consolidatorEngine.ConsolidateTickerEntries();

                Console.WriteLine("Consolidator End @ " + DateTime.Now.ToString());


            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message + " " + ex.StackTrace);
            }
        }

        //private bool TradingTime()
        //{
        //    TimeSpan CurrentTime = DateTime.Now.TimeOfDay;
        //    return ((CurrentTime > _configSettings.StartingTime) && (CurrentTime < _configSettings.EndingTime));
        //}

        //public void StartEngine()
        //{
        //    this.tickerTimer.Interval = Convert.ToInt32(_configSettings.DelayInMin) * 60 * 1000; //convert minutes to milliseconds
        //    this.tickerTimer.Elapsed += new System.Timers.ElapsedEventHandler(this.tickerTimer_Tick);
        //    this.tickerTimer.Enabled = true;
        //}


        //private void tickerTimer_Tick(object sender, ElapsedEventArgs e)
        //{
        //    Console.WriteLine("Timer called");

        //    if (TradingTime())
        //    {
        //        Console.WriteLine("Trading Time");

        //        try
        //        {
        //            ProcessOHLCMain();
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex);
        //        }
        //    }

        //}
    }
}
