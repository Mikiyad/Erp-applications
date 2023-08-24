using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PaymentRequestData : DataSet
	{
		public const string PAYMENTREQUEST_TABLE = "Payment_Request";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TYPEID_FIELD = "TypeID";

		public const string PAYFROMID_FIELD = "PayFromID";

		public const string PAYEETYPE_FIELD = "PayeeType";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string POSYSDOCID_FIELD = "POSysDocID";

		public const string POVOUCHERID_FIELD = "POVoucherID";

		public const string PLSYSDOCID_FIELD = "PLSysDocID";

		public const string PLVOUCHERID_FIELD = "PLVoucherID";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string REASON_FIELD = "Reason";

		public const string STATUS_FIELD = "Status";

		public const string REFERENCE_FIELD = "Reference";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string NOTE_FIELD = "Note";

		public const string ISVOID_FIELD = "IsVoid";

		public const string AVAILABLEBAL_FIELD = "AvailableBal";

		public const string CURRENTBAL_FIELD = "CurrentBal";

		public const string PAYMENTREQUESTED_FIELD = "PaymentRequested";

		public const string PAYMENTREQUESTEDFC_FIELD = "PaymentRequestedFC";

		public const string INVOICENO_FIELD = "InvoiceNos";

		public const string AUTHORIZEDBY_FIELD = "Authorizedby";

		public const string NOOFINVOICES_FIELD = "NoofInvoices";

		public const string NOOFPL_FIELD = "NoofPL";

		public const string NOOFBOL_FIELD = "NoofBOL";

		public const string NOOFGOODS_FIELD = "NoofGoods";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string JOBID_FIELD = "JobID";

		public DataTable PaymentRequestTable => base.Tables["Payment_Request"];

		public PaymentRequestData()
		{
			BuildDataTables();
		}

		public PaymentRequestData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Payment_Request");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("TypeID", typeof(byte));
			columns.Add("PayFromID", typeof(string));
			columns.Add("PayeeType", typeof(string));
			columns.Add("PayeeID", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("POSysDocID", typeof(string));
			columns.Add("POVoucherID", typeof(string));
			columns.Add("PLSysDocID", typeof(string));
			columns.Add("PLVoucherID", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("Status", typeof(byte)).DefaultValue = 1;
			columns.Add("Reason", typeof(byte));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("AvailableBal", typeof(decimal));
			columns.Add("CurrentBal", typeof(decimal));
			columns.Add("InvoiceNos", typeof(string));
			columns.Add("Authorizedby", typeof(string));
			columns.Add("NoofInvoices", typeof(short));
			columns.Add("NoofPL", typeof(short));
			columns.Add("NoofBOL", typeof(short));
			columns.Add("NoofGoods", typeof(string));
			columns.Add("PaymentRequested", typeof(decimal));
			columns.Add("PaymentRequestedFC", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
