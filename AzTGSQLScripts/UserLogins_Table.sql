/****** Object:  Table [dbo].[UserLogins]    Script Date: 5 Sep 2019 17:27:46 ******/
DROP TABLE [dbo].[UserLogins]
GO

/****** Object:  Table [dbo].[UserLogins]    Script Date: 5 Sep 2019 17:27:46 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserLogins](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Broker] [varchar](10) NOT NULL,
	[ClientId] [varchar](10) NOT NULL,
	[LoginDateTime] [datetime] NOT NULL,
	[LogoutDateTime] [datetime] NOT NULL,
	[RequestToken] [varchar](100) NULL,
	[AccessToken] [varchar](100) NULL,
	[PublicToken] [varchar](100) NULL,
	[Status] [varchar](10) NULL
 CONSTRAINT [PK_UserLogin] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


