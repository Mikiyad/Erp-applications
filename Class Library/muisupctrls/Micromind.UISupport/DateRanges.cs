using Micromind.ClientLibraries;
using System;
using System.Collections;

namespace Micromind.UISupport
{
	public class DateRanges
	{
		private DateTime fiscalDate = DateTime.MinValue;

		private ArrayList dateRangeList = new ArrayList();

		public ArrayList DateRangeList => dateRangeList;

		public DateTime FiscalDate
		{
			get
			{
				if (Global.ConStatus != 0)
				{
					return new DateTime(DateTime.Now.Year, 1, 1);
				}
				if (fiscalDate == DateTime.MinValue)
				{
					fiscalDate = Factory.CompanyInformationSystem.GetFiscalStartDate();
				}
				return fiscalDate;
			}
		}

		public void Load()
		{
			dateRangeList.Clear();
			DateRange dateRange = new DateRange("ALL Dates", DateTime.MinValue, DateTime.MaxValue);
			dateRange.HasNoDate = true;
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("Today", DateTime.Today, DateTime.Today);
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("Tomorrow", DateTime.Today.AddDays(1.0), DateTime.Today.AddDays(1.0));
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("Yesterday", DateTime.Today.AddDays(-1.0), DateTime.Today.AddDays(-1.0));
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("This Week", GetThisWeekStart(), GetThisWeekStart().AddDays(6.0));
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("This Week-to-date", GetThisWeekStart(), DateTime.Today);
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("This Month", GetThisMonthStart(), GetThisMonthStart().AddMonths(1).AddDays(-1.0));
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("This Month-to-date", GetThisMonthStart(), DateTime.Today);
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("This Fiscal Year", GetThisFiscalYearStart(), GetThisFiscalYearStart().AddYears(1).AddDays(-1.0));
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("This Fiscal Year-to-date", GetThisFiscalYearStart(), DateTime.Today);
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("Last Week", GetThisWeekStart().AddDays(-7.0), GetThisWeekStart().AddDays(-1.0));
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("Last Month", GetThisMonthStart().AddMonths(-1), GetThisMonthStart().AddDays(-1.0));
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("Last Fiscal Year", GetThisFiscalYearStart().AddYears(-1), GetThisFiscalYearStart().AddDays(-1.0));
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("Next Week", GetThisWeekStart().AddDays(7.0), GetThisWeekStart().AddDays(13.0));
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("Next Month", GetThisMonthStart().AddMonths(1), GetThisMonthStart().AddMonths(2).AddDays(-1.0));
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("Next Fiscal Year", GetThisFiscalYearStart().AddYears(1), GetThisFiscalYearStart().AddYears(2).AddDays(-1.0));
			dateRangeList.Add(dateRange);
			dateRange = new DateRange("Custom Dates...", DateRange.SmallDateTimeFrom, DateRange.SmallDateTimeTo);
			dateRange.HasCustomDate = true;
			dateRangeList.Add(dateRange);
		}

		private DateTime GetThisWeekStart()
		{
			DayOfWeek dayOfWeek = DateTime.Today.DayOfWeek;
			DateTime result = DateTime.Today;
			switch (dayOfWeek)
			{
			case DayOfWeek.Sunday:
				result = DateTime.Today;
				break;
			case DayOfWeek.Monday:
				result = DateTime.Today.AddDays(-1.0);
				break;
			case DayOfWeek.Tuesday:
				result = DateTime.Today.AddDays(-2.0);
				break;
			case DayOfWeek.Wednesday:
				result = DateTime.Today.AddDays(-3.0);
				break;
			case DayOfWeek.Thursday:
				result = DateTime.Today.AddDays(-4.0);
				break;
			case DayOfWeek.Friday:
				result = DateTime.Today.AddDays(-5.0);
				break;
			case DayOfWeek.Saturday:
				result = DateTime.Today.AddDays(-6.0);
				break;
			}
			return result;
		}

		private DateTime GetThisMonthStart()
		{
			return new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
		}

		private DateTime GetThisFiscalYearStart()
		{
			return FiscalDate;
		}
	}
}
