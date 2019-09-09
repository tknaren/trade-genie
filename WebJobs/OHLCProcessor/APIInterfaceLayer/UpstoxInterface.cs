using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpstoxNet;
using CoreLayer;

namespace APIInterfaceLayer
{
    public class UpstoxInterface : IUpstoxInterface
    {
        Upstox upstox;

        public UpstoxInterface()
        {
            upstox = new Upstox();
        }

        public bool InitializeUpstox(string apiKey, string apiSecret, string redirectUrl)
        {
            bool isInitialized = false;

            try
            {
                upstox.Api_Key = apiKey;
                upstox.Api_Secret = apiSecret;
                upstox.Redirect_Url = redirectUrl;

                isInitialized = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while initializing Upstox: {0}", ex.Message);

                throw ex;
            }

            return isInitialized;
        }

        public bool SetUpstoxAccessToken(string accesToken)
        {
            bool isSuccessful = false;

            try
            {
                isSuccessful = upstox.SetAccessToken(accesToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while setting Access Token: {0}", ex.Message);

                throw ex;
            }

            return isSuccessful;
        }
    }
}
