using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections;
using Microsoft.Extensions.Configuration;

namespace OHLCProcessor
{
    public interface IConfigSettings
    {
        string AzTGSQLDBConString { get; }
        string StartingTimeHour { get; }
        string StartingTimeMinute { get; }
        string EndingTimeHour { get; }
        string EndingTimeMinute { get; }
        string Exchange { get; }
        string PeriodInDays { get; }
        string DelayInMin { get; }
        string IntervalInMin { get; }
    }

    public class ConfigSettings : IConfigSettings
    {
        IConfigurationRoot configuration;

        public ConfigSettings()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json");

            configuration = builder.Build();
        }

        public string AzTGSQLDBConString
        {
            get { return configuration["AzTGSQLDBConString"].ToString(); }
        }

        public string StartingTimeHour
        {
            get { return configuration["StartingTimeHour"].ToString(); }
        }

        public string StartingTimeMinute
        {
            get { return configuration["StartingTimeMinute"].ToString(); }
        }

        public string EndingTimeHour
        {
            get { return configuration["EndingTimeHour"].ToString(); }
        }

        public string EndingTimeMinute
        {
            get { return configuration["EndingTimeMinute"].ToString(); }
        }

        public string Exchange
        {
            get { return configuration["Exchange"].ToString(); }
        }

        public string PeriodInDays
        {
            get { return configuration["PeriodInDays"].ToString(); }
        }

        public string DelayInMin
        {
            get { return configuration["DelayInMin"].ToString(); }
        }

        public string IntervalInMin
        {
            get { return configuration["IntervalInMin"].ToString(); }
        }
    }
}
