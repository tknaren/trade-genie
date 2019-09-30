
--select count(1) from TickerMinElderIndicators
--where TickerDateTime > '2017-12-27' and TimePeriod = 5
--union
--select count(1) from TickerMinElderIndicators
--where TickerDateTime > '2017-12-27' and TimePeriod = 15
--union
--select count(1) from TickerMinElderIndicators
--where TickerDateTime > '2017-12-27' and TimePeriod = 30


--select * from TickerMinElderIndicators
--where TickerDateTime > '2017-12-27' and TimePeriod = 30


--select * from MasterStockList

--update MasterStockList
--set IsIncluded = 1
--where IsIncluded is null


--select StockCode, Max(PriceHigh) as [High], Min(PriceLow) as [Low] from TickerMinElderIndicators
--where StockCode in ('ABB','APOLLOHOSP','APOLLOTYRE','BAJFINANCE','BHEL','CIPLA','DABUR','GLAXO','KOTAKBANK','SIEMENS','TATACOMM','WIPRO','SRTRANSFIN','INDUSINDBK','STAR','GODREJCP','IGL','RCOM','EMAMILTD','DISHTV','COLPAL','NMDC','ADANIPOWER','IBULHSGFIN')
--and TimePeriod = 5 and TickerDateTime >= '2017-12-27 09:20:00' and TickerDateTime <= '2017-12-27 09:45:00'
--group by StockCode

--select * from TickerMinElderIndicators
--where StockCode in ('ABB','APOLLOHOSP','APOLLOTYRE','BAJFINANCE','BHEL','CIPLA','DABUR','GLAXO','KOTAKBANK','SIEMENS','TATACOMM','WIPRO','SRTRANSFIN','INDUSINDBK','STAR','GODREJCP','IGL','RCOM','EMAMILTD','DISHTV','COLPAL','NMDC','ADANIPOWER','IBULHSGFIN')
--and TimePeriod = 5 and TickerDateTime >= '2017-12-27 09:15:00' and TickerDateTime < '2017-12-27 09:30:00'

--select * from OHLCData
--where OHLCDateTime >= '2017-12-27'

----'ABB','ACC','ADANIENT','ADANIPORTS','AJANTPHARM','AMARAJABAT','AMBUJACEM','APOLLOHOSP','APOLLOTYRE','ARVIND','ASHOKLEY','ASIANPAINT','AUROPHARMA','AXISBANK','BAJFINANCE','BANKBARODA','BANKINDIA','BEL','BHARATFORG','BHARTIARTL','BIOCON','BPCL','CADILAHC','CANBK','CASTROLIND','CENTURYTEX','CESC','CIPLA','COALINDIA','CONCOR','CUMMINSIND','DABUR','DLF','EMAMILTD','ENGINERSIN','EXIDEIND','FEDERALBNK','GAIL','GLENMARK','GODREJCP','GODREJIND','HAVELLS','HCLTECH','HDFCBANK','HINDALCO','HINDPETRO','HINDUNILVR','HINDZINC','IBULHSGFIN','ICICIPRULI','IDEA','INDIGO','INDUSINDBK','INFRATEL','INFY','IOC','IRB','JINDALSTEL','JSWSTEEL','JUBLFOOD','LT','LUPIN','MINDTREE','MOTHERSUMI','MUTHOOTFIN','NMDC','NTPC','OIL','ONGC','PETRONET','PFC','PNB','POWERGRID','RECLTD','RELIANCE','RELINFRA','SBIN','SIEMENS','SRF','SRTRANSFIN','STAR','SUNPHARMA','SUNTV','TATACOMM','TATAGLOBAL','TATAMOTORS','TATAMTRDVR','TATASTEEL','TECHM','TITAN','TORNTPHARM','UBL','UNIONBANK','VEDL','VOLTAS','WIPRO','YESBANK','ZEEL'

--select * from TickerMinElderIndicators
--where StockCode in ('ABB','ACC','ADANIENT','ADANIPORTS','AJANTPHARM','AMARAJABAT','AMBUJACEM','APOLLOHOSP','APOLLOTYRE','ARVIND','ASHOKLEY','BATAINDIA','ASIANPAINT','AUROPHARMA','BHEL','AXISBANK','BRITANNIA','BAJFINANCE','BANKBARODA','BANKINDIA','BEL','BHARATFORG','BHARTIARTL','DRREDDY','EICHERMOT','BIOCON','GLAXO','BPCL','CADILAHC','HDFC','CANBK','HEROMOTOCO','CASTROLIND','CENTURYTEX','CESC','CIPLA','IDBI','COALINDIA','CONCOR','ITC','CUMMINSIND','KOTAKBANK','LICHSGFIN','BOSCHLTD','MRF','PEL','DABUR','PGHH','PIDILITIND','RELCAPITAL','DLF','SAIL','EMAMILTD','ENGINERSIN','SHREECEM','EXIDEIND','GSKCONS','FEDERALBNK','GAIL','TATACHEM','TATAPOWER','GLENMARK','GODREJCP','GODREJIND','HAVELLS','HCLTECH','HDFCBANK','HINDALCO','HINDPETRO','HINDUNILVR','MARICO','HINDZINC','IBULHSGFIN','ICICIPRULI','IDEA','INDIGO','INDUSINDBK','INFRATEL','ICICIBANK','INFY','IOC','IRB','JINDALSTEL','JSWSTEEL','JUBLFOOD','WOCKPHARMA','LT','LUPIN','TVSMOTOR','MINDTREE','MOTHERSUMI','MUTHOOTFIN','MCDOWELL-N','NMDC','NTPC','OFSS','OIL','ONGC','PETRONET','DIVISLAB','MARUTI','IDFCBANK','PFC','IGL','UPL','PNB','POWERGRID','RECLTD','ULTRACEMCO','TCS','RELIANCE','RELINFRA','SBIN','IDFC','RCOM','SIEMENS','SRF','GMRINFRA','SRTRANSFIN','STAR','SUNPHARMA','SUNTV','PAGEIND','DISHTV','TATACOMM','TATAGLOBAL','TATAMOTORS','COLPAL','RPOWER','TATAMTRDVR','TATASTEEL','TECHM','BAJAJ-AUTO','BAJAJFINSV','TITAN','TORNTPHARM','ADANIPOWER','NHPC','UBL','JSWENERGY','UNIONBANK','VEDL','VOLTAS','DALMIABHA','WIPRO','PCJEWELLER','YESBANK','ZEEL')
--and TimePeriod = 15 and TickerDateTime >= '2017-12-27 09:30:00' and TickerDateTime < '2017-12-27 09:35:00'


--select * from TickerMinElderIndicators
--where StockCode in ('ABB','ACC','ADANIENT','ADANIPORTS','AJANTPHARM','AMARAJABAT','AMBUJACEM','APOLLOHOSP','APOLLOTYRE','ARVIND','ASHOKLEY','BATAINDIA','ASIANPAINT','AUROPHARMA','BHEL','AXISBANK','BRITANNIA','BAJFINANCE','BANKBARODA','BANKINDIA','BEL','BHARATFORG','BHARTIARTL','DRREDDY','EICHERMOT','BIOCON','GLAXO','BPCL','CADILAHC','HDFC','CANBK','HEROMOTOCO','CASTROLIND','CENTURYTEX','CESC','CIPLA','IDBI','COALINDIA','CONCOR','ITC','CUMMINSIND','KOTAKBANK','LICHSGFIN','BOSCHLTD','MRF','PEL','DABUR','PGHH','PIDILITIND','RELCAPITAL','DLF','SAIL','EMAMILTD','ENGINERSIN','SHREECEM','EXIDEIND','GSKCONS','FEDERALBNK','GAIL','TATACHEM','TATAPOWER','GLENMARK','GODREJCP','GODREJIND','HAVELLS','HCLTECH','HDFCBANK','HINDALCO','HINDPETRO','HINDUNILVR','MARICO','HINDZINC','IBULHSGFIN','ICICIPRULI','IDEA','INDIGO','INDUSINDBK','INFRATEL','ICICIBANK','INFY','IOC','IRB','JINDALSTEL','JSWSTEEL','JUBLFOOD','WOCKPHARMA','LT','LUPIN','TVSMOTOR','MINDTREE','MOTHERSUMI','MUTHOOTFIN','MCDOWELL-N','NMDC','NTPC','OFSS','OIL','ONGC','PETRONET','DIVISLAB','MARUTI','IDFCBANK','PFC','IGL','UPL','PNB','POWERGRID','RECLTD','ULTRACEMCO','TCS','RELIANCE','RELINFRA','SBIN','IDFC','RCOM','SIEMENS','SRF','GMRINFRA','SRTRANSFIN','STAR','SUNPHARMA','SUNTV','PAGEIND','DISHTV','TATACOMM','TATAGLOBAL','TATAMOTORS','COLPAL','RPOWER','TATAMTRDVR','TATASTEEL','TECHM','BAJAJ-AUTO','BAJAJFINSV','TITAN','TORNTPHARM','ADANIPOWER','NHPC','UBL','JSWENERGY','UNIONBANK','VEDL','VOLTAS','DALMIABHA','WIPRO','PCJEWELLER','YESBANK','ZEEL')
--and TimePeriod = 30 and TickerDateTime >= '2017-12-27 09:45:00' and TickerDateTime <= '2017-12-27 10:15:00'


--select StockCode, Max(PriceHigh) 'PriceHigh', Min(PriceLow) 'PriceLow' from TickerMinElderIndicators
--where StockCode in ('ADANIENT','AJANTPHARM','APOLLOTYRE','ARVIND','ASHOKLEY','AXISBANK','BANKBARODA','BANKINDIA','BIOCON','BPCL','CADILAHC','CANBK','CESC','CIPLA','CONCOR','CUMMINSIND','DLF','GLENMARK','GODREJIND','HINDUNILVR','ICICIPRULI','INDIGO','INFY','IRB','JINDALSTEL','JSWSTEEL','MINDTREE','MUTHOOTFIN','NMDC','NTPC','PETRONET','PNB','POWERGRID','RECLTD','RELINFRA','SRF','SUNPHARMA','SUNTV','TATACOMM','TORNTPHARM','UBL','UNIONBANK','VOLTAS')
--and TimePeriod = 30 and TickerDateTime >= '2017-12-27 09:45:00' and TickerDateTime <= '2017-12-27 10:15:00'
--group by StockCode


