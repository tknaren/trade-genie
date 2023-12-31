USE [master]
GO
/****** Object:  Database [TradeGenie]    Script Date: 08-02-2018 15:56:18 ******/
CREATE DATABASE [TradeGenie]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'TradeGenie', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\TradeGenie.mdf' , SIZE = 18432KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'TradeGenie_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\TradeGenie_log.ldf' , SIZE = 7616KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [TradeGenie] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [TradeGenie].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [TradeGenie] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [TradeGenie] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [TradeGenie] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [TradeGenie] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [TradeGenie] SET ARITHABORT OFF 
GO
ALTER DATABASE [TradeGenie] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [TradeGenie] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [TradeGenie] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [TradeGenie] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [TradeGenie] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [TradeGenie] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [TradeGenie] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [TradeGenie] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [TradeGenie] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [TradeGenie] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [TradeGenie] SET  DISABLE_BROKER 
GO
ALTER DATABASE [TradeGenie] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [TradeGenie] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [TradeGenie] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [TradeGenie] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [TradeGenie] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [TradeGenie] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [TradeGenie] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [TradeGenie] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [TradeGenie] SET  MULTI_USER 
GO
ALTER DATABASE [TradeGenie] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [TradeGenie] SET DB_CHAINING OFF 
GO
ALTER DATABASE [TradeGenie] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [TradeGenie] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [TradeGenie]
GO
/****** Object:  User [TradeAdmin]    Script Date: 08-02-2018 15:56:18 ******/
CREATE USER [TradeAdmin] FOR LOGIN [TradeAdmin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [TradeAdmin]
GO
/****** Object:  UserDefinedTableType [dbo].[ElderStrategyOrderType]    Script Date: 08-02-2018 15:56:18 ******/
CREATE TYPE [dbo].[ElderStrategyOrderType] AS TABLE(
	[InstrumentToken] [int] NOT NULL,
	[TradingSymbol] [varchar](50) NOT NULL,
	[TradeDate] [date] NOT NULL,
	[EntryPrice] [decimal](18, 2) NULL,
	[ExitPrice] [decimal](18, 2) NULL,
	[StopLoss] [decimal](18, 2) NULL,
	[Quantity] [int] NULL,
	[ProfitLoss] [decimal](18, 2) NULL,
	[EntryTime] [datetime] NOT NULL,
	[ExitTime] [datetime] NULL,
	[StopLossHitTime] [datetime] NULL,
	[CurrOpen] [decimal](18, 2) NULL,
	[CurrHigh] [decimal](18, 2) NULL,
	[CurrLow] [decimal](18, 2) NULL,
	[CurrClose] [decimal](18, 2) NULL,
	[PrevOpen] [decimal](18, 2) NULL,
	[PrevHigh] [decimal](18, 2) NULL,
	[PrevLow] [decimal](18, 2) NULL,
	[PrevClose] [decimal](18, 2) NULL,
	[ShortEMA] [float] NULL,
	[LongEMA] [float] NULL,
	[ShortEMADev] [float] NULL,
	[LongEMADev] [float] NULL,
	[ForceIndex] [float] NULL,
	[CurrHistogramMovement] [varchar](10) NULL,
	[PrevHistogramMovement] [varchar](10) NULL,
	[CurrImpulse] [varchar](10) NULL,
	[PrevImpulse] [varchar](10) NULL,
	[StockTrend] [varchar](10) NULL,
	[PositionType] [varchar](10) NULL,
	[PositionStatus] [varchar](10) NULL,
	[isRealOrderPlaced] [bit] NULL,
	[OrderId] [varchar](50) NULL,
	[SLOrderId] [varchar](50) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[OHLCType]    Script Date: 08-02-2018 15:56:18 ******/
CREATE TYPE [dbo].[OHLCType] AS TABLE(
	[InstrumentToken] [int] NOT NULL,
	[OHLCDate] [date] NOT NULL,
	[LastUpdatedTime] [datetime] NOT NULL,
	[TradingSymbol] [varchar](50) NOT NULL,
	[PreviousClose] [decimal](10, 2) NULL,
	[Open] [decimal](10, 2) NULL,
	[High] [decimal](10, 2) NULL,
	[Low] [decimal](10, 2) NULL,
	[LastPrice] [decimal](10, 2) NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[OrderType]    Script Date: 08-02-2018 15:56:18 ******/
CREATE TYPE [dbo].[OrderType] AS TABLE(
	[OrderId] [varchar](50) NOT NULL,
	[ExchangeOrderId] [varchar](50) NOT NULL,
	[ParentOrderId] [varchar](50) NULL,
	[InstrumentToken] [int] NULL,
	[Exchange] [nchar](10) NULL,
	[OrderTimestamp] [datetime] NULL,
	[ExchangeTimestamp] [datetime] NULL,
	[OrderType] [varchar](50) NULL,
	[Tradingsymbol] [varchar](50) NULL,
	[TransactionType] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[CancelledQuantity] [int] NULL,
	[DisclosedQuantity] [int] NULL,
	[FilledQuantity] [int] NULL,
	[PendingQuantity] [int] NULL,
	[Price] [decimal](18, 2) NULL,
	[TriggerPrice] [decimal](18, 2) NULL,
	[AveragePrice] [decimal](18, 2) NULL,
	[Product] [varchar](50) NULL,
	[PlacedBy] [varchar](50) NULL,
	[Validity] [varchar](50) NULL,
	[Variety] [varchar](50) NULL,
	[Tag] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[StatusMessage] [varchar](50) NULL
)
GO
/****** Object:  StoredProcedure [dbo].[spAddUpdateOrders]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spAddUpdateOrders]
      @tblOrders OrderType READONLY
AS
BEGIN
      SET NOCOUNT ON;
 
      MERGE INTO Orders t1
      USING @tblOrders t2
      ON t1.OrderId = t2.OrderId and t1.ExchangeOrderId = t2.ExchangeOrderId
	  WHEN MATCHED THEN
	  UPDATE
	  SET	t1.OrderTimestamp = t2.OrderTimestamp,
			t1.ExchangeTimestamp = t2.ExchangeTimestamp,
			t1.Quantity = t2.Quantity,
			t1.CancelledQuantity = t2.CancelledQuantity, 	
			t1.DisclosedQuantity = t2.DisclosedQuantity,
			t1.FilledQuantity = t2.FilledQuantity,
			t1.PendingQuantity = t2.PendingQuantity,
			t1.Price = t2.Price,
			t1.TriggerPrice = t2.TriggerPrice,
			t1.AveragePrice = t2.AveragePrice,
			t1.Product = t2.Product,
			t1.PlacedBy = t2.PlacedBy,
			t1.Validity = t2.Validity,
			t1.Variety = t2.Variety,
			t1.Tag = t2.Tag,
			t1.Status = t2.Status,
			t1.StatusMessage = t2.StatusMessage
      WHEN NOT MATCHED THEN
      INSERT VALUES(t2.OrderId, t2.ExchangeOrderId, t2.ParentOrderId, t2.InstrumentToken, t2.Exchange, 
				t2.OrderTimestamp, t2.ExchangeTimestamp, t2.OrderType, t2.Tradingsymbol, t2.TransactionType, t2.Quantity,
				t2.CancelledQuantity, t2.DisclosedQuantity, t2.FilledQuantity, t2.PendingQuantity, t2.Price,
				t2.TriggerPrice, t2.AveragePrice, t2.Product, t2.PlacedBy, t2.Validity, t2.Variety,
				t2.Tag, t2.Status, t2.StatusMessage);
END



GO
/****** Object:  StoredProcedure [dbo].[spUpdateOHLC]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spUpdateOHLC]
      @tblOHLC OHLCType READONLY
AS
BEGIN
      SET NOCOUNT ON;
 
      MERGE INTO OHLCData t1
      USING @tblOHLC t2
      ON t1.InstrumentToken = t2.InstrumentToken and t1.OHLCDate = t2.OHLCDate
	  WHEN MATCHED THEN
	  UPDATE
	  SET	t1.LastUpdatedTime = t2.LastUpdatedTime,
			t1.PreviousClose = t2.PreviousClose,
			t1.[Open] = t2.[Open],
			t1.High = t2.High, 	
			t1.Low = t2.Low,
			t1.LastPrice = t2.LastPrice
      WHEN NOT MATCHED THEN
      INSERT VALUES(t2.InstrumentToken, t2.OHLCDate, t2.LastUpdatedTime, t2.TradingSymbol, 
			t2.PreviousClose, t2.[Open], t2.[High], t2.[Low], t2.LastPrice);
END



GO
/****** Object:  Table [dbo].[ElderStrategyOrders]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ElderStrategyOrders](
	[InstrumentToken] [int] NOT NULL,
	[TradingSymbol] [varchar](50) NOT NULL,
	[TradeDate] [date] NOT NULL,
	[EntryPrice] [decimal](18, 2) NULL,
	[ExitPrice] [decimal](18, 2) NULL,
	[StopLoss] [decimal](18, 2) NULL,
	[Quantity] [int] NULL,
	[ProfitLoss] [decimal](18, 2) NULL,
	[EntryTime] [datetime] NOT NULL,
	[ExitTime] [datetime] NULL,
	[StopLossHitTime] [datetime] NULL,
	[CurrOpen] [decimal](18, 2) NULL,
	[CurrHigh] [decimal](18, 2) NULL,
	[CurrLow] [decimal](18, 2) NULL,
	[CurrClose] [decimal](18, 2) NULL,
	[PrevOpen] [decimal](18, 2) NULL,
	[PrevHigh] [decimal](18, 2) NULL,
	[PrevLow] [decimal](18, 2) NULL,
	[PrevClose] [decimal](18, 2) NULL,
	[ShortEMA] [float] NULL,
	[LongEMA] [float] NULL,
	[ShortEMADev] [float] NULL,
	[LongEMADev] [float] NULL,
	[ForceIndex] [float] NULL,
	[CurrHistogramMovement] [varchar](10) NULL,
	[PrevHistogramMovement] [varchar](10) NULL,
	[CurrImpulse] [varchar](10) NULL,
	[PrevImpulse] [varchar](10) NULL,
	[StockTrend] [varchar](10) NULL,
	[PositionType] [varchar](10) NULL,
	[PositionStatus] [varchar](10) NULL,
	[isRealOrderPlaced] [bit] NULL,
	[OrderId] [varchar](50) NULL,
	[SLOrderId] [varchar](50) NULL,
 CONSTRAINT [PK_ElderTrend] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC,
	[TradingSymbol] ASC,
	[TradeDate] ASC,
	[EntryTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Instruments]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Instruments](
	[InstrumentToken] [int] NOT NULL,
	[ExchangeToken] [int] NULL,
	[TradingSymbol] [varchar](50) NULL,
	[Name] [varchar](50) NULL,
	[LastPrice] [decimal](18, 2) NULL,
	[TickSize] [decimal](18, 2) NULL,
	[Expiry] [varchar](50) NULL,
	[InstrumentType] [varchar](50) NULL,
	[Segment] [varchar](50) NULL,
	[Exchange] [varchar](50) NULL,
	[Strike] [decimal](18, 2) NULL,
	[LotSize] [int] NULL,
	[Margin] [decimal](18, 2) NULL,
	[COLower] [decimal](18, 2) NULL,
	[MISMultiplier] [decimal](18, 2) NULL,
	[COUpper] [decimal](18, 2) NULL,
	[NRMLMargin] [decimal](18, 2) NULL,
	[MISMargin] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Instrument] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Margins]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Margins](
	[MarginDate] [datetime] NOT NULL,
	[Enabled] [bit] NULL,
	[Net] [decimal](18, 2) NULL,
	[AdhocMargin] [decimal](18, 2) NULL,
	[Collateral] [decimal](18, 2) NULL,
	[IntradayPayin] [decimal](18, 2) NULL,
	[Cash] [decimal](18, 2) NULL,
	[M2MUnrealised] [decimal](18, 2) NULL,
	[M2MRealised] [decimal](18, 2) NULL,
	[Debits] [decimal](18, 2) NULL,
	[Span] [decimal](18, 2) NULL,
	[OptionPremium] [decimal](18, 2) NULL,
	[HoldingSales] [decimal](18, 2) NULL,
	[Exposure] [decimal](18, 2) NULL,
	[Turnover] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Margin] PRIMARY KEY CLUSTERED 
(
	[MarginDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MasterStockList]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MasterStockList](
	[InstrumentToken] [int] NOT NULL,
	[TradingSymbol] [varchar](15) NOT NULL,
	[Name] [varchar](200) NULL,
	[Collection] [varchar](50) NULL,
	[IsIncluded] [bit] NULL,
 CONSTRAINT [PK_MasterStockList] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MorningBreakouts]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MorningBreakouts](
	[InstrumentToken] [int] NOT NULL,
	[DateTimePeriod] [datetime] NOT NULL,
	[TradingSymbol] [varchar](50) NOT NULL,
	[Open] [decimal](18, 2) NULL,
	[High] [decimal](18, 2) NULL,
	[Low] [decimal](18, 2) NULL,
	[Close] [decimal](18, 2) NULL,
	[LTP] [decimal](18, 2) NULL,
	[CandleSize] [decimal](18, 2) NULL,
	[DayHigh] [decimal](18, 2) NULL,
	[DayLow] [decimal](18, 2) NULL,
	[Quantity] [int] NULL,
	[Entry] [decimal](18, 2) NULL,
	[EntryTime] [datetime] NULL,
	[Exit] [decimal](18, 2) NULL,
	[ExitTime] [datetime] NULL,
	[StopLoss] [decimal](18, 2) NULL,
	[StopLossHitTime] [datetime] NULL,
	[ProfitLoss] [decimal](18, 2) NULL,
	[RAGStatus] [varchar](50) NULL,
	[Movement] [varchar](50) NULL,
	[isRealOrderPlaced] [bit] NULL,
 CONSTRAINT [PK_MorningBreakout] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC,
	[DateTimePeriod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[NetPositions]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[NetPositions](
	[InstrumentToken] [int] NOT NULL,
	[PositionDate] [date] NOT NULL,
	[TradingSymbol] [varchar](50) NOT NULL,
	[AveragePrice] [decimal](18, 2) NULL,
	[BuyM2M] [decimal](18, 2) NULL,
	[BuyPrice] [decimal](18, 2) NULL,
	[BuyQuantity] [int] NULL,
	[BuyValue] [decimal](18, 2) NULL,
	[ClosePrice] [decimal](18, 2) NULL,
	[Exchange] [varchar](10) NULL,
	[LastPrice] [decimal](18, 2) NULL,
	[M2M] [decimal](18, 2) NULL,
	[Multiplier] [decimal](18, 2) NULL,
	[NetBuyAmountM2M] [decimal](18, 2) NULL,
	[NetSellAmountM2M] [decimal](18, 2) NULL,
	[OvernightQuantity] [int] NULL,
	[PNL] [decimal](18, 2) NULL,
	[Product] [varchar](10) NULL,
	[Quantity] [int] NULL,
	[Realised] [decimal](18, 2) NULL,
	[SellM2M] [decimal](18, 2) NULL,
	[SellPrice] [decimal](18, 2) NULL,
	[SellQuantity] [int] NULL,
	[SellValue] [decimal](18, 2) NULL,
	[Unrealised] [decimal](18, 2) NULL,
	[Value] [decimal](18, 2) NULL,
 CONSTRAINT [PK_NetPosition] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC,
	[PositionDate] ASC,
	[TradingSymbol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OHLCData]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OHLCData](
	[InstrumentToken] [int] NOT NULL,
	[OHLCDate] [date] NOT NULL,
	[LastUpdatedTime] [datetime] NOT NULL,
	[TradingSymbol] [varchar](50) NULL,
	[PreviousClose] [decimal](18, 2) NULL,
	[Open] [decimal](18, 2) NULL,
	[High] [decimal](18, 2) NULL,
	[Low] [decimal](18, 2) NULL,
	[LastPrice] [decimal](18, 2) NULL,
 CONSTRAINT [PK_OHLCData] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC,
	[OHLCDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Orders](
	[OrderId] [varchar](50) NOT NULL,
	[ExchangeOrderId] [varchar](50) NOT NULL,
	[ParentOrderId] [varchar](50) NULL,
	[InstrumentToken] [int] NULL,
	[Exchange] [nchar](10) NULL,
	[OrderTimestamp] [datetime] NULL,
	[ExchangeTimestamp] [datetime] NULL,
	[OrderType] [varchar](50) NULL,
	[Tradingsymbol] [varchar](50) NULL,
	[TransactionType] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[CancelledQuantity] [int] NULL,
	[DisclosedQuantity] [int] NULL,
	[FilledQuantity] [int] NULL,
	[PendingQuantity] [int] NULL,
	[Price] [decimal](18, 2) NULL,
	[TriggerPrice] [decimal](18, 2) NULL,
	[AveragePrice] [decimal](18, 2) NULL,
	[Product] [varchar](50) NULL,
	[PlacedBy] [varchar](50) NULL,
	[Validity] [varchar](50) NULL,
	[Variety] [varchar](50) NULL,
	[Tag] [varchar](50) NULL,
	[Status] [varchar](50) NULL,
	[StatusMessage] [varchar](50) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ExchangeOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Quotes]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Quotes](
	[InstrumentToken] [int] NOT NULL,
	[LastUpdatedTime] [datetime] NOT NULL,
	[Volume] [int] NULL,
	[LastQuantity] [int] NULL,
	[Change] [decimal](6, 2) NULL,
	[ChangePercent] [decimal](6, 2) NULL,
	[OpenInterest] [decimal](6, 2) NULL,
	[BuyQuantity] [int] NULL,
	[SellQuantity] [int] NULL,
	[LastPrice] [decimal](18, 2) NULL,
	[Open] [decimal](18, 2) NULL,
	[Close] [decimal](18, 2) NULL,
	[High] [decimal](18, 2) NULL,
	[Low] [decimal](18, 2) NULL,
 CONSTRAINT [PK_Quotes] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC,
	[LastUpdatedTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TickerElderIndicators]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TickerElderIndicators](
	[InstrumentToken] [int] NOT NULL,
	[TickerDateTime] [datetime] NOT NULL,
	[TimePeriod] [int] NOT NULL,
	[TradingSymbol] [varchar](50) NULL,
	[PriceOpen] [float] NULL,
	[PriceHigh] [float] NULL,
	[PriceLow] [float] NULL,
	[PriceClose] [float] NULL,
	[Volume] [int] NULL,
	[IsCalculated] [bit] NULL,
	[EMA1] [float] NULL,
	[EMA2] [float] NULL,
	[EMA3] [float] NULL,
	[EMA4] [float] NULL,
	[MACD] [float] NULL,
	[Signal] [float] NOT NULL,
	[Histogram] [float] NULL,
	[AG1] [float] NULL,
	[AL1] [float] NULL,
	[RSI1] [float] NULL,
	[AG2] [float] NULL,
	[AL2] [float] NULL,
	[RSI2] [float] NULL,
	[ForceIndex] [float] NULL,
	[OBV] [float] NULL,
	[ATR] [float] NULL,
	[Impulse] [varchar](10) NULL,
 CONSTRAINT [PK_TickerElderIndicators] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC,
	[TickerDateTime] ASC,
	[TimePeriod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Trades]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Trades](
	[TradeId] [varchar](50) NOT NULL,
	[OrderId] [varchar](50) NOT NULL,
	[ExchangeOrderId] [varchar](50) NOT NULL,
	[TradingSymbol] [varchar](50) NULL,
	[Exchange] [varchar](50) NULL,
	[InstrumentToken] [int] NOT NULL,
	[TransactionType] [varchar](50) NULL,
	[Product] [varchar](50) NULL,
	[AveragePrice] [decimal](18, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[OrderTimestamp] [datetime] NULL,
	[ExchangeTimestamp] [datetime] NULL,
 CONSTRAINT [PK_Trade] PRIMARY KEY CLUSTERED 
(
	[TradeId] ASC,
	[OrderId] ASC,
	[ExchangeOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserLogins]    Script Date: 08-02-2018 15:56:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserLogins](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ClientId] [varchar](10) NOT NULL,
	[LoginDate] [date] NOT NULL,
	[RequestToken] [varchar](50) NULL,
	[AccessToken] [varchar](50) NULL,
	[PublicToken] [varchar](50) NULL,
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
USE [master]
GO
ALTER DATABASE [TradeGenie] SET  READ_WRITE 
GO
