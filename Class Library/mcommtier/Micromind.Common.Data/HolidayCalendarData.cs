using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class HolidayCalendarData : DataSet
	{
		public const string CALENDARID_FIELD = "CalendarID";

		public const string CALENDARNAME_FIELD = "CalendarName";

		public const string REMARKS_FIELD = "Remarks";

		public const string OFFDAYS_FIELD = "OffDays";

		public const string OFFDATEFROM_FIELD = "OffDateFrom";

		public const string OFFDATETO_FIELD = "OffDateTo";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string APPROVALSTATUS_FIELD = "ApprovalStatus";

		public const string VERIFICATIONSTATUS_FIELD = "VerificationStatus";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string FROMDATE_FIELD = "FromDate";

		public const string TODATE_FIELD = "ToDate";

		public const string DAYS_FIELD = "Days";

		public const string HOLIDAYTYPE_FIELD = "HolidayType";

		public const string HOLIDAYCALENDAR_TABLE = "Holiday_Calendar";

		public const string HOLIDAYCALENDARDETAIL_TABLE = "Holiday_Calendar_Detail";

		public DataTable HolidayCalendarTable => base.Tables["Holiday_Calendar"];

		public DataTable HolidayCalendarDetailTable => base.Tables["Holiday_Calendar_Detail"];

		public HolidayCalendarData()
		{
			BuildDataTables();
		}

		public HolidayCalendarData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Holiday_Calendar");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CalendarID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CalendarName", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("OffDays", typeof(string));
			columns.Add("OffDateFrom", typeof(DateTime));
			columns.Add("OffDateTo", typeof(DateTime));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Holiday_Calendar_Detail");
			columns = dataTable.Columns;
			columns.Add("CalendarID", typeof(string));
			columns.Add("FromDate", typeof(DateTime));
			columns.Add("ToDate", typeof(DateTime));
			columns.Add("Days", typeof(long));
			columns.Add("RowIndex", typeof(short));
			columns.Add("HolidayType", typeof(string));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
