using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class OverTimeEntryData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string DOCID_FIELD = "VoucherID";

		public const string APPROVEDBY_FIELD = "ApprovedBy";

		public const string APPROVALDATE_FIELD = "ApprovalDate";

		public const string MONTH_FIELD = "Month";

		public const string YEAR_FIELD = "Year";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string EMPLOYEENAME_FIELD = "EmployeeName";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string PAYROLLPERIOD_FIELD = "PayrollPeriod";

		public const string WORKDATE_FIELD = "WorkDate";

		public const string FROMTIME_FIELD = "FromTime";

		public const string TOTIME_FIELD = "ToTime";

		public const string HOURS_FIELD = "Hours";

		public const string OTHOURS_FIELD = "OTHours";

		public const string OTTYPE_FIELD = "OTType";

		public const string OTRATE_FIELD = "OTRate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string AMOUNT_FIELD = "Amount";

		public const string LEAVEDAYS_FIELD = "LeaveDays";

		public const string REMARKS_FIELD = "Remarks";

		public const string OVERTIMEENTRY_TABLE = "OverTimeEntry";

		public const string OVERTIMEENTRYDETAIL_TABLE = "OverTimeEntry_Detail";

		public DataTable OverTimeEntryTable => base.Tables["OverTimeEntry"];

		public DataTable OverTimeEntryDetailTable => base.Tables["OverTimeEntry_Detail"];

		public OverTimeEntryData()
		{
			BuildDataTables();
		}

		public OverTimeEntryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("OverTimeEntry");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			DataColumn dataColumn2 = columns.Add("SysDocID", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("Month", typeof(string));
			columns.Add("Year", typeof(string));
			columns.Add("ApprovedBy", typeof(string));
			columns.Add("ApprovalDate", typeof(DateTime));
			columns.Add("CreatedBy", typeof(string));
			columns.Add("DateCreated", typeof(DateTime));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("OverTimeEntry_Detail");
			columns = dataTable.Columns;
			columns.Add("VoucherID", typeof(string));
			columns.Add("SysDocID", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("EmployeeName", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("PayrollPeriod", typeof(DateTime));
			columns.Add("WorkDate", typeof(DateTime));
			columns.Add("FromTime", typeof(DateTime));
			columns.Add("ToTime", typeof(DateTime));
			columns.Add("OTHours", typeof(double));
			columns.Add("Hours", typeof(double));
			columns.Add("OTType", typeof(string));
			columns.Add("OTRate", typeof(decimal));
			columns.Add("Amount", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("LeaveDays", typeof(short));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
