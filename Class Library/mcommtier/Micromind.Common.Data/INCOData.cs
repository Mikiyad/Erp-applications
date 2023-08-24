using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class INCOData : DataSet
	{
		public const string INCO_TABLE = "INCO";

		public const string INCOID_FIELD = "INCOID";

		public const string INCONAME_FIELD = "INCOName";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable INCOTable => base.Tables["INCO"];

		public INCOData()
		{
			BuildDataTables();
		}

		public INCOData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("INCO");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("INCOID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("INCOName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
