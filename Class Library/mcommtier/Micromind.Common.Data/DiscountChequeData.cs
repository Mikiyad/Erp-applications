using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class DiscountChequeData : DataSet
	{
		public const string CHEQUEDISCOUNT_TABLE = "Cheque_Discount";

		public const string CHEQUEDISCOUNTDETAIL_TABLE = "Cheque_Discount_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

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

		public const string NOTE_FIELD = "Note";

		public const string REFERENCE_FIELD = "Reference";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string DISCOUNTAMOUNT_FIELD = "DiscountAmount";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ChequeDiscountTable => base.Tables["Cheque_Discount"];

		public DataTable ChequeDiscountDetailTable => base.Tables["Cheque_Discount_Detail"];

		public DiscountChequeData()
		{
			BuildDataTables();
		}

		public DiscountChequeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Cheque_Discount");
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
			columns.Add("ChequeID", typeof(int));
			columns.Add("BankAccountID", typeof(string));
			columns.Add("BankFacilityID", typeof(string));
			columns.Add("BankChargeAmount", typeof(decimal));
			columns.Add("BankCommission", typeof(decimal));
			columns.Add("BankChargePercent", typeof(decimal));
			columns.Add("BankChargeAccountID", typeof(string));
			columns.Add("BankInterestAccountID", typeof(string));
			columns.Add("LiabilityAccountID", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Note", typeof(string));
			columns.Add("Reference", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Cheque_Discount_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ChequeID", typeof(int));
			columns.Add("BankChargeAmount", typeof(decimal));
			columns.Add("DiscountAmount", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
