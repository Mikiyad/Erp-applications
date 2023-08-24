using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeePerformanceData : DataSet
	{
		public const string EMPLOYEEPERFORMANCE_TABLE = "Employee_Performance";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string FROMMONTH_FIELD = "FromMonth";

		public const string TOMONTH_FIELD = "ToMonth";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string NOTE_FIELD = "Note";

		public const string POSITIONID_FIELD = "PositionID";

		public const string PERFORMANCEPARAMETER_FIELD = "PerformanceParameter";

		public const string SCORE_FIELD = "Score";

		public const string REMARKS_FIELD = "Remarks";

		public const string PLUSSCORE_FIELD = "PlusScore";

		public const string MINUSSCORE_FIELD = "MinusScore";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string EMPLOYEEPERFORMANCEDETAIL_TABLE = "Employee_Performance_Detail";

		public DataTable EmployeePerformanceTable => base.Tables["Employee_Performance"];

		public DataTable EmployeePerformanceDetailTable => base.Tables["Employee_Performance_Detail"];

		public EmployeePerformanceData()
		{
			BuildDataTables();
		}

		public EmployeePerformanceData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Performance");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			columns.Add("SysDocID", typeof(string)).AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("FromMonth", typeof(DateTime));
			columns.Add("ToMonth", typeof(DateTime));
			columns.Add("EmployeeID", typeof(string));
			columns.Add("PositionID", typeof(string));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Employee_Performance_Detail");
			columns = dataTable.Columns;
			columns.Add("VoucherID", typeof(string));
			columns.Add("SysDocID", typeof(string));
			columns.Add("PerformanceParameter", typeof(string));
			columns.Add("Score", typeof(decimal));
			columns.Add("PlusScore", typeof(decimal));
			columns.Add("Remarks", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("MinusScore", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
