USE [FinancialMarket]
GO
/****** Object:  Table [dbo].[TickerRealTimeIndicators]    Script Date: 10-06-2018 21:21:07 ******/
DROP TABLE [dbo].[TickerRealTimeIndicators]
GO
/****** Object:  Table [dbo].[TickerRealTimeIndex]    Script Date: 10-06-2018 21:21:07 ******/
DROP TABLE [dbo].[TickerRealTimeIndex]
GO
/****** Object:  Table [dbo].[TickerRealTimeFO]    Script Date: 10-06-2018 21:21:07 ******/
DROP TABLE [dbo].[TickerRealTimeFO]
GO
/****** Object:  Table [dbo].[TickerRealTime]    Script Date: 10-06-2018 21:21:07 ******/
DROP TABLE [dbo].[TickerRealTime]
GO
/****** Object:  Table [dbo].[TickerMinSuperTrend]    Script Date: 10-06-2018 21:21:07 ******/
DROP TABLE [dbo].[TickerMinSuperTrend]
GO
/****** Object:  Table [dbo].[TickerMinEMAHA]    Script Date: 10-06-2018 21:21:07 ******/
DROP TABLE [dbo].[TickerMinEMAHA]
GO
/****** Object:  Table [dbo].[TickerMinElderIndicators]    Script Date: 10-06-2018 21:21:07 ******/
DROP TABLE [dbo].[TickerMinElderIndicators]
GO
/****** Object:  Table [dbo].[TickerMin]    Script Date: 10-06-2018 21:21:07 ******/
DROP TABLE [dbo].[TickerMin]
GO
/****** Object:  Table [dbo].[MasterStockList]    Script Date: 10-06-2018 21:21:07 ******/
DROP TABLE [dbo].[MasterStockList]
GO
/****** Object:  Table [dbo].[ErrorTable]    Script Date: 10-06-2018 21:21:07 ******/
DROP TABLE [dbo].[ErrorTable]
GO
/****** Object:  Table [dbo].[DailyVolatality]    Script Date: 10-06-2018 21:21:07 ******/
DROP TABLE [dbo].[DailyVolatality]
GO
/****** Object:  UserDefinedFunction [dbo].[ufn_CSVToTable]    Script Date: 10-06-2018 21:21:07 ******/
DROP FUNCTION [dbo].[ufn_CSVToTable]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateTickerElderIndicators]    Script Date: 10-06-2018 21:21:07 ******/
DROP PROCEDURE [dbo].[spUpdateTickerElderIndicators]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateTickerElder]    Script Date: 10-06-2018 21:21:07 ******/
DROP PROCEDURE [dbo].[spUpdateTickerElder]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateTicker]    Script Date: 10-06-2018 21:21:07 ******/
DROP PROCEDURE [dbo].[spUpdateTicker]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateRealTimeFO]    Script Date: 10-06-2018 21:21:07 ******/
DROP PROCEDURE [dbo].[spUpdateRealTimeFO]
GO
/****** Object:  StoredProcedure [dbo].[spUpdateRealTime]    Script Date: 10-06-2018 21:21:07 ******/
DROP PROCEDURE [dbo].[spUpdateRealTime]
GO
/****** Object:  StoredProcedure [dbo].[spGetTickerElderForTimePeriod]    Script Date: 10-06-2018 21:21:07 ******/
DROP PROCEDURE [dbo].[spGetTickerElderForTimePeriod]
GO
/****** Object:  StoredProcedure [dbo].[spGetTickerDataForIndicators]    Script Date: 10-06-2018 21:21:07 ******/
DROP PROCEDURE [dbo].[spGetTickerDataForIndicators]
GO
/****** Object:  StoredProcedure [dbo].[spGenerateOHLC]    Script Date: 10-06-2018 21:21:07 ******/
DROP PROCEDURE [dbo].[spGenerateOHLC]
GO
/****** Object:  UserDefinedTableType [dbo].[TickerRealTimeFOType]    Script Date: 10-06-2018 21:21:07 ******/
DROP TYPE [dbo].[TickerRealTimeFOType]
GO
/****** Object:  UserDefinedTableType [dbo].[TickerMinType]    Script Date: 10-06-2018 21:21:07 ******/
DROP TYPE [dbo].[TickerMinType]
GO
/****** Object:  UserDefinedTableType [dbo].[TickerElderIndicatorType]    Script Date: 10-06-2018 21:21:07 ******/
DROP TYPE [dbo].[TickerElderIndicatorType]
GO
/****** Object:  UserDefinedTableType [dbo].[TickerElderIndicatorType]    Script Date: 10-06-2018 21:21:07 ******/
CREATE TYPE [dbo].[TickerElderIndicatorType] AS TABLE(
	[StockCode] [varchar](15) NOT NULL,
	[TickerDateTime] [datetime] NOT NULL,
	[TimePeriod] [int] NOT NULL,
	[PriceOpen] [float] NULL,
	[PriceHigh] [float] NULL,
	[PriceLow] [float] NULL,
	[PriceClose] [float] NULL,
	[Volume] [int] NULL,
	[Change] [float] NULL,
	[ChangePercent] [float] NULL,
	[TradedValue] [decimal](18, 2) NULL,
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
	[TrueRange] [float] NULL,
	[ATR1] [float] NULL,
	[ATR2] [float] NULL,
	[ATR3] [float] NULL,
	[BUB1] [float] NULL,
	[BUB2] [float] NULL,
	[BUB3] [float] NULL,
	[BLB1] [float] NULL,
	[BLB2] [float] NULL,
	[BLB3] [float] NULL,
	[FUB1] [float] NULL,
	[FUB2] [float] NULL,
	[FUB3] [float] NULL,
	[FLB1] [float] NULL,
	[FLB2] [float] NULL,
	[FLB3] [float] NULL,
	[ST1] [float] NULL,
	[ST2] [float] NULL,
	[ST3] [float] NULL,
	[Trend1] [varchar](15) NULL,
	[Trend2] [varchar](15) NULL,
	[Trend3] [varchar](15) NULL,
	[EHEMA1] [float] NULL,
	[EHEMA2] [float] NULL,
	[EHEMA3] [float] NULL,
	[EHEMA4] [float] NULL,
	[EHEMA5] [float] NULL,
	[VWMA1] [float] NULL,
	[VWMA2] [float] NULL,
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
	[varVWMA1vPriceClose] [float] NULL,
	[varVWMA2vPriceClose] [float] NULL,
	[varVWMA1vEMA1] [float] NULL,
	[varHAOvHAC] [float] NULL,
	[varHAOvHAPO] [float] NULL,
	[varHACvHAPC] [float] NULL,
	[varOvC] [float] NULL,
	[varOvPO] [float] NULL,
	[varCvPC] [float] NULL,
	[HAOCwEMA1] [char](1) NULL,
	[OCwEMA1] [char](1) NULL,
	[AllEMAsInNum] [int] NULL
)
GO
/****** Object:  UserDefinedTableType [dbo].[TickerMinType]    Script Date: 10-06-2018 21:21:08 ******/
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
/****** Object:  UserDefinedTableType [dbo].[TickerRealTimeFOType]    Script Date: 10-06-2018 21:21:08 ******/
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
/****** Object:  StoredProcedure [dbo].[spGenerateOHLC]    Script Date: 10-06-2018 21:21:08 ******/
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

