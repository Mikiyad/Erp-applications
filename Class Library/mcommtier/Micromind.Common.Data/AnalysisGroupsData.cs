using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class AnalysisGroupsData : DataSet
	{
		public const string GROUPID_FIELD = "GroupID";

		public const string GROUPNAME_FIELD = "GroupName";

		public const string DESCRIPTION_FIELD = "Description";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ANALYSISGROUPS_TABLE = "Analysis_Group";

		public DataTable AnalysisGroupsTable => base.Tables["Analysis_Group"];

		public AnalysisGroupsData()
		{
			BuildDataTables();
		}

		public AnalysisGroupsData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Analysis_Group");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("GroupID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("GroupName", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
