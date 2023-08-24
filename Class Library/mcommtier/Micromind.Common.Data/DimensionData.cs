using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class DimensionData : DataSet
	{
		public const string DIMENSIONID_FIELD = "DimensionID";

		public const string DIMENSIONNAME_FIELD = "DimensionName";

		public const string ISINACTIVE_FIELD = "IsInactive ";

		public const string DIMENSION_TABLE = "Dimension";

		public const string DIMENSIONATTRIBUTE_TABLE = "Dimension_Attribute";

		public const string ATTRIBUTEID_FIELD = "AttributeID";

		public const string ATTRIBUTEIDNAME_FIELD = "AttributeName";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable DimensionTable => base.Tables["Dimension"];

		public DataTable DimensionAttributeTable => base.Tables["Dimension_Attribute"];

		public DimensionData()
		{
			BuildDataTables();
		}

		public DimensionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Dimension");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("DimensionID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("DimensionName", typeof(string));
			columns.Add("IsInactive ", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Dimension_Attribute");
			columns = dataTable.Columns;
			dataColumn = columns.Add("AttributeID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("DimensionID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("AttributeName", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("IsInactive ", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
