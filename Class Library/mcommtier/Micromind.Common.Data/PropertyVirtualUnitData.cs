using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PropertyVirtualUnitData : DataSet
	{
		public const string PROPERTYVIRTUALUNITID_FIELD = "PropertyVirtualUnitID";

		public const string PROPERTYVIRTUALUNITNAME_FIELD = "PropertyVirtualUnitName";

		public const string PROPERTYUNITID_FIELD = "PropertyUnitID";

		public const string PROPERTYUNITNAME_FIELD = "PropertyUnitName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string ISVIRTUAL_FIELD = "IsVirtual";

		public const string PROPERTYVIRTUALUNIT_TABLE = "Property_VirtualUnit";

		public const string PROPERTYVIRTUALUNIT_DETAIL_TABLE = "Property_VirtualUnit_Detail";

		public const string PROPERTYUNIT_TABLE = "Property_Unit";

		public static string DESCRIPTION_FIELD = "Description";

		public static string SHARINGPERCENT_FIELD = "SharingPercent";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable PropertyVirtualUnitTable => base.Tables["Property_VirtualUnit"];

		public DataTable PropertyVirtualUnitDetailTable => base.Tables["Property_VirtualUnit_Detail"];

		public DataTable PropertyUnitTable => base.Tables["Property_Unit"];

		public PropertyVirtualUnitData()
		{
			BuildDataTables();
		}

		public PropertyVirtualUnitData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Property_VirtualUnit");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PropertyVirtualUnitID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PropertyVirtualUnitName", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Property_VirtualUnit_Detail");
			columns = dataTable.Columns;
			columns.Add("PropertyVirtualUnitID", typeof(string));
			columns.Add("PropertyUnitID", typeof(string));
			columns.Add(DESCRIPTION_FIELD, typeof(string));
			columns.Add(SHARINGPERCENT_FIELD, typeof(decimal));
			columns.Add("RowIndex", typeof(short)).AllowDBNull = false;
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Property_Unit");
			columns = dataTable.Columns;
			columns.Add("PropertyUnitID", typeof(string));
			columns.Add("PropertyUnitName", typeof(string));
			columns.Add("IsVirtual", typeof(bool)).DefaultValue = true;
			base.Tables.Add(dataTable);
		}
	}
}
