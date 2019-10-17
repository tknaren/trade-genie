﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccessLayer
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class aztgsqldbEntities : DbContext
    {
        public aztgsqldbEntities()
            : base("name=SQLAZURECONNSTR_aztgsqldbEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<GapStrategyPotentialOrder> GapStrategyPotentialOrders { get; set; }
        public virtual DbSet<Instrument> Instruments { get; set; }
        public virtual DbSet<Margin> Margins { get; set; }
        public virtual DbSet<MasterStockList> MasterStockLists { get; set; }
        public virtual DbSet<MorningBreakout> MorningBreakouts { get; set; }
        public virtual DbSet<NetPosition> NetPositions { get; set; }
        public virtual DbSet<OHLCData> OHLCDatas { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Quote> Quotes { get; set; }
        public virtual DbSet<TickerMin> TickerMins { get; set; }
        public virtual DbSet<TickerMinElderIndicator> TickerMinElderIndicators { get; set; }
        public virtual DbSet<TickerMinEMAHA> TickerMinEMAHAs { get; set; }
        public virtual DbSet<TickerMinSuperTrend> TickerMinSuperTrends { get; set; }
        public virtual DbSet<Trade> Trades { get; set; }
        public virtual DbSet<UserLogin> UserLogins { get; set; }
        public virtual DbSet<GetDistinctFOStock> GetDistinctFOStocks { get; set; }
        public virtual DbSet<GetDistinctNiftyStock> GetDistinctNiftyStocks { get; set; }
    
        [DbFunction("SQLAZURECONNSTR_aztgsqldbEntities", "ufn_CSVToTable")]
        public virtual IQueryable<ufn_CSVToTable_Result> ufn_CSVToTable(string stringInput, string delimiter)
        {
            var stringInputParameter = stringInput != null ?
                new ObjectParameter("StringInput", stringInput) :
                new ObjectParameter("StringInput", typeof(string));
    
            var delimiterParameter = delimiter != null ?
                new ObjectParameter("Delimiter", delimiter) :
                new ObjectParameter("Delimiter", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.CreateQuery<ufn_CSVToTable_Result>("[SQLAZURECONNSTR_aztgsqldbEntities].[ufn_CSVToTable](@StringInput, @Delimiter)", stringInputParameter, delimiterParameter);
        }
    
        public virtual int spAddUpdateOrders()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spAddUpdateOrders");
        }
    
        public virtual int spGenerateOHLC(string strDateTime, Nullable<int> intMinuteBar)
        {
            var strDateTimeParameter = strDateTime != null ?
                new ObjectParameter("strDateTime", strDateTime) :
                new ObjectParameter("strDateTime", typeof(string));
    
            var intMinuteBarParameter = intMinuteBar.HasValue ?
                new ObjectParameter("intMinuteBar", intMinuteBar) :
                new ObjectParameter("intMinuteBar", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spGenerateOHLC", strDateTimeParameter, intMinuteBarParameter);
        }
    
        public virtual ObjectResult<spGetGapOpenedScripts_Result> spGetGapOpenedScripts(Nullable<System.DateTime> yesterday, Nullable<System.DateTime> today, Nullable<int> targetPercentage, Nullable<int> gapPercentage, Nullable<int> priceRangeHigh, Nullable<int> priceRangeLow)
        {
            var yesterdayParameter = yesterday.HasValue ?
                new ObjectParameter("yesterday", yesterday) :
                new ObjectParameter("yesterday", typeof(System.DateTime));
    
            var todayParameter = today.HasValue ?
                new ObjectParameter("today", today) :
                new ObjectParameter("today", typeof(System.DateTime));
    
            var targetPercentageParameter = targetPercentage.HasValue ?
                new ObjectParameter("targetPercentage", targetPercentage) :
                new ObjectParameter("targetPercentage", typeof(int));
    
            var gapPercentageParameter = gapPercentage.HasValue ?
                new ObjectParameter("gapPercentage", gapPercentage) :
                new ObjectParameter("gapPercentage", typeof(int));
    
            var priceRangeHighParameter = priceRangeHigh.HasValue ?
                new ObjectParameter("priceRangeHigh", priceRangeHigh) :
                new ObjectParameter("priceRangeHigh", typeof(int));
    
            var priceRangeLowParameter = priceRangeLow.HasValue ?
                new ObjectParameter("priceRangeLow", priceRangeLow) :
                new ObjectParameter("priceRangeLow", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetGapOpenedScripts_Result>("spGetGapOpenedScripts", yesterdayParameter, todayParameter, targetPercentageParameter, gapPercentageParameter, priceRangeHighParameter, priceRangeLowParameter);
        }
    
        public virtual ObjectResult<spGetScriptsForBackTest_Result> spGetScriptsForBackTest(Nullable<System.DateTime> strCurrentDate, Nullable<System.DateTime> strPrevDate, Nullable<System.DateTime> strPrevToPrevDate, Nullable<int> intMinuteBar, string position)
        {
            var strCurrentDateParameter = strCurrentDate.HasValue ?
                new ObjectParameter("strCurrentDate", strCurrentDate) :
                new ObjectParameter("strCurrentDate", typeof(System.DateTime));
    
            var strPrevDateParameter = strPrevDate.HasValue ?
                new ObjectParameter("strPrevDate", strPrevDate) :
                new ObjectParameter("strPrevDate", typeof(System.DateTime));
    
            var strPrevToPrevDateParameter = strPrevToPrevDate.HasValue ?
                new ObjectParameter("strPrevToPrevDate", strPrevToPrevDate) :
                new ObjectParameter("strPrevToPrevDate", typeof(System.DateTime));
    
            var intMinuteBarParameter = intMinuteBar.HasValue ?
                new ObjectParameter("intMinuteBar", intMinuteBar) :
                new ObjectParameter("intMinuteBar", typeof(int));
    
            var positionParameter = position != null ?
                new ObjectParameter("position", position) :
                new ObjectParameter("position", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetScriptsForBackTest_Result>("spGetScriptsForBackTest", strCurrentDateParameter, strPrevDateParameter, strPrevToPrevDateParameter, intMinuteBarParameter, positionParameter);
        }
    
        public virtual ObjectResult<spGetStocksInMomentum_Result> spGetStocksInMomentum(Nullable<System.DateTime> strCurrentDate, Nullable<System.DateTime> strPrevDate, Nullable<System.DateTime> strPrevToPrevDate, Nullable<int> intMinuteBar, string position)
        {
            var strCurrentDateParameter = strCurrentDate.HasValue ?
                new ObjectParameter("strCurrentDate", strCurrentDate) :
                new ObjectParameter("strCurrentDate", typeof(System.DateTime));
    
            var strPrevDateParameter = strPrevDate.HasValue ?
                new ObjectParameter("strPrevDate", strPrevDate) :
                new ObjectParameter("strPrevDate", typeof(System.DateTime));
    
            var strPrevToPrevDateParameter = strPrevToPrevDate.HasValue ?
                new ObjectParameter("strPrevToPrevDate", strPrevToPrevDate) :
                new ObjectParameter("strPrevToPrevDate", typeof(System.DateTime));
    
            var intMinuteBarParameter = intMinuteBar.HasValue ?
                new ObjectParameter("intMinuteBar", intMinuteBar) :
                new ObjectParameter("intMinuteBar", typeof(int));
    
            var positionParameter = position != null ?
                new ObjectParameter("position", position) :
                new ObjectParameter("position", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetStocksInMomentum_Result>("spGetStocksInMomentum", strCurrentDateParameter, strPrevDateParameter, strPrevToPrevDateParameter, intMinuteBarParameter, positionParameter);
        }
    
        public virtual ObjectResult<spGetStocksWithGapOpening_Result> spGetStocksWithGapOpening(Nullable<System.DateTime> yesterday, Nullable<System.DateTime> today, Nullable<int> percentage, Nullable<int> stockPrice)
        {
            var yesterdayParameter = yesterday.HasValue ?
                new ObjectParameter("yesterday", yesterday) :
                new ObjectParameter("yesterday", typeof(System.DateTime));
    
            var todayParameter = today.HasValue ?
                new ObjectParameter("today", today) :
                new ObjectParameter("today", typeof(System.DateTime));
    
            var percentageParameter = percentage.HasValue ?
                new ObjectParameter("percentage", percentage) :
                new ObjectParameter("percentage", typeof(int));
    
            var stockPriceParameter = stockPrice.HasValue ?
                new ObjectParameter("stockPrice", stockPrice) :
                new ObjectParameter("stockPrice", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetStocksWithGapOpening_Result>("spGetStocksWithGapOpening", yesterdayParameter, todayParameter, percentageParameter, stockPriceParameter);
        }
    
        public virtual ObjectResult<spGetTickerDataForIndicators_Result> spGetTickerDataForIndicators(string instrumentList, string timePeriods, Nullable<System.DateTime> dateFrom)
        {
            var instrumentListParameter = instrumentList != null ?
                new ObjectParameter("InstrumentList", instrumentList) :
                new ObjectParameter("InstrumentList", typeof(string));
    
            var timePeriodsParameter = timePeriods != null ?
                new ObjectParameter("TimePeriods", timePeriods) :
                new ObjectParameter("TimePeriods", typeof(string));
    
            var dateFromParameter = dateFrom.HasValue ?
                new ObjectParameter("DateFrom", dateFrom) :
                new ObjectParameter("DateFrom", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetTickerDataForIndicators_Result>("spGetTickerDataForIndicators", instrumentListParameter, timePeriodsParameter, dateFromParameter);
        }
    
        public virtual ObjectResult<spGetTickerElderForTimePeriod_Result> spGetTickerElderForTimePeriod(string instrumentList, string timePeriods, Nullable<System.DateTime> startTime, Nullable<System.DateTime> endTime)
        {
            var instrumentListParameter = instrumentList != null ?
                new ObjectParameter("InstrumentList", instrumentList) :
                new ObjectParameter("InstrumentList", typeof(string));
    
            var timePeriodsParameter = timePeriods != null ?
                new ObjectParameter("TimePeriods", timePeriods) :
                new ObjectParameter("TimePeriods", typeof(string));
    
            var startTimeParameter = startTime.HasValue ?
                new ObjectParameter("StartTime", startTime) :
                new ObjectParameter("StartTime", typeof(System.DateTime));
    
            var endTimeParameter = endTime.HasValue ?
                new ObjectParameter("EndTime", endTime) :
                new ObjectParameter("EndTime", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetTickerElderForTimePeriod_Result>("spGetTickerElderForTimePeriod", instrumentListParameter, timePeriodsParameter, startTimeParameter, endTimeParameter);
        }
    
        public virtual ObjectResult<spGetTickerLatestData_Result> spGetTickerLatestData(string instrumentList, Nullable<System.DateTime> dateTill)
        {
            var instrumentListParameter = instrumentList != null ?
                new ObjectParameter("InstrumentList", instrumentList) :
                new ObjectParameter("InstrumentList", typeof(string));
    
            var dateTillParameter = dateTill.HasValue ?
                new ObjectParameter("DateTill", dateTill) :
                new ObjectParameter("DateTill", typeof(System.DateTime));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spGetTickerLatestData_Result>("spGetTickerLatestData", instrumentListParameter, dateTillParameter);
        }
    
        public virtual ObjectResult<spImpulseMediumFrame_Result> spImpulseMediumFrame(Nullable<System.DateTime> strCurrentDate, Nullable<System.DateTime> strPrevDate, Nullable<int> intMinuteBar, string position)
        {
            var strCurrentDateParameter = strCurrentDate.HasValue ?
                new ObjectParameter("strCurrentDate", strCurrentDate) :
                new ObjectParameter("strCurrentDate", typeof(System.DateTime));
    
            var strPrevDateParameter = strPrevDate.HasValue ?
                new ObjectParameter("strPrevDate", strPrevDate) :
                new ObjectParameter("strPrevDate", typeof(System.DateTime));
    
            var intMinuteBarParameter = intMinuteBar.HasValue ?
                new ObjectParameter("intMinuteBar", intMinuteBar) :
                new ObjectParameter("intMinuteBar", typeof(int));
    
            var positionParameter = position != null ?
                new ObjectParameter("position", position) :
                new ObjectParameter("position", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<spImpulseMediumFrame_Result>("spImpulseMediumFrame", strCurrentDateParameter, strPrevDateParameter, intMinuteBarParameter, positionParameter);
        }
    
        public virtual int spMergeTicker()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spMergeTicker");
        }
    
        public virtual int spStockStatistics(Nullable<System.DateTime> strCurrentDate, Nullable<System.DateTime> strPrevDate, Nullable<int> intMinuteBar)
        {
            var strCurrentDateParameter = strCurrentDate.HasValue ?
                new ObjectParameter("strCurrentDate", strCurrentDate) :
                new ObjectParameter("strCurrentDate", typeof(System.DateTime));
    
            var strPrevDateParameter = strPrevDate.HasValue ?
                new ObjectParameter("strPrevDate", strPrevDate) :
                new ObjectParameter("strPrevDate", typeof(System.DateTime));
    
            var intMinuteBarParameter = intMinuteBar.HasValue ?
                new ObjectParameter("intMinuteBar", intMinuteBar) :
                new ObjectParameter("intMinuteBar", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spStockStatistics", strCurrentDateParameter, strPrevDateParameter, intMinuteBarParameter);
        }
    
        public virtual int spUpdateOHLC()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spUpdateOHLC");
        }
    
        public virtual int spUpdateRealTime()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spUpdateRealTime");
        }
    
        public virtual int spUpdateRealTimeFO()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spUpdateRealTimeFO");
        }
    
        public virtual int spUpdateTicker()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spUpdateTicker");
        }
    
        public virtual int spUpdateTickerElder(Nullable<int> timePeriod)
        {
            var timePeriodParameter = timePeriod.HasValue ?
                new ObjectParameter("timePeriod", timePeriod) :
                new ObjectParameter("timePeriod", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spUpdateTickerElder", timePeriodParameter);
        }
    
        public virtual int spUpdateTickerElderIndicators()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("spUpdateTickerElderIndicators");
        }
    
        public virtual ObjectResult<RealTimeGapOpenedScripts_Result> RealTimeGapOpenedScripts(Nullable<System.DateTime> yesterday, Nullable<System.DateTime> today, 
            Nullable<double> targetPercentage, Nullable<double> gapPercentage, Nullable<int> priceRangeHigh, Nullable<int> priceRangeLow)
        {
            var yesterdayParameter = yesterday.HasValue ?
                new ObjectParameter("yesterday", yesterday) :
                new ObjectParameter("yesterday", typeof(System.DateTime));
    
            var todayParameter = today.HasValue ?
                new ObjectParameter("today", today) :
                new ObjectParameter("today", typeof(System.DateTime));
    
            var targetPercentageParameter = targetPercentage.HasValue ?
                new ObjectParameter("targetPercentage", targetPercentage) :
                new ObjectParameter("targetPercentage", typeof(double));
    
            var gapPercentageParameter = gapPercentage.HasValue ?
                new ObjectParameter("gapPercentage", gapPercentage) :
                new ObjectParameter("gapPercentage", typeof(double));
    
            var priceRangeHighParameter = priceRangeHigh.HasValue ?
                new ObjectParameter("priceRangeHigh", priceRangeHigh) :
                new ObjectParameter("priceRangeHigh", typeof(int));
    
            var priceRangeLowParameter = priceRangeLow.HasValue ?
                new ObjectParameter("priceRangeLow", priceRangeLow) :
                new ObjectParameter("priceRangeLow", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<RealTimeGapOpenedScripts_Result>("RealTimeGapOpenedScripts", yesterdayParameter, todayParameter, targetPercentageParameter, gapPercentageParameter, priceRangeHighParameter, priceRangeLowParameter);
        }
    }
}
