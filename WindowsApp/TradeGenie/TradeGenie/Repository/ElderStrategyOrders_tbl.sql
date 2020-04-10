USE [TradeGenie]
GO

/****** Object:  Table [dbo].[ElderStrategyOrders]    Script Date: 03-02-2018 11:22:36 ******/
DROP TABLE [dbo].[ElderStrategyOrders]
GO

/****** Object:  Table [dbo].[ElderStrategyOrders]    Script Date: 03-02-2018 11:22:36 ******/
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
	[PositionType] [varchar](10) NULL, -- LONG / SHORT
	[PositionStatus] [varchar](10) NULL, -- PENDING / COMPLETED
	[isRealOrderPlaced] [bit] NULL,
	[OrderId] [varchar](50) NULL,
	[SLOrderId] [varchar](50) NULL
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


