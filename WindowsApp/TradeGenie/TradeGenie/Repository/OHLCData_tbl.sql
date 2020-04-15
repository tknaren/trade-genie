USE [TradeGenie]
GO

/****** Object:  Table [dbo].[OHLCData]    Script Date: 27-01-2018 10:46:14 ******/
DROP TABLE [dbo].[OHLCData]
GO

/****** Object:  Table [dbo].[OHLCData]    Script Date: 27-01-2018 10:46:14 ******/
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


