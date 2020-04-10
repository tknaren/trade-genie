USE [TradeGenie]
GO

/****** Object:  StoredProcedure [dbo].[spAddUpdateNetPositions]    Script Date: 05-03-2018 11:54:09 ******/
DROP PROCEDURE [dbo].[spAddUpdateNetPositions]
GO

/****** Object:  StoredProcedure [dbo].[spAddUpdateNetPositions]    Script Date: 05-03-2018 11:54:09 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE PROCEDURE [dbo].[spAddUpdateNetPositions]
      @tblNetPositions NetPositionType READONLY
AS
BEGIN
      SET NOCOUNT ON;
 
      MERGE INTO NetPositions t1
      USING @tblNetPositions t2
      ON t1.InstrumentToken = t2.InstrumentToken and t1.PositionDate = t2.PositionDate and t1.Product = t2.Product
	  WHEN MATCHED THEN
	  UPDATE
	  SET	t1.[TradingSymbol] = t2.[TradingSymbol],
			t1.[AveragePrice] = t2.[AveragePrice],
			t1.[BuyM2M] = t2.[BuyM2M],
			t1.[BuyPrice] = t2.[BuyPrice],
			t1.[BuyQuantity] = t2.[BuyQuantity],
			t1.[BuyValue] = t2.[BuyValue],
			t1.[ClosePrice] = t2.[ClosePrice],
			t1.[Exchange] = t2.[Exchange],
			t1.[LastPrice] = t2.[LastPrice],
			t1.[M2M] = t2.[M2M],
			t1.[Multiplier] = t2.[Multiplier],
			t1.[NetBuyAmountM2M] = t2.[NetBuyAmountM2M],
			t1.[NetSellAmountM2M] = t2.[NetSellAmountM2M],
			t1.[OvernightQuantity] = t2.[OvernightQuantity],
			t1.[PNL] = t2.[PNL],
			t1.[Quantity] = t2.[Quantity],
			t1.[Realised] = t2.[Realised],
			t1.[SellM2M] = t2.[SellM2M],
			t1.[SellPrice] = t2.[SellPrice],
			t1.[SellQuantity] = t2.[SellQuantity],
			t1.[SellValue] = t2.[SellValue],
			t1.[Unrealised] = t2.[Unrealised],
			t1.[Value] = t2.[Value]
      WHEN NOT MATCHED THEN
      INSERT VALUES(t2.InstrumentToken, t2.PositionDate, t2.TradingSymbol, t2.AveragePrice, t2.BuyM2M, t2.BuyPrice, 
			t2.BuyQuantity, t2.BuyValue, t2.ClosePrice, t2.Exchange, t2.LastPrice, t2.M2M, t2.Multiplier, t2.NetBuyAmountM2M, 
			t2.NetSellAmountM2M, t2.OvernightQuantity, t2.PNL, t2.Product, t2.Quantity, t2.Realised, t2.SellM2M, t2.SellPrice, 
			t2.SellQuantity, t2.SellValue, t2.Unrealised, t2.Value);
END




GO


