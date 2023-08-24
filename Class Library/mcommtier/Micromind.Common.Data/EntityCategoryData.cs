using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EntityCategoryData : DataSet
	{
		public const string ENTITYCATEGORY_TABLE = "Entity_Category";

		public const string ENTITYCATEGORYDETAIL_TABLE = "Entity_Category_Detail";

		public const string ENTITYCATEGORYID_FIELD = "CategoryID";

		public const string ENTITYCATEGORYNAME_FIELD = "CategoryName";

		public const string ENTITYTYPE_FIELD = "EntityType";

		public const string ENTITYID_FIELD = "EntityID";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string PARENTCATEGORYID_FIELD = "ParentCategoryID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EntityCategoryTable => base.Tables["Entity_Category"];

		public DataTable EntityCategoryDetailTable => base.Tables["Entity_Category_Detail"];

		public EntityCategoryData()
		{
			BuildDataTables();
		}

		public EntityCategoryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Entity_Category");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CategoryID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CategoryName", typeof(string)).AllowDBNull = false;
			columns.Add("EntityType", typeof(byte)).DefaultValue = (byte)0;
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool));
			columns.Add("ParentCategoryID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Entity_Category_Detail");
			columns = dataTable.Columns;
			dataColumn = columns.Add("EntityID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			columns.Add("CategoryID", typeof(string)).AllowDBNull = false;
			columns.Add("EntityType", typeof(byte)).DefaultValue = (byte)0;
			base.Tables.Add(dataTable);
		}
	}
}
