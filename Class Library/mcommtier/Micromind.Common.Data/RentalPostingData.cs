using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class RentalPostingData : DataSet
	{
		public const string RENTALPOSTING_TABLE = "Rental_Posting";

		public const string RENTALPOSTINGDETAIL_TABLE = "Rental_Posting_Detail";

		public const string RENTALPOSTINGDETAILITEMS_TABLE = "Rental_Posting_Detail_Item";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string SHEETNAME_FIELD = "SheetName";

		public const string MONTH_FIELD = "Month";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ASOFDATE_FIELD = "AsofDate";

		public const string YEAR_FIELD = "Year";

		public const string NOTE_FIELD = "Note";

		public const string REFERENCE_FIELD = "Reference";

		public const string ISPOSTED_FIELD = "IsPosted";

		public const string ISVOID_FIELD = "IsVoid";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string PAYTYPE_FIELD = "PayType";

		public const string TENANTID_FIELD = "TenantID";

		public const string RENTEDDAYS_FIELD = "RentedDays";

		public const string NETAMOUNT_FIELD = "NetAmount";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string PAYROLLITEMID_FIELD = "PayrollItemID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string AMOUNT_FIELD = "Amount";

		public const string PAYABLEAMOUNT_FIELD = "PayableAmount";

		public const string PAID_FIELD = "PaidAmount";

		public const string PAYCODETYPE_FIELD = "PayCodeType";

		public const string ISFIXED_FIELD = "IsFixed";

		public DataTable RentalPostingTable => base.Tables["Rental_Posting"];

		public DataTable RentalPostingDetailTable => base.Tables["Rental_Posting_Detail"];

		public DataTable RentalPostingDetailItemsTable => base.Tables["Rental_Posting_Detail_Item"];

		public RentalPostingData()
		{
			BuildDataTables();
		}

		public RentalPostingData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Rental_Posting");
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
			columns.Add("SheetName", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("Month", typeof(byte));
			columns.Add("Year", typeof(short));
			columns.Add("Note", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("IsPosted", typeof(bool));
			columns.Add("AsofDate", typeof(DateTime));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Rental_Posting_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("TenantID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("RentedDays", typeof(decimal));
			columns.Add("NetAmount", typeof(decimal));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("Property", typeof(string));
			columns.Add("PropertyUnitID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Rental_Posting_Detail_Item");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("TenantID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("PayType", typeof(byte));
			columns.Add("PayrollItemID", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("PayableAmount", typeof(decimal));
			columns.Add("PaidAmount", typeof(decimal));
			columns.Add("Property", typeof(string));
			columns.Add("IsFixed", typeof(bool));
			columns.Add("PayCodeType", typeof(byte));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
