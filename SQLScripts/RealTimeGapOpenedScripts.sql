/****** Object:  StoredProcedure [dbo].[RealTimeGapOpenedScripts]    Script Date: 17-10-2019 09:41:29 ******/
DROP PROCEDURE [dbo].[RealTimeGapOpenedScripts]
GO

/****** Object:  StoredProcedure [dbo].[RealTimeGapOpenedScripts]    Script Date: 17-10-2019 09:41:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[RealTimeGapOpenedScripts] ( 
	@yesterday DATETIME,
	@today DATETIME,
	@targetPercentage FLOAT,
	@gapPercentage FLOAT,
	@priceRangeHigh INT,
	@priceRangeLow INT
) AS
BEGIN
	
SELECT today.TradingSymbol, 
	msl.[Collection] as 'NiftyIndex', 
	CAST(yesterday.TickerDateTime AS DATE) AS 'Yesterday', 
	yesterday.PriceClose as 'YesterdayClose', 
	yesterday.PriceLow as 'YesterdayHL',
	CAST(today.[DateTime] AS DATE) AS 'Today', 
	today.[Open] as 'TodayOpen', 
	today.[High] as 'TodayHL',
	((today.[Open] - yesterday.PriceClose) / today.[Open]) * 100 AS GapPer,
	'SHORT' as 'OrderType'
FROM TickerMinElderIndicators yesterday 
INNER JOIN TickerMin today ON yesterday.StockCode = today.TradingSymbol 
	AND today.[DateTime] = @today 
	AND yesterday.TickerDateTime = @yesterday
	AND yesterday.TimePeriod = 375
INNER JOIN MasterStockList msl ON msl.TradingSymbol = today.TradingSymbol
WHERE (((today.[Open] - yesterday.PriceClose) / today.[Open]) * 100  > @gapPercentage) 
	AND today.[Close] < @priceRangeHigh 
	AND today.[Close] > @priceRangeLow

UNION

SELECT today.TradingSymbol, 
	msl.[Collection]  as 'NiftyIndex', 
	CAST(yesterday.TickerDateTime AS DATE) AS 'Yesterday', 
	yesterday.PriceClose as 'YesterdayClose', 
	yesterday.PriceHigh as 'YesterdayHL',
	CAST(today.[DateTime] AS DATE) AS 'Today', 
	today.[Open] as 'TodayOpen', 
	today.[Low] as 'TodayHL',
	((today.[Open] - yesterday.PriceClose) / today.[Open]) * 100 AS GapPer,
	'LONG' as 'OrderType'
FROM TickerMinElderIndicators yesterday 
INNER JOIN TickerMin today ON yesterday.StockCode = today.TradingSymbol 
	AND today.[DateTime] = @today 
	AND yesterday.TickerDateTime = @yesterday
	AND yesterday.TimePeriod = 375
INNER JOIN MasterStockList msl ON msl.TradingSymbol = today.TradingSymbol
WHERE (((today.[Open] - yesterday.PriceClose) / today.[Open]) * 100  < -@gapPercentage) 
	AND today.[Close] < @priceRangeHigh 
	AND today.[Close] > @priceRangeLow

END



GO


