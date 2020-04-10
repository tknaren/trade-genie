using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeGenie
{
    public static class Logger
    {
        public static void LogToFile(string textToLog)
        {
            using (StreamWriter sw = new StreamWriter(UserConfiguration.LogFile, true))
            {
                sw.WriteLine(string.Format("{0},{1}", DateTime.Now.ToString(), textToLog));
            }
        }

        public static void ErrorLogToFile(Exception ex)
        {
            using (StreamWriter sw = new StreamWriter(UserConfiguration.ErrorLogFile, true))
            {
                while (ex != null)
                {
                    sw.WriteLine(string.Format("{0} -- {1} -- Stack Trace :: {2}", DateTime.Now.ToString(), ex.Message, ex.StackTrace));

                    ex = ex.InnerException;
                }
            }
        }

        public static void GenericLog(string textToLog)
        {
            using (StreamWriter sw = new StreamWriter(UserConfiguration.GenericLogFile, true))
            {
                sw.WriteLine(string.Format("{0},{1}", DateTime.Now.ToString(), textToLog));
            }
        }
    }
}
