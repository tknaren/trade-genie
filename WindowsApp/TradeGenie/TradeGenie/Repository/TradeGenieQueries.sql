
--update MorningBreakout
--set StopLoss = null, StopLossHitTime = null
--where StopLoss is not null

--select cast(round((100000 / 315.10),0) as int)

BEGIN TRAN
update MorningBreakout
set Quantity = cast(round((100000 / Entry),0) as int)
where EntryTime is not null
COMMIT TRAN

begin tran
update MorningBreakout
set ProfitLoss = ((Quantity * [Entry]) - (Quantity * [Exit]) )
where ExitTime is not null and Movement = 'DOWN'
commit tran

begin tran
update MorningBreakout
set ProfitLoss = ((Quantity * [Exit]) - (Quantity * [Entry]) )
where ExitTime is not null and Movement = 'UP'
commit tran

begin tran
update MorningBreakout
set ProfitLoss = ((Quantity * [StopLoss]) - (Quantity * [Entry]) )
where StopLossHitTime is not null and Movement = 'UP'
commit tran

begin tran
update MorningBreakout
set ProfitLoss = ((Quantity * [Entry]) - (Quantity * [StopLoss]) )
where StopLossHitTime is not null and Movement = 'DOWN'
commit tran

select * from MorningBreakout 
where EntryTime is not null
order by ExitTime desc, StopLossHitTime desc

select 'Gain - ' + convert(varchar,sum(ProfitLoss))   from MorningBreakout where ExitTime is not null
Union all
select 'Loss - ' + convert(varchar,sum(ProfitLoss))   from MorningBreakout where StopLossHitTime is not null
Union all
select 'P/L - ' + convert(varchar,sum(ProfitLoss))   from MorningBreakout where EntryTime is not null


select * from [Order]
select * from Trade
select * from NetPosition
select * from Margin

Select TradingSymbol, TransactionType, AVG(AveragePrice) from [Trade] 
where OrderTimestamp >= '2017-12-22' 
group by TradingSymbol, TransactionType
order by TradingSymbol, TransactionType asc

------------------------------------------- Report Query --------------------------------------------------

select priceTable.InstrumentToken, priceTable.TradingSymbol, mbk.CandzleSize, mbk.Entry, priceTable.BUY as BuyPrice, mbk.[Exit], mbk.StopLoss, priceTable.SELL as SellPrice, 
	mbk.EntryTime, timeTable.BUY as BuyTime, mbk.ExitTime, mbk.StopLossHitTime,  timeTable.SELL as SellTime, priceTable.SELL - priceTable.BUY as PNL, 
	case when timeTable.BUY < timeTable.SELL then 'LONG' else 'SHORT' end AS 'Position'
from 
(
	select InstrumentToken, Tradingsymbol, [BUY], [SELL], [SELL] - [BUY] as PNL
	from (Select InstrumentToken, Tradingsymbol, TransactionType, AveragePrice from [Trade] where OrderTimestamp >= '2017-12-22') As SourceTable
	PIVOT (max(AveragePrice) FOR TransactionType IN ([BUY],[SELL])) AS PivotTable
) priceTable inner join 
(
	select InstrumentToken, [BUY], [SELL]
	from (Select InstrumentToken, TransactionType, OrderTimestamp from [Trade] where OrderTimestamp >= '2017-12-22') As SourceTable
	PIVOT (max(OrderTimestamp) FOR TransactionType IN ([BUY],[SELL])) AS PivotTable1
) timeTable on priceTable.InstrumentToken = timeTable.InstrumentToken
inner join MorningBreakout mbk on priceTable.InstrumentToken = mbk.InstrumentToken and DateTimePeriod >= '2017-12-22'
order by PNL asc

------------------------------------------- Report Query --------------------------------------------------

select * from OHLCData
where OHLCDateTime > '2017-12-25'
and LastPrice > 100 and LastPrice < 2000

select * from OHLCData
where OHLCDateTime > '2017-12-25'


select * from OHLCData
where OHLCDateTime > '2017-12-25'
and LastPrice > 1500 and LastPrice < 2000

select * from MorningBreakout
where DateTimePeriod > '2017-12-28'
--and EntryTime is not null
order by TradingSymbol 


update MorningBreakout
set Quantity = null, Entry = null, EntryTime = null, ExitTime = null, [Exit] = null, ProfitLoss = null, StopLoss = null, StopLossHitTime = null, Movement = null, isRealOrderPlaced = null
where DateTimePeriod > '2017-12-28'

update MorningBreakout
--set LTP = Round([Low] / 1.0004,1)
--set LTP = Round([Low] / 1.0005,1)
--set LTP = Round([Low] / 1.0006,1)
set LTP = Round([Low] / 1.001,1)
--set LTP = Round([Low] / 1.002,1)
--set LTP = Round([Low] / 1.0035,1)
--set LTP = Round([Low] / 1.004,1)
where DateTimePeriod > '2017-12-28'

update MorningBreakout
--set LTP = Round([High] * 1.0004,1)
--set LTP = Round([High] * 1.0005,1)
--set LTP = Round([High] * 1.0006,1)
set LTP = Round([High] * 1.001,1)
--set LTP = Round([High] * 1.002,1)
--set LTP = Round([High] * 1.003,1)
--set LTP = Round([High] * 1.004,1)
where DateTimePeriod > '2017-12-28'


