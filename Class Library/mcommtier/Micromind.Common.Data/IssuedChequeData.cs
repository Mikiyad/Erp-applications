using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class IssuedChequeData : DataSet
	{
		public const string ISSUEDCHEQUE_TABLE = "Cheque_Issued";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string CHEQUENUMBER_FIELD = "ChequeNumber";

		public const string BANKID_FIELD = "BankID";

		public const string PAYEETYPE_FIELD = "PayeeType";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string PAYEEACCOUNTID_FIELD = "PayeeAccountID";

		public const string CHEQUEBOOKID_FIELD = "ChequebookID";

		public const string CHEQUEDATE_FIELD = "ChequeDate";

		public const string ISSUEDATE_FIELD = "IssueDate";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string EXCHANGERATE_FIELD = "ExchangeRate";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string NOTE_FIELD = "Note";

		public const string STATUS_FIELD = "Status";

		public const string REFERENCE_FIELD = "Reference";

		public const string PDCACCOUNTID_FIELD = "PDCAccountID";

		public const string BANKACCOUNTID_FIELD = "BankAccountID";

		public const string CLEARANCEDATE_FIELD = "ClearanceDate";

		public const string CLEARANCEACCOUNTID_FIELD = "ClearanceAccountID";

		public const string CLEARANCEVOUCHERID_FIELD = "ClearanceVoucherID";

		public const string CHEQUEID_FIELD = "ChequeID";

		public const string SECURITYCHEQUETABLE_FIELD = "Security_Cheque";

		public const string ISVOID_FIELD = "IsVoid";

		public const string VOIDDATE_FIELD = "VoidDate";

		public const string TRANSACTIONID_FIELD = "TransactionID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable IssuedChequeTable => base.Tables["Cheque_Issued"];

		public IssuedChequeData()
		{
			BuildDataTables();
		}

		public IssuedChequeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Cheque_Issued");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			DataColumn dataColumn3 = columns.Add("ChequeNumber", typeof(string));
			DataColumn dataColumn4 = columns.Add("PayeeType", typeof(string));
			DataColumn dataColumn5 = columns.Add("PayeeID", typeof(string));
			dataTable.PrimaryKey = new DataColumn[5]
			{
				dataColumn,
				dataColumn2,
				dataColumn3,
				dataColumn4,
				dataColumn5
			};
			columns.Add("PayeeAccountID", typeof(string));
			columns.Add("ChequeDate", typeof(DateTime));
			columns.Add("IssueDate", typeof(DateTime));
			columns.Add("BankID", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("ChequebookID", typeof(string));
			columns.Add("ExchangeRate", typeof(decimal));
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("Note", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("Reference", typeof(string));
			columns.Add("PDCAccountID", typeof(string));
			columns.Add("BankAccountID", typeof(string));
			columns.Add("ClearanceDate", typeof(DateTime));
			columns.Add("ClearanceAccountID", typeof(string));
			columns.Add("ClearanceVoucherID", typeof(string));
			base.Tables.Add(dataTable);
		}

		public static void BuildSecurityChequeDataTable(DataSet data)
		{
			DataTable dataTable = new DataTable("Security_Cheque");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("ChequeDate", typeof(DateTime));
			columns.Add("IssueDate", typeof(DateTime));
			columns.Add("ChequeNumber", typeof(string));
			columns.Add("ChequebookID", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("Note", typeof(string));
			columns.Add("PayeeType", typeof(string));
			columns.Add("PayeeID", typeof(string));
			columns.Add("VoidDate", typeof(DateTime));
			columns.Add("IsVoid", typeof(bool));
			data.Tables.Add(dataTable);
		}
	}
}
