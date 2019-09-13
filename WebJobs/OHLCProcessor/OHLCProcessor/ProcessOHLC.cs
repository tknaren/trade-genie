using System;
using System.Collections.Generic;
using APIInterfaceLayer;
using BusinessLogicLayer;
using DataAccessLayer;
using Utilities;

namespace OHLCProcessor
{
    public class ProcessOHLC
    {
        IConfigSettings _configSettings;
        IUpstoxInterface _upstoxInterface;
        IDBMethods _dBMethods;
        IHistoryLoaderEngine _historyLoaderEngine;

        public ProcessOHLC()
        {
            _configSettings = new ConfigSettings();
            _upstoxInterface = new UpstoxInterface(_configSettings);
            _dBMethods = new DBMethods(_configSettings);
            _historyLoaderEngine = new HistoryLoaderEngine(_configSettings, _upstoxInterface, _dBMethods);
        }

        public void ProcessOHLCMain()
        {
            try
            {
                //Call History Loader Engine
                //Call Consolidator Engine
                //Call Indicator Engine

                _historyLoaderEngine.LoadHistory();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
