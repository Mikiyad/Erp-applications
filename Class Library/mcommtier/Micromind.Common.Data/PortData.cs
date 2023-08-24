using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PortData : DataSet
	{
		public const string PORT_TABLE = "Port";

		public const string PORTID_FIELD = "PortID";

		public const string PORTNAME_FIELD = "PortName";

		public const string NOTE_FIELD = "Note";

		public const string COUNTRYID_FIELD = "CountryID";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable PortTable => base.Tables["Port"];

		public PortData()
		{
			BuildDataTables();
		}

		public PortData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Port");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PortID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PortName", typeof(string)).AllowDBNull = false;
			columns.Add("CountryID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("Inactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
