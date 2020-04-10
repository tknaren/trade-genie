USE [TradeGenie]
GO

/****** Object:  UserDefinedTableType [dbo].[ElderStrategyOrderType]   Script Date: 03-02-2018 22:18:04 ******/
DROP TYPE [dbo].[ElderStrategyOrderType]
GO

/****** Object:  UserDefinedTableType [dbo].[ElderStrategyOrderType]    Script Date: 03-02-2018 22:18:04 ******/
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


