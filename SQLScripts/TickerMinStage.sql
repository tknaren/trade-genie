/****** Object:  Table [dbo].[TickerMinStage]    Script Date: 9 Oct 2019 18:30:09 ******/
DROP TABLE [dbo].[TickerMinStage]
GO

/****** Object:  Table [dbo].[TickerMinStage]    Script Date: 9 Oct 2019 18:30:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[TickerMinStage](
	[InstrumentToken] [int] NOT NULL,
	[TradingSymbol] [varchar](15) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Open] [decimal](10, 2) NULL,
	[High] [decimal](10, 2) NULL,
	[Low] [decimal](10, 2) NULL,
	[Close] [decimal](10, 2) NULL,
	[Volume] [int] NULL,
) 

GO


