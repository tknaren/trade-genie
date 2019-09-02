USE [FinancialMarket]
GO

/****** Object:  StoredProcedure [dbo].[spGetStocksInMomentum]    Script Date: 01-07-2018 13:47:37 ******/
DROP PROCEDURE [dbo].[spGetStocksInMomentum]
GO

/****** Object:  StoredProcedure [dbo].[spGetStocksInMomentum]    Script Date: 01-07-2018 13:47:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[spGetStocksInMomentum] ( 
	@strCurrentDate datetime,
	@strPrevDate datetime,
	@strPrevToPrevDate datetime,
	@intMinuteBar int,
	@position varchar(10)
) AS
BEGIN
   IF (@position = 'LONG') 
   BEGIN
      WITH Momemtum AS 
      (
         SELECT
            TickerDateTime,
            STOCKCODE,
            PRICEOPEN,
            PRICEHIGH,
            PRICELOW,
            PRICECLOSE,
            TRADEDVALUE, -- Traded value for selected timeperiod
            (
               SELECT
                  SUM(TRADEDVALUE) 
               FROM
                  TickerMinElderIndicators T8 
               WHERE
                  T8.StockCode = T1.STOCKCODE 
                  AND T8.TimePeriod = @intMinuteBar 
                  AND T8.TickerDateTime > CAST(@strCurrentDate AS DATE) 
                  AND T8.TickerDateTime <= @strCurrentDate
            )
            AS CURRTRADEDVALUE, -- Traded value till now from Market Open
            ROUND(((PRICECLOSE - (
				SELECT
				   MIN(PRICELOW) 
				FROM
				   TickerMinElderIndicators T6 
				WHERE
				   T6.StockCode = T1.STOCKCODE 
				   AND T6.TimePeriod = @intMinuteBar 
				   AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
				   AND T6.TickerDateTime <= @strCurrentDate)) 
			   / PRICECLOSE) * 100, 2) 
			AS PERFROMLOW, -- Percentage increase of Close Price from Current days Low
			ROUND(((PRICECLOSE - (
				SELECT
					MAX(PRICEHIGH) 
				FROM
					TickerMinElderIndicators T6 
				WHERE
					T6.StockCode = T1.STOCKCODE 
					AND T6.TimePeriod = @intMinuteBar 
					AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
					AND T6.TickerDateTime <= @strCurrentDate)) 
				/ PRICECLOSE) * 100, 2) 
			AS PERFROMHIGH, -- Percentage decrease of Close Price from Current days High
			(
				SELECT
				MIN(PRICELOW) 
				FROM
				TickerMinElderIndicators T6 
				WHERE
				T6.StockCode = T1.STOCKCODE 
				AND T6.TimePeriod = @intMinuteBar 
				AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
				AND T6.TickerDateTime <= @strCurrentDate
			)
			AS CURRPRICELOW, -- Low Price recorded till now
			(
				SELECT
				MAX(PRICEHIGH) 
				FROM
				TickerMinElderIndicators T6 
				WHERE
				T6.StockCode = T1.STOCKCODE 
				AND T6.TimePeriod = @intMinuteBar 
				AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
				AND T6.TickerDateTime <= @strCurrentDate
			)
			AS CURRPRICEHIGH, -- High Price recorded till now
            RANK() OVER (
			ORDER BY
				ROUND((((
					SELECT
						MAX(PRICEHIGH) 
					FROM
						TickerMinElderIndicators T6 
					WHERE
						T6.StockCode = T1.STOCKCODE 
						AND T6.TimePeriod = @intMinuteBar 
						AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
						AND T6.TickerDateTime <= @strCurrentDate) 
				- PRICECLOSE) / PRICECLOSE) * 100, 2) DESC) 
			AS INTRARANKHIGH, -- Rank of Current Time period Close price from the Today's HIGH
            RANK() OVER (
			ORDER BY
				ROUND(((PRICECLOSE - (
					SELECT
					MIN(PRICELOW) 
					FROM
					TickerMinElderIndicators T6 
					WHERE
					T6.StockCode = T1.STOCKCODE 
					AND T6.TimePeriod = @intMinuteBar 
					AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
					AND T6.TickerDateTime <= @strCurrentDate)) 
				/ PRICECLOSE) * 100, 2) DESC) 
			AS INTRARANKLOW, -- Rank of Current time period Close Price from Today's LOW
			--        RANK() OVER (
			--        ORDER BY
						--ROUND((((
						--	SELECT
						--		T7.PRICECLOSE 
						--	FROM
						--		TickerMinElderIndicators T7 
						--	WHERE
						--		T7.StockCode = T1.STOCKCODE 
						--		AND T7.TimePeriod = 375 
						--		AND T7.TickerDateTime = @strPrevDate) 
						--- PRICECLOSE) / PRICECLOSE) * 100, 2) DESC) 
			0 AS PREVDAYRANKCLOSE,
			--         RANK() OVER (
					--ORDER BY
					--	ROUND(((PRICEHIGH - PRICECLOSE) / PRICECLOSE) * 100, 2) DESC) 
			0 AS CURRTIMERANKHIGH,
		   --         RANK() OVER (
					--ORDER BY
					--	ROUND(((PRICECLOSE - PRICELOW) / PRICECLOSE) * 100, 2) DESC) 
			0 AS CURRTIMERANKLOW,
            --(
            --    SELECT
            --        PRICECLOSE 
            --    FROM
            --        TickerMinElderIndicators T8 
            --    WHERE
            --        T8.StockCode = T1.STOCKCODE 
            --        AND T8.TimePeriod = 375 
            --        AND T8.TickerDateTime = @strPrevDate
            --)
            0 AS PREVPRICECLOSE,
            --(
            --    SELECT
            --        PRICELOW 
            --    FROM
            --        TickerMinElderIndicators T2 
            --    WHERE
            --        T2.StockCode = T1.STOCKCODE 
            --        AND T2.TimePeriod = 375 
            --        AND T2.TickerDateTime = @strPrevDate
            --)
            0 AS PREVPRICELOW,
            --(
            --    SELECT
            --        PRICEHIGH 
            --    FROM
            --        TickerMinElderIndicators T3 
            --    WHERE
            --        T3.StockCode = T1.STOCKCODE 
            --        AND T3.TimePeriod = 375 
            --        AND T3.TickerDateTime = @strPrevDate
            --)
            0 AS PREVPRICEHIGH,
            --(
            --    SELECT
            --        PRICELOW 
            --    FROM
            --        TickerMinElderIndicators T4 
            --    WHERE
            --        T4.StockCode = T1.STOCKCODE 
            --        AND T4.TimePeriod = 375 
            --        AND T4.TickerDateTime = @strPrevToPrevDate
            --)
            0 AS PREVPREVPRICELOW,
            --(
            --    SELECT
            --        PRICEHIGH 
            --    FROM
            --        TickerMinElderIndicators T5 
            --    WHERE
            --        T5.StockCode = T1.STOCKCODE 
            --        AND T5.TimePeriod = 375 
            --        AND T5.TickerDateTime = @strPrevToPrevDate
            --)
            0 AS PREVPREVPRICEHIGH 
            FROM
                TickerMinElderIndicators T1 
            WHERE
                TimePeriod = @intMinuteBar 
                AND TickerDateTime = @strCurrentDate 
      )
      SELECT
         *
      FROM
         Momemtum 			--WHERE CURRTIMERANKLOW < 10
      ORDER BY
         INTRARANKLOW 
   END
   ELSE
      BEGIN
         WITH Momemtum AS 
         (
            SELECT
               TickerDateTime,
               STOCKCODE,
               PRICEOPEN,
               PRICEHIGH,
               PRICELOW,
               PRICECLOSE,
               TRADEDVALUE,
               (
                  SELECT
                     SUM(TRADEDVALUE) 
                  FROM
                     TickerMinElderIndicators T8 
                  WHERE
                     T8.StockCode = T1.STOCKCODE 
                     AND T8.TimePeriod = @intMinuteBar 
                     AND T8.TickerDateTime > CAST(@strCurrentDate AS DATE) 
                     AND T8.TickerDateTime <= @strCurrentDate
               )
               AS CURRTRADEDVALUE,
               ROUND(((PRICECLOSE - (
               SELECT
                  MIN(PRICELOW) 
               FROM
                  TickerMinElderIndicators T6 
               WHERE
                  T6.StockCode = T1.STOCKCODE 
                  AND T6.TimePeriod = @intMinuteBar 
                  AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
                  AND T6.TickerDateTime <= @strCurrentDate)) / PRICECLOSE) * 100, 2) AS PERFROMLOW,
                  ROUND(((PRICECLOSE - (
                  SELECT
                     MAX(PRICEHIGH) 
                  FROM
                     TickerMinElderIndicators T6 
                  WHERE
                     T6.StockCode = T1.STOCKCODE 
                     AND T6.TimePeriod = @intMinuteBar 
                     AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
                     AND T6.TickerDateTime <= @strCurrentDate)) / PRICECLOSE) * 100, 2) AS PERFROMHIGH,
                     (
                        SELECT
                           MIN(PRICELOW) 
                        FROM
                           TickerMinElderIndicators T6 
                        WHERE
                           T6.StockCode = T1.STOCKCODE 
                           AND T6.TimePeriod = @intMinuteBar 
                           AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
                           AND T6.TickerDateTime <= @strCurrentDate
                     )
                     AS CURRPRICELOW,
                     (
                        SELECT
                           MAX(PRICEHIGH) 
                        FROM
                           TickerMinElderIndicators T6 
                        WHERE
                           T6.StockCode = T1.STOCKCODE 
                           AND T6.TimePeriod = @intMinuteBar 
                           AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
                           AND T6.TickerDateTime <= @strCurrentDate
                     )
                     AS CURRPRICEHIGH,
                     RANK() OVER (
                  ORDER BY
                     ROUND((((
                     SELECT
                        MAX(PRICEHIGH) 
                     FROM
                        TickerMinElderIndicators T6 
                     WHERE
                        T6.StockCode = T1.STOCKCODE 
                        AND T6.TimePeriod = @intMinuteBar 
                        AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
                        AND T6.TickerDateTime <= @strCurrentDate) - PRICECLOSE) / PRICECLOSE) * 100, 2) DESC) AS INTRARANKHIGH,
                        RANK() OVER (
                     ORDER BY
                        ROUND(((PRICECLOSE - (
                        SELECT
                           MIN(PRICELOW) 
                        FROM
                           TickerMinElderIndicators T6 
                        WHERE
                           T6.StockCode = T1.STOCKCODE 
                           AND T6.TimePeriod = @intMinuteBar 
                           AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
                           AND T6.TickerDateTime <= @strCurrentDate)) / PRICECLOSE) * 100, 2) DESC) AS INTRARANKLOW,
                           RANK() OVER (
                        ORDER BY
                           ROUND(((PRICECLOSE - (
                           SELECT
                              T7.PRICECLOSE 
                           FROM
                              TickerMinElderIndicators T7 
                           WHERE
                              T7.StockCode = T1.STOCKCODE 
                              AND T7.TimePeriod = 375 
                              AND T7.TickerDateTime = @strPrevDate)) / PRICECLOSE) * 100, 2) DESC) AS PREVDAYRANKCLOSE,
                              RANK() OVER (
                           ORDER BY
                              ROUND(((PRICEHIGH - PRICECLOSE) / PRICECLOSE) * 100, 2) DESC) AS CURRTIMERANKHIGH,
                              RANK() OVER (
                           ORDER BY
                              ROUND(((PRICECLOSE - PRICELOW) / PRICECLOSE) * 100, 2) DESC) AS CURRTIMERANKLOW,
                              (
                                 SELECT
                                    PRICECLOSE 
                                 FROM
                                    TickerMinElderIndicators T8 
                                 WHERE
                                    T8.StockCode = T1.STOCKCODE 
                                    AND T8.TimePeriod = 375 
                                    AND T8.TickerDateTime = @strPrevDate
                              )
                              AS PREVPRICECLOSE,
                              (
                                 SELECT
                                    PRICELOW 
                                 FROM
                                    TickerMinElderIndicators T2 
                                 WHERE
                                    T2.StockCode = T1.STOCKCODE 
                                    AND T2.TimePeriod = 375 
                                    AND T2.TickerDateTime = @strPrevDate
                              )
                              AS PREVPRICELOW,
                              (
                                 SELECT
                                    PRICEHIGH 
                                 FROM
                                    TickerMinElderIndicators T3 
                                 WHERE
                                    T3.StockCode = T1.STOCKCODE 
                                    AND T3.TimePeriod = 375 
                                    AND T3.TickerDateTime = @strPrevDate
                              )
                              AS PREVPRICEHIGH,
                              (
                                 SELECT
                                    PRICELOW 
                                 FROM
                                    TickerMinElderIndicators T4 
                                 WHERE
                                    T4.StockCode = T1.STOCKCODE 
                                    AND T4.TimePeriod = 375 
                                    AND T4.TickerDateTime = @strPrevToPrevDate
                              )
                              AS PREVPREVPRICELOW,
                              (
                                 SELECT
                                    PRICEHIGH 
                                 FROM
                                    TickerMinElderIndicators T5 
                                 WHERE
                                    T5.StockCode = T1.STOCKCODE 
                                    AND T5.TimePeriod = 375 
                                    AND T5.TickerDateTime = @strPrevToPrevDate
                              )
                              AS PREVPREVPRICEHIGH 
                           FROM
                              TickerMinElderIndicators T1 
                           WHERE
                              TimePeriod = @intMinuteBar 
                              AND TickerDateTime = @strCurrentDate 
         )
         SELECT
            *				--, (INTRARANKHIGH + PREVDAYRANKCLOSE) AS OVERALLRANKHIGH, (INTRARANKLOW + PREVDAYRANKCLOSE) AS OVERALLRANKLOW 
         FROM
            Momemtum 
         ORDER BY
            INTRARANKHIGH 
      END
END


GO


