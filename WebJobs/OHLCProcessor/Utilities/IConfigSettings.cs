using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public interface IConfigSettings
    {
        string StartingTimeHour { get; }
        string StartingTimeMinute { get; }
        string EndingTimeHour { get; }
        string EndingTimeMinute { get; }
        string Exchange { get; }
        string PeriodInDays { get; }
        string DelayInMin { get; }
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
    }
}
