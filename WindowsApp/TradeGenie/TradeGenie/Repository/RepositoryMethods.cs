using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KiteConnect;
using TradeGenie;
using System.Data.SqlClient;
using System.Data;

namespace TradeGenie.Repository
{
    public class RepositoryMethods
    {
        TradeGenieEntities tradeGenie = new TradeGenieEntities();
        TickerEntities tickerDB = new TickerEntities();

        public int LoadInstruments(List<KiteConnect.Instrument> instruments)
        {
            bool recExists = false;
            int retVal = 0;

            try
            {

                foreach (KiteConnect.Instrument instrument in instruments)
                {
                    try
                    {

                        recExists = tradeGenie.Instruments.Any(trade => trade.InstrumentToken == instrument.InstrumentToken);

                        if (!recExists)
                        {
                            Instrument newInstrument = new Instrument();

                            newInstrument.Exchange = instrument.Exchange;
                            newInstrument.ExchangeToken = (int?)instrument.ExchangeToken;
                            newInstrument.Expiry = instrument.Expiry.ToString();
                            newInstrument.InstrumentToken = (int)instrument.InstrumentToken;
                            newInstrument.InstrumentType = instrument.InstrumentType;
                            newInstrument.LastPrice = instrument.LastPrice;
                            newInstrument.LotSize = (int?)instrument.LotSize;
                            newInstrument.Name = instrument.Name;
                            newInstrument.Segment = instrument.Segment;
                            newInstrument.Strike = instrument.Strike;
                            newInstrument.TickSize = instrument.TickSize;
                            newInstrument.TradingSymbol = instrument.TradingSymbol;

                            tradeGenie.Instruments.Add(newInstrument);
                        }
                        else
                        {
                            var selInstrument = tradeGenie.Instruments.Find(instrument.InstrumentToken);

                            selInstrument.Expiry = instrument.Expiry.ToString();
                            selInstrument.LastPrice = instrument.LastPrice;

                        }
                    }
                    catch { }
                }

                tradeGenie.SaveChanges();
            }
            catch (Exception ex)
            {
                //Log the exception to DB

                retVal = 1;
                Logger.ErrorLogToFile(ex);
            }

            return retVal;
        }

        /// <summary>
        /// stockGroup is nothing but the collection in MasterStockList
        /// </summary>
        /// <param name="stockGroup"></param>
        /// <returns></returns>
        public List<InstrumentVM> GetStockList(string stockGroup)
        {
            List<InstrumentVM> masterInstruments = new List<InstrumentVM>();

            var instruments = from ins in tradeGenie.Instruments
                              join msl in tradeGenie.MasterStockLists on ins.TradingSymbol equals msl.TradingSymbol
                              where msl.Collection == stockGroup
                              select new InstrumentVM
                              {
                                  Exchange = ins.Exchange,
                                  ExchangeToken = (int)ins.ExchangeToken,
                                  Expiry = ins.Expiry,
                                  InstrumentToken = ins.InstrumentToken,
                                  InstrumentType = ins.InstrumentType,
                                  LastPrice = (decimal)ins.LastPrice,
                                  LotSize = (int)ins.LotSize,
                                  Name = ins.Name,
                                  Segment = ins.Segment,
                                  Strike = (decimal)ins.Strike,
                                  TickSize = (decimal)ins.TickSize,
                                  TradingSymbol = ins.TradingSymbol
                              };

            masterInstruments = instruments.ToList();

            return masterInstruments;
        }

        public int LoadOHLCData(List<InstrumentOHLCVM> ohlcData)
        {
            int retVal = 0;

            try
            {

                foreach (InstrumentOHLCVM ohlcItem in ohlcData)
                {
                    OHLCData ohlc = new OHLCData();

                    ohlc.InstrumentToken = ohlcItem.InstrumentToken;
                    ohlc.TradingSymbol = ohlcItem.TradingSymbol;
                    ohlc.LastUpdatedTime = ohlcItem.OHLCDateTime;
                    ohlc.Open = ohlcItem.Open;
                    ohlc.PreviousClose = ohlcItem.PreviousClose;
                    ohlc.Low = ohlcItem.Low;
                    ohlc.High = ohlcItem.High;
                    ohlc.LastPrice = ohlcItem.LastPrice;

                    tradeGenie.OHLCDatas.Add(ohlc);

                }

                tradeGenie.SaveChanges();
            }
            catch (Exception ex)
            {
                retVal = 1;
                Logger.ErrorLogToFile(ex);
            }

            return retVal;
        }

        public void LoadOrders(List<KiteConnect.Order> orders)
        {
            bool recExists = false;

            try
            {
                foreach (KiteConnect.Order order in orders)
                {

                    recExists = tradeGenie.Orders.Any(ord => ord.OrderId == order.OrderId && ord.ExchangeOrderId == order.ExchangeOrderId);

                    if (!recExists)
                    {
                        Order newOrder = new Order();

                        newOrder.OrderId = order.OrderId;
                        newOrder.InstrumentToken = Convert.ToInt32(order.InstrumentToken);
                        newOrder.ExchangeOrderId = order.ExchangeOrderId;

                        newOrder.AveragePrice = order.AveragePrice;
                        newOrder.CancelledQuantity = order.CancelledQuantity;
                        newOrder.DisclosedQuantity = order.DisclosedQuantity;
                        newOrder.Exchange = order.Exchange;
                        newOrder.FilledQuantity = order.FilledQuantity;
                        //newOrder.MarketProtection = order.ma;
                        newOrder.OrderType = order.OrderType;
                        newOrder.ParentOrderId = order.ParentOrderId;
                        newOrder.PendingQuantity = order.PendingQuantity;
                        newOrder.PlacedBy = order.PlacedBy;
                        newOrder.Price = order.Price;
                        newOrder.Product = order.Product;
                        newOrder.Quantity = order.Quantity;
                        newOrder.Status = order.Status;
                        newOrder.StatusMessage = order.StatusMessage;
                        newOrder.Tag = order.Tag;
                        newOrder.Tradingsymbol = order.Tradingsymbol;
                        newOrder.TransactionType = order.TransactionType;
                        newOrder.TriggerPrice = order.TriggerPrice;
                        newOrder.Validity = order.Validity;
                        newOrder.Variety = order.Variety;

                        newOrder.ExchangeTimestamp = order.ExchangeTimestamp;
                        newOrder.OrderTimestamp = order.OrderTimestamp;

                        tradeGenie.Orders.Add(newOrder);
                    }
                    else
                    {
                        Order existingOrder = new Order();

                        existingOrder = tradeGenie.Orders.Single(ord => ord.OrderId == order.OrderId && ord.ExchangeOrderId == order.ExchangeOrderId);

                        existingOrder.AveragePrice = order.AveragePrice;
                        existingOrder.CancelledQuantity = order.CancelledQuantity;
                        existingOrder.DisclosedQuantity = order.DisclosedQuantity;
                        existingOrder.Exchange = order.Exchange;
                        existingOrder.FilledQuantity = order.FilledQuantity;
                        //existingOrder.MarketProtection = order.MarketProtection;
                        existingOrder.OrderType = order.OrderType;
                        existingOrder.ParentOrderId = order.ParentOrderId;
                        existingOrder.PendingQuantity = order.PendingQuantity;
                        existingOrder.PlacedBy = order.PlacedBy;
                        existingOrder.Price = order.Price;
                        existingOrder.Product = order.Product;
                        existingOrder.Quantity = order.Quantity;
                        existingOrder.Status = order.Status;
                        existingOrder.StatusMessage = order.StatusMessage;
                        existingOrder.Tag = order.Tag;
                        existingOrder.TransactionType = order.TransactionType;
                        existingOrder.TriggerPrice = order.TriggerPrice;
                        existingOrder.Validity = order.Validity;
                        existingOrder.Variety = order.Variety;

                        existingOrder.ExchangeTimestamp = order.ExchangeTimestamp;
                        existingOrder.OrderTimestamp = order.OrderTimestamp;
                    }
                }

                tradeGenie.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.ErrorLogToFile(ex);
            }
        }

