using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class OpeningBalanceLeaveData : DataSet
	{
		public const string BATCHID_FIELD = "BatchID";

		public const string BATCHDATE_FIELD = "BatchDate";

		public const string BATCHTYPE_FIELD = "BatchType";

		public const string REFERENCE_FIELD = "Reference";

		public const string DESCRIPTION_FIELD = "Description";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string OPENINGBALANCELEAVE_TABLE = "Opening_Balance_Leave";

		public const string TRANSACTIONSYSDOCID_FIELD = "TransactionSysDocID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string LEAVETYPEID_FIELD = "LeaveTypeID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string LEAVESTARTDATE_FIELD = "LeaveStartDate";

		public const string LEAVEENDDATE_FIELD = "LeaveEndDate";

		public const string LEAVETAKEN_FIELD = "LeaveTaken";

		public const string PAIDDAYS_FIELD = "PaidDays";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string CURRENCYID_FIELD = "SysDocID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string OPENINGBALANCELEAVEDETAIL_TABLE = "Opening_Balance_Leave_Detail";

		public DataTable OpeningBalanceLeaveTable => base.Tables["Opening_Balance_Leave"];

		public DataTable OpeningBalanceLeaveDetailsTable => base.Tables["Opening_Balance_Leave_Detail"];

		public OpeningBalanceLeaveData()
		{
			BuildDataTables();
		}

		public OpeningBalanceLeaveData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Opening_Balance_Leave");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("BatchID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("SysDocID", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataColumn2.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("BatchDate", typeof(DateTime));
			columns.Add("BatchType", typeof(byte));
			columns.Add("TransactionSysDocID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("LocationID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Opening_Balance_Leave_Detail");
			columns = dataTable.Columns;
			columns.Add("BatchID", typeof(string));
			columns.Add("SysDocID", typeof(string));
			columns.Add("TransactionSysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("BatchType", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Description", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("LeaveTypeID", typeof(string));
			columns.Add("LeaveStartDate", typeof(DateTime));
			columns.Add("LeaveEndDate", typeof(DateTime));
			columns.Add("LeaveTaken", typeof(int)).DefaultValue = 0;
			columns.Add("PaidDays", typeof(int)).DefaultValue = 0;
			columns.Add("Reference", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
