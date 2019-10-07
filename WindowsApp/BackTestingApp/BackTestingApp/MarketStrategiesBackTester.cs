using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using BusinessLogicLayer;
using DataAccessLayer;

namespace BackTestingApp
{
    public class MarketStrategiesBackTester
    {
        IConfigSettings _config;
        IDBMethods _dbMethods;

        public MarketStrategiesBackTester()
        {
            _config = new ConfigSettings();
            _dbMethods = new DBMethods();
        }

        public void RunBackTest()
        {
            GapOpeningStrategyTest gapTest = new GapOpeningStrategyTest(_dbMethods, _config);
            gapTest.RunGapOpeningStrategyTest();
        }
    }
}
