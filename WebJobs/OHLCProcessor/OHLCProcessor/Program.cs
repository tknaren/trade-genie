using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

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

                using (ProcessOHLC processOHLC = new ProcessOHLC())
                {
                    Console.WriteLine("Started @" + DateTime.Now.ToString());

                    processOHLC.ProcessOHLCMain();

                    Console.WriteLine("Ended @" + DateTime.Now.ToString());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        //public static void StartEngine([TimerTrigger("0 */1 3-11 * * 1-5")] TimerInfo timerInfo, TextWriter log)
        //{
        //    using (ProcessOHLC processOHLC = new ProcessOHLC())
        //    {
        //        log.WriteLine("Started @" + DateTime.Now.ToString());

        //        processOHLC.ProcessOHLCMain();

        //        log.WriteLine("Ended @" + DateTime.Now.ToString());
        //    }
        //}
    }
}
