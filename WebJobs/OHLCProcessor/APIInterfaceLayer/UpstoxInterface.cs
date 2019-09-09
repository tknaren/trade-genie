using System;
using UpstoxNet;

namespace APIInterfaceLayer
{
    public class UpstoxInterface
    {
        public bool SetUpstoxAccessToken(string accesToken)
        {
            bool isSuccessful = false;

            try
            {
                Upstox upstox = new Upstox();

                isSuccessful = upstox.SetAccessToken(accesToken);
            }
            catch (Exception ex)
            {
                throw ex;                
            }

            return isSuccessful;
        }
    }
}
