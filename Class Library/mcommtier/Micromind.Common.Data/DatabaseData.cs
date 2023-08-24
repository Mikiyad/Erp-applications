using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class DatabaseData : DataSet
	{
		public const string DATABASES_TABLE = "Databases";

		public const string DATABASEID_FIELD = "DatabaseID";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string UPDATEDBYLOGINNAME_FIELD = "UpdatedByLoginName";

		public const string CREATEDBYLOGINNAME_FIELD = "CreatedByLoginName";

		public DataTable DatabaseTable => base.Tables["Databases"];

		public DatabaseData()
		{
			BuildDataTables();
		}

		public DatabaseData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Databases");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("DatabaseID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("DateTimeStamp", typeof(DateTime));
			columns.Add("DateCreated", typeof(DateTime));
			columns.Add("DateUpdated", typeof(DateTime));
			columns.Add("CreatedByLoginName", typeof(string));
			columns.Add("UpdatedByLoginName", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
