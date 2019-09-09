using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using DataAccessLayer;
using System.Collections;
using System.Collections.Generic;

namespace OHLCProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            if (!Environment.UserInteractive)
            {
                IDictionary envVariables = Environment.GetEnvironmentVariables();

                Console.WriteLine("Connection String " + envVariables["SQLAZURECONNSTR_AzTGSQLDBConString"].ToString());
                Console.WriteLine("Alphavantage " + envVariables["Alphavantage"].ToString());
            }
            else
            {
                var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json");

                var configuration = builder.Build();

                Console.WriteLine(configuration["AzTGSQLDBConString"].ToString());
                Console.WriteLine(configuration["Alphavantage"].ToString());
            }
            */

            IConfigSettings configSettings = new ConfigSettings();

            ProcessOHLC processOHLC = new ProcessOHLC(configSettings);

            processOHLC.ProcessOHLCMain();
        }
    }

    //Algorithm Steps
    /**
    
    1. Run a loop which has a thread delay of 1min.
    2. Connect to DB and get the current AccessToken for the session.
    3. Connect to MasterStockDetails and get all the intruments that need the history.
    4. Connect to the Upstox API and get the history for all the stoks one by one.
    5. Store that in the Azure DB.
    6. Run the Consolidater engine to run the consolidator program in DB.
    7. Run the indicator engine to load all the indicators to the DB. 

     */
}
