using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Serilog;
using Serilog.Exceptions;

namespace BusinessLogicLayer
{
    public interface IConsolidatorEngine
    {
        bool ConsolidateTickerEntries();
    }

    public class ConsolidatorEngine : IConsolidatorEngine
    {
        private readonly IConfigSettings _settings;
        private readonly IDBMethods _dBMethods;

        public ConsolidatorEngine(IConfigSettings settings, IDBMethods dBMethods)
        {
            _settings = settings;
            _dBMethods = dBMethods;
        }

        public bool ConsolidateTickerEntries()
        {
            // Have a list that contains the times when the program has to run
            // start 5 mins from 9:22 
            // add 5 mins to the start time and continue the same for 5, 15, 30 and 60 mins
            bool loadIndicators = false;

            TimeSpan currentTime = new TimeSpan(AuxiliaryMethods.GetCurrentIndianTimeStamp().TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, 0);
            //TimeSpan startTime = _settings.StartingTime;
            DateTime startDateTime = DateTime.Today + _settings.StartingTime;

            if (!_settings.IsPositional)
            {

                Task<bool>[] taskArray = { Task<bool>.Factory.StartNew(() => OHLC_3Min(currentTime, startDateTime)),
                                           Task<bool>.Factory.StartNew(() => OHLC_5Min(currentTime, startDateTime)),
                                           Task<bool>.Factory.StartNew(() => OHLC_10Min(currentTime, startDateTime)),
                                           Task<bool>.Factory.StartNew(() => OHLC_15Min(currentTime, startDateTime)),
                                           Task<bool>.Factory.StartNew(() => OHLC_30Min(currentTime, startDateTime)),
                                           Task<bool>.Factory.StartNew(() => OHLC_60Min(currentTime, startDateTime)) };

                Task.WaitAll(taskArray);

                var results = new bool[taskArray.Length];

                for (int i = 0; i < taskArray.Length; i++)
                {
                    results[i] = taskArray[i].Result;

                    if (results[i] == true)
                    {
                        loadIndicators = true;
                        break;
                    }
                }
            }
            else
            {
                if (AuxiliaryMethods.MinuteTimer(_settings.Min60Timer).Contains(currentTime))
                {
                    loadIndicators = true;
                }
            }

            return loadIndicators;
        }

        private bool OHLC_60Min(TimeSpan CurrentTime, DateTime startDateTime)
        {
            bool loadIndicators = false;

            if (AuxiliaryMethods.MinuteTimer(_settings.Min60Timer).Contains(CurrentTime))
            {
                Log.Information(" Min60Timer IN " + CurrentTime.ToString());

                _dBMethods.GenerateOHLC(startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 60);

                loadIndicators = true;
            }
            else
            {
                Log.Information("Min60Timer OUT " + CurrentTime.ToString());
            }

            return loadIndicators;
        }

        private bool OHLC_30Min(TimeSpan CurrentTime, DateTime startDateTime)
        {
            bool loadIndicators = false;

            if (AuxiliaryMethods.MinuteTimer(_settings.Min30Timer).Contains(CurrentTime))
            {
                Log.Information(" Min30Timer IN " + CurrentTime.ToString());

                _dBMethods.GenerateOHLC(startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 30);

                loadIndicators = true;
            }
            else
            {
                Log.Information("Min30Timer OUT " + CurrentTime.ToString());
            }

            return loadIndicators;
        }

        private bool OHLC_15Min(TimeSpan CurrentTime, DateTime startDateTime)
        {
            bool loadIndicators = false;

            if (AuxiliaryMethods.MinuteTimer(_settings.Min15Timer).Contains(CurrentTime))
            {
                Log.Information(" Min15Timer IN " + CurrentTime.ToString());

                _dBMethods.GenerateOHLC(startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 15);

                loadIndicators = true;
            }
            else
            {
                Log.Information("Min15Timer OUT " + CurrentTime.ToString());
            }

            return loadIndicators;
        }

        private bool OHLC_10Min(TimeSpan CurrentTime, DateTime startDateTime)
        {
            bool loadIndicators = false;

            if (AuxiliaryMethods.MinuteTimer(_settings.Min10Timer).Contains(CurrentTime))
            {
                Log.Information(" Min10Timer IN " + CurrentTime.ToString());

                _dBMethods.GenerateOHLC(startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 10);

                loadIndicators = true;
            }
            else
            {
                Log.Information("Min10Timer OUT " + CurrentTime.ToString());
            }

            return loadIndicators;
        }

        private bool OHLC_5Min(TimeSpan CurrentTime, DateTime startDateTime)
        {
            bool loadIndicators = false;

            if (AuxiliaryMethods.MinuteTimer(_settings.Min5Timer).Contains(CurrentTime))
            {
                Log.Information(" Min5Timer IN " + CurrentTime.ToString());

                _dBMethods.GenerateOHLC(startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 5);

                loadIndicators = true;
            }
            else
            {
                Log.Information("Min5Timer OUT " + CurrentTime.ToString());
            }

            return loadIndicators;
        }

        private bool OHLC_3Min(TimeSpan CurrentTime, DateTime startDateTime)
        {
            bool loadIndicators = false;

            if (AuxiliaryMethods.MinuteTimer(_settings.Min3Timer).Contains(CurrentTime))
            {
                Log.Information("Min3Timer IN " + CurrentTime.ToString());

                _dBMethods.GenerateOHLC(startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 3);

                loadIndicators = true;
            }
            else
            {
                Log.Information("Min3Timer OUT " + CurrentTime.ToString());
            }

            return loadIndicators;
        }
    }
}
