using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeActivityTypeData : DataSet
	{
		public const string ACTIVITYTYPE_TABLE = "Employee_Activity_Type";

		public const string ACTIVITYTYPEID_FIELD = "ActivityTypeID";

		public const string ACTIVITYTYPENAME_FIELD = "ActivityTypeName";

		public const string ACTIVITYNATURE_FIELD = "ActivityNature";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ActivityTypeTable => base.Tables["Employee_Activity_Type"];

		public EmployeeActivityTypeData()
		{
			BuildDataTables();
		}

		public EmployeeActivityTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Activity_Type");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ActivityTypeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ActivityTypeName", typeof(string)).AllowDBNull = false;
			columns.Add("ActivityNature", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
