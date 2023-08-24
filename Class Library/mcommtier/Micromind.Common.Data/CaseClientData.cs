using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CaseClientData : DataSet
	{
		public const string CASECLIENTID_FIELD = "CaseClientID";

		public const string NAME_FIELD = "CaseClientName";

		public const string FOREIGNNAME_FIELD = "ForeignName";

		public const string COMPANYNAME_FIELD = "CompanyName";

		public const string LEGALNAME_FIELD = "LegalName";

		public const string FIRSTNAME_FIELD = "FirstName";

		public const string LASTNAME_FIELD = "LastName";

		public const string MIDDLENAME_FIELD = "MiddleName";

		public const string NOTE_FIELD = "Note";

		public const string PARENTCASECLIENTID_FIELD = "ParentCaseClientID";

		public const string CUSTOMERGROUPID_FIELD = "CustomerGroupID";

		public const string CUSTOMERCLASSID_FIELD = "CustomerClassID";

		public const string ISHOLD_FIELD = "IsHold";

		public const string AREAID_FIELD = "AreaID";

		public const string COUNTRYID_FIELD = "CountryID";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string POSHIDDEN_FIELD = "POSHidden";

		public const string BILLTOADDRESSID_FIELD = "BillToAddressID";

		public const string SHIPTOADDRESSID_FIELD = "ShipToAddressID";

		public const string STATEMENTADDRESSID_FIELD = "StatementAddressID";

		public const string PAYMENTMETHODID_FIELD = "PaymentMethodID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string ACCEPTCHECKPAYMENT_FIELD = "AcceptCheckPayment";

		public const string ACCEPTPDC_FIELD = "AcceptPDC";

		public const string CREDITLIMITTYPE_FIELD = "CreditLimitType";

		public const string RATINGREMARKS_FIELD = "RatingRemarks";

		public const string ALLOWCONSIGNMENT_FIELD = "AllowConsignment";

		public const string CONSIGNCOMPERCENT_FIELD = "ConsignComPercent";

		public const string INSSTATUS_FIELD = "InsStatus";

		public const string INSAPPLICATIONDATE_FIELD = "InsApplicationDate";

		public const string INSEXPIRYDATE_FIELD = "InsExpiryDate";

		public const string INSREQUESTEDAMOUNT_FIELD = "InsRequestedAmount";

		public const string INSAPPROVEDAMOUNT_FIELD = "InsApprovedAmount";

		public const string INSPOLICYNUMBER_FIELD = "InsPolicyNumber";

		public const string INSREMARKS_FIELD = "InsRemarks";

		public const string INSRATING_FIELD = "InsRating";

		public const string INSPROVIDER_FIELD = "InsProviderID";

		public const string INSURANCEID_FIELD = "InsuranceID";

		public const string INSEFFECTIVEDATE_FIELD = "InsEffectiveDate";

		public const string BANKNAME_FIELD = "BankName";

		public const string BANKBRANCH_FIELD = "BankBranch";

		public const string BANKACCOUNTNUMBER_FIELD = "BankAccountNumber";

		public const string VATREGISTRATIONNUMBER_FIELD = "VATRegistrationNumber";

		public const string ARACCOUNTID_FIELD = "ARAccountID";

		public const string PRIMARYADDRESSID_FIELD = "PrimaryAddressID";

		public const string STATEMENTSENDMETHOD_FIELD = "StatementSendingMethod";

		public const string SHORTNAME_FIELD = "ShortName";

		public const string SUBAREAID_FIELD = "SubAreaID";

		public const string RATING_FIELD = "Rating";

		public const string RATINGDATE_FIELD = "RatingDate";

		public const string RATINGBY_FIELD = "RatingBy";

		public const string STATEMENTEMAIL_FIELD = "StatementEmail";

		public const string DELIVERYINSTRUCTIONS_FIELD = "DeliveryInstructions";

		public const string ACCOUNTINSTRUCTIONS_FIELD = "AccountInstructions";

		public const string DATEESTABLISHED_FIELD = "DateEstablished";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string CREDITREVIEWBY_FIELD = "CreditReviewBy";

		public const string CREDITREVIEWDATE_FIELD = "CreditReviewDate";

		public const string ISCUSTOMERSINCE_FIELD = "IsCustomerSince";

		public const string ISWEIGHTINVOICE_FIELD = "IsWeightInvoice";

		public const string COLLECTIONUSERID_FIELD = "CollectionUserID";

		public const string LEADSOURCEID_FIELD = "LeadSourceID";

		public const string CONTRACTEXPDATE_FIELD = "ContractExpDate";

		public const string LICENSEEXPDATE_FIELD = "LicenseExpDate";

		public const string PROFILEDETAILS_FIELD = "ProfileDetails";

		public const string USERDEFINED1_FIELD = "UserDefined1";

		public const string USERDEFINED2_FIELD = "UserDefined2";

		public const string USERDEFINED3_FIELD = "UserDefined3";

		public const string USERDEFINED4_FIELD = "UserDefined4";

		public const string PRICELEVELID_FIELD = "PriceLevelID";

		public const string SALESPERSONID_FIELD = "SalesPersonID";

		public const string BALANCE_FIELD = "Balance";

		public const string PDCAMOUNT_FIELD = "PDCAmount";

		public const string PARTYTYPE_FIELD = "PartyType";

		public const string PARTYID_FILED = "PartyID";

		public const string ISDEFENDANT_FIELD = "IsDefendant";

		public const string ISPLANTIFF_FIELD = "IsPlantiff";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string CREDITAMOUNT_FIELD = "CreditAmount";

		public const string PAYMENTTERMID_FIELD = "PaymentTermID";

		public const string ISINACTIVE_FIELD = "IsInactive";

		public const string ISPARENTPOSTING_FIELD = "IsParentPosting";

		public const string CASECLIENTADDRESS_TABLE = "Case_Client_Address";

		public const string CASECLIENTCONTACT_TABLE = "Case_Client_Contact_Detail";

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

		public const string CASECLIENT_TABLE = "Case_Client";

		public const string PHOTOPATH_FIELD = "PhotoPath";

		public const string CREDITCARDID_FIELD = "CreditCardID";

		public const string SENDMETHOD_FIELD = "SendMethod";

		public const string PREFERREDPAYMENT_FIELD = "PreferredPayment";

		public const string ENTITYTYPEID_FIELD = "EntityTypeID";

		public const string CUSTOMERRELATION_REL = "CustomerRel";

		public const string PAYEETYPEID_FIELD = "PayeeTypeID";

		public const string SOURCELEADID_FIELD = "SourceLeadID";

		public DataTable CaseClientTable => base.Tables["Case_Client"];

		public DataTable CaseClientAddressTable => base.Tables["Case_Client_Address"];

		public DataTable CaseClientContactTable => base.Tables["Case_Client_Contact_Detail"];

		public CaseClientData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		public CaseClientData()
		{
			BuildDataTables();
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Case_Client");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("CaseClientID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("CaseClientName", typeof(string)).AllowDBNull = false;
			columns.Add("ForeignName", typeof(string));
			columns.Add("CompanyName", typeof(string));
			columns.Add("LegalName", typeof(string));
			columns.Add("ContactName", typeof(string));
			columns.Add("PaymentTermID", typeof(string));
			columns.Add("AreaID", typeof(string));
			columns.Add("RatingRemarks", typeof(string));
			columns.Add("POSHidden", typeof(bool));
			columns.Add("IsWeightInvoice", typeof(bool));
			columns.Add("AcceptCheckPayment", typeof(bool));
			columns.Add("AcceptPDC", typeof(bool));
			columns.Add("CreditLimitType", typeof(byte));
			columns.Add("CreditAmount", typeof(decimal)).DefaultValue = 0;
			columns.Add("ARAccountID", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			columns.Add("IsHold", typeof(bool)).DefaultValue = false;
			columns.Add("PaymentMethodID", typeof(string));
			columns.Add("BillToAddressID", typeof(string));
			columns.Add("IsCustomerSince", typeof(DateTime));
			columns.Add("ShipToAddressID", typeof(string));
			columns.Add("StatementEmail", typeof(string));
			columns.Add("DeliveryInstructions", typeof(string));
			columns.Add("AccountInstructions", typeof(string));
			columns.Add("AllowConsignment", typeof(bool));
			columns.Add("ConsignComPercent", typeof(decimal));
			columns.Add("StatementAddressID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("SalesPersonID", typeof(string));
			columns.Add("PriceLevelID", typeof(string));
			columns.Add("ParentCaseClientID", typeof(string));
			columns.Add("CustomerGroupID", typeof(string));
			columns.Add("IsParentPosting", typeof(bool)).DefaultValue = false;
			columns.Add("CustomerClassID", typeof(string));
			columns.Add("CountryID", typeof(string));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("BankName", typeof(string));
			columns.Add("BankBranch", typeof(string));
			columns.Add("BankAccountNumber", typeof(string));
			columns.Add("VATRegistrationNumber", typeof(string));
			columns.Add("ShortName", typeof(string));
			columns.Add("SubAreaID", typeof(string));
			columns.Add("LeadSourceID", typeof(string));
			columns.Add("Rating", typeof(byte));
			columns.Add("RatingDate", typeof(DateTime));
			columns.Add("RatingBy", typeof(string));
			columns.Add("SourceLeadID", typeof(string));
			columns.Add("InsApplicationDate", typeof(DateTime));
			columns.Add("InsApprovedAmount", typeof(decimal));
			columns.Add("InsExpiryDate", typeof(DateTime));
			columns.Add("InsPolicyNumber", typeof(string));
			columns.Add("InsRating", typeof(byte));
			columns.Add("InsRemarks", typeof(string));
			columns.Add("InsRequestedAmount", typeof(decimal));
			columns.Add("InsStatus", typeof(byte));
			columns.Add("InsProviderID", typeof(string));
			columns.Add("InsuranceID", typeof(string));
			columns.Add("InsEffectiveDate", typeof(DateTime));
			columns.Add("DateEstablished", typeof(DateTime));
			columns.Add("LicenseExpDate", typeof(DateTime));
			columns.Add("ContractExpDate", typeof(DateTime));
			columns.Add("DivisionID", typeof(string));
			columns.Add("CollectionUserID", typeof(string));
			columns.Add("CreditReviewBy", typeof(string));
			columns.Add("CreditReviewDate", typeof(DateTime));
			columns.Add("UserDefined1", typeof(string));
			columns.Add("UserDefined2", typeof(string));
			columns.Add("UserDefined3", typeof(string));
			columns.Add("UserDefined4", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("StatementSendingMethod", typeof(byte));
			columns.Add("PrimaryAddressID", typeof(string)).DefaultValue = "PRIMARY";
			columns.Add("ProfileDetails", typeof(string));
			columns.Add("PartyType", typeof(string));
			columns.Add("PartyID", typeof(string));
			columns.Add("IsDefendant", typeof(bool));
			columns.Add("IsPlantiff", typeof(bool));
			columns.Add("CreatedBy", typeof(string));
			columns.Add("DateCreated", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("UpdatedBy", typeof(string));
			columns.Add("DateUpdated", typeof(DateTime)).DefaultValue = DateTime.Now;
			base.Tables.Add(dataTable);
			CaseClientAddressData caseClientAddressData = new CaseClientAddressData();
			dataTable = new DataTable("Case_Client_Address");
			foreach (DataColumn column in caseClientAddressData.CaseClientAddressTable.Columns)
			{
				dataTable.Columns.Add(column.ColumnName, column.DataType);
			}
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Case_Client_Contact_Detail");
			columns = dataTable.Columns;
			dataColumn = columns.Add("CaseClientID", typeof(string));
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
			columns.Add("Note", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}

		public void CreateCustomerRelation()
		{
			base.Relations.Add("CustomerRel", CaseClientTable.Columns["CaseClientID"], CaseClientAddressTable.Columns["CaseClientID"]);
		}
	}
}
