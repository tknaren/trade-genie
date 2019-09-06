-- =========================================
-- Create table template
-- =========================================
USE FinancialMarket
GO

DROP TABLE dbo.ConsolidatedStockStatistics
GO

CREATE TABLE dbo.ConsolidatedStockStatistics
(
	TickerDateTime	datetime NOT NULL,
	STOCKCODE	varchar(25) NULL,
	TimePeriod	int NULL,
	PRICEOPEN	float NULL,
	PRICEHIGH	float NULL,
	PRICELOW	float NULL,
	PRICECLOSE	float NULL,
	TRADEDVALUE	float NULL,
	PERFROMLOW	float NULL,
	PERFROMHIGH	float NULL,
	INTRARANKHIGH	int NULL,
	INTRARANKLOW	int NULL,
	PREVDAYRANKHIGH	int NULL,
	PREVDAYRANKLOW	int NULL,
	PREVPRICECLOSE	float NULL,
	PREVPRICELOW	float NULL,
	PREVPRICEHIGH	float NULL,
	Impulse	CHAR(1) NULL,
	MACD	float NULL,
	Signal	float NULL,
	Histogram	float NULL,
	EHEMA1	float NULL,
	EHEMA2	float NULL,
	EHEMA3	float NULL,
	EHEMA4	float NULL,
	EHEMA5	float NULL,
	ForceIndex1	float NULL,
	ForceIndex2	float NULL,
	RSI1	float NULL,
	RSI2	float NULL,
	AllEMAsInNum	int NULL

)
GO
