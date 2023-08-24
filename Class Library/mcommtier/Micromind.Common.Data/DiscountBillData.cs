using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class DiscountBillData : DataSet
	{
		public const string BILLDISCOUNT_TABLE = "Bill_Discount";

		public const string BILLDISCOUNTDETAIL_TABLE = "Bill_Discount_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string INVOICESYSDOCID_FIELD = "InvoiceSysDocID";

		public const string INVOICEVOUCHERID_FIELD = "InvoiceVoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string CHEQUEID_FIELD = "ChequeID";

		public const string BANKACCOUNTID_FIELD = "BankAccountID";

		public const string LIABILITYACCOUNTID_FIELD = "LiabilityAccountID";

		public const string BANKFACILITYID_FIELD = "BankFacilityID";

		public const string BANKCHARGEAMOUNT_FIELD = "BankChargeAmount";

		public const string BANKINTERESTAMOUNT_FIELD = "BankCommission";

		public const string BANKCHARGEPERCENT_FIELD = "BankChargePercent";

		public const string BANKCHARGEACCOUNTID_FIELD = "BankChargeAccountID";

		public const string BANKINTERESTACCOUNTID_FIELD = "BankInterestAccountID";

		public const string ISVOID_FIELD = "IsVoid";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string NOTE_FIELD = "Note";

		public const string REFERENCE_FIELD = "Reference";

		public const string FACILITYTYPE_FIELD = "FacilityType";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string DATE_FIELD = "Date";

		public const string TOTAL_FIELD = "Total";

		public const string DUEDATE_FIELD = "DueDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string DISCOUNTAMOUNT_FIELD = "DiscountAmount";

		public const string PAYEEID_FIELD = "PayeeID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable BillDiscountTable => base.Tables["Bill_Discount"];

		public DataTable BillDiscountDetailTable => base.Tables["Bill_Discount_Detail"];

		public DiscountBillData()
		{
			BuildDataTables();
		}

		public DiscountBillData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Bill_Discount");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("DivisionID", typeof(string));
			columns.Add("CompanyID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("ChequeID", typeof(short));
			columns.Add("BankAccountID", typeof(string));
			columns.Add("BankFacilityID", typeof(string));
			columns.Add("BankChargeAmount", typeof(decimal));
			columns.Add("BankCommission", typeof(decimal));
			columns.Add("BankChargePercent", typeof(decimal));
			columns.Add("BankChargeAccountID", typeof(string));
			columns.Add("BankInterestAccountID", typeof(string));
			columns.Add("LiabilityAccountID", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("FacilityType", typeof(int));
			columns.Add("DueDate", typeof(DateTime));
			columns.Add("Note", typeof(string));
			columns.Add("Reference", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Bill_Discount_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("InvoiceVoucherID", typeof(string));
			columns.Add("InvoiceSysDocID", typeof(string));
			columns.Add("Date", typeof(DateTime));
			columns.Add("Total", typeof(decimal));
			columns.Add("DueDate", typeof(DateTime));
			columns.Add("BankChargeAmount", typeof(decimal));
			columns.Add("DiscountAmount", typeof(decimal));
			columns.Add("PayeeID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
