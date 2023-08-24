using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ReturnedChequeReasonData : DataSet
	{
		public const string RETURNEDCHEQUEREASON_TABLE = "Returned_Cheque_Reason";

		public const string REASONID_FIELD = "ReasonID";

		public const string REASONNAME_FIELD = "ReasonName";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ReturnedChequeReasonTable => base.Tables["Returned_Cheque_Reason"];

		public ReturnedChequeReasonData()
		{
			BuildDataTables();
		}

		public ReturnedChequeReasonData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Returned_Cheque_Reason");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ReasonID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ReasonName", typeof(string)).AllowDBNull = false;
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
