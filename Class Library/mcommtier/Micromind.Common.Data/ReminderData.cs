using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ReminderData : DataSet
	{
		public const string REMINDERSETTING_TABLE = "Reminder_Setting";

		public const string REMINDERID_FIELD = "ReminderID";

		public const string USERID_FIELD = "UserID";

		public const string DAYS_FIELD = "Days";

		public const string ISSELECTED_FIELD = "IsSelected";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ReminderSettingTable => base.Tables["Reminder_Setting"];

		public ReminderData()
		{
			BuildDataTables();
		}

		public ReminderData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Reminder_Setting");
			DataColumnCollection columns = dataTable.Columns;
			columns.Add("ReminderID", typeof(byte)).AllowDBNull = false;
			columns.Add("UserID", typeof(string)).AllowDBNull = false;
			columns.Add("Days", typeof(byte));
			columns.Add("IsSelected", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
