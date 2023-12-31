using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Configuration;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using Serilog.Exceptions;
using Serilog.Exceptions.SqlServer;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.SqlServer.Destructurers;
using Utilities;
using Microsoft.Azure;
using Serilog.Formatting.Compact;

namespace OHLCProcessor
{
    class Program
    {
        static void Main()
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
                    //.WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception} {Properties:j}")
                    //.WriteTo.File(new CompactJsonFormatter(), "log.txt")
                    .WriteTo.MSSqlServer(_config.AzSQLConString, "Logs", columnOptions: columnOption)
                    .CreateLogger();
                    
                Log.Information("START " + AuxiliaryMethods.GetCurrentIndianTimeStamp());

                ProcessOHLC processOHLC = new ProcessOHLC();
                processOHLC.StartEngine();

                Log.Information("END " + AuxiliaryMethods.GetCurrentIndianTimeStamp());
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Main Exception");
            }
        }
    }
}
