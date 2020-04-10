USE [TradeGenie]
GO

/****** Object:  StoredProcedure [dbo].[spGetOrders]    Script Date: 18-03-2018 23:29:11 ******/
DROP PROCEDURE [dbo].[spGetOrders]
GO

/****** Object:  StoredProcedure [dbo].[spGetOrders]    Script Date: 18-03-2018 23:29:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spGetOrders]
      @dtDate date 
AS
BEGIN
      SET NOCOUNT ON;
 
	  SELECT * FROM Orders
	  WHERE CONVERT(DATE,OrderTimestamp) = @dtDate
END


GO


