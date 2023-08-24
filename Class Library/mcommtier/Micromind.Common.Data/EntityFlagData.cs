using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EntityFlagData : DataSet
	{
		public const string ENTITYFLAG_TABLE = "Entity_Flag";

		public const string ENTITYFLAGDETAIL_TABLE = "Entity_Flag_Detail";

		public const string ENTITYFLAGID_FIELD = "FlagID";

		public const string ENTITYFLAGNAME_FIELD = "FlagName";

		public const string ENTITYTYPE_FIELD = "EntityType";

		public const string ENTITYID_FIELD = "EntityID";

		public const string COLOR_FIELD = "Color";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EntityFlagTable => base.Tables["Entity_Flag"];

		public DataTable EntityFlagDetailTable => base.Tables["Entity_Flag_Detail"];

		public EntityFlagData()
		{
			BuildDataTables();
		}

		public EntityFlagData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Entity_Flag");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("FlagID", typeof(short));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("FlagName", typeof(string)).AllowDBNull = false;
			columns.Add("EntityType", typeof(byte)).DefaultValue = (byte)0;
			columns.Add("Color", typeof(int));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Entity_Flag_Detail");
			columns = dataTable.Columns;
			dataColumn = columns.Add("EntityID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			columns.Add("FlagID", typeof(short)).AllowDBNull = false;
			columns.Add("EntityType", typeof(byte)).DefaultValue = (byte)0;
			base.Tables.Add(dataTable);
		}
	}
}
