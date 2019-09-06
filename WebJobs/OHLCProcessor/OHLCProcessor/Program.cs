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

        }
    }
}