--select count(1) from TickerMinElderIndicators
--where TimePeriod = 5 and TickerDateTime > '2017-12-29'
--union
--select count(1) from TickerMinElderIndicators
--where TimePeriod = 15 and TickerDateTime > '2017-12-29' 
--union
--select count(1) from TickerMinElderIndicators
--where TimePeriod = 30 and TickerDateTime > '2017-12-29' 
--union
--select count(1) from TickerMinElderIndicators
--where TimePeriod = 60 and TickerDateTime > '2017-12-29'

--DECLARE @StockCode varchar(25)
--SET @StockCode = 'ASHOKLEY'
--select  TimePeriod, StockCode, TickerDateTime, PriceClose,  EMA3, EMA4, Impulse, Round((PriceClose - EMA4),2) as 'Var EMA CLose',
--case 
--	when EMA3 = 0 then '--' 
--	when (PriceClose > EMA3) and (Round((PriceClose - EMA4),2) > 0.4) then 'LONG' 
--	when  (PriceClose < EMA3) and (Round((PriceClose - EMA4),2) < -0.4) then 'SHORT' 
--	else '--' end as 'Position'
--from TickerMinElderIndicators
--where StockCode = @StockCode and TimePeriod = 5 and TickerDateTime > '2017-12-28'
------union
------select TimePeriod, StockCode, TickerDateTime, EMA3, EMA4, Impulse from TickerMinElderIndicators
------where StockCode = @StockCode and TimePeriod = 30 and TickerDateTime > '2017-12-28'
----order by TimePeriod, TickerDateTime

--DECLARE @StockCode varchar(25)
--SET @StockCode = 'ASHOKLEY'
--select  TimePeriod, StockCode, TickerDateTime, PriceClose,  EMA3, EMA4, Impulse, Round((PriceClose - EMA4),2) as 'Var EMA CLose',
--case 
--	when EMA3 = 0 then '--' 
--	when (PriceClose > EMA3) and (Round((PriceClose - EMA4),2) > 0.3) then 'LONG' 
--	when  (PriceClose < EMA3) and (Round((PriceClose - EMA4),2) < -0.3) then 'SHORT' 
--	else '--' end as 'Position'
--from TickerMinElderIndicators
--where StockCode = @StockCode and TimePeriod = 15 and TickerDateTime > '2017-12-28'


--DECLARE @StockCode varchar(25)
--SET @StockCode = 'ASHOKLEY'
--select  TimePeriod, StockCode, TickerDateTime, PriceClose,  EMA3, EMA4, Impulse, Round((PriceClose - EMA4),2) as 'Var EMA CLose',
--case 
--	when EMA3 = 0 then '--' 
--	when (PriceClose > EMA3) and (Round((PriceClose - EMA4),2) > 0.2) then 'LONG' 
--	when  (PriceClose < EMA3) and (Round((PriceClose - EMA4),2) < -0.2) then 'SHORT' 
--	else '--' end as 'Position'
--from TickerMinElderIndicators
--where StockCode = @StockCode and TimePeriod = 30 and TickerDateTime > '2017-12-28'

--select * from TickerMinElderIndicators
--where TimePeriod = 60 

--select InstrumentToken, TradingSymbol, Instrument.Name, mast.Collection, mast.IsIncluded from Instrument
--join [FinancialMarket].[dbo].[MasterStockList] mast on  Instrument.TradingSymbol = mast.Code 
--where TradingSymbol in ('ABB','ACC','ADANIENT','ADANIPORTS','ADANIPOWER','AJANTPHARM','AMARAJABAT','AMBUJACEM','APOLLOHOSP','APOLLOTYRE','ARVIND','ASHOKLEY','ASIANPAINT','AUROPHARMA','AXISBANK','BAJAJ-AUTO','BAJAJFINSV','BAJFINANCE','BANKBARODA','BANKINDIA','BATAINDIA','BEL','BHARATFORG','BHARTIARTL','BHEL','BIOCON','BOSCHLTD','BPCL','BRITANNIA','CADILAHC','CANBK','CASTROLIND','CENTURYTEX','CESC','CIPLA','COALINDIA','COLPAL','CONCOR','CUMMINSIND','DABUR','DALMIABHA','DISHTV','DIVISLAB','DLF','DRREDDY','EICHERMOT','EMAMILTD','ENGINERSIN','EXIDEIND','FEDERALBNK','GAIL','GLAXO','GLENMARK','GMRINFRA','GODREJCP','GODREJIND','GSKCONS','HAVELLS','HCLTECH','HDFC','HDFCBANK','HEROMOTOCO','HINDALCO','HINDPETRO','HINDUNILVR','HINDZINC','IBULHSGFIN','ICICIBANK','ICICIPRULI','IDBI','IDEA','IDFC','IDFCBANK','IGL','INDIGO','INDUSINDBK','INFRATEL','INFY','IOC','IRB','ITC','JINDALSTEL','JSWENERGY','JSWSTEEL','JUBLFOOD','KOTAKBANK','LICHSGFIN','LT','LUPIN','MARICO','MARUTI','MCDOWELL-N','MINDTREE','MOTHERSUMI','MRF','MUTHOOTFIN','NHPC','NMDC','NTPC','OFSS','OIL','ONGC','PAGEIND','PCJEWELLER','PEL','PETRONET','PFC','PGHH','PIDILITIND','PNB','POWERGRID','RCOM','RECLTD','RELCAPITAL','RELIANCE','RELINFRA','RPOWER','SAIL','SBIN','SHREECEM','SIEMENS','SRF','SRTRANSFIN','STAR','SUNPHARMA','SUNTV','TATACHEM','TATACOMM','TATAGLOBAL','TATAMOTORS','TATAMTRDVR','TATAPOWER','TATASTEEL','TCS','TECHM','TITAN','TORNTPHARM','TVSMOTOR','UBL','ULTRACEMCO','UNIONBANK','UPL','VEDL','VOLTAS','WIPRO','WOCKPHARMA','YESBANK','ZEEL')

--select * from TickerMin 
--where [DateTime] >= '2018-01-10'

--select * from TickerMinElderIndicators where [TickerDateTime] >= '2018-01-10'

--select * from TickerMin		

--sp_help TickerMin	

--select * from MasterStockList

--update MasterStockList
--set IsIncluded = 1
--where TradingSymbol not like 'A%'

--select top 10 * from TickerMin

----truncate table TickerMin
----truncate table TickerMinElderIndicators
----truncate table TickerMinSuperTrend


--select TradingSymbol, count(InstrumentToken) from TickerMin
--group by TradingSymbol
----having count(InstrumentToken) < 375
--order by count(InstrumentToken) asc

--select getdate()
--exec spGenerateOHLC '2018-01-01 09:15:00', 30
--select getdate()

--select * from TickerMinElderIndicators where TimePeriod = 30

--select count(*) from TickerMinElderIndicators 
--where TimePeriod = 30 and StockCode = 'ABB' and TickerDateTime > '2018-01-12'

--select count(*) from TickerMinElderIndicators 
--where TimePeriod = 30 and StockCode = 'ABB' 

--select * from ErrorTable

--exec spGetTickerDataForIndicators 'ABB,ACC,ASHOKLEY,ASIANPAINT,AUROPHARMA,BAJFINANCE,BEL,BHEL,BPCL,BRITANNIA,CIPLA,DABUR,DRREDDY,EICHERMOT,GLAXO,AMBUJACEM,HDFC,HDFCBANK,HEROMOTOCO,HINDALCO,HINDUNILVR,HINDPETRO,HINDZINC,INFY,IOC,ITC,CUMMINSIND,KOTAKBANK,LICHSGFIN,BOSCHLTD,PEL,ONGC,PGHH,PIDILITIND,RELIANCE,SAIL,SBIN,VEDL,SHREECEM,SIEMENS,GSKCONS,SUNPHARMA,TATAPOWER,TATAMOTORS,TATASTEEL,TITAN,TORNTPHARM,WIPRO,ZEEL,MARICO,MOTHERSUMI,SRTRANSFIN,BANKBARODA,GAIL,CONCOR,ICICIBANK,INDUSINDBK,AXISBANK,HCLTECH,GLENMARK,CADILAHC,HAVELLS,GODREJCP,LUPIN,MCDOWELL-N,BHARTIARTL,PNB,OFSS,DIVISLAB,MARUTI,INDIGO,UPL,PETRONET,LT,ULTRACEMCO,TCS,NTPC,JSWSTEEL,YESBANK,SUNTV,EMAMILTD,TECHM,PFC,IDEA,DLF,POWERGRID,ADANIPORTS,COLPAL,NMDC,RECLTD,BAJAJ-AUTO,BAJAJFINSV,UBL,TATAMTRDVR,NHPC,OIL,ICICIPRULI,COALINDIA,INFRATEL,IBULHSGFIN','5,15,30,60'

--select * from TickerRealTimeFO
--select * from TickerRealTime

--sp_helptext spGetTickerDataForIndicators

--select StockCode,TickerDateTime,TimePeriod,PriceOpen,PriceHigh,PriceLow,PriceClose,Volume,MACD,Signal,Histogram,HistIncDec,Impulse,ForceIndex1,EMA1Dev,EMA2Dev
--from TickerMinElderIndicators
--where TimePeriod = 5 and TickerDateTime between '2018-01-04' and '2018-01-05'

--select * from MasterStockList
----truncate table TickerMinElderIndicators

--sp_help TickerMinElderIndicators

--update TickerMinElderIndicators
--set 
--	[EMA1] = NULL,
--	[EMA2] = NULL,
--	[EMA3] =  NULL,
--	[EMA4] = NULL,
--	[MACD] = NULL,
--	[Signal] = NULL,
--	[Histogram] = NULL,
--	[HistIncDec] =  NULL,
--	[Impulse] = NULL,
--	[ForceIndex1] = NULL,
--	[ForceIndex2] = NULL,
--	[EMA1Dev] = NULL,
--	[EMA2Dev] = NULL,
--	[AG1] = NULL,
--	[AL1] = NULL,
--	[RSI1] = NULL,
--	[AG2] = NULL,
--	[AL2] = NULL,
--	[RSI2] = NULL

--select * from ErrorTable

--exec spGenerateOHLC '01-01-2018 09:15',5
--exec spGenerateOHLC '01-01-2018 09:15',30

--exec spGetTickerElderForTimePeriod 'RELIANCE,INDIGO,HCLTECH','30','01-04-2018 09:15', '01-04-2018 09:40'


--DECLARE @InstrumentList VARCHAR(2000), @TimePeriods VARCHAR(25)
--SET @InstrumentList = 'RELIANCE,INDIGO,HCLTECH'
--SET @TimePeriods = '5'

