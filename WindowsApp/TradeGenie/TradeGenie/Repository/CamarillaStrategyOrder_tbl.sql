USE [TradeGenie]
GO

/****** Object:  Table [dbo].[CamarillaStrategyOrders]    Script Date: 18-06-2018 11:40:37 ******/
DROP TABLE [dbo].[CamarillaStrategyOrders]
GO

/****** Object:  Table [dbo].[CamarillaStrategyOrders]    Script Date: 18-06-2018 11:40:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CamarillaStrategyOrders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[InstrumentToken] [int] NOT NULL,
	[TradingSymbol] [varchar](50) NOT NULL,
	[TradeDate] [date] NOT NULL,
	[Quantity] [int] NULL,
	[EntryPrice] [decimal](18,2) NULL,
	[ExitPrice] [decimal](18,2) NULL,
	[SLPrice] [decimal](18,2) NULL,
	[BOTargetValue] [decimal](18,2) NULL,
	[BOSLValue] [decimal](18,2) NULL,
	[BOTSLValue] [decimal](18,2) NULL,
	[EntryTime] [datetime] NULL,
	[ExitTime] [datetime] NULL,
	[SLHitTime] [datetime] NULL,
	[PositionType] [varchar](50) NULL,
	[OrderVariety] [varchar](50) NULL,
	[isRealOrderPlaced] [bit] NULL,
	[PositionStatus] [varchar](50) NULL,
	[SLOrderStatus] [varchar](50) NULL,
	[TargetOrderStatus] [varchar](50) NULL,
	[OrderId] [int] NULL,
	[SLOrderId] [int] NULL,
	[TargetOrderId] [int] NULL,
	[ProfitLoss] [decimal](18,2) NULL,
	[PLAfterBrok] [decimal](18,2) NULL,
	[Camarilla] [varchar](50) NULL,
	[H1] [decimal](18,2) NULL,
	[H2] [decimal](18,2) NULL,
	[H3] [decimal](18,2) NULL,
	[H4] [decimal](18,2) NULL,
	[H5] [decimal](18,2) NULL,
	[H6] [decimal](18,2) NULL,
	[L1] [decimal](18,2) NULL,
	[L2] [decimal](18,2) NULL,
	[L3] [decimal](18,2) NULL,
	[L4] [decimal](18,2) NULL,
	[L5] [decimal](18,2) NULL,
	[L6] [decimal](18,2) NULL,
	[PDClose] [decimal](18,2) NULL,
	[PDHigh] [decimal](18,2) NULL,
	[PDLow] [decimal](18,2) NULL,
	[CDOpen] [decimal](18,2) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

