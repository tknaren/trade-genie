using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public interface IConfigSettings
    {
        TimeSpan StartingTime { get; }
        TimeSpan EndingTime { get; }
        TimeSpan HistoryEndTime { get; }
        DateTime DayHistoryStartDate { get; }
        DateTime DayHistoryEndDate { get; }
        string Exchange { get; }
        string PeriodInDays { get; }
        string DelayInSec { get; }
        string IntervalInMin { get; }
        string APIKey { get; }
        string APISecret { get; }
        string RedirectUrl { get; }
        string UpstoxBaseUri { get; }
        string HistoricalAPI { get; }
        string UserAgent { get; }
        string StartDate { get; }
        string EndDate { get; }
        string AzSQLConString { get; }
        string AzEFConString { get; }
        string Min3Timer { get; }
        string Min5Timer { get; }
        string Min10Timer { get; }
        string Min15Timer { get; }
        string Min30Timer { get; }
        string Min60Timer { get; }
        string EODTimer { get; }
        

        string TimePeriodsToCalculate { get; }
        string EMAsToCalculate { get; }
        string EHEMAsToCalculate { get; }
        string VWMAsToCalculate { get; }
        string RSIsToCalculate { get; }
        string ForceIndexesToCalculate { get; }
        string ATRsToCalculate { get; }
        string SuperTrendMultipliers { get; }
        string EMADeviationPeriods { get; }

    }
}
