﻿using Microsoft.Azure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public interface IConfigSettings
    {
        string AzSQLConString { get; }
        string AzEFConString { get; }
        DateTime PreviousTradeDay { get; }
        double TargetPercentage { get; }
        double GapPercentage { get; }
        int PriceRangeHigh { get; }
        int PriceRangeLow { get; }
        string Exchange { get; }
        string ApiKey { get; }
        string ApiSecret { get; }
        string RedirectUrl { get; }
        string UpstoxBaseUri { get; }
    }
    public class ConfigSettings : IConfigSettings
    {
        public string AzSQLConString => ConfigurationManager.ConnectionStrings["SQLAZURECONNSTR_aztgsqldb"].ToString();
        public string AzEFConString => ConfigurationManager.ConnectionStrings["SQLAZURECONNSTR_aztgsqldbEntities"].ToString();
        public DateTime PreviousTradeDay => DateTime.Parse(CloudConfigurationManager.GetSetting("PreviousTradeDay").ToString());
        public double TargetPercentage => Convert.ToDouble(CloudConfigurationManager.GetSetting("TargetPercentage").ToString());
        public double GapPercentage => Convert.ToDouble(CloudConfigurationManager.GetSetting("GapPercentage").ToString());
        public int PriceRangeHigh => Convert.ToInt32(CloudConfigurationManager.GetSetting("PriceRangeHigh").ToString());
        public int PriceRangeLow => Convert.ToInt32(CloudConfigurationManager.GetSetting("PriceRangeLow").ToString());
        public string Exchange => CloudConfigurationManager.GetSetting("Exchange").ToString();
        public string ApiKey => CloudConfigurationManager.GetSetting("ApiKey").ToString();
        public string ApiSecret => CloudConfigurationManager.GetSetting("ApiSecret").ToString();
        public string RedirectUrl => CloudConfigurationManager.GetSetting("RedirectUrl").ToString();
        public string UpstoxBaseUri => CloudConfigurationManager.GetSetting("UpstoxBaseUri").ToString();
    }
}
