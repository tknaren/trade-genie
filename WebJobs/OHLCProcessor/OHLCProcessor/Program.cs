using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using CoreLayer;
using APIInterfaceLayer;
using DataAccessLayer;

namespace OHLCProcessor
{
    class Program
    {
        static void Main(string[] args)
        {
            // The code provided will print ‘Hello World’ to the console.
            // Press Ctrl+F5 (or go to Debug > Start Without Debugging) to run your app.
            IConfigSettings configSettings = new ConfigSettings();
            IUpstoxInterface upstoxInterface = new UpstoxInterface();
            IDBMethods dBMethods = new DBMethods();

            ProcessOHLC processOHLC = new ProcessOHLC(configSettings, upstoxInterface, dBMethods);

            processOHLC.ProcessOHLCMain();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }
    }
}
