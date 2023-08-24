using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CustomFieldGroupData : DataSet
	{
		public const string CUSTOMFIELDGROUPS_TABLE = "[Custom Field Groups]";

		public const string CUSTOMFIELDGROUPID_FIELD = "CustomFieldGroupID";

		public const string CUSTOMFIELDGROUPNAME_FIELD = "CustomFieldGroupName";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public DataTable CustomFieldGroupsTable => base.Tables["[Custom Field Groups]"];

		public CustomFieldGroupData()
		{
			BuildDataTables();
		}

		public CustomFieldGroupData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Custom Field Groups]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CustomFieldGroupID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CustomFieldGroupName", typeof(string));
			columns.Add("DateTimeStamp", typeof(DateTime));
			base.Tables.Add(dataTable);
		}
	}
}
