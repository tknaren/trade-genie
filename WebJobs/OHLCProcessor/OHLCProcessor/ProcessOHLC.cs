using System;
using System.Collections.Generic;
using System.Text;
using APIInterfaceLayer;
using DataAccessLayer;

namespace OHLCProcessor
{
    public class ProcessOHLC
    {
        //Get Latest Access Token from DB
        //Set Access Token and login to Upstox
        //Get Master Stock Info
        //Get Config info for downlading history
        //Get History from Upstox
        //Format and store in DB

        //Call Consolidator Engine
        //Call Indicator Engine

        private readonly IConfigSettings _settings;

        public ProcessOHLC(IConfigSettings settings)
        {
            _settings = settings;
        }

        public void ProcessOHLCMain()
        {
            string accessToken = string.Empty;
            bool isSuccessfulLogin = false;
            DBMethods dBMethods = new DBMethods();
            UpstoxInterface upstoxInterface = new UpstoxInterface();

            try
            {
                var iInMin = _settings.IntervalInMin;

                accessToken = dBMethods.GetLatestAccessToken();

                if (!string.IsNullOrEmpty(accessToken))
                {
                    isSuccessfulLogin = upstoxInterface.SetUpstoxAccessToken(accessToken);
                }

                Console.WriteLine("Is Login Successful: {0}", isSuccessfulLogin.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                // Log the exception as this is the front interface
            }
        }
    }
}
