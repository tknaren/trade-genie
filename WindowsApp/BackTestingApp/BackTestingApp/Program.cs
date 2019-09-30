using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.SqlServer.Destructurers;

namespace BackTestingApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IConfigSettings _config = new ConfigSettings();

                var columnOption = new ColumnOptions();
                columnOption.Store.Remove(StandardColumn.MessageTemplate);

                Log.Logger = new LoggerConfiguration()
                    .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                        .WithDefaultDestructurers()
                        .WithDestructurers(new[] { new SqlExceptionDestructurer() }))
                    .MinimumLevel.Debug()
                    .WriteTo.MSSqlServer(_config.AzSQLConString, "BackTestLogs", columnOptions: columnOption)
                    .CreateLogger();

                Log.Information("START " + AuxiliaryMethods.GetCurrentIndianTimeStamp());

                MarketStrategiesBackTester strategiesTester = new MarketStrategiesBackTester();
                strategiesTester.RunBackTest();

                Log.Information("END " + AuxiliaryMethods.GetCurrentIndianTimeStamp());
            }
            catch (Exception ex)
            {
                Log.Error(ex,"Main Exception");
            }
        }
    }
}