--SELECT StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, EMA1, EMA2, EMA3, EMA4, 
--		MACD, Signal, Histogram, HistIncDec, Impulse, ForceIndex1, ForceIndex2, EMA1Dev, EMA2Dev, AG1, AL1, RSI1, AG2, AL2, RSI2
--FROM (SELECT *,
--			ROW_NUMBER() OVER(partition BY StockCode,TimePeriod ORDER BY TickerDateTime DESC) AS RankValue
--			FROM TickerMinElderIndicators 
--			where StockCode IN (SELECT * FROM ufn_CSVToTable(@InstrumentList,','))  
--			AND TimePeriod IN (SELECT * FROM ufn_CSVToTable(@TimePeriods,','))
--			AND EMA1 IS NOT NULL AND TickerDateTime BETWEEN '2018-01-25 09:15' AND '2018-01-25 15:30'
--		) i
--WHERE RankValue IN (1,2,3)
--ORDER BY StockCode, TickerDateTime

--select * from OHLCData
--select * from Quote

--select * from Instrument
--select * from MasterStockList
--select * from NetPosition

--sp_help ElderBackTesting


--select * from ElderBackTesting

----truncate table ElderBackTesting

----update MasterStockList
----set IsIncluded = 0
----where Collection in ('Nifty Next 50','Nifty Midcap 50')

--select * from OHLCData

--select top 100 * from TickerMin(nolock)
--order by [DateTime] desc

--select top 10 * from TickerMin(nolock)
--order by [DateTime] desc

--select top 10 * from TickerMinElderIndicators
--where Timeperiod = 30 and Impulse = 'B' and HistIncDec = 'I' and Histogram > 0 and MACD < 0 and Signal < 0
--order by TickerDateTime desc

--select top 100 * from TickerMinElderIndicators
--where Timeperiod = 30 and StockCode in ('ACC','ASIANPAINT','AUROPHARMA','BANKBARODA','BHARTIARTL','BOSCHLTD','BPCL','CIPLA','DRREDDY','EICHERMOT','GAIL','HINDALCO','HINDUNILVR','INFRATEL','LUPIN','MARUTI','NTPC','POWERGRID','TATAMOTORS','TATAMTRDVR','TATAPOWER','TATASTEEL')
--order by StockCode, TickerDateTime desc

--select * from ElderBackTesting 

--select * from OHLCData
--where TradingSymbol in ('ACC','ASIANPAINT','AUROPHARMA','BANKBARODA','BHARTIARTL','BOSCHLTD','BPCL','CIPLA','DRREDDY','EICHERMOT','GAIL','HINDALCO','HINDUNILVR','INFRATEL','LUPIN','MARUTI','NTPC','POWERGRID','TATAMOTORS','TATAMTRDVR','TATAPOWER','TATASTEEL')
--and OHLCDate >= convert(date, getdate())


----truncate table ElderBackTesting
--sp_helptext spGetTickerElderForTimePeriod
--exec spGetTickerElderForTimePeriod 'ACC,ASIANPAINT,AUROPHARMA,BANKBARODA,BHARTIARTL,BOSCHLTD,BPCL,CIPLA,DRREDDY,EICHERMOT,GAIL,HINDALCO,HINDUNILVR,INFRATEL,LUPIN,MARUTI,NTPC,POWERGRID,TATAMOTORS,TATAMTRDVR,TATAPOWER,TATASTEEL','30','2018-02-01 09:15','2018-02-01 11:45'

--select top 100 * from TickerMinElderIndicators
--where Timeperiod = 15 and StockCode in ('ACC','AUROPHARMA','HEROMOTOCO','HINDALCO','ITC') and TickerDateTime > Convert(date,getdate())
--order by StockCode, TickerDateTime desc

--select * from ElderStrategyORders

--select * from Orders
--select * from Margins

--sp_help ElderStrategyORders

--exec spGetTickerElderForTimePeriod 'ACC,ASIANPAINT,AUROPHARMA,BPCL,CIPLA,DRREDDY,EICHERMOT,AMBUJACEM,HDFC,HDFCBANK,HEROMOTOCO,HINDALCO,HINDUNILVR,INFY,IOC,ITC,KOTAKBANK,BOSCHLTD,ONGC,RELIANCE,SBIN,VEDL,SUNPHARMA,TATAPOWER,TATAMOTORS,TATASTEEL,WIPRO,ZEEL,BANKBARODA,GAIL,ICICIBANK,INDUSINDBK,AXISBANK,HCLTECH,LUPIN,BHARTIARTL,MARUTI,LT,ULTRACEMCO,TCS,NTPC,YESBANK,TECHM,POWERGRID,ADANIPORTS,BAJAJ-AUTO,TATAMTRDVR,COALINDIA,INFRATEL,IBULHSGFIN', '15','2018-02-05 09:15','2018-02-05 10:00'

--select top 10 * from TickerMinElderIndicators
--order by TickerDateTime desc

--exec spGetTickerDataForIndicators 'ACC,ASIANPAINT,AUROPHARMA,BPCL,CIPLA,DRREDDY,EICHERMOT,AMBUJACEM,HDFC,HDFCBANK,HEROMOTOCO,HINDALCO,HINDUNILVR,INFY,IOC,ITC,KOTAKBANK,BOSCHLTD,ONGC,RELIANCE,SBIN,VEDL,SUNPHARMA,TATAPOWER,TATAMOTORS,TATASTEEL,WIPRO,ZEEL,BANKBARODA,GAIL,ICICIBANK,INDUSINDBK,AXISBANK,HCLTECH,LUPIN,BHARTIARTL,MARUTI,LT,ULTRACEMCO,TCS,NTPC,YESBANK,TECHM,POWERGRID,ADANIPORTS,BAJAJ-AUTO,TATAMTRDVR,COALINDIA,INFRATEL,IBULHSGFIN', '15'
--exec spGetTickerElderForTimePeriod 'ACC,ASIANPAINT,AUROPHARMA,BPCL,CIPLA,DRREDDY,EICHERMOT,AMBUJACEM,HDFC,HDFCBANK,HEROMOTOCO,HINDALCO,HINDUNILVR,INFY,IOC,ITC,KOTAKBANK,BOSCHLTD,ONGC,RELIANCE,SBIN,VEDL,SUNPHARMA,TATAPOWER,TATAMOTORS,TATASTEEL,WIPRO,ZEEL,BANKBARODA,GAIL,ICICIBANK,INDUSINDBK,AXISBANK,HCLTECH,LUPIN,BHARTIARTL,MARUTI,LT,ULTRACEMCO,TCS,NTPC,YESBANK,TECHM,POWERGRID,ADANIPORTS,BAJAJ-AUTO,TATAMTRDVR,COALINDIA,INFRATEL,IBULHSGFIN', '15'

--select top 10 * from TickerMinElderIndicators
--order by TickerDateTime desc

--exec spGenerateOHLC '2018-03-28 09:15',3
--exec spGenerateOHLC '2018-03-28 09:15',5
--exec spGenerateOHLC '2018-03-28 09:15',10
--exec spGenerateOHLC '2018-03-28 09:15',15
--exec spGenerateOHLC '2018-03-28 09:15',30
--exec spGenerateOHLC '2018-03-28 09:15',60

--sp_help spGenerateOHLC

--select top 10 * from TickerMin
--order by [DAteTime] desc

--select * from MasterStockList
--where IsIncluded = 1

--update MasterStockList
--set IsIncluded = 0
--where Collection in ('Nifty Next 50','Nifty Midcap 50') 

--select count(1) from TickerMin
--where dateTime < '2018-01-25' 

--delete from TickerMin
--where dateTime < '2018-01-25' 


--select * from MasterStockList
--where IsIncluded = 1

--update MasterStockList
--set IsIncluded = 1
--where TradingSymbol in (
--	select StockCode from TickerMinElderIndicators
--	where TickerDateTime = '2018-01-01 09:15:00.000' and TimePeriod = 5
--	and PriceClose < 500 and PriceClose > 100 )


--select count(1) from TickerMin
--order by [dateTime] desc

--select * from TickerMinElderIndicators
--where TimePeriod = 30 and StockCode = 'ADANIENT'
--order by TickerDateTime desc


--select min([DateTime]) from TickerMin
--select max([DateTime]) from TickerMin

--select tRADINGSYMBOL, COUNT(1) from TickerMin
--GROUP BY tRADINGSYMBOL

--select convert(date, [DateTime]),  COUNT(1) from TickerMin
--group by convert(date, [DateTime])
--order by convert(date, [DateTime]) asc

--select * from TickerMin
--where TradingSymbol = 'DABUR' and convert(date, [DateTime]) = '2018-02-07'


----truncate table TickerMin
----truncate table TickerMinElderIndicators


--select min(TickerDateTime) from TickerMinElderIndicators where TimePeriod = 60
--select max(TickerDateTime) from TickerMinElderIndicators --where TimePeriod = 375

--select min([DateTime]) from TickerMin 
--select max([DateTime]) from TickerMin

--select * from [dbo].[TickerMinEMAHA]

----delete from TickerMinElderIndicators
----where TickerDateTime < '2018-04-01'
----delete from TickerMinSuperTrend
----where TickerDateTime < '2018-04-01'


--select distinct convert(date,[DateTime]) from TickerMin
--select distinct convert(date,TickerDateTime) from TickerMinElderIndicators


--select * from ElderStrategyOrders
--where TradeDate = Convert(date, getdate())
--order by EntryTime

--update ElderStrategyOrders
--set ExitPrice = 201.50
--where TradeDate = Convert(date, getdate())
--and OrderId = '180220000444742'

--select * from OHLCData
--order by LastUpdatedTime desc

--select * from Orders

--SELECT * FROM TickerMinElderIndicators
--WHERE StockCode = 'FEDERALBNK' AND TimePeriod = 5
--ORDER BY TickerDateTime DESC

--SET TRANSACTION ISOLATION LEVEL READ COMMITTED

--SELECT * FROM orDERS 
--WHERE convert(date, OrderTimestamp) = convert(date,getDate()) 
--and Variety = 'co'

--select * from Trades where OrderTimestamp >= convert(date, getdate())
--select * from NetPositions where PositionDate = convert(date, getdate())
--select * from Margins where MarginDate = convert(date, getdate())


