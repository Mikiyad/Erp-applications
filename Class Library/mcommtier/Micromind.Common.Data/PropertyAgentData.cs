using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PropertyAgentData : DataSet
	{
		public const string PROPERTYAGENT_TABLE = "Property_Agent";

		public const string PROPERTYAGENTID_FIELD = "PropertyAgentID";

		public const string PROPERTYAGENTNAME_FIELD = "PropertyAgentName";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable PropertyAgentTable => base.Tables["Property_Agent"];

		public PropertyAgentData()
		{
			BuildDataTables();
		}

		public PropertyAgentData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Property_Agent");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PropertyAgentID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PropertyAgentName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
