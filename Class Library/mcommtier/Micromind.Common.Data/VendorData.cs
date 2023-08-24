using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class VendorData : DataSet
	{
		public const string VENDORID_FIELD = "VendorID";

		public const string NAME_FIELD = "VendorName";

		public const string FOREIGNNAME_FIELD = "ForeignName";

		public const string COMPANYNAME_FIELD = "CompanyName";

		public const string LEGALNAME_FIELD = "LegalName";

		public const string FIRSTNAME_FIELD = "FirstName";

		public const string LASTNAME_FIELD = "LastName";

		public const string MIDDLENAME_FIELD = "MiddleName";

		public const string NOTE_FIELD = "Note";

		public const string PARENTVENDORID_FIELD = "ParentVendorID";

		public const string VENDORCLASSID_FIELD = "VendorClassID";

		public const string VENDORGROUPID_FIELD = "VendorGroupID";

		public const string ISHOLD_FIELD = "IsHold";

		public const string AREAID_FIELD = "AreaID";

		public const string COUNTRYID_FIELD = "CountryID";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string ISSERVICEPROVIDER_FIELD = "IsServiceProvider";

		public const string PAYMENTMETHODID_FIELD = "PaymentMethodID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string ACCEPTCHECKPAYMENT_FIELD = "AcceptCheckPayment";

		public const string ACCEPTPDC_FIELD = "AcceptPDC";

		public const string CREDITLIMITTYPE_FIELD = "CreditLimitType";

		public const string ALLOWCONSIGNMENT_FIELD = "AllowConsignment";

		public const string CONSIGNCOMPERCENT_FIELD = "ConsignComPercent";

		public const string ALLOWOPENACCOUNTPAYMENT_FIELD = "AllowOAP";

		public const string CONTRACTEXPDATE_FIELD = "ContractExpDate";

		public const string LICENSEEXPDATE_FIELD = "LicenseExpDate";

		public const string BANKNAME_FIELD = "BankName";

		public const string BANKBRANCH_FIELD = "BankBranch";

		public const string BANKACCOUNTNUMBER_FIELD = "BankAccountNumber";

		public const string SWIFTCODE_FIELD = "SwiftCode";

		public const string APACCOUNTID_FIELD = "APAccountID";

		public const string PRIMARYADDRESSID_FIELD = "PrimaryAddressID";

		public const string ISHOLDFORPAYMENT_FIELD = "IsHoldForPayment";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string TAXIDNUMBER_FIELD = "TaxIDNumber";

		public const string USERDEFINED1_FIELD = "UserDefined1";

		public const string USERDEFINED2_FIELD = "UserDefined2";

		public const string USERDEFINED3_FIELD = "UserDefined3";

		public const string USERDEFINED4_FIELD = "UserDefined4";

		public const string PRICELEVELID_FIELD = "PriceLevelID";

		public const string BUYERID_FIELD = "BuyerID";

		public const string BALANCE_FIELD = "Balance";

		public const string PDCAMOUNT_FIELD = "PDCAmount";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string CREDITAMOUNT_FIELD = "CreditAmount";

		public const string PAYMENTTERMID_FIELD = "PaymentTermID";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string VENDORADDRESS_TABLE = "Vendor_Address";

		public const string VENDORCONTACT_TABLE = "Vendor_Contact_Detail";

		public const string CONTACTNAME_FIELD = "ContactName";

		public const string JOBTITLE_FIELD = "JobTitle";

		public const string CONTACTID_FIELD = "ContactID";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string INITIALBALANCE_FIELD = "InitialBalance";

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

		public const string EMAIL2_FIELD = "Email2";

		public const string MOBILE_FIELD = "Mobile";

		public const string HOMEPAGE_FIELD = "HomePage";

		public const string DEFAULTSHIPPINGID_FIELD = "DefaultShippingID";

		public const string DEFAULTBILLINGID_FIELD = "DefaultBillingID";

		public const string VENDORS_TABLE = "Vendor";

		public const string PHOTOPATH_FIELD = "PhotoPath";

		public const string CREDITCARDID_FIELD = "CreditCardID";

		public const string SENDMETHOD_FIELD = "SendMethod";

		public const string PREFERREDPAYMENT_FIELD = "PreferredPayment";

		public const string ENTITYTYPEID_FIELD = "EntityTypeID";

		public const string VENDORRELATION_REL = "VendorRel";

		public const string PAYEETYPEID_FIELD = "PayeeTypeID";

		public DataTable VendorTable => base.Tables["Vendor"];

		public DataTable VendorAddressTable => base.Tables["Vendor_Address"];

		public DataTable VendorContactTable => base.Tables["Vendor_Contact_Detail"];

		public VendorData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public VendorData()
		{
			BuildDataTables();
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Vendor");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("VendorID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("VendorName", typeof(string)).AllowDBNull = false;
			columns.Add("ForeignName", typeof(string));
			columns.Add("CompanyName", typeof(string));
			columns.Add("LegalName", typeof(string));
			columns.Add("ContactName", typeof(string));
			columns.Add("PaymentTermID", typeof(string));
			columns.Add("AreaID", typeof(string));
			columns.Add("AcceptCheckPayment", typeof(bool));
			columns.Add("AcceptPDC", typeof(bool));
			columns.Add("AllowConsignment", typeof(bool));
			columns.Add("ConsignComPercent", typeof(decimal));
			columns.Add("CreditLimitType", typeof(byte));
			columns.Add("CreditAmount", typeof(decimal)).DefaultValue = 0;
			columns.Add("APAccountID", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("IsHold", typeof(bool)).DefaultValue = false;
			columns.Add("IsHoldForPayment", typeof(bool)).DefaultValue = false;
			columns.Add("PaymentMethodID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("BuyerID", typeof(string));
			columns.Add("PriceLevelID", typeof(string));
			columns.Add("ParentVendorID", typeof(string));
			columns.Add("VendorClassID", typeof(string));
			columns.Add("VendorGroupID", typeof(string));
			columns.Add("CountryID", typeof(string));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("LicenseExpDate", typeof(DateTime));
			columns.Add("ContractExpDate", typeof(DateTime));
			columns.Add("BankName", typeof(string));
			columns.Add("BankBranch", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("BankAccountNumber", typeof(string));
			columns.Add("SwiftCode", typeof(string));
			columns.Add("AllowOAP", typeof(bool));
			columns.Add("TaxOption", typeof(string));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxIDNumber", typeof(string));
			columns.Add("UserDefined1", typeof(string));
			columns.Add("UserDefined2", typeof(string));
			columns.Add("UserDefined3", typeof(string));
			columns.Add("UserDefined4", typeof(string));
			columns.Add("PrimaryAddressID", typeof(string)).DefaultValue = "PRIMARY";
			columns.Add("IsServiceProvider", typeof(string));
			columns.Add("CreatedBy", typeof(string));
			columns.Add("DateCreated", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("UpdatedBy", typeof(string));
			columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now;
			base.Tables.Add(dataTable);
			VendorAddressData vendorAddressData = new VendorAddressData();
			dataTable = new DataTable("Vendor_Address");
			foreach (DataColumn column in vendorAddressData.VendorAddressTable.Columns)
			{
				dataTable.Columns.Add(column.ColumnName, column.DataType);
			}
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Vendor_Contact_Detail");
			columns = dataTable.Columns;
			dataColumn = columns.Add("VendorID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			DataColumn dataColumn3 = columns.Add("ContactID", typeof(string));
			dataColumn3.AllowDBNull = false;
			dataColumn3.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn3
			};
			columns.Add("JobTitle", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
		}

		public void CreateVendorRelation()
		{
			base.Relations.Add("VendorRel", VendorTable.Columns["VendorID"], VendorAddressTable.Columns["VendorID"]);
		}
	}
}