--declare @fromDateTime datetime, @tradingSymbol varchar(20)
--set @fromDateTime = '2018-03-18 09:30:00'
--set @tradingSymbol = 'PNB'
--select TOP 15 * from TickerMinElderIndicators
--where convert(date, TickerDateTime) = convert(date, @fromDateTime) and TimePeriod = 15 and StockCode = @tradingSymbol AND TickerDateTime >= CONVERT(DATETIME,@fromDateTime)
--select TOP 250 * from TickerMinElderIndicators
--where convert(date, TickerDateTime) = convert(date, @fromDateTime) and TimePeriod = 5 and StockCode = @tradingSymbol AND TickerDateTime >= CONVERT(DATETIME,@fromDateTime)


----delete from TickerMin
----where convert(date, [DateTime]) < '2018-03-05'
----delete from TickerMinElderIndicators
----where convert(date, TickerDateTime) < '2018-03-05'

--select InstrumentToken, TradingSymbol from Instruments
--where TradingSymbol in ('3MINDIA','8KMILES','AARTIIND','ABAN','ABB','ABFRL','ACC','ADANIENT','ADANIPORTS','ADANIPOWER','ADANITRANS','ADVENZYMES','AEGISCHEM','AHLUCONT','AIAENG','AJANTPHARM','AKZOINDIA','ALBK','ALKEM','ALLCARGO','AMARAJABAT','AMBUJACEM','ANDHRABANK','APARINDS','APLAPOLLO','APLLTD','APOLLOHOSP','APOLLOTYRE','ARVIND','ASHOKA','ASHOKLEY','ASIANPAINT','ASTRAL','ASTRAZEN','ATUL','AUBANK','AUROPHARMA','AVANTIFEED','AXISBANK','BAJAJ-AUTO','BAJAJCORP','BAJAJELEC','BAJAJFINSV','BAJAJHIND','BAJAJHLDNG','BAJFINANCE','BALKRISIND','BALLARPUR','BALMLAWRIE','BALRAMCHIN','BANKBARODA','BANKINDIA','BASF','BATAINDIA','BBTC','BEL','BEML','BERGEPAINT','BFUTILITIE','BGRENERGY','BHARATFIN','BHARATFORG','BHARTIARTL','BHEL','BHUSANSTL','BIOCON','BIRLACORPN','BLISSGVS','BLUEDART','BLUESTARCO','BOMDYEING','BOSCHLTD','BPCL','BRFL','BRIGADE','BRITANNIA','BSE','CADILAHC','CANBK','CANFINHOME','CAPF','CAPLIPOINT','CARBORUNIV','CARERATING','CASTROLIND','CCL','CEATLTD','CENTRALBK','CENTURYPLY','CENTURYTEX','CERA','CGPOWER','CHAMBLFERT','CHENNPETRO','CHOLAFIN','CIPLA','COALINDIA','COCHINSHIP','COFFEEDAY','COLPAL','CONCOR','COROMANDEL','CORPBANK','COX&KINGS','CRISIL','CROMPTON','CUB','CUMMINSIND','CYIENT','DABUR','DALMIABHA','DBCORP','DBL','DBREALTY','DCBBANK','DCMSHRIRAM','DEEPAKFERT','DELTACORP','DEN','DENABANK','DHANUKA','DHFL','DISHTV','DIVISLAB','DLF','DMART','DREDGECORP','DRREDDY','ECLERX','EDELWEISS','EICHERMOT','EIDPARRY','EIHOTEL','EMAMILTD','ENDURANCE','ENGINERSIN','EQUITAS','ERIS','EROSMEDIA','ESCORTS','ESSELPACK','EVEREADY','EXIDEIND','FCONSUMER','FEDERALBNK','FINCABLES','FINPIPE','FLFL','FSL','GAIL','GATI','GDL','GEPIL','GESHIP','GET&D','GHCL','GILLETTE','GLAXO','GLENMARK','GMDCLTD','GMRINFRA','GNFC','GODFRYPHLP','GODREJCP','GODREJIND','GODREJPROP','GPPL','GRANULES','GREAVESCOT','GREENPLY','GRUH','GSFC','GSKCONS','GSPL','GUJALKALI','GUJFLUORO','GUJGASLTD','GULFOILLUB','GVKPIL','HATHWAY','HATSUN','HAVELLS','HCC','HCL-INSYS','HCLTECH','HDFC','HDFCBANK','HDIL','HEIDELBERG','HERITGFOOD','HEROMOTOCO','HEXAWARE','HFCL','HIMATSEIDE','HINDALCO','HINDCOPPER','HINDPETRO','HINDUNILVR','HINDZINC','HSIL','HTMEDIA','HUDCO','IBREALEST','IBULHSGFIN','IBVENTURES','ICICIBANK','ICICIPRULI','ICIL','ICRA','IDBI','IDEA','IDFC','IDFCBANK','IFBIND','IFCI','IGARASHI','IGL','IL&FSTRANS','INDHOTEL','INDIACEM','INDIANB','INDIGO','INDOCO','INDUSINDBK','INFIBEAM','INFRATEL','INFY','INGERRAND','INOXLEISUR','INOXWIND','INTELLECT','IOB','IOC','IPCALAB','IRB','ITC','ITDC','ITDCEM','ITI','J&KBANK','JAGRAN','JAICORPLTD','JBCHEPHARM','JBFIND','JCHAC','JETAIRWAYS','JINDALPOLY','JINDALSAW','JINDALSTEL','JISLJALEQS','JKCEMENT','JKIL','JKLAKSHMI','JKTYRE','JMFINANCIL','JPASSOCIAT','JPPOWER','JSL','JSLHISAR','JSWENERGY','JSWSTEEL','JUBILANT','JUBLFOOD','JUSTDIAL','JYOTHYLAB','KAJARIACER','KALPATPOWR','KANSAINER','KARURVYSYA','KEC','KESORAMIND','KITEX','KOLTEPATIL','KOTAKBANK','KPIT','KPRMILL','KRBL','KSCL','KTKBANK','KWALITY','L&TFH','LAKSHVILAS','LALPATHLAB','LAURUSLABS','LAXMIMACH','LICHSGFIN','LINDEINDIA','LT','LTI','LTTS','LUPIN','M&M','M&MFIN','MAGMA','MAHINDCIE','MANAPPURAM','MANPASAND','MARICO','MARKSANS','MARUTI','MAXINDIA','MCDOWELL-N','MCLEODRUSS','MERCK','MFSL','MGL','MHRIL','MINDACORP','MINDAIND','MINDTREE','MMTC','MOIL','MONSANTO','MOTHERSUMI','MOTILALOFS','MPHASIS','MRF','MRPL','MTNL','MUTHOOTFIN','NATCOPHARM','NATIONALUM','NAUKRI','NAVINFLUOR','NAVKARCORP','NAVNETEDUL','NBCC','NBVENTURES','NCC','NETWORK18','NFL','NH','NHPC','NIITTECH','NILKAMAL','NLCINDIA','NMDC','NTPC','OBEROIRLTY','OFSS','OIL','OMAXE','ONGC','ORIENTBANK','ORIENTCEM','PAGEIND','PARAGMILK','PCJEWELLER','PEL','PERSISTENT','PETRONET','PFC','PFIZER','PFS','PGHH','PHOENIXLTD','PIDILITIND','PIIND','PNB','PNBHOUSING','PNCINFRA','POWERGRID','PRAJIND','PRESTIGE','PRISMCEM','PTC','PVR','QUESS','RADICO','RAIN','RAJESHEXPO','RALLIS','RAMCOCEM','RAMCOSYS','RAYMOND','RBLBANK','RCF','RCOM','RECLTD','REDINGTON','RELAXO','RELCAPITAL','RELIANCE','RELIGARE','RELINFRA','RENUKA','REPCOHOME','RKFORGE','RNAVAL','RPOWER','RTNPOWER','RUPA','SADBHAV','SAIL','SANOFI','SBIN','SCHAEFFLER','SCHNEIDER','SCI','SFL','SHARDACROP','SHILPAMED','SHK','SHOPERSTOP','SHREECEM','SHRIRAMCIT','SIEMENS','SJVN','SKFINDIA','SMLISUZU','SNOWMAN','SOBHA','SOLARINDS','SOMANYCERA','SONATSOFTW','SOUTHBANK','SPARC','SREINFRA','SRF','SRTRANSFIN','STARCEMENT','STRTECH','SUDARSCHEM','SUNDRMFAST','SUNPHARMA','SUNTECK','SUNTV','SUPPETRO','SUPRAJIT','SUPREMEIND','SUVEN','SUZLON','SWANENERGY','SYMPHONY','SYNDIBANK','SYNGENE','TAKE','TATACHEM','TATACOFFEE','TATACOMM','TATAELXSI','TATAGLOBAL','TATAINVEST','TATAMOTORS','TATAMTRDVR','TATAPOWER','TATASPONGE','TATASTEEL','TCS','TECHM','TECHNO','TEXRAIL','THERMAX','THOMASCOOK','THYROCARE','TIFIN','TIMETECHNO','TIMKEN','TITAN','TNPL','TORNTPHARM','TORNTPOWER','TRENT','TRIDENT','TTKPRESTIG','TV18BRDCST','TVSMOTOR','TVSSRICHAK','TVTODAY','UBL','UCOBANK','UFLEX','UJJIVAN','ULTRACEMCO','UNICHEMLAB','UNIONBANK','UNITECH','UNITEDBNK','UPL','VAKRANGEE','VBL','VEDL','VESUVIUS','VGUARD','VIJAYABANK','VINATIORGA','VIPIND','VOLTAS','VRLLOG','VTL','WABAG','WABCOINDIA','WELCORP','WELSPUNIND','WHIRLPOOL','WIPRO','WOCKPHARMA','YESBANK','ZEEL','ZEELEARN','ZENSARTECH')
--order by TradingSymbol asc

--select * from MasterStockList

----truncate table TickerMin
----truncate table TickerMinElderIndicators

--select * from TickerMin
--select * from TickerMinElderIndicators
--where TimePeriod = 60


--exec spGenerateOHLC '2018-02-12 09:15',30
--exec spGenerateOHLC '2018-02-12 09:15',60
--exec spGenerateOHLC '2018-02-12 09:15',25


--select MSL.Collection, MSL.Industry, TEL.* from  TickerMinElderIndicators TEL
--inner join MasterStockList MSL on TEL.StockCode = MSL.TradingSymbol
--where TimePeriod = 375 and convert(date,TickerDateTime) > convert(date, getdate()-3) 
--and MSL.Collection = 'Nifty 500'
--and Impulse = 'G' 
--and PriceClose > EMA2 and PriceClose > EMA4 
--and EMA1Dev > 2 



--select * from MasterStockList
--where Collection = 'Nifty 500'


