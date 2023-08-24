using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeDependentData : DataSet
	{
		public const string EMPLOYEEDEPENDENT_TABLE = "Employee_Dependent";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string DEPENDENTNAME_FIELD = "DependentName";

		public const string GENDER_FIELD = "Gender";

		public const string BIRTHDATE_FIELD = "BirthDate";

		public const string ADDRESS_FIELD = "Address";

		public const string NATIONALID_FIELD = "NationalID";

		public const string RELATION_FIELD = "Relation";

		public const string PHONE_FIELD = "Phone";

		public const string COMMENT_FIELD = "Comment";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EmployeeDependentTable => base.Tables["Employee_Dependent"];

		public EmployeeDependentData()
		{
			BuildDataTables();
		}

		public EmployeeDependentData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Dependent");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EmployeeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("DependentName", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("Gender", typeof(string));
			columns.Add("BirthDate", typeof(DateTime));
			columns.Add("Address", typeof(string));
			columns.Add("NationalID", typeof(string));
			columns.Add("Relation", typeof(string));
			columns.Add("Phone", typeof(string));
			columns.Add("Comment", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
