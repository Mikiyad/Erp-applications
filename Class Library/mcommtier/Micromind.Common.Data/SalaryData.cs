using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SalaryData : DataSet
	{
		public const string DOCID_FIELD = "VoucherID";

		public const string PERIOD_FIELD = "TransactionDate";

		public const string NOTE_FIELD = "Note";

		public const string APPROVEDBY_FIELD = "ApprovedBy";

		public const string APPROVALDATE_FIELD = "ApprovalDate";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string EMPLOYEENAME_FIELD = "EmployeeName";

		public const string PAYROLLPERIOD_FIELD = "PayrollPeriod";

		public const string DEDUCTIONCODE_FIELD = "DeductionCode";

		public const string ADDITIONCODE_FIELD = "AdditionCode";

		public const string REMARKS_FIELD = "Remarks";

		public const string AMOUNTTYPE_FIELD = "AmountType";

		public const string AMOUNT_FIELD = "Amount";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string QUANTITY_FIELD = "Quantity";

		public const string RATE_FIELD = "Rate";

		public const string SALARYDEDUCTION_TABLE = "Salary_Deduction";

		public const string SALARYDEDUCTION_DETAIL_TABLE = "Salary_Deduction_Detail";

		public const string SALARYADDITION_TABLE = "Salary_Addition";

		public const string SALARYADDITION_DETAIL_TABLE = "Salary_Addition_Detail";

		public DataTable SalaryDeductionTable => base.Tables["Salary_Deduction"];

		public DataTable SalaryDeductionDetailTable => base.Tables["Salary_Deduction_Detail"];

		public DataTable SalaryAdditionTable => base.Tables["Salary_Addition"];

		public DataTable SalaryAdditionDetailTable => base.Tables["Salary_Addition_Detail"];

		public SalaryData()
		{
			BuildDataTables();
		}

		public SalaryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Salary_Deduction");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("ApprovedBy", typeof(string));
			columns.Add("ApprovalDate", typeof(DateTime));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Salary_Deduction_Detail");
			columns = dataTable.Columns;
			columns.Add("VoucherID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("EmployeeName", typeof(string));
			columns.Add("PayrollPeriod", typeof(DateTime));
			columns.Add("DeductionCode", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Quantity", typeof(decimal));
			columns.Add("Rate", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Salary_Addition");
			columns = dataTable.Columns;
			dataColumn = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("ApprovedBy", typeof(string));
			columns.Add("ApprovalDate", typeof(DateTime));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Salary_Addition_Detail");
			columns = dataTable.Columns;
			columns.Add("VoucherID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("EmployeeName", typeof(string));
			columns.Add("PayrollPeriod", typeof(DateTime));
			columns.Add("AdditionCode", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountType", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Quantity", typeof(decimal));
			columns.Add("Rate", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