--truncate table TickerMinElderIndicators
----truncate table TickerMin
--truncate table TickerMinSuperTrend

--select * from TickerMin
--select * from TickerMinElderIndicators
--select * from MasterStockList

----update MasterStockList
----set IsIncluded = 1
----where Collection in ('Nifty Next 50', 'Nifty Midcap 50')

--exec spGenerateOHLC '2018-04-02 09:15',3
--exec spGenerateOHLC '2018-04-02 09:15',5
--exec spGenerateOHLC '2018-04-02 09:15',10
--exec spGenerateOHLC '2018-04-02 09:15',15
--exec spGenerateOHLC '2018-04-02 09:15',30
--exec spGenerateOHLC '2018-04-02 09:15',60
--exec spGenerateOHLC '2018-04-02 09:15',375

--sp_helptext spGenerateOHLC

----delete from TickerMinElderIndicators
----where timeperiod = 375
----delete from TickerMinSuperTrend
----where timeperiod = 375



--select * from TickerMinElderIndicators
--where TickerDateTime >= '2018-04-02 09:15'

--select * from TickerMin where [DateTime] >= '2018-04-17'
--and TradingSymbol = 'HINDCOPPER'

--select * from MasterStockList
--where TradingSymbol = 'MERCK'

--update TickerMinElderIndicators
--set [EMA1] = NULL,[EMA2] = NULL,[EMA3] =  NULL,[EMA4] = NULL,[MACD] = NULL,[Signal] = NULL,[Histogram] = NULL,[HistIncDec] =  NULL,[Impulse] = NULL,
--	[ForceIndex1] = NULL,[ForceIndex2] = NULL,[EMA1Dev] = NULL,[EMA2Dev] = NULL,[AG1] = NULL,[AL1] = NULL,[RSI1] = NULL,[AG2] = NULL,[AL2] = NULL, [RSI2] = NULL
--WHERE TickerDateTime > '2018-05-17 09:15:00.000' and TimePeriod in (5)

--update TickerMinSuperTrend
--set TrueRange = NULL, ATR1 = NULL,ATR2 = NULL,ATR3 = NULL,BUB1 = NULL,BUB2 = NULL,BUB3 = NULL,BLB1 = NULL,BLB2 = NULL,BLB3 = NULL,
--FUB1 = NULL,FUB2 = NULL,FUB3 = NULL,FLB1 = NULL,FLB2 = NULL,FLB3 = NULL,ST1 = NULL,ST2 = NULL,ST3 = NULL,Trend1 = NULL,Trend2 = NULL,Trend3 = NULL
--WHERE TickerDateTime > '2018-05-17 09:15:00.000' and TimePeriod in (5)

--update TickerMinEMAHA
--SET EHEMA1 = NULL, EHEMA2 = NULL, EHEMA3 = NULL, EHEMA4 = NULL,EHEMA5 = NULL,VWMA1 = NULL,VWMA2 = NULL,HAOpen = NULL,HAHigh = NULL,HALow = NULL,HAClose = NULL,varEMA1v2 = NULL,
--varEMA1v3 = NULL,varEMA1v4 = NULL,varEMA2v3 = NULL,varEMA2v4 = NULL,varEMA3v4 = NULL,varEMA4v5 = NULL,varVWMA1vVWMA2 = NULL,varVWMA1vPriceClose = NULL,varVWMA2vPriceClose = NULL,
--varVWMA1vEMA1 = NULL,varHAOvHAC = NULL,varHAOvHAPO = NULL,varHACvHAPC = NULL,varOvC = NULL,varOvPO = NULL,varCvPC = NULL,HAOCwEMA1 = NULL,OCwEMA1 = NULL, AllEMAsInNum = NULL
--WHERE TickerDateTime > '2018-05-17 09:15:00.000' and TimePeriod in (5)



--select StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, TrueRange, ATR1,ATR2,ATR3, ST1,ST2,ST3, Trend1, Trend2,Trend3
--from TickerMinSuperTrend
--where StockCode = 'HINDUNILVR' and TimePeriod = 5 and TickerDateTime > '2018-04-06'


--select StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, TrueRange, ATR1,ATR2,ATR3, ST1,ST2,ST3, Trend1, Trend2,Trend3
--from TickerMinSuperTrend
--where StockCode in ('AUROPHARMA','BAJFINANCE','BHARTIARTL','BPCL','COALINDIA','HCLTECH','HDFCBANK','INFY','LUPIN','TATASTEEL') and timeperiod = 30


--select distinct Convert(date,TickerDateTime) from TickerMinSuperTrend where  TimePeriod = 60 order by Convert(date,TickerDateTime) 
--select * from TickerMinSuperTrend where  TimePeriod = 60 

--select ST.StockCode, ST.TickerDateTime, ST.PriceOpen, ST.PriceHigh, ST.PriceLow, ST.PriceClose, ST.Volume, ATR1, ATR2, ATR3, ST1,ST2,ST3, Trend1, Trend2,Trend3, MACD, Signal, Histogram, HistIncDec, Impulse, EMA1, EMA2, RSI1, RSI2
--from TickerMinSuperTrend ST join TickerMinElderIndicators EI on St.StockCode = EI.StockCode and ST.TickerDateTime = EI.TickerDateTime and ST.TimePeriod = EI.TimePeriod
--where ST.StockCode in ('BAJAJ-AUTO') and ST.timeperiod = 30 and ST.TickerDateTime >= '2018-04-13' and ST.TickerDateTime <= '2018-04-14'

--select ST.StockCode, ST.TickerDateTime, ST.PriceOpen, ST.PriceHigh, ST.PriceLow, ST.PriceClose, ST.Volume, ATR1, ATR2, ATR3, ST1,ST2,ST3, Trend1, Trend2,Trend3, MACD, Signal, Histogram, HistIncDec, Impulse, EMA1, EMA2, RSI1, RSI2
--from TickerMinSuperTrend ST join TickerMinElderIndicators EI on St.StockCode = EI.StockCode and ST.TickerDateTime = EI.TickerDateTime and ST.TimePeriod = EI.TimePeriod
--where ST.StockCode in ('BAJAJ-AUTO') and ST.timeperiod = 5 and ST.TickerDateTime >= '2018-04-13' and ST.TickerDateTime <= '2018-04-14'


--select * from TickerMin where TimePeriod = 0 

--select StockCode, PriceOpen, PriceClose, change, changePercent, TradedValue from TickerMinElderIndicators  
--where TickerDateTime = '2018-04-02 09:15:00.000' and TimePeriod = 15 and StockCode in 
--(
--	select StockCode from TickerMinElderIndicators  
--	where TickerDateTime = '2018-04-02 09:15:00.000' and TimePeriod = 3 and ChangePercent between 0.5 and 1
--)
--order by changePercent desc


-----------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------


--DECLARE @SmallRunDate datetime
--DECLARE @LargeRunDate datetime
--DECLARE @LargeTimePeriod int
--DECLARE @SmallTimePeriod int

--set @LargeRunDate = '2018-04-24 00:00:00'
--set @SmallRunDate = '2018-04-24 09:15:00'
--set @LargeTimePeriod = 375
--set @SmallTimePeriod = 15



--;with cte as (
--select Min15.StockCode , Min15.PriceOpen as LargeOpen, Min15.PriceClose as LargeClose, Min3.PriceOpen as SmallOpen, Min3.PriceClose as SmallClose, 
--Min15.change as LargeChange, Min3.change as SmallChange, Min15.changePercent as LargeChangePer, Min3.changePercent as SmallChangePer, 
--Min15.TradedValue as LargeValue, Min3.TradedValue as SmallValue, 
--(Min15.changePercent - Min3.changePercent) as PercentageGain, IIF((Min15.changePercent - Min3.changePercent) < -0.6, -0.7, 
--(Min15.changePercent - Min3.changePercent) - 0.1) as AfterBrokerage
--from TickerMinElderIndicators Min15 join TickerMinElderIndicators Min3 on Min15.StockCode = Min3.StockCode 
--where Min15.TickerDateTime = @LargeRunDate and Min3.TickerDateTime = @SmallRunDate and Min15.TimePeriod = @LargeTimePeriod and Min3.timeperiod = @SmallTimePeriod and Min15.StockCode in 
--(
--	select StockCode from TickerMinSuperTrend
--	where TickerDateTime = @LargeRunDate and TimePeriod = @LargeTimePeriod and Trend1 = 'UP'
--	and StockCode in (
--		select top 25 StockCode from    TickerMinElderIndicators 
--		where TickerDateTime = @SmallRunDate and TimePeriod = @SmallTimePeriod and ChangePercent > 0.5
--		order by TradedValue desc
--		--select top 25 StockCode from  TickerMinElderIndicators 
--		--where TickerDateTime = @SmallRunDate and TimePeriod = @SmallTimePeriod and PriceOpen = PriceLow
--	)
--)

--Union all

--select Min15.StockCode , Min15.PriceOpen as LargeOpen, Min15.PriceClose as LargeClose, Min3.PriceOpen as SmallOpen, Min3.PriceClose as SmallClose, 
--Min15.change as LargeChange, Min3.change as SmallChange, Min15.changePercent as LargeChangePer, Min3.changePercent as SmallChangePer, 
--Min15.TradedValue as LargeValue, Min3.TradedValue as SmallValue, 
--(Min3.changePercent - Min15.changePercent) as PercentageGain, IIF((Min3.changePercent - Min15.changePercent) < -0.6, -0.7, 
--(Min3.changePercent - Min15.changePercent) - 0.1) as AfterBrokerage
--from TickerMinElderIndicators Min15 join TickerMinElderIndicators Min3 on Min15.TickerDateTime = Min3.TickerDateTime and Min15.StockCode = Min3.StockCode 
--where Min15.TickerDateTime = @LargeRunDate and Min3.TickerDateTime = @SmallRunDate and Min15.TimePeriod = @LargeTimePeriod and Min3.timeperiod = @SmallTimePeriod and Min15.StockCode in 
--(
--	select StockCode from TickerMinSuperTrend
--	where TickerDateTime = @LargeRunDate and TimePeriod = @LargeTimePeriod and Trend1 = 'DOWN'
--	and StockCode in (
--		select top 25 StockCode from TickerMinElderIndicators  
--		where TickerDateTime = @SmallRunDate and TimePeriod = @SmallTimePeriod and ChangePercent < -0.5
--		order by TradedValue desc

--	)
--)
--)
----select StockCode, Sum(AfterBrokerage) from cte
----group by StockCode WITH ROLLUP
--select * from cte

