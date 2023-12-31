USE [TradeGenie]
GO
/****** Object:  Table [dbo].[Instrument]    Script Date: 15-12-2017 13:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Instrument](
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
 CONSTRAINT [PK_Instrument] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MasterStockList]    Script Date: 15-12-2017 13:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MasterStockList](
	[Code] [varchar](15) NOT NULL,
	[Name] [varchar](200) NULL,
	[Collection] [varchar](50) NULL,
	[LastDownloaded] [datetime] NULL,
	[LastCalculated] [datetime] NULL,
	[IsIncluded] [bit] NULL,
 CONSTRAINT [PK_MasterStockList] PRIMARY KEY CLUSTERED 
(
	[Code] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MorningBreakout]    Script Date: 15-12-2017 13:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MorningBreakout](
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
 CONSTRAINT [PK_MorningBreakout] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC,
	[DateTimePeriod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[OHLCData]    Script Date: 15-12-2017 13:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[OHLCData](
	[InstrumentToken] [int] NOT NULL,
	[OHLCDateTime] [datetime] NOT NULL,
	[TradingSymbol] [varchar](50) NULL,
	[PreviousClose] [decimal](18, 2) NULL,
	[Open] [decimal](18, 2) NULL,
	[High] [decimal](18, 2) NULL,
	[Low] [decimal](18, 2) NULL,
	[LastPrice] [decimal](18, 2) NULL,
 CONSTRAINT [PK_OHLCData] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC,
	[OHLCDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Order]    Script Date: 15-12-2017 13:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Order](
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
	[MarketProtection] [int] NULL,
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
/****** Object:  Table [dbo].[Quote]    Script Date: 29-12-2017 14:41:45 ******/
DROP TABLE [dbo].[Quote]
GO

/****** Object:  Table [dbo].[Quote]    Script Date: 29-12-2017 14:41:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Quote](
	[InstrumentToken] [int] NOT NULL,
	[QuoteTime] [datetime] NOT NULL,
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
	[InstrumentToken] ASC, [QuoteTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TickerElderIndicators]    Script Date: 15-12-2017 13:20:09 ******/
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

/****** Object:  Table [dbo].[Trade]    Script Date: 18-12-2017 13:04:14 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Trade](
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
/****** Object:  Table [dbo].[UserLogin]    Script Date: 15-12-2017 13:20:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[UserLogin](
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

/****** Object:  Table [dbo].[NetPosition]    Script Date: 12/18/2017 8:31:04 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NetPosition](
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

/****** Object:  Table [dbo].[Margin]    Script Date: 18-12-2017 13:43:15 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Margin](
	[MarginDate] [datetime] NOT NULL,
	[Enabled] [varchar](50) NULL,
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

SET ANSI_PADDING OFF
GO

/***************************/
ALTER TABLE Instrument
  ADD	Margin decimal(18,2) NULL,
		COLower decimal(18,2) NULL,
		MISMultiplier decimal(18,2) NULL,
		COUpper decimal(18,2) NULL,
		NRMLMargin decimal(18,2) NULL,
		MISMargin decimal(18,2) NULL
GO
/***************************/
