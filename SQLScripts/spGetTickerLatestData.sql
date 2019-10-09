/****** Object:  StoredProcedure [dbo].[spGetTickerLatestData]    Script Date: 9 Oct 2019 20:56:34 ******/
DROP PROCEDURE [dbo].[spGetTickerLatestData]
GO

/****** Object:  StoredProcedure [dbo].[spGetTickerLatestData]    Script Date: 9 Oct 2019 20:56:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[spGetTickerLatestData]
      @InstrumentList varchar(2500),
	  @DateTill date
AS
BEGIN

	SELECT InstrumentToken, TradingSymbol, [DateTime], [Open], High, Low, [Close], Volume
	FROM (
		SELECT InstrumentToken, TradingSymbol, [DateTime], [Open], High, Low, [Close], Volume, 
		ROW_NUMBER() OVER(partition BY TradingSymbol ORDER BY [DateTime] DESC) AS RankValue
		FROM TICKERMIN WHERE [DateTime] > @DateTill and TradingSymbol IN (SELECT * FROM ufn_CSVToTable(@InstrumentList,',')) ) i
	Where RankValue = 1
	Order by TradingSymbol, [DateTime]
		
END

GO


