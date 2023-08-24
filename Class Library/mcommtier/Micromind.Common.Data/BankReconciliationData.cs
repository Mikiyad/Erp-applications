using System;
using System.ComponentModel;
using System.Data;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class BankReconciliationData : DataSet
	{
		public const string BANKRECONCILIATION_TABLE = "Bank_Reconciliation";

		public const string BANKRECID_FIELD = "BankReconciliationID";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string RECONCILEDATE_FIELD = "ReconcileDate";

		public const string DEBIT_FIELD = "Debit";

		public const string CREDIT_FIELD = "Credit";

		public const string DEBITFC_FIELD = "DebitFC";

		public const string CREDITFC_FIELD = "CreditFC";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable BankReconciliationTable => base.Tables["Bank_Reconciliation"];

		public BankReconciliationData()
		{
			BuildDataTables();
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Bank_Reconciliation");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("AccountID", typeof(string));
			DataColumn dataColumn2 = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn3 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn2.AllowDBNull = false;
			dataColumn3.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[3]
			{
				dataColumn,
				dataColumn2,
				dataColumn3
			};
			columns.Add("Description", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("ReconcileDate", typeof(DateTime));
			columns.Add("Debit", typeof(decimal)).DefaultValue = 0;
			columns.Add("Credit", typeof(decimal)).DefaultValue = 0;
			columns.Add("DebitFC", typeof(decimal)).DefaultValue = 0;
			columns.Add("CreditFC", typeof(decimal)).DefaultValue = 0;
			columns.Add("RowIndex", typeof(byte));
			base.Tables.Add(dataTable);
		}
	}
}
