using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class WebDashboardData : DataSet
	{
		public const string WEBDASHBOARD_TABLE = "WebDashboard";

		public const string WEBDASHBOARDID_FIELD = "WebDashboardID";

		public const string NAME_FIELD = "Name";

		public const string USERID_FIELD = "UserID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string ZONEINDEX_FIELD = "ZoneIndex";

		public const string ZONELAYOUT_FIELD = "ZoneLayout";

		public const string LAYOUT_FIELD = "Layout";

		public const string SELECTEDGADGETS = "SelectedGadgets";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable WebDashboardTable => base.Tables["WebDashboard"];

		public WebDashboardData()
		{
			BuildDataTables();
		}

		public WebDashboardData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("WebDashboard");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("WebDashboardID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn2 = columns.Add("UserID", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("Name", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("ZoneIndex", typeof(short));
			columns.Add("ZoneLayout", typeof(string));
			columns.Add("Layout", typeof(string));
			columns.Add("SelectedGadgets", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
