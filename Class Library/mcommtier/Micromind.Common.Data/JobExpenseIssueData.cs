using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class JobExpenseIssueData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REQUESTEDBY_FIELD = "RequestedBy";

		public const string JOBEXPENSEISSUE_TABLE = "Job_Expense_Issue";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string EXPENSEID_FIELD = "ExpenseID";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string QUANTITY_FIELD = "Quantity";

		public const string AMOUNT_FIELD = "Amount";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string REMARKS_FIELD = "Remarks";

		public const string JOBEXPENSEISSUEDETAIL_TABLE = "Job_Expense_Issue_Detail";

		public DataTable JobExpenseIssueTable => base.Tables["Job_Expense_Issue"];

		public DataTable JobExpenseIssueDetailTable => base.Tables["Job_Expense_Issue_Detail"];

		public JobExpenseIssueData()
		{
			BuildDataTables();
		}

		public JobExpenseIssueData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job_Expense_Issue");
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
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("AccountID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("RequestedBy", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Expense_Issue_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ExpenseID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("UnitPrice", typeof(decimal));
			columns.Add("Amount", typeof(decimal));
			columns.Add("Quantity", typeof(float));
			columns.Add("Description", typeof(string));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
