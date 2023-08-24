using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class StoreData : DataSet
	{
		public const string STORES_TABLE = "Stores";

		public const string STOREID_FIELD = "StoreID";

		public const string STORENAME_FIELD = "StoreName";

		public const string ADDRESS_FIELD = "Address";

		public const string ADDRESS2_FIELD = "Address2";

		public const string ADDRESS3_FIELD = "Address3";

		public const string CITY_FIELD = "City";

		public const string COUNTRY_FIELD = "Country";

		public const string POSTALCODE_FIELD = "PostalCode";

		public const string STATE_FIELD = "State";

		public const string PHONE_FIELD = "Phone";

		public const string PHONE2_FIELD = "Phone2";

		public const string FAX_FIELD = "Fax";

		public const string ISSHOP_FIELD = "IsShop";

		public const string ISWAREHOUSE_FIELD = "IsWarehouse";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string NOTES_FIELD = "Notes";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public DataTable StoreTable => base.Tables["Stores"];

		public StoreData()
		{
			BuildDataTables();
		}

		public StoreData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Stores");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("StoreID", typeof(short));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("StoreName", typeof(string)).AllowDBNull = false;
			columns.Add("Address", typeof(string));
			columns.Add("Address2", typeof(string));
			columns.Add("Address3", typeof(string));
			columns.Add("City", typeof(string));
			columns.Add("Country", typeof(string));
			columns.Add("PostalCode", typeof(string));
			columns.Add("State", typeof(string));
			columns.Add("Phone", typeof(string));
			columns.Add("Phone2", typeof(string));
			columns.Add("Fax", typeof(string));
			columns.Add("IsShop", typeof(bool)).DefaultValue = false;
			columns.Add("IsWarehouse", typeof(bool)).DefaultValue = false;
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("Notes", typeof(string));
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}
	}
}
