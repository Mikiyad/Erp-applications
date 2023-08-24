using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeTypeData : DataSet
	{
		public const string EMPLOYEETYPE_TABLE = "Employee_Type";

		public const string EMPLOYEETYPEID_FIELD = "TypeID";

		public const string EMPLOYEETYPENAME_FIELD = "TypeName";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string EOSID_FIELD = "EOSID";

		public const string CALENDARID_FIELD = "CalendarID";

		public const string DEFAULTOTTYPEID_FIELD = "DefaultOTTypeID";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string ISPAYROLL_FIELD = "IsPayroll";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string EMPLOYEETYPEDETAIL_TABLE = "Employee_Type_Detail";

		public const string LEAVETYPEID_FIELD = "LeaveTypeID";

		public const string OTTYPEID_FIELD = "OTTypeID";

		public const string LEAVESELECTION_FIELD = "LeaveSelection";

		public DataTable EmployeeTypeTable => base.Tables["Employee_Type"];

		public DataTable EmployeeTypeDetailTable => base.Tables["Employee_Type_Detail"];

		public EmployeeTypeData()
		{
			BuildDataTables();
		}

		public EmployeeTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Type");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TypeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TypeName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("AccountID", typeof(string));
			columns.Add("DefaultOTTypeID", typeof(string));
			columns.Add("EOSID", typeof(string));
			columns.Add("CalendarID", typeof(string));
			columns.Add("Inactive", typeof(bool));
			columns.Add("IsPayroll", typeof(bool));
			columns.Add("LeaveSelection", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Employee_Type_Detail");
			columns = dataTable.Columns;
			columns.Add("TypeID", typeof(string));
			columns.Add("LeaveTypeID", typeof(string));
			columns.Add("OTTypeID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
