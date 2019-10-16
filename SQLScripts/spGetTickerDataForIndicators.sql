/****** Object:  StoredProcedure [dbo].[spGetTickerDataForIndicators]    Script Date: 15 Oct 2019 18:58:28 ******/
DROP PROCEDURE [dbo].[spGetTickerDataForIndicators]
GO

/****** Object:  StoredProcedure [dbo].[spGetTickerDataForIndicators]    Script Date: 15 Oct 2019 18:58:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spGetTickerDataForIndicators]
      @InstrumentList varchar(2500),
	  @TimePeriods varchar(50),
	  @DateFrom date
AS
BEGIN

	--SELECT TMEI.StockCode, TMEI.TickerDateTime, TMEI.TimePeriod, TMEI.PriceOpen, TMEI.PriceHigh, TMEI.PriceLow, TMEI.PriceClose, TMEI.Volume,
	--	TMEI.Change, TMEI.ChangePercent, TMEI.TradedValue,
	--	TMEI.EMA1, TMEI.EMA2, TMEI.EMA3, TMEI.EMA4, 
	--	TMEI.MACD, TMEI.Signal, TMEI.Histogram, TMEI.HistIncDec, TMEI.Impulse, TMEI.ForceIndex1, TMEI.ForceIndex2, 
	--	TMEI.EMA1Dev, TMEI.EMA2Dev, TMEI.AG1, TMEI.AL1, TMEI.RSI1, TMEI.AG2, TMEI.AL2, TMEI.RSI2,
	--	TMST.TrueRange, TMST.ATR1, TMST.ATR2, TMST.ATR3, TMST.BUB1, TMST.BUB2, TMST.BUB3, TMST.BLB1, TMST.BLB2, TMST.BLB3,
	--	TMST.FUB1, TMST.FUB2, TMST.FUB3, TMST.FLB1, TMST.FLB2, TMST.FLB3, TMST.ST1, TMST.ST2, TMST.ST3, TMST.Trend1, TMST.Trend2, TMST.Trend3,
	--	TMEH.EHEMA1, TMEH.EHEMA2, TMEH.EHEMA3, TMEH.EHEMA4, TMEH.EHEMA5, TMEH.VWMA1, TMEH.VWMA2, 
	--	TMEH.HAOpen, TMEH.HAHigh, TMEH.HALow, TMEH.HAClose, TMEH.varEMA1v2, TMEH.varEMA1v3, TMEH.varEMA1v4, 
	--	TMEH.varEMA2v3, TMEH.varEMA2v4, TMEH.varEMA3v4, TMEH.varEMA4v5, TMEH.varVWMA1vVWMA2, TMEH.varVWMA1vPriceClose, 
	--	TMEH.varVWMA2vPriceClose, TMEH.varVWMA1vEMA1, TMEH.varHAOvHAC, TMEH.varHAOvHAPO, TMEH.varHACvHAPC, 
	--	TMEH.varOvC, TMEH.varOvPO, TMEH.varCvPC, TMEH.HAOCwEMA1, TMEH.OCwEMA1, TMEH.AllEMAsInNum
	--FROM TickerMinElderIndicators TMEI 
	--	INNER JOIN TickerMinSuperTrend TMST 
	--	ON TMEI.StockCode = TMST.StockCode AND TMEI.TimePeriod = TMST.TimePeriod AND TMEI.TickerDateTime = TMST.TickerDateTime
	--	INNER JOIN TickerMinEMAHA TMEH 
	--	ON TMEI.StockCode = TMEH.StockCode AND TMEI.TimePeriod = TMEH.TimePeriod AND TMEI.TickerDateTime = TMEH.TickerDateTime
	--WHERE TMEI.StockCode IN (SELECT * FROM ufn_CSVToTable(@InstrumentList,',')) 
	--AND TMEI.TimePeriod IN (SELECT * FROM ufn_CSVToTable(@TimePeriods,','))
	--AND (TMEI.EMA1 IS NULL OR ST1 IS NULL OR TMEH.EHEMA1 IS NULL) 
	--UNION
	--SELECT StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, 
	--	Change, ChangePercent, TradedValue, EMA1, EMA2, EMA3, EMA4, 
	--	MACD, Signal, Histogram, HistIncDec, Impulse, ForceIndex1, ForceIndex2, EMA1Dev, EMA2Dev, AG1, AL1, RSI1, AG2, AL2, RSI2,
	--	TrueRange, ATR1, ATR2, ATR3, BUB1, BUB2, BUB3, BLB1, BLB2, BLB3,
	--	FUB1, FUB2, FUB3, FLB1, FLB2, FLB3, ST1, ST2, ST3, Trend1, Trend2, Trend3,
	--	EHEMA1, EHEMA2, EHEMA3, EHEMA4, EHEMA5, VWMA1, VWMA2, HAOpen, HAHigh, HALow, HAClose, 
	--	varEMA1v2, varEMA1v3, varEMA1v4, varEMA2v3, varEMA2v4, varEMA3v4, varEMA4v5, 
	--	varVWMA1vVWMA2, varVWMA1vPriceClose, varVWMA2vPriceClose, varVWMA1vEMA1, varHAOvHAC, varHAOvHAPO, varHACvHAPC, 
	--	varOvC, varOvPO, varCvPC, HAOCwEMA1, OCwEMA1, AllEMAsInNum
	--FROM (SELECT TMEI.StockCode, TMEI.TickerDateTime, TMEI.TimePeriod, TMEI.PriceOpen, TMEI.PriceHigh, TMEI.PriceLow, TMEI.PriceClose, TMEI.Volume, 
	--			TMEI.Change, TMEI.ChangePercent, TMEI.TradedValue, TMEI.EMA1, TMEI.EMA2, TMEI.EMA3, TMEI.EMA4, 
	--			TMEI.MACD, TMEI.Signal, TMEI.Histogram, TMEI.HistIncDec, TMEI.Impulse, TMEI.ForceIndex1, TMEI.ForceIndex2, 
	--			TMEI.EMA1Dev, TMEI.EMA2Dev, TMEI.AG1, TMEI.AL1, TMEI.RSI1, TMEI.AG2, TMEI.AL2, TMEI.RSI2,
	--			TMST.TrueRange, TMST.ATR1, TMST.ATR2, TMST.ATR3, TMST.BUB1, TMST.BUB2, TMST.BUB3, TMST.BLB1, TMST.BLB2, TMST.BLB3,
	--			TMST.FUB1, TMST.FUB2, TMST.FUB3, TMST.FLB1, TMST.FLB2, TMST.FLB3, TMST.ST1, TMST.ST2, TMST.ST3, TMST.Trend1, TMST.Trend2, TMST.Trend3,
	--			TMEH.EHEMA1, TMEH.EHEMA2, TMEH.EHEMA3, TMEH.EHEMA4, TMEH.EHEMA5, TMEH.VWMA1, TMEH.VWMA2, 
	--			TMEH.HAOpen, TMEH.HAHigh, TMEH.HALow, TMEH.HAClose, TMEH.varEMA1v2, TMEH.varEMA1v3, TMEH.varEMA1v4, 
	--			TMEH.varEMA2v3, TMEH.varEMA2v4, TMEH.varEMA3v4, TMEH.varEMA4v5, TMEH.varVWMA1vVWMA2, TMEH.varVWMA1vPriceClose, 
	--			TMEH.varVWMA2vPriceClose, TMEH.varVWMA1vEMA1, TMEH.varHAOvHAC, TMEH.varHAOvHAPO, TMEH.varHACvHAPC, 
	--			TMEH.varOvC, TMEH.varOvPO, TMEH.varCvPC, TMEH.HAOCwEMA1, TMEH.OCwEMA1, TMEH.AllEMAsInNum,
	--			ROW_NUMBER() OVER(partition BY TMEI.StockCode,TMEI.TimePeriod ORDER BY TMEI.TickerDateTime DESC) AS RankValue
	--			FROM TickerMinElderIndicators TMEI INNER JOIN TickerMinSuperTrend TMST 
	--				ON TMEI.StockCode = TMST.StockCode AND TMEI.TimePeriod = TMST.TimePeriod AND TMEI.TickerDateTime = TMST.TickerDateTime
	--			INNER JOIN TickerMinEMAHA TMEH 
	--				ON TMEI.StockCode = TMEH.StockCode AND TMEI.TimePeriod = TMEH.TimePeriod AND TMEI.TickerDateTime = TMEH.TickerDateTime
	--			where TMEI.StockCode IN (SELECT * FROM ufn_CSVToTable(@InstrumentList,','))  
	--			AND TMEI.TimePeriod IN (SELECT * FROM ufn_CSVToTable(@TimePeriods,','))
	--			AND EMA1 IS NOT NULL
	--		) i
	--WHERE RankValue=1
	--ORDER BY StockCode, TimePeriod, TickerDateTime 

	IF (@TimePeriods != '375')
	BEGIN

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
					AND TMEI.TickerDateTime >= @DateFrom
				) i
		WHERE RankValue=1
		ORDER BY StockCode, TimePeriod, TickerDateTime 

	END
	ELSE
	BEGIN

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
					AND TMEI.TimePeriod = 375
					AND EMA1 IS NOT NULL
					AND TMEI.TickerDateTime >= getDate() - 5
				) i
		WHERE RankValue=1
		ORDER BY StockCode, TimePeriod, TickerDateTime 

	END


END

GO


