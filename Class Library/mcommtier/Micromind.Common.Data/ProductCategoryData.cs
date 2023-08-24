using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductCategoryData : DataSet
	{
		public const string CATEGORYID_FIELD = "CategoryID";

		public const string CATEGORYNAME_FIELD = "CategoryName";

		public const string PARENTCATEGORYID_FIELD = "ParentCategoryID";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string AGEGROUP1_FIELD = "AgeGroup1";

		public const string AGEGROUP2_FIELD = "AgeGroup2";

		public const string AGEGROUP3_FIELD = "AgeGroup3";

		public const string STANDARDPRICEPERCENT_FIELD = "StandardPricePercent";

		public const string WHOLESALEPRICEPERCENT_FIELD = "WholesalePricePercent";

		public const string SPECIALPRICEPERCENT_FIELD = "SpecialPricePercent";

		public const string MINIMUMPRICEPERCENT_FIELD = "MinimumPricePercent";

		public const string COMMISSIONPERCENT_FIELD = "CommissionPercent";

		public const string NOTE_FIELD = "Note";

		public const string PRODUCTCATEGORY_TABLE = "Product_Category";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ProductCategoryTable => base.Tables["Product_Category"];

		public ProductCategoryData()
		{
			BuildDataTables();
		}

		public ProductCategoryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Product_Category");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CategoryID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CategoryName", typeof(string));
			columns.Add("ParentCategoryID", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("AgeGroup1", typeof(short));
			columns.Add("AgeGroup2", typeof(short));
			columns.Add("AgeGroup3", typeof(short));
			columns.Add("StandardPricePercent", typeof(string));
			columns.Add("WholesalePricePercent", typeof(string));
			columns.Add("SpecialPricePercent", typeof(string));
			columns.Add("MinimumPricePercent", typeof(string));
			columns.Add("CommissionPercent", typeof(decimal));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
