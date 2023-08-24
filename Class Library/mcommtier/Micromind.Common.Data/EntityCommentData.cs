using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EntityCommentData : DataSet
	{
		public const string ENTITYCOMMENTS_TABLE = "Entity_Comments";

		public const string COMMENTID_FIELD = "CommentID";

		public const string ENTITYID_FIELD = "EntityID";

		public const string ENTITYSYSDOCID_FIELD = "EntitySysDocID";

		public const string ENTITYTYPE_FIELD = "EntityType";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EntityCommentsTable => base.Tables["Entity_Comments"];

		public EntityCommentData()
		{
			BuildDataTables();
		}

		public EntityCommentData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Entity_Comments");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CommentID", typeof(int));
			DataColumn dataColumn2 = columns.Add("EntityType", typeof(byte));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("RowIndex", typeof(short)).AllowDBNull = true;
			columns.Add("EntityID", typeof(string));
			columns.Add("EntitySysDocID", typeof(string));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
