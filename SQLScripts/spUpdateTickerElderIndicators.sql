/****** Object:  StoredProcedure [dbo].[spUpdateTickerElderIndicators]    Script Date: 13-10-2019 13:29:59 ******/
DROP PROCEDURE [dbo].[spUpdateTickerElderIndicators]
GO

/****** Object:  StoredProcedure [dbo].[spUpdateTickerElderIndicators]    Script Date: 13-10-2019 13:29:59 ******/
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
		t1.RSI2 = t2.RSI2
	 WHEN NOT MATCHED THEN 
		INSERT (StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, Change, ChangePercent, TradedValue, 
			EMA1, EMA2, EMA3, EMA4, MACD, Signal, Histogram, HistIncDec, Impulse, ForceIndex1, ForceIndex2, EMA1Dev, EMA2Dev, 
			AG1, AL1, RSI1, AG2, AL2, RSI2)
		VALUES (t2.StockCode, t2.TickerDateTime, t2.TimePeriod, t2.PriceOpen, t2.PriceHigh, t2.PriceLow, t2.PriceClose, t2.Volume, t2.Change, t2.ChangePercent, t2.TradedValue, 
			t2.EMA1, t2.EMA2, t2.EMA3, t2.EMA4, t2.MACD, t2.Signal, t2.Histogram, t2.HistIncDec, t2.Impulse, t2.ForceIndex1, t2.ForceIndex2, t2.EMA1Dev, t2.EMA2Dev, 
			t2.AG1, t2.AL1, t2.RSI1, t2.AG2, t2.AL2, t2.RSI2);

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
		t1.Trend3 = t2.Trend3
	WHEN NOT MATCHED THEN 
	INSERT (StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, 
		TrueRange, ATR1, ATR2, ATR3, BUB1, BUB2, BUB3, BLB1, BLB2, BLB3, FUB1, FUB2, FUB3, FLB1, FLB2, FLB3, 
		ST1, ST2, ST3, Trend1, Trend2, Trend3)
	VALUES (t2.StockCode, t2.TickerDateTime, t2.TimePeriod, t2.PriceOpen, t2.PriceHigh, t2.PriceLow, t2.PriceClose, t2.Volume, 
		t2.TrueRange, t2.ATR1, t2.ATR2, t2.ATR3, t2.BUB1, t2.BUB2, t2.BUB3, t2.BLB1, t2.BLB2, t2.BLB3, t2.FUB1, t2.FUB2, t2.FUB3, t2.FLB1, t2.FLB2, t2.FLB3, 
		t2.ST1, t2.ST2, t2.ST3, t2.Trend1, t2.Trend2, t2.Trend3);


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
		t1.AllEMAsInNum = t2.AllEMAsInNum
	WHEN NOT MATCHED THEN 
		INSERT (StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, 
			EHEMA1, EHEMA2, EHEMA3, EHEMA4, EHEMA5, VWMA1, VWMA2, HAOpen, HAHigh, HALow, HAClose, 
			varEMA1v2, varEMA1v3, varEMA1v4, varEMA2v3, varEMA2v4, varEMA3v4, varEMA4v5, 
			varVWMA1vVWMA2, varVWMA1vPriceClose, varVWMA2vPriceClose, varVWMA1vEMA1, 
			varHAOvHAC, varHAOvHAPO, varHACvHAPC, varOvC, varOvPO, varCvPC, HAOCwEMA1, OCwEMA1, AllEMAsInNum)
		VALUES (t2.StockCode, t2.TickerDateTime, t2.TimePeriod, t2.PriceOpen, t2.PriceHigh, t2.PriceLow, t2.PriceClose, t2.Volume, 
			t2.EHEMA1, t2.EHEMA2, t2.EHEMA3, t2.EHEMA4, t2.EHEMA5, t2.VWMA1, t2.VWMA2, t2.HAOpen, t2.HAHigh, t2.HALow, t2.HAClose, 
			t2.varEMA1v2, t2.varEMA1v3, t2.varEMA1v4, t2.varEMA2v3, t2.varEMA2v4, t2.varEMA3v4, t2.varEMA4v5, 
			t2.varVWMA1vVWMA2, t2.varVWMA1vPriceClose, t2.varVWMA2vPriceClose, t2.varVWMA1vEMA1, 
			t2.varHAOvHAC, t2.varHAOvHAPO, t2.varHACvHAPC, t2.varOvC, t2.varOvPO, t2.varCvPC, t2.HAOCwEMA1, t2.OCwEMA1, t2.AllEMAsInNum);

END


GO


