using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeAppraisalData : DataSet
	{
		public const string EMPLOYEEAPPRAISAL_TABLE = "Employee_Appraisal";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string NOTE_FIELD = "Note";

		public const string POSITIONID_FIELD = "PositionID";

		public const string KPIPARAMETER_FIELD = "KPIParameter";

		public const string WEIGHTAGE_FIELD = "Weightage";

		public const string REMARKS_FIELD = "Remarks";

		public const string POINTS_FIELD = "Points";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string ACTUAL_FIELD = "Actual";

		public const string ACH_FIELD = "ACH";

		public const string RATING_FIELD = "Rating";

		public const string SCALE_FIELD = "Scale";

		public const string TARGET_FIELD = "Target";

		public const string EMPLOYEEAPPRAISALDETAIL_TABLE = "Employee_Appraisal_Detail";

		public DataTable EmployeeAppraisalTable => base.Tables["Employee_Appraisal"];

		public DataTable EmployeeAppraisalDetailTable => base.Tables["Employee_Appraisal_Detail"];

		public EmployeeAppraisalData()
		{
			BuildDataTables();
		}

		public EmployeeAppraisalData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Appraisal");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			columns.Add("SysDocID", typeof(string)).AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("PositionID", typeof(string));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Employee_Appraisal_Detail");
			columns = dataTable.Columns;
			columns.Add("VoucherID", typeof(string));
			columns.Add("SysDocID", typeof(string));
			columns.Add("KPIParameter", typeof(string));
			columns.Add("Weightage", typeof(decimal));
			columns.Add("Points", typeof(decimal));
			columns.Add("Remarks", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Actual", typeof(decimal));
			columns.Add("ACH", typeof(decimal));
			columns.Add("Rating", typeof(decimal));
			columns.Add("Scale", typeof(string));
			columns.Add("Target", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
