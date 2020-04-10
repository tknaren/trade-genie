USE [TradeGenie]
GO

/****** Object:  StoredProcedure [dbo].[spAddUpdateOrders]    Script Date: 05-02-2018 13:06:29 ******/
DROP PROCEDURE [dbo].[spAddUpdateOrders]
GO

/****** Object:  StoredProcedure [dbo].[spAddUpdateOrders]    Script Date: 05-02-2018 13:06:29 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spAddUpdateOrders]
      @tblOrders OrderType READONLY
AS
BEGIN
      SET NOCOUNT ON;
 
      MERGE INTO Orders t1
      USING @tblOrders t2
      ON t1.OrderId = t2.OrderId and t1.ExchangeOrderId = t2.ExchangeOrderId
	  WHEN MATCHED THEN
	  UPDATE
	  SET	t1.OrderTimestamp = t2.OrderTimestamp,
			t1.ExchangeTimestamp = t2.ExchangeTimestamp,
			t1.Quantity = t2.Quantity,
			t1.CancelledQuantity = t2.CancelledQuantity, 	
			t1.DisclosedQuantity = t2.DisclosedQuantity,
			t1.FilledQuantity = t2.FilledQuantity,
			t1.PendingQuantity = t2.PendingQuantity,
			t1.Price = t2.Price,
			t1.TriggerPrice = t2.TriggerPrice,
			t1.AveragePrice = t2.AveragePrice,
			t1.Product = t2.Product,
			t1.PlacedBy = t2.PlacedBy,
			t1.Validity = t2.Validity,
			t1.Variety = t2.Variety,
			t1.Tag = t2.Tag,
			t1.Status = t2.Status,
			t1.StatusMessage = t2.StatusMessage
      WHEN NOT MATCHED THEN
      INSERT VALUES(t2.OrderId, t2.ExchangeOrderId, t2.ParentOrderId, t2.InstrumentToken, t2.Exchange, 
				t2.OrderTimestamp, t2.ExchangeTimestamp, t2.OrderType, t2.Tradingsymbol, t2.TransactionType, t2.Quantity,
				t2.CancelledQuantity, t2.DisclosedQuantity, t2.FilledQuantity, t2.PendingQuantity, t2.Price,
				t2.TriggerPrice, t2.AveragePrice, t2.Product, t2.PlacedBy, t2.Validity, t2.Variety,
				t2.Tag, t2.Status, t2.StatusMessage);
END


GO


