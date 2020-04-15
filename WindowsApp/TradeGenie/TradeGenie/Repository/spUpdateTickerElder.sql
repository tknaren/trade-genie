USE [FinancialMarket]
GO

/****** Object:  StoredProcedure [dbo].[spUpdateTickerElder]    Script Date: 21-03-2018 09:56:04 ******/
DROP PROCEDURE [dbo].[spUpdateTickerElder]
GO

/****** Object:  StoredProcedure [dbo].[spUpdateTickerElder]    Script Date: 21-03-2018 09:56:04 ******/
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
      INSERT (StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume	) 
	  VALUES(t2.TradingSymbol, t2.[DateTime], @timePeriod, t2.[Open], t2.[High], t2.[Low], t2.[Close], t2.Volume);
END


GO


