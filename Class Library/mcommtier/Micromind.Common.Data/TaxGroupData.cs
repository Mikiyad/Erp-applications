using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class TaxGroupData : DataSet
	{
		public const string TAXGROUP_TABLE = "Tax_Group";

		public const string TAXGROUPCODE_FIELD = "TaxGroupID";

		public const string TAXGROUPNAME_FIELD = "TaxGroupName";

		public const string NOTE_FIELD = "Note";

		public const string TAXGROUPDETAIL_TABLE = "Tax_Group_Detail";

		public const string TAXCODE_FIELD = "TaxCode";

		public const string DESCRIPTION_FIELD = "Description";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable TaxTable => base.Tables["Tax_Group"];

		public DataTable TaxGroupDetailTable => base.Tables["Tax_Group_Detail"];

		public TaxGroupData()
		{
			BuildDataTables();
		}

		public TaxGroupData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Tax_Group");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TaxGroupID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TaxGroupName", typeof(string)).AllowDBNull = false;
			columns.Add("Inactive", typeof(bool));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Tax_Group_Detail");
			columns = dataTable.Columns;
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxCode", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
