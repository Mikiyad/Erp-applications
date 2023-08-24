using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CityData : DataSet
	{
		public const string CITY_TABLE = "City";

		public const string CITYID_FIELD = "CityID";

		public const string CITYNAME_FIELD = "CityName";

		public const string COUNTRYID_FIELD = "CountryID";

		public const string INACTIVE_FIELD = "IsInactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CityTable => base.Tables["City"];

		public CityData()
		{
			BuildDataTables();
		}

		public CityData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("City");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CityID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CityName", typeof(string)).AllowDBNull = false;
			columns.Add("CountryID", typeof(string));
			columns.Add("IsInactive", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
