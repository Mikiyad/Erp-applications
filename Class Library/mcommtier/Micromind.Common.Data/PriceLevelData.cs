using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PriceLevelData : DataSet
	{
		public const string PRICELEVEL_TABLE = "Price_Level";

		public const string PRICELEVELID_FIELD = "PriceLevelID";

		public const string PRICELEVELNAME_FIELD = "PriceLevelName";

		public const string NOTE_FIELD = "Note";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable PriceLevelTable => base.Tables["Price_Level"];

		public PriceLevelData()
		{
			BuildDataTables();
		}

		public PriceLevelData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Price_Level");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PriceLevelID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			columns.Add("PriceLevelName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
