using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Serilog;
using Serilog.Exceptions;
using Serilog.Exceptions.SqlServer;
using Serilog.Exceptions.Core;
using Serilog.Exceptions.SqlServer.Destructurers;

namespace OHLCProcessor
{
    class Program
    {
        static void Main()
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.

            //if (System.Environment.UserInteractive)
            //{
            //    using (ProcessOHLC processOHLC = new ProcessOHLC())
            //    {
            //        processOHLC.ProcessOHLCMain();
            //    }
            //}
            //else
            //{
            //    var config = new JobHostConfiguration();
            //    config.UseTimers();

            //    if (config.IsDevelopment)
            //    {
            //        config.UseDevelopmentSettings();
            //    }

            //    var host = new JobHost(config);
            //    host.RunAndBlock();
            //}

            try
            {
                Log.Logger = new LoggerConfiguration()
                    .Enrich.WithExceptionDetails(new DestructuringOptionsBuilder()
                        .WithDefaultDestructurers()
                        .WithDestructurers(new[] { new SqlExceptionDestructurer() }))
                    .MinimumLevel.Debug()
                    .WriteTo.Console(outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception} {Properties:j}")
                    .CreateLogger();

                using (ProcessOHLC processOHLC = new ProcessOHLC())
                {
                    Log.Information("START");

                    processOHLC.StartEngine();

                    Log.Information("END");

                }
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Main Exception");
            }
        }
    }
}
