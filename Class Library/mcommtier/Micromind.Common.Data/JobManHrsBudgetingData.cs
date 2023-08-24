using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class JobManHrsBudgetingData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REQUESTEDBY_FIELD = "RequestedBy";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string JOBID_FIELD = "JobID";

		public const string JOBMANHRSBUDGETING_TABLE = "Job_Man_Hrs_Budgeting";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string EMPNO_FIELD = "EmployeeID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string EMPPOSITION_FIELD = "EmpPositionID";

		public const string REQUIREDHRS_FIELD = "RequiredHrs";

		public const string VARIANCE_FIELD = "Variance";

		public const string FROMDATE_FIELD = "FromDate";

		public const string TODATE_FIELD = "ToDate";

		public const string AMOUNT_FIELD = "Amount";

		public const string JOBMANHRSBUDGETINGDETAIL_TABLE = "Job_Man_Hrs_Budgeting_Detail";

		public DataTable JobManHrsBudgetingDataTable => base.Tables["Job_Man_Hrs_Budgeting"];

		public DataTable JobManHrsBudgetingDetailTable => base.Tables["Job_Man_Hrs_Budgeting_Detail"];

		public JobManHrsBudgetingData()
		{
			BuildDataTables();
		}

		public JobManHrsBudgetingData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job_Man_Hrs_Budgeting");
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
			columns.Add("Reference", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("RequestedBy", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("JobID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Man_Hrs_Budgeting_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("EmpPositionID", typeof(string));
			columns.Add("RequiredHrs", typeof(float)).DefaultValue = 0;
			columns.Add("FromDate", typeof(DateTime));
			columns.Add("ToDate", typeof(DateTime));
			columns.Add("Variance", typeof(decimal)).DefaultValue = 0;
			columns.Add("Amount", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
