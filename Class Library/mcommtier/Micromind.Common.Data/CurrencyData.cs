using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CurrencyData : DataSet
	{
		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYNAME_FIELD = "CurrencyName";

		public const string EXCHANGERATE_FIELD = "ExchangeRate";

		public const string DESCRIPTION_FIELD = "Description";

		public const string RATETYPE_FIELD = "RateType";

		public const string RATEUPDATEDDATE_FIELD = "RateUpdatedDate";

		public const string INACTIVE_FIELD = "Inactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string ISBASE_FIELD = "IsBase";

		public const string CURRENCY_TABLE = "Currency";

		public const string CURRENCYEXCHANGERATE_TABLE = "Currency_Exchange_Rate";

		public DataTable CurrencyTable => base.Tables["Currency"];

		public CurrencyData()
		{
			BuildDataTables();
		}

		public CurrencyData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Currency");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CurrencyID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CurrencyName", typeof(string)).AllowDBNull = false;
			columns.Add("ExchangeRate", typeof(decimal));
			columns.Add("RateType", typeof(char));
			columns.Add("RateUpdatedDate", typeof(DateTime));
			columns.Add("Description", typeof(string));
			columns.Add("DateCreated", typeof(DateTime));
			columns.Add("CreatedBy", typeof(string));
			columns.Add("Inactive", typeof(bool));
			columns.Add("IsBase", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
