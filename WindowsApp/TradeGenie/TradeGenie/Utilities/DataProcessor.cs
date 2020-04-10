using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Globalization;
using System.Threading;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using TradeGenie.Repository;

namespace TradeGenie
{
    /// <summary>
    /// Processes the raw data from Google and transforms it into something
    /// more readable.
    /// </summary>
    public class DataProcessor {
        /// <summary>
        /// Data columns enumeration.
        /// </summary>
        private enum Columns { date = 0, close = 1, open = 4, high = 2, low = 3, volume = 5 };

        /// <summary>
        /// Beginning of the field that defines the columns of data.
        /// </summary>
        private const string COLUMNS_ROW_BEGINNING = "COLUMNS=";

        /// <summary>
        /// Expected value of the columns field. Currently, Google always returns the
        /// same order no matter how the URL has been invoked. This constant is used to check that 
        /// no arbitrary order is suddenly used. 
        /// </summary>
        private const string COLUMNS_ROW = COLUMNS_ROW_BEGINNING + "DATE,CLOSE,HIGH,LOW,OPEN,VOLUME";

        /// <summary>
        /// Number of columns.
        /// </summary>
        private const int EXPECTED_NUMBER_OF_FIELDS = 6;

        /// <summary>
        /// Beginning of an absolute date value. 
        /// </summary>
        private const string BEGINNING_OF_DATE = "a";
        
        /// <summary>
        /// Beginning of the row with the timezone offset for the current exchange. 
        /// </summary>
        private const string TIME_ZONE_OFFSET_ROW_BEGINNING = "TIMEZONE_OFFSET=";

        #region Public methods
        /// <summary>
        /// Recovers the current (sometimes real-time) quote from the input stream.
        /// To be used in conjunction with DownloadUriBuilder.getGetPricesUrlForLastQuote().
        /// Extracts today's open, high, low, close and volume (OHLCV) from 'stream'. 
        /// To do this, it scans the returned data. The 'open' is the first value, the 'close'
        /// the last, the high and the low are the highest and lowest of all of the values
        /// returned and the volume is the sum of all the volume fields.
        /// </summary>                
        /// <param name="errorMessage">Output argument. Describes an error.</param>
        public String processStreamMadeOfOneMinuteBarsToExtractMostRecentOHLCVForCurrentDay(Stream stream, out string errorMessage) {
            //Se the culture to the invariant culture. This ensures the
            //retrieved data will be interpreted correctly.
            CultureInfo originalCulture = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            try {
                errorMessage = string.Empty;
                bool processingData = false;

                int offset = int.MinValue;
                float totalVolume = 0L;
                float open = float.NaN, high = float.MinValue, low = float.MaxValue, close = float.NaN;
                DateTime dateGoogle = DateTime.MinValue;

                int counterOfLines = 0;
                string lastLine = null; //Last line, used to recover the close value from it.
                string[] elements = null;

                //Anonymous method that recovers the value of a column.
                ExtractValue extractValue = delegate(Columns column, out float value, out string localMessage) {
                    localMessage = null;
                    if (!float.TryParse(elements[(int)column], out value)) {
                        localMessage = "Unable to retrieve' " + Enum.GetName(typeof(Columns), open) + "'. Line: " + counterOfLines;
                        return false;
                    }
                    else {
                        return true;
                    }
                };

                //Reads the file
                using (StreamReader inputStream = new StreamReader(stream)) {
                    string line;
                    while ((line = inputStream.ReadLine()) != null) {
                        line = line.Trim();
                        bool isFirstLineOfData = false;
                        counterOfLines++;

                        if (String.IsNullOrEmpty(line)) {
                            Debug.Fail("Empty line!");
                            continue;
                        }

                        //Extract information from the file headers.
                        if (!processingData) {
                            if (line.StartsWith(COLUMNS_ROW_BEGINNING)) {
                                if (line != COLUMNS_ROW) {
                                    errorMessage = "Unexpected fields ('" + line + "')";
                                    return null;
                                }
                            }
                            else if (line.StartsWith(TIME_ZONE_OFFSET_ROW_BEGINNING)) {
                                if (!int.TryParse(line.Substring(TIME_ZONE_OFFSET_ROW_BEGINNING.Length), out offset)) {
                                    errorMessage = "Error extracting time zone offset";
                                    return null;
                                }
                            }
                            else if (line.StartsWith(BEGINNING_OF_DATE)) {
                                processingData = true;
                                isFirstLineOfData = true;
                            }
                        }

                        //Process the lines with the data.
                        elements = null;
                        if (processingData) {
                            lastLine = line; //It'll be used later to retrieve the close value.

                            if (!getElements(line, counterOfLines, ref elements, out errorMessage)) {

                                return null;
                            }

                            if (isFirstLineOfData) {
                                isFirstLineOfData = false;

                                if (offset == int.MinValue) {
                                    Debug.Fail("Timezone offset unitialized.");
                                    offset = 0;
                                }

                                if (!getGoogleDate(elements, out dateGoogle, offset)) {
                                    errorMessage = "Unable to retrieve date. Line: '" + line + "'.";
                                    return null;
                                }

                                if (!extractValue(Columns.open, out open, out errorMessage)) {
                                    return null;
                                }
                            }

                            //Process high and low.
                            float localHigh;
                            if (!extractValue(Columns.high, out localHigh, out errorMessage)) {
                                return null;
                            }

                            if (localHigh > high)
                                high = localHigh;

                            float localLow;
                            if (!extractValue(Columns.low, out localLow, out errorMessage)) {
                                return null;
                            }

                            if (localLow < low) {
                                low = localLow;
                            }

                            //Process volume
                            float localVolume;
                            if (!extractValue(Columns.volume, out localVolume, out errorMessage)) {
                                return null;
                            }
                            totalVolume += localVolume;
                        }
                    }
                }

                //Checks that the file is valid.
                if (!processingData) {
                    errorMessage = "File with no data!";
                    return null;
                }

                //Retrieves the close (from the last line).
                if (!String.IsNullOrEmpty(lastLine)) {
                    if (!extractValue(Columns.close, out close, out errorMessage)) {
                        return null;
                    }
                }

                //Checks that all values have been initialized.
                float[] allValues = { open, high, low, close };

                for (int i = 0; i < allValues.Length; i++) {
                    if (float.IsNaN(allValues[i])) {
                        errorMessage = "Unexpected error. Some values weren't initialized.";
                        return null;
                    }
                }

                if (totalVolume < 0) {
                    errorMessage = "Incorrect volume. It can't be negative.";
                    return null;
                }

                //Initializes the CSVLine object.                        
                return formatData(dateGoogle, open, high, low, close, totalVolume);
            } finally {
                //Retrieve the original culture
                if (originalCulture != null)
                    Thread.CurrentThread.CurrentCulture = originalCulture;
                else
                    Debug.Fail("No original culture defined!");
            }
        }


