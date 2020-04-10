USE [TradeGenie]
GO

/****** Object:  UserDefinedTableType [dbo].[TradeType]    Script Date: 04-03-2018 17:04:56 ******/
DROP TYPE [dbo].[TradeType]
GO

/****** Object:  UserDefinedTableType [dbo].[TradeType]    Script Date: 04-03-2018 17:04:56 ******/
CREATE TYPE [dbo].[TradeType] AS TABLE(
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
	[ExchangeTimestamp] [datetime] NULL

)
GO


