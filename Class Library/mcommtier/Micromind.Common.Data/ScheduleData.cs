using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ScheduleData : DataSet
	{
		public const string SCHEDULES_TABLE = "Schedules";

		public const string SCHEDULEID_FIELD = "ScheduleID";

		public const string SUBJECT_FIELD = "Subject";

		public const string STATUS_FIELD = "Status";

		public const string STARTINGDATE_FIELD = "StartingDate";

		public const string DUEDATE_FIELD = "DueDate";

		public const string ISRECURRING_FIELD = "IsRecurring";

		public const string RECURRINGPATTERN_FIELD = "RecurringPattern";

		public const string PRIORITY_FIELD = "Priority";

		public const string DAYSOFWEEK_FIELD = "DaysOfWeek";

		public const string DAYOFMONTH_FIELD = "DayOfMonth";

		public const string MONTHOFYEAR_FIELD = "MonthOfYear";

		public const string DAYOFMONTHINYEAR_FIELD = "DayOfMonthInYear";

		public const string COMPLETEDBY_FIELD = "CompletedBy";

		public const string HASENDDATE_FIELD = "HasEndDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string COMPLETEDDATE_FIELD = "CompletedDate";

		public const string PERCENTCOMPLETED_FIELD = "PercentCompleted";

		public const string NOTE_FIELD = "Note";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public DataTable ScheduleTable => base.Tables["Schedules"];

		public ScheduleData()
		{
			BuildDataTables();
		}

		public ScheduleData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Schedules");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ScheduleID", typeof(int));
			dataColumn.AllowDBNull = true;
			dataColumn.AutoIncrement = false;
			columns.Add("Subject", typeof(string)).AllowDBNull = false;
			columns.Add("Status", typeof(byte)).DefaultValue = ScheduleStatus.Pending;
			columns.Add("StartingDate", typeof(DateTime));
			columns.Add("DueDate", typeof(DateTime)).DefaultValue = DateTime.Today;
			columns.Add("IsRecurring", typeof(bool)).DefaultValue = false;
			columns.Add("RecurringPattern", typeof(byte)).DefaultValue = RecurringPatterns.Once;
			columns.Add("Priority", typeof(byte)).DefaultValue = SchedulePriorities.Medium;
			columns.Add("DaysOfWeek", typeof(string));
			columns.Add("DayOfMonth", typeof(byte)).DefaultValue = DateTime.Today.Day;
			columns.Add("MonthOfYear", typeof(byte)).DefaultValue = DateTime.Today.Month;
			columns.Add("DayOfMonthInYear", typeof(byte)).DefaultValue = DateTime.Today.Day;
			columns.Add("CompletedBy", typeof(int));
			columns.Add("HasEndDate", typeof(bool)).DefaultValue = false;
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("CompletedDate", typeof(DateTime));
			columns.Add("PercentCompleted", typeof(byte)).DefaultValue = 0;
			columns.Add("Note", typeof(string));
			columns.Add("DateTimeStamp", typeof(DateTime));
			base.Tables.Add(dataTable);
		}
	}
}
