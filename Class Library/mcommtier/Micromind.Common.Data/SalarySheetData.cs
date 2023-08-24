using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SalarySheetData : DataSet
	{
		public const string SALARYSHEET_TABLE = "SalarySheet";

		public const string SALARYSHEETDETAIL_TABLE = "SalarySheet_Detail";

		public const string SALARYSHEETDETAILITEMS_TABLE = "SalarySheet_Detail_Item";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string SHEETNAME_FIELD = "SheetName";

		public const string MONTH_FIELD = "Month";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string YEAR_FIELD = "Year";

		public const string NOTE_FIELD = "Note";

		public const string REFERENCE_FIELD = "Reference";

		public const string ISPOSTED_FIELD = "IsPosted";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string PAYTYPE_FIELD = "PayType";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string WORKDAYS_FIELD = "WorkDays";

		public const string ABSENT_FIELD = "Absent";

		public const string ANNUALLEAVES_FIELD = "AnnualLeaves";

		public const string REMARKS_FIELD = "Remarks";

		public const string UNPAIDANNUALLEAVES_FIELD = "UnpaidAnnualLeaves";

		public const string NORMALLEAVES_FIELD = "NormalLeaves";

		public const string SICKLEAVES_FIELD = "SickLeaves";

		public const string BASIC_FIELD = "Basic";

		public const string ALLOWANCE_FIELD = "Allowance";

		public const string DEDUCTION_FIELD = "Deduction";

		public const string LOANDEDUCTION_FIELD = "LoanDeduction";

		public const string OTHOURS_FIELD = "OTHours";

		public const string OTRATE_FIELD = "OTRate";

		public const string OTBASE_FIELD = "OTBase";

		public const string OTAMOUNT_FIELD = "OTAmount";

		public const string NETSALARY_FIELD = "NetSalary";

		public const string OTISFIXED_FIELD = "OTIsFixed";

		public const string OTFACTOR_FIELD = "OTFactor";

		public const string OTFIXEDAMOUNT_FIELD = "OTFixedAmount";

		public const string EMPLOYEENAME_FIELD = "EmployeeName";

		public const string PAYROLLITEMID_FIELD = "PayrollItemID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string LOANSYSDOCID_FIELD = "LoanSysDocID";

		public const string AMOUNT_FIELD = "Amount";

		public const string PAYABLEAMOUNT_FIELD = "PayableAmount";

		public const string PAID_FIELD = "Paid";

		public const string INDEDUCTION_FIELD = "InDeduction";

		public const string PAYCODETYPE_FIELD = "PayCodeType";

		public const string ISFIXED_FIELD = "IsFixed";

		public DataTable SalarySheetTable => base.Tables["SalarySheet"];

		public DataTable SalarySheetDetailTable => base.Tables["SalarySheet_Detail"];

		public DataTable SalarySheetDetailItemsTable => base.Tables["SalarySheet_Detail_Item"];

		public SalarySheetData()
		{
			BuildDataTables();
		}

		public SalarySheetData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("SalarySheet");
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
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("Note", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("IsPosted", typeof(bool));
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("SalarySheet_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("WorkDays", typeof(decimal));
			columns.Add("AnnualLeaves", typeof(decimal));
			columns.Add("UnpaidAnnualLeaves", typeof(decimal));
			columns.Add("NormalLeaves", typeof(decimal));
			columns.Add("SickLeaves", typeof(decimal));
			columns.Add("Absent", typeof(decimal));
			columns.Add("Basic", typeof(decimal));
			columns.Add("Allowance", typeof(decimal));
			columns.Add("Deduction", typeof(decimal));
			columns.Add("LoanDeduction", typeof(decimal));
			columns.Add("OTHours", typeof(decimal));
			columns.Add("OTRate", typeof(decimal));
			columns.Add("OTAmount", typeof(decimal));
			columns.Add("OTBase", typeof(decimal));
			columns.Add("NetSalary", typeof(decimal));
			columns.Add("OTFixedAmount", typeof(decimal));
			columns.Add("OTIsFixed", typeof(bool));
			columns.Add("OTFactor", typeof(decimal));
			columns.Add("EmployeeName", typeof(string));
			columns.Add("NetDays", typeof(decimal));
			columns.Add("GrossSalary", typeof(decimal));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("SalarySheet_Detail_Item");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("LoanSysDocID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("PayType", typeof(byte));
			columns.Add("PayrollItemID", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("PayableAmount", typeof(decimal));
			columns.Add("Paid", typeof(decimal));
			columns.Add("InDeduction", typeof(bool));
			columns.Add("IsFixed", typeof(bool));
			columns.Add("PayCodeType", typeof(byte));
			base.Tables.Add(dataTable);
		}
	}
}
