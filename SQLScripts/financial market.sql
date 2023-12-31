USE [master]
GO
/****** Object:  Database [FinancialMarket]    Script Date: 08-02-2018 15:53:13 ******/
CREATE DATABASE [FinancialMarket]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'FinancialMarket', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\FinancialMarket.mdf' , SIZE = 212992KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'FinancialMarket_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\FinancialMarket_log.ldf' , SIZE = 517184KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [FinancialMarket] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [FinancialMarket].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [FinancialMarket] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [FinancialMarket] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [FinancialMarket] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [FinancialMarket] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [FinancialMarket] SET ARITHABORT OFF 
GO
ALTER DATABASE [FinancialMarket] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [FinancialMarket] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [FinancialMarket] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [FinancialMarket] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [FinancialMarket] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [FinancialMarket] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [FinancialMarket] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [FinancialMarket] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [FinancialMarket] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [FinancialMarket] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [FinancialMarket] SET  DISABLE_BROKER 
GO
ALTER DATABASE [FinancialMarket] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [FinancialMarket] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [FinancialMarket] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [FinancialMarket] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [FinancialMarket] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [FinancialMarket] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [FinancialMarket] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [FinancialMarket] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [FinancialMarket] SET  MULTI_USER 
GO
ALTER DATABASE [FinancialMarket] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [FinancialMarket] SET DB_CHAINING OFF 
GO
ALTER DATABASE [FinancialMarket] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [FinancialMarket] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [FinancialMarket]
GO
/****** Object:  User [TradeAdmin]    Script Date: 08-02-2018 15:53:13 ******/
CREATE USER [TradeAdmin] FOR LOGIN [TradeAdmin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [TradeAdmin]
GO
/****** Object:  UserDefinedTableType [dbo].[TickerElderIndicatorType]    Script Date: 08-02-2018 15:53:14 ******/
CREATE TYPE [dbo].[TickerElderIndicatorType] AS TABLE(
	[StockCode] [varchar](15) NOT NULL,
	[TickerDateTime] [datetime] NOT NULL,
	[TimePeriod] [int] NOT NULL,
	[PriceOpen] [float] NULL,
	[PriceHigh] [float] NULL,
	[PriceLow] [float] NULL,
	[PriceClose] [float] NULL,
	[Volume] [int] NULL,
	[EMA1] [float] NULL,
	[EMA2] [float] NULL,
	[EMA3] [float] NULL,
	[EMA4] [float] NULL,
	[MACD] [float] NULL,
	[Signal] [float] NULL,
	[Histogram] [float] NULL,
	[HistIncDec] [varchar](2) NULL,
	[Impulse] [nchar](50) NULL,
	[ForceIndex1] [float] NULL,
	[ForceIndex2] [float] NULL,
	[EMA1Dev] [float] NULL,
	[EMA2Dev] [float] NULL,
	[AG1] [float] NULL,
	[AL1] [float] NULL,
	[RSI1] [float] NULL,
	[AG2] [float] NULL,
	[AL2] [float] NULL,
	[RSI2] [float] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TickerMinType]    Script Date: 08-02-2018 15:53:14 ******/
CREATE TYPE [dbo].[TickerMinType] AS TABLE(
	[InstrumentToken] [int] NOT NULL,
	[TradingSymbol] [varchar](50) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Open] [decimal](10, 2) NULL,
	[High] [decimal](10, 2) NULL,
	[Low] [decimal](10, 2) NULL,
	[Close] [decimal](10, 2) NULL,
	[Volume] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TickerRealTimeFOType]    Script Date: 08-02-2018 15:53:14 ******/