-----------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------
-----------------------------------------------------------------------------------------------------------------


--select top 25 StockCode, PriceOpen, PriceClose, change, changePercent, TradedValue from TickerMinElderIndicators  
--where TickerDateTime = '2018-04-20 09:15:00.000' and TimePeriod = 15 --and ChangePercent between 0.5 and 1
--order by TradedValue desc


----delete from TickerMinElderIndicators
----where TimePeriod = 3
----delete from TickerMinSuperTrend
----where TimePeriod = 3

exec spGenerateOHLC '2019-09-03 09:15',3
exec spGenerateOHLC '2019-09-03 09:15',5
exec spGenerateOHLC '2019-09-03 09:15',10
exec spGenerateOHLC '2019-09-03 09:15',15
--exec spGenerateOHLC '2019-09-08 09:15',25
--exec spGenerateOHLC '2019-09-29 09:15',30
--exec spGenerateOHLC '2019-09-29 09:15',60
--exec spGenerateOHLC '2019-09-29 09:15',375

--sp_helptext spGenerateOHLC

--exec spGetTickerDataForIndicators 'ABB,ACC,ADANIENT,ADANIPORTS,ADANIPOWER,AJANTPHARM,AMARAJABAT,AMBUJACEM,APOLLOHOSP,APOLLOTYRE,ARVIND,ASHOKLEY,ASIANPAINT,AUROPHARMA,AXISBANK,BAJAJ-AUTO,BAJAJFINSV,BAJFINANCE,BALKRISIND,BANKBARODA,BANKINDIA,BEL,BERGEPAINT,BHARATFORG,BHARTIARTL,BHEL,BIOCON,BOSCHLTD,BPCL,BRITANNIA,CADILAHC,CANBK,CASTROLIND,CENTURYTEX,CHOLAFIN,CIPLA,COALINDIA,COLPAL,CONCOR,CUMMINSIND,DABUR,DALMIABHA,DISHTV,DIVISLAB,DLF,DMART,DRREDDY,EICHERMOT,EMAMILTD,ENGINERSIN,EXIDEIND,FEDERALBNK,GAIL,GLAXO,GLENMARK,GMRINFRA,GODREJCP,GODREJIND,GSKCONS,HAVELLS,HCLTECH,HDFC,HDFCBANK,HEROMOTOCO,HINDALCO,HINDPETRO,HINDUNILVR,HINDZINC,IBULHSGFIN,ICICIBANK,ICICIPRULI,IDBI,IDEA,IDFC,IDFCBANK,IGL,INDIGO,INDUSINDBK,INFRATEL,INFY,IOC,IRB,ITC,JINDALSTEL,JSWSTEEL,KOTAKBANK,L&TFH,LICHSGFIN,LT,LUPIN,M&M,M&MFIN,MARICO,MARUTI,MCDOWELL-N,MINDTREE,MOTHERSUMI,MRF,MRPL,MUTHOOTFIN,NBCC,NHPC,NMDC,NTPC,OFSS,OIL,ONGC,PAGEIND,PCJEWELLER,PEL,PETRONET,PFC,PGHH,PIDILITIND,PNB,POWERGRID,RBLBANK,RCOM,RECLTD,RELIANCE,RELINFRA,RPOWER,SAIL,SBIN,SHREECEM,SIEMENS,SRF,SRTRANSFIN,SUNPHARMA,SUNTV,TATACHEM,TATACOMM,TATAGLOBAL,TATAMOTORS,TATAPOWER,TATASTEEL,TCS,TECHM,TITAN,TORNTPHARM,TVSMOTOR,UBL,ULTRACEMCO,UNIONBANK,UPL,VEDL,VOLTAS,WIPRO,YESBANK,ZEEL', '5'

--SELECT * FROM ufn_CSVToTable('5',',')

--select * from TickerMinElderIndicators
--where TimePeriod = 5

--select distinct convert(date, TickerDateTime) from TickerMinSuperTrend
--order by convert(date, TickerDateTime) desc
----where TickerDateTime > '2018-04-16 09:15'

--select count(*) from TickerMinSuperTrend
--where ST1 is not null

--select * from TickerMinElderIndicators
--where TimePeriod = 60
--order by ChangePercent desc

--select * from TickerMinElderIndicators
--where TimePeriod = 375 and TickerDateTime between '2018-04-01' and '2018-04-30'
--order by ChangePercent desc

--exec spGetTickerDataForIndicators 'RELIANCE','5,10,15,30,60,375'

--select * from TickerMinElderIndicators
--where TimePeriod = 15 and TickerDateTime >= '2018-04-24' and stockcode = 'VEDL'

--select ST.StockCode, ST.TickerDateTime, ST.PriceOpen, ST.PriceHigh, ST.PriceLow, ST.PriceClose, ST.Volume, ST1,ST2,ST3, Trend1, Trend2,Trend3, 
--Histogram, HistIncDec, Impulse, EMA1, EMA2
--from TickerMinSuperTrend ST join TickerMinElderIndicators EI on St.StockCode = EI.StockCode and ST.TickerDateTime = EI.TickerDateTime and ST.TimePeriod = EI.TimePeriod
--where ST.StockCode in ('INFRATEL') and ST.timeperiod = 375 and ST.TickerDateTime >= '2018-04-20' --and ST.TickerDateTime <= '2018-04-24'

--select ST.StockCode, ST.TickerDateTime, ST.PriceOpen, ST.PriceHigh, ST.PriceLow, ST.PriceClose, ST.Volume, ST1,ST2,ST3, Trend1, Trend2,Trend3, 
--Histogram, HistIncDec, Impulse, EMA1, EMA2
--from TickerMinSuperTrend ST join TickerMinElderIndicators EI on St.StockCode = EI.StockCode and ST.TickerDateTime = EI.TickerDateTime and ST.TimePeriod = EI.TimePeriod
--where ST.StockCode in ('INFRATEL') and ST.timeperiod = 15 and ST.TickerDateTime >= '2018-04-20' --and ST.TickerDateTime <= '2018-04-24'


--select * from TickerMinElderIndicators where StockCode in ('INFRATEL')  TimePeriod = 375 and EMA1 is NULL
--select StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, AllEMAsInNum from TickerMinEMAHA where TimePeriod = 15 and StockCode in ('UBL') 
--select * from TickerMinSuperTrend where TimePeriod = 10 and ST1 is NULL

----delete from TickerMinElderIndicators where TimePeriod in (15,60,375) 
----delete from TickerMinSuperTrend where TimePeriod in (15,60,375)




--select StockCode,TickerDateTime,TimePeriod,PriceOpen,PriceHigh,PriceLow,PriceClose,ST1, ST2, ST3,TRend1,trend2, Trend3
--from TickerMinSuperTrend where TimePeriod = 5 and StockCode in ('UBL') 

--sp_help TickerMinSuperTrend

--sp_helptext spGetTickerDataForIndicators

--sp_helptext spUpdateTicker

--select MSL.*, DV.CurrVolatality from MasterStockList MSL inner join DailyVolatality DV on MSL.TradingSymbol = DV.Symbol
--where IsIncluded = 1
--order by DV.CurrVolatality desc


--select * from DailyVolatality

--select * from TickerMinElderIndicators where TimePeriod = 375


--select min(TickerDateTime) from TickerMinElderIndicators where TimePeriod = 375
--select max(TickerDateTime) from TickerMinElderIndicators where TimePeriod = 375
--select min(TickerDateTime) from TickerMinSuperTrend where TimePeriod = 5
--select max(TickerDateTime) from TickerMinSuperTrend where TimePeriod = 5
--select min(TickerDateTime) from TickerMinEMAHA where TimePeriod = 5
--select max(TickerDateTime) from TickerMinEMAHA where TimePeriod = 5

--select * from TickerMinSuperTrend where TimePeriod = 375 and StockCode = 'DIVISLAB'
--select * from TickerMinElderIndicators where TimePeriod = 375 and StockCode = 'DIVISLAB'

--select * from TickerMin where TradingSymbol = 'DIVISLAB'

--select * from MasterStockList

--select StockCode, TickerDateTime, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume, EHEMA1, HAOpen, HAHigh, HALow, HAClose, HAOCwEMA1, OCwEMA1, AllEMAsInNum
--from TickerMinEMAHA 
--where TimePeriod = 3 and StockCode = 'HDFCBANK' --and TickerDAteTime > '2018-06-18'
--order by TickerDateTime 

--select count(*) from TickerMinEMAHA
--where TimePeriod = 3

----delete from TickerMinEMAHA
----where TimePeriod in (10,15) and TickerDateTime < '2018-06-01'

----delete from TickerMinElderIndicators
----where TimePeriod in (10,15) and TickerDateTime < '2018-06-01'

----delete from TickerMinSuperTrend
----where TimePeriod in (10,15) and TickerDateTime < '2018-06-01'

--MERGE INTO TickerMinEMAHA with (TABLOCK) t1
--USING TickerMinElderIndicators t2
--ON t1.StockCode = t2.StockCode and t1.TickerDateTime = t2.TickerDateTime and t1.TimePeriod = t2.TimePeriod
--WHEN NOT MATCHED THEN
--INSERT(StockCode, TickerDateTime, TimePeriod, PriceOpen, PriceHigh, PriceLow, PriceClose, Volume)
--VALUES(t2.StockCode, t2.TickerDateTime, t2.TimePeriod, t2.PriceOpen, t2.PriceHigh, t2.PriceLow, t2.PriceClose, t2.Volume);


--select t1.StockCode, t1.TickerDateTime, t1.PriceOpen, t1.PriceHigh, t1.PriceLow, t1.PriceClose, t1.Volume, 
--EHEMA1, EHEMA2, EHEMA3, EHEMA4, EHEMA5,
--ST3, Trend1,Trend2, Trend3, RSI1, RSI2,
--HAOpen, HALow, HAHigh, HAClose, HAOCwEMA1, OCwEMA1, AllEMAsInNum
--from TickerMinEMAHA t1 
--inner join TickerMinElderIndicators t2 on t1.StockCode = t2.StockCode and t1.TickerDateTime = t2.TickerDateTime and t1.TimePeriod = t2.TimePeriod
--inner join TickerMinSuperTrend t3 on t1.StockCode = t3.StockCode and t1.TickerDateTime = t3.TickerDateTime and t1.TimePeriod = t3.TimePeriod
--where t1.TimePeriod = 15 and t1.TickerDateTime > '2018-06-15' and t1.TickerDateTime < '2018-06-20'
--and t1.StockCode = 'ABB'
--order by t1.TickerDateTime asc


