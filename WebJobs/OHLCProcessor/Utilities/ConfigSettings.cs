using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Utilities
{
    public class ConfigSettings : IConfigSettings
    {
        public TimeSpan StartingTime
        {
            get
            {
                return new TimeSpan(Int32.Parse(ConfigurationManager.AppSettings["StartingTimeHour"]),
                                             Int32.Parse(ConfigurationManager.AppSettings["StartingTimeMinute"]), 0);
            }
        }

        public TimeSpan EndingTime
        {
            get
            {
                return new TimeSpan(Int32.Parse(ConfigurationManager.AppSettings["EndingTimeHour"]),
                                             Int32.Parse(ConfigurationManager.AppSettings["EndingTimeMinute"]), 0);
            }
        }

        public string Exchange
        {
            get { return ConfigurationManager.AppSettings["Exchange"].ToString(); }
        }

        public string PeriodInDays
        {
            get { return ConfigurationManager.AppSettings["PeriodInDays"].ToString(); }
        }

        public string DelayInMin
        {
            get { return ConfigurationManager.AppSettings["DelayInMin"].ToString(); }
        }

        public string IntervalInMin
        {
            get { return ConfigurationManager.AppSettings["IntervalInMin"].ToString(); }
        }

        public string APIKey
        {
            get { return ConfigurationManager.AppSettings["Api_Key"].ToString(); }
        }

        public string APISecret
        {
            get { return ConfigurationManager.AppSettings["API_Secret"].ToString(); }
        }

        public string RedirectUrl
        {
            get { return ConfigurationManager.AppSettings["Redirect_Url"].ToString(); }
        }

        public string UpstoxBaseUri
        {
            get { return ConfigurationManager.AppSettings["UpstoxBaseUri"].ToString(); }
        }

        public string HistoricalAPI
        {
            get { return ConfigurationManager.AppSettings["HistoricalAPI"].ToString(); }
        }

        public string UserAgent
        {
            get { return ConfigurationManager.AppSettings["UserAgent"].ToString(); }
        }

        public string StartDate
        {
            get { return ConfigurationManager.AppSettings["StartDate"].ToString(); }
        }

        public string EndDate
        {
            get { return ConfigurationManager.AppSettings["EndDate"].ToString(); }
        }

        public string AzSQLConString
        {
            get { return ConfigurationManager.ConnectionStrings["aztgsqldb"].ToString(); }
        }

        public string Min3Timer
        {
            get { return ConfigurationManager.AppSettings["Min3Timer"].ToString(); }
        }

        public string Min5Timer
        {
            get { return ConfigurationManager.AppSettings["Min5Timer"].ToString(); }
        }

        public string Min10Timer
        {
            get { return ConfigurationManager.AppSettings["Min10Timer"].ToString(); }
        }

        public string Min15Timer
        {
            get { return ConfigurationManager.AppSettings["Min15Timer"].ToString(); }
        }

        public string Min30Timer
        {
            get { return ConfigurationManager.AppSettings["Min30Timer"].ToString(); }
        }

        public string Min60Timer
        {
            get { return ConfigurationManager.AppSettings["Min60Timer"].ToString(); }
        }

        public bool IsPositional
        {
            get { return Convert.ToBoolean(ConfigurationManager.AppSettings["IsPositional"].ToString()); }
        }

        public string TimePeriodsToCalculate
        {
            get { return ConfigurationManager.AppSettings["TimePeriodsToCalculate"].ToString(); }
        }

        public string EMAsToCalculate
        {
            get { return ConfigurationManager.AppSettings["EMAsToCalculate"].ToString(); }
        }

        public string EHEMAsToCalculate
        {
            get { return ConfigurationManager.AppSettings["EHEMAsToCalculate"].ToString(); }
        }

        public string VWMAsToCalculate
        {
            get { return ConfigurationManager.AppSettings["VWMAsToCalculate"].ToString(); }
        }

        public string RSIsToCalculate
        {
            get { return ConfigurationManager.AppSettings["RSIsToCalculate"].ToString(); }
        }

        public string ForceIndexesToCalculate
        {
            get { return ConfigurationManager.AppSettings["ForceIndexesToCalculate"].ToString(); }
        }

        public string ATRsToCalculate
        {
            get { return ConfigurationManager.AppSettings["ATRsToCalculate"].ToString(); }
        }

        public string SuperTrendMultipliers
        {
            get { return ConfigurationManager.AppSettings["SuperTrendMultipliers"].ToString(); }
        }

        public string EMADeviationPeriods
        {
            get { return ConfigurationManager.AppSettings["EMADeviationPeriods"].ToString(); }
        }
    }
}