        public List<OHLCData> GetOHLCData()
        {
            List<OHLCData> ohlcForInstruments = new List<OHLCData>();

            var ohlcs = from ohlc in tradeGenie.OHLCDatas
                        where ohlc.LastUpdatedTime > DateTime.Today
                        select ohlc;

            ohlcForInstruments = ohlcs.ToList();

            return ohlcForInstruments;
        }

        public int LoadMorningBreakoutData(List<MorningBreakOutStrategyDM> mbkData)
        {
            int retVal = 0;
            bool isSomethingToAdd = false;

            try
            {

                foreach (MorningBreakOutStrategyDM mbkItem in mbkData)
                {
                    if (!tradeGenie.MorningBreakouts.Any(tmp => tmp.InstrumentToken == mbkItem.InstrumentToken && tmp.DateTimePeriod >= DateTime.Today))
                    {
                        MorningBreakout mbk = new MorningBreakout();

                        mbk.InstrumentToken = mbkItem.InstrumentToken;
                        mbk.InstrumentToken = mbkItem.InstrumentToken;
                        mbk.TradingSymbol = mbkItem.TradingSymbol;
                        mbk.DateTimePeriod = mbkItem.DateTimePeriod;
                        mbk.Open = mbkItem.Open;
                        mbk.Close = mbkItem.Close;
                        mbk.Low = mbkItem.Low;
                        mbk.High = mbkItem.High;
                        mbk.CandleSize = mbkItem.CandleSize;

                        tradeGenie.MorningBreakouts.Add(mbk);

                        isSomethingToAdd = true;
                    }
                }

                if (isSomethingToAdd)
                    tradeGenie.SaveChanges();
            }
            catch (Exception ex)
            {
                retVal = 1;
                Logger.ErrorLogToFile(ex);
            }

            return retVal;
        }

        public List<MorningBreakOutStrategyDM> GetAllMorningBreakouts()
        {
            List<MorningBreakOutStrategyDM> morningBreakouts = new List<MorningBreakOutStrategyDM>();

            var morningBKs = from mbk in tradeGenie.MorningBreakouts
                             where mbk.DateTimePeriod >= DateTime.Today
                             select new MorningBreakOutStrategyDM
                             {
                                 CandleSize = mbk.CandleSize,
                                 Close = mbk.Close,
                                 DateTimePeriod = mbk.DateTimePeriod,
                                 DayHigh = mbk.DayHigh,
                                 DayLow = mbk.DayLow,
                                 Entry = mbk.Entry,
                                 EntryTime = mbk.EntryTime,
                                 Exit = mbk.Exit,
                                 ExitTime = mbk.ExitTime,
                                 High = mbk.High,
                                 InstrumentToken = mbk.InstrumentToken,
                                 Low = mbk.Low,
                                 LTP = mbk.LTP,
                                 Movement = mbk.Movement,
                                 Open = mbk.Open,
                                 ProfitLoss = mbk.ProfitLoss,
                                 Quantity = mbk.Quantity,
                                 RAGStatus = mbk.RAGStatus,
                                 StopLoss = mbk.StopLoss,
                                 StopLossHitTime = mbk.StopLossHitTime,
                                 TradingSymbol = mbk.TradingSymbol,
                                 isRealOrderPlaced = mbk.isRealOrderPlaced
                             };

            morningBreakouts = morningBKs.ToList();

            return morningBreakouts;
        }