--select t1.StockCode, t1.TickerDateTime, t1.PriceOpen, t1.PriceHigh, t1.PriceLow, t1.PriceClose, t1.Volume, t2.TradedValue,
--EHEMA1, EHEMA2, EHEMA3, EHEMA4, EHEMA5, HistIncDec, Impulse,
--ST3, Trend1,Trend2, Trend3, RSI1, RSI2,
--HAOpen, HALow, HAHigh, HAClose, HAOCwEMA1, OCwEMA1, AllEMAsInNum
--from TickerMinEMAHA t1 
--inner join TickerMinElderIndicators t2 on t1.StockCode = t2.StockCode and t1.TickerDateTime = t2.TickerDateTime and t1.TimePeriod = t2.TimePeriod
--inner join TickerMinSuperTrend t3 on t1.StockCode = t3.StockCode and t1.TickerDateTime = t3.TickerDateTime and t1.TimePeriod = t3.TimePeriod
--where t1.TimePeriod = 375 --and t1.TickerDateTime > '2018-06-15' and t1.TickerDateTime < '2018-06-20'
--and t1.StockCode = 'ABB'
--order by t1.TickerDateTime asc


----ALTER TABLE TickerMinEMAHA
----DROP COLUMN EMA3v4

----HDFC
----HDFCBANK
----HEROMOTOCO
----HINDALCO
----HINDUNILVR
----HINDPETRO
----HINDZINC

--select * from MasterStockList where IsIncluded = 1
--and TradingSymbol like '%&%'

----update MasterStockList
----set IsIncluded = 0
----where TradingSymbol like '%&%'


--select * from TickerMinElderIndicators 
--where TimePeriod = 375 and StockCode = 'ABB' and TickerDateTime = '2018-06-15 00:00:00'


--select * from TickerMinElderIndicators 
--where TimePeriod = 375 and StockCode = 'ABB' 

--select * from TickerMinEMAHA 
--where TimePeriod = 3 and StockCode = 'ABB' 

--select * from TickerMinSuperTrend
--where TimePeriod = 3 and StockCode = 'ABB' 



--select Max(TickerDateTime) from TickerMinElderIndicators 
--where TimePeriod = 60 and StockCode = 'ABB' 


--select * from TickerMin
--where TradingSymbol = 'ABB' 

--select * from DailyVolatality

--select * from TickerMinElderIndicators 
--where TimePeriod = 375 --and PriceOpen = PriceLow
--and StockCode = 'AMARAJABAT'
--and TickerDateTime > '2018-03-27' and TickerDateTime < '2018-04-03'
--order by TickerDateTime asc

--select * from TickerMinElderIndicators 
--where TimePeriod = 5 --and PriceOpen = PriceLow
--and StockCode = 'AMARAJABAT'
--and TickerDateTime > '2018-04-02'
--order by TickerDateTime asc

--select * from DailyVolatality

--select * from Instruments


--select * from TickerMinElderIndicators
--where timeperiod = 375 and StockCode = 'ABB'

--select * from TickerMinElderIndicators
--where timeperiod = 5 and StockCode = 'ABB'


--select * from TickerMinElderIndicators
--where timeperiod = 5 and StockCode = 'EMAMILTD'
--and TickerDateTime between '2018-06-20 09:30' and '2018-06-20 10:15'

--declare @dateTiem

--Select StockCode, PriceHigh, PriceLow, Close_Price, PerFromHigh, PerFromLow, RANK() over (order by PerFromHigh asc) as IntraRankHigh from (
--select StockCode,
--      MAX(PRICEHIGH)PRICEHIGH,
--	  MIN(PRICELOW)PRICELOW,
--     (SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime))CLOSE_PRICE,
	 
--	 Round((( (SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime)) - MIN(PRICELOW) ) / (SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime)) )* 100,2) PerFromLow,
--	 Round((((SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime))- MAX(PRICEHIGH)) / (SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime)) )* 100,2) PerFromHigh
-- from (
--select StockCode, Pricehigh , PriceLow,priceclose,tickerdatetime from TickerMinElderIndicators
--where timeperiod = 5 
--and TickerDateTime >= '2018-06-20 09:45' and TickerDateTime  < '2018-06-20 10:00'
--) abcd
--GROUP BY StockCode
--) AllData




--Select StockCode, PriceHigh, PriceLow, Close_Price, PerFromHigh, PerFromLow, RANK() over (order by PerFromLow desc) as IntraRankLow from (
--select StockCode,
--      MAX(PRICEHIGH)PRICEHIGH,
--	  MIN(PRICELOW)PRICELOW,
--     (SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime))CLOSE_PRICE,
	 
--	 Round((( (SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime)) - MIN(PRICELOW) ) / (SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime)) )* 100,2) PerFromLow,
--	 Round((((SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime))- MAX(PRICEHIGH)) / (SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime)) )* 100,2) PerFromHigh
-- from (
--select StockCode, Pricehigh , PriceLow,priceclose,tickerdatetime from TickerMinElderIndicators
--where timeperiod = 5 
--and TickerDateTime >= '2018-06-21 09:15' and TickerDateTime  < '2018-06-21 09:40' 
--) abcd
--GROUP BY StockCode
--) AllData 
--where Perfromhigh > -0.25 
--order by StockCode asc


--Select StockCode, PriceHigh --, PriceLow, Close_Price, PerFromHigh, PerFromLow, RANK() over (order by PerFromLow desc) as IntraRankLow 
--from (
--select StockCode,
--      MAX(PRICEHIGH)PRICEHIGH,
--	  MIN(PRICELOW)PRICELOW,
--     (SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime))CLOSE_PRICE,
	 
--	 Round((( (SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime)) - MIN(PRICELOW) ) / (SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime)) )* 100,2) PerFromLow,
--	 Round((((SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime))- MAX(PRICEHIGH)) / (SELECT SUM(PRICECLOSE) FROM TickerMinElderIndicators WHERE timeperiod=5 AND StockCode = ABCD.StockCode AND tickerdatetime = MAX(abcd.tickerdatetime)) )* 100,2) PerFromHigh
-- from (
--select StockCode, Pricehigh , PriceLow,priceclose,tickerdatetime from TickerMinElderIndicators
--where timeperiod = 5 
--and TickerDateTime >= '2018-06-21 09:45' and TickerDateTime  < '2018-06-21 15:15' and StockCode in ('ADANIENT','ARVIND','ASHOKLEY','ASIANPAINT','BAJAJFINSV','CADILAHC','CASTROLIND','CHOLAFIN','CUMMINSIND','EMAMILTD','GLENMARK','GODREJIND','IBULHSGFIN','IGL','INDUSINDBK','IRB','LICHSGFIN','MINDTREE','MRF','NMDC','RECLTD','SRTRANSFIN','SUNPHARMA','TATAMOTORS','VOLTAS','YESBANK','ZEEL')
--) abcd
--GROUP BY StockCode
--) AllData 
----where Perfromhigh = 0 
--order by StockCode asc



--SELECT TOP 10 STOCKCODE, PRICEHIGH, PRICELOW, PRICECLOSE, 
--	ROUND(((PRICECLOSE - PRICELOW) / PRICECLOSE) * 100, 2) AS PERFROMLOW, ROUND(((PRICEHIGH - PRICECLOSE) / PRICECLOSE) * 100, 2) AS PERFROMHIGH,
--	RANK() OVER (ORDER BY ROUND(((PRICECLOSE - PRICELOW) / PRICECLOSE) * 100, 2) DESC) AS INTRARANKLOW
--FROM  TickerMinElderIndicators
--WHERE TimePeriod = 10
--AND TickerDateTime = '2018-06-21 09:15'

--SELECT TOP 10 STOCKCODE, PRICEHIGH, PRICELOW, PRICECLOSE, 
--	ROUND(((PRICECLOSE - PRICELOW) / PRICECLOSE) * 100, 2) AS PERFROMLOW, ROUND(((PRICEHIGH - PRICECLOSE) / PRICECLOSE) * 100, 2) AS PERFROMHIGH,
--	RANK() OVER (ORDER BY ROUND(((PRICECLOSE - PRICELOW) / PRICECLOSE) * 100, 2) DESC) AS INTRARANKLOW
--FROM  TickerMinElderIndicators
--WHERE TimePeriod = 15
--AND TickerDateTime = '2018-06-21 09:15'

--WITH MIN30 AS
--(
--SELECT STOCKCODE, PRICEOPEN, PRICEHIGH, PRICELOW, PRICECLOSE, 
--	ROUND(((PRICECLOSE - PRICELOW) / PRICECLOSE) * 100, 2) AS PERFROMLOW, ROUND(((PRICECLOSE - PRICEHIGH) / PRICECLOSE) * 100, 2) AS PERFROMHIGH,
--	RANK() OVER (ORDER BY ROUND(((PRICECLOSE - PRICELOW) / PRICECLOSE) * 100, 2) DESC) AS INTRARANKLOW,
	
--	(SELECT PRICEHIGH FROM TickerMinElderIndicators T2 WHERE T2.StockCode = T1.STOCKCODE AND T2.TimePeriod = 375 AND T2.TickerDateTime = '2018-06-22') AS PREVPRICEHIGH
--FROM  TickerMinElderIndicators T1
--WHERE TimePeriod = 30
--AND TickerDateTime = '2018-06-25 09:15'
--) 
--SELECT *, Round(PriceClose * 1.003,1) AS TARGET FROM MIN30 
--WHERE --PRICEOPEN = PRICELOW and PREVPRICEHIGH < PriceHigh AND 
--PRICEOPEN < PREVPRICEHIGH AND PRICECLOSE > PREVPRICEHIGH
--ORDER BY STOCKCODE ASC

--select stockcode, max(pricehigh)
--FROM  TickerMinElderIndicators
--where timeperiod = 5 
--and TickerDateTime >= '2018-06-25 09:45' and TickerDateTime  <= '2018-06-25 15:15' and StockCode in ('ACC','AMBUJACEM','BAJFINANCE','MARICO','MRPL','SHREECEM','TATAGLOBAL','ULTRACEMCO')
--group by stockcode

--select stockcode, priceclose
--FROM  TickerMinElderIndicators
--where timeperiod = 5 
--and TickerDateTime = '2018-06-25 15:15' and StockCode in ('ACC','AMBUJACEM','BAJFINANCE','MARICO','MRPL','SHREECEM','TATAGLOBAL','ULTRACEMCO')


--EXEC spGetScriptsForBackTest '2018-07-06 09:45','2018-07-05','2018-07-04', 15,'LONG'

