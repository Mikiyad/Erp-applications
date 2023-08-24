using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EmployeeSkillData : DataSet
	{
		public const string EMPLOYEESKILL_TABLE = "Employee_Skill_Detail";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string SKILLID_FIELD = "SkillID";

		public const string SKILLLEVEL_FIELD = "SkillLevel";

		public const string REMARKS_FIELD = "Remarks";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EmployeeSkillTable => base.Tables["Employee_Skill_Detail"];

		public EmployeeSkillData()
		{
			BuildDataTables();
		}

		public EmployeeSkillData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Skill_Detail");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EmployeeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("SkillID", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataColumn2.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("SkillLevel", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
