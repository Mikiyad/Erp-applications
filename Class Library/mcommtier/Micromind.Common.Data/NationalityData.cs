using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class NationalityData : DataSet
	{
		public const string NATIONALITY_TABLE = "Nationality";

		public const string NATIONALITYID_FIELD = "NationalityID";

		public const string NATIONALITYNAME_FIELD = "NationalityName";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable NationalityTable => base.Tables["Nationality"];

		public NationalityData()
		{
			BuildDataTables();
		}

		public NationalityData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Nationality");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("NationalityID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("NationalityName", typeof(string)).AllowDBNull = false;
			base.Tables.Add(dataTable);
		}
	}
}
