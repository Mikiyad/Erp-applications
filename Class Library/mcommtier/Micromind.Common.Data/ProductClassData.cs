using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductClassData : DataSet
	{
		public const string PRODUCTCLASS_TABLE = "Product_Class";

		public const string PRODUCTCLASSID_FIELD = "ClassID";

		public const string PRODUCTCLASSNAME_FIELD = "ClassName";

		public const string ITEMTYPE_FIELD = "ItemType";

		public const string COSTMETHOD_FIELD = "CostMethod";

		public const string CATEGORYID_FIELD = "CategoryID";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string UNITID_FIELD = "UnitID";

		public const string DEFAULTLOCATIONID_FIELD = "DefaultLocationID";

		public const string COGSACCOUNT_FIELD = "COGSAccount";

		public const string ASSETACCOUNT_FIELD = "AssetAccount";

		public const string INCOMEACCOUNT_FIELD = "IncomeAccount";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string TAXPRODUCTCLASSDETAIL_TABLE = "Tax_ProductClass_Detail";

		public const string TAXID_FIELD = "TaxID";

		public const string TAXPERCENT_FIELD = "TaxPercent";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ProductClassTable => base.Tables["Product_Class"];

		public DataTable TaxProductClassDetailTable => base.Tables["Tax_ProductClass_Detail"];

		public ProductClassData()
		{
			BuildDataTables();
		}

		public ProductClassData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Product_Class");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ClassID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ClassName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("ItemType", typeof(byte));
			columns.Add("CostMethod", typeof(byte)).DefaultValue = (byte)1;
			columns.Add("CategoryID", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("DefaultLocationID", typeof(string));
			columns.Add("COGSAccount", typeof(string));
			columns.Add("AssetAccount", typeof(string));
			columns.Add("IncomeAccount", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = 0;
			columns.Add("TaxOption", typeof(string));
			columns.Add("TaxGroupID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Tax_ProductClass_Detail");
			columns = dataTable.Columns;
			columns.Add("TaxID", typeof(string));
			columns.Add("ClassID", typeof(string));
			columns.Add("TaxPercent", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
