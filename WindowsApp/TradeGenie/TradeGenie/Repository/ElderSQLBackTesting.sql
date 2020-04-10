DECLARE @InstrumentList varchar(2500)
DECLARE @TimePeriod INT
DECLARE @StartTime datetime
DECLARE @EndTime datetime
DECLARE @TickerTime datetime
DECLARE @TestEndTime datetime
DECLARE @TestStartPeriod datetime 
DECLARE @TestEndPeriod datetime 
DECLARE @StopLossValue decimal (9,3)
DECLARE @ProfitValue decimal (9,3)
DECLARE @TestPositionType varchar(10)
--DECLARE @ProfitValue decimal (9,3)

SET @TestPositionType = 'LONG'
SET @TestStartPeriod = '2018-02-09'
SET @TestEndPeriod = '2018-02-10'
SET @TimePeriod = 15
SET @StopLossValue = 1.006
SET @ProfitValue = 1.003

DECLARE @tempElderTable TABLE
(
	InstrumentToken INT,
	StockCode	varchar(20),
	TickerDateTime	datetime,
	TimePeriod	int,
	PriceOpen	float,
	PriceHigh	float,
	PriceLow	float,
	PriceClose	float,
	Volume	int,
	EMA1	float,
	EMA2	float,
	EMA3	float,
	EMA4	float,
	MACD	float,
	Signal	float,
	Histogram	float,
	HistIncDec	varchar(1),
	Impulse	varchar(1),
	ForceIndex1	float,
	ForceIndex2	float,
	EMA1Dev	float,
	EMA2Dev	float	
);

DECLARE @tempElderOrders TABLE
(
	TradingSymbol	varchar(20),
	TradeDate	date,
	EntryPrice	decimal(9,2),
	ExitPrice	decimal(9,2),
	StopLoss	decimal(9,2),
	Quantity	int,
	ProfitLoss	decimal(9,2),
	EntryTime	datetime,
	ExitTime	datetime,
	StopLossHitTime	datetime,
	PositionType	varchar(20),
	PositionStatus	varchar(20)
);

