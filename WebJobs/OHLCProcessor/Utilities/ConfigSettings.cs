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
        public string StartingTimeHour
        {
            get { return ConfigurationManager.AppSettings["StartingTimeHour"].ToString(); }
        }

        public string StartingTimeMinute
        {
            get { return ConfigurationManager.AppSettings["StartingTimeMinute"].ToString(); }
        }

        public string EndingTimeHour
        {
            get { return ConfigurationManager.AppSettings["EndingTimeHour"].ToString(); }
        }

        public string EndingTimeMinute
        {
            get { return ConfigurationManager.AppSettings["EndingTimeMinute"].ToString(); }
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
    }
}