DECLARE Ticker_Cursor CURSOR FOR 
	SELECT TradingSymbol FROM MasterStockList WHERE IsIncluded = 1

CREATE TABLE #TempTickerMinConsolidated
(
	[StockCode] [varchar](15) NOT NULL,
	[TickerDateTime] [datetime] NOT NULL,
	[TimePeriod] [int] NOT NULL,
	[PriceOpen] [float] NULL,
	[PriceHigh] [float] NULL,
	[PriceLow] [float] NULL,
	[PriceClose] [float] NULL,
	[Volume] [int] NULL,
	[PriceIncrease] [float] NULL,
	[PercentageIncrease] [float] NULL,
	[TradedValue] [decimal](18,2) NULL
)

OPEN Ticker_Cursor

FETCH NEXT FROM Ticker_Cursor INTO @strTradingSymbol

	WHILE @@FETCH_STATUS = 0

	BEGIN

		BEGIN TRY

			TRUNCATE TABLE #TempTickerMinConsolidated

			IF (@intMinuteBar != 375)
				BEGIN

					;WITH TickerCTE (StockCode, MinuteBar, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, Interval, PriceIncrease, PercentageIncrease, TradedValue) 
					AS (
						SELECT 
							TradingSymbol,
							MIN([DateTime]) AS MinuteBar,
							@intMinuteBar AS 'TimePeriod',
							Opening,
							MAX([High]) AS High,
							MIN([Low]) AS Low,
							Closing,
							SUM(Volume) as Volume,
							Interval,
							(Closing - Opening) as PriceIncrease,
							ROUND(((Closing - Opening) / Opening) * 100, 2)  as PercentageIncrease,
							ROUND(((Opening + Closing + MAX([High]) +  MIN([Low]))/4) * SUM(Volume),2) AS TradedValue
						FROM 
						(	SELECT FIRST_VALUE([Open]) OVER (PARTITION BY DATEDIFF(MINUTE, @strDateTime, [DateTime]) / @intMinuteBar ORDER BY [DateTime]) AS Opening,
									FIRST_VALUE([Close]) OVER (PARTITION BY DATEDIFF(MINUTE, @strDateTime, [DateTime]) / @intMinuteBar ORDER BY [DateTime] DESC) AS Closing,
									DATEDIFF(MINUTE, @strDateTime, [DateTime]) / @intMinuteBar AS Interval,
									*
							FROM TickerMin where TradingSymbol = @strTradingSymbol and [DateTime] >= @strDateTime
						) AS T
						GROUP BY TradingSymbol, Interval, Opening, Closing	
					)
					INSERT INTO #TempTickerMinConsolidated (StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, PriceIncrease, PercentageIncrease, TradedValue)
					SELECT StockCode, MinuteBar, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, PriceIncrease, PercentageIncrease, TradedValue FROM TickerCTE
					WHERE MinuteBar NOT IN
						(SELECT TickerDateTime FROM TickerMinElderIndicators WHERE StockCode = @strTradingSymbol AND TimePeriod = @intMinuteBar) 

				END	
			ELSE
				BEGIN
			
					;WITH TickerCTE (StockCode, MinuteBar, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, Interval, PriceIncrease, PercentageIncrease, TradedValue) 
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
							Interval,
							(Closing - Opening) as PriceIncrease,
							ROUND(((Closing - Opening) / Opening) * 100, 2)  as PercentageIncrease,
							ROUND(((Opening + Closing + MAX([High]) +  MIN([Low]))/4) * SUM(Volume),2) AS TradedValue
						FROM 
						(	SELECT FIRST_VALUE([Open]) OVER (PARTITION BY DATEDIFF(DAY, @strDateTime, [DateTime]) ORDER BY [DateTime]) AS Opening,
									FIRST_VALUE([Close]) OVER (PARTITION BY DATEDIFF(DAY, @strDateTime, [DateTime]) ORDER BY [DateTime] DESC) AS Closing,
									DATEDIFF(DAY, @strDateTime, [DateTime]) AS Interval,
									*
							FROM TickerMin where TradingSymbol = @strTradingSymbol and [DateTime] >= @strDateTime
						) AS T
						GROUP BY TradingSymbol, Interval, Opening, Closing	
					)
					INSERT INTO #TempTickerMinConsolidated (StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, PriceIncrease, PercentageIncrease, TradedValue)
					SELECT StockCode, MinuteBar, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, PriceIncrease, PercentageIncrease, TradedValue FROM TickerCTE
					WHERE MinuteBar NOT IN
						(SELECT TickerDateTime FROM TickerMinElderIndicators WHERE StockCode = @strTradingSymbol AND TimePeriod = @intMinuteBar) 
				END


			INSERT INTO TickerMinElderIndicators (StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, Change, ChangePercent, TradedValue)
			SELECT StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, PriceIncrease, PercentageIncrease, TradedValue FROM #TempTickerMinConsolidated

			INSERT INTO TickerMinSuperTrend (StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume)
			SELECT StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume FROM #TempTickerMinConsolidated

			INSERT INTO TickerMinEMAHA (StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume)
			SELECT StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume FROM #TempTickerMinConsolidated


		END TRY

		BEGIN CATCH

			INSERT INTO ErrorTable
			SELECT @strTradingSymbol,GETDATE(), ERROR_NUMBER(), ERROR_MESSAGE()

		END CATCH

	FETCH NEXT FROM Ticker_Cursor INTO @strTradingSymbol

	END

