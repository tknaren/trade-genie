USE [TradeGenie]
GO

/****** Object:  UserDefinedTableType [dbo].[NetPositionType]    Script Date: 05-03-2018 11:53:51 ******/
DROP TYPE [dbo].[NetPositionType]
GO

/****** Object:  UserDefinedTableType [dbo].[NetPositionType]    Script Date: 05-03-2018 11:53:51 ******/
CREATE TYPE [dbo].[NetPositionType] AS TABLE(
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
	[Product] [varchar](10) NOT NULL,
	[Quantity] [int] NULL,
	[Realised] [decimal](18, 2) NULL,
	[SellM2M] [decimal](18, 2) NULL,
	[SellPrice] [decimal](18, 2) NULL,
	[SellQuantity] [int] NULL,
	[SellValue] [decimal](18, 2) NULL,
	[Unrealised] [decimal](18, 2) NULL,
	[Value] [decimal](18, 2) NULL
)
GO


