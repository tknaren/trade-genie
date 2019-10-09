/****** Object:  StoredProcedure [dbo].[spMergeTicker]    Script Date: 9 Oct 2019 17:11:39 ******/
DROP PROCEDURE [dbo].[spMergeTicker]
GO

/****** Object:  StoredProcedure [dbo].[spMergeTicker]    Script Date: 9 Oct 2019 17:11:39 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[spMergeTicker]
AS
BEGIN
      SET NOCOUNT ON;
 
      MERGE INTO TickerMin WITH (TABLOCK) t1 
      USING TickerMinStage t2
      ON t1.InstrumentToken = t2.InstrumentToken and t1.TradingSymbol = t2.TradingSymbol and t1.[DateTime] = t2.[DateTime]
	  WHEN MATCHED THEN 
	  UPDATE SET 
		t1.[Open] = t2.[Open], 
		t1.[High] = t2.[High], 
		t1.[Low] = t2.[Low], 
		t1.[Close] = t2.[Close], 
		t1.Volume = t2.Volume
      WHEN NOT MATCHED THEN
      INSERT VALUES(t2.InstrumentToken, t2.TradingSymbol, t2.[DateTime], t2.[Open], t2.[High], t2.[Low], t2.[Close], t2.Volume);

	  TRUNCATE TABLE TickerMinStage
END


GO


