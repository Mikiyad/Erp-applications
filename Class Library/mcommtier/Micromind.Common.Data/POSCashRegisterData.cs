using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class POSCashRegisterData : DataSet
	{
		public const string POSCASHREGISTER_TABLE = "POS_CashRegister";

		public const string CASHREGISTERID_FIELD = "CashRegisterID";

		public const string CASHREGISTERNAME_FIELD = "CashRegisterName";

		public const string COMPUTERNAME_FIELD = "ComputerName";

		public const string DEFAULTCUSTOMERID_FIELD = "DefaultCustomerID";

		public const string DISCOUNTACCOUNTID_FIELD = "DiscountAccountID";

		public const string PETTYCASHACCOUNTID_FIELD = "PettyCashAccountID";

		public const string RECEIPTDOCID_FIELD = "ReceiptDocID";

		public const string EXPENSEDOCID_FIELD = "ExpenseDocID";

		public const string NOTE_FIELD = "Note";

		public const string POSCASHREGISTERPAYMENTMETHOD_TABLE = "POS_CashRegister_PaymentMethod";

		public const string PAYMENTMETHODID_FIELD = "PaymentMethodID";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string DISPLAYNAME_FIELD = "DisplayName";

		public const string INACTIVE_FIELD = "Inactive";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string POSCASHREGISTEREXPENSE_TABLE = "POS_CashRegister_EXPENSE";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable POSCashRegisterTable => base.Tables["POS_CashRegister"];

		public DataTable PaymentMethodTable => base.Tables["POS_CashRegister_PaymentMethod"];

		public DataTable ExpenseAccountTable => base.Tables["POS_CashRegister_EXPENSE"];

		public POSCashRegisterData()
		{
			BuildDataTables();
		}

		public POSCashRegisterData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("POS_CashRegister");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CashRegisterID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CashRegisterName", typeof(string)).AllowDBNull = false;
			columns.Add("LocationID", typeof(string));
			columns.Add("ExpenseDocID", typeof(string));
			columns.Add("ComputerName", typeof(string));
			columns.Add("DiscountAccountID", typeof(string));
			columns.Add("PettyCashAccountID", typeof(string));
			columns.Add("ReceiptDocID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("DefaultCustomerID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("POS_CashRegister_PaymentMethod");
			columns = dataTable.Columns;
			columns.Add("CashRegisterID", typeof(string)).AllowDBNull = false;
			columns.Add("PaymentMethodID", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("DisplayName", typeof(string));
			columns.Add("Inactive", typeof(bool));
			columns.Add("RowIndex", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("POS_CashRegister_EXPENSE");
			columns = dataTable.Columns;
			columns.Add("CashRegisterID", typeof(string)).AllowDBNull = false;
			columns.Add("AccountID", typeof(string));
			columns.Add("DisplayName", typeof(string));
			columns.Add("RowIndex", typeof(byte));
			base.Tables.Add(dataTable);
		}
	}
}
