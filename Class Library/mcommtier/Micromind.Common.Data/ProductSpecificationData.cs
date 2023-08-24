using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductSpecificationData : DataSet
	{
		public const string SPECIFICATIONID_FIELD = "SpecificationID";

		public const string SPECIFICATIONNAME_FIELD = "SpecificationName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string PRODUCTSPECIFICATION_TABLE = "Product_Specification";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ProductSpecificationTable => base.Tables["Product_Specification"];

		public ProductSpecificationData()
		{
			BuildDataTables();
		}

		public ProductSpecificationData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Product_Specification");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SpecificationID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("SpecificationName", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
