using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class GradeData : DataSet
	{
		public const string GRADE_TABLE = "Employee_Grade";

		public const string GRADEID_FIELD = "GradeID";

		public const string GRADENAME_FIELD = "GradeName";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable GradeTable => base.Tables["Employee_Grade"];

		public GradeData()
		{
			BuildDataTables();
		}

		public GradeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Employee_Grade");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("GradeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("GradeName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
