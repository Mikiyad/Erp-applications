using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class POSBatchData : DataSet
	{
		public const string POSBATCH_TABLE = "POS_Batch";

		public const string BATCHID_FIELD = "BatchID";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string OPENDATE_FIELD = "OpenDate";

		public const string CLOSEDATE_FIELD = "CloseDate";

		public const string STATUS_FIELD = "Status";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable POSBatchTable => base.Tables["POS_Batch"];

		public POSBatchData()
		{
			BuildDataTables();
		}

		public POSBatchData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("POS_Batch");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("BatchID", typeof(int));
			dataColumn.AllowDBNull = true;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LocationID", typeof(string)).AllowDBNull = true;
			columns.Add("OpenDate", typeof(DateTime));
			columns.Add("CloseDate", typeof(DateTime));
			columns.Add("Status", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
