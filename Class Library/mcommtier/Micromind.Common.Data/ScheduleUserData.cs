using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ScheduleUserData : DataSet
	{
		public const string SCHEDULEID_FIELD = "ScheduleID";

		public const string USERID_FIELD = "UserID";

		public const string SCHEDULEUSERS_TABLE = "[Schedule Users]";

		public DataTable ScheduleUserTable => base.Tables["[Schedule Users]"];

		public ScheduleUserData()
		{
			BuildDataTables();
		}

		public ScheduleUserData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Schedule Users]");
			DataColumnCollection columns = dataTable.Columns;
			columns.Add("ScheduleID", typeof(int)).AllowDBNull = false;
			columns.Add("UserID", typeof(short)).AllowDBNull = false;
			base.Tables.Add(dataTable);
		}
	}
}
