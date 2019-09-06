DROP PROC [dbo].[spStockStatistics]
GO

CREATE PROC [dbo].[spStockStatistics] ( 
	@strCurrentDate datetime,
	@strPrevDate date,
	@intMinuteBar int
) AS
BEGIN

    WITH Impulse AS 
    (
	SELECT
        T1.TickerDateTime,
        T1.STOCKCODE,
        T1.PRICEOPEN,
        T1.PRICEHIGH,
        T1.PRICELOW,
        T1.PRICECLOSE,
		T1.TRADEDVALUE,
        ROUND(((T1.PRICECLOSE - (
			SELECT
				MIN(PRICELOW) 
			FROM
				TickerMinElderIndicators T4 
			WHERE
				T4.StockCode = T1.STOCKCODE 
				AND T4.TimePeriod = @intMinuteBar 
				AND T4.TickerDateTime > CAST(@strCurrentDate AS DATE) 
				AND T4.TickerDateTime <= @strCurrentDate)) 
			/ T1.PRICECLOSE) * 100, 2) 
		AS PERFROMLOW, -- Percentage increase of Close Price from Current days Low
		ROUND((((
			SELECT
				MAX(PRICEHIGH) 
			FROM
				TickerMinElderIndicators T5 
			WHERE
				T5.StockCode = T1.STOCKCODE 
				AND T5.TimePeriod = @intMinuteBar 
				AND T5.TickerDateTime > CAST(@strCurrentDate AS DATE) 
				AND T5.TickerDateTime <= @strCurrentDate)
			- T1.PRICECLOSE ) / T1.PRICECLOSE) * 100, 2) 
		AS PERFROMHIGH, -- Percentage decrease of Close Price from Current days High
        RANK() OVER (
            ORDER BY
                ROUND((((
                SELECT
                MAX(T6.PRICEHIGH) 
                FROM
                TickerMinElderIndicators T6 
                WHERE
                T6.StockCode = T1.STOCKCODE 
                AND T6.TimePeriod = @intMinuteBar 
                AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
                AND T6.TickerDateTime <= @strCurrentDate) - T1.PRICECLOSE) / T1.PRICECLOSE) * 100, 2) DESC) 
		AS INTRARANKHIGH, -- Rank for close price against current days highest price
        RANK() OVER (
                ORDER BY
                ROUND(((T1.PRICECLOSE - (
                SELECT
                    MIN(PRICELOW) 
                FROM
                    TickerMinElderIndicators T7 
                WHERE
                    T7.StockCode = T1.STOCKCODE 
                    AND T7.TimePeriod = @intMinuteBar 
                    AND T7.TickerDateTime > CAST(@strCurrentDate AS DATE) 
                    AND T7.TickerDateTime <= @strCurrentDate)) / T1.PRICECLOSE) * 100, 2) DESC) 
		AS INTRARANKLOW, -- Rank for close price against current day's lowest price
        RANK() OVER (
            ORDER BY
                ROUND((((
                SELECT
                MAX(T6.PRICEHIGH) 
                FROM
                TickerMinElderIndicators T6 
                WHERE
                T6.StockCode = T1.STOCKCODE 
                AND T6.TimePeriod = @intMinuteBar 
                AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
                AND T6.TickerDateTime <= @strCurrentDate) - T2.PRICECLOSE) / T2.PRICECLOSE) * 100, 2) DESC) 
		AS PREVDAYRANKHIGH, -- Rank for high price against previous days close
        RANK() OVER (
                ORDER BY
                ROUND(((T2.PRICECLOSE - (
                SELECT
                    MIN(T7.PRICELOW) 
                FROM
                    TickerMinElderIndicators T7 
                WHERE
                    T7.StockCode = T1.STOCKCODE 
                    AND T7.TimePeriod = @intMinuteBar 
                    AND T7.TickerDateTime > CAST(@strCurrentDate AS DATE) 
                    AND T7.TickerDateTime <= @strCurrentDate)) / T2.PRICECLOSE) * 100, 2) DESC) 
		AS PREVDAYRANKLOW, -- Rank for close price against previous days lowest price
		T2.PRICECLOSE AS PREVPRICECLOSE,
		T2.PRICELOW AS PREVPRICELOW,
		T2.PRICEHIGH AS PREVPRICEHIGH,
		T1.Impulse,
		T1.MACD,
		T1.Signal,
		T1.Histogram,
		T3.EHEMA1, -- EMA 8
		T3.EHEMA2, -- EMA 13
		T3.EHEMA3, -- EMA 21
		T3.EHEMA4, -- EMA 34
		T3.EHEMA5, -- EMA 55
		T1.ForceIndex1, -- ForceIndex 2
		T1.ForceIndex2, -- ForceIndex 13
		T1.RSI1, -- RSI 7
		T1.RSI2, -- RSI 14,
		T3.AllEMAsInNum
    FROM
        TickerMinElderIndicators T1
	LEFT OUTER JOIN TickerMinElderIndicators T2 
	ON T1.StockCode = T2.StockCode AND T2.TimePeriod = 375 AND CAST(T2.TickerDateTime AS DATE) = CAST(@strPrevDate as DATE)
	INNER JOIN  TickerMinEMAHA T3
	ON T1.StockCode = T3.StockCode AND T1.TimePeriod = T3.TimePeriod AND T1.TickerDateTime = T3.TickerDateTime
    WHERE
        T1.TimePeriod = @intMinuteBar 
        AND T1.TickerDateTime = @strCurrentDate 
	)
	  
    MERGE INTO ConsolidatedStockStatistics t1
    USING Impulse t2
    ON t1.StockCode = t2.StockCode and t1.TickerDateTime = t2.TickerDateTime AND t1.TimePeriod = @intMinuteBar
    WHEN NOT MATCHED THEN
    INSERT (TickerDateTime, STOCKCODE, TimePeriod, PRICEOPEN, PRICEHIGH, PRICELOW, PRICECLOSE, TRADEDVALUE, PERFROMLOW, PERFROMHIGH, 
			INTRARANKHIGH, INTRARANKLOW, PREVDAYRANKHIGH, PREVDAYRANKLOW, PREVPRICECLOSE, PREVPRICELOW, PREVPRICEHIGH, 
			Impulse, MACD, Signal, Histogram, EHEMA1, EHEMA2, EHEMA3, EHEMA4, EHEMA5, ForceIndex1, ForceIndex2, RSI1, RSI2, AllEMAsInNum)
	VALUES(t2.TickerDateTime, t2.STOCKCODE, CAST(@intMinuteBar as INT), t2.PRICEOPEN, t2.PRICEHIGH, t2.PRICELOW, t2.PRICECLOSE, t2.TRADEDVALUE, t2.PERFROMLOW, t2.PERFROMHIGH, 
		t2.INTRARANKHIGH, t2.INTRARANKLOW, t2.PREVDAYRANKHIGH, t2.PREVDAYRANKLOW, t2.PREVPRICECLOSE, t2.PREVPRICELOW, t2.PREVPRICEHIGH, 
		CAST(t2.Impulse as CHAR(1)), t2.MACD, t2.Signal, t2.Histogram, t2.EHEMA1, t2.EHEMA2, t2.EHEMA3, t2.EHEMA4, t2.EHEMA5, t2.ForceIndex1, t2.ForceIndex2, t2.RSI1, t2.RSI2 , CAST(t2.AllEMAsInNum as INT));

	--SELECT *  FROM Impulse

END
GO

