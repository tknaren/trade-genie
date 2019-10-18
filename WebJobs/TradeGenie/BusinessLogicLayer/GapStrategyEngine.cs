using APIInterfaceLayer;
using DataAccessLayer;
using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace BusinessLogicLayer
{
    public interface IGapStrategyEngine
    {
        /*
            Strategy

            Trade Pre-requisites
            System should be logged-on by the user. 
            Make sure the previous day's day end data is present for all the stocks.
        
            Trade Setup
            At 9:16 run the Ticker Loader for all the stocks and get the First minute data
            Run the "RealTimeGapOpenedScripts" and get the scripts that are potential to Trade.
            Calculate the Support and Resistance for all the stocks based on the Logic and hold that in the table.
            Get the Traded Value for the previous day and determine if the stock is worth to Trade.
        
            Trade
            At 9:17, get the CMP for all the filtered stocks.
            Check the CMP to the first S/R level. 
            Trade only if the margin is more than 0.5%
            If the CMP is greater/lesser than R1/S1, consider the next R2/S2 level. If greater/lesser pick the next immediate resistance/support level.
            RR Ratio to be half of the Target, so SL has to be placed based on the Entry CMP.
            Calculate the points for Target and SL from CMP.
            Place the OCO Order.
            If the OCO Order is rejected for a particular stock, place the CO order. 
            If CO Order, the Target order (LONG-SELL, SHORT-BUY) has to be placed separately.
            If CO Order cannot be placed, Place Intraday order. 
            If Intraday order, SL and Target orders has to be placed separately.

            Closing the Trade 
            Do Continuous monitoring of CMP of the scripts.
            If the time reaches 12pm, exit the positions at whatever point it may be.
            If the trade reaches the S2/R2, Modify the SL of the order to S1/R1
            Do the same when the Stock CMP reaches S3/R3 and S4/R4.
            Exit the position once the stock reaches the final S4/R4.

        */

        /*
            Calculate the 5 Levels of support (Gapup) and resistance (Gapdown)

            Gapup
            Support 1 - 1% from the order placed price.
            Support 2 - Open Price for the day
            Support 3 - Low Price for the day (at the time of order placement, i.e., at 9:17)
            Support 4 - High Price for previous day
            Support 5 - Close Price for previous day

            GapDown
            Resistance 1 - 1% from the order placed time
            Resistance 2 - Open price for the day
            Resistance 3 - High Price for the day (at the time of order placement, i.e., at 9:17)
            Resistance 4 - Low Price for the Previous day
            Resistance 5 - Close Price for the previous day.         
         
         */

        void RunGapStrategy();
    }

    public class GapStrategyEngine : IGapStrategyEngine
    {
        private readonly IConfigSettings _settings;
        private readonly IUpstoxInterface _upstoxInterface;
        private readonly IDBMethods _dBMethods;

        

        public GapStrategyEngine(IConfigSettings settings, IUpstoxInterface upstoxInterface, IDBMethods dBMethods)
        {
            _settings = settings;
            _upstoxInterface = upstoxInterface;
            _dBMethods = dBMethods;
        }

        public void RunGapStrategy()
        {
            List<GapOpenedScript> gapOpenedScripts = _dBMethods.GetRealTimeGapOpenedScripts();

            foreach (GapOpenedScript script in gapOpenedScripts)
            {
                script.CMP = _upstoxInterface.GetCurrentMarketPrice(script.TradingSymbol);
            }

        }

        private void CalculateSRLevels(GapOpenedScript script)
        {
            /*
            GapDown
            Resistance 1 - 1% from the order placed time
            Resistance 2 - Open price for the day
            Resistance 3 - High Price for the day (at the time of order placement, i.e., at 9:17)
            Resistance 4 - Low Price for the Previous day
            Resistance 5 - Close Price for the previous day.              

            P1 - Today's Open
            P2 - Today's High
            P3 - Prev Low
            P4 - Prev Close

            R&R 1:2 so 05% (SL) vs 1% (Target)

            Calculate varaince from CMP to various levels

            R1 - 0.5% - 1%
            R2 - 1% - 1.5%
            R3 - 1.5% - 2%
            R4 - > 2%

            */


            if (script.OrderType == AuxiliaryMethods.LONG)
            {
                script.Leveltype = LevelType.Resistance;

                Dictionary<string, double> priceLevels = new Dictionary<string, double>();

                string level = DetermineResistanceName(script.TodayOpen, script.CMP);

                if (!priceLevels.ContainsKey(level))
                    priceLevels.Add(level, script.CMP);
                else
                {
                    double val = 0.0;
                    if (priceLevels.TryGetValue(level, out val))
                    {
                        //val
                    }

                }


                //List <Level> priceLevels = new List<Level>
                //{
                //    new Level { Price = script.TodayOpen },
                //    new Level { Price = script.TodayHL },
                //    new Level { Price = script.YesterdayHL },
                //    new Level { Price = script.YesterdayClose }
                //};

                //foreach(Level level in priceLevels)
                //{
                //    level.LevelName = DetermineResistanceName(level.Price, script.CMP);
                //}


            }
        }

        private string DetermineResistanceName(double price, double cmp)
        {
            double percentDeviation = Math.Round(((price - cmp) / cmp * 100), 2);
            string levelName = string.Empty;

            if (percentDeviation >= 0.5 && percentDeviation < 1)
            {
                levelName = "R1";
            }
            else if (percentDeviation >= 1 && percentDeviation < 1.5)
            {
                levelName = "R2";
            }
            else if (percentDeviation >= 1.5 && percentDeviation < 2)
            {
                levelName = "R3";
            }
            else if (percentDeviation >= 2)
            {
                levelName = "R4";
            }
            else
            {
                levelName = "R0";
            }

            return levelName;

        }
    }
}
