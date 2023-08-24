using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class TRApplicationData : DataSet
	{
		public const string TRAPPLICATION_TABLE = "TR_Application";

		public const string TRANSACTIONID_FIELD = "TransactionID";

		public const string SYSDOCTYPE_FIELD = "SysDocType";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string COSTCENTERID_FIELD = "CostCenterID";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string PAYEETYPE_FIELD = "PayeeType";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string BANKFACILITYID_FIELD = "BankFacilityID";

		public const string REFERENCE_FIELD = "Reference";

		public const string DUEDATE_FIELD = "DueDate";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string REQUESTSYSDOCID_FIELD = "RequestSysDocID";

		public const string REQUESTVOUCHERID_FIELD = "RequestVoucherID";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string ISVOID_FIELD = "IsVoid";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string NOTE_FIELD = "Note";

		public const string POSYSDOCID_FIELD = "POSysDocID";

		public const string POVOUCHERID_FIELD = "POVoucherID";

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

		public DataTable TRApplicationTable => base.Tables["TR_Application"];

		public TRApplicationData()
		{
			BuildDataTables();
		}

		public TRApplicationData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("TR_Application");
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
			columns.Add("SysDocType", typeof(byte));
			columns.Add("CostCenterID", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("PayeeType", typeof(string));
			columns.Add("PayeeID", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("BankFacilityID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("DueDate", typeof(DateTime));
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("RequestSysDocID", typeof(string));
			columns.Add("RequestVoucherID", typeof(string));
			columns.Add("POSysDocID", typeof(string));
			columns.Add("POVoucherID", typeof(string));
			columns.Add("InvoiceNos", typeof(string));
			columns.Add("Authorizedby", typeof(string));
			columns.Add("NoofInvoices", typeof(short));
			columns.Add("NoofPL", typeof(short));
			columns.Add("NoofBOL", typeof(short));
			columns.Add("NoofGoods", typeof(string));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