        /// <summary>
        /// To be used together with DownloadUriBuilder.getGetPricesUriToDownloadAllData() and 
        /// DownloadUriBuilder.getGetPricesUriForRecentData(). Transforms the input into a CSV
        /// file with Date, Open, High, Low, Close and Volume headers and a line per day. 
        /// </summary>
        /// <param name="errorMessage">Output argument. Describes an error.</param>        
        public String processStreamMadeOfOneDayLinesToExtractHistoricalData(Stream str, out string errorMessage) {
            errorMessage = String.Empty;

            StringBuilder stringResult = new StringBuilder();

            stringResult.AppendLine("Date,Open,High,Low,Close,Volume");

            StreamReader sr = new StreamReader(str);

            string line;
            DateTime lastInterpretedDate = DateTime.MaxValue;
            DateTime previousDate = DateTime.MaxValue;
            bool processingData = false;

            //Process the retrieved file.
            int numberOfLines = 0;
            bool atLeastOneLineProcessed = false;
            while ((line = sr.ReadLine()) != null) {
                numberOfLines++;

                if (!processingData) {
                    if (line.StartsWith(BEGINNING_OF_DATE)) {
                        processingData = true;
                    }
                    else {
                        continue;
                    }
                }

                if (line.StartsWith(TIME_ZONE_OFFSET_ROW_BEGINNING)) { //From time to time, information about the time zone offset is inserted. It's ignored.
                    continue;
                }

                string[] elements = null;
                if (!getElements(line, numberOfLines, ref elements, out errorMessage)) {
                    return null;
                }

                DateTime dt;
                if (elements[(int)Columns.date].StartsWith(BEGINNING_OF_DATE)) {
                    dt = lastInterpretedDate = ConvertFromUnixTimestamp(double.Parse(elements[0].Substring(1)));
                }
                else {
                    int days2Add;
                    if (!int.TryParse(elements[0], out days2Add)) {
                        errorMessage = "Invalid line " + numberOfLines + ": '" + line + "'.";
                        return null;
                    }
                    dt = lastInterpretedDate.AddDays(int.Parse(elements[0]));
                }

                if (dt.Date == previousDate) { //There are lines with repeated dates when a day is a holiday.
                    continue;
                }

                string convertedLine = formatData(dt, elements[(int)Columns.open], elements[(int)Columns.high], elements[(int)Columns.low],
                    elements[(int)Columns.close], elements[(int)Columns.volume]);

                stringResult.AppendLine(convertedLine);
                previousDate = dt.Date;

                if (!atLeastOneLineProcessed)
                    atLeastOneLineProcessed = true;
            }

            //Returns the stream (null if no data has been processed).
            if (!atLeastOneLineProcessed) {
                errorMessage = "No data recovered.";
                return string.Empty;
            }
            else {
                return stringResult.ToString();
            }
        }

