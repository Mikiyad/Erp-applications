using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductBrandData : DataSet
	{
		public const string BRANDID_FIELD = "BrandID";

		public const string BRANDNAME_FIELD = "BrandName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string PREFERREDVENDOR_FIELD = "PreferredVendor";

		public const string NOTE_FIELD = "Note";

		public const string PRODUCTBRAND_TABLE = "Product_Brand";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ProductBrandTable => base.Tables["Product_Brand"];

		public ProductBrandData()
		{
			BuildDataTables();
		}

		public ProductBrandData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Product_Brand");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("BrandID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("BrandName", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("PreferredVendor", typeof(string));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
