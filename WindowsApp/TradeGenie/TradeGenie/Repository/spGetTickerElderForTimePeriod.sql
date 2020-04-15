USE [FinancialMarket]
GO

/****** Object:  StoredProcedure [dbo].[spGetTickerElderForTimePeriod]    Script Date: 26-01-2018 15:51:53 ******/
DROP PROCEDURE [dbo].[spGetTickerElderForTimePeriod]
GO

/****** Object:  StoredProcedure [dbo].[spGetTickerElderForTimePeriod]    Script Date: 26-01-2018 15:51:53 ******/
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

	SELECT StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, EMA1, EMA2, EMA3, EMA4, 
			MACD, Signal, Histogram, HistIncDec, Impulse, ForceIndex1, ForceIndex2, EMA1Dev, EMA2Dev
	FROM (SELECT *,
				ROW_NUMBER() OVER(partition BY StockCode,TimePeriod ORDER BY TickerDateTime DESC) AS RankValue
				FROM TickerMinElderIndicators 
				where StockCode IN (SELECT * FROM ufn_CSVToTable(@InstrumentList,','))  
				AND TimePeriod IN (SELECT * FROM ufn_CSVToTable(@TimePeriods,','))
				AND EMA1 IS NOT NULL AND TickerDateTime BETWEEN @StartTime AND @EndTime
			) i
	WHERE RankValue IN (1,2,3)
	ORDER BY StockCode, TickerDateTime

END

GO