        public String processStreamMadeOfOneMinuteLinesToExtractHistoricalData(Stream str, out string errorMessage, out List<string> outputInList)
        {
            errorMessage = String.Empty;
            outputInList = new List<string>();

            StringBuilder stringResult = new StringBuilder();

            stringResult.AppendLine("Date,Open,High,Low,Close,Volume");

            StreamReader sr = new StreamReader(str);

            string line;
            DateTime lastInterpretedDate = DateTime.MaxValue;
            DateTime previousDate = DateTime.MaxValue;
            bool processingData = false;

            //Process the retrieved file.
            int numberOfLines = 0;
            bool atLeastOneLineProcessed = false;
            while ((line = sr.ReadLine()) != null)
            {
                numberOfLines++;

                if (!processingData)
                {
                    if (line.StartsWith(BEGINNING_OF_DATE))
                    {
                        processingData = true;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (line.StartsWith(TIME_ZONE_OFFSET_ROW_BEGINNING))
                { //From time to time, information about the time zone offset is inserted. It's ignored.
                    continue;
                }

                string[] elements = null;
                if (!getElements(line, numberOfLines, ref elements, out errorMessage))
                {
                    return null;
                }

                DateTime dt;
                if (elements[(int)Columns.date].StartsWith(BEGINNING_OF_DATE))
                {
                    dt = lastInterpretedDate = ConvertFromUnixTimestamp(double.Parse(elements[0].Substring(1))).Date;
                    TimeSpan ts = new TimeSpan(09, 15, 0);
                    dt = lastInterpretedDate = lastInterpretedDate + ts;
                }
                else
                {
                    int days2Add;
                    if (!int.TryParse(elements[0], out days2Add))
                    {
                        errorMessage = "Invalid line " + numberOfLines + ": '" + line + "'.";
                        return null;
                    }
                    dt = lastInterpretedDate.AddMinutes(int.Parse(elements[0]));
                }

                //if (dt.Date == previousDate)
                //{ //There are lines with repeated dates when a day is a holiday.
                //    continue;
                //}

                string convertedLine = formatDataWithTime(dt, elements[(int)Columns.open], elements[(int)Columns.high], elements[(int)Columns.low],
                    elements[(int)Columns.close], elements[(int)Columns.volume]);

                outputInList.Add(convertedLine);

                stringResult.AppendLine(convertedLine);
                previousDate = dt.Date;

                if (!atLeastOneLineProcessed)
                    atLeastOneLineProcessed = true;
            }

            //Returns the stream (null if no data has been processed).
            if (!atLeastOneLineProcessed)
            {
                errorMessage = "No data recovered.";
                return string.Empty;
            }
            else
            {
                return stringResult.ToString();
            }
        }

        public List<TickerElderIndicatorsVM> extractHistoricalData(Stream str, int instrumentToken, string stockCode, int minuteInterval, out string errorMessage)
        {
            errorMessage = String.Empty;
            List<TickerElderIndicatorsVM> outputInList = new List<TickerElderIndicatorsVM>();

            StringBuilder stringResult = new StringBuilder();

            stringResult.AppendLine("Date,Open,High,Low,Close,Volume");

            StreamReader sr = new StreamReader(str);

            string line;
            DateTime lastInterpretedDate = DateTime.MaxValue;
            DateTime previousDate = DateTime.MaxValue;
            bool processingData = false;

            //Process the retrieved file.
            int numberOfLines = 0;
            bool atLeastOneLineProcessed = false;
            while ((line = sr.ReadLine()) != null)
            {
                numberOfLines++;

                if (!processingData)
                {
                    if (line.StartsWith(BEGINNING_OF_DATE))
                    {
                        processingData = true;
                    }
                    else
                    {
                        continue;
                    }
                }

                if (line.StartsWith(TIME_ZONE_OFFSET_ROW_BEGINNING))
                { //From time to time, information about the time zone offset is inserted. It's ignored.
                    continue;
                }

                string[] elements = null;
                if (!getElements(line, numberOfLines, ref elements, out errorMessage))
                {
                    return null;
                }

                DateTime dt;
                if (elements[(int)Columns.date].StartsWith(BEGINNING_OF_DATE))
                {
                    dt = lastInterpretedDate = ConvertFromUnixTimestamp(double.Parse(elements[0].Substring(1))).Date;
                    TimeSpan ts = new TimeSpan(09, 15, 0);
                    dt = lastInterpretedDate = lastInterpretedDate + ts;
                }
                else
                {
                    int days2Add;
                    if (!int.TryParse(elements[0], out days2Add))
                    {
                        errorMessage = "Invalid line " + numberOfLines + ": '" + line + "'.";
                        return null;
                    }

                    dt = lastInterpretedDate.AddMinutes(minuteInterval * int.Parse(elements[0]));
                }

                //if (dt.Date == previousDate)
                //{ //There are lines with repeated dates when a day is a holiday.
                //    continue;
                //}

                string convertedLine = formatDataWithTime(dt, elements[(int)Columns.open], elements[(int)Columns.high], elements[(int)Columns.low],
                    elements[(int)Columns.close], elements[(int)Columns.volume]);

                TickerElderIndicatorsVM ticker = new TickerElderIndicatorsVM()
                {
                    TradingSymbol = stockCode,
                    TickerDateTime = dt,
                    TimePeriod = minuteInterval,
                    InstrumentToken = instrumentToken,
                    PriceOpen = Convert.ToDouble(elements[(int)Columns.open]),
                    PriceHigh = Convert.ToDouble(elements[(int)Columns.high]),
                    PriceLow = Convert.ToDouble(elements[(int)Columns.low]),
                    PriceClose = Convert.ToDouble(elements[(int)Columns.close]),
                    Volume = Convert.ToInt32(elements[(int)Columns.volume])
                };

                outputInList.Add(ticker);

                stringResult.AppendLine(convertedLine);
                previousDate = dt.Date;

                if (!atLeastOneLineProcessed)
                    atLeastOneLineProcessed = true;
            }

            //Returns the stream (null if no data has been processed).
            if (!atLeastOneLineProcessed)
            {
                errorMessage = "No data recovered.";
            }

            return outputInList;
        }


        #endregion

        #region Auxiliary methods
        /// <summary>
        /// Used later to define an anonymous method.
        /// </summary>        
        delegate bool ExtractValue(Columns columnNumber, out float value, out string localMessage);

        /// <summary>
        /// Creates a DateTime object from a UNIX time stamp.
        /// </summary>        
        private DateTime ConvertFromUnixTimestamp(double timestamp) {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            return origin.AddSeconds(timestamp);
        }
        
        /// <summary>
        /// Throws an exception if a line's columns doesn't match
        /// the expected format.
        /// </summary>
        /// <param name="elements"></param>
        private void checkElements(string[] elements) {
            if (elements == null || elements.Length != EXPECTED_NUMBER_OF_FIELDS)
                throw new ArgumentException("'elements' was null or had an unexpected number of fields.");
        }

        /// <summary>
        /// Retrieves the google date form 'elements'. Invoked over
        /// the first field of the first line. 
        /// </summary>
        /// <returns>'False' if the date can't be retrieved.</returns>
        private bool getGoogleDate(string[] elements, out DateTime googleDate, int offset) {
            checkElements(elements);
            googleDate = DateTime.MinValue;

            Regex regexFirstDate = new Regex(@"a\d+");
            if (!regexFirstDate.IsMatch(elements[0])) {
                return false;
            }

            if (offset == int.MinValue) {
                throw new ArgumentException("Invalid value for offset.");
            }

            long value;
            if (!long.TryParse(elements[0].Substring(1), out value)) {
                return false;
            }


            googleDate = new DateTime(ConvertFromUnixTimestamp(value).Ticks).AddMinutes(offset).Date;
            return true;
        }

        /// <summary>
        /// Returns a "CSV" line with date, open, high, low, close and volume fields.
        /// </summary>
        private string formatData<T>(DateTime date, T open, T high, T low, T close, T volume) {
            return String.Format("{0},{1},{2},{3},{4},{5}", date.ToString("yyyy-MM-dd"),
                    open, high, low, close, volume);
        }

        private string formatDataWithTime<T>(DateTime date, T open, T high, T low, T close, T volume)
        {
            return String.Format("{0},{1},{2},{3},{4},{5}", date.ToString("yyyy-MM-dd HH:mm"),
                    open, high, low, close, volume);
        }


        /// <summary>
        /// Gets the columns from a line of data. Check that they are valid. 
        /// </summary>        
        private static bool getElements(string line, int counterOfLines, ref string[] elements, out string message) {
            message = String.Empty;
            if (String.IsNullOrEmpty(line))
                throw new ArgumentException("'line' empty or null.");

            elements = line.Split(',');

            bool returnValue = elements != null && elements.Length == EXPECTED_NUMBER_OF_FIELDS;
            if (!returnValue) {
                message = "Unexpected number of fields at line '" + counterOfLines + "'.";
            }
            return returnValue;
        }
        #endregion
    }
}
