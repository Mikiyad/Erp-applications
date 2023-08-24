using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeLeaveDetailData : DataSet
	{
		public const string EMPLOYEELEAVEDETAIL_TABLE = "Employee_Leave_Detail";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string LEAVETYPEID_FIELD = "LeaveTypeID";

		public const string DAYS_FIELD = "Days";

		public const string REMARKS_FIELD = "Remarks";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EmployeeLeaveDetailTable => base.Tables["Employee_Leave_Detail"];

		public EmployeeLeaveDetailData()
		{
			BuildDataTables();
		}

		public EmployeeLeaveDetailData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Leave_Detail");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EmployeeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("LeaveTypeID", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataColumn2.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("Days", typeof(decimal));
			columns.Add("Remarks", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
