using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Serilog;
using Microsoft.Azure;

namespace Utilities
{
    public class ConfigSettings : IConfigSettings
    {
        public TimeSpan StartingTime
        {
            get
            {
                return new TimeSpan(Int32.Parse(CloudConfigurationManager.GetSetting("StartingTimeHour")),
                                             Int32.Parse(CloudConfigurationManager.GetSetting("StartingTimeMinute")), 0);

                //return new TimeSpan(Int32.Parse(ConfigurationManager.AppSettings["StartingTimeHour"]),
                //                             Int32.Parse(ConfigurationManager.AppSettings["StartingTimeMinute"]), 0);
            }
        }

        public TimeSpan EndingTime
        {
            get
            {
                return new TimeSpan(Int32.Parse(CloudConfigurationManager.GetSetting("EndingTimeHour")),
                                             Int32.Parse(CloudConfigurationManager.GetSetting("EndingTimeMinute")), 0);

                //return new TimeSpan(Int32.Parse(ConfigurationManager.AppSettings["EndingTimeHour"]),
                //                             Int32.Parse(ConfigurationManager.AppSettings["EndingTimeMinute"]), 0);
            }
        }

        public TimeSpan HistoryEndTime
        {
            get
            {
                return new TimeSpan(Int32.Parse(CloudConfigurationManager.GetSetting("HistoryEndTimeHour")),
                                             Int32.Parse(CloudConfigurationManager.GetSetting("HistoryEndTimeMinute")), 0);

                //return new TimeSpan(Int32.Parse(ConfigurationManager.AppSettings["HistoryEndTimeHour"]),
                //                             Int32.Parse(ConfigurationManager.AppSettings["HistoryEndTimeMinute"]), 0);
            }
        }

        public string Exchange
        {
            get { return CloudConfigurationManager.GetSetting("Exchange").ToString(); }
            //get { return ConfigurationManager.AppSettings["Exchange"].ToString(); }
        }

        public string PeriodInDays
        { 
            get { return CloudConfigurationManager.GetSetting("PeriodInDays").ToString(); }
            //get { return ConfigurationManager.AppSettings["PeriodInDays"].ToString(); }
        }

        public string DelayInMin
        {
            get { return CloudConfigurationManager.GetSetting("DelayInMin").ToString(); }
            //get { return ConfigurationManager.AppSettings["DelayInMin"].ToString(); }
        }

        public string IntervalInMin
        {
            get { return CloudConfigurationManager.GetSetting("IntervalInMin").ToString(); }
            //get { return ConfigurationManager.AppSettings["IntervalInMin"].ToString(); }
        }

        public string APIKey
        {
            get { return CloudConfigurationManager.GetSetting("Api_Key").ToString(); }
            //get { return ConfigurationManager.AppSettings["Api_Key"].ToString(); }
        }

        public string APISecret
        {
            get { return CloudConfigurationManager.GetSetting("API_Secret").ToString(); }
            //get { return ConfigurationManager.AppSettings["API_Secret"].ToString(); }
        }

        public string RedirectUrl
        {
            get { return CloudConfigurationManager.GetSetting("Redirect_Url").ToString(); }
            //get { return ConfigurationManager.AppSettings["Redirect_Url"].ToString(); }
        }

        public string UpstoxBaseUri
        {
            get { return CloudConfigurationManager.GetSetting("UpstoxBaseUri").ToString(); }
            //get { return ConfigurationManager.AppSettings["UpstoxBaseUri"].ToString(); }
        }

        public string HistoricalAPI
        {
            get { return CloudConfigurationManager.GetSetting("HistoricalAPI").ToString(); }
            //get { return ConfigurationManager.AppSettings["HistoricalAPI"].ToString(); }
        }

        public string UserAgent
        {
            get { return CloudConfigurationManager.GetSetting("UserAgent").ToString(); }
            //get { return ConfigurationManager.AppSettings["UserAgent"].ToString(); }
        }

