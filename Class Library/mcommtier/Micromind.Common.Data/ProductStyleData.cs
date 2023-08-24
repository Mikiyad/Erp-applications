using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductStyleData : DataSet
	{
		public const string STYLEID_FIELD = "StyleID";

		public const string STYLENAME_FIELD = "StyleName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string PRODUCTSTYLE_TABLE = "Product_Style";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ProductStyleTable => base.Tables["Product_Style"];

		public ProductStyleData()
		{
			BuildDataTables();
		}

		public ProductStyleData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Product_Style");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("StyleID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("StyleName", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
