using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.SqlServer.Destructurers;
using Serilog.Sinks.MSSqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace TradeGenie
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ConfigSettings _config = new ConfigSettings();

                var columnOption = new ColumnOptions();
                columnOption.Store.Remove(StandardColumn.MessageTemplate);

                Log.Logger = new LoggerConfiguration()
                    .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                        .WithDefaultDestructurers()
                        .WithDestructurers(new[] { new SqlExceptionDestructurer() }))
                    .MinimumLevel.Debug()
                    //.WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception} {Properties:j}")
                    //.WriteTo.File(new CompactJsonFormatter(), "log.txt")
                    .WriteTo.MSSqlServer(_config.AzSQLConString, "Logs", columnOptions: columnOption)
                    .CreateLogger();

                Log.Information("START " + AuxiliaryMethods.GetCurrentIndianTimeStamp());

                AlgoTrade trade = new AlgoTrade();
                trade.StartTrading();

                Log.Information("END " + AuxiliaryMethods.GetCurrentIndianTimeStamp());
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Main Exception");
            }
        }
    }
}
