/****** Object:  Table [dbo].[MasterStockList]    Script Date: 17 Oct 2019 12:44:34 ******/
DROP TABLE [dbo].[MasterStockList]
GO

/****** Object:  Table [dbo].[MasterStockList]    Script Date: 17 Oct 2019 12:44:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MasterStockList](
	[InstrumentToken] [int] NOT NULL,
	[TradingSymbol] [varchar](15) NOT NULL,
	[Name] [varchar](200) NULL,
	[Collection] [varchar](50) NULL,
	[Industry] [varchar](100) NULL,
	[IsIncluded] [bit] NULL,
 CONSTRAINT [PK_MasterStockList] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


