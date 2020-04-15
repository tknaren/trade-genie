USE [TradeGenie]
GO

/****** Object:  StoredProcedure [dbo].[spAddUpdateTrades]    Script Date: 04-03-2018 19:07:12 ******/
DROP PROCEDURE [dbo].[spAddUpdateTrades]
GO

/****** Object:  StoredProcedure [dbo].[spAddUpdateTrades]    Script Date: 04-03-2018 19:07:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE PROCEDURE [dbo].[spAddUpdateTrades]
      @tblTrades TradeType READONLY
AS
BEGIN
      SET NOCOUNT ON;
 
      MERGE INTO Trades t1
      USING @tblTrades t2
      ON t1.[TradeId]  = t2.[TradeId] and t1.[OrderId]  = t2.[OrderId] and t1.[ExchangeOrderId]  = t2.[ExchangeOrderId]
	  WHEN MATCHED THEN
	  UPDATE
	  SET	t1.[TradingSymbol] = t2.[TradingSymbol],
			t1.[Exchange] = t2.[Exchange],
			t1.[InstrumentToken] = t2.[InstrumentToken],
			t1.[TransactionType] = t2.[TransactionType],
			t1.[Product] = t2.[Product],
			t1.[AveragePrice] = t2.[AveragePrice],
			t1.[Quantity] = t2.[Quantity],
			t1.[OrderTimestamp] = t2.[OrderTimestamp],
			t1.[ExchangeTimestamp] = t2.[ExchangeTimestamp]
      WHEN NOT MATCHED THEN
      INSERT VALUES(t2.[TradeId],	t2.[OrderId],	t2.[ExchangeOrderId],	t2.[TradingSymbol],	t2.[Exchange],	
			t2.[InstrumentToken],	t2.[TransactionType],	t2.[Product],	t2.[AveragePrice],	t2.[Quantity],	
			t2.[OrderTimestamp],	t2.[ExchangeTimestamp]);
END



GO


