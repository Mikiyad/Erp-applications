using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ActivityData : DataSet
	{
		public static string ACTIVITY_TABLE = "Activities";

		public static string USERID_FIELD = "UserID";

		public static string TOKENID_FIELD = "ID";

		public static string MACHINEID_FIELD = "MachineID";

		public static string SERVERID_FIELD = "ServerID";

		public static string LOGINDATE_FIELD = "LoginDate";

		public static string LASTLOGDATE_FIELD = "LastLogDate";

		public static string DBNAME_FIELD = "DBName";

		public static string APPNAME_FIELD = "AppName";

		public static string SYSTEMID_FIELD = "SystemID";

		public static string PRODUCTKEY_FIELD = "ProductKey";

		public static string ACTIVITYID_FIELD = "ActivityID";

		public DataTable ActivityTable => base.Tables[ACTIVITY_TABLE];

		public ActivityData()
		{
			BuildDataTables();
		}

		public ActivityData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable(ACTIVITY_TABLE);
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add(ACTIVITYID_FIELD, typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add(USERID_FIELD, typeof(string));
			columns.Add(MACHINEID_FIELD, typeof(string));
			columns.Add(SERVERID_FIELD, typeof(string));
			columns.Add(TOKENID_FIELD, typeof(string));
			columns.Add(DBNAME_FIELD, typeof(string));
			columns.Add(APPNAME_FIELD, typeof(string));
			columns.Add(SYSTEMID_FIELD, typeof(string));
			columns.Add(PRODUCTKEY_FIELD, typeof(string));
			columns.Add(LOGINDATE_FIELD, typeof(DateTime));
			columns.Add(LASTLOGDATE_FIELD, typeof(DateTime));
			base.Tables.Add(dataTable);
		}
	}
}
