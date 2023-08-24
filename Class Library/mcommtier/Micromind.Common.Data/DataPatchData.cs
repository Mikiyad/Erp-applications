using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class DataPatchData : DataSet
	{
		public const string PATCHID_FIELD = "PatchID";

		public const string PATCHDESCRIPTION_FIELD = "PatchDescription";

		public const string PATCHQUERY_FIELD = "PatchQuery";

		public const string STATUS_FIELD = "Status";

		public const string DATEEXECUTED_FIELD = "DateExecuted";

		public const string DATAVERSION_FIELD = "DataVersion";

		public const string DATAPATCH_TABLE = "Data_Patch";

		public DataTable DataPatchTable => base.Tables["Data_Patch"];

		public DataPatchData()
		{
			BuildDataTables();
		}

		public DataPatchData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Data_Patch");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PatchID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PatchDescription", typeof(string)).AllowDBNull = false;
			columns.Add("PatchQuery", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("DataVersion", typeof(string));
			columns.Add("DateExecuted", typeof(DateTime));
			base.Tables.Add(dataTable);
		}
	}
}
