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

    }
    public class ConfigSettings : IConfigSettings
    {
        public string AzSQLConString => ConfigurationManager.ConnectionStrings["SQLAZURECONNSTR_aztgsqldb"].ToString();
        public string AzEFConString => ConfigurationManager.ConnectionStrings["SQLAZURECONNSTR_aztgsqldbEntities"].ToString();
    }
}
