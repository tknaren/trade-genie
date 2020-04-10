USE [TradeGenie]
GO

/****** Object:  UserDefinedTableType [dbo].[MarginType]    Script Date: 04-03-2018 17:38:50 ******/
DROP TYPE [dbo].[MarginType]
GO

/****** Object:  UserDefinedTableType [dbo].[MarginType]    Script Date: 04-03-2018 17:38:50 ******/
CREATE TYPE [dbo].[MarginType] AS TABLE(
	[MarginDate] [date] NOT NULL,
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
	[Turnover] [decimal](18, 2) NULL
)
GO


