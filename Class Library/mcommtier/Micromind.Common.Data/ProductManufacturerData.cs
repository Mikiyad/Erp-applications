using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProductManufacturerData : DataSet
	{
		public const string MANUFACTURERID_FIELD = "ManufacturerID";

		public const string MANUFACTURERNAME_FIELD = "ManufacturerName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string PRODUCTMANUFACTURER_TABLE = "Product_Manufacturer";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ProductManufacturerTable => base.Tables["Product_Manufacturer"];

		public ProductManufacturerData()
		{
			BuildDataTables();
		}

		public ProductManufacturerData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Product_Manufacturer");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("ManufacturerID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("ManufacturerName", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
