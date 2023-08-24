using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PropertyClassData : DataSet
	{
		public const string PROPERTYCLASS_TABLE = "Property_Class";

		public const string PROPERTYCLASSID_FIELD = "PropertyClassID";

		public const string PROPERTYCLASSNAME_FIELD = "PropertyClassName";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable PropertyClassTable => base.Tables["Property_Class"];

		public PropertyClassData()
		{
			BuildDataTables();
		}

		public PropertyClassData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Property_Class");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PropertyClassID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("PropertyClassName", typeof(string)).AllowDBNull = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
