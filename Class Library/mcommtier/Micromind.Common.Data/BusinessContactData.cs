using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class BusinessContactData : DataSet
	{
		private const string BUSINESSCONTACTID_FIELD = "BusinessContactID";

		private const string COMPANYNAME_FIELD = "CompanyName";

		private const string ADDRESS_FIELD = "Address";

		private const string CITY_FIELD = "City";

		private const string COUNTRY_FIELD = "Country";

		private const string POSTALCODE_FIELD = "PostalCode";

		private const string JOBTITLE_FIELD = "JobTitle";

		private const string DEPARTMENT_FIELD = "Department";

		private const string OFFICE_FIELD = "Office";

		private const string PHONE_FIELD = "Phone";

		private const string FAX_FIELD = "Fax";

		private const string PAGER_FIELD = "Pager";

		private const string WEBPAGE_FIELD = "WebPage";

		private const string NOTES_FIELD = "Notes";

		private const string BUSINESSCONTACT_TABLE = "BusinessContacts";

		public BusinessContactData()
		{
			BuildDataTables();
		}

		public BusinessContactData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("BusinessContacts");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("BusinessContactID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("CompanyName", typeof(string));
			columns.Add("Address", typeof(string));
			columns.Add("City", typeof(string));
			columns.Add("Country", typeof(string));
			columns.Add("PostalCode", typeof(string));
			columns.Add("JobTitle", typeof(string));
			columns.Add("Department", typeof(string));
			columns.Add("Office", typeof(string));
			columns.Add("Phone", typeof(string));
			columns.Add("Fax", typeof(string));
			columns.Add("Pager", typeof(string));
			columns.Add("WebPage", typeof(string));
			columns.Add("Notes", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
