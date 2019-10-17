using Microsoft.Azure;
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

    }
}
