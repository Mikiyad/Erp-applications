using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class BinData : DataSet
	{
		public const string BIN_TABLE = "Bin";

		public const string BINID_FIELD = "BinID";

		public const string ISINACTIVE_FIELD = "Inactive";

		public const string BINNAME_FIELD = "BinName";

		public const string REMARKS_FIELD = "Remarks";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable BinTable => base.Tables["Bin"];

		public BinData()
		{
			BuildDataTables();
		}

		public BinData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Bin");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("BinID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("BinName", typeof(string));
			columns.Add("Inactive", typeof(bool));
			columns.Add("Remarks", typeof(string));
			columns.Add("LocationID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
