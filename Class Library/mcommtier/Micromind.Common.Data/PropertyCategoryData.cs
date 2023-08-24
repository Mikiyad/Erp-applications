using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PropertyCategoryData : DataSet
	{
		public const string PROPERTYCATEGORY_TABLE = "Property_Category";

		public const string PROPERTYCATEGORYDETAIL_TABLE = "Property_Category_Detail";

		public const string PROPERTYCATEGORYID_FIELD = "CategoryID";

		public const string PROPERTYCATEGORYNAME_FIELD = "CategoryName";

		public const string ENTITYTYPE_FIELD = "EntityType";

		public const string PROPERTYID_FIELD = "PropertyID";

		public const string NOTE_FIELD = "Note";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable PropertyCategoryTable => base.Tables["Property_Category"];

		public DataTable PropertyCategoryDetailTable => base.Tables["Property_Category_Detail"];

		public PropertyCategoryData()
		{
			BuildDataTables();
		}

		public PropertyCategoryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Property_Category");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CategoryID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CategoryName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Property_Category_Detail");
			columns = dataTable.Columns;
			dataColumn = columns.Add("PropertyID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			columns.Add("CategoryID", typeof(string)).AllowDBNull = false;
			columns.Add("EntityType", typeof(byte)).DefaultValue = (byte)14;
			base.Tables.Add(dataTable);
		}
	}
}