update MorningBreakout
set DateTimePeriod = DATEADD(day, 1, DateTimePeriod)
where DateTimePeriod > '2017-12-26'


select * from [ORder]

--update [ORder]
--set OrderTimestamp = DATEADD(day, 4, OrderTimestamp)
--where OrderTimestamp >= '2017-12-22'

select * from [Order] where OrderTimestamp >= '2017-12-26'
select * from Trade where OrderTimestamp >= '2017-12-26'
select * from NetPosition where PositionDate = '2017-12-26'


select * from [Order] 
where OrderTimestamp >= '2017-12-29' and ParentOrderId is not null

select Instrument.TradingSymbol, Quote.* from Quote inner join Instrument on Quote.InstrumentToken = Instrument.InstrumentToken and Instrument.TradingSymbol = 'SBIN'
order by Quote.QuoteTime desc

select distinct Instrument.InstrumentToken, Instrument.TradingSymbol from Quote inner join Instrument on Quote.InstrumentToken = Instrument.InstrumentToken

select Instrument.TradingSymbol, Quote.* 
from Quote inner join Instrument on Quote.InstrumentToken = Instrument.InstrumentToken and Instrument.TradingSymbol = 'RELIANCE'
and Quote.QuoteTime >= '2017-12-29 15:00:00' and Quote.QuoteTime <= '2017-12-29 15:30:00'
order by Quote.QuoteTime asc


 DECLARE @TradingSymbols TABLE  
 (  
    Symbol varchar(20)
 )   
 --Insert data to Table variable @TStudent   
 INSERT INTO @TradingSymbols(Symbol)  
 SELECT DISTINCT TradingSymbol from Quote inner join Instrument on Quote.InstrumentToken = Instrument.InstrumentToken 

--select * from @TradingSymbols

declare @startTime datetime, @endTime datetime --, @tradingSymbol varchar(20)
set @startTime = '2018-01-01 09:10:00'
set @endTime = '2018-01-01 15:15:00'
--set @tradingSymbol = 'SBIN'

SELECT HighLow.TradingSymbol, [Open], High, Low, [Close] FROM 
	(
	select Instrument.TradingSymbol, Max(Quote.LastPrice) as High, Min(Quote.LastPrice) as Low 
	from Quote inner join Instrument on Quote.InstrumentToken = Instrument.InstrumentToken and Instrument.TradingSymbol in ('RELIANCE')--(select Symbol from @TradingSymbols)
	and Quote.QuoteTime >= @startTime and Quote.QuoteTime <= @endTime
	group by Instrument.TradingSymbol
	) HighLow inner join
	(
	select top 1 Instrument.TradingSymbol, FIRST_VALUE(Quote.LastPrice) OVER (ORDER BY Quote.QuoteTime ASC) AS [Open],
	FIRST_VALUE(Quote.LastPrice) OVER (ORDER BY Quote.QuoteTime DESC) AS [Close]
	from Quote inner join Instrument on Quote.InstrumentToken = Instrument.InstrumentToken and Instrument.TradingSymbol in ('RELIANCE')--(select Symbol from @TradingSymbols)
	and Quote.QuoteTime >= @startTime and Quote.QuoteTime <= @endTime
	) OpenClose on HighLow.TradingSymbol = OpenClose.TradingSymbol


select * from TickerMinElderIndicators

select StockCode, TickerDateTime, MACD, EMA3, EMA4, PriceClose, ForceIndex,
case when (EMA3 > EMA4) and (MACD > 1) then 'LONG' when (EMA3 < EMA4) and (MACD < 0 ) then 'SHORT' else '--' end AS 'EMA'
from TickerMinElderIndicators
where TimePeriod = 5 and StockCode = 'ASHOKLEY'

select StockCode, TickerDateTime, MACD, EMA3, EMA4, PriceClose, ForceIndex,
case when (EMA3 > EMA4) and (MACD > 0) then 'LONG' when (EMA3 < EMA4) and (MACD < 0 ) then 'SHORT' else '--' end AS 'EMA'
from TickerMinElderIndicators
where TimePeriod = 30 and StockCode = 'ASHOKLEY'


select * from Quote

--truncate table Quote

SELECT 
    MIN(TickerDateTime) AS MinuteBar15,
    Opening,
    MAX(PriceHigh) AS High,
    MIN(PriceLow) AS Low,
    Closing,
    Interval
FROM 
(
    SELECT FIRST_VALUE([PriceOpen]) OVER (PARTITION BY DATEDIFF(MINUTE, '2018-01-01 00:00:00', TickerDateTime) / 5 ORDER BY TickerDateTime) AS Opening,
           FIRST_VALUE([PriceClose]) OVER (PARTITION BY DATEDIFF(MINUTE, '2018-01-01 00:00:00', TickerDateTime) / 5 ORDER BY TickerDateTime DESC) AS Closing,
           DATEDIFF(MINUTE, '2018-01-01 00:00:00', TickerDateTime) / 5 AS Interval,
           *
    FROM TickerMinElderIndicators where TimePeriod = 15 and StockCode = 'ASHOKLEY' and TickerDateTime > '2018-01-01'
) AS T
GROUP BY Interval, Opening, Closing