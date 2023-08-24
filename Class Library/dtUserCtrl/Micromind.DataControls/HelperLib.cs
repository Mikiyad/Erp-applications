using Micromind.ClientLibraries;
using Micromind.DataControls.Libraries;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Micromind.DataControls
{
	public static class HelperLib
	{
		public static List<ComboData> GetDateRangeComboList(bool includeCustom)
		{
			List<ComboData> list = new List<ComboData>();
			list.Add(new ComboData("Today", "0"));
			list.Add(new ComboData("Yesterday", "1"));
			list.Add(new ComboData("This Week", "6"));
			list.Add(new ComboData("Last Week", "7"));
			list.Add(new ComboData("This Month to Date", "2"));
			list.Add(new ComboData("Last Month", "4"));
			list.Add(new ComboData("Last 30 Days", "14"));
			list.Add(new ComboData("This Year to Date", "3"));
			list.Add(new ComboData("Last Year", "15"));
			list.Add(new ComboData("First Quarter", "5"));
			list.Add(new ComboData("Second Quarter", "11"));
			list.Add(new ComboData("Third Quarter", "12"));
			list.Add(new ComboData("Fourth Quarter", "13"));
			list.Add(new ComboData("All Dates", "9"));
			list.Add(new ComboData("Date Equal to", "16"));
			if (includeCustom)
			{
				list.Add(new ComboData("Custom...", "10"));
			}
			return list;
		}

		public static DateRangeStruct GetDateRange(DatePeriods period)
		{
			try
			{
				DateRangeStruct result = default(DateRangeStruct);
				DayOfWeek firstDayOfWeek = CultureInfo.CurrentCulture.DateTimeFormat.FirstDayOfWeek;
				int num = DateTime.Today.DayOfWeek - firstDayOfWeek;
				if (num < 0)
				{
					num += 7;
				}
				int year = DateTime.Today.Year;
				int month = DateTime.Today.Month;
				month = Global.CurrentCalendar.GetMonth(DateTime.Today);
				year = Global.CurrentCalendar.GetYear(DateTime.Today);
				Calendar currentCalendar = Global.CurrentCalendar;
				switch (period)
				{
				case DatePeriods.Today:
					result.From = CreateStartDate(DateTime.Today);
					result.To = CreateEndDate(DateTime.Today);
					break;
				case DatePeriods.Yesterday:
					result.From = CreateStartDate(DateTime.Today.AddDays(-1.0));
					result.To = CreateEndDate(DateTime.Today.AddDays(-1.0));
					break;
				case DatePeriods.ThisWeek:
					result.From = CreateStartDate(DateTime.Today.AddDays(-1 * num));
					result.To = CreateEndDate(result.From.AddDays(7.0).AddSeconds(-1.0));
					break;
				case DatePeriods.ThisMonthToDate:
					result.From = CreateStartDate(new DateTime(year, month, 1, currentCalendar));
					result.To = CreateEndDate(DateTime.Today);
					break;
				case DatePeriods.ThisYearToDate:
					result.From = CreateStartDate(new DateTime(year, 1, 1, currentCalendar));
					result.To = CreateEndDate(DateTime.Today);
					break;
				case DatePeriods.LastYear:
					result.From = CreateStartDate(new DateTime(year - 1, 1, 1, currentCalendar));
					result.To = CreateStartDate(new DateTime(year, 1, 1, currentCalendar)).AddMilliseconds(-1.0);
					break;
				case DatePeriods.LastWeek:
					result.From = CreateStartDate(DateTime.Today.AddDays(-1 * num).AddDays(-7.0));
					result.To = CreateEndDate(result.From.AddDays(7.0).AddSeconds(-1.0));
					break;
				case DatePeriods.FirstQuarter:
					result.From = CreateStartDate(new DateTime(year, 1, 1, currentCalendar));
					result.To = CreateEndDate(currentCalendar.AddDays(new DateTime(year, 4, 1, currentCalendar), -1));
					break;
				case DatePeriods.SecondQuarter:
					result.From = CreateStartDate(new DateTime(year, 4, 1, currentCalendar));
					result.To = CreateEndDate(currentCalendar.AddDays(new DateTime(year, 7, 1, currentCalendar), -1));
					break;
				case DatePeriods.ThirdQuarter:
					result.From = CreateStartDate(new DateTime(year, 7, 1, currentCalendar));
					result.To = CreateEndDate(currentCalendar.AddDays(new DateTime(year, 10, 1, currentCalendar), -1));
					break;
				case DatePeriods.FourthQuarter:
					result.From = CreateStartDate(new DateTime(year, 10, 1, currentCalendar));
					result.To = CreateEndDate(currentCalendar.AddDays(new DateTime(year + 1, 1, 1, currentCalendar), -1));
					break;
				case DatePeriods.Last30Days:
					result.From = CreateStartDate(currentCalendar.AddDays(DateTime.Today, -30));
					result.To = CreateEndDate(DateTime.Today);
					break;
				case DatePeriods.LastMonth:
					result.From = CreateStartDate(new DateTime(currentCalendar.GetYear(currentCalendar.AddMonths(DateTime.Today, -1)), currentCalendar.GetMonth(currentCalendar.AddMonths(DateTime.Today, -1)), 1, currentCalendar));
					result.To = CreateEndDate(new DateTime(currentCalendar.GetYear(result.From), currentCalendar.GetMonth(result.From), GetLastDayOfMonth(currentCalendar.GetYear(result.From), currentCalendar.GetMonth(result.From)), currentCalendar));
					break;
				case DatePeriods.AllDates:
					result.From = CreateStartDate(currentCalendar.MinSupportedDateTime);
					result.To = CreateEndDate(currentCalendar.MaxSupportedDateTime);
					break;
				case DatePeriods.DateEqualTo:
					result.From = CreateStartDate(DateTime.Today.AddDays(-3.0));
					result.To = CreateEndDate(DateTime.Today.AddDays(-3.0));
					break;
				}
				return result;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				throw;
			}
		}

		private static int GetLastDayOfMonth(int year, int month)
		{
			try
			{
				DateTime time = new DateTime(year, month, 1, Global.CurrentCalendar);
				time = Global.CurrentCalendar.AddMonths(time, 1);
				return Global.CurrentCalendar.AddDays(time, -1).Day;
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
				throw;
			}
		}

		public static DateTime CreateStartDate(DateTime date)
		{
			Calendar currentCalendar = Global.CurrentCalendar;
			int year = currentCalendar.GetYear(date);
			int month = currentCalendar.GetMonth(date);
			int dayOfMonth = currentCalendar.GetDayOfMonth(date);
			date = new DateTime(year, month, dayOfMonth, 0, 0, 0, 0, Global.CurrentCalendar);
			return date;
		}

		public static DateTime CreateEndDate(DateTime date)
		{
			Calendar currentCalendar = Global.CurrentCalendar;
			int year = currentCalendar.GetYear(date);
			int month = currentCalendar.GetMonth(date);
			int dayOfMonth = currentCalendar.GetDayOfMonth(date);
			date = new DateTime(year, month, dayOfMonth, 23, 59, 59, 59, currentCalendar);
			return date;
		}
	}
}
