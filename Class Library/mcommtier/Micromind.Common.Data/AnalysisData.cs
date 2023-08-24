using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class AnalysisData : DataSet
	{
		public const string ANALYSISID_FIELD = "AnalysisID";

		public const string ANALYSISNAME_FIELD = "AnalysisName";

		public const string DESCRIPTION_FIELD = "Description";

		public const string GROUPID_FIELD = "GroupID";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ANALYSIS_TABLE = "Analysis";

		public DataTable AnalysisTable => base.Tables["Analysis"];

		public AnalysisData()
		{
			BuildDataTables();
		}

		public AnalysisData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Analysis");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("AnalysisID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("AnalysisName", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("GroupID", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
