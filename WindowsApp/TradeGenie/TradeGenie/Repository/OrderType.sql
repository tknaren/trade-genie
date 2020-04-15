USE [TradeGenie]
GO

/****** Object:  UserDefinedTableType [dbo].[OrderType]    Script Date: 05-02-2018 13:04:12 ******/
DROP TYPE [dbo].[OrderType]
GO

/****** Object:  UserDefinedTableType [dbo].[OrderType]    Script Date: 05-02-2018 13:04:12 ******/
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


