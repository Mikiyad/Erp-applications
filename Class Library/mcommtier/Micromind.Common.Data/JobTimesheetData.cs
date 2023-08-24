using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class JobTimesheetData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string REFERENCE_FIELD = "Reference";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REQUESTEDBY_FIELD = "RequestedBy";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string MONTH_FIELD = "Month";

		public const string YEAR_FIELD = "Year";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string UNIT_FIELD = "Unit";

		public const string QUANTITY_FIELD = "Quantity";

		public const string AMOUNT_FIELD = "Amount";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string FEEID_FIELD = "FeeID";

		public const string TASKID_FIELD = "TaskID";

		public const string TASKPERCENT_FIELD = "TaskPercent";

		public const string TSDATE_FIELD = "TSDate";

		public const string BEGINTIME_FIELD = "BeginTime";

		public const string ENDTIME_FIELD = "EndTime";

		public const string RATE_FIELD = "Rate";

		public const string PAYROLLITEM_FIELD = "PayrollItemType";

		public const string JOBTIMESHEET_TABLE = "Job_Timesheet";

		public const string JOBTIMESHEETDETAIL_TABLE = "Job_Timesheet_Detail";

		public DataTable JobTimesheetTable => base.Tables["Job_Timesheet"];

		public DataTable JobTimesheetDetailTable => base.Tables["Job_Timesheet_Detail"];

		public JobTimesheetData()
		{
			BuildDataTables();
		}

		public JobTimesheetData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Job_Timesheet");
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
			columns.Add("EmployeeID", typeof(string));
			columns.Add("Month", typeof(string));
			columns.Add("Year", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Job_Timesheet_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Unit", typeof(decimal));
			columns.Add("Amount", typeof(decimal));
			columns.Add("Quantity", typeof(float));
			columns.Add("Description", typeof(string));
			columns.Add("FeeID", typeof(string));
			columns.Add("TaskID", typeof(string));
			columns.Add("TaskPercent", typeof(decimal));
			columns.Add("TSDate", typeof(string));
			columns.Add("BeginTime", typeof(DateTime));
			columns.Add("EndTime", typeof(DateTime));
			columns.Add("Rate", typeof(string));
			columns.Add("PayrollItemType", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
