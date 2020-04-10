using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TradeGenie
{
    public static class Utilities
    {
        //public const decimal CandleLengthPercentageHigh = 1.75M;
        //public const decimal CandleLengthPercentageLow = 0.5M;
        //public const decimal UpperBandPercentage = 1.005M;
        //public const decimal EntryPercentage = 1.0005M;
        //public const decimal ExitPercentage = 1.003M;
        //public const decimal StopLossPercentage = 1.0075M;
        //public const decimal TotalPurchaseValue = 100000;
        //public const decimal PriceUpperBoundary = 3000;
        //public const decimal PriceLowerBoundary = 300;

        public const string UpwardMovement = "UP";
        public const string DownwardMovement = "DOWN";
        public const string BuyOrder = "BUY";
        public const string SellOrder = "SELL";
        public const string LongPosition = "LONG";
        public const string ShortPosition = "SHORT";

        public const string MarketOrder = "MARKET";
        public const string LimitOrder = "LIMIT";

        public const string MISProduct = "MIS";
        public const string CNCProduct = "CNC";
        public const string NRMLProduct = "NRML";
        public const string BOProduct = "BO";
        public const string COProduct = "CO";

        public const string OrderValidity = "DAY";

        public const string RealTimeOrderPlacementON = "REAL TIME ON";
        public const string RealTimeOrderPlacementOFF = "REAL TIME OFF";

        public const string RealTimeOrderRefreshON = "ORDER REFRESH ON";
        public const string RealTimeOrderRefreshOFF = "ORDER REFRESH OFF";

        public const string OS_Open = "OPEN";
        public const string OS_Complete = "COMPLETE";
        public const string OS_TriggerPending = "TRIGGER PENDING";
        public const string OS_Cancelled = "CANCELLED";

        public enum ImpulseColor
        {
            BLUE,
            GREEN,
            RED,
            NA
        }

        public enum HistogramMovement
        {
            INCREASING,
            DECREASING,
            NA
        }

        public enum Trend
        {
            UP,
            DOWN,
            FLAT,
            NA
        }

        public enum PriceRange
        {
            VERYLOW,
            LOW,
            MEDIUM,
            HIGH,
            VERYHIGH,
            NA
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

                if (type.Name == "UInt32")
                {
                    type = typeof(System.Int32);
                }

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

        public static Func<T, object> GetOrderByExpression<T>(string sortColumn)
        {
            Func<T, object> orderByExpr = null;
            if (!String.IsNullOrEmpty(sortColumn))
            {
                Type sponsorResultType = typeof(T);

                if (sponsorResultType.GetProperties().Any(prop => prop.Name == sortColumn))
                {
                    System.Reflection.PropertyInfo pinfo = sponsorResultType.GetProperty(sortColumn);
                    orderByExpr = (data => pinfo.GetValue(data, null));
                }
            }
            return orderByExpr;
        }

        public static List<T> OrderByDir<T>(IEnumerable<T> source, string dir, Func<T, object> OrderByColumn)
        {
            return dir.ToUpper() == "ASC" ? source.OrderBy(OrderByColumn).ToList() : source.OrderByDescending(OrderByColumn).ToList();
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

    }
}
