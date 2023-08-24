using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeProvisionTypeData : DataSet
	{
		public const string EMPLOYEEPROVISIONTYPE_TABLE = "Employee_Provision_Type";

		public const string PROVISIONTYPEID_FIELD = "ProvisionTypeID";

		public const string PROVISIONTYPENAME_FIELD = "ProvisionTypeName";

		public const string EXPENSEACCOUNTID_FIELD = "ExpenseAccountID";

		public const string PROVISIONACCOUNTID_FIELD = "ProvisionAccountID";

		public const string PROVISIONFOR_FIELD = "ProvisionFor";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EmployeeProvisionTypeTable => base.Tables["Employee_Provision_Type"];

		public EmployeeProvisionTypeData()
		{
			BuildDataTables();
		}

		public EmployeeProvisionTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Provision_Type");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ProvisionTypeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ProvisionTypeName", typeof(string)).AllowDBNull = false;
			columns.Add("ExpenseAccountID", typeof(string));
			columns.Add("ProvisionAccountID", typeof(string));
			columns.Add("ProvisionFor", typeof(int));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
