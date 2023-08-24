using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductTypeData : DataSet
	{
		public const string TYPEID_FIELD = "TypeID";

		public const string TYPENAME_FIELD = "TypeName";

		public const string MAKEID_FIELD = "MakeID";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string PRODUCTTYPE_TABLE = "Product_Type";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ProductTypeTable => base.Tables["Product_Type"];

		public ProductTypeData()
		{
			BuildDataTables();
		}

		public ProductTypeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Product_Type");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("TypeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("TypeName", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("MakeID", typeof(string));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
