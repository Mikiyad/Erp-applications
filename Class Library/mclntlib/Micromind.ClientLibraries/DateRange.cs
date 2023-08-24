using System;

namespace Micromind.ClientLibraries
{
	public class DateRange
	{
		private DateTime toDate;

		private DateTime fromDate;

		public static DateTime SmallDateTimeFrom = new DateTime(1900, 1, 1);

		public static DateTime SmallDateTimeTo = new DateTime(2079, 6, 6);

		public DateTime ToDate
		{
			get
			{
				return toDate;
			}
			set
			{
				toDate = value;
			}
		}

		public DateTime FromDate
		{
			get
			{
				return fromDate;
			}
			set
			{
				fromDate = value;
			}
		}

		public static DateRange ThisMonthToDate => new DateRange
		{
			fromDate = CreateFromDate(new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1)),
			toDate = CreateToDate(DateTime.Today)
		};

		public static DateRange Today => new DateRange
		{
			fromDate = CreateFromDate(DateTime.Today),
			toDate = CreateToDate(DateTime.Today)
		};

		private static DateTime CreateFromDate(DateTime date)
		{
			date = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0, 0);
			return date;
		}

		private static DateTime CreateToDate(DateTime date)
		{
			date = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59, 59);
			return date;
		}

		public static DateRange LastWeek(DateTime date)
		{
			DateRange dateRange = ThisWeek(date);
			dateRange.fromDate = dateRange.fromDate.AddDays(-7.0);
			dateRange.toDate = dateRange.toDate.AddDays(-7.0);
			return dateRange;
		}

		public static DateRange ThisWeek(DateTime date)
		{
			DateRange obj = new DateRange
			{
				fromDate = date.Date.AddDays(checked(0 - date.DayOfWeek))
			};
			obj.toDate = obj.fromDate.AddDays(7.0).AddMilliseconds(-1.0);
			return obj;
		}

		public static DateRange LastMonth(DateTime date)
		{
			DateRange obj = new DateRange
			{
				fromDate = new DateTime(date.Year, date.Month, 1).AddMonths(-1)
			};
			obj.toDate = obj.fromDate.AddMonths(1).AddMilliseconds(-1.0);
			return obj;
		}

		public static DateRange ThisMonth(DateTime date)
		{
			DateRange obj = new DateRange
			{
				fromDate = new DateTime(date.Year, date.Month, 1)
			};
			obj.toDate = obj.fromDate.AddMonths(1).AddMilliseconds(-1.0);
			return obj;
		}

		public static DateRange ThisYear(DateTime date)
		{
			DateRange obj = new DateRange
			{
				fromDate = new DateTime(date.Year, 1, 1)
			};
			obj.toDate = obj.fromDate.AddYears(1).AddMilliseconds(-1.0);
			return obj;
		}

		public static DateRange LastYear(DateTime date)
		{
			DateRange obj = new DateRange
			{
				fromDate = new DateTime(checked(date.Year - 1), 1, 1)
			};
			obj.toDate = obj.fromDate.AddYears(1).AddMilliseconds(-1.0);
			return obj;
		}
	}
}