        public string StartDate
        {
            get { return CloudConfigurationManager.GetSetting("StartDate").ToString(); }
            //get { return ConfigurationManager.AppSettings["StartDate"].ToString(); }
        }

        public string EndDate
        {
            get { return CloudConfigurationManager.GetSetting("EndDate").ToString(); }
            //get { return ConfigurationManager.AppSettings["EndDate"].ToString(); }
        }

        public string AzSQLConString
        {
            get { return ConfigurationManager.ConnectionStrings["aztgsqldb"].ToString(); }
        }

        public string Min3Timer
        {
            get { return CloudConfigurationManager.GetSetting("Min3Timer").ToString(); }
            //get { return ConfigurationManager.AppSettings["Min3Timer"].ToString(); }
        }

        public string Min5Timer
        {
            get { return CloudConfigurationManager.GetSetting("Min5Timer").ToString(); }
            //get { return ConfigurationManager.AppSettings["Min5Timer"].ToString(); }
        }

        public string Min10Timer
        {
            get { return CloudConfigurationManager.GetSetting("Min10Timer").ToString(); }
            //get { return ConfigurationManager.AppSettings["Min10Timer"].ToString(); }
        }

        public string Min15Timer
        {
            get { return CloudConfigurationManager.GetSetting("Min15Timer").ToString(); }
            //get { return ConfigurationManager.AppSettings["Min15Timer"].ToString(); }
        }

        public string Min30Timer
        {
            get { return CloudConfigurationManager.GetSetting("Min30Timer").ToString(); }
            //get { return ConfigurationManager.AppSettings["Min30Timer"].ToString(); }
        }

        public string Min60Timer
        {
            get { return CloudConfigurationManager.GetSetting("Min60Timer").ToString(); }
            //get { return ConfigurationManager.AppSettings["Min60Timer"].ToString(); }
        }

        public bool IsPositional
        {
            get { return Convert.ToBoolean(CloudConfigurationManager.GetSetting("IsPositional").ToString()); }
            //get { return Convert.ToBoolean(ConfigurationManager.AppSettings["IsPositional"].ToString()); }
        }

        public string TimePeriodsToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("TimePeriodsToCalculate").ToString(); }
            //get { return ConfigurationManager.AppSettings["TimePeriodsToCalculate"].ToString(); }
        }

        public string EMAsToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("EMAsToCalculate").ToString(); }
            //get { return ConfigurationManager.AppSettings["EMAsToCalculate"].ToString(); }
        }

        public string EHEMAsToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("EHEMAsToCalculate").ToString(); }
            //get { return ConfigurationManager.AppSettings["EHEMAsToCalculate"].ToString(); }
        }

        public string VWMAsToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("VWMAsToCalculate").ToString(); }
            //get { return ConfigurationManager.AppSettings["VWMAsToCalculate"].ToString(); }
        }

        public string RSIsToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("RSIsToCalculate").ToString(); }
            //get { return ConfigurationManager.AppSettings["RSIsToCalculate"].ToString(); }
        }

        public string ForceIndexesToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("ForceIndexesToCalculate").ToString(); }
            //get { return ConfigurationManager.AppSettings["ForceIndexesToCalculate"].ToString(); }
        }

        public string ATRsToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("ATRsToCalculate").ToString(); }
            //get { return ConfigurationManager.AppSettings["ATRsToCalculate"].ToString(); }
        }

        public string SuperTrendMultipliers
        {
            get { return CloudConfigurationManager.GetSetting("SuperTrendMultipliers").ToString(); }
            //get { return ConfigurationManager.AppSettings["SuperTrendMultipliers"].ToString(); }
        }

        public string EMADeviationPeriods
        {
            get { return CloudConfigurationManager.GetSetting("EMADeviationPeriods").ToString(); }
            //get { return ConfigurationManager.AppSettings["EMADeviationPeriods"].ToString(); }
        }
    }
}
