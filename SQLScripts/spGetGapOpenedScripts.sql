/****** Object:  StoredProcedure [dbo].[spGetGapOpenedScripts]    Script Date: 29-09-2019 19:15:12 ******/
DROP PROCEDURE [dbo].[spGetGapOpenedScripts]
GO

/****** Object:  StoredProcedure [dbo].[spGetGapOpenedScripts]    Script Date: 29-09-2019 19:15:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spGetGapOpenedScripts] ( 
	@yesterday datetime,
	@today datetime,
	@targetPercentage int,
	@gapPercentage int,
	@priceRangeHigh int,
	@priceRangeLow int
) AS
BEGIN
	
select today.StockCode, msl.[Collection], cast(yesterday.TickerDateTime as Date) as 'Yesterday', yesterday.PriceClose, 
cast(today.TickerDateTime as date) as 'Today', today.PriceOpen, ((today.PriceOpen - yesterday.PriceClose) / today.PriceOpen) * 100 as GapPer,
(today.PriceOpen - (today.PriceOpen * 0.01)) as 'Target', today.PriceLow as 'Final', 
CASE WHEN today.PriceLow < (today.PriceOpen - (today.PriceOpen * (@targetPercentage / 100))) THEN 'TRUE' ELSE 'FALSE' END as 'IsProfit'
from TickerMinElderIndicators yesterday 
inner join TickerMinElderIndicators today on yesterday.StockCode = today.StockCode and today.TickerDateTime = @today and yesterday.TickerDateTime = @yesterday
inner join MasterStockList msl on msl.TradingSymbol = today.StockCode
where (((today.PriceOpen - yesterday.PriceClose) / today.PriceOpen) * 100  > @gapPercentage) and today.PriceClose < @priceRangeHigh and today.PriceClose > @priceRangeLow
union
select today.StockCode, msl.[Collection], cast(yesterday.TickerDateTime as Date) as 'Yesterday', yesterday.PriceClose, 
cast(today.TickerDateTime as date) as 'Today', today.PriceOpen, ((today.PriceOpen - yesterday.PriceClose) / today.PriceOpen) * 100 as GapPer,
(today.PriceOpen + (today.PriceOpen * 0.01)) as 'Target', today.PriceHigh as 'Final',
CASE WHEN today.PriceHigh > (today.PriceOpen + (today.PriceOpen * (@targetPercentage / 100))) THEN 'TRUE' ELSE 'FALSE' END as 'IsProfit'
from TickerMinElderIndicators yesterday 
inner join TickerMinElderIndicators today on yesterday.StockCode = today.StockCode and today.TickerDateTime = @today and yesterday.TickerDateTime = @yesterday
inner join MasterStockList msl on msl.TradingSymbol = today.StockCode
where (((today.PriceOpen - yesterday.PriceClose) / today.PriceOpen) * 100  < -@gapPercentage) and today.PriceClose < @priceRangeHigh and today.PriceClose > @priceRangeLow

END



GO


