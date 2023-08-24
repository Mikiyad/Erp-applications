using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class HomeContactData : DataSet
	{
		private const string HOMECONTACTID_FIELD = "HomeContactID";

		private const string ADDRESS_FIELD = "Address";

		private const string CITY_FIELD = "City";

		private const string COUNTRY_FIELD = "Country";

		private const string PHONE_FIELD = "Phone";

		private const string FAX_FIELD = "Fax";

		private const string MOBILE_FIELD = "Mobile";

		private const string WEBPAGE_FIELD = "WebPage";

		private const string NOTES_FIELD = "Notes";

		private const string HOMECONTACT_TABLE = "[Home Contacts]";

		public HomeContactData()
		{
			BuildDataTables();
		}

		public HomeContactData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataColumnCollection columns = new DataTable("[Home Contacts]").Columns;
			DataColumn dataColumn = columns.Add("HomeContactID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("Address", typeof(string)).AllowDBNull = false;
			columns.Add("City", typeof(string)).AllowDBNull = false;
			columns.Add("Country", typeof(string)).AllowDBNull = false;
			columns.Add("Phone", typeof(string)).AllowDBNull = false;
			columns.Add("Fax", typeof(string));
			columns.Add("Mobile", typeof(string));
			columns.Add("WebPage", typeof(string));
			columns.Add("Notes", typeof(string));
			base.Tables.Add("[Home Contacts]");
		}
	}
}