        public int SetLTPHighLow(Tick ticker)
        {
            int retVal = 0;

            MorningBreakout mbkInstrument = new MorningBreakout();

            try
            {
                bool isPresent = tradeGenie.MorningBreakouts.Any(wh => wh.DateTimePeriod >= DateTime.Today && wh.InstrumentToken == ticker.InstrumentToken);

                if (isPresent)
                {
                    mbkInstrument = (from mbk in tradeGenie.MorningBreakouts
                                     where mbk.DateTimePeriod >= DateTime.Today && mbk.InstrumentToken == ticker.InstrumentToken
                                     select mbk).Single<MorningBreakout>();

                    mbkInstrument.LTP = ticker.LastPrice;
                    //mbkInstrument.DayHigh = ticker.High;
                    //mbkInstrument.DayLow = ticker.Low;

                    tradeGenie.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                retVal = 1;
                Logger.ErrorLogToFile(ex);
            }

            return retVal;
        }

        public int UpdateMorningBreakoutData(List<MorningBreakOutStrategyDM> mbklist)
        {
            int retVal = 0;

            try
            {

                List<MorningBreakout> mbkInstruments = new List<MorningBreakout>();

                mbkInstruments = (from mbk in tradeGenie.MorningBreakouts
                                  where mbk.DateTimePeriod >= DateTime.Today
                                  select mbk).ToList();


                foreach (MorningBreakOutStrategyDM mbkItem in mbklist)
                {
                    if (mbkItem.Entry != null)
                    {
                        var mbkInstrument = mbkInstruments.Single<MorningBreakout>(a => a.InstrumentToken == mbkItem.InstrumentToken);

                        mbkInstrument.Entry = mbkItem.Entry;
                        mbkInstrument.EntryTime = mbkItem.EntryTime;
                        mbkInstrument.Exit = mbkItem.Exit;
                        mbkInstrument.ExitTime = mbkItem.ExitTime;
                        mbkInstrument.StopLoss = mbkItem.StopLoss;
                        mbkInstrument.StopLossHitTime = mbkItem.StopLossHitTime;
                        mbkInstrument.Movement = mbkItem.Movement;
                        mbkInstrument.Quantity = mbkItem.Quantity;
                        mbkInstrument.ProfitLoss = mbkItem.ProfitLoss;
                        mbkInstrument.isRealOrderPlaced = mbkItem.isRealOrderPlaced;
                    }

                }

                tradeGenie.SaveChanges();
            }
            catch (Exception ex)
            {
                retVal = 1;
                Logger.ErrorLogToFile(ex);
            }

            return retVal;
        }

        public List<MasterStockList> GetMasterStockList()
        {
            var masterStockList = from msl in tradeGenie.MasterStockLists
                                  where msl.IsIncluded == true
                                  select msl;

            List<MasterStockList> mslist = masterStockList.ToList();

            return mslist;
        }

        public List<TickerElderIndicatorsVM> GetTickerDataForElder(MasterStockList stock, int timePeriod)
        {
            List<TickerElderIndicatorsVM> tickerDataList = new List<TickerElderIndicatorsVM>();

            int recCount = (from td in tradeGenie.TickerElderIndicator
                            where td.TradingSymbol == stock.TradingSymbol && td.TimePeriod == timePeriod
                            select td.TradingSymbol).Count();

            if (recCount > 0)
            {

                var tickerData = from td in tradeGenie.TickerElderIndicator
                                 where td.TradingSymbol == stock.TradingSymbol && td.TimePeriod == timePeriod
                                 orderby td.TickerDateTime ascending
                                 select new TickerElderIndicatorsVM
                                 {
                                     TradingSymbol = td.TradingSymbol.Trim(),
                                     TickerDateTime = td.TickerDateTime,
                                     TimePeriod = td.TimePeriod,
                                     PriceOpen = td.PriceOpen,
                                     PriceHigh = td.PriceHigh,
                                     PriceLow = td.PriceLow,
                                     PriceClose = td.PriceClose,
                                     Volume = td.Volume,

                                     EMA1 = td.EMA1,
                                     EMA2 = td.EMA2,
                                     EMA3 = td.EMA3,
                                     EMA4 = td.EMA4,

                                     RSI1 = td.RSI1,
                                     RSI2 = td.RSI2,

                                     AG1 = td.AG1,
                                     AL1 = td.AL1,
                                     AG2 = td.AG2,
                                     AL2 = td.AL2,

                                     MACD = td.MACD,
                                     Signal = td.Signal,
                                     Histogram = td.Histogram,

                                     ForceIndex1 = td.ForceIndex,
                                     Impulse = td.Impulse

                                 };

                tickerDataList = tickerData.ToList<TickerElderIndicatorsVM>();
            }

            return tickerDataList;
        }

        public void InsertTickerElderIndicators(List<TickerElderIndicatorsVM> tickerList, int timePeriod)
        {
            //bool recExists = false;
            try
            {
                foreach (TickerElderIndicatorsVM ticker in tickerList)
                {
                    var tickSelItem = tradeGenie.TickerElderIndicator.FirstOrDefault(tick => tick.TradingSymbol.Equals(ticker.TradingSymbol)
                                        && tick.TickerDateTime.Equals(ticker.TickerDateTime) && (tick.TimePeriod == timePeriod));

                    if (tickSelItem != null)
                    {

                        tickSelItem.EMA1 = ticker.EMA1;
                        tickSelItem.EMA2 = ticker.EMA2;
                        tickSelItem.EMA3 = ticker.EMA3;
                        tickSelItem.EMA4 = ticker.EMA4;

                        tickSelItem.AG1 = ticker.AG1;
                        tickSelItem.AL1 = ticker.AL1;
                        tickSelItem.RSI1 = ticker.RSI1;

                        tickSelItem.AG2 = ticker.AG2;
                        tickSelItem.AL2 = ticker.AL2;
                        tickSelItem.RSI2 = ticker.RSI2;

                        tickSelItem.MACD = ticker.MACD;
                        tickSelItem.Signal = (double)ticker.Signal;
                        tickSelItem.Histogram = ticker.Histogram;

                        tickSelItem.ForceIndex = ticker.ForceIndex1;

                        tickSelItem.Impulse = ticker.Impulse;
                    }
                }

                tradeGenie.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.ErrorLogToFile(ex);
            }
        }

        public int InsertInitialElderData(List<TickerElderIndicatorsVM> tickerList)
        {
            bool recExists = false;
            int retVal = 0;

            try
            {
                foreach (TickerElderIndicatorsVM ticker in tickerList)
                {
                    recExists = tradeGenie.TickerElderIndicator.Any(tick => tick.TradingSymbol.Equals(ticker.TradingSymbol)
                        && tick.TickerDateTime.Equals(ticker.TickerDateTime) && (tick.TimePeriod == ticker.TimePeriod));

                    if (!recExists)
                        tradeGenie.TickerElderIndicator.Add(new TickerElderIndicator
                        {
                            InstrumentToken = ticker.InstrumentToken,
                            TradingSymbol = ticker.TradingSymbol,
                            TimePeriod = ticker.TimePeriod,
                            TickerDateTime = ticker.TickerDateTime,
                            PriceOpen = ticker.PriceOpen,
                            PriceHigh = ticker.PriceHigh,
                            PriceLow = ticker.PriceLow,
                            PriceClose = ticker.PriceClose,
                            Volume = ticker.Volume
                        });
                }

                if (!recExists)
                    tradeGenie.SaveChanges();
            }
            catch (Exception ex)
            {
                retVal = 1;
                Logger.ErrorLogToFile(ex);
            }

            return retVal;
        }

        public int GetInstrumentToken(string tradingSymbol)
        {
            int instrumentToken = tradeGenie.Instruments.Single(sel => sel.TradingSymbol == tradingSymbol).InstrumentToken;

            return instrumentToken;
        }

        public void LoadUserLoginInformation(UserLoginVM userLogin)
        {
            try
            {
                tradeGenie.UserLogins.Add(new UserLogin
                {
                    LoginDateTime = userLogin.LoginDate,
                    LogoutDateTime = DateTime.Now,
                    AccessToken = userLogin.AccessToken,
                    PublicToken = userLogin.PublicToken,
                    RequestToken = userLogin.RequestToken,
                    ClientId = userLogin.ClientId,
                    Broker = "Kite",
                    Status = "IN"
                });

                tradeGenie.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.ErrorLogToFile(ex);
            }
        }

        public UserLoginVM GetUserLoginInformation()
        {
            UserLoginVM userLogin = new UserLoginVM();

            try
            {

                var loginInfo = tradeGenie.UserLogins.Where(sel => sel.ClientId == UserConfiguration.UserId
                                                    && sel.LoginDateTime == DateTime.Today).OrderByDescending(or => or.ID).First();

                if (loginInfo != null)
                {
                    userLogin.ID = loginInfo.ID;
                    userLogin.LoginDate = loginInfo.LoginDateTime;
                    userLogin.ClientId = loginInfo.ClientId;
                    userLogin.AccessToken = loginInfo.AccessToken;
                    userLogin.RequestToken = loginInfo.RequestToken;
                    userLogin.PublicToken = loginInfo.PublicToken;

                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLogToFile(ex);
            }

            return userLogin;
        }

        public void InvalidateLoginInformation()
        {
            tradeGenie.UserLogins.RemoveRange(tradeGenie.UserLogins.Where(sel => sel.LoginDateTime == DateTime.Today).ToList());

            tradeGenie.SaveChanges();
        }

        public void LoadPositions(List<Position> positions)
        {
            bool recExists = false;

            try
            {
                foreach (Position position in positions)
                {
                    recExists = tradeGenie.NetPositions.Any(pstn => pstn.InstrumentToken == position.InstrumentToken &&
                                    pstn.PositionDate == DateTime.Today && pstn.TradingSymbol == position.TradingSymbol);

                    if (!recExists)
                    {
                        NetPosition newPosition = new NetPosition();

                        newPosition.PositionDate = DateTime.Today;
                        newPosition.InstrumentToken = (int)position.InstrumentToken;
                        newPosition.TradingSymbol = position.TradingSymbol;

                        newPosition.AveragePrice = position.AveragePrice;
                        newPosition.BuyM2M = position.BuyM2M;
                        newPosition.BuyPrice = position.BuyPrice;
                        newPosition.BuyQuantity = position.BuyQuantity;
                        newPosition.BuyValue = position.BuyValue;
                        newPosition.ClosePrice = position.ClosePrice;
                        newPosition.Exchange = position.Exchange;
                        newPosition.LastPrice = position.LastPrice;
                        newPosition.M2M = position.M2M;
                        newPosition.Multiplier = position.Multiplier;
                        //newPosition.NetBuyAmountM2M = position.NetBuyAmountM2M;
                        //newPosition.NetSellAmountM2M = position.NetSellAmountM2M;
                        newPosition.OvernightQuantity = position.OvernightQuantity;
                        newPosition.PNL = position.PNL;
                        newPosition.Product = position.Product;
                        newPosition.Quantity = position.Quantity;
                        newPosition.Realised = position.Realised;
                        newPosition.SellM2M = position.SellM2M;
                        newPosition.SellPrice = position.SellPrice;
                        newPosition.SellQuantity = position.SellQuantity;
                        newPosition.SellValue = position.SellValue;
                        newPosition.Unrealised = position.Unrealised;
                        newPosition.Value = position.Value;

                        tradeGenie.NetPositions.Add(newPosition);
                    }
                    else
                    {
                        NetPosition existingPosition = tradeGenie.NetPositions.Single(pstn => pstn.InstrumentToken == position.InstrumentToken &&
                                        pstn.PositionDate == DateTime.Today && pstn.TradingSymbol == position.TradingSymbol);

                        existingPosition.AveragePrice = position.AveragePrice;
                        existingPosition.BuyM2M = position.BuyM2M;
                        existingPosition.BuyPrice = position.BuyPrice;
                        existingPosition.BuyQuantity = position.BuyQuantity;
                        existingPosition.BuyValue = position.BuyValue;
                        existingPosition.ClosePrice = position.ClosePrice;
                        existingPosition.Exchange = position.Exchange;
                        existingPosition.LastPrice = position.LastPrice;
                        existingPosition.M2M = position.M2M;
                        existingPosition.Multiplier = position.Multiplier;
                        //existingPosition.NetBuyAmountM2M = position.NetBuyAmountM2M;
                        //existingPosition.NetSellAmountM2M = position.NetSellAmountM2M;
                        existingPosition.OvernightQuantity = position.OvernightQuantity;
                        existingPosition.PNL = position.PNL;
                        existingPosition.Product = position.Product;
                        existingPosition.Quantity = position.Quantity;
                        existingPosition.Realised = position.Realised;
                        existingPosition.SellM2M = position.SellM2M;
                        existingPosition.SellPrice = position.SellPrice;
                        existingPosition.SellQuantity = position.SellQuantity;
                        existingPosition.SellValue = position.SellValue;
                        existingPosition.Unrealised = position.Unrealised;
                        existingPosition.Value = position.Value;
                    }
                }

                tradeGenie.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.ErrorLogToFile(ex);
            }
        }

        public void LoadTrades(List<KiteConnect.Trade> trades)
        {
            bool recExists = false;

            try
            {
                foreach (KiteConnect.Trade trade in trades)
                {
                    recExists = tradeGenie.Trades.Any(tr => tr.TradeId == trade.TradeId
                        && tr.OrderId == trade.OrderId && tr.ExchangeOrderId == trade.ExchangeOrderId);

                    if (!recExists)
                    {
                        Trade tradeRep = new Trade();
                        tradeRep.AveragePrice = trade.AveragePrice;
                        tradeRep.Exchange = trade.Exchange;
                        tradeRep.ExchangeOrderId = trade.ExchangeOrderId;
                        tradeRep.ExchangeTimestamp = trade.ExchangeTimestamp;
                        tradeRep.InstrumentToken = Convert.ToInt32(trade.InstrumentToken);
                        tradeRep.OrderId = trade.OrderId;
                        tradeRep.OrderTimestamp = trade.FillTimestamp;
                        tradeRep.Product = trade.Product;
                        tradeRep.Quantity = trade.Quantity;
                        tradeRep.TradeId = trade.TradeId;
                        tradeRep.TradingSymbol = trade.Tradingsymbol;
                        tradeRep.TransactionType = trade.TransactionType;

                        tradeGenie.Trades.Add(tradeRep);
                    }
                }

                tradeGenie.SaveChanges();
            }
            catch (Exception ex)
            {
                Logger.ErrorLogToFile(ex);
            }

        }

        public void LoadMargins(KiteConnect.UserMargin currentMargin)
        {
            bool recExists = false;

            recExists = tradeGenie.Margins.Any(mar => mar.MarginDate == DateTime.Today);

            if (!recExists)
            {
                Margin margin = new Margin();

                margin.MarginDate = DateTime.Today;
                margin.AdhocMargin = currentMargin.Available.AdHocMargin;
                margin.Cash = currentMargin.Available.Cash;
                margin.Collateral = currentMargin.Available.Collateral;
                margin.Debits = currentMargin.Utilised.Debits;
                margin.Enabled = currentMargin.Enabled;
                margin.Exposure = currentMargin.Utilised.Exposure;
                margin.HoldingSales = currentMargin.Utilised.HoldingSales;
                margin.IntradayPayin = currentMargin.Available.IntradayPayin;
                margin.M2MRealised = currentMargin.Utilised.M2MRealised;
                margin.M2MUnrealised = currentMargin.Utilised.M2MUnrealised;
                margin.Net = currentMargin.Net;
                margin.Payout = currentMargin.Utilised.Payout;
                margin.OptionPremium = currentMargin.Utilised.OptionPremium;
                margin.Span = currentMargin.Utilised.Span;
                margin.Turnover = currentMargin.Utilised.Turnover;

                tradeGenie.Margins.Add(margin);
            }
            else
            {
                Margin existingMargin = tradeGenie.Margins.Single(mar => mar.MarginDate == DateTime.Today);

                existingMargin.AdhocMargin = currentMargin.Available.AdHocMargin;
                existingMargin.Cash = currentMargin.Available.Cash;
                existingMargin.Collateral = currentMargin.Available.Collateral;
                existingMargin.Debits = currentMargin.Utilised.Debits;
                existingMargin.Enabled = currentMargin.Enabled;
                existingMargin.Exposure = currentMargin.Utilised.Exposure;
                existingMargin.HoldingSales = currentMargin.Utilised.HoldingSales;
                existingMargin.IntradayPayin = currentMargin.Available.IntradayPayin;
                existingMargin.M2MRealised = currentMargin.Utilised.M2MRealised;
                existingMargin.M2MUnrealised = currentMargin.Utilised.M2MUnrealised;
                existingMargin.Net = currentMargin.Net;
                existingMargin.Payout = currentMargin.Utilised.Payout;
                existingMargin.OptionPremium = currentMargin.Utilised.OptionPremium;
                existingMargin.Span = currentMargin.Utilised.Span;
                existingMargin.Turnover = currentMargin.Utilised.Turnover;

            }

            tradeGenie.SaveChanges();

        }

        public int GetOrderCount(string status, bool onlyParentOrders = false, bool onlyChildOrders = false)
        {
            int orderCount = 0;

            if (onlyParentOrders)
            {
                orderCount = tradeGenie.Orders.Count(cnt => cnt.OrderTimestamp >= DateTime.Today && cnt.Status == status && cnt.ParentOrderId == null);
            }
            else if (onlyChildOrders)
            {
                orderCount = tradeGenie.Orders.Count(cnt => cnt.OrderTimestamp >= DateTime.Today && cnt.Status == status && cnt.ParentOrderId != null);
            }
            else
            {
                orderCount = tradeGenie.Orders.Count(cnt => cnt.OrderTimestamp >= DateTime.Today && cnt.Status == status);
            }

            return orderCount;
        }

        public void GetProfitLossPositions(out decimal profit, out decimal loss, out decimal pl)
        {
            profit = 0.0M;
            loss = 0.0M;
            pl = 0.0M;

            profit = Convert.ToDecimal(tradeGenie.NetPositions.Where(con => con.PositionDate == DateTime.Today && con.PNL > 0).Sum(agg => agg.PNL));
            loss = Convert.ToDecimal(tradeGenie.NetPositions.Where(con => con.PositionDate == DateTime.Today && con.PNL < 0).Sum(agg => agg.PNL));
            pl = Convert.ToDecimal(tradeGenie.NetPositions.Where(con => con.PositionDate == DateTime.Today).Sum(agg => agg.PNL));
        }

        public void LoadQuote(Tick TickData)
        {
            tradeGenie.Quotes.Add(new Quote
            {
                BuyQuantity = (int)TickData.BuyQuantity,
                Change = TickData.Change,
                Close = TickData.Close,
                High = TickData.High,
                InstrumentToken = (int)TickData.InstrumentToken,
                LastPrice = TickData.LastPrice,
                LastQuantity = (int)TickData.LastQuantity,
                LastUpdatedTime = DateTime.Now,
                Low = TickData.Low,
                Open = TickData.Open,
                SellQuantity = (int)TickData.SellQuantity,
                Volume = (int)TickData.Volume
            });

            tradeGenie.SaveChanges();
        }

        public List<uint> GetAllInstrumentTokenForSubscription()
        {
            List<uint> subscriptionList = new List<uint>();

            var instrumentTokens = from masStock in tradeGenie.MasterStockLists
                                   join insTok in tradeGenie.Instruments on masStock.TradingSymbol equals insTok.TradingSymbol
                                   select Convert.ToUInt32(insTok.InstrumentToken);

            subscriptionList = instrumentTokens.ToList();

            return subscriptionList;
        }

        public List<TickerElderIndicatorsVM> GetTickerDataForIndicators(string instrumentList, string timePeriod, DateTime startTime, DateTime endTime)
        {
            List<spGetTickerElderForTimePeriod_Result> elderData = tickerDB.spGetTickerElderForTimePeriod(instrumentList, timePeriod, startTime, endTime).ToList();

            List<TickerElderIndicatorsVM> tickerElderList = new List<TickerElderIndicatorsVM>();

            foreach (spGetTickerElderForTimePeriod_Result item in elderData)
            {
                TickerElderIndicatorsVM ticker = new TickerElderIndicatorsVM();

                ticker.InstrumentToken = item.InstrumentToken;
                ticker.TradingSymbol = item.StockCode;
                ticker.TickerDateTime = item.TickerDateTime;
                ticker.TimePeriod = item.TimePeriod;

                ticker.PriceOpen = item.PriceOpen;
                ticker.PriceHigh = item.PriceHigh;
                ticker.PriceLow = item.PriceLow;
                ticker.PriceClose = item.PriceClose;

                ticker.EMA1 = item.EMA1;
                ticker.EMA2 = item.EMA2;
                ticker.EMA3 = item.EMA3;
                ticker.EMA4 = item.EMA4;

                ticker.EMA1Dev = item.EMA1Dev;
                ticker.EMA2Dev = item.EMA2Dev;

                ticker.ForceIndex1 = item.ForceIndex1;
                ticker.ForceIndex2 = item.ForceIndex2;

                ticker.MACD = item.MACD;
                ticker.Signal = item.Signal;
                ticker.Histogram = item.Histogram;
                ticker.HistMovement = item.HistIncDec;

                ticker.Impulse = item.Impulse;

                tickerElderList.Add(ticker);
            }

            return tickerElderList;
        }

        public bool IsPositionExists(string tradingSymbol, string positionType, DateTime tradeDate, string variety, string transactionType, bool isRealTime)
        {
            bool isPositionExists = true;

            //If variety is BO, OrderType is Limit and Status is OPEN and OrderType is SL-M and Status is TriggerPending, then position exists

            if (isRealTime)
            {
                if (variety == Constants.VARIETY_CO)
                {
                    isPositionExists = tradeGenie.Orders.Any(ord => ord.Tradingsymbol == tradingSymbol
                                                            && ord.OrderTimestamp > DateTime.Today
                                                            && ord.Variety == variety
                                                            && (ord.Status == Utilities.OS_Open || ord.Status == Utilities.OS_TriggerPending));
                }
                else if (variety == Constants.VARIETY_BO)
                {
                    string orderStatusTranCheck = string.Empty;

                    if (transactionType == Constants.TRANSACTION_TYPE_BUY)
                    {
                        orderStatusTranCheck = Constants.TRANSACTION_TYPE_SELL;
                    }
                    else if (transactionType == Constants.TRANSACTION_TYPE_SELL)
                    {
                        orderStatusTranCheck = Constants.TRANSACTION_TYPE_BUY;
                    }

                    bool limitOrderStatus = tradeGenie.Orders.Any(ord => ord.Tradingsymbol == tradingSymbol
                                                            && ord.OrderTimestamp > DateTime.Today
                                                            && ord.Variety == variety && ord.TransactionType == orderStatusTranCheck
                                                            && ord.OrderType == Constants.ORDER_TYPE_LIMIT && ord.Status == Utilities.OS_Open);

                    bool stopLossOrderStatus = tradeGenie.Orders.Any(ord => ord.Tradingsymbol == tradingSymbol
                                                            && ord.OrderTimestamp > DateTime.Today
                                                            && ord.Variety == variety && ord.TransactionType == orderStatusTranCheck
                                                            && ord.OrderType == Constants.ORDER_TYPE_SLM && ord.Status == Utilities.OS_TriggerPending);

                    if (limitOrderStatus && stopLossOrderStatus)
                    {
                        isPositionExists = true;
                    }
                    else
                    {
                        isPositionExists = false;
                    }

                }
            }
            else
            {
                isPositionExists = tradeGenie.ElderStrategyOrders.Any(ord => ord.TradingSymbol == tradingSymbol
                                                        && ord.TradeDate == tradeDate
                                                        && ord.PositionType == positionType
                                                        && ord.ExitTime == null);
            }

            Logger.LogToFile(string.Format("{0},{1},{2},{3}", 
                                    "IsPositionExists", 
                                    "isRealTime:" + isRealTime.ToString(), 
                                    tradingSymbol,
                                    "isPositionExists:" + isPositionExists.ToString()));

            return isPositionExists;
        }

        public void InsertElderStrategyOrder(ElderStrategyOrder elderOrder)
        {
            tradeGenie.ElderStrategyOrders.Add(elderOrder);

            tradeGenie.SaveChanges();
        }

        public void UpdateElderStrategyOrders(List<ElderStrategyOrder> elderOrders)
        {
            foreach (ElderStrategyOrder elderOrder in elderOrders)
            {
                ElderStrategyOrder dbItem = tradeGenie.ElderStrategyOrders.Where(x => x.OrderId == elderOrder.OrderId).FirstOrDefault<ElderStrategyOrder>();

                dbItem.ExitPrice = elderOrder.ExitPrice;
                dbItem.ExitTime = elderOrder.ExitTime;
                dbItem.StopLoss = elderOrder.StopLoss;
                dbItem.StopLossHitTime = elderOrder.StopLossHitTime;
                dbItem.ProfitLoss = elderOrder.ProfitLoss;
                //dbItem.OrderId = elderOrder.OrderId;
                dbItem.SLOrderId = elderOrder.SLOrderId;
                dbItem.PositionStatus = elderOrder.PositionStatus;
                dbItem.TargetOrderStatus = elderOrder.TargetOrderStatus;
                dbItem.SLOrderStatus = elderOrder.SLOrderStatus;
                dbItem.TargetOrderId = elderOrder.TargetOrderId;
            }

            tradeGenie.SaveChanges();
        }

        public List<ElderStrategyOrder> GetElderStrategyOrders(DateTime tradeDate)
        {
            List<ElderStrategyOrder> elderOrder = new List<ElderStrategyOrder>();

            elderOrder = tradeGenie.ElderStrategyOrders.Where(con => con.TradeDate == tradeDate && con.PositionStatus == Utilities.OS_TriggerPending).ToList();

            return elderOrder;
        }

        public List<ElderStrategyOrderVM> GetElderStrategyOrdersForView(DateTime tradeDate)
        {
            List<ElderStrategyOrderVM> elderOrder = new List<ElderStrategyOrderVM>();

            //elderOrder = tradeGenie.ElderStrategyOrders.Where(con => con.TradeDate == tradeDate).ToList();

            var res = from elder in tradeGenie.ElderStrategyOrders
                      join ohlc in tradeGenie.OHLCDatas on elder.InstrumentToken equals ohlc.InstrumentToken
                      where ohlc.OHLCDate == tradeDate && elder.TradeDate == tradeDate
                      select new ElderStrategyOrderVM
                      {
                          InstrumentToken = elder.InstrumentToken,
                          TradingSymbol = elder.TradingSymbol,
                          TradeDate = elder.TradeDate,
                          EntryPrice = elder.EntryPrice,
                          ExitPrice = elder.ExitPrice,
                          StopLoss = elder.StopLoss,
                          Ltp = ohlc.LastPrice,
                          Quantity = elder.Quantity,
                          ProfitLoss = elder.ProfitLoss,
                          EntryTime = elder.EntryTime,
                          ExitTime = elder.ExitTime,
                          StopLossHitTime = elder.StopLossHitTime,
                          StockTrend = elder.StockTrend,
                          PositionType = elder.PositionType,
                          PositionStatus = elder.PositionStatus,
                          isRealOrderPlaced = elder.isRealOrderPlaced,
                          //OrderId = elder.OrderId,
                          //SLOrderId = elder.SLOrderId,
                          Variety = elder.OrderVariety,
                          SLOrderStatus = elder.SLOrderStatus,
                          TargetOrderStatus = elder.TargetOrderStatus
                      };

            elderOrder = res.ToList();

            return elderOrder;
        }

        #region Camarilla Commented
        //public void InsertCamarillaStrateyOrder(CamarillaStrategyOrder camarillaOrder)
        //{
        //    tradeGenie.CamarillaStrategyOrders.Add(camarillaOrder);

        //    tradeGenie.SaveChanges();
        //}

        //public void UpdateCamarillaStrategyOrders(List<CamarillaStrategyOrder> camarillaOrders)
        //{
        //    foreach (CamarillaStrategyOrder camarillaOrder in camarillaOrders)
        //    {
        //        CamarillaStrategyOrder dbItem = tradeGenie.CamarillaStrategyOrders.Where(x => x.OrderId == camarillaOrder.OrderId).FirstOrDefault<CamarillaStrategyOrder>();

        //        dbItem.ExitPrice = camarillaOrder.ExitPrice;
        //        dbItem.ExitTime = camarillaOrder.ExitTime;
        //        dbItem.SLPrice = camarillaOrder.SLPrice;
        //        dbItem.SLHitTime = camarillaOrder.SLHitTime;
        //        dbItem.ProfitLoss = camarillaOrder.ProfitLoss;
        //        dbItem.SLOrderId = camarillaOrder.SLOrderId;
        //        dbItem.PositionStatus = camarillaOrder.PositionStatus;
        //        dbItem.TargetOrderStatus = camarillaOrder.TargetOrderStatus;
        //        dbItem.SLOrderStatus = camarillaOrder.SLOrderStatus;
        //        dbItem.TargetOrderId = camarillaOrder.TargetOrderId;
        //    }
        //}

        //public List<CamarillaStrategyOrderVM> GetCamarillaStrategyOrdersForView(DateTime tradeDate)
        //{
        //    List<CamarillaStrategyOrderVM> elderOrder = new List<CamarillaStrategyOrderVM>();

        //    var res = from camarilla in tradeGenie.CamarillaStrategyOrders
        //              join ohlc in tradeGenie.OHLCDatas on camarilla.InstrumentToken equals ohlc.InstrumentToken
        //              where ohlc.OHLCDate == tradeDate && camarilla.TradeDate == tradeDate
        //              select new CamarillaStrategyOrderVM
        //              {
        //                  InstrumentToken = camarilla.InstrumentToken,
        //                  TradingSymbol = camarilla.TradingSymbol,
        //                  TradeDate = camarilla.TradeDate,
        //                  EntryPrice = camarilla.EntryPrice,
        //                  ExitPrice = camarilla.ExitPrice,
        //                  SLPrice = camarilla.SLPrice,
        //                  Ltp = ohlc.LastPrice,
        //                  Quantity = camarilla.Quantity,
        //                  ProfitLoss = camarilla.ProfitLoss,
        //                  EntryTime = camarilla.EntryTime,
        //                  ExitTime = camarilla.ExitTime,
        //                  SLHitTime = camarilla.SLHitTime,
        //                  PositionType = camarilla.PositionType,
        //                  PositionStatus = camarilla.PositionStatus,
        //                  isRealOrderPlaced = camarilla.isRealOrderPlaced,
        //                  Variety = camarilla.OrderVariety,
        //                  SLOrderStatus = camarilla.SLOrderStatus,
        //                  TargetOrderStatus = camarilla.TargetOrderStatus
        //              };

        //    elderOrder = res.ToList();

        //    return elderOrder;
        //}
        #endregion

        public List<OHLCData> GetOHLCForScripts(List<string> tradingSymbols)
        {
            List<OHLCData> ohlcData = new List<OHLCData>();

            ohlcData = tradeGenie.OHLCDatas.Where(con => tradingSymbols.Contains(con.TradingSymbol) && con.LastUpdatedTime >= DateTime.Today).ToList(); 

            return ohlcData;
        }

        public TickerMinElderIndicator GetCurrentElderInfo(string tradingSymbol, DateTime currentDateTime, int timePeriod)
        {
            TickerMinElderIndicator currentElderInfo = new TickerMinElderIndicator();

            currentElderInfo = tickerDB.TickerMinElderIndicators.Where(con => con.StockCode == tradingSymbol
                                && con.TimePeriod == timePeriod && con.TickerDateTime == currentDateTime && con.EMA1 != null)
                                .OrderByDescending(x => x.TickerDateTime).First();

            return currentElderInfo;
        }

        public decimal GetLTP(string tradingSymbol)
        {
            decimal ltp = 0.0M;

            var result = tradeGenie.OHLCDatas.Where(con => con.TradingSymbol == tradingSymbol && con.LastUpdatedTime >= DateTime.Today).Select(sel => sel.LastPrice).FirstOrDefault();

            ltp = Convert.ToDecimal(result);

            return ltp;
        }

        public OHLCData GetOHLC(string tradingSymbol)
        {
            OHLCData ohlc = new OHLCData();

            ohlc = tradeGenie.OHLCDatas.Where(con => con.TradingSymbol == tradingSymbol && con.LastUpdatedTime >= DateTime.Today).FirstOrDefault();

            return ohlc;
        }

        public List<ElderStrategyOrder> GetPendingOrders(DateTime tradeDate)
        {
            List<ElderStrategyOrder> elderOrders = new List<ElderStrategyOrder>();

            elderOrders = tradeGenie.ElderStrategyOrders.Where(con => con.TradeDate == tradeDate && con.PositionStatus != Constants.ORDER_STATUS_COMPLETE).ToList();

            return elderOrders;
        }

        public void GetOrderInfo(DateTime tradeDate, string orderId, out string slOrderId, 
                                out decimal entryPrice, out decimal exitPrice, 
                                out decimal slPrice,  out string slOrderStatus, 
                                out string targetOrderStatus, out string targetOrderId)
        {
            exitPrice = 0.0M;
            slPrice = 0.0M;
            entryPrice = 0.0M;
            slOrderStatus = string.Empty;
            slOrderId = string.Empty;
            targetOrderId = string.Empty;
            targetOrderStatus = string.Empty;

            try
            {
                List<spGetOrders_Result> orders = tradeGenie.spGetOrders(tradeDate).ToList();

                //var slOrderInfo = tradeGenie.Orders.Where(con => con.ParentOrderId == orderId).FirstOrDefault();
                var slOrderInfo = orders.Where(con => con.ParentOrderId == orderId).FirstOrDefault();

                if (slOrderInfo.Variety == Constants.VARIETY_CO)
                {
                    //var orderInfo = tradeGenie.Orders.Where(con => con.OrderId == orderId).FirstOrDefault();
                    var orderInfo = orders.Where(con => con.OrderId == orderId).FirstOrDefault();

                    slOrderId = slOrderInfo.OrderId;

                    if (orderInfo.Status == Utilities.OS_Complete)
                    {
                        if (slOrderInfo.Status == Utilities.OS_Complete)
                        {
                            if ((decimal)slOrderInfo.AveragePrice != 0.0M)
                                exitPrice = (decimal)slOrderInfo.AveragePrice;
                            else if ((decimal)slOrderInfo.Price != 0.0M)
                                exitPrice = (decimal)slOrderInfo.Price;
                            else
                                exitPrice = (decimal)slOrderInfo.TriggerPrice;
                        }
                        else if (slOrderInfo.Status == Utilities.OS_TriggerPending)
                        {
                            slPrice = (decimal)slOrderInfo.TriggerPrice;
                        }

                        slOrderStatus = slOrderInfo.Status;
                    }
                    else if (orderInfo.Status == Utilities.OS_Cancelled || orderInfo.Status == Utilities.OS_Open)
                    {
                        slOrderStatus = orderInfo.Status;
                    }
                }
                else if (slOrderInfo.Variety == Constants.VARIETY_BO)
                {
                    //var targetOrderInfo = tradeGenie.Orders.Where(con => con.ParentOrderId == orderId && con.OrderType == Constants.ORDER_TYPE_LIMIT).FirstOrDefault();
                    var targetOrderInfo = orders.Where(con => con.ParentOrderId == orderId && con.OrderType == Constants.ORDER_TYPE_LIMIT).FirstOrDefault();
                    var boSLOrderInfo = orders.Where(con => con.ParentOrderId == orderId && con.OrderType == Constants.ORDER_TYPE_SLM).FirstOrDefault();

                    slOrderId = boSLOrderInfo.OrderId;
                    slOrderStatus = boSLOrderInfo.Status;

                    targetOrderId = targetOrderInfo.OrderId;
                    targetOrderStatus = targetOrderInfo.Status;

                    if (targetOrderStatus == Constants.ORDER_STATUS_COMPLETE)
                    {
                        exitPrice = (decimal)targetOrderInfo.AveragePrice;
                    }
                    else if (slOrderStatus == Constants.ORDER_STATUS_COMPLETE)
                    {
                        slPrice = (decimal)targetOrderInfo.AveragePrice;
                    }

                }
            }
            catch (Exception ex)
            {
                Logger.ErrorLogToFile(ex);
            }
        }

        

        public void RefreshOrders(DataTable dtOrders)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(UserConfiguration.TradeGenieConString))
                {
                    using (SqlCommand sqlComm = new SqlCommand("spAddUpdateOrders"))
                    {
                        sqlComm.Connection = sqlConn;
                        sqlComm.CommandType = CommandType.StoredProcedure;

                        SqlParameter tblParam = new SqlParameter("@tblOrders", SqlDbType.Structured);
                        tblParam.Value = dtOrders;

                        sqlComm.Parameters.Add(tblParam);

                        sqlConn.Open();
                        int ret = sqlComm.ExecuteNonQuery();
                        sqlConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //UserConfiguration.WriteErrorLog(ex);
                Logger.ErrorLogToFile(ex);
            }
        }

        public void RefreshPositions(DataTable dtPositions)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(UserConfiguration.TradeGenieConString))
                {
                    using (SqlCommand sqlComm = new SqlCommand("spAddUpdateNetPositions"))
                    {
                        sqlComm.Connection = sqlConn;
                        sqlComm.CommandType = CommandType.StoredProcedure;

                        SqlParameter tblParam = new SqlParameter("@tblNetPositions", SqlDbType.Structured);
                        tblParam.Value = dtPositions;

                        sqlComm.Parameters.Add(tblParam);

                        sqlConn.Open();
                        int ret = sqlComm.ExecuteNonQuery();
                        sqlConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //UserConfiguration.WriteErrorLog(ex);
                Logger.ErrorLogToFile(ex);
            }
        }

