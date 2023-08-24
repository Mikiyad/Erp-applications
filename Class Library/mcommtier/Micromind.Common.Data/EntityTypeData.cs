using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EntityTypeData : DataSet
	{
		public const string ENTITYTYPEID_FIELD = "EntityTypeID";

		public const string ENTITYTYPENAME_FIELD = "EntityTypeName";

		public const string ENTITYTYPE_FIELD = "EntityType";

		public const string NOTE_FIELD = "Note";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string ENTITYTYPES_TABLE = "[Entity Types]";

		public DataTable EntityTypeTable => base.Tables["[Entity Types]"];

		public EntityTypeData()
		{
			BuildDataTables();
		}

		public EntityTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Entity Types]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EntityTypeID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("EntityTypeName", typeof(string)).AllowDBNull = false;
			columns.Add("EntityType", typeof(byte));
			columns.Add("Note", typeof(string));
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
