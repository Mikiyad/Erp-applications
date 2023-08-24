using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ReceivedChequeData : DataSet
	{
		public const string RECEIVEDCHEQUE_TABLE = "Cheque_Received";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string CHEQUENUMBER_FIELD = "ChequeNumber";

		public const string BANKID_FIELD = "BankID";

		public const string PAYEETYPE_FIELD = "PayeeType";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string PAYEEACCOUNTID_FIELD = "PayeeAccountID";

		public const string CHEQUEDATE_FIELD = "ChequeDate";

		public const string RECEIPTDATE_FIELD = "ReceiptDate";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string EXCHANGERATE_FIELD = "ExchangeRate";

		public const string AMOUNT_FIELD = "Amount";

		public const string DISCOUNTAMOUNT_FIELD = "DiscountAmount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string CONAMOUNTFC_FIELD = "ConAmountFC";

		public const string CONRATE_FIELD = "ConRate";

		public const string NOTE_FIELD = "Note";

		public const string STATUS_FIELD = "Status";

		public const string REFERENCE_FIELD = "Reference";

		public const string PDCACCOUNTID_FIELD = "PDCAccountID";

		public const string DEPOSITDATE_FIELD = "DepositDate";

		public const string DEPOSITACCOUNTID_FIELD = "DepositAccountID";

		public const string DEPOSITBANKID_FIELD = "DepositBankID";

		public const string DEPOSITSYSDOCID_FIELD = "DepositSysDocID";

		public const string DEPOSITVOUCHERID_FIELD = "DepositVoucherID";

		public const string DISCOUNTACCOUNTID_FIELD = "DiscountAccountID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ReceivedChequeTable => base.Tables["Cheque_Received"];

		public ReceivedChequeData()
		{
			BuildDataTables();
		}

		public ReceivedChequeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Cheque_Received");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			DataColumn dataColumn3 = columns.Add("ChequeNumber", typeof(string));
			DataColumn dataColumn4 = columns.Add("BankID", typeof(string));
			DataColumn dataColumn5 = columns.Add("PayeeType", typeof(string));
			DataColumn dataColumn6 = columns.Add("PayeeID", typeof(string));
			dataTable.PrimaryKey = new DataColumn[6]
			{
				dataColumn,
				dataColumn2,
				dataColumn3,
				dataColumn4,
				dataColumn5,
				dataColumn6
			};
			columns.Add("PayeeAccountID", typeof(string));
			columns.Add("ChequeDate", typeof(DateTime));
			columns.Add("ReceiptDate", typeof(DateTime));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("ExchangeRate", typeof(decimal));
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("ConAmountFC", typeof(decimal));
			columns.Add("ConRate", typeof(decimal));
			columns.Add("DiscountAmount", typeof(decimal));
			columns.Add("Note", typeof(string));
			columns.Add("Status", typeof(byte)).DefaultValue = (byte)1;
			columns.Add("Reference", typeof(string));
			columns.Add("PDCAccountID", typeof(string));
			columns.Add("DepositDate", typeof(DateTime));
			columns.Add("DepositAccountID", typeof(string));
			columns.Add("DepositBankID", typeof(string));
			columns.Add("DepositSysDocID", typeof(string));
			columns.Add("DepositVoucherID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
