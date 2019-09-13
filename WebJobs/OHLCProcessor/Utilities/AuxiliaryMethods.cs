using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public class AuxiliaryMethods
    {
        public static DateTime ConvertUnixTimeStampToWindows(string unixTimeStamp)
        {
            string trimmedTime = unixTimeStamp.Substring(0, unixTimeStamp.Length - 3);

            double timestamp = Convert.ToDouble(trimmedTime);

            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            DateTime dtInUTC = origin.AddSeconds(timestamp);

            return TimeZoneInfo.ConvertTimeFromUtc(dtInUTC, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        }
    }
}
