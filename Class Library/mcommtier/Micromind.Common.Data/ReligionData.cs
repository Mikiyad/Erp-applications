using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ReligionData : DataSet
	{
		public const string RELIGION_TABLE = "Religion";

		public const string RELIGIONID_FIELD = "ReligionID";

		public const string RELIGIONNAME_FIELD = "ReligionName";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ReligionTable => base.Tables["Religion"];

		public ReligionData()
		{
			BuildDataTables();
		}

		public ReligionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Religion");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ReligionID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ReligionName", typeof(string)).AllowDBNull = false;
			base.Tables.Add(dataTable);
		}
	}
}