--select top 100 * from TickerMinElderIndicators where TimePeriod = 5 order by TickerDateTime desc
--select count(*) from TickerMin




--MasterStockList, TickerMin, TickerMinElderIndicators, TickerMinEMAHA, TickerMinSuperTrend


--select * from TickerMin where dateTime > '2019-04-02'

--	select * from TickerMinElderIndicators 
--	where TimePeriod = 15 and StockCode = 'AShOKLEY' and TickerDateTime > '2019-06-04 09:15' and TickerDateTime < '2018-07-06'

--select * from TickerMinSuperTrend 
--where TimePeriod = 5 and StockCode = 'ABB' and TickerDateTime > '2019-03-01' and TickerDateTime < '2018-07-07'


--select distinct TRadingSymbol from TickerMin 
--where dateTime > '2019-04-02'
--order by TRadingSymbol

--select TRadingSymbol, count(1) from TickerMin 
--where dateTime > '2019-06-03'
--group by TRadingSymbol
--having count(1) > 8000

--exec spGetTickerDataForIndicators

----delete from TickerMin 
----where dateTime < '2019-04-02'


----update MasterStockList
----set IsIncluded = 0
----where TradingSymbol in 
----	(select distinct TRadingSymbol from TickerMin 
----	where dateTime > '2019-06-03')

----update MasterStockList
----set IsIncluded = 0

----update MasterStockList
----set IsIncluded = 0
----where Collection not in ('Nifty 50')

----update MasterStockList
----set IsIncluded = 0
----where TradingSymbol in ('M&M','BAJAJ-AUTO')

--select * from MasterStockList
--where IsIncluded = 1 
--order by TradingSymbol

--sp_helptext spGetScriptsForBackTest
--sp_helptext spGetTickerDataForIndicators

--sp_helptext spGenerateOHLC

--exec spGetScriptsForBackTest '2019-07-12 15:15', '2019-07-11', '2019-07-10',30,'LONG'

--select * from TickerMin where DateTime > '2019-06-10'

--select * from TickerMinElderIndicators where stockCode = 'AUROPHARMA' and timeperiod = 375

----select * into TickerMin_bkp_140719 from TickerMin
----select * into TickerMinElderIndicators_bkp_140719 from TickerMinElderIndicators
----select * into TickerMinEMAHA_bkp_140719 from TickerMinEMAHA
----select * into TickerMinSuperTrend_bkp_140719 from TickerMinSuperTrend


----truncate table TickerMin
----truncate table TickerMinElderIndicators
----truncate table TickerMinEMAHA
----truncate table TickerMinSuperTren

--select * from TickerMinElderIndicators
--where stockCode = 'WIPRO'
--and Timeperiod = 30

--select min(DateTime), max(DateTime) from TickerMin 

--select top 10 * from TickerMin
--select * from TickerMinEMAHA
--where TimePeriod = 5

--select * from TickerMinSuperTrend
--where TimePeriod = 5


--exec spStockStatistics '2019-08-02 09:15', '2019-08-01', 30,'LONG'


--select * from TickerMinSuperTrend
--where stockCode = 'AUROPHARMA' and timeperiod = 15

--sp_helptext spGetScriptsForBackTest

--select distinct(cast (DateTime as date))  from tickerMin 

--select * from TickerMinElderIndicators where TimePeriod = 25

--sp_helptext spGenerateOHLC

----truncate table ConsolidatedStockStatistics

--select * from ConsolidatedStockStatistics

--select stockCode, PriceOpen, PriceHigh, PriceLow, PriceClose, PrevPriceClose, prevdayrankhigh, prevdayranklow, intrarankhigh, intraranklow
--from ConsolidatedStockStatistics
--where timeperiod = 30 and TickerDateTime = '2019-08-02 13:15:00.000'
--order by prevdayrankhigh

--select * from TickerMinEMAHA

--select * from UserLogins

--select * from MasterStockList

----truncate table UserLogins


--sp_helptext spUpdateTicker

--select * from TickerMin

--select distinct(TradingSymbol) from TickerMin

--select top 10 * from TickerMin where Datetime > '2019-09-18'

--select * from TickerMinElderIndicators 
--where TickerDatetime > '2019-09-20' 
--and stockcode = 'RELIANCE' and timeperiod = 5

--select * from TickerMinSuperTrend 
--where TickerDatetime > '2019-09-20' 
--and stockcode = 'RELIANCE' and timeperiod = 5


--select * from TickerMinEMAHA where TickerDatetime > '2019-09-19'
--select * from TickerMinSuperTrend where TickerDatetime > '2019-09-19'

--select * from TickerMin where Datetime > '2019-09-19'

select TradingSymbol, count(1) from TickerMin 
where Datetime > '2019-09-03'
group by TradingSymbol
order by  TradingSymbol asc


--select Timeperiod, count(1) from TickerMinElderIndicators 
--where TickerDatetime > '2019-09-26'
--group by  Timeperiod 
--order by Timeperiod 

--select * from TickerMinElderIndicators 
--where TickerDatetime > '2019-09-26'
--and StockCode = 'ADANIPORTS'

--select * from UserLogins

--select * from Logs where id > 700
----where TimeStamp > '2019-09-19 20:00'

----delete from Logs
----truncate table logs

--select top 1000 * from Logs(nolock)
--where Timestamp > '2019-09-27' and id > 7500
--order by id desc


----select * from TickerMin
----where Datetime > '2019-09-26'
----and TradingSymbol = 'ADANIPORTS'
----order by Datetime desc

--select * from TickerMinElderIndicators 
--where TickerDatetime > '2019-09-01'
--and StockCode = 'ADANIPORTS' and timeperiod = 375
--order by TickerDateTime desc

--select * from MasterStockList
--where IsIncluded = 1

--select * from MasterStockList where Collection = 'Nifty Midcap 50'

--update MasterStockList 
--set IsIncluded = 0
----where Collection = 'Nifty Midcap 50'
--where TradingSymbol in ('IDFCBANK','TATACOMM')


----delete from Logs where Timestamp < getDate() - 2 

select * from Logs
where Timestamp > '2019-09-30'
order by timeStamp desc

----sp_helptext spGenerateOHLC

----declare @Yesterday datetime, @Today datetime
----set @Yesterday = '2019-09-23' 
----set @Today = '2019-09-24'
----select today.TradingSymbol, cast(yesterday.[DateTime] as Date) as 'Yesterday', yesterday.[Close], 
----cast(today.[DateTime] as date) as 'Today', today.[Open], ((today.[Open] - yesterday.[Close]) / today.[Open]) * 100 as GapPer 
----from TickerMin yesterday inner join TickerMin today on yesterday.TradingSymbol = today.tradingsymbol  
----and today.[DateTime] = @Today and yesterday.[DateTime] = @Yesterday
----where (((today.[Open] - yesterday.[Close]) / today.[Open]) * 100 > 2)

--select * from TickerMin
--where TradingSymbol = 'AMARAJABAT'
--and [DateTime] > '2019-09-24 09:15'


--select top 100 * from TickerMin as today
--where DateTime = '2019-09-26 09:15'


--exec spGenerateOHLC '2019-09-21 09:15',3
--exec spGenerateOHLC '2019-09-21 09:15',5
--exec spGenerateOHLC '2019-09-21 09:15',10
--exec spGenerateOHLC '2019-09-21 09:15',15
--exec spGenerateOHLC '2019-09-21 09:15',25
--exec spGenerateOHLC '2019-09-21 09:15',30
--exec spGenerateOHLC '2019-09-21 09:15',60
--exec spGenerateOHLC '2019-09-21 09:15',375

--select * from TickerMinElderIndicators 

--declare @Yesterday datetime, @Today datetime
--set @Yesterday = '2019-09-20' 
--set @Today = '2019-09-23'
--select today.StockCode, msl.[collection], cast(yesterday.TickerDateTime as Date) as 'Yesterday', yesterday.PriceClose, 
--cast(today.TickerDateTime as date) as 'Today', today.PriceOpen, ((today.PriceOpen - yesterday.PriceClose) / today.PriceOpen) * 100 as GapPer,
--(today.PriceOpen - (today.PriceOpen * 0.01)) as 'Target', today.PriceLow as 'Final', 
--CASE WHEN today.PriceLow < (today.PriceOpen - (today.PriceOpen * 0.01)) THEN 'TRUE' ELSE 'FALSE' END
--from TickerMinElderIndicators yesterday 
--inner join TickerMinElderIndicators today on yesterday.StockCode = today.StockCode and today.TickerDateTime = @Today and yesterday.TickerDateTime = @Yesterday
--inner join MasterStockList msl on msl.TradingSymbol = today.StockCode
--where (((today.PriceOpen - yesterday.PriceClose) / today.PriceOpen) * 100  > 2) and today.PriceClose > 50
--union
--select today.StockCode, msl.[collection], cast(yesterday.TickerDateTime as Date) as 'Yesterday', yesterday.PriceClose, 
--cast(today.TickerDateTime as date) as 'Today', today.PriceOpen, ((today.PriceOpen - yesterday.PriceClose) / today.PriceOpen) * 100 as GapPer,
--(today.PriceOpen + (today.PriceOpen * 0.01)) as 'Target', today.PriceHigh as 'Final',
--CASE WHEN today.PriceHigh > (today.PriceOpen + (today.PriceOpen * 0.01)) THEN 'TRUE' ELSE 'FALSE' END
--from TickerMinElderIndicators yesterday 
--inner join TickerMinElderIndicators today on yesterday.StockCode = today.StockCode and today.TickerDateTime = @Today and yesterday.TickerDateTime = @Yesterday
--inner join MasterStockList msl on msl.TradingSymbol = today.StockCode
--where (((today.PriceOpen - yesterday.PriceClose) / today.PriceOpen) * 100  < -2) and today.PriceClose > 50

----truncate table TickerMinElderIndicators
----truncate table TickerMinSuperTrend
----Truncate table TickerMinEMAHA

----truncate table Logs

--select * from TickerMinElderIndicators
--where stockCode = 'ABB'

----delete from TickerMinElderIndicators
----where TimePeriod <> 375

select * from UserLogins
--select * from MasterStockList

--INSERT INTO MasterStockList VALUES (1111111, '','','Nifty Midcap', 1)


exec spGetGapOpenedScripts '2019-09-20', '2019-09-23', 1, 2, 5000, 50 


select top 10 * from TickerMinElderIndicators(nolock)
where TimePeriod = 3

select * from Logs

select * from BackTestLogs

