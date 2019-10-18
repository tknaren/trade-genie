using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpstoxNet;
using Utilities;

namespace APIInterfaceLayer
{
    public interface IUpstoxInterface
    {
        double GetCurrentMarketPrice(string tradingSymbol);
    }

    public class UpstoxInterface : IUpstoxInterface
    {
        private readonly WebClientInterface _webClient;
        private readonly IConfigSettings _configSettings;

        public UpstoxInterface(IConfigSettings configSettings)
        {
            _configSettings = configSettings;
            _webClient = new WebClientInterface(_configSettings);
        }

        public double GetCurrentMarketPrice(string tradingSymbol)
        {
            double cmp = 0.0;

            try
            {
                
            }
            catch(Exception ex)
            {
                Log.Error(ex, "Error - GET CMP " + tradingSymbol);
            }

            return cmp;
        }
    }
}
