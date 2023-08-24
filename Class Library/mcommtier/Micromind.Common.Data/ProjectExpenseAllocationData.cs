using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProjectExpenseAllocationData : DataSet
	{
		public const string PROJECTEXPENSEALLOCATION_TABLE = "Project_Expense_Allocation";

		public const string PROJECTEXPENSEALLOCATIONDETAIL_TABLE = "Project_Expense_Allocation_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string NAME_FIELD = "Name";

		public const string MONTH_FIELD = "Month";

		public const string YEAR_FIELD = "Year";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ISVOID_FIELD = "IsVoid";

		public const string REFERENCE_FIELD = "Reference";

		public const string TRANSACTIONSTATUS_FIELD = "TransactionStatus";

		public const string DESCRIPTION_FIELD = "Description";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string PROJECTID_FIELD = "ProjectID";

		public const string AMOUNT_FIELD = "Amount";

		public const string HOURS_FIELD = "Hours";

		public const string SHEETSYSDOCID_FIELD = "SheetSysDocID";

		public const string SHEETVOUCHERID_FIELD = "SheetVoucherID";

		public const string SHEETROWINDEX_FIELD = "SheetRowIndex";

		public const string OTAMOUNT_FIELD = "OTAmount";

		public const string GROSSSALARY_FIELD = "GrossSalary";

		public const string COSTCATEGROYID_FIELD = "CostCategoryID";

		public DataTable ProjectExpenseAllocationTable => base.Tables["Project_Expense_Allocation"];

		public DataTable ProjectExpenseAllocationDetailTable => base.Tables["Project_Expense_Allocation_Detail"];

		public ProjectExpenseAllocationData()
		{
			BuildDataTables();
		}

		public ProjectExpenseAllocationData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Project_Expense_Allocation");
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
			columns.Add("Name", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("Month", typeof(byte));
			columns.Add("Year", typeof(byte));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Reference", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("TransactionStatus", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Project_Expense_Allocation_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("SheetSysDocID", typeof(string));
			columns.Add("SheetVoucherID", typeof(string));
			columns.Add("SheetRowIndex", typeof(int));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("ProjectID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("Hours", typeof(decimal));
			columns.Add("Amount", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("OTAmount", typeof(decimal));
			columns.Add("GrossSalary", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
