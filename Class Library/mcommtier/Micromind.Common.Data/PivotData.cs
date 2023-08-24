using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PivotData : DataSet
	{
		public const string PIVOT_TABLE = "Pivot_Report";

		public const string PIVOTFIELD_TABLE = "Pivot_Report_Field";

		public const string PIVOTID_FIELD = "PivotID";

		public const string PIVOTNAME_FIELD = "PivotName";

		public const string GROUPID_FIELD = "GroupID";

		public const string DATAQUERY_FIELD = "DataQuery";

		public const string CHARTLAYOUT_FIELD = "ChartLayout";

		public const string NOTE_FIELD = "Note";

		public const string HIDETOTAL_FIELD = "HideTotal";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string FIELDNAME_FIELD = "FieldName";

		public const string DISPLAYNAME_FIELD = "DisplayName";

		public const string DATATYPE_FIELD = "DataType";

		public const string AREA_FIELD = "Area";

		public const string AREAINDEX_FIELD = "AreaIndex";

		public const string GROUPINTERVAL_FIELD = "GroupInterval";

		public DataTable PivotTable => base.Tables["Pivot_Report"];

		public DataTable PivotFieldTable => base.Tables["Pivot_Report_Field"];

		public PivotData()
		{
			BuildDataTables();
		}

		public PivotData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Pivot_Report");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PivotID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PivotName", typeof(string));
			columns.Add("GroupID", typeof(string));
			columns.Add("DataQuery", typeof(string));
			columns.Add("ChartLayout", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("HideTotal", typeof(bool));
			columns.Add("Inactive", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Pivot_Report_Field");
			columns = dataTable.Columns;
			columns.Add("PivotID", typeof(string));
			columns.Add("FieldName", typeof(string));
			columns.Add("GroupInterval", typeof(short));
			columns.Add("DisplayName", typeof(string));
			columns.Add("DataType", typeof(string));
			columns.Add("Area", typeof(byte));
			columns.Add("AreaIndex", typeof(byte));
			base.Tables.Add(dataTable);
		}
	}
}
