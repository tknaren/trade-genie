
ALTER PROC [dbo].[spGetScriptsForBackTest] ( 
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
				- PRICECLOSE ) / PRICECLOSE) * 100, 2) 
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
			CAST(1 as bigint) AS PREVDAYRANKCLOSE,
			RANK() OVER (
				ORDER BY
				ROUND(((PRICEHIGH - PRICECLOSE) / PRICECLOSE) * 100, 2) DESC) 
			AS CURRTIMERANKHIGH,
		    RANK() OVER (
				ORDER BY
				ROUND(((PRICECLOSE - PRICELOW) / PRICECLOSE) * 100, 2) DESC) 
			AS CURRTIMERANKLOW,
            (
                SELECT
                    PRICECLOSE 
                FROM
                    TickerMinElderIndicators T8 
                WHERE
                    T8.StockCode = T1.STOCKCODE 
                    AND T8.TimePeriod = 375 
                    AND CAST(T8.TickerDateTime as DATE) = @strPrevDate
            )
			AS PREVPRICECLOSE,
            --CAST(1 as float) AS PREVPRICECLOSE,
            (
                SELECT
                    PRICELOW 
                FROM
                    TickerMinElderIndicators T2 
                WHERE
                    T2.StockCode = T1.STOCKCODE 
                    AND T2.TimePeriod = 375 
                    AND CAST(T2.TickerDateTime as DATE) = @strPrevDate
            )
			AS PREVPRICELOW,
            --CAST(1 as float) AS PREVPRICELOW,
            (
                SELECT
                    PRICEHIGH 
                FROM
                    TickerMinElderIndicators T3 
                WHERE
                    T3.StockCode = T1.STOCKCODE 
                    AND T3.TimePeriod = 375 
                    AND CAST(T3.TickerDateTime AS DATE) = @strPrevDate
            )
			AS PREVPRICEHIGH,
            --CAST(1 as float) AS PREVPRICEHIGH,
            (
                SELECT
                    PRICELOW 
                FROM
                    TickerMinElderIndicators T4 
                WHERE
                    T4.StockCode = T1.STOCKCODE 
                    AND T4.TimePeriod = 375 
                    AND CAST(T4.TickerDateTime AS DATE) = @strPrevToPrevDate
            )
			AS PREVPREVPRICELOW,
            --CAST(1 as float) AS PREVPREVPRICELOW,
            (
                SELECT
                    PRICEHIGH 
                FROM
                    TickerMinElderIndicators T5 
                WHERE
                    T5.StockCode = T1.STOCKCODE 
                    AND T5.TimePeriod = 375 
                    AND CAST(T5.TickerDateTime AS DATE) = @strPrevToPrevDate
            )
			AS PREVPREVPRICEHIGH 
            --CAST(1 as float) AS PREVPREVPRICEHIGH 
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
               AS CURRTRADEDVALUE, -- Traded value from Market Open till now
               ROUND(((PRICECLOSE - (
				   SELECT
					  MIN(PRICELOW) 
				   FROM
					  TickerMinElderIndicators T6 
				   WHERE
					  T6.StockCode = T1.STOCKCODE 
					  AND T6.TimePeriod = @intMinuteBar 
					  AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
					  AND T6.TickerDateTime <= @strCurrentDate)) / PRICECLOSE) * 100, 2) 
				AS PERFROMLOW, -- Percentage increase of close price from current days low
                ROUND((((
					SELECT
						MAX(PRICEHIGH) 
					FROM
						TickerMinElderIndicators T6 
					WHERE
						T6.StockCode = T1.STOCKCODE 
						AND T6.TimePeriod = @intMinuteBar 
						AND T6.TickerDateTime > CAST(@strCurrentDate AS DATE) 
						AND T6.TickerDateTime <= @strCurrentDate) - PRICECLOSE) / PRICECLOSE) * 100, 2) 
				AS PERFROMHIGH, --  Percentage decrease of close price from current days high
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
                AS CURRPRICELOW, -- Current day's lowest price recorded
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
                AS CURRPRICEHIGH, -- Current day highest price recorded
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
                        AND T6.TickerDateTime <= @strCurrentDate) - PRICECLOSE) / PRICECLOSE) * 100, 2) DESC) 
				AS INTRARANKHIGH, -- Rank for close price against current days highest price
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
                           AND T6.TickerDateTime <= @strCurrentDate)) / PRICECLOSE) * 100, 2) DESC) 
				AS INTRARANKLOW, -- Rank for close price against current day's lowest price
     --           RANK() OVER (
					--ORDER BY
					--	ROUND(((PRICECLOSE - (
					--	SELECT
					--		T7.PRICECLOSE 
					--	FROM
					--		TickerMinElderIndicators T7 
					--	WHERE
					--		T7.StockCode = T1.STOCKCODE 
					--		AND T7.TimePeriod = 375 
					--		AND T7.TickerDateTime = @strPrevDate)) / PRICECLOSE) * 100, 2) DESC) 
				CAST(1 as bigint) AS PREVDAYRANKCLOSE,
                RANK() OVER (
					ORDER BY
						ROUND(((PRICEHIGH - PRICECLOSE) / PRICECLOSE) * 100, 2) DESC) 
				AS CURRTIMERANKHIGH, -- Rank for the current period Close against high
                RANK() OVER (
					ORDER BY
					ROUND(((PRICECLOSE - PRICELOW) / PRICECLOSE) * 100, 2) DESC) 
				AS CURRTIMERANKLOW, -- Rank for the current period Close against Low
                (
                    SELECT
                    PRICECLOSE 
                    FROM
                    TickerMinElderIndicators T8 
                    WHERE
                    T8.StockCode = T1.STOCKCODE 
                    AND T8.TimePeriod = 375 
                    AND CAST(T8.TickerDateTime AS DATE) = @strPrevDate
                )
				AS PREVPRICECLOSE,
                --CAST(1 as float) AS PREVPRICECLOSE,
                (
                    SELECT
                    PRICELOW 
                    FROM
                    TickerMinElderIndicators T2 
                    WHERE
                    T2.StockCode = T1.STOCKCODE 
                    AND T2.TimePeriod = 375 
                    AND CAST(T2.TickerDateTime AS DATE) = @strPrevDate
                )
				AS PREVPRICELOW,
                --CAST(1 as float) AS PREVPRICELOW,
                (
                    SELECT
                    PRICEHIGH 
                    FROM
                    TickerMinElderIndicators T3 
                    WHERE
                    T3.StockCode = T1.STOCKCODE 
                    AND T3.TimePeriod = 375 
                    AND CAST(T3.TickerDateTime AS DATE) = @strPrevDate
                )
				AS PREVPRICEHIGH,
                --CAST(1 as float) AS PREVPRICEHIGH,
                (
                    SELECT
                    PRICELOW 
                    FROM
                    TickerMinElderIndicators T4 
                    WHERE
                    T4.StockCode = T1.STOCKCODE 
                    AND T4.TimePeriod = 375 
                    AND CAST(T4.TickerDateTime AS DATE) = @strPrevToPrevDate
                )
				AS PREVPREVPRICELOW,
                --CAST(1 as float) AS PREVPREVPRICELOW,
                (
                    SELECT
                    PRICEHIGH 
                    FROM
                    TickerMinElderIndicators T5 
                    WHERE
                    T5.StockCode = T1.STOCKCODE 
                    AND T5.TimePeriod = 375 
                    AND CAST(T5.TickerDateTime AS DATE) = @strPrevToPrevDate
                )
				AS PREVPREVPRICEHIGH 
                --CAST(1 as float) AS PREVPREVPRICEHIGH 
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



