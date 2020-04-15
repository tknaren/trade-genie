USE [TradeGenie]
GO

/****** Object:  Table [dbo].[Orders]    Script Date: 05-02-2018 13:07:51 ******/
DROP TABLE [dbo].[Orders]
GO

/****** Object:  Table [dbo].[Orders]    Script Date: 05-02-2018 13:07:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Orders](
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
	[StatusMessage] [varchar](50) NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC,
	[ExchangeOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