        public void RefreshTrades(DataTable dtTrades)
        {
            try
            {
                using (SqlConnection sqlConn = new SqlConnection(UserConfiguration.TradeGenieConString))
                {
                    using (SqlCommand sqlComm = new SqlCommand("spAddUpdateTrades"))
                    {
                        sqlComm.Connection = sqlConn;
                        sqlComm.CommandType = CommandType.StoredProcedure;

                        SqlParameter tblParam = new SqlParameter("@tblTrades", SqlDbType.Structured);
                        tblParam.Value = dtTrades;

                        sqlComm.Parameters.Add(tblParam);

                        sqlConn.Open();
                        int ret = sqlComm.ExecuteNonQuery();
                        sqlConn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //UserConfiguration.WriteErrorLog(ex);
                Logger.ErrorLogToFile(ex);
            }
        }

        public void GetPLStatus(DateTime tradeDate, out decimal profit, out decimal loss, out decimal profitLoss, 
                                out int boCompleted, out int boPending, out int boTotal,
                                out int coCompleted, out int coPending, out int coTotal)
        {
            profit = 0.0M;
            loss = 0.0M;
            profitLoss = 0.0M;

            boCompleted = coCompleted = 0;
            boPending = coPending = 0;
            boTotal = coTotal = 0;

            profit = Convert.ToDecimal(tradeGenie.NetPositions.Where(con => con.PositionDate == tradeDate && con.PNL > 0).Sum(s => s.PNL));
            loss = Convert.ToDecimal(tradeGenie.NetPositions.Where(con => con.PositionDate == tradeDate && con.PNL < 0).Sum(s => s.PNL));
            profitLoss = Convert.ToDecimal(tradeGenie.NetPositions.Where(con => con.PositionDate == tradeDate).Sum(s => s.PNL));

            boCompleted = tradeGenie.Orders.Where(con => con.OrderTimestamp >= tradeDate && con.Variety == Constants.VARIETY_BO && con.ParentOrderId != null
                                && ((con.OrderType == Constants.ORDER_TYPE_LIMIT && con.Status == Constants.ORDER_STATUS_COMPLETE)
                                    || (con.OrderType == Constants.ORDER_TYPE_SLM && con.Status == Constants.ORDER_STATUS_COMPLETE))).Count();

            boPending = tradeGenie.Orders.Where(con => con.OrderTimestamp >= tradeDate && con.Variety == Constants.VARIETY_BO && con.ParentOrderId != null
                                && con.OrderType == Constants.ORDER_TYPE_LIMIT && con.Status == Utilities.OS_Open).Count();

            boTotal = tradeGenie.Orders.Where(con => con.OrderTimestamp >= tradeDate && con.Variety == Constants.VARIETY_BO && con.ParentOrderId == null
                                && con.Status == Constants.ORDER_STATUS_COMPLETE ).Count();

            coCompleted = tradeGenie.Orders.Where(con => con.OrderTimestamp >= tradeDate && con.Variety == Constants.VARIETY_CO && con.ParentOrderId != null
                                && con.Status == Constants.ORDER_STATUS_COMPLETE).Count();

            coPending = tradeGenie.Orders.Where(con => con.OrderTimestamp >= tradeDate && con.Variety == Constants.VARIETY_CO && con.ParentOrderId != null
                                && con.Status != Constants.ORDER_STATUS_COMPLETE).Count();

            coTotal = tradeGenie.Orders.Where(con => con.OrderTimestamp >= tradeDate && con.Variety == Constants.VARIETY_CO && con.ParentOrderId == null
                                && con.Status == Constants.ORDER_STATUS_COMPLETE).Count();
        }


        public DayOHLC GetPreviousDayOHLC(string tradingSymbol, DateTime dtDateToRetrieve)
        {
            DayOHLC ohlc = (from prevDay in tickerDB.TickerMinElderIndicators
                                    where prevDay.StockCode == tradingSymbol
                                    && prevDay.TimePeriod == 375
                                    && prevDay.TickerDateTime == dtDateToRetrieve
                                    select new DayOHLC
                                    {
                                        OHLCDate = prevDay.TickerDateTime,
                                        TradingSymbol = prevDay.StockCode,
                                        Open = (double)prevDay.PriceOpen,
                                        High = (double)prevDay.PriceHigh,
                                        Low = (double)prevDay.PriceLow,
                                        Close = (double)prevDay.PriceClose
                                    }).SingleOrDefault();


            return ohlc;
        }

        public DayOHLC GetCurrentDayOHLC(string tradingSymbol, int timePeriod, DateTime dtBeginTime, DateTime dtEndTime)
        {
            var dayHigh = (from currDay in tickerDB.TickerMinElderIndicators
                           where currDay.StockCode == tradingSymbol
                           && currDay.TimePeriod == timePeriod
                           && currDay.TickerDateTime >= dtBeginTime
                           && currDay.TickerDateTime < dtEndTime
                           select currDay.PriceHigh).Max();

            var dayLow = (from currDay in tickerDB.TickerMinElderIndicators
                          where currDay.StockCode == tradingSymbol
                          && currDay.TimePeriod == timePeriod
                          && currDay.TickerDateTime >= dtBeginTime
                          && currDay.TickerDateTime < dtEndTime
                          select currDay.PriceLow).Min();

            var dayOpen = (from currDay in tickerDB.TickerMinElderIndicators
                           where currDay.StockCode == tradingSymbol
                           && currDay.TimePeriod == timePeriod
                           && currDay.TickerDateTime == dtBeginTime
                           select currDay.PriceOpen).Single();

            var dayClose = (from currDay in tickerDB.TickerMinElderIndicators
                           where currDay.StockCode == tradingSymbol
                           && currDay.TimePeriod == timePeriod
                           && currDay.TickerDateTime == dtBeginTime
                           select currDay.PriceOpen).Single();


            DayOHLC ohlc = new DayOHLC();
            ohlc.OHLCDate = dtBeginTime;

            if (dayHigh != null)
                ohlc.High = (double)dayHigh;
            if (dayLow != null)
                ohlc.Low = (double)dayLow;

            ohlc.Open = (double)dayOpen;
            ohlc.Close = (double)dayClose;

            ohlc.TradingSymbol = tradingSymbol;

            return ohlc;
        }
    }
}
