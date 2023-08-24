using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PartnerData : DataSet
	{
		public const string NAME_FIELD = "Name";

		public const string COMPANYNAME_FIELD = "CompanyName";

		public const string FIRSTNAME_FIELD = "FirstName";

		public const string LASTNAME_FIELD = "LastName";

		public const string MIDDLENAME_FIELD = "MiddleName";

		public const string SALUTATION_FIELD = "Salutation";

		public const string NOTE_FIELD = "Note";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string ISCUSTOMER_FIELD = "IsCustomer";

		public const string ISSUPPLIER_FIELD = "IsSupplier";

		public const string CREDITAMOUNT_FIELD = "CreditAmount";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string PAYMENTTERMID_FIELD = "TermID";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string INITIALBALANCE_FIELD = "InitialBalance";

		public const string PARTNERID_FIELD = "PartnerID";

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

		public const string PHONE2_FIELD = "Phone2";

		public const string FAX_FIELD = "Fax";

		public const string EMAIL_FIELD = "Email";

		public const string EMAIL2_FIELD = "Email2";

		public const string MOBILE_FIELD = "Mobile";

		public const string HOMEPAGE_FIELD = "HomePage";

		public const string DEFAULTSHIPPINGID_FIELD = "DefaultShippingID";

		public const string DEFAULTBILLINGID_FIELD = "DefaultBillingID";

		public const string PARTNERS_TABLE = "Partners";

		public const string PHOTOPATH_FIELD = "PhotoPath";

		public const string CREDITCARDID_FIELD = "CreditCardID";

		public const string PRICELEVELID_FIELD = "PriceLevelID";

		public const string SALESPERSON_FIELD = "SalesPerson";

		public const string SENDMETHOD_FIELD = "SendMethod";

		public const string PREFERREDPAYMENT_FIELD = "PreferredPayment";

		public const string ENTITYTYPEID_FIELD = "EntityTypeID";

		public const string PARTNERRELATION_REL = "PartnerRel";

		public const string PARTNERADDRESSES_TABLE = "[Partner Addresses]";

		public const string PAYEETYPEID_FIELD = "PayeeTypeID";

		public DataTable PartnerTable => base.Tables["Partners"];

		public DataTable PartnerAddressTable => base.Tables["[Partner Addresses]"];

		public PartnerData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public PartnerData()
		{
			BuildDataTables();
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Partners");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("PartnerID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Name", typeof(string)).AllowDBNull = false;
			columns.Add("CompanyName", typeof(string));
			columns.Add("FirstName", typeof(string));
			columns.Add("LastName", typeof(string));
			columns.Add("MiddleName", typeof(string));
			columns.Add("Salutation", typeof(string));
			columns.Add("CreditAmount", typeof(decimal)).DefaultValue = 0;
			columns.Add("IsCustomer", typeof(bool)).DefaultValue = false;
			columns.Add("IsSupplier", typeof(bool)).DefaultValue = false;
			columns.Add("TermID", typeof(short));
			columns.Add("PreferredPayment", typeof(short));
			columns.Add("SendMethod", typeof(byte));
			columns.Add("PriceLevelID", typeof(short));
			columns.Add("CreditCardID", typeof(int));
			columns.Add("SalesPerson", typeof(int));
			columns.Add("PayeeTypeID", typeof(short));
			columns.Add("InitialBalance", typeof(decimal)).DefaultValue = 0;
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("DateCreated", typeof(DateTime)).DefaultValue = DateTime.Today;
			columns.Add("Note", typeof(string));
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			columns.Add("EntityTypeID", typeof(int));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("[Partner Addresses]");
			columns = dataTable.Columns;
			dataColumn = columns.Add("PartnerID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("ContactName", typeof(string));
			columns.Add("ContactTitle", typeof(string));
			columns.Add("Address", typeof(string));
			columns.Add("Address2", typeof(string));
			columns.Add("Address3", typeof(string));
			columns.Add("City", typeof(string));
			columns.Add("PostalCode", typeof(string));
			columns.Add("State", typeof(string));
			columns.Add("Country", typeof(string));
			columns.Add("Phone", typeof(string));
			columns.Add("Phone2", typeof(string));
			columns.Add("Fax", typeof(string));
			columns.Add("Mobile", typeof(string));
			columns.Add("Email", typeof(string));
			columns.Add("Email2", typeof(string));
			columns.Add("HomePage", typeof(string));
			columns.Add("DefaultShippingID", typeof(int));
			columns.Add("DefaultBillingID", typeof(int));
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			base.Tables.Add(dataTable);
		}

		public void CreatePartnerRelation()
		{
			base.Relations.Add("PartnerRel", PartnerTable.Columns["PartnerID"], PartnerAddressTable.Columns["PartnerID"]);
		}
	}
}
