using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeGenie
{
    public class BusinessLogic
    {
        /**
         * 1. Connect Button
         *      1. Get the Login URL from Kite.
         *      2. Get the API Key and API Secret
         *      3. Use the browser tool to load the url in it.
         *      4. Login to Kite, it will return the url with the request token
         *      5. Show the connection state in the status bar or on top right corner.
         * 2. Subscribe Button 
         *      1. Subscribe for Nifty 50, Junior Nifty, Nifty Midcap and Bank Nifty
         *      2. Just show the ticker values on the top. No need to store it in DB.
         * 3. Get Instrument List
         *      1. Have a table to store the NSE Instruments
         *      2. if the instrument exists, just update the info.
         *      3. Have couple of more columns like, Last Updated Date, OHLC Last Retrived Date, IsActive, Index
         * 4. Get OHLC History Button
         *      1. Use the existing table ElderTickerMinIndicator to looad the OHLC data
         *      2. Use the NSE Instrument List to pull the data and from when the data is required.
         *      3. Run this process on a separate thread on request.
         *      4. Calculate the indicators as well and load the table.
         *      5. Have a button to start and stop the process.
         * 5. Show OHLC data in grid
         *      1. Have filters and grid to fetch the data that is required.
         *      2. Show the data in the grid that is filtered from the query.
         *      3. Decide based on the Imulse Indicator.
         * 6. Morning Break out Strategy Automation
         *      1. Pick the Nifty 50 instruments from the Intrument Table.
         *      2. Get the first 15 min OHLC data from Kite Bulk OHLC on 9:31 am and load it in a table
         *      3. Measure the first candle height for all the scripts. Formula = (High - Low) / Open
         *      4. Pick the intrument with candle heights of size 0.5% to 1.5% and place those in the subscription list.
         *      5. Subscribe those instruments
         *      6. Call Async method to update the tick to the table.
         *      7. Call Async method to check whether the tick is greater than the High or lesser that the low.
         *      8. Calculate 0.01% either way and place an BO order with Target as 0.04% and StopLoss as first 15min candles other end.
         *      9. Trailing SL to the Min Rs.1 and max can be 0.01% of the stock.
         *      10. Load the order to a table with the Order Id.
         *      11. For Backtesting - Check if the Target is achieved by polling LTP
         * 
         */

        /* TTD
         * 1. Implement Kite History 
         * 2. History toggle
         * 3. Separate page for Morning Breakout
         *      a. Having different grids denoting 
         * 4. Morning brakout should contain filters 
         *      a. Index filter 
         *          i. Below -0.3 - downward, Above 0.3 Upward, In between - flat 
         *          ii. Based on the Index direction pick the stocks in that directions
         *      b. Previous day Impulse filter
         *          i. 15min should be green
         *          ii. 30min should be green
         *          iii. 1hr should be either blue or green
         *      c. Gap open filter @ 09:30
         *          i. LONG - Previous Day High and Current Day First Candle Low
         *          ii. SHORT - Previous Day Low and Current Day First Candle High
         *      d. Open High and Open Low @ 09:30
         *          i. When Open is High = Go Short
         *          ii. When Open is Low = Go Long
         * 5. Downward logic
         * 6. Invoke thread @ 9:30 to run OHLC
         * **/

        /* Rules
         * 1. Candle size should not be more than 1%
         * 2. Ignore the stocks that are less than Rs.100 and greater than Rs.3000
         * 3. Dont enter after 10:30 AM
         * 4. Apply trailing SL principle
         * **/

        /* Issues
         * 1. Stop Lost Hit Time is marked as current time - 
         * **/

        // if placeOrders is true

        //https://github.com/rainmattertech/javakiteconnect/blob/master/sample/src/Examples.java#L248

        //1. Get Initial Capital from config
        //2. Filter the stocks with LTP from 300 to 3000
        //3. Have counters for orders placed, orders executed and orders completed
        //4. Get the trades count every 5 mins period to check the the number of trades completed 
        //5. Before placing an order check for the margin available. - Margin is not consistent. Check with local application counters
        //6. Get the margin required for the order based on the config values
        //7. If the margin available is more that that of the margin required. Place the order and get the margin and update DB.
        //8. Record the time when the last order is places using a static varaible. 
        //9. if the diff bet the previous order and the new order is less than a sec, Place the order only after one sec of the previous order by sleeping for a sec
        //10. Once the order is placed load the orders table

        //1. Show the positions, trades, PL on requests

        //1. Initial capital - get from config
        //2. Total Orders - get from config
        //3. Margin for a single order is InitialCapital/TotalOders
        //4. Total Stock Amount = Margin for a single order * 20 times
        //5. Quantity = Total Stock Amount / Stock LTP

        //1. Static variable Total Margin
        //2. Static variable Available Margin
        //3. Static variable Used Margin
        //4. Place the order only when the available margin is greater or equal to the required margin for a single order

        //Get Order thread - Run this thread every 5 seconds to refresh the orders and calculate the margin available
        //1. Columns to use in Order table Order Id and Parent Order Id
        //2. Base Orders = Pick the orders with ParentOrderId is NULL for the current day
        //3. Pick the Base orders that are pending to be executed - Calculate the available margin / utilized margin based on that.
        //4. If the Base order are executed, check the status of the child orders 
        //5. If the child orders are in pending status - Calculate the available margin / utilized margin based on that.
        //6. If the both ParentOrder and ChildOrder are completed status, Release the margin. Calculate the available margin / utilized margin.


        /* * Sample Kite Outputs *
         * 
            //OHLC

            {
              "NSE:INFY": {
                "InstrumentToken": 408065,
                "LastPrice": 958.8,
                "Open": 979.4,
                "Close": 976.1,
                "High": 983.2,
                "Low": 957.05
              },
              "NSE:ASHOKLEY": {
                "InstrumentToken": 54273,
                "LastPrice": 120.05,
                "Open": 119.2,
                "Close": 117.85,
                "High": 123.2,
                "Low": 119.2
              }
            }
  
            //Ticker - OnTick  
  
            {
              "Mode": "ltp",
              "InstrumentToken": 256265,
              "Tradable": false,
              "LastPrice": 10121.8,
              "LastQuantity": 0,
              "AveragePrice": 0,
              "Volume": 0,
              "BuyQuantity": 0,
              "SellQuantity": 0,
              "Open": 0,
              "High": 0,
              "Low": 0,
              "Close": 0,
              "Change": 0,
              "Bids": null,
              "Offers": null
            }

            //LTP
            {
              "NSE:INFY": {
                "InstrumentToken": 408065,
                "LastPrice": 958.8
              },
              "NSE:ASHOKLEY": {
                "InstrumentToken": 54273,
                "LastPrice": 120.05
              }
            }

            //GetQuote

            {
              "Volume": 0,
              "LastQuantity": 10,
              "LastTime": null,
              "Change": 0,
              "OpenInterest": 0,
              "SellQuantity": 0,
              "ChangePercent": 0,
              "LastPrice": 910.1,
              "BuyQuantity": 0,
              "Open": 924.45,
              "Close": 910.1,
              "High": 931.25,
              "Low": 907.7,
              "Bids": [
                {
                  "Quantity": 0,
                  "Price": -0.01,
                  "Orders": 0
                },
                {
                  "Quantity": 0,
                  "Price": -0.01,
                  "Orders": 0
                },
                {
                  "Quantity": 0,
                  "Price": -0.01,
                  "Orders": 0
                },
                {
                  "Quantity": 0,
                  "Price": -0.01,
                  "Orders": 0
                },
                {
                  "Quantity": 0,
                  "Price": -0.01,
                  "Orders": 0
                }
              ],
              "Offers": [
                {
                  "Quantity": 0,
                  "Price": -0.01,
                  "Orders": 0
                },
                {
                  "Quantity": 0,
                  "Price": -0.01,
                  "Orders": 0
                },
                {
                  "Quantity": 0,
                  "Price": -0.01,
                  "Orders": 0
                },
                {
                  "Quantity": 0,
                  "Price": -0.01,
                  "Orders": 0
                },
                {
                  "Quantity": 0,
                  "Price": -0.01,
                  "Orders": 0
                }
              ]
            }

            // History OHLC
            {
              "TimeStamp": "2017-12-01T09:15:00+0530",
              "Open": 119.5,
              "High": 120.45,
              "Low": 119.5,
              "Close": 120.4,
              "Volume": 675937
            }

            //GetORders
            {
              "AveragePrice": 0,
              "CancelledQuantity": 0,
              "DisclosedQuantity": 0,
              "Exchange": "NSE",
              "ExchangeOrderId": null,
              "ExchangeTimestamp": null,
              "FilledQuantity": 0,
              "InstrumentToken": 738561,
              "MarketProtection": 0,
              "OrderId": "171202000004087",
              "OrderTimestamp": "2017-12-02 11:59:42",
              "OrderType": "LIMIT",
              "ParentOrderId": null,
              "PendingQuantity": 1,
              "PlacedBy": "ZP5612",
              "Price": 910.1,
              "Product": "BO",
              "Quantity": 1,
              "Status": "PUT ORDER REQ RECEIVED",
              "StatusMessage": null,
              "Tag": null,
              "Tradingsymbol": "RELIANCE",
              "TrasactionType": "BUY",
              "TriggerPrice": 0,
              "Validity": "DAY",
              "Variety": "bo"
            }

            {
              "AveragePrice": 0,
              "CancelledQuantity": 0,
              "DisclosedQuantity": 0,
              "Exchange": "NSE",
              "ExchangeOrderId": null,
              "ExchangeTimestamp": null,
              "FilledQuantity": 0,
              "InstrumentToken": 424961,
              "MarketProtection": 0,
              "OrderId": "171202000004103",
              "OrderTimestamp": "2017-12-02 12:00:30",
              "OrderType": "LIMIT",
              "ParentOrderId": null,
              "PendingQuantity": 1,
              "PlacedBy": "ZP5612",
              "Price": 255.2,
              "Product": "BO",
              "Quantity": 1,
              "Status": "PUT ORDER REQ RECEIVED",
              "StatusMessage": null,
              "Tag": null,
              "Tradingsymbol": "ITC",
              "TransactionType": "SELL",
              "TriggerPrice": 0,
              "Validity": "DAY",
              "Variety": "bo"
            }

            {
              "AveragePrice": 0,
              "CancelledQuantity": 0,
              "DisclosedQuantity": 0,
              "Exchange": "NSE",
              "ExchangeOrderId": null,
              "ExchangeTimestamp": null,
              "FilledQuantity": 0,
              "InstrumentToken": 738561,
              "MarketProtection": 0,
              "OrderId": "171202000004208",
              "OrderTimestamp": "2017-12-02 12:07:09",
              "OrderType": "MARKET",
              "ParentOrderId": null,
              "PendingQuantity": 0,
              "PlacedBy": "ZP5612",
              "Price": 0,
              "Product": "MIS",
              "Quantity": 1,
              "Status": "REJECTED",
              "StatusMessage": "ADAPTER is down",
              "Tag": null,
              "Tradingsymbol": "RELIANCE",
              "TransactionType": "BUY",
              "TriggerPrice": 0,
              "Validity": "DAY",
              "Variety": "regular"
            }
         * 
         * 
         * 
         * */
    }
}

