/****** Object:  StoredProcedure [dbo].[spGetStocksWithGapOpening]    Script Date: 27 Sep 2019 14:34:51 ******/
DROP PROCEDURE [dbo].[spGetStocksWithGapOpening]
GO

/****** Object:  StoredProcedure [dbo].[spGetStocksWithGapOpening]    Script Date: 27 Sep 2019 14:34:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spGetStocksWithGapOpening] ( 
	@yesterday datetime,
	@today datetime,
	@percentage int,
	@stockPrice int
) AS
BEGIN

select today.StockCode, cast(yesterday.TickerDateTime as Date) as 'Yesterday', yesterday.PriceClose, 
cast(today.TickerDateTime as date) as 'Today', today.PriceOpen, ((today.PriceOpen - yesterday.PriceClose) / today.PriceOpen) * 100 as GapPer 
from TickerMinElderIndicators yesterday inner join TickerMinElderIndicators today on yesterday.StockCode = today.StockCode
and today.TickerDateTime = @Today and yesterday.TickerDateTime = @Yesterday
where (((today.PriceOpen - yesterday.PriceClose) / today.PriceOpen) * 100  > @percentage) and today.PriceClose > @stockPrice

union

select today.StockCode, cast(yesterday.TickerDateTime as Date) as 'Yesterday', yesterday.PriceClose, 
cast(today.TickerDateTime as date) as 'Today', today.PriceOpen, ((today.PriceOpen - yesterday.PriceClose) / today.PriceOpen) * 100 as GapPer 
from TickerMinElderIndicators yesterday inner join TickerMinElderIndicators today on yesterday.StockCode = today.StockCode
and today.TickerDateTime = @Today and yesterday.TickerDateTime = @Yesterday
where (((today.PriceOpen - yesterday.PriceClose) / today.PriceOpen) * 100  < -(@percentage)) and today.PriceClose > @stockPrice

END

GO


