using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeLeaveProcessData : DataSet
	{
		public const string EMPLOYEELEAVEPROCESS_TABLE = "Employee_Leave_Process";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string EMPLOYEENAME_FIELD = "EmployeeName";

		public const string DAYS_FIELD = "Days";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string FROMDATE_FIELD = "FromDate";

		public const string TODATE_FIELD = "ToDate";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string DOCID_FIELD = "VoucherID";

		public const string PERIOD_FIELD = "TransactionDate";

		public const string NOTE_FIELD = "Note";

		public DataTable EmployeeLeaveProcessTable => base.Tables["Employee_Leave_Process"];

		public EmployeeLeaveProcessData()
		{
			BuildDataTables();
		}

		public EmployeeLeaveProcessData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Leave_Process");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Note", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Days", typeof(short));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("EmployeeName", typeof(string));
			columns.Add("FromDate", typeof(DateTime));
			columns.Add("ToDate", typeof(DateTime));
			base.Tables.Add(dataTable);
		}
	}
}
