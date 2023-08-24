using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PivotGroupData : DataSet
	{
		public const string PIVOTGROUP_TABLE = "Pivot_Group";

		public const string PIVOTGROUPID_FIELD = "GroupID";

		public const string PIVOTGROUPNAME_FIELD = "GroupName";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable PivotGroupTable => base.Tables["Pivot_Group"];

		public PivotGroupData()
		{
			BuildDataTables();
		}

		public PivotGroupData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Pivot_Group");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("GroupID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("GroupName", typeof(string)).AllowDBNull = false;
			base.Tables.Add(dataTable);
		}
	}
}
