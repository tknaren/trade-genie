using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeGenie
{
    public static class UserConfiguration
    {
        public static void LoadUserConfiguration()
        {
            APIKey = ConfigurationManager.AppSettings["APIKey"];
            APISecret = ConfigurationManager.AppSettings["APISecret"];
            UserId = ConfigurationManager.AppSettings["UserId"];
            EMAsToCalculate = Convert.ToString(ConfigurationManager.AppSettings["EMAsToCalculate"]);
            RSIsToCalculate = Convert.ToString(ConfigurationManager.AppSettings["RSIsToCalculate"]);

            GooglePriceUrl = Convert.ToString(ConfigurationManager.AppSettings["GooglePriceUrl"]);
            Exchange = Convert.ToString(ConfigurationManager.AppSettings["Exchange"]);
            Period = Convert.ToString(ConfigurationManager.AppSettings["Period"]);
            IntervalInMin = Convert.ToString(ConfigurationManager.AppSettings["IntervalInMin"]);
            ValuesToFetch = Convert.ToString(ConfigurationManager.AppSettings["ValuesToFetch"]);
            RSIsToCalculate = Convert.ToString(ConfigurationManager.AppSettings["RSIsToCalculate"]);
            DownloadData = Convert.ToBoolean(ConfigurationManager.AppSettings["DownloadData"]);
            CalculateElderIndicators = Convert.ToBoolean(ConfigurationManager.AppSettings["CalculateElderIndicators"]);

            LogFile = Convert.ToString(ConfigurationManager.AppSettings["LogFile"]);
            ErrorLogFile = Convert.ToString(ConfigurationManager.AppSettings["ErrorLogFile"]);
            GenericLogFile = Convert.ToString(ConfigurationManager.AppSettings["GenericLogFile"]);

            MorningBreakoutInterval = Convert.ToInt32(ConfigurationManager.AppSettings["MorningBreakoutInterval"]);
            MorningOTMPInterval = Convert.ToInt32(ConfigurationManager.AppSettings["MorningOTMPInterval"]);
            KiteHistoryInterval = Convert.ToInt32(ConfigurationManager.AppSettings["KiteHistoryInterval"]);

            Capital = Convert.ToInt32(ConfigurationManager.AppSettings["Capital"]);
            MaxOrders = Convert.ToInt32(ConfigurationManager.AppSettings["MaxOrders"]);
            BracketMargin = Convert.ToInt32(ConfigurationManager.AppSettings["BracketMargin"]);

            CandleLengthPercentageHigh = Convert.ToDecimal(ConfigurationManager.AppSettings["CandleLengthPercentageHigh"]);
            CandleLengthPercentageLow = Convert.ToDecimal(ConfigurationManager.AppSettings["CandleLengthPercentageLow"]);
            UpperBandPercentage = Convert.ToDecimal(ConfigurationManager.AppSettings["UpperBandPercentage"]);
            EntryPercentage = Convert.ToDecimal(ConfigurationManager.AppSettings["EntryPercentage"]);
            ExitPercentage = Convert.ToDecimal(ConfigurationManager.AppSettings["ExitPercentage"]);
            StopLossPercentage = Convert.ToDecimal(ConfigurationManager.AppSettings["StopLossPercentage"]);
            PriceUpperBoundary = Convert.ToInt32(ConfigurationManager.AppSettings["PriceUpperBoundary"]);
            PriceLowerBoundary = Convert.ToInt32(ConfigurationManager.AppSettings["PriceLowerBoundary"]);
            TotalPurchaseValue = Convert.ToInt32(ConfigurationManager.AppSettings["TotalPurchaseValue"]);

            TimePeriodsToCalculate = Convert.ToString(ConfigurationManager.AppSettings["TimePeriodsToCalculate"]);

            TimePeriodLong = Convert.ToInt32(ConfigurationManager.AppSettings["TimePeriodLongInMin"]);
            TimePeriodShort = Convert.ToInt32(ConfigurationManager.AppSettings["TimePeriodShortInMin"]);

            Min5Timer = Convert.ToString(ConfigurationManager.AppSettings["Min5Timer"]);
            Min15Timer = Convert.ToString(ConfigurationManager.AppSettings["Min15Timer"]);
            Min30Timer = Convert.ToString(ConfigurationManager.AppSettings["Min30Timer"]);
            Min60Timer = Convert.ToString(ConfigurationManager.AppSettings["Min60Timer"]);
            TimerToRun = Convert.ToString(ConfigurationManager.AppSettings["TimerToRun"]);

            EMAPerDevBottom = Convert.ToDouble(ConfigurationManager.AppSettings["EMAPerDevBottom"]);
            EMAPerDevTop = Convert.ToDouble(ConfigurationManager.AppSettings["EMAPerDevTop"]);

            TradeGenieConString = Convert.ToString(ConfigurationManager.ConnectionStrings["TradeGenieConString"]);

            DelayInSec = Convert.ToInt32(ConfigurationManager.AppSettings["DelayInSec"]);

            InitialExitPercentage = Convert.ToDecimal(ConfigurationManager.AppSettings["InitialExitPercentage"]);
            SubsequentExitPercentage = Convert.ToDecimal(ConfigurationManager.AppSettings["SubsequentExitPercentage"]);
            StopLossPercentageLow = Convert.ToDecimal(ConfigurationManager.AppSettings["StopLossPercentageLow"]);
            StopLossPercentageHigh = Convert.ToDecimal(ConfigurationManager.AppSettings["StopLossPercentageHigh"]);
            EMAPercentageIncreaseLow = Convert.ToDecimal(ConfigurationManager.AppSettings["EMAPercentageIncreaseLow"]);
            EMAPercentageIncreaseHigh = Convert.ToDecimal(ConfigurationManager.AppSettings["EMAPercentageIncreaseHigh"]);
            
        }

        public static string APIKey { get; set; }
        public static string APISecret { get; set; }
        public static string UserId { get; set; }

        public static string TradeGenieConString { get; set; }

        public static string PublicToken { get; set; }
        public static string AccessToken { get; set; }

        public static string EMAsToCalculate { get; set; }
        public static string RSIsToCalculate { get; set; }

        public static string GooglePriceUrl { get; set; }
        public static string Exchange { get; set; }
        public static string Period { get; set; }
        public static string IntervalInMin { get; set; }
        public static string ValuesToFetch { get; set; }
        public static bool DownloadData { get; set; }
        public static bool CalculateElderIndicators { get; set; }

        public static string LogFile { get; set; }
        public static string ErrorLogFile { get; set; }
        public static string GenericLogFile { get; set; }

        public static int MorningBreakoutInterval { get; set; }
        public static int MorningOTMPInterval { get; set; }
        public static int KiteHistoryInterval { get; set; }

        public static int Capital { get; set; }
        public static int MaxOrders { get; set; }
        public static int BracketMargin { get; set; }

        public static decimal CandleLengthPercentageHigh { get; set; }
        public static decimal CandleLengthPercentageLow { get; set; }
        public static decimal UpperBandPercentage { get; set; }
        public static decimal EntryPercentage { get; set; }
        public static decimal ExitPercentage { get; set; }
        public static decimal StopLossPercentage { get; set; }
        public static int PriceUpperBoundary { get; set; }
        public static int PriceLowerBoundary { get; set; }
        public static int TotalPurchaseValue { get; set; }

        public static string TimePeriodsToCalculate { get; set; }

        public static int TimePeriodLong { get; set; }
        public static int TimePeriodShort { get; set; }
        public static string Min5Timer { get; set; }
        public static string Min15Timer { get; set; }
        public static string Min30Timer { get; set; }
        public static string Min60Timer { get; set; }
        public static string TimerToRun { get; set; }
        public static double EMAPerDevBottom { get; set; }
        public static double EMAPerDevTop { get; set; }
        public static decimal InitialExitPercentage { get; set; }
        public static decimal SubsequentExitPercentage { get; set; }
        public static decimal StopLossPercentageLow { get; set; }
        public static decimal StopLossPercentageHigh { get; set; }
        public static decimal EMAPercentageIncreaseLow { get; set; }
        public static decimal EMAPercentageIncreaseHigh { get; set; }

        public static int DelayInSec { get; set; }
    }
}
