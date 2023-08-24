using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class LPOReceiptData : DataSet
	{
		public const string LPORECEIPT_TABLE = "LPO_Receipt";

		public const string LPORECEIPTDETAIL_TABLE = "LPO_Receipt_Details";

		public const string SYSDOCTYPE_FIELD = "SysDocType";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string TOTAL_FIELD = "Total";

		public const string ISVOID_FIELD = "IsVoid";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PAYEETYPE_FIELD = "PayeeType";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string LPONUMBER_FIELD = "LPONumber";

		public const string LPODATE_FIELD = "LPODate";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string ROWINDEX_FIELD = "RowIndex";

		public DataTable LPOReceiptTable => base.Tables["LPO_Receipt"];

		public DataTable LPOReceiptDetailTable => base.Tables["LPO_Receipt_Details"];

		public LPOReceiptData()
		{
			BuildDataTables();
		}

		public LPOReceiptData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("LPO_Receipt");
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
			columns.Add("CustomerID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("SysDocType", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("LPO_Receipt_Details");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("PayeeType", typeof(string));
			columns.Add("PayeeID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("LPONumber", typeof(string));
			columns.Add("LPODate", typeof(DateTime));
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("AccountID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