WHILE(@TestStartPeriod < @TestEndPeriod)
BEGIN

	SET @StartTime		= DATEADD(HOUR,9,DATEADD(MINUTE,15,@TestStartPeriod)) 
	SET @EndTime		= DATEADD(HOUR,9,DATEADD(MINUTE,30,@TestStartPeriod)) 
	SET @TestEndTime	= DATEADD(HOUR,15,DATEADD(MINUTE,15,@TestStartPeriod)) 

	WHILE(@EndTime <= @TestEndTime)

	BEGIN

		DECLARE @strTradingSymbol VARCHAR(25)

		DECLARE Ticker_Cursor CURSOR FOR 
			SELECT TradingSymbol FROM MasterStockList WHERE IsIncluded = 1 order by TradingSymbol

		OPEN Ticker_Cursor

		FETCH NEXT FROM Ticker_Cursor INTO @strTradingSymbol

			WHILE @@FETCH_STATUS = 0

			BEGIN
				
				PRINT @strTradingSymbol

				----------------------------------------------------------------------------------------------------------------------------------
				------------------------------------- RETRIEVE LAST 3 RECORDS --------------------------------------------------------------------
				----------------------------------------------------------------------------------------------------------------------------------

				DELETE FROM @tempElderTable

				INSERT INTO @tempElderTable
				SELECT InstrumentToken, StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, EMA1, EMA2, EMA3, EMA4, 
						MACD, Signal, Histogram, HistIncDec, Impulse, ForceIndex1, ForceIndex2, EMA1Dev, EMA2Dev
				FROM (SELECT tMSL.InstrumentToken, tElder.*,
							ROW_NUMBER() OVER(partition BY StockCode,TimePeriod ORDER BY TickerDateTime DESC) AS RankValue
							FROM TickerMinElderIndicators tElder 
							INNER JOIN MasterStockList tMSL ON tElder.StockCode = tMSL.TradingSymbol
							WHERE StockCode = @strTradingSymbol -- (SELECT * FROM ufn_CSVToTable(@InstrumentList,','))  
							AND TimePeriod =  @TimePeriod --(SELECT * FROM ufn_CSVToTable(@TimePeriods,','))
							AND EMA1 IS NOT NULL AND TickerDateTime BETWEEN @StartTime AND @EndTime
						) i
				WHERE RankValue IN (1,2,3)
				ORDER BY StockCode, TickerDateTime

				SELECT COUNT(*) FROM @tempElderTable

				----------------------------------------------------------------------------------------------------------------------------------
				------------------------------------- VARAIBLE DECLARATION SECTION ---------------------------------------------------------------
				----------------------------------------------------------------------------------------------------------------------------------

				DECLARE @CurrPriceOpen float, @CurrPriceHigh float, @CurrPriceLow float, @CurrPriceClose float
				DECLARE @CurrEMAShort float, @CurrEMAMedium float, @CurrEMALong float, @CurrShortEMADev float, @CurrLongEMADev float
				DECLARE @CurrHistIncDec varchar(1), @CurrImpulse varchar(1)

				DECLARE @PrevPriceOpen float, @PrevPriceHigh float, @PrevPriceLow float, @PrevPriceClose float
				DECLARE @PrevEMAShort float, @PrevEMAMedium float, @PrevEMALong float, @PrevShortEMADev float, @PrevLongEMADev float
				DECLARE @PrevHistIncDec varchar(1), @PrevImpulse varchar(1)

				DECLARE @PrevPrevPriceOpen float, @PrevPrevPriceHigh float, @PrevPrevPriceLow float, @PrevPrevPriceClose float
				DECLARE @PrevPrevEMAShort float, @PrevPrevEMAMedium float, @PrevPrevEMALong float, @PrevPrevShortEMADev float, @PrevPrevLongEMADev float
				DECLARE @PrevPrevHistIncDec varchar(1), @PrevPrevImpulse varchar(1)

				----------------------------------------------------------------------------------------------------------------------------------
				------------------------------------- VALUE ASSIGNMENT SECTION -------------------------------------------------------------------
				----------------------------------------------------------------------------------------------------------------------------------


				SELECT @CurrPriceOpen = PriceOpen, @CurrPriceHigh = PriceHigh, @CurrPriceLow = PriceLow, @CurrPriceClose = PriceClose
						,@CurrEMAShort = ISNULL(EMA1,0), @CurrEMAMedium = ISNULL(EMA2,0), @CurrEMALong = ISNULL(EMA4,0), @CurrShortEMADev = ISNULL(EMA1Dev,0), @CurrLongEMADev = ISNULL(EMA2Dev,0)
						,@CurrHistIncDec = RTRIM(LTRIM(ISNULL(HistIncDec,''))), @CurrImpulse = RTRIM(LTRIM(ISNULL(Impulse,'')))
				FROM @tempElderTable WHERE StockCode = @strTradingSymbol AND TickerDateTime = @EndTime 

				SELECT @PrevPriceOpen = PriceOpen, @PrevPriceHigh = PriceHigh, @PrevPriceLow = PriceLow, @PrevPriceClose = PriceClose
						,@PrevEMAShort = ISNULL(EMA1,0), @PrevEMAMedium = ISNULL(EMA2,0), @PrevEMALong = ISNULL(EMA4,0), @PrevShortEMADev = ISNULL(EMA1Dev,0), @PrevLongEMADev = ISNULL(EMA2Dev,0)
						,@PrevHistIncDec = RTRIM(LTRIM(ISNULL(HistIncDec,''))), @PrevImpulse = RTRIM(LTRIM(ISNULL(Impulse,'')))
				FROM @tempElderTable WHERE StockCode = @strTradingSymbol AND TickerDateTime = DATEADD(minute,-(@TimePeriod),@EndTime) 

				SELECT @PrevPrevPriceOpen = PriceOpen, @PrevPrevPriceHigh = PriceHigh, @PrevPrevPriceLow = PriceLow, @PrevPrevPriceClose = PriceClose
						,@PrevPrevEMAShort = ISNULL(EMA1,0), @PrevPrevEMAMedium = ISNULL(EMA2,0), @PrevPrevEMALong = ISNULL(EMA4,0), @PrevPrevShortEMADev = ISNULL(EMA1Dev,0), @PrevPrevLongEMADev = ISNULL(EMA2Dev,0)
						,@PrevPrevHistIncDec = RTRIM(LTRIM(ISNULL(HistIncDec,''))), @PrevPrevImpulse = RTRIM(LTRIM(ISNULL(Impulse,'')))
				FROM @tempElderTable WHERE StockCode = @strTradingSymbol AND TickerDateTime = DATEADD(minute,-(@TimePeriod * 2),@EndTime) 

				--PRINT 'CURRENT-' + CONVERT(VARCHAR,@CurrPriceOpen) + ',' + CONVERT(VARCHAR,@CurrPriceHigh) + ',' + CONVERT(VARCHAR,@CurrPriceLow) + ',' + CONVERT(VARCHAR,@CurrPriceClose) 
				--		+ ',' + CONVERT(VARCHAR,@CurrEMAShort) + ',' + CONVERT(VARCHAR,@CurrEMALong) + ',' + CONVERT(VARCHAR,@CurrShortEMADev) + ',' + CONVERT(VARCHAR,@CurrLongEMADev) 
				--		+ ',' + @CurrHistIncDec + ',' + @CurrImpulse

				--PRINT 'PREVIOUS-' + CONVERT(VARCHAR,@PrevPriceOpen) + ',' + CONVERT(VARCHAR,@PrevPriceHigh) + ',' + CONVERT(VARCHAR,@PrevPriceLow) + ',' + CONVERT(VARCHAR,@PrevPriceClose) 
				--		+ ',' + CONVERT(VARCHAR,@PrevEMAShort) + ',' + CONVERT(VARCHAR,@PrevEMALong) + ',' + CONVERT(VARCHAR,@PrevShortEMADev) + ',' + CONVERT(VARCHAR,@PrevLongEMADev) 
				--		+ ',' + @PrevHistIncDec + ',' + @PrevImpulse

				----------------------------------------------------------------------------------------------------------------------------------
				------------------------------------- BUSINESS LOGIC SECTION ---------------------------------------------------------------------
				----------------------------------------------------------------------------------------------------------------------------------

				DECLARE @EntryPrice	decimal(9,2)
				DECLARE @ExitPrice	decimal(9,2)
				DECLARE @StopLoss	decimal(9,2)
				DECLARE @Quantity	int
				DECLARE @ProfitLoss	decimal(9,2)
				DECLARE @EntryTime	datetime
				DECLARE @ExitTime	datetime
				DECLARE @StopLossHitTime	datetime
				DECLARE @PositionType	varchar(20)
				DECLARE @PositionStatus	varchar(20)
				DECLARE @TickerPriceLow decimal(9,2)
				DECLARE @TickerPriceHigh decimal(9,2)
				DECLARE @TickerPriceClose decimal(9,2)
				DECLARE @Capital int

				-- LOGIC FOR LONG 

				IF (@TestPositionType = 'LONG')
				BEGIN
					IF (
					--LOGIC - IsCurrentOpenClosePriceGreaterThanLongEMA
						--(@CurrPriceOpen > @CurrEMAShort AND @CurrPriceClose > @CurrEMAShort) 
							(@CurrPriceClose > @CurrEMAShort) 
						AND (@CurrPriceOpen > @CurrEMAMedium AND @CurrPriceClose > @CurrEMAMedium) 
						AND (@CurrPriceOpen > @CurrEMALong AND @CurrPriceClose > @CurrEMALong AND @CurrPriceLow > @CurrEMALong) 
					--LOGIC - IsPreviousOpenClosePriceGreaterThanLongEMA || IsPreviousClosePriceGreaterThanLongEMA
						AND ((@PrevPriceOpen > @PrevEMALong AND @PrevPriceClose > @PrevEMAShort) OR (@PrevPriceClose > @PrevEMAShort))
					--LOGIC - CurrentImpulseColor
						AND (@CurrImpulse = 'G')
					--LOGIC - PreviousImpulseColor
						AND ((@PrevImpulse = 'G') OR (@PrevImpulse = 'B'))
					--LOGIC - CurrentHistogramMovement
						AND (@CurrHistIncDec = 'I')
					--LOGIC - PreviousHistogramMovement
						AND (@PrevHistIncDec = 'I')
					--LOGIC - IsCurrentShortEMADeviationWithinPercentageRange
						--AND (@CurrShortEMADev > 0.5) --AND (@CurrShortEMADev < 2)
					--LOGIC - IsCurrentShortEMADevIsGreaterThenPreviousShortEMADev
						AND (@CurrShortEMADev > @PrevShortEMADev) AND (@PrevShortEMADev > @PrevPrevShortEMADev)
					--LOGIC - IsCurrentShortEMAisGreaterThanLongEMA AND IsPreviousShortEMAisGreaterThanCurrentLongEMA
						AND (@PrevEMAShort > (@PrevEMALong * 1.002)) AND (@CurrEMAShort > (@CurrEMALong * 1.004))
					--LOGIC - PriceRangeOfTheStock
						AND (@CurrPriceClose > 100 AND @CurrPriceClose < 3000)
						)

					--LOGIC - TrendOfTheStock
		
					BEGIN

						SET  @Capital = 10000

						SELECT @EntryPrice = [Open] FROM TickerMin WHERE [DateTime] = DATEADD(minute, @TimePeriod + 2, @EndTime) AND TradingSymbol = @strTradingSymbol
						SELECT @EntryTime = [DateTime] FROM TickerMin WHERE [DateTime] = DATEADD(minute, @TimePeriod + 2, @EndTime) AND TradingSymbol = @strTradingSymbol 
						SELECT @StopLoss = ROUND(@EntryPrice / @StopLossValue,1)
						SELECT @StopLoss = MIN(PriceLow) FROM @tempElderTable
						SELECT @Quantity = ROUND(@Capital/@EntryPrice,0) FROM @tempElderTable

						 --GET THE RECORD FOR THE TIME PERIOD FROM THE TICKER MIN
						 --SET THE OPEN PRICE AS THE ENTRY PRICE.
						 --GET THE SL PRICE AS THE MIN OF THE LAST 3 PRICES.
						 --LOOP THRU THE TICKER MIN TABLE FOR THAT DATE
						 --IF PRICELOW IS <= SL, THEN EXIT = SL AND EXIT THE POSITION AND CAPTURE EXIT AND SL TIME
						 --IF PRICEHIGH IS >= ENTRY * 1.003 THEN EXIT = ENTRY
						 --IF EXIT IS NOT NULL AND PRICEHIGH IS >= EXIT * 1.006 THEN EXIT = EXIT * 1.003
						 --IF EXIT IS NOT NULL AND PRICELOW IS <= EXIT THEN EXIT THE POSITION AND CAPTURE EXIT TIME

						IF NOT EXISTS( SELECT 1 FROM @tempElderOrders WHERE TradingSymbol = @strTradingSymbol AND ExitPrice IS NULL )
						BEGIN
							IF (DATEPART(HOUR, @EntryTime) < 15)
							BEGIN
								INSERT INTO @tempElderOrders(TradingSymbol, TradeDate, EntryPrice, StopLoss, Quantity, EntryTime, PositionType, PositionStatus)
								VALUES (@strTradingSymbol, CONVERT(date, @EndTime), @EntryPrice, @StopLoss, @Quantity, @EntryTime, 'LONG', 'OPEN')
							END
						END
				
						SET @TickerTime = DATEADD(minute,@TimePeriod + 2,@EndTime)

						WHILE(@TickerTime <= @TestEndTime)
						BEGIN
					
							SELECT @TickerPriceLow = [Low], @TickerPriceHigh = [High], @TickerPriceClose = [Close] FROM TickerMin 
							WHERE TradingSymbol = @strTradingSymbol AND [DateTime] = @TickerTime

							IF (@TickerPriceLow <= @StopLoss)
							BEGIN
								UPDATE @tempElderOrders
								SET StopLossHitTime = @TickerTime, PositionStatus = 'LOSS', ExitPrice = @TickerPriceLow, ExitTime = @TickerTime, ProfitLoss = ROUND((@TickerPriceLow - @EntryPrice) * @Quantity,1)
								WHERE TradingSymbol = @strTradingSymbol AND ExitPrice IS NULL
							END
							ELSE IF (@TickerPriceHigh >= ROUND(@EntryPrice * @ProfitValue,1))
							BEGIN
								UPDATE @tempElderOrders
								SET PositionStatus = 'PROFIT', ExitPrice = @TickerPriceHigh, ExitTime = @TickerTime, ProfitLoss = ROUND((@TickerPriceClose - @EntryPrice) * @Quantity,1) --ProfitLoss = 50 
								WHERE TradingSymbol = @strTradingSymbol AND ExitPrice IS NULL						
							END

							SET @TickerTime = DATEADD(minute,1,@TickerTime)
						END

						UPDATE @tempElderOrders
						SET ProfitLoss = ROUND((@TickerPriceClose - @EntryPrice) * @Quantity,1)
						WHERE TradingSymbol = @strTradingSymbol AND PositionStatus = 'OPEN'

					END
				END
				ELSE
				-- LOGIC FOR SHORT
				BEGIN
					IF (
				--LOGIC - IsCurrentOpenClosePriceGreaterThanLongEMA
					--(@CurrPriceOpen > @CurrEMAShort AND @CurrPriceClose > @CurrEMAShort) 
						(@CurrPriceClose < @CurrEMAShort) 
					AND (@CurrPriceOpen < @CurrEMAMedium AND @CurrPriceClose < @CurrEMAMedium) 
					AND (@CurrPriceOpen < @CurrEMALong AND @CurrPriceClose < @CurrEMALong AND @CurrPriceHigh < @CurrEMALong) 
				--LOGIC - IsPreviousOpenClosePriceGreaterThanLongEMA || IsPreviousClosePriceGreaterThanLongEMA
					AND ((@PrevPriceOpen < @PrevEMALong AND @PrevPriceClose < @PrevEMAShort) OR (@PrevPriceClose < @PrevEMAShort))
				--LOGIC - CurrentImpulseColor
					AND (@CurrImpulse = 'R')
				--LOGIC - PreviousImpulseColor
					AND ((@PrevImpulse = 'R') OR (@PrevImpulse = 'B'))
				--LOGIC - CurrentHistogramMovement
					AND (@CurrHistIncDec = 'D')
				--LOGIC - PreviousHistogramMovement
					AND (@PrevHistIncDec = 'D')
				--LOGIC - IsCurrentShortEMADeviationWithinPercentageRange
					--AND (@CurrShortEMADev > 0.5) --AND (@CurrShortEMADev < 2)
				--LOGIC - IsCurrentShortEMADevIsGreaterThenPreviousShortEMADev
					AND (@CurrShortEMADev < @PrevShortEMADev) AND (@PrevShortEMADev < @PrevPrevShortEMADev)
				--LOGIC - IsCurrentShortEMAisGreaterThanLongEMA AND IsPreviousShortEMAisGreaterThanCurrentLongEMA
					AND (@PrevEMAShort < (@PrevEMALong / 1.002)) AND (@CurrEMAShort < (@CurrEMALong / 1.004))
				--LOGIC - PriceRangeOfTheStock
					AND (@CurrPriceClose > 100 AND @CurrPriceClose < 3000)
					)

				--LOGIC - TrendOfTheStock
		
				BEGIN

					SET  @Capital = 10000

					SELECT @EntryPrice = [Open] FROM TickerMin WHERE [DateTime] = DATEADD(minute, @TimePeriod + 2, @EndTime) AND TradingSymbol = @strTradingSymbol
					SELECT @EntryTime = [DateTime] FROM TickerMin WHERE [DateTime] = DATEADD(minute, @TimePeriod + 2, @EndTime) AND TradingSymbol = @strTradingSymbol 
					SELECT @StopLoss = ROUND(@EntryPrice * @StopLossValue,1)
					--SELECT @StopLoss = MIN(PriceLow) FROM @tempElderTable
					SELECT @Quantity = ROUND(@Capital / @EntryPrice,0) FROM @tempElderTable

					-- GET THE RECORD FOR THE TIME PERIOD FROM THE TICKER MIN
					-- SET THE OPEN PRICE AS THE ENTRY PRICE.
					-- GET THE SL PRICE AS THE MIN OF THE LAST 3 PRICES.
					-- LOOP THRU THE TICKER MIN TABLE FOR THAT DATE
					-- IF PRICELOW IS <= SL, THEN EXIT = SL AND EXIT THE POSITION AND CAPTURE EXIT AND SL TIME
					-- IF PRICEHIGH IS >= ENTRY * 1.003 THEN EXIT = ENTRY
					-- IF EXIT IS NOT NULL AND PRICEHIGH IS >= EXIT * 1.006 THEN EXIT = EXIT * 1.003
					-- IF EXIT IS NOT NULL AND PRICELOW IS <= EXIT THEN EXIT THE POSITION AND CAPTURE EXIT TIME

					IF NOT EXISTS( SELECT 1 FROM @tempElderOrders WHERE TradingSymbol = @strTradingSymbol AND ExitPrice IS NULL )
					BEGIN
						IF (DATEPART(HOUR, @EntryTime) < 15)
						BEGIN
							INSERT INTO @tempElderOrders(TradingSymbol, TradeDate, EntryPrice, StopLoss, Quantity, EntryTime, PositionType, PositionStatus)
							VALUES (@strTradingSymbol, CONVERT(date, @EndTime), @EntryPrice, @StopLoss, @Quantity, @EntryTime, 'SHORT', 'OPEN')
						END
					END
				
					SET @TickerTime = DATEADD(minute,@TimePeriod + 2,@EndTime)

					WHILE(@TickerTime <= @TestEndTime)
					BEGIN
					
						SELECT @TickerPriceLow = [Low], @TickerPriceHigh = [High], @TickerPriceClose = [Close] FROM TickerMin 
						WHERE TradingSymbol = @strTradingSymbol AND [DateTime] = @TickerTime

						IF (@TickerPriceHigh >= @StopLoss)
						BEGIN
							UPDATE @tempElderOrders
							SET StopLossHitTime = @TickerTime, PositionStatus = 'LOSS', ExitPrice = @TickerPriceHigh, ExitTime = @TickerTime, ProfitLoss = ROUND((@EntryPrice - @TickerPriceHigh) * @Quantity,1)
							WHERE TradingSymbol = @strTradingSymbol AND ExitPrice IS NULL
						END
						ELSE IF (@TickerPriceLow <= ROUND(@EntryPrice / @ProfitValue,1))
						BEGIN
							UPDATE @tempElderOrders
							SET PositionStatus = 'PROFIT', ExitPrice = @TickerPriceLow, ExitTime = @TickerTime, ProfitLoss = ROUND((@EntryPrice - @TickerPriceLow) * @Quantity,1) --ProfitLoss = 50 
							WHERE TradingSymbol = @strTradingSymbol AND ExitPrice IS NULL						
						END

						SET @TickerTime = DATEADD(minute,1,@TickerTime)
					END

					UPDATE @tempElderOrders
					SET ProfitLoss = ROUND((@EntryPrice - @TickerPriceClose) * @Quantity,1)
					WHERE TradingSymbol = @strTradingSymbol AND PositionStatus = 'OPEN'

				END
				END


			FETCH NEXT FROM Ticker_Cursor INTO @strTradingSymbol

			END

		CLOSE Ticker_Cursor
		DEALLOCATE Ticker_Cursor

		SET @EndTime = DATEADD(minute,@TimePeriod,@EndTime)

	END

	SET @TestStartPeriod = DATEADD(DAY,1,@TestStartPeriod)
END

--SELECT * FROM TickerMinElderIndicators WHERE StockCode = 'ABB' AND TickerDateTime = 'Feb  7 2018  9:25AM'
SELECT * FROM @tempElderOrders ORDER BY TradeDate, TRadingSymbol, EntryTime