CLOSE Ticker_Cursor
DEALLOCATE Ticker_Cursor

END;


GO
/****** Object:  StoredProcedure [dbo].[spGetTickerDataForIndicators]    Script Date: 10-06-2018 21:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[spGetTickerDataForIndicators]
      @InstrumentList varchar(2500),
	  @TimePeriods varchar(50)
AS
BEGIN

	SELECT TMEI.StockCode, TMEI.TickerDateTime, TMEI.TimePeriod, TMEI.PriceOpen, TMEI.PriceHigh, TMEI.PriceLow, TMEI.PriceClose, TMEI.Volume,
		TMEI.Change, TMEI.ChangePercent, TMEI.TradedValue,
		TMEI.EMA1, TMEI.EMA2, TMEI.EMA3, TMEI.EMA4, 
		TMEI.MACD, TMEI.Signal, TMEI.Histogram, TMEI.HistIncDec, TMEI.Impulse, TMEI.ForceIndex1, TMEI.ForceIndex2, 
		TMEI.EMA1Dev, TMEI.EMA2Dev, TMEI.AG1, TMEI.AL1, TMEI.RSI1, TMEI.AG2, TMEI.AL2, TMEI.RSI2,
		TMST.TrueRange, TMST.ATR1, TMST.ATR2, TMST.ATR3, TMST.BUB1, TMST.BUB2, TMST.BUB3, TMST.BLB1, TMST.BLB2, TMST.BLB3,
		TMST.FUB1, TMST.FUB2, TMST.FUB3, TMST.FLB1, TMST.FLB2, TMST.FLB3, TMST.ST1, TMST.ST2, TMST.ST3, TMST.Trend1, TMST.Trend2, TMST.Trend3,
		TMEH.EHEMA1, TMEH.EHEMA2, TMEH.EHEMA3, TMEH.EHEMA4, TMEH.EHEMA5, TMEH.VWMA1, TMEH.VWMA2, 
		TMEH.HAOpen, TMEH.HAHigh, TMEH.HALow, TMEH.HAClose, TMEH.varEMA1v2, TMEH.varEMA1v3, TMEH.varEMA1v4, 
		TMEH.varEMA2v3, TMEH.varEMA2v4, TMEH.varEMA3v4, TMEH.varEMA4v5, TMEH.varVWMA1vVWMA2, TMEH.varVWMA1vPriceClose, 
		TMEH.varVWMA2vPriceClose, TMEH.varVWMA1vEMA1, TMEH.varHAOvHAC, TMEH.varHAOvHAPO, TMEH.varHACvHAPC, 
		TMEH.varOvC, TMEH.varOvPO, TMEH.varCvPC, TMEH.HAOCwEMA1, TMEH.OCwEMA1, TMEH.AllEMAsInNum
	FROM TickerMinElderIndicators TMEI 
		INNER JOIN TickerMinSuperTrend TMST 
		ON TMEI.StockCode = TMST.StockCode AND TMEI.TimePeriod = TMST.TimePeriod AND TMEI.TickerDateTime = TMST.TickerDateTime
		INNER JOIN TickerMinEMAHA TMEH 
		ON TMEI.StockCode = TMEH.StockCode AND TMEI.TimePeriod = TMEH.TimePeriod AND TMEI.TickerDateTime = TMEH.TickerDateTime
	WHERE TMEI.StockCode IN (SELECT * FROM ufn_CSVToTable(@InstrumentList,',')) 
	AND TMEI.TimePeriod IN (SELECT * FROM ufn_CSVToTable(@TimePeriods,','))
	AND (TMEI.EMA1 IS NULL OR ST1 IS NULL OR TMEH.EHEMA1 IS NULL) 
	UNION
	SELECT StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, 
		Change, ChangePercent, TradedValue, EMA1, EMA2, EMA3, EMA4, 
		MACD, Signal, Histogram, HistIncDec, Impulse, ForceIndex1, ForceIndex2, EMA1Dev, EMA2Dev, AG1, AL1, RSI1, AG2, AL2, RSI2,
		TrueRange, ATR1, ATR2, ATR3, BUB1, BUB2, BUB3, BLB1, BLB2, BLB3,
		FUB1, FUB2, FUB3, FLB1, FLB2, FLB3, ST1, ST2, ST3, Trend1, Trend2, Trend3,
		EHEMA1, EHEMA2, EHEMA3, EHEMA4, EHEMA5, VWMA1, VWMA2, HAOpen, HAHigh, HALow, HAClose, 
		varEMA1v2, varEMA1v3, varEMA1v4, varEMA2v3, varEMA2v4, varEMA3v4, varEMA4v5, 
		varVWMA1vVWMA2, varVWMA1vPriceClose, varVWMA2vPriceClose, varVWMA1vEMA1, varHAOvHAC, varHAOvHAPO, varHACvHAPC, 
		varOvC, varOvPO, varCvPC, HAOCwEMA1, OCwEMA1, AllEMAsInNum
	FROM (SELECT TMEI.StockCode, TMEI.TickerDateTime, TMEI.TimePeriod, TMEI.PriceOpen, TMEI.PriceHigh, TMEI.PriceLow, TMEI.PriceClose, TMEI.Volume, 
				TMEI.Change, TMEI.ChangePercent, TMEI.TradedValue, TMEI.EMA1, TMEI.EMA2, TMEI.EMA3, TMEI.EMA4, 
				TMEI.MACD, TMEI.Signal, TMEI.Histogram, TMEI.HistIncDec, TMEI.Impulse, TMEI.ForceIndex1, TMEI.ForceIndex2, 
				TMEI.EMA1Dev, TMEI.EMA2Dev, TMEI.AG1, TMEI.AL1, TMEI.RSI1, TMEI.AG2, TMEI.AL2, TMEI.RSI2,
				TMST.TrueRange, TMST.ATR1, TMST.ATR2, TMST.ATR3, TMST.BUB1, TMST.BUB2, TMST.BUB3, TMST.BLB1, TMST.BLB2, TMST.BLB3,
				TMST.FUB1, TMST.FUB2, TMST.FUB3, TMST.FLB1, TMST.FLB2, TMST.FLB3, TMST.ST1, TMST.ST2, TMST.ST3, TMST.Trend1, TMST.Trend2, TMST.Trend3,
				TMEH.EHEMA1, TMEH.EHEMA2, TMEH.EHEMA3, TMEH.EHEMA4, TMEH.EHEMA5, TMEH.VWMA1, TMEH.VWMA2, 
				TMEH.HAOpen, TMEH.HAHigh, TMEH.HALow, TMEH.HAClose, TMEH.varEMA1v2, TMEH.varEMA1v3, TMEH.varEMA1v4, 
				TMEH.varEMA2v3, TMEH.varEMA2v4, TMEH.varEMA3v4, TMEH.varEMA4v5, TMEH.varVWMA1vVWMA2, TMEH.varVWMA1vPriceClose, 
				TMEH.varVWMA2vPriceClose, TMEH.varVWMA1vEMA1, TMEH.varHAOvHAC, TMEH.varHAOvHAPO, TMEH.varHACvHAPC, 
				TMEH.varOvC, TMEH.varOvPO, TMEH.varCvPC, TMEH.HAOCwEMA1, TMEH.OCwEMA1, TMEH.AllEMAsInNum,
				ROW_NUMBER() OVER(partition BY TMEI.StockCode,TMEI.TimePeriod ORDER BY TMEI.TickerDateTime DESC) AS RankValue
				FROM TickerMinElderIndicators TMEI INNER JOIN TickerMinSuperTrend TMST 
					ON TMEI.StockCode = TMST.StockCode AND TMEI.TimePeriod = TMST.TimePeriod AND TMEI.TickerDateTime = TMST.TickerDateTime
				INNER JOIN TickerMinEMAHA TMEH 
					ON TMEI.StockCode = TMEH.StockCode AND TMEI.TimePeriod = TMEH.TimePeriod AND TMEI.TickerDateTime = TMEH.TickerDateTime
				where TMEI.StockCode IN (SELECT * FROM ufn_CSVToTable(@InstrumentList,','))  
				AND TMEI.TimePeriod IN (SELECT * FROM ufn_CSVToTable(@TimePeriods,','))
				AND EMA1 IS NOT NULL
			) i
	WHERE RankValue=1
	ORDER BY StockCode, TimePeriod, TickerDateTime 
		
END


GO
/****** Object:  StoredProcedure [dbo].[spGetTickerElderForTimePeriod]    Script Date: 10-06-2018 21:21:08 ******/
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
	WHERE RankValue IN (1,2)
	ORDER BY StockCode, TickerDateTime

