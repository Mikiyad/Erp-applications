using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductSizeData : DataSet
	{
		public const string SIZEID_FIELD = "SizeID";

		public const string SIZENAME_FIELD = "SizeName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string PRODUCTSIZE_TABLE = "Product_Size";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ProductSizeTable => base.Tables["Product_Size"];

		public ProductSizeData()
		{
			BuildDataTables();
		}

		public ProductSizeData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Product_Size");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SizeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("SizeName", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
