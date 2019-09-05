Imports UpstoxNet
Imports AzTGDataLayer

Module GlobalVariables
    Public Upstox As New Upstox

    Public MarketWatchFileName As String = "TickerListHelloUpstox.txt"
    Public LastSelectedPage As String = ""

    Public AccessCodeLocal As String = ""
    Public DisableMarketWatch As Boolean = My.Settings.DisableMarketWatch
    Public ListMarketWatchSymbol As New List(Of String)
    Public LockAddSymbol As New Object
    Public LockUpdateMarketWatchFile As New Object
    Public AzDbMethods As New DbMethods

    Public APIRootEndPoint As String = "https://api.upstox.com"
    Public LoginUrl As String = "https://api.upstox.com"

    Public ReadOnly ScreenWidth As Integer = Screen.PrimaryScreen.WorkingArea.Width
    Public ReadOnly ScreenHeight As Integer = Screen.PrimaryScreen.WorkingArea.Height

    'Default Column Headers
    Public OrderBookHeader As String = "EXCHANGE,TOKEN,SYMBOL,PRODUCT,ORDER_TYPE,DURATION,PRICE,TRIGGER_PRICE,QUANTITY,DISCLOSED_QUANTITY,TRANSACTION_TYPE,AVERAGE_PRICE,TRADED_QUANTITY,MESSAGE,EXCHANGE_ORDER_ID,PARENT_ORDER_ID,ORDER_ID,EXCHANGE_TIME,TIME_IN_MICRO,STATUS,IS_AMO,VALID_DATE,ORDER_REQUEST_ID"
    Public TradeBookHeader As String = "EXCHANGE,TOKEN,SYMBOL,PRODUCT,ORDER_TYPE,TRANSACTION_TYPE,TRADED_QUANTITY,EXCHANGE_ORDER_ID,ORDER_ID,EXCHANGE_TIME,TIME_IN_MICRO,TRADED_PRICE,TRADE_ID"
    Public PositionsHeader As String = "EXCHANGE,PRODUCT,SYMBOL,TOKEN,BUY_AMOUNT,SELL_AMOUNT,BUY_QUANTITY,SELL_QUANTITY,CF_BUY_AMOUNT,CF_SELL_AMOUNT,CF_BUY_QUANTITY,CF_SELL_QUANTITY,AVG_BUY_PRICE,AVG_SELL_PRICE,NET_QUANTITY,CLOSE_PRICE,LAST_TRADED_PRICE,REALIZED_PROFIT,UNREALIZED_PROFIT,CF_AVG_PRICE"
    Public FundsHeader As String = "SEGMENT,USED_MARGIN,UNREALIZED_MTM,REALIZED_MTM,PAYIN_AMOUNT,SPAN_MARGIN,ADHOC_MARGIN,NOTIONAL_CASH,AVAILABLE_MARGIN,EXPOSURE_MARGIN"
    Public HoldingsHeader As String = "EXCHANGE,SYMBOL,TOKEN,PRODUCT,COLLATERAL_TYPE ,CNC_USED_QUANTITY,QUANTITY,COLLATERAL_QTY,HAIRCUT,AVG_PRICE"

    Public BridgePositionsHeader As String = "EXCHANGE,TRADE_SYMBOL,STGY_CODE,SOURCE,MODE,LONG_TRADES,SHORT_TRADES,TOTAL_TRADES,BUY_QTY,AVG_BUY_PRICE,SELL_QTY,AVG_SELL_PRICE,NET_QTY, MTM,LAST_TRADE_PRICE"
    Public BridgeLogsHeader As String = "TIME,ACTION,SOURCE,TIMESTAMP,EXCH,TRADE_SYMBOL,SIGNAL_TYPE,QTY,ORDER_TYPE,PROD_TYPE,LMT_PRICE,TRG_PRICE,VALIDITY,DISCQTY,CTAG,STGY_CODE,ISLIVE,ISASYNC,TGT_POINTS,SL_POINTS,TRAILTICKS,SL_PRICE,TAG,ORDERID,PARENTID,INST_TOKEN,EXCH_TOKEN,RESPONSE"

    Public BridgePositionsAllHeader As String = "STGY_CODE,SOURCE,MODE,TOTAL_POSITIONS,OPEN_POSITIONS,CLOSED_POSITIONS,TOTAL_MTM"

    'CellIndex
    Public Index_OrderId_OrderBook As Integer = 16
    Public Index_ParentId_OrderBook As Integer = 15
    Public Index_Status_OrderBook As Integer = 19
    Public Index_ProdType_OrderBook As Integer = 3
    Public Index_OrdType_OrderBook As Integer = 4
    Public Index_Trans_OrderBook As Integer = 10
    Public Index_Exch_OrderBook As Integer = 0
    Public Index_Token_OrderBook As Integer = 1
    Public Index_TrdSym_OrderBook As Integer = 2
    Public Index_LmtPrice_OrderBook As Integer = 6
    Public Index_TrgPrice_OrderBook As Integer = 7
    Public Index_Qty_OrderBook As Integer = 8

    Public Index_Exch_Positions As Integer = 0
    Public Index_TrdSym_Positions As Integer = 2
    Public Index_Token_Positions As Integer = 3
    Public Index_ProdType_Positions As Integer = 1
    Public Index_NetQty_Positions As Integer = 14

    Public MsgBoxTitle As String = "Hello Upstox"

End Module

Public Enum BoxStyle
    Information = 1
    Critical = 2
    Warning = 3
    Exclamation = 4
End Enum

Public Enum LogType
    AppEvent = 1
    OrderUpdate = 2
    PositionUpdate = 3
    Debug = 4
    Others = 5
End Enum