END





GO
/****** Object:  StoredProcedure [dbo].[spUpdateRealTime]    Script Date: 10-06-2018 21:21:08 ******/
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
/****** Object:  StoredProcedure [dbo].[spUpdateRealTimeFO]    Script Date: 10-06-2018 21:21:08 ******/
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
/****** Object:  StoredProcedure [dbo].[spUpdateTicker]    Script Date: 10-06-2018 21:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spUpdateTicker]
      @tblTicker TickerMinType READONLY
AS
BEGIN
      SET NOCOUNT ON;
 
      MERGE INTO TickerMin with (TABLOCK) t1
      USING @tblTicker t2
      ON t1.InstrumentToken = t2.InstrumentToken and t1.[DateTime] = t2.[DateTime]
      WHEN NOT MATCHED THEN
      INSERT VALUES(t2.InstrumentToken, t2.TradingSymbol, t2.[DateTime], t2.[Open], t2.[High], t2.[Low], t2.[Close], t2.Volume);
END



GO
/****** Object:  StoredProcedure [dbo].[spUpdateTickerElder]    Script Date: 10-06-2018 21:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spUpdateTickerElder]
      @tblTicker TickerMinType READONLY,
	  @timePeriod int
AS
BEGIN
      SET NOCOUNT ON;
 
      MERGE INTO TickerMinElderIndicators t1
      USING @tblTicker t2
      ON t1.StockCode = t2.TradingSymbol and t1.TickerDateTime = t2.[DateTime] and TimePeriod = @timePeriod
      WHEN NOT MATCHED THEN
      INSERT (StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, Change, ChangePercent, TradedValue) 
	  VALUES(t2.TradingSymbol, t2.[DateTime], @timePeriod, t2.[Open], t2.[High], t2.[Low], t2.[Close], t2.Volume,
				(t2.[Close] - t2.[Open]),
				ROUND(((t2.[Close] - t2.[Open]) / t2.[Open]) * 100, 2),
				ROUND(((t2.[Open] + t2.[Close] + t2.[High] +  t2.[Low])/4) * t2.Volume,2)	  
	  
	  );

      MERGE INTO TickerMinSuperTrend t1
      USING @tblTicker t2
      ON t1.StockCode = t2.TradingSymbol and t1.TickerDateTime = t2.[DateTime]
      WHEN NOT MATCHED THEN
      INSERT (StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume) 
	  VALUES(t2.TradingSymbol, t2.[DateTime], @timePeriod, t2.[Open], t2.[High], t2.[Low], t2.[Close], t2.Volume);

      MERGE INTO TickerMinEMAHA t1
      USING @tblTicker t2
      ON t1.StockCode = t2.TradingSymbol and t1.TickerDateTime = t2.[DateTime]
      WHEN NOT MATCHED THEN
      INSERT (StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume) 
	  VALUES(t2.TradingSymbol, t2.[DateTime], @timePeriod, t2.[Open], t2.[High], t2.[Low], t2.[Close], t2.Volume);
END


GO
/****** Object:  StoredProcedure [dbo].[spUpdateTickerElderIndicators]    Script Date: 10-06-2018 21:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE PROCEDURE [dbo].[spUpdateTickerElderIndicators]
      @tblTickerElder TickerElderIndicatorType READONLY
AS
BEGIN
      SET NOCOUNT ON;
 
      MERGE INTO TickerMinElderIndicators with (TABLOCK) t1
      USING @tblTickerElder t2
      ON t1.StockCode = t2.StockCode AND t1.[TickerDateTime] = t2.[TickerDateTime] AND t1.TimePeriod = t2.TimePeriod
      WHEN MATCHED THEN
	  UPDATE SET 
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

      MERGE INTO TickerMinSuperTrend with (TABLOCK) t1
      USING @tblTickerElder t2
      ON t1.StockCode = t2.StockCode AND t1.[TickerDateTime] = t2.[TickerDateTime] AND t1.TimePeriod = t2.TimePeriod
      WHEN MATCHED THEN
	  UPDATE SET 
		t1.TrueRange = t2.TrueRange, 
		t1.ATR1 = t2.ATR1, 
		t1.ATR2 = t2.ATR2, 
		t1.ATR3 = t2.ATR3, 
		t1.BUB1 = t2.BUB1, 
		t1.BUB2 = t2.BUB2, 
		t1.BUB3 = t2.BUB3, 
		t1.BLB1 = t2.BLB1, 
		t1.BLB2 = t2.BLB2,
		t1.BLB3 = t2.BLB3, 
		t1.FUB1 = t2.FUB1, 
		t1.FUB2 = t2.FUB2,
		t1.FUB3 = t2.FUB3,
		t1.FLB1 = t2.FLB1, 
		t1.FLB2 = t2.FLB2, 
		t1.FLB3 = t2.FLB3, 
		t1.ST1 = t2.ST1, 
		t1.ST2 = t2.ST2, 
		t1.ST3 = t2.ST3,
		t1.Trend1 = t2.Trend1, 
		t1.Trend2 = t2.Trend2, 
		t1.Trend3 = t2.Trend3;

      MERGE INTO TickerMinEMAHA with (TABLOCK) t1
      USING @tblTickerElder t2
      ON t1.StockCode = t2.StockCode AND t1.[TickerDateTime] = t2.[TickerDateTime] AND t1.TimePeriod = t2.TimePeriod
      WHEN MATCHED THEN
	  UPDATE SET 
		t1.EHEMA1 = t2.EHEMA1,
		t1.EHEMA2 = t2.EHEMA2,
		t1.EHEMA3 = t2.EHEMA3,
		t1.EHEMA4 = t2.EHEMA4,
		t1.EHEMA5 = t2.EHEMA5,
		t1.VWMA1 = t2.VWMA1,
		t1.VWMA2 = t2.VWMA2,
		t1.HAOpen = t2.HAOpen,
		t1.HAHigh = t2.HAHigh,
		t1.HALow = t2.HALow,
		t1.HAClose = t2.HAClose,
		t1.varEMA1v2 = t2.varEMA1v2,
		t1.varEMA1v3 = t2.varEMA1v3,
		t1.varEMA1v4 = t2.varEMA1v4,
		t1.varEMA2v3 = t2.varEMA2v3,
		t1.varEMA2v4 = t2.varEMA2v4,
		t1.varEMA3v4 = t2.varEMA3v4,
		t1.varEMA4v5 = t2.varEMA4v5,
		t1.varVWMA1vVWMA2 = t2.varVWMA1vVWMA2,
		t1.varVWMA1vPriceClose = t2.varVWMA1vPriceClose,
		t1.varVWMA2vPriceClose = t2.varVWMA2vPriceClose,
		t1.varVWMA1vEMA1 = t2.varVWMA1vEMA1,
		t1.varHAOvHAC = t2.varHAOvHAC,
		t1.varHAOvHAPO = t2.varHAOvHAPO,
		t1.varHACvHAPC = t2.varHACvHAPC,
		t1.varOvC = t2.varOvC,
		t1.varOvPO = t2.varOvPO,
		t1.varCvPC = t2.varCvPC,
		t1.HAOCwEMA1 = t2.HAOCwEMA1,
		t1.OCwEMA1 = t2.OCwEMA1,
		t1.AllEMAsInNum = t2.AllEMAsInNum;

END



GO
/****** Object:  UserDefinedFunction [dbo].[ufn_CSVToTable]    Script Date: 10-06-2018 21:21:08 ******/
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
/****** Object:  Table [dbo].[DailyVolatality]    Script Date: 10-06-2018 21:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[DailyVolatality](
	[RunDate] [date] NOT NULL,
	[Symbol] [varchar](50) NOT NULL,
	[ClosePrice] [float] NULL,
	[PrevClosePrice] [float] NULL,
	[LogReturns] [float] NULL,
	[PrevVolatality] [float] NULL,
	[CurrVolatality] [float] NULL,
	[AnnualVolatality] [float] NULL,
 CONSTRAINT [PK_DailyVolatality] PRIMARY KEY CLUSTERED 
(
	[RunDate] ASC,
	[Symbol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ErrorTable]    Script Date: 10-06-2018 21:21:08 ******/
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
/****** Object:  Table [dbo].[MasterStockList]    Script Date: 10-06-2018 21:21:08 ******/
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
/****** Object:  Table [dbo].[TickerMin]    Script Date: 10-06-2018 21:21:08 ******/
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
/****** Object:  Table [dbo].[TickerMinElderIndicators]    Script Date: 10-06-2018 21:21:08 ******/
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
	[Change] [float] NULL,
	[ChangePercent] [float] NULL,
	[TradedValue] [decimal](18, 2) NULL,
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
/****** Object:  Table [dbo].[TickerMinEMAHA]    Script Date: 10-06-2018 21:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING OFF
GO
CREATE TABLE [dbo].[TickerMinEMAHA](
	[StockCode] [varchar](15) NOT NULL,
	[TickerDateTime] [datetime] NOT NULL,
	[TimePeriod] [int] NOT NULL,
	[PriceOpen] [float] NULL,
	[PriceHigh] [float] NULL,
	[PriceLow] [float] NULL,
	[PriceClose] [float] NULL,
	[Volume] [int] NULL,
	[EHEMA1] [float] NULL,
	[EHEMA2] [float] NULL,
	[EHEMA3] [float] NULL,
	[EHEMA4] [float] NULL,
	[EHEMA5] [float] NULL,
	[VWMA1] [float] NULL,
	[VWMA2] [float] NULL,
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
	[varVWMA1vPriceClose] [float] NULL,
	[varVWMA2vPriceClose] [float] NULL,
	[varVWMA1vEMA1] [float] NULL,
	[varHAOvHAC] [float] NULL,
	[varHAOvHAPO] [float] NULL,
	[varHACvHAPC] [float] NULL,
	[varOvC] [float] NULL,
	[varOvPO] [float] NULL,
	[varCvPC] [float] NULL,
	[HAOCwEMA1] [char](1) NULL,
	[OCwEMA1] [char](1) NULL,
	[EMA2v34] [char](2) NULL,
	[AllEMAsInNum] [int] NULL,
 CONSTRAINT [PK_TickerMinEMAHA] PRIMARY KEY CLUSTERED 
(
	[StockCode] ASC,
	[TickerDateTime] ASC,
	[TimePeriod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TickerMinSuperTrend]    Script Date: 10-06-2018 21:21:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[TickerMinSuperTrend](
	[StockCode] [varchar](15) NOT NULL,
	[TickerDateTime] [datetime] NOT NULL,
	[TimePeriod] [int] NOT NULL,
	[PriceOpen] [float] NULL,
	[PriceHigh] [float] NULL,
	[PriceLow] [float] NULL,
	[PriceClose] [float] NULL,
	[Volume] [int] NULL,
	[TrueRange] [float] NULL,
	[ATR1] [float] NULL,
	[ATR2] [float] NULL,
	[ATR3] [float] NULL,
	[BUB1] [float] NULL,
	[BUB2] [float] NULL,
	[BUB3] [float] NULL,
	[BLB1] [float] NULL,
	[BLB2] [float] NULL,
	[BLB3] [float] NULL,
	[FUB1] [float] NULL,
	[FUB2] [float] NULL,
	[FUB3] [float] NULL,
	[FLB1] [float] NULL,
	[FLB2] [float] NULL,
	[FLB3] [float] NULL,
	[ST1] [float] NULL,
	[ST2] [float] NULL,
	[ST3] [float] NULL,
	[Trend1] [varchar](15) NULL,
	[Trend2] [varchar](15) NULL,
	[Trend3] [varchar](15) NULL,
 CONSTRAINT [PK_TickerMinSuperTrend] PRIMARY KEY CLUSTERED 
(
	[StockCode] ASC,
	[TickerDateTime] ASC,
	[TimePeriod] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TickerRealTime]    Script Date: 10-06-2018 21:21:08 ******/
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
/****** Object:  Table [dbo].[TickerRealTimeFO]    Script Date: 10-06-2018 21:21:08 ******/
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
/****** Object:  Table [dbo].[TickerRealTimeIndex]    Script Date: 10-06-2018 21:21:08 ******/
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
/****** Object:  Table [dbo].[TickerRealTimeIndicators]    Script Date: 10-06-2018 21:21:08 ******/
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
