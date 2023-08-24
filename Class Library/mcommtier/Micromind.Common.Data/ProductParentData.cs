using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductParentData : DataSet
	{
		public const string PRODUCTPARENT_TABLE = "Product_Parent";

		public const string PRODUCTPARENTID_FIELD = "ProductParentID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string PARENTTYPE_FIELD = "ParentType";

		public const string DIMENSIONS_FIELD = "Dimensions";

		public const string DIM1_FIELD = "Dim1";

		public const string DIM2_FIELD = "Dim2";

		public const string DIM3_FIELD = "Dim3";

		public const string LOOKUPCODE_FIELD = "LookupCode";

		public const string UNITPRICE1_FIELD = "UnitPrice1";

		public const string UNITPRICE2_FIELD = "UnitPrice2";

		public const string UNITPRICE3_FIELD = "UnitPrice3";

		public const string MINPRICE_FIELD = "MinPrice";

		public const string EXCLUDEFROMCATALOGE_FIELD = "ExcludeFromCatalogue";

		public const string ITEMTYPE_FIELD = "ItemType";

		public const string COSTMETHOD_FIELD = "CostMethod";

		public const string CATEGORYID_FIELD = "CategoryID";

		public const string QUANTITYPERUNIT_FIELD = "QuantityPerUnit";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string UNITID_FIELD = "UnitID";

		public const string PREFERREDVENDOR_FIELD = "PreferredVendor";

		public const string BRANDID_FIELD = "BrandID";

		public const string MANUFACTURERID_FIELD = "ManufacturerID";

		public const string ORIGIN_FIELD = "Origin";

		public const string NOTE_FIELD = "Note";

		public const string MATRIXATTRIBUTEDIMENSION_TABLE = "Matrix_Attribute_Dimension";

		public const string DIMENSION_FIELD = "Dimension";

		public const string DIMENSIONID_FIELD = "DimensionID";

		public const string ATTRIBUTEID_FIELD = "AttributeID";

		public const string ATTRIBUTENAME_FIELD = "AttributeName";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string PRODUCTPARENTCOMPONENTS_TABLE = "Product_Parent_Components";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string ATTRIBUTE1_FIELD = "Attribute1";

		public const string ATTRIBUTE2_FIELD = "Attribute2";

		public const string ATTRIBUTE3_FIELD = "Attribute3";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ProductParentTable => base.Tables["Product_Parent"];

		public DataTable MatrixAttributeDimensionTable => base.Tables["Matrix_Attribute_Dimension"];

		public DataTable ProdutParentComponentsTable => base.Tables["Product_Parent_Components"];

		public ProductParentData()
		{
			BuildDataTables();
		}

		public ProductParentData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Product_Parent");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ProductParentID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Description", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("ItemType", typeof(byte));
			columns.Add("CostMethod", typeof(byte)).DefaultValue = (byte)1;
			columns.Add("CategoryID", typeof(string));
			columns.Add("QuantityPerUnit", typeof(float)).DefaultValue = 1;
			columns.Add("UnitID", typeof(string));
			columns.Add("ExcludeFromCatalogue", typeof(bool));
			columns.Add("ParentType", typeof(byte));
			columns.Add("Dimensions", typeof(byte));
			columns.Add("Dim1", typeof(string));
			columns.Add("Dim2", typeof(string));
			columns.Add("Dim3", typeof(string));
			columns.Add("LookupCode", typeof(string));
			columns.Add("UnitPrice1", typeof(decimal));
			columns.Add("UnitPrice2", typeof(decimal));
			columns.Add("UnitPrice3", typeof(decimal));
			columns.Add("MinPrice", typeof(decimal));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = 0;
			columns.Add("PreferredVendor", typeof(string));
			columns.Add("BrandID", typeof(string));
			columns.Add("ManufacturerID", typeof(string));
			columns.Add("Origin", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Matrix_Attribute_Dimension");
			columns = dataTable.Columns;
			columns.Add("ProductParentID", typeof(string)).AllowDBNull = false;
			columns.Add("Dimension", typeof(byte)).AllowDBNull = false;
			columns.Add("DimensionID", typeof(string)).AllowDBNull = false;
			columns.Add("AttributeID", typeof(string)).AllowDBNull = false;
			columns.Add("AttributeName", typeof(string));
			columns.Add("RowIndex", typeof(int));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Product_Parent_Components");
			columns = dataTable.Columns;
			columns.Add("ProductParentID", typeof(string)).AllowDBNull = false;
			columns.Add("IsNewRow", typeof(bool)).DefaultValue = true;
			foreach (DataColumn column in new ProductData().ProductTable.Columns)
			{
				columns.Add(column.ColumnName, column.DataType);
			}
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Product");
			columns = dataTable.Columns;
			foreach (DataColumn column2 in new ProductData().ProductTable.Columns)
			{
				columns.Add(column2.ColumnName, column2.DataType);
			}
			base.Tables.Add(dataTable);
		}
	}
}
