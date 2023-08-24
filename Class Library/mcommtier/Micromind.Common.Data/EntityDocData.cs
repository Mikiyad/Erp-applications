using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EntityDocData : DataSet
	{
		public const string ENTITYDOC_TABLE = "EntityDocs";

		public const string ENTITYID_FIELD = "EntityID";

		public const string ENTITYSYSDOCID_FIELD = "EntitySysDocID";

		public const string ENTITYTYPE_FIELD = "EntityType";

		public const string ENTITYDOCNAME_FIELD = "EntityDocName";

		public const string ENTITYDOCPATH_FIELD = "EntityDocPath";

		public const string ENTITYDOCDESC_FIELD = "EntityDocDesc";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string ENTITYDOCKEYWORD_FIELD = "EntityDocKeyword";

		public const string FILEDATA_FIELD = "FileData";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EntityDocTable => base.Tables["EntityDocs"];

		public EntityDocData()
		{
			BuildDataTables();
		}

		public EntityDocData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("EntityDocs");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EntityID", typeof(string));
			DataColumn dataColumn2 = columns.Add("EntityType", typeof(byte));
			DataColumn dataColumn3 = columns.Add("EntityDocName", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataColumn2.AllowDBNull = false;
			dataColumn3.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[3]
			{
				dataColumn,
				dataColumn2,
				dataColumn3
			};
			columns.Add("EntitySysDocID", typeof(string)).AllowDBNull = true;
			columns.Add("EntityDocPath", typeof(string)).AllowDBNull = true;
			columns.Add("EntityDocDesc", typeof(string)).AllowDBNull = true;
			columns.Add("EntityDocKeyword", typeof(string)).AllowDBNull = true;
			columns.Add("RowIndex", typeof(short)).AllowDBNull = true;
			columns.Add("FileData", typeof(byte[])).AllowDBNull = true;
			base.Tables.Add(dataTable);
		}
	}
}
