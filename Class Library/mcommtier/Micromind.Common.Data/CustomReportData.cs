using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CustomReportData : DataSet
	{
		public const string CUSTOMREPORTID_FIELD = "CustomReportID";

		public const string CUSTOMREPORTNAME_FIELD = "CustomReportName";

		public const string PARENTMENUNAME_FIELD = "ParentMenuName";

		public const string TEMPLATENAME_FIELD = "TemplateName";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string REPORTDATA_FIELD = "ReportData";

		public const string QUERY_FIELD = "Query";

		public const string NOTE_FIELD = "Note";

		public const string DISPLAYNOTE_FIELD = "DisplayNote";

		public const string CUSTOMREPORT_TABLE = "Custom_Report";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CustomReportTable => base.Tables["Custom_Report"];

		public CustomReportData()
		{
			BuildDataTables();
		}

		public CustomReportData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Custom_Report");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CustomReportID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CustomReportName", typeof(string));
			columns.Add("ParentMenuName", typeof(string));
			columns.Add("TemplateName", typeof(string));
			columns.Add("ReportData", typeof(byte[]));
			columns.Add("Query", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("Note", typeof(string));
			columns.Add("DisplayNote", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
