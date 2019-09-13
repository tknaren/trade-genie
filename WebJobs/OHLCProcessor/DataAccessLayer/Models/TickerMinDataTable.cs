using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public partial class TickerMinDataTable : DataTable
    {
        public TickerMinDataTable()
        {
            this.Clear();

            this.Columns.Add(new DataColumn("InstrumentToken", typeof(int)));
            this.Columns.Add(new DataColumn("TradingSymbol", typeof(string)));
            this.Columns.Add(new DataColumn("DateTime", typeof(DateTime)));
            this.Columns.Add(new DataColumn("Open", typeof(decimal)));
            this.Columns.Add(new DataColumn("High", typeof(decimal)));
            this.Columns.Add(new DataColumn("Low", typeof(decimal)));
            this.Columns.Add(new DataColumn("Close", typeof(decimal)));
            this.Columns.Add(new DataColumn("Volume", typeof(int)));
        }

        public void AddRow(int instrumentToken, string tradingSymbol, DateTime date,
            decimal open, decimal high, decimal low, decimal close, int volume)
        {
            this.Rows.Add(instrumentToken, tradingSymbol, date, open, high, low, close, volume);
        }
    }
}