CREATE TYPE [dbo].[TickerRealTimeFOType] AS TABLE(
	[StockCode] [nvarchar](50) NOT NULL,
	[TickerDate] [datetime] NOT NULL,
	[LastUpdatedTime] [datetime] NOT NULL,
	[Open] [float] NULL,
	[High] [float] NULL,
	[Low] [float] NULL,
	[LTP] [float] NULL,
	[Change] [float] NULL,
	[ChangePercentage] [float] NULL,
	[PreviousClose] [float] NULL,
	[DayEndClose] [float] NULL,
	[NetTurnOverInCr] [float] NULL,
	[TradedVolume] [float] NULL,
	[YearlyPercentageChange] [float] NULL,
	[MonthlyPercentageChange] [float] NULL
)
GO
/****** Object:  StoredProcedure [dbo].[spGenerateOHLC]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spGenerateOHLC] ( 
	@strDateTime varchar(20),
	@intMinuteBar int
) AS
BEGIN

DECLARE @strTradingSymbol varchar(20)
DECLARE @bitTickerRunStatus bit

/************************************************************************************************************/

--SELECT @bitTickerRunStatus = TickerStatus FROM ##GlobalTickerStatus(nolock) 
--WHERE TickerProgram = 'CONSOLIDATE' AND TickerDateTime = CONVERT(date, getdate())

--IF (@bitTickerRunStatus IS NULL)
--BEGIN
--    INSERT INTO ##GlobalTickerStatus (TickerDateTime, TickerProgram, TickerStatus)
--    VALUES (CONVERT(date, getdate()), 'CONSOLIDATE', 1)
--END
--ELSE
--BEGIN
--	UPDATE ##GlobalTickerStatus
--	SET TickerStatus = 1
--	WHERE TickerProgram = 'CONSOLIDATE' AND TickerDateTime = CONVERT(date, getdate())
--END

/************************************************************************************************************/

DECLARE Ticker_Cursor CURSOR FOR 
	SELECT TradingSymbol FROM MasterStockList WHERE IsIncluded = 1

OPEN Ticker_Cursor

FETCH NEXT FROM Ticker_Cursor INTO @strTradingSymbol

	WHILE @@FETCH_STATUS = 0

	BEGIN

		BEGIN TRY

			--BEGIN TRANSACTION

			;WITH TickerCTE (StockCode, MinuteBar, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, Interval) 
			AS (
				SELECT 
					TradingSymbol,
					MIN([DateTime]) AS MinuteBar,
					@intMinuteBar AS 'TimePeriod',
					Opening,
					MAX([High]) AS High,
					MIN([Low]) AS Low,
					Closing,
					Sum(Volume) as Volume,
					Interval
				FROM 
				(	SELECT FIRST_VALUE([Open]) OVER (PARTITION BY DATEDIFF(MINUTE, @strDateTime, [DateTime]) / @intMinuteBar ORDER BY [DateTime]) AS Opening,
							FIRST_VALUE([Close]) OVER (PARTITION BY DATEDIFF(MINUTE, @strDateTime, [DateTime]) / @intMinuteBar ORDER BY [DateTime] DESC) AS Closing,
							DATEDIFF(MINUTE, @strDateTime, [DateTime]) / @intMinuteBar AS Interval,
							*
					FROM TickerMin where TradingSymbol = @strTradingSymbol and [DateTime] >= @strDateTime
				) AS T
				GROUP BY TradingSymbol, Interval, Opening, Closing	
			)
			INSERT INTO TickerMinElderIndicators (StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume)
			SELECT StockCode, MinuteBar, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume FROM TickerCTE
			WHERE MinuteBar NOT IN
				(SELECT TickerDateTime FROM TickerMinElderIndicators WHERE StockCode = @strTradingSymbol AND TimePeriod = @intMinuteBar) 


			--COMMIT TRANSACTION

		END TRY

		BEGIN CATCH

			--IF @@Trancount > 0 ROLLBACK TRANSACTION

			INSERT INTO ErrorTable
			SELECT @strTradingSymbol,GETDATE(), ERROR_NUMBER(), ERROR_MESSAGE()

		END CATCH

	FETCH NEXT FROM Ticker_Cursor INTO @strTradingSymbol

	END

CLOSE Ticker_Cursor
DEALLOCATE Ticker_Cursor

/******************************************************************************************/
--UPDATE ##GlobalTickerStatus
--SET TickerStatus = 0
--WHERE TickerProgram = 'CONSOLIDATE' AND TickerDateTime = CONVERT(date, getdate())
/******************************************************************************************/

