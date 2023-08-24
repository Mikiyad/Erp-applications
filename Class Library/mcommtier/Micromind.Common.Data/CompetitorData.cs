using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CompetitorData : DataSet
	{
		public const string COMPETITOR_TABLE = "Competitor";

		public const string COMPETITORID_FIELD = "CompetitorID";

		public const string COMPETITORNAME_FIELD = "CompetitorName";

		public const string LEADID_FIELD = "LeadID";

		public const string LEADNAME_FIELD = "LeadName";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CompetitorTable => base.Tables["Competitor"];

		public CompetitorData()
		{
			BuildDataTables();
		}

		public CompetitorData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Competitor");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CompetitorID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CompetitorName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
