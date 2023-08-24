using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CountryData : DataSet
	{
		public const string COUNTRYID_FIELD = "CountryID";

		public const string COUNTRYNAME_FIELD = "CountryName";

		public const string PHONECODE_FIELD = "PhoneCode";

		public const string CURRENCYCODE_FIELD = "CurrencyCode";

		public const string NOTE_FIELD = "Note";

		public const string COUNTRY_TABLE = "Country";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable CountryTable => base.Tables["Country"];

		public CountryData()
		{
			BuildDataTables();
		}

		public CountryData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Country");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CountryID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CountryName", typeof(string)).AllowDBNull = false;
			columns.Add("PhoneCode", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("CurrencyCode", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
