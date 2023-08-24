using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class RackData : DataSet
	{
		public const string RACK_TABLE = "Rack";

		public const string RACKID_FIELD = "RackID";

		public const string BINID_FIELD = "BinID";

		public const string ISINACTIVE_FIELD = "Inactive";

		public const string RACKNAME_FIELD = "RackName";

		public const string REMARKS_FIELD = "Remarks";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable RackTable => base.Tables["Rack"];

		public RackData()
		{
			BuildDataTables();
		}

		public RackData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Rack");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("RackID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("BinID", typeof(string));
			columns.Add("RackName", typeof(string));
			columns.Add("Inactive", typeof(bool));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
