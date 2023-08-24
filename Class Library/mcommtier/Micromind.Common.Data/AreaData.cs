using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class AreaData : DataSet
	{
		public const string AREA_TABLE = "Area";

		public const string AREAID_FIELD = "AreaID";

		public const string AREANAME_FIELD = "AreaName";

		public const string NOTE_FIELD = "Note";

		public const string COUNTRYID_FIELD = "CountryID";

		public const string PARENTAREAID_FIELD = "ParentAreaID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable AreaTable => base.Tables["Area"];

		public AreaData()
		{
			BuildDataTables();
		}

		public AreaData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Area");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("AreaID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("AreaName", typeof(string)).AllowDBNull = false;
			columns.Add("CountryID", typeof(string));
			columns.Add("ParentAreaID", typeof(string));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
