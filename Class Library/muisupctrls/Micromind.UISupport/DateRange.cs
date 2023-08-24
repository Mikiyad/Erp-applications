using System;

namespace Micromind.UISupport
{
	public class DateRange
	{
		private object name = "";

		private bool hasCustomDate;

		private bool hasNoDate;

		private DateTime from = DateTime.MinValue;

		private DateTime to = DateTime.MinValue;

		public static DateTime SmallDateTimeFrom = new DateTime(1900, 1, 1);

		public static DateTime SmallDateTimeTo = new DateTime(2079, 6, 6);

		public object Name
		{
			get
			{
				return name;
			}
			set
			{
				name = value;
			}
		}

		public object Value => this;

		public DateTime From
		{
			get
			{
				return from;
			}
			set
			{
				from = value;
			}
		}

		public DateTime To
		{
			get
			{
				return to;
			}
			set
			{
				to = value;
			}
		}

		public bool HasCustomDate
		{
			get
			{
				return hasCustomDate;
			}
			set
			{
				hasCustomDate = value;
			}
		}

		public bool HasNoDate
		{
			get
			{
				return hasNoDate;
			}
			set
			{
				hasNoDate = value;
			}
		}

		public DateRange(string name, DateTime from, DateTime to)
		{
			this.name = name;
			this.from = from;
			this.to = to;
		}
	}
}
