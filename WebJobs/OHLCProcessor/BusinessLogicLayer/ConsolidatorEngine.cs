using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

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

            TimeSpan CurrentTime = new TimeSpan(DateTime.Now.TimeOfDay.Hours, DateTime.Now.TimeOfDay.Minutes, 0);
            TimeSpan startTime = new TimeSpan(9, 15, 0);
            DateTime startDateTime = DateTime.Today + startTime;

            if (!_settings.IsPositional)
            {
                if (AuxiliaryMethods.MinuteTimer(_settings.Min3Timer).Contains(CurrentTime))
                {
                    Console.WriteLine(" Min3Timer " + CurrentTime.ToString());

                    _dBMethods.GenerateOHLC(startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 3);

                    loadIndicators = true;
                }

                if (AuxiliaryMethods.MinuteTimer(_settings.Min5Timer).Contains(CurrentTime))
                {
                    Console.WriteLine(" Min5Timer " + CurrentTime.ToString());

                    _dBMethods.GenerateOHLC(startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 5);

                    loadIndicators = true;
                }

                if (AuxiliaryMethods.MinuteTimer(_settings.Min10Timer).Contains(CurrentTime))
                {
                    Console.WriteLine(" Min10Timer " + CurrentTime.ToString());

                    _dBMethods.GenerateOHLC(startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 10);
                }

                if (AuxiliaryMethods.MinuteTimer(_settings.Min15Timer).Contains(CurrentTime))
                {
                    Console.WriteLine(" Min15Timer " + CurrentTime.ToString());

                    _dBMethods.GenerateOHLC(startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 15);
                }

                if (AuxiliaryMethods.MinuteTimer(_settings.Min30Timer).Contains(CurrentTime))
                {
                    Console.WriteLine(" Min30Timer " + CurrentTime.ToString());

                    _dBMethods.GenerateOHLC(startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 30);
                }

                if (AuxiliaryMethods.MinuteTimer(_settings.Min60Timer).Contains(CurrentTime))
                {
                    Console.WriteLine(" Min60Timer " + CurrentTime.ToString());

                    _dBMethods.GenerateOHLC(startDateTime.ToString("yyyy-MM-dd HH:mm:ss"), 60);
                }
            }
            else
            {
                if (AuxiliaryMethods.MinuteTimer(_settings.Min60Timer).Contains(CurrentTime))
                {
                    loadIndicators = true;
                }

            }

            return loadIndicators;
        }
    }
}
