using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductAttributeData : DataSet
	{
		public const string ATTRIBUTEID_FIELD = "AttributeID";

		public const string ATTRIBUTENAME_FIELD = "AttributeName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string PRODUCTATTRIBUTE_TABLE = "Product_Attribute";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ProductAttributeTable => base.Tables["Product_Attribute"];

		public ProductAttributeData()
		{
			BuildDataTables();
		}

		public ProductAttributeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Product_Attribute");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("AttributeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("AttributeName", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
