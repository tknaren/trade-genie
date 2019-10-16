using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace DataAccessLayer
{
    public interface IDBMethods
    {

    }

    public class DBMethods : IDBMethods
    {
        private readonly IConfigSettings _configSettings;

        public DBMethods(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
        }
    }
}
