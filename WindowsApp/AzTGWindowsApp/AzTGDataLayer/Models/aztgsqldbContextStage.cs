using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AzTGDataLayer.Models
{
    public partial class aztgsqldbContextStage : DbContext
    {
        public aztgsqldbContextStage()
        {
        }

        public aztgsqldbContextStage(DbContextOptions<aztgsqldbContextStage> options)
            : base(options)
        {
        }

        public virtual DbSet<BackTesting> BackTesting { get; set; }
        public virtual DbSet<DailyVolatality> DailyVolatality { get; set; }
        public virtual DbSet<ElderBackTesting> ElderBackTesting { get; set; }
        public virtual DbSet<ElderStrategyOrders> ElderStrategyOrders { get; set; }
        public virtual DbSet<Instruments> Instruments { get; set; }
        public virtual DbSet<Margins> Margins { get; set; }
        public virtual DbSet<MasterStockList> MasterStockList { get; set; }
        public virtual DbSet<MorningBreakouts> MorningBreakouts { get; set; }
        public virtual DbSet<NetPositions> NetPositions { get; set; }
        public virtual DbSet<Ohlcdata> Ohlcdata { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Quotes> Quotes { get; set; }
        public virtual DbSet<TickerMin> TickerMin { get; set; }
        public virtual DbSet<TickerMinElderIndicators> TickerMinElderIndicators { get; set; }
        public virtual DbSet<TickerMinEmaha> TickerMinEmaha { get; set; }
        public virtual DbSet<TickerMinSuperTrend> TickerMinSuperTrend { get; set; }
        public virtual DbSet<TickerRealTime> TickerRealTime { get; set; }
        public virtual DbSet<TickerRealTimeFo> TickerRealTimeFo { get; set; }
        public virtual DbSet<TickerRealTimeIndex> TickerRealTimeIndex { get; set; }
        public virtual DbSet<TickerRealTimeIndicators> TickerRealTimeIndicators { get; set; }
        public virtual DbSet<Trades> Trades { get; set; }
        public virtual DbSet<UserLogins> UserLogins { get; set; }

        // Unable to generate entity type for table 'dbo.ConsolidatedStockStatistics'. Please see the warning messages.
        // Unable to generate entity type for table 'dbo.ErrorTable'. Please see the warning messages.

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:aztgsqlserver.database.windows.net,1433;Initial Catalog=aztgsqldb-stage;Persist Security Info=False;User ID=aztgsqlserver;Password=N@ren123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.6-servicing-10079");

            modelBuilder.Entity<BackTesting>(entity =>
            {
                entity.HasKey(e => e.TransactionId);

                entity.Property(e => e.BuyDateTime).HasColumnType("datetime");

                entity.Property(e => e.BuyType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.SellDateTime).HasColumnType("datetime");

                entity.Property(e => e.SellType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StockCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DailyVolatality>(entity =>
            {
                entity.HasKey(e => new { e.RunDate, e.Symbol });

                entity.Property(e => e.RunDate).HasColumnType("date");

                entity.Property(e => e.Symbol)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ElderBackTesting>(entity =>
            {
                entity.HasKey(e => new { e.InstrumentToken, e.TradingSymbol, e.TradeDate, e.EntryTime });

                entity.Property(e => e.TradingSymbol)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TradeDate).HasColumnType("date");

                entity.Property(e => e.EntryTime).HasColumnType("datetime");

                entity.Property(e => e.Ema1).HasColumnName("EMA1");

                entity.Property(e => e.Ema1dev).HasColumnName("EMA1Dev");

                entity.Property(e => e.Ema2).HasColumnName("EMA2");

                entity.Property(e => e.Ema2dev).HasColumnName("EMA2Dev");

                entity.Property(e => e.ExitTime).HasColumnType("datetime");

                entity.Property(e => e.HistogramMovement)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Impulse)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.OrderType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ElderStrategyOrders>(entity =>
            {
                entity.HasKey(e => new { e.InstrumentToken, e.TradingSymbol, e.TradeDate, e.EntryTime })
                    .HasName("PK_ElderTrend");

                entity.Property(e => e.TradingSymbol)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TradeDate).HasColumnType("date");

                entity.Property(e => e.EntryTime).HasColumnType("datetime");

                entity.Property(e => e.CurrClose).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrHigh).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrHistogramMovement)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CurrImpulse)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CurrLow).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.CurrOpen).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EntryPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExitPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExitTime).HasColumnType("datetime");

                entity.Property(e => e.IsRealOrderPlaced).HasColumnName("isRealOrderPlaced");

                entity.Property(e => e.LongEma).HasColumnName("LongEMA");

                entity.Property(e => e.LongEmadev).HasColumnName("LongEMADev");

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PositionStatus)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PositionType)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PrevClose).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PrevHigh).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PrevHistogramMovement)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PrevImpulse)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.PrevLow).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PrevOpen).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProfitLoss).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ShortEma).HasColumnName("ShortEMA");

                entity.Property(e => e.ShortEmadev).HasColumnName("ShortEMADev");

                entity.Property(e => e.SlorderId)
                    .HasColumnName("SLOrderId")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StockTrend)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StopLoss).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StopLossHitTime).HasColumnType("datetime");
            });

            modelBuilder.Entity<Instruments>(entity =>
            {
                entity.HasKey(e => e.InstrumentToken)
                    .HasName("PK_Instrument");

                entity.Property(e => e.InstrumentToken).ValueGeneratedNever();

                entity.Property(e => e.Colower)
                    .HasColumnName("COLower")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Coupper)
                    .HasColumnName("COUpper")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Exchange)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Expiry)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InstrumentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Margin).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mismargin)
                    .HasColumnName("MISMargin")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Mismultiplier)
                    .HasColumnName("MISMultiplier")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Nrmlmargin)
                    .HasColumnName("NRMLMargin")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Segment)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Strike).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TickSize).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TradingSymbol)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Margins>(entity =>
            {
                entity.HasKey(e => e.MarginDate)
                    .HasName("PK_Margin");

                entity.Property(e => e.MarginDate).HasColumnType("datetime");

                entity.Property(e => e.AdhocMargin).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Cash).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Collateral).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Debits).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Exposure).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.HoldingSales).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IntradayPayin).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.M2mrealised)
                    .HasColumnName("M2MRealised")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.M2munrealised)
                    .HasColumnName("M2MUnrealised")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Net).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OptionPremium).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Span).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Turnover).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<MasterStockList>(entity =>
            {
                entity.HasKey(e => e.InstrumentToken);

                entity.Property(e => e.InstrumentToken).ValueGeneratedNever();

                entity.Property(e => e.Collection)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.TradingSymbol)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<MorningBreakouts>(entity =>
            {
                entity.HasKey(e => new { e.InstrumentToken, e.DateTimePeriod })
                    .HasName("PK_MorningBreakout");

                entity.Property(e => e.DateTimePeriod).HasColumnType("datetime");

                entity.Property(e => e.CandleSize).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Close).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DayHigh).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.DayLow).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Entry).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EntryTime).HasColumnType("datetime");

                entity.Property(e => e.Exit).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ExitTime).HasColumnType("datetime");

                entity.Property(e => e.High).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.IsRealOrderPlaced).HasColumnName("isRealOrderPlaced");

                entity.Property(e => e.Low).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ltp)
                    .HasColumnName("LTP")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Movement)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Open).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ProfitLoss).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Ragstatus)
                    .HasColumnName("RAGStatus")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StopLoss).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StopLossHitTime).HasColumnType("datetime");

                entity.Property(e => e.TradingSymbol)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NetPositions>(entity =>
            {
                entity.HasKey(e => new { e.InstrumentToken, e.PositionDate, e.TradingSymbol })
                    .HasName("PK_NetPosition");

                entity.Property(e => e.PositionDate).HasColumnType("date");

                entity.Property(e => e.TradingSymbol)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AveragePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BuyM2m)
                    .HasColumnName("BuyM2M")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BuyPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.BuyValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ClosePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Exchange)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LastPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.M2m)
                    .HasColumnName("M2M")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Multiplier).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NetBuyAmountM2m)
                    .HasColumnName("NetBuyAmountM2M")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.NetSellAmountM2m)
                    .HasColumnName("NetSellAmountM2M")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Pnl)
                    .HasColumnName("PNL")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Product)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Realised).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SellM2m)
                    .HasColumnName("SellM2M")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SellPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SellValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Unrealised).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Value).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Ohlcdata>(entity =>
            {
                entity.HasKey(e => new { e.InstrumentToken, e.Ohlcdate });

                entity.ToTable("OHLCData");

                entity.Property(e => e.Ohlcdate)
                    .HasColumnName("OHLCDate")
                    .HasColumnType("date");

                entity.Property(e => e.High).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LastPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LastUpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.Low).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Open).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.PreviousClose).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TradingSymbol)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.ExchangeOrderId });

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeOrderId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AveragePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Exchange).HasMaxLength(10);

                entity.Property(e => e.ExchangeTimestamp).HasColumnType("datetime");

                entity.Property(e => e.OrderTimestamp).HasColumnType("datetime");

                entity.Property(e => e.OrderType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ParentOrderId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PlacedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Product)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StatusMessage)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tag)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tradingsymbol)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TriggerPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Validity)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Variety)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Quotes>(entity =>
            {
                entity.HasKey(e => new { e.InstrumentToken, e.LastUpdatedTime });

                entity.Property(e => e.LastUpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.Change).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.ChangePercent).HasColumnType("decimal(6, 2)");

                entity.Property(e => e.Close).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.High).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LastPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Low).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Open).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OpenInterest).HasColumnType("decimal(6, 2)");
            });

            modelBuilder.Entity<TickerMin>(entity =>
            {
                entity.HasKey(e => new { e.InstrumentToken, e.TradingSymbol, e.DateTime });

                entity.Property(e => e.TradingSymbol)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Close).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.High).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Low).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Open).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<TickerMinElderIndicators>(entity =>
            {
                entity.HasKey(e => new { e.StockCode, e.TickerDateTime, e.TimePeriod });

                entity.Property(e => e.StockCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TickerDateTime).HasColumnType("datetime");

                entity.Property(e => e.Ag1).HasColumnName("AG1");

                entity.Property(e => e.Ag2).HasColumnName("AG2");

                entity.Property(e => e.Al1).HasColumnName("AL1");

                entity.Property(e => e.Al2).HasColumnName("AL2");

                entity.Property(e => e.Ema1).HasColumnName("EMA1");

                entity.Property(e => e.Ema1dev).HasColumnName("EMA1Dev");

                entity.Property(e => e.Ema2).HasColumnName("EMA2");

                entity.Property(e => e.Ema2dev).HasColumnName("EMA2Dev");

                entity.Property(e => e.Ema3).HasColumnName("EMA3");

                entity.Property(e => e.Ema4).HasColumnName("EMA4");

                entity.Property(e => e.HistIncDec)
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Impulse).HasMaxLength(50);

                entity.Property(e => e.Macd).HasColumnName("MACD");

                entity.Property(e => e.Rsi1).HasColumnName("RSI1");

                entity.Property(e => e.Rsi2).HasColumnName("RSI2");

                entity.Property(e => e.TradedValue).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<TickerMinEmaha>(entity =>
            {
                entity.HasKey(e => new { e.StockCode, e.TickerDateTime, e.TimePeriod });

                entity.ToTable("TickerMinEMAHA");

                entity.Property(e => e.StockCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TickerDateTime).HasColumnType("datetime");

                entity.Property(e => e.AllEmasInNum).HasColumnName("AllEMAsInNum");

                entity.Property(e => e.Ehema1).HasColumnName("EHEMA1");

                entity.Property(e => e.Ehema2).HasColumnName("EHEMA2");

                entity.Property(e => e.Ehema3).HasColumnName("EHEMA3");

                entity.Property(e => e.Ehema4).HasColumnName("EHEMA4");

                entity.Property(e => e.Ehema5).HasColumnName("EHEMA5");

                entity.Property(e => e.Ema2v34)
                    .HasColumnName("EMA2v34")
                    .HasMaxLength(2)
                    .IsUnicode(false);

                entity.Property(e => e.Haclose).HasColumnName("HAClose");

                entity.Property(e => e.Hahigh).HasColumnName("HAHigh");

                entity.Property(e => e.Halow).HasColumnName("HALow");

                entity.Property(e => e.HaocwEma1)
                    .HasColumnName("HAOCwEMA1")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.Haopen).HasColumnName("HAOpen");

                entity.Property(e => e.OcwEma1)
                    .HasColumnName("OCwEMA1")
                    .HasMaxLength(1)
                    .IsUnicode(false);

                entity.Property(e => e.VarCvPc).HasColumnName("varCvPC");

                entity.Property(e => e.VarEma1v2).HasColumnName("varEMA1v2");

                entity.Property(e => e.VarEma1v3).HasColumnName("varEMA1v3");

                entity.Property(e => e.VarEma1v4).HasColumnName("varEMA1v4");

                entity.Property(e => e.VarEma2v3).HasColumnName("varEMA2v3");

                entity.Property(e => e.VarEma2v4).HasColumnName("varEMA2v4");

                entity.Property(e => e.VarEma3v4).HasColumnName("varEMA3v4");

                entity.Property(e => e.VarEma4v5).HasColumnName("varEMA4v5");

                entity.Property(e => e.VarHacvHapc).HasColumnName("varHACvHAPC");

                entity.Property(e => e.VarHaovHac).HasColumnName("varHAOvHAC");

                entity.Property(e => e.VarHaovHapo).HasColumnName("varHAOvHAPO");

                entity.Property(e => e.VarOvC).HasColumnName("varOvC");

                entity.Property(e => e.VarOvPo).HasColumnName("varOvPO");

                entity.Property(e => e.VarVwma1vEma1).HasColumnName("varVWMA1vEMA1");

                entity.Property(e => e.VarVwma1vPriceClose).HasColumnName("varVWMA1vPriceClose");

                entity.Property(e => e.VarVwma1vVwma2).HasColumnName("varVWMA1vVWMA2");

                entity.Property(e => e.VarVwma2vPriceClose).HasColumnName("varVWMA2vPriceClose");

                entity.Property(e => e.Vwma1).HasColumnName("VWMA1");

                entity.Property(e => e.Vwma2).HasColumnName("VWMA2");
            });

            modelBuilder.Entity<TickerMinSuperTrend>(entity =>
            {
                entity.HasKey(e => new { e.StockCode, e.TickerDateTime, e.TimePeriod });

                entity.Property(e => e.StockCode)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.TickerDateTime).HasColumnType("datetime");

                entity.Property(e => e.Atr1).HasColumnName("ATR1");

                entity.Property(e => e.Atr2).HasColumnName("ATR2");

                entity.Property(e => e.Atr3).HasColumnName("ATR3");

                entity.Property(e => e.Blb1).HasColumnName("BLB1");

                entity.Property(e => e.Blb2).HasColumnName("BLB2");

                entity.Property(e => e.Blb3).HasColumnName("BLB3");

                entity.Property(e => e.Bub1).HasColumnName("BUB1");

                entity.Property(e => e.Bub2).HasColumnName("BUB2");

                entity.Property(e => e.Bub3).HasColumnName("BUB3");

                entity.Property(e => e.Flb1).HasColumnName("FLB1");

                entity.Property(e => e.Flb2).HasColumnName("FLB2");

                entity.Property(e => e.Flb3).HasColumnName("FLB3");

                entity.Property(e => e.Fub1).HasColumnName("FUB1");

                entity.Property(e => e.Fub2).HasColumnName("FUB2");

                entity.Property(e => e.Fub3).HasColumnName("FUB3");

                entity.Property(e => e.St1).HasColumnName("ST1");

                entity.Property(e => e.St2).HasColumnName("ST2");

                entity.Property(e => e.St3).HasColumnName("ST3");

                entity.Property(e => e.Trend1)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Trend2)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Trend3)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TickerRealTime>(entity =>
            {
                entity.HasKey(e => new { e.StockCode, e.TickerDate });

                entity.Property(e => e.StockCode).HasMaxLength(50);

                entity.Property(e => e.TickerDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.Ltp).HasColumnName("LTP");
            });

            modelBuilder.Entity<TickerRealTimeFo>(entity =>
            {
                entity.HasKey(e => new { e.StockCode, e.TickerDate });

                entity.ToTable("TickerRealTimeFO");

                entity.Property(e => e.StockCode).HasMaxLength(50);

                entity.Property(e => e.TickerDate).HasColumnType("datetime");

                entity.Property(e => e.LastUpdatedTime).HasColumnType("datetime");

                entity.Property(e => e.Ltp).HasColumnName("LTP");
            });

            modelBuilder.Entity<TickerRealTimeIndex>(entity =>
            {
                entity.HasKey(e => new { e.IndexName, e.LastTickerTime });

                entity.Property(e => e.IndexName).HasMaxLength(50);

                entity.Property(e => e.LastTickerTime).HasColumnType("datetime");

                entity.Property(e => e.Ltp).HasColumnName("LTP");
            });

            modelBuilder.Entity<TickerRealTimeIndicators>(entity =>
            {
                entity.HasKey(e => new { e.TimePeriod, e.StockCode, e.PeriodDateTime });

                entity.Property(e => e.StockCode).HasMaxLength(50);

                entity.Property(e => e.PeriodDateTime).HasColumnType("datetime");

                entity.Property(e => e.Atr).HasColumnName("ATR");

                entity.Property(e => e.Ema1).HasColumnName("EMA1");

                entity.Property(e => e.Ema2).HasColumnName("EMA2");

                entity.Property(e => e.Ema3).HasColumnName("EMA3");

                entity.Property(e => e.Ema4).HasColumnName("EMA4");

                entity.Property(e => e.Impulse).HasMaxLength(50);

                entity.Property(e => e.Macd).HasColumnName("MACD");

                entity.Property(e => e.Obv).HasColumnName("OBV");

                entity.Property(e => e.Rsi1).HasColumnName("RSI1");

                entity.Property(e => e.Rsi2).HasColumnName("RSI2");
            });

            modelBuilder.Entity<Trades>(entity =>
            {
                entity.HasKey(e => new { e.TradeId, e.OrderId, e.ExchangeOrderId })
                    .HasName("PK_Trade");

                entity.Property(e => e.TradeId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OrderId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeOrderId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AveragePrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Exchange)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ExchangeTimestamp).HasColumnType("datetime");

                entity.Property(e => e.OrderTimestamp).HasColumnType("datetime");

                entity.Property(e => e.Product)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TradingSymbol)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserLogins>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Broker)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.AccessToken)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.ClientId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.LoginDateTime).HasColumnType("datetime");

                entity.Property(e => e.LogoutDateTime).HasColumnType("datetime");

                entity.Property(e => e.PublicToken)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RequestToken)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });
        }
    }
}
