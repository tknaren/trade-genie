﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class AuxiliaryMethods
    {
        public const string TREND_UP = "UP";
        public const string TREND_DOWN = "DOWN";
        public const string TREND_CHANGE_UP = "CHANGE-UP";
        public const string TREND_CHANGE_DOWN = "CHANGE-DOWN";

        public static DateTime ConvertUnixTimeStampToWindows(string unixTimeStamp)
        {
            string trimmedTime = unixTimeStamp.Substring(0, unixTimeStamp.Length - 3);

            double timestamp = Convert.ToDouble(trimmedTime);

            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

            DateTime dtInUTC = origin.AddSeconds(timestamp);

            return TimeZoneInfo.ConvertTimeFromUtc(dtInUTC, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        }

        public static DateTime GetCurrentIndianTimeStamp()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        }

        public static DateTime ToIndianTimeStamp(this DateTime dateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dateTime.ToUniversalTime(), TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        }

        public static string ToKiteInternal(this int timePeriod)
        {
            string timePeriodText = string.Empty;

            switch (timePeriod)
            {
                case 1:
                    timePeriodText = "minute";
                    break;
                case 3:
                    timePeriodText = "3minute";
                    break;
                case 5:
                    timePeriodText = "5minute";
                    break;
                case 10:
                    timePeriodText = "10minute";
                    break;
                case 15:
                    timePeriodText = "15minute";
                    break;
                case 30:
                    timePeriodText = "30minute";
                    break;
                case 60:
                    timePeriodText = "60minute";
                    break;
            }

            return timePeriodText;
        }

        public static List<TimeSpan> MinuteTimer(string timerString)
        {
            List<string> minList = timerString.Split(',').ToList();

            List<TimeSpan> minTimeSpanList = new List<TimeSpan>();

            foreach (string item in minList)
            {
                TimeSpan ts = new TimeSpan(Convert.ToInt32(item.Split(':')[0]), Convert.ToInt32(item.Split(':')[1]), 0);

                minTimeSpanList.Add(ts);
            }

            return minTimeSpanList;
        }

        public static double ToMinute(this string KiteInterval)
        {
            double minute = 0.0;

            switch (KiteInterval)
            {
                case "minute":
                    minute = 1;
                    break;
                case "3minute":
                    minute = 3;
                    break;
                case "5minute":
                    minute = 5;
                    break;
                case "10minute":
                    minute = 10;
                    break;
                case "15minute":
                    minute = 15;
                    break;
                case "30minute":
                    minute = 30;
                    break;
                case "60minute":
                    minute = 60;
                    break;
            }

            return minute;
        }

        public static DataTable ToDataTable<T>(this List<T> iList)
        {
            DataTable dataTable = new DataTable();
            PropertyDescriptorCollection propertyDescriptorCollection =
                TypeDescriptor.GetProperties(typeof(T));
            for (int i = 0; i < propertyDescriptorCollection.Count; i++)
            {
                PropertyDescriptor propertyDescriptor = propertyDescriptorCollection[i];
                Type type = propertyDescriptor.PropertyType;

                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                    type = Nullable.GetUnderlyingType(type);


                dataTable.Columns.Add(propertyDescriptor.Name, type);
            }
            object[] values = new object[propertyDescriptorCollection.Count];
            foreach (T iListItem in iList)
            {
                for (int i = 0; i < values.Length; i++)
                {
                    values[i] = propertyDescriptorCollection[i].GetValue(iListItem);
                }
                dataTable.Rows.Add(values);
            }
            return dataTable;
        }

        public static DataTable ToDataTable<T>(this IEnumerable<T> data)
        {
            PropertyDescriptorCollection properties =
                TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;
        }

    }
}
