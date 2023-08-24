using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CompanyInformationData : DataSet
	{
		public const string SETUPID_FIELD = "SetupID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string COMPANYNAME_FIELD = "CompanyName";

		public const string BASECURRENCYID_FIELD = "BaseCurrencyID";

		public const string DBVERSION_FIELD = "DBVersion";

		public const string LASTBACKUPDATE_FIELD = "LastBackupDate";

		public const string NOTES_FIELD = "Notes";

		public const string LOGO_FIELD = "Logo";

		public const string USELOGO_FIELD = "UseLogo";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string COMPANYADDRESS_TABLE = "Company_Address";

		public const string ISTAX_FIELD = "IsTax";

		public const string TAXPERCENT_FIELD = "TaxPercent";

		public const string FISCALFIRSTMONTH_FIELD = "FiscalFirstMonth";

		public const string ISDNINVENTORY_FIELD = "IsDNInventory";

		public const string ITEMPRICE1NAME_FIELD = "ItemPrice1Name";

		public const string ITEMPRICE2NAME_FIELD = "ItemPrice2Name";

		public const string ITEMPRICE3NAME_FIELD = "ItemPrice3Name";

		public const string PDCRECEIVEDACCOUNTID_FIELD = "PDCReceivedAccountID";

		public const string PDCISSUEDACCOUNTID_FIELD = "PDCIssuedAccountID";

		public const string ACCOUNTUD1_FIELD = "AccountUD1";

		public const string ACCOUNTUD2_FIELD = "AccountUD2";

		public const string ACCOUNTUD3_FIELD = "AccountUD3";

		public const string ACCOUNTUD4_FIELD = "AccountUD4";

		public const string CUSTOMERUD1_FIELD = "CustomerUD1";

		public const string CUSTOMERUD2_FIELD = "CustomerUD2";

		public const string CUSTOMERUD3_FIELD = "CustomerUD3";

		public const string CUSTOMERUD4_FIELD = "CustomerUD4";

		public const string VENDORUD1_FIELD = "VendorUD1";

		public const string VENDORUD2_FIELD = "VendorUD2";

		public const string VENDORUD3_FIELD = "VendorUD3";

		public const string VENDORUD4_FIELD = "VendorUD4";

		public const string EMPLOYEEUD1_FIELD = "EMPLOYEEUD1";

		public const string EMPLOYEEUD2_FIELD = "EMPLOYEEUD2";

		public const string EMPLOYEEUD3_FIELD = "EMPLOYEEUD3";

		public const string EMPLOYEEUD4_FIELD = "EMPLOYEEUD4";

		public const string INVENTORYUD1_FIELD = "InventoryUD1";

		public const string INVENTORYUD2_FIELD = "InventoryUD2";

		public const string INVENTORYUD3_FIELD = "InventoryUD3";

		public const string INVENTORYUD4_FIELD = "InventoryUD4";

		public const string MINPRICESALEACTION_FIELD = "MinPriceSaleAction";

		public const string MINQTYPACKINGACTION_FIELD = "MinQtyPackingAction";

		public const string MINPRICESALEPASS_FIELD = "MinPriceSalePass";

		public const string OVERCLACTION_FIELD = "OverCLAction";

		public const string OVERCLPASS_FIELD = "OverCLPass";

		public const string NEGATIVEQUANTITYACTION_FIELD = "NegativeQuantityAction";

		public const string REMOVEALLOCATIONACTION_FIELD = "RemoveAllocationAction";

		public const string NEGATIVEQUANTITYPASS_FIELD = "NegativeQuantityPass";

		public const string PRICELESSCOSTACTION_FIELD = "PricelessCostAction";

		public const string PRICELESSCOSTPASS_FIELD = "PricelessCostPass";

		public const string INCLUDEPDC_FIELD = "IncludePDC";

		public const string USEMULTICURRENCY_FIELD = "UseMultiCurrency";

		public const string USEJOBCOSTING_FIELD = "UseJobCosting";

		public const string AGINGBYDATE_FIELD = "AgingByDate";

		public const string LOCALPURCHASEFLOW_FIELD = "LocalPurchaseFlow";

		public const string IMPORTPURCHASEFLOW_FIELD = "ImportPurchaseFlow";

		public const string SALESFLOW_FIELD = "SalesFlow";

		public const string PRIMARYADDRESSID_FIELD = "PrimaryAddressID";

		public const string CLOSINGDATE_FIELD = "ClosingDate";

		public const string CLOSINGPASSWORD_FIELD = "ClosingPassword";

		public const string FILESAVINGPATH_FIELD = "FileSavingPath";

		public const string TEMPLATEPATHLOCATION_FIELD = "TemplatePathLocation";

		public const string TEMPLATEPATHFOLDER_FIELD = "TemplatePathFolder";

		public const string TEMPLATEPATHSERVER_FIELD = "TemplatePathServer";

		public const string COMPANYWPSID_FIELD = "CompanyWPSID";

		public const string TOTALDAYHOURS_FIELD = "TotalWorkingDayHours";

		public const string TOTALMONTHHOURS_FIELD = "TotalWorkingMonthHours";

		public const string OFFDAY_FIELD = "OffDay";

		public const string DAYSINMONTH_FIELD = "DaysInMonth";

		public const string THIRTYDAYS_FIELD = "ThirtyDays";

		public const string ANNUAL_FIELD = "Annual";

		public const string AUTORESUMPTIONDAYS_FIELD = "AutoResumptionDays";

		public const string HRANALYSISGROUP_FIELD = "HRAnalysisGroup";

		public const string HRANALYSISPREFIX_FIELD = "HRAnalysisPrefix";

		public const string VEHICLEANALYSISGROUP_FIELD = "VehicleAnalysisGroup";

		public const string VEHICLEANALYSISPREFIX_FIELD = "VehicleAnalysisPrefix";

		public const string LEGALANALYSISGROUP_FIELD = "LegalAnalysisGroup";

		public const string LEGALANALYSISPREFIX_FIELD = "LegalAnalysisPrefix";

		public const string PATIENTANALYSISGROUP_FIELD = "PatientAnalysisGroup";

		public const string PATIENTANALYSISPREFIX_FIELD = "PatientAnalysisPrefix";

		public const string LOADITEMDESCFROMPRICELIST_FIELD = "LoadItemDescFromPriceList";

		public const string CURDECIMALPOINT_FIELD = "CurDecimalPoint";

		public const string DISCOUNTWRITEOFFPERCENT_FIELD = "DiscountWriteoffPercent";

		public const string ISLOCATIONCOSTING_FIELD = "IsLocationCosting";

		public const string COMPANYINFORMATION_TABLE = "Company";

		public const string EMAILCONFIG_TABLE = "Email_Config";

		public const string EMAILID_FIELD = "EmailID";

		public const string EMAILADDRESS_FIELD = "EmailAddress";

		public const string EMAILOUTGOINGSERVER_FIELD = "OutgoingServer";

		public const string EMAILINCOMMINGSERVER_FIELD = "IncommingServer";

		public const string EMAILPASS_FIELD = "EmailPass";

		public const string EMAILUSERNAME_FIELD = "UserName";

		public const string EMAILSENDERNAME_FIELD = "SenderName";

		public const string EMAILSMTPPORT_FIELD = "EmailSMTPPort";

		public const string CCSALESPERSON_FIELD = "CCSalesperson";

		public const string CC1_FIELD = "CC1";

		public const string CC2_FIELD = "CC2";

		public const string CC3_FIELD = "CC3";

		public const string CC4_FIELD = "CC4";

		public const string BODY1_FIELD = "Body1";

		public const string BODY2_FIELD = "Body2";

		public const string BODY3_FIELD = "Body3";

		public const string BODY4_FIELD = "Body4";

		public const string EMAILUSESSL_FIELD = "EmailUseSSL";

		public const string SMSUSERNAME_FIELD = "SMSUserName";

		public const string SMSPASSWORD_FIELD = "SMSPassword";

		public const string SMSMOBILENO_FIELD = "SMSMobileNo";

		public const string TAXENTITYTYPES_FIELD = "TaxEntityTypes";

		public const string DEFAULTTAXOPTION_FIELD = "DefaultTaxOption";

		public const string DEFAULTTAXGROUPID_FIELD = "DefaultTaxGroupID";

		public const string LOTNOIDENTITY_FIELD = "LotNoIdentity";

		public const string REFERENCE2_FIELD = "Reference2";

		public DataTable CompanyInformationTable => base.Tables["Company"];

		public DataTable CompanyAddressTable => base.Tables["Company_Address"];

		public DataTable EmailConfigTable => base.Tables["Email_Config"];

		public CompanyInformationData()
		{
			BuildDataTables();
		}

		public CompanyInformationData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Company");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SetupID", typeof(byte));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			columns.Add("CompanyID", typeof(string)).AllowDBNull = false;
			columns.Add("CompanyName", typeof(string)).AllowDBNull = false;
			columns.Add("BaseCurrencyID", typeof(string));
			columns.Add("FiscalFirstMonth", typeof(byte)).DefaultValue = 1;
			columns.Add("LastBackupDate", typeof(DateTime));
			columns.Add("Notes", typeof(string));
			columns.Add("Logo", typeof(byte[]));
			columns.Add("FileSavingPath", typeof(string));
			columns.Add("TemplatePathLocation", typeof(byte));
			columns.Add("TemplatePathFolder", typeof(string));
			columns.Add("TemplatePathServer", typeof(string));
			columns.Add("IsDNInventory", typeof(bool));
			columns.Add("ItemPrice1Name", typeof(string));
			columns.Add("ItemPrice2Name", typeof(string));
			columns.Add("ItemPrice3Name", typeof(string));
			columns.Add("CurDecimalPoint", typeof(byte));
			columns.Add("PDCReceivedAccountID", typeof(string));
			columns.Add("PDCIssuedAccountID", typeof(string));
			columns.Add("UseLogo", typeof(bool));
			columns.Add("IsTax", typeof(bool)).DefaultValue = 0;
			columns.Add("TaxPercent", typeof(decimal));
			columns.Add("DiscountWriteoffPercent", typeof(decimal));
			columns.Add("AccountUD1", typeof(string));
			columns.Add("AccountUD2", typeof(string));
			columns.Add("AccountUD3", typeof(string));
			columns.Add("AccountUD4", typeof(string));
			columns.Add("CustomerUD1", typeof(string));
			columns.Add("CustomerUD2", typeof(string));
			columns.Add("CustomerUD3", typeof(string));
			columns.Add("CustomerUD4", typeof(string));
			columns.Add("VendorUD1", typeof(string));
			columns.Add("VendorUD2", typeof(string));
			columns.Add("VendorUD3", typeof(string));
			columns.Add("VendorUD4", typeof(string));
			columns.Add("EMPLOYEEUD1", typeof(string));
			columns.Add("EMPLOYEEUD2", typeof(string));
			columns.Add("EMPLOYEEUD3", typeof(string));
			columns.Add("EMPLOYEEUD4", typeof(string));
			columns.Add("InventoryUD1", typeof(string));
			columns.Add("InventoryUD2", typeof(string));
			columns.Add("InventoryUD3", typeof(string));
			columns.Add("InventoryUD4", typeof(string));
			columns.Add("MinPriceSaleAction", typeof(byte));
			columns.Add("MinPriceSalePass", typeof(string));
			columns.Add("OverCLAction", typeof(byte));
			columns.Add("OverCLPass", typeof(string));
			columns.Add("NegativeQuantityAction", typeof(byte));
			columns.Add("NegativeQuantityPass", typeof(string));
			columns.Add("PricelessCostAction", typeof(byte));
			columns.Add("RemoveAllocationAction", typeof(byte));
			columns.Add("PricelessCostPass", typeof(string));
			columns.Add("IncludePDC", typeof(bool));
			columns.Add("AgingByDate", typeof(bool));
			columns.Add("LocalPurchaseFlow", typeof(byte));
			columns.Add("ImportPurchaseFlow", typeof(byte));
			columns.Add("SalesFlow", typeof(byte));
			columns.Add("UseMultiCurrency", typeof(bool));
			columns.Add("UseJobCosting", typeof(bool));
			columns.Add("SMSUserName", typeof(string));
			columns.Add("SMSPassword", typeof(string));
			columns.Add("SMSMobileNo", typeof(string));
			columns.Add("LotNoIdentity", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("DaysInMonth", typeof(bool));
			columns.Add("ThirtyDays", typeof(bool));
			columns.Add("Annual", typeof(bool));
			columns.Add("HRAnalysisGroup", typeof(string));
			columns.Add("HRAnalysisPrefix", typeof(string));
			columns.Add("VehicleAnalysisGroup", typeof(string));
			columns.Add("VehicleAnalysisPrefix", typeof(string));
			columns.Add("LegalAnalysisGroup", typeof(string));
			columns.Add("LegalAnalysisPrefix", typeof(string));
			columns.Add("IsLocationCosting", typeof(bool));
			columns.Add("AutoResumptionDays", typeof(short));
			columns.Add("PatientAnalysisGroup", typeof(string));
			columns.Add("PatientAnalysisPrefix", typeof(string));
			columns.Add("TaxEntityTypes", typeof(string));
			columns.Add("DefaultTaxGroupID", typeof(string));
			columns.Add("DefaultTaxOption", typeof(string));
			columns.Add("MinQtyPackingAction", typeof(byte));
			base.Tables.Add(dataTable);
			CompanyAddressData companyAddressData = new CompanyAddressData();
			dataTable = new DataTable("Company_Address");
			foreach (DataColumn column in companyAddressData.CompanyAddressTable.Columns)
			{
				dataTable.Columns.Add(column.ColumnName, column.DataType);
			}
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Email_Config");
			DataColumnCollection columns2 = dataTable.Columns;
			columns2.Add("CompanyID", typeof(short)).AllowDBNull = false;
			columns2.Add("EmailID", typeof(byte)).AllowDBNull = false;
			columns2.Add("EmailAddress", typeof(string));
			columns2.Add("IncommingServer", typeof(string));
			columns2.Add("OutgoingServer", typeof(string));
			columns2.Add("EmailPass", typeof(string));
			columns2.Add("EmailSMTPPort", typeof(short));
			columns2.Add("UserName", typeof(string));
			columns2.Add("SenderName", typeof(string));
			columns2.Add("EmailUseSSL", typeof(bool));
			columns2.Add("CCSalesperson", typeof(bool));
			columns2.Add("CC1", typeof(string));
			columns2.Add("CC2", typeof(string));
			columns2.Add("CC3", typeof(string));
			columns2.Add("CC4", typeof(string));
			columns2.Add("Body1", typeof(string));
			columns2.Add("Body2", typeof(string));
			columns2.Add("Body3", typeof(string));
			columns2.Add("Body4", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
