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
            }
        }

        public TimeSpan EndingTime
        {
            get
            {
                return new TimeSpan(Int32.Parse(CloudConfigurationManager.GetSetting("EndingTimeHour")),
                                             Int32.Parse(CloudConfigurationManager.GetSetting("EndingTimeMinute")), 0);
            }
        }

        public TimeSpan HistoryEndTime
        {
            get
            {
                return new TimeSpan(Int32.Parse(CloudConfigurationManager.GetSetting("HistoryEndTimeHour")),
                                             Int32.Parse(CloudConfigurationManager.GetSetting("HistoryEndTimeMinute")), 0);

              }
        }

        public DateTime DayHistoryStartDate
        {
            get
            {
                return DateTime.Parse(CloudConfigurationManager.GetSetting("DayHistoryStartDate").ToString());
            }
        }

        public DateTime DayHistoryEndDate
        {
            get
            {
                return DateTime.Parse(CloudConfigurationManager.GetSetting("DayHistoryEndDate").ToString());
            }
        }

        public string Exchange
        {
            get { return CloudConfigurationManager.GetSetting("Exchange").ToString(); }
        }

        public string PeriodInDays
        { 
            get { return CloudConfigurationManager.GetSetting("PeriodInDays").ToString(); }
        }

        public string DelayInSec
        {
            get { return CloudConfigurationManager.GetSetting("DelayInSec").ToString(); }
        }

        public string IntervalInMin
        {
            get { return CloudConfigurationManager.GetSetting("IntervalInMin").ToString(); }
        }

        public string IntervalPeriod
        {
            get { return CloudConfigurationManager.GetSetting("IntervalPeriod").ToString(); }
        }

        public string APIKey
        {
            get { return CloudConfigurationManager.GetSetting("Api_Key").ToString(); }
        }

        public string APISecret
        {
            get { return CloudConfigurationManager.GetSetting("API_Secret").ToString(); }
        }

        public string RedirectUrl
        {
            get { return CloudConfigurationManager.GetSetting("Redirect_Url").ToString(); }
        }

        public string UpstoxBaseUri
        {
            get { return CloudConfigurationManager.GetSetting("UpstoxBaseUri").ToString(); }
        }

        public string HistoricalAPI
        {
            get { return CloudConfigurationManager.GetSetting("HistoricalAPI").ToString(); }
        }

        public string UserAgent
        {
            get { return CloudConfigurationManager.GetSetting("UserAgent").ToString(); }
        }

        public string StartDate
        {
            get { return CloudConfigurationManager.GetSetting("StartDate").ToString(); }
        }

        public string EndDate
        {
            get { return CloudConfigurationManager.GetSetting("EndDate").ToString(); }
        }

        public string AzSQLConString
        {
            get { return ConfigurationManager.ConnectionStrings["SQLAZURECONNSTR_aztgsqldb"].ToString(); }
        }
        public string AzEFConString
        {
            get { return ConfigurationManager.ConnectionStrings["SQLAZURECONNSTR_aztgsqldbEntities"].ToString(); }
        }

        public string Min3Timer
        {
            get { return CloudConfigurationManager.GetSetting("Min3Timer").ToString(); }
        }

        public string Min5Timer
        {
            get { return CloudConfigurationManager.GetSetting("Min5Timer").ToString(); }
        }

        public string Min10Timer
        {
            get { return CloudConfigurationManager.GetSetting("Min10Timer").ToString(); }
        }

        public string Min15Timer
        {
            get { return CloudConfigurationManager.GetSetting("Min15Timer").ToString(); }
        }

        public string Min30Timer
        {
            get { return CloudConfigurationManager.GetSetting("Min30Timer").ToString(); }
        }

        public string Min60Timer
        {
            get { return CloudConfigurationManager.GetSetting("Min60Timer").ToString(); }
        }

        public string EODTimer
        {
            get { return CloudConfigurationManager.GetSetting("EODTimer").ToString(); }
        }

        public string TimePeriodsToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("TimePeriodsToCalculate").ToString(); }
        }

        public string EMAsToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("EMAsToCalculate").ToString(); }
        }

        public string EHEMAsToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("EHEMAsToCalculate").ToString(); }
        }

        public string VWMAsToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("VWMAsToCalculate").ToString(); }
        }

        public string RSIsToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("RSIsToCalculate").ToString(); }
        }

        public string ForceIndexesToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("ForceIndexesToCalculate").ToString(); }
        }

        public string ATRsToCalculate
        {
            get { return CloudConfigurationManager.GetSetting("ATRsToCalculate").ToString(); }
        }

        public string SuperTrendMultipliers
        {
            get { return CloudConfigurationManager.GetSetting("SuperTrendMultipliers").ToString(); }
        }

        public string EMADeviationPeriods
        {
            get { return CloudConfigurationManager.GetSetting("EMADeviationPeriods").ToString(); }
        }
    }
}