END;




GO
/****** Object:  StoredProcedure [dbo].[spGetTickerDataForIndicators]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spGetTickerDataForIndicators]
      @InstrumentList varchar(2500),
	  @TimePeriods varchar(50)
AS
BEGIN

	SELECT StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, EMA1, EMA2, EMA3, EMA4, 
		MACD, Signal, Histogram, HistIncDec, Impulse, ForceIndex1, ForceIndex2, EMA1Dev, EMA2Dev, AG1, AL1, RSI1, AG2, AL2, RSI2 
	FROM TickerMinElderIndicators
	WHERE StockCode IN (SELECT * FROM ufn_CSVToTable(@InstrumentList,',')) 
	AND TimePeriod IN (SELECT * FROM ufn_CSVToTable(@TimePeriods,','))
	AND EMA1 IS NULL
	UNION
	SELECT StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, EMA1, EMA2, EMA3, EMA4, 
		MACD, Signal, Histogram, HistIncDec, Impulse, ForceIndex1, ForceIndex2, EMA1Dev, EMA2Dev, AG1, AL1, RSI1, AG2, AL2, RSI2
	FROM (SELECT *,
				ROW_NUMBER() OVER(partition BY StockCode,TimePeriod ORDER BY TickerDateTime DESC) AS RankValue
				FROM TickerMinElderIndicators 
				where StockCode IN (SELECT * FROM ufn_CSVToTable(@InstrumentList,','))  
				AND TimePeriod IN (SELECT * FROM ufn_CSVToTable(@TimePeriods,','))
				AND EMA1 IS NOT NULL
			) i
	WHERE RankValue=1
	ORDER BY StockCode, TimePeriod, TickerDateTime 
		
END

GO
/****** Object:  StoredProcedure [dbo].[spGetTickerElderForTimePeriod]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[spGetTickerElderForTimePeriod]
      @InstrumentList varchar(2500),
	  @TimePeriods varchar(50),
	  @StartTime datetime,
	  @EndTime datetime

AS
BEGIN

	SELECT InstrumentToken, StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, EMA1, EMA2, EMA3, EMA4, 
			MACD, Signal, Histogram, HistIncDec, Impulse, ForceIndex1, ForceIndex2, EMA1Dev, EMA2Dev
	FROM (SELECT tMSL.InstrumentToken, tElder.*,
				ROW_NUMBER() OVER(partition BY StockCode,TimePeriod ORDER BY TickerDateTime DESC) AS RankValue
				FROM TickerMinElderIndicators tElder 
				INNER JOIN MasterStockList tMSL ON tElder.StockCode = tMSL.TradingSymbol
				WHERE StockCode IN (SELECT * FROM ufn_CSVToTable(@InstrumentList,','))  
				AND TimePeriod IN (SELECT * FROM ufn_CSVToTable(@TimePeriods,','))
				AND EMA1 IS NOT NULL AND TickerDateTime BETWEEN @StartTime AND @EndTime
			) i
	WHERE RankValue IN (1,2,3)
	ORDER BY StockCode, TickerDateTime

END




GO
/****** Object:  StoredProcedure [dbo].[spUpdateRealTime]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spUpdateRealTime]
      @tblRealTime TickerRealTimeFOType READONLY
AS
BEGIN
      SET NOCOUNT ON;
 
      MERGE INTO TickerRealTime t1
      USING @tblRealTime t2
      ON t1.StockCode = t2.StockCode AND CONVERT(date, t1.TickerDate) = CONVERT(date, t2.TickerDate)
	  WHEN NOT MATCHED THEN
		INSERT VALUES(t2.StockCode, t2.TickerDate, t2.LastUpdatedTime, t2.[Open], t2.[High], t2.[Low], t2.[LTP], t2.Change,
			t2.ChangePercentage, t2.PreviousClose, t2.DayEndClose, t2.NetTurnOverInCr, t2.TradedVolume, 
			t2.YearlyPercentageChange, t2.MonthlyPercentageChange)
      WHEN MATCHED THEN
		  UPDATE SET 
			t1.LastUpdatedTime = t2.LastUpdatedTime,
			t1.[Open] = t2.[Open],
			t1.High = t2.High,
			t1.Low = t2.Low,
			t1.LTP = t2.LTP,
			t1.Change = t2.Change,
			t1.ChangePercentage = t2.ChangePercentage,
			t1.PreviousClose = t2.PreviousClose,
			t1.DayEndClose = t2.DayEndClose,
			t1.NetTurnOverInCr = t2.NetTurnOverInCr,
			t1.TradedVolume = t2.TradedVolume,
			t1.YearlyPercentageChange = t2.YearlyPercentageChange,
			t1.MonthlyPercentageChange = t2.MonthlyPercentageChange;
END



GO
/****** Object:  StoredProcedure [dbo].[spUpdateRealTimeFO]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spUpdateRealTimeFO]
      @tblRealTimeFO TickerRealTimeFOType READONLY
AS
BEGIN
      SET NOCOUNT ON;
 
      MERGE INTO TickerRealTimeFO t1
      USING @tblRealTimeFO t2
      ON t1.StockCode = t2.StockCode AND t1.TickerDate = t2.TickerDate
	  WHEN NOT MATCHED THEN
		INSERT VALUES(t2.StockCode, t2.TickerDate, t2.LastUpdatedTime, t2.[Open], t2.[High], t2.[Low], t2.[LTP], t2.Change,
			t2.ChangePercentage, t2.PreviousClose, t2.DayEndClose, t2.NetTurnOverInCr, t2.TradedVolume, 
			t2.YearlyPercentageChange, t2.MonthlyPercentageChange)
      WHEN MATCHED THEN
		  UPDATE SET 
			t1.LastUpdatedTime = t2.LastUpdatedTime,
			t1.[Open] = t2.[Open],
			t1.High = t2.High,
			t1.Low = t2.Low,
			t1.LTP = t2.LTP,
			t1.Change = t2.Change,
			t1.ChangePercentage = t2.ChangePercentage,
			t1.PreviousClose = t2.PreviousClose,
			t1.DayEndClose = t2.DayEndClose,
			t1.NetTurnOverInCr = t2.NetTurnOverInCr,
			t1.TradedVolume = t2.TradedVolume,
			t1.YearlyPercentageChange = t2.YearlyPercentageChange,
			t1.MonthlyPercentageChange = t2.MonthlyPercentageChange;
END


GO
/****** Object:  StoredProcedure [dbo].[spUpdateTicker]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spUpdateTicker]
      @tblTicker TickerMinType READONLY
AS
BEGIN
      SET NOCOUNT ON;
 
      MERGE INTO TickerMin t1
      USING @tblTicker t2
      ON t1.InstrumentToken = t2.InstrumentToken and t1.[DateTime] = t2.[DateTime]
      WHEN NOT MATCHED THEN
      INSERT VALUES(t2.InstrumentToken, t2.TradingSymbol, t2.[DateTime], t2.[Open], t2.[High], t2.[Low], t2.[Close], t2.Volume);
END


GO
/****** Object:  StoredProcedure [dbo].[spUpdateTickerElderIndicators]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spUpdateTickerElderIndicators]
      @tblTickerElder TickerElderIndicatorType READONLY
AS
BEGIN
      SET NOCOUNT ON;
 
      MERGE INTO TickerMinElderIndicators t1
      USING @tblTickerElder t2
      ON t1.StockCode = t2.StockCode AND t1.[TickerDateTime] = t2.[TickerDateTime] AND t1.TimePeriod = t2.TimePeriod
      WHEN MATCHED THEN
      --INSERT VALUES(t2.InstrumentToken, t2.TradingSymbol, t2.[DateTime], t2.[Open], t2.[High], t2.[Low], t2.[Close], t2.Volume);
	  UPDATE SET 
		t1.PriceOpen = t2.PriceOpen, 
		t1.PriceHigh = t2.PriceHigh, 
		t1.PriceLow = t2.PriceLow, 
		t1.PriceClose = t2.PriceClose, 
		t1.Volume = t2.Volume, 
		t1.EMA1 = t2.EMA1, 
		t1.EMA2 = t2.EMA2, 
		t1.EMA3 = t2.EMA3, 
		t1.EMA4 = t2.EMA4, 
		t1.MACD = t2.MACD, 
		t1.Signal = t2.Signal, 
		t1.[Histogram] = t2.[Histogram], 
		t1.[HistIncDec] = t2.[HistIncDec], 
		t1.Impulse = t2.Impulse,
		t1.ForceIndex1 = t2.ForceIndex1, 
		t1.ForceIndex2 = t2.ForceIndex2, 
		t1.EMA1Dev = t2.EMA1Dev,
		t1.EMA2Dev = t2.EMA2Dev,
		t1.AG1 = t2.AG1, 
		t1.AL1 = t2.AL1, 
		t1.RSI1 = t2.RSI1, 
		t1.AG2 = t2.AG2, 
		t1.AL2 = t2.AL2, 
		t1.RSI2 = t2.RSI2;

END




GO
/****** Object:  UserDefinedFunction [dbo].[ufn_CSVToTable]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[ufn_CSVToTable] ( @StringInput VARCHAR(8000), @Delimiter nvarchar(1))
RETURNS @OutputTable TABLE ( [String] VARCHAR(10) )
AS
BEGIN

    DECLARE @String  VARCHAR(20)

    WHILE LEN(@StringInput) > 0
    BEGIN
        SET @String = LEFT(@StringInput, 
                                ISNULL(NULLIF(CHARINDEX(@Delimiter, @StringInput) - 1, -1),
                                LEN(@StringInput)))
        SET @StringInput = SUBSTRING(@StringInput,
                                     ISNULL(NULLIF(CHARINDEX(@Delimiter, @StringInput), 0),
                                     LEN(@StringInput)) + 1, LEN(@StringInput))

        INSERT INTO @OutputTable ( [String] )
        VALUES ( @String )
    END

    RETURN
END

GO
/****** Object:  Table [dbo].[BackTesting]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[BackTesting](
	[TransactionId] [int] IDENTITY(1,1) NOT NULL,
	[StockCode] [varchar](15) NULL,
	[BuyDateTime] [datetime] NULL,
	[SellDateTime] [datetime] NULL,
	[BuyType] [varchar](10) NULL,
	[SellType] [varchar](10) NULL,
	[BuyPrice] [float] NULL,
	[SellPrice] [float] NULL,
	[Qty] [int] NULL,
	[BuyAmount] [float] NULL,
	[SellAmount] [float] NULL,
	[ProfitLoss] [float] NULL,
 CONSTRAINT [PK_BackTesting] PRIMARY KEY CLUSTERED 
(
	[TransactionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ElderBackTesting]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ElderBackTesting](
	[InstrumentToken] [int] NOT NULL,
	[TradingSymbol] [varchar](50) NOT NULL,
	[TradeDate] [date] NOT NULL,
	[EntryTime] [datetime] NOT NULL,
	[ExitTime] [datetime] NULL,
	[EntryPrice] [float] NULL,
	[ExitPrice] [float] NULL,
	[OrderType] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[EMA1] [float] NULL,
	[EMA2] [float] NULL,
	[EMA1Dev] [float] NULL,
	[EMA2Dev] [float] NULL,
	[ForceIndex] [float] NULL,
	[Histogram] [float] NULL,
	[HistogramMovement] [varchar](10) NULL,
	[Impulse] [varchar](10) NULL,
 CONSTRAINT [PK_ElderBackTesting] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC,
	[TradingSymbol] ASC,
	[TradeDate] ASC,
	[EntryTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ErrorTable]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ErrorTable](
	[TradingSymbol] [varchar](20) NULL,
	[ErrorOccurredOn] [datetime] NULL,
	[ErrorNumber] [int] NULL,
	[ErrorMessage] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[MasterStockList]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[MasterStockList](
	[InstrumentToken] [int] NOT NULL,
	[TradingSymbol] [varchar](15) NOT NULL,
	[Name] [varchar](200) NULL,
	[Collection] [varchar](50) NULL,
	[IsIncluded] [bit] NULL,
 CONSTRAINT [PK_MasterStockList] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TickerMin]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TickerMin](
	[InstrumentToken] [int] NOT NULL,
	[TradingSymbol] [varchar](15) NOT NULL,
	[DateTime] [datetime] NOT NULL,
	[Open] [decimal](10, 2) NULL,
	[High] [decimal](10, 2) NULL,
	[Low] [decimal](10, 2) NULL,
	[Close] [decimal](10, 2) NULL,
	[Volume] [int] NULL,
 CONSTRAINT [PK_TickerMin] PRIMARY KEY CLUSTERED 
(
	[InstrumentToken] ASC,
	[TradingSymbol] ASC,
	[DateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TickerMinElderIndicators]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TickerMinElderIndicators](
	[StockCode] [varchar](15) NOT NULL,
	[TickerDateTime] [datetime] NOT NULL,
	[TimePeriod] [int] NOT NULL,
	[PriceOpen] [float] NULL,
	[PriceHigh] [float] NULL,
	[PriceLow] [float] NULL,
	[PriceClose] [float] NULL,
	[Volume] [int] NULL,
	[EMA1] [float] NULL,
	[EMA2] [float] NULL,
	[EMA3] [float] NULL,
	[EMA4] [float] NULL,
	[MACD] [float] NULL,
	[Signal] [float] NULL,
	[Histogram] [float] NULL,
	[HistIncDec] [varchar](2) NULL,
	[Impulse] [nchar](50) NULL,
	[ForceIndex1] [float] NULL,
	[ForceIndex2] [float] NULL,
	[EMA1Dev] [float] NULL,
	[EMA2Dev] [float] NULL,
	[AG1] [float] NULL,
	[AL1] [float] NULL,
	[RSI1] [float] NULL,
	[AG2] [float] NULL,
	[AL2] [float] NULL,
	[RSI2] [float] NULL,
 CONSTRAINT [PK_TickerMinElderIndicators] PRIMARY KEY CLUSTERED 
(
	[StockCode] ASC,
	[TickerDateTime] ASC,
	[TimePeriod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TickerMinEMAHA]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TickerMinEMAHA](
	[StockCode] [varchar](15) NOT NULL,
	[PriceDateTime] [datetime] NOT NULL,
	[EMA1] [float] NULL,
	[EMA2] [float] NULL,
	[EMA3] [float] NULL,
	[EMA4] [float] NULL,
	[EMA5] [float] NULL,
	[VWMA1] [float] NULL,
	[VWMA2] [float] NULL,
	[RSI1] [float] NULL,
	[RSI2] [float] NULL,
	[HAOpen] [float] NULL,
	[HAHigh] [float] NULL,
	[HALow] [float] NULL,
	[HAClose] [float] NULL,
	[varEMA1v2] [float] NULL,
	[varEMA1v3] [float] NULL,
	[varEMA1v4] [float] NULL,
	[varEMA2v3] [float] NULL,
	[varEMA2v4] [float] NULL,
	[varEMA3v4] [float] NULL,
	[varEMA4v5] [float] NULL,
	[varVWMA1vVWMA2] [float] NULL,
	[varHAOvHAC] [float] NULL,
	[varHAOvHAPO] [float] NULL,
	[varHACvHAPC] [float] NULL,
	[varOvC] [float] NULL,
	[varOvPO] [float] NULL,
	[varCvPC] [float] NULL,
	[HAOCwEMA1] [char](1) NULL,
	[OCwEMA1] [char](1) NULL,
	[AllEMAsInNum] [int] NULL,
 CONSTRAINT [PK_TickerMinEMAHA] PRIMARY KEY CLUSTERED 
(
	[StockCode] ASC,
	[PriceDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TickerRealTime]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TickerRealTime](
	[StockCode] [nvarchar](50) NOT NULL,
	[TickerDate] [datetime] NOT NULL,
	[LastUpdatedTime] [datetime] NOT NULL,
	[Open] [float] NULL,
	[High] [float] NULL,
	[Low] [float] NULL,
	[LTP] [float] NULL,
	[Change] [float] NULL,
	[ChangePercentage] [float] NULL,
	[PreviousClose] [float] NULL,
	[DayEndClose] [float] NULL,
	[NetTurnOverInCr] [float] NULL,
	[TradedVolume] [float] NULL,
	[YearlyPercentageChange] [float] NULL,
	[MonthlyPercentageChange] [float] NULL,
 CONSTRAINT [PK_TickerRealTime] PRIMARY KEY CLUSTERED 
(
	[StockCode] ASC,
	[TickerDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TickerRealTimeFO]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TickerRealTimeFO](
	[StockCode] [nvarchar](50) NOT NULL,
	[TickerDate] [datetime] NOT NULL,
	[LastUpdatedTime] [datetime] NOT NULL,
	[Open] [float] NULL,
	[High] [float] NULL,
	[Low] [float] NULL,
	[LTP] [float] NULL,
	[Change] [float] NULL,
	[ChangePercentage] [float] NULL,
	[PreviousClose] [float] NULL,
	[DayEndClose] [float] NULL,
	[NetTurnOverInCr] [float] NULL,
	[TradedVolume] [float] NULL,
	[YearlyPercentageChange] [float] NULL,
	[MonthlyPercentageChange] [float] NULL,
 CONSTRAINT [PK_TickerRealTimeFO] PRIMARY KEY CLUSTERED 
(
	[StockCode] ASC,
	[TickerDate] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TickerRealTimeIndex]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TickerRealTimeIndex](
	[IndexName] [nvarchar](50) NOT NULL,
	[LastTickerTime] [datetime] NOT NULL,
	[Open] [float] NULL,
	[High] [float] NULL,
	[Low] [float] NULL,
	[LTP] [float] NULL,
	[Change] [float] NULL,
	[ChgPercentage] [float] NULL,
	[YearlyChange] [float] NULL,
	[MonthlyChange] [float] NULL,
	[YearlyHigh] [float] NULL,
	[YearlyLow] [float] NULL,
	[Advances] [int] NULL,
	[Declines] [int] NULL,
	[Unchanged] [int] NULL,
 CONSTRAINT [PK_TickerRealTimeIndex] PRIMARY KEY CLUSTERED 
(
	[IndexName] ASC,
	[LastTickerTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TickerRealTimeIndicators]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TickerRealTimeIndicators](
	[TimePeriod] [int] NOT NULL,
	[StockCode] [nvarchar](50) NOT NULL,
	[PeriodDateTime] [datetime] NOT NULL,
	[Prie] [float] NULL,
	[ChangePercentage] [float] NULL,
	[TradedVolume] [float] NULL,
	[EMA1] [float] NULL,
	[EMA2] [float] NULL,
	[EMA3] [float] NULL,
	[EMA4] [float] NULL,
	[MACD] [float] NULL,
	[Signal] [float] NULL,
	[Histogram] [float] NULL,
	[RSI1] [float] NULL,
	[RSI2] [float] NULL,
	[ForceIndex] [float] NULL,
	[OBV] [float] NULL,
	[ATR] [float] NULL,
	[Impulse] [nvarchar](50) NULL,
 CONSTRAINT [PK_TickerRealTimeIndicators] PRIMARY KEY CLUSTERED 
(
	[TimePeriod] ASC,
	[StockCode] ASC,
	[PeriodDateTime] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  View [dbo].[GetDistinctFOStocks]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[GetDistinctFOStocks]
AS  
SELECT DISTINCT StockCode FROM  TickerRealTimeFO

GO
/****** Object:  View [dbo].[GetDistinctNiftyStocks]    Script Date: 08-02-2018 15:53:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[GetDistinctNiftyStocks]
AS  
SELECT DISTINCT StockCode FROM  TickerRealTime

GO
USE [master]
GO
ALTER DATABASE [FinancialMarket] SET  READ_WRITE 
GO
