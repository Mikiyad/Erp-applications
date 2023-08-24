using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EventData : DataSet
	{
		public const string EVENTID_FIELD = "EventID";

		public const string EVENTNAME_FIELD = "EventName";

		public const string TYPE_FIELD = "Type";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string LEADID_FIELD = "LeadID";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string EVENTS_TABLE = "Events";

		public const string EVENTEMPLOYEE_TABLE = "Event_Employee";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public DataTable EventTable => base.Tables["Events"];

		public DataTable EventEmployeeTable => base.Tables["Event_Employee"];

		public EventData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public EventData()
		{
			BuildDataTables();
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Events");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EventID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("EventName", typeof(string)).AllowDBNull = false;
			columns.Add("StartDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("EndDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("Type", typeof(string));
			columns.Add("LeadID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("CreatedBy", typeof(string));
			columns.Add("DateCreated", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("UpdatedBy", typeof(string));
			columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now;
			base.Tables.Add(dataTable);
			DataTable dataTable2 = new DataTable("Event_Employee");
			columns = dataTable2.Columns;
			columns.Add("EventID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			base.Tables.Add(dataTable2);
		}
	}
}
