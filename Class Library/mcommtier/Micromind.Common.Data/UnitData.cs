using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class UnitData : DataSet
	{
		public const string UNIT_TABLE = "Unit";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITNAME_FIELD = "UnitName";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable UnitTable => base.Tables["Unit"];

		public UnitData()
		{
			BuildDataTables();
		}

		public UnitData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Unit");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("UnitID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("UnitName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
