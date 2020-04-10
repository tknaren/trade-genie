using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeGenie
{
    public class MarginCalculator
    {
        public decimal CalculatEquityMargin(decimal trigger, decimal price, decimal stoploss, int quantity, string transaction_type)
        {
            decimal co_lower = 0.0M;
            decimal co_upper = 0.0M;
            decimal margin = 0.0M;

            co_lower = co_lower / 100;
            co_upper = co_upper / 100;

            trigger = price - (co_upper * price);

            decimal x = 0.0M;

            if (transaction_type == "buy")
            {
                x = (price - trigger) * quantity;

                if (stoploss < trigger)
                    stoploss = trigger;
                else
                    trigger = stoploss;
            }
            else
            {
                x = (trigger - price) * quantity;

                if (stoploss > trigger)
                    trigger = stoploss;
                else
                    stoploss = trigger;
            }


            decimal y = co_lower * price * quantity;

            margin = x > y ? x : y;
            margin = margin + (margin * 0.2M);

            return margin;
        }
    }
}
