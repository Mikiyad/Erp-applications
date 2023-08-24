using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class BankData : DataSet
	{
		public const string BANKID_FIELD = "BankID";

		public const string BANKNAME_FIELD = "BankName";

		public const string ROUTINGCODE_FIELD = "RoutingCode";

		public const string TAXIDNUMBER_FIELD = "TaxIDNumber";

		public const string CONTACTNAME_FIELD = "ContactName";

		public const string CONTACTTITLE_FIELD = "ContactTitle";

		public const string ADDRESS_FIELD = "Address";

		public const string ADDRESS2_FIELD = "Address2";

		public const string ADDRESS3_FIELD = "Address3";

		public const string CITY_FIELD = "City";

		public const string POSTALCODE_FIELD = "PostalCode";

		public const string STATE_FIELD = "State";

		public const string COUNTRY_FIELD = "Country";

		public const string PHONE_FIELD = "Phone";

		public const string FAX_FIELD = "Fax";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string BANK_TABLE = "Bank";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable BankTable => base.Tables["Bank"];

		public BankData()
		{
			BuildDataTables();
		}

		public BankData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Bank");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("BankID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("BankName", typeof(string));
			columns.Add("ContactName", typeof(string));
			columns.Add("ContactTitle", typeof(string));
			columns.Add("RoutingCode", typeof(string));
			columns.Add("TaxIDNumber", typeof(string));
			columns.Add("Address", typeof(string));
			columns.Add("Address2", typeof(string));
			columns.Add("Address3", typeof(string));
			columns.Add("City", typeof(string));
			columns.Add("PostalCode", typeof(string));
			columns.Add("State", typeof(string));
			columns.Add("Country", typeof(string));
			columns.Add("Phone", typeof(string));
			columns.Add("Fax", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
