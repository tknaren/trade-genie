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
        int TargetPercentage { get; }
        int GapPercentage { get; }
        int PriceRangeHigh { get; }
        int PriceRangeLow { get; }
    }

    public class ConfigSettings : IConfigSettings
    {
        public string AzSQLConString
        {
            get { return ConfigurationManager.ConnectionStrings["SQLAZURECONNSTR_aztgsqldb"].ToString(); }
        }
        public string AzEFConString
        {
            get { return ConfigurationManager.ConnectionStrings["SQLAZURECONNSTR_aztgsqldbEntities"].ToString(); }
        }

        public int TargetPercentage
        {
            get { return Convert.ToInt32(CloudConfigurationManager.GetSetting("TargetPercentage").ToString()); }
        }

        public int GapPercentage
        {
            get { return Convert.ToInt32(CloudConfigurationManager.GetSetting("GapPercentage").ToString()); }
        }

        public int PriceRangeHigh
        {
            get { return Convert.ToInt32(CloudConfigurationManager.GetSetting("PriceRangeHigh").ToString()); }
        }

        public int PriceRangeLow
        {
            get { return Convert.ToInt32(CloudConfigurationManager.GetSetting("PriceRangeLow").ToString()); }
        }
    }
}
