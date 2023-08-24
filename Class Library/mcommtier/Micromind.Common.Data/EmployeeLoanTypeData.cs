using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeLoanTypeData : DataSet
	{
		public const string EMPLOYEELOANTYPE_TABLE = "Employee_Loan_Type";

		public const string EMPLOYEELOANTYPEID_FIELD = "LoanTypeID";

		public const string EMPLOYEELOANTYPENAME_FIELD = "LoanTypeName";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EmployeeLoanTypeTable => base.Tables["Employee_Loan_Type"];

		public EmployeeLoanTypeData()
		{
			BuildDataTables();
		}

		public EmployeeLoanTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Loan_Type");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("LoanTypeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("LoanTypeName", typeof(string)).AllowDBNull = false;
			columns.Add("AccountID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
