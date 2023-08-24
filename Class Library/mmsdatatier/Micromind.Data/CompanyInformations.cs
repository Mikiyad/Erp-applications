using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using Micromind.Securities;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Micromind.Data
{
	public sealed class CompanyInformations : StoreObject
	{
		private const string SETUPID_PARM = "@SetupID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string COMPANYNAME_PARM = "@CompanyName";

		private const string BASECURRENCYID_PARM = "@BaseCurrencyID";

		private const string NOTES_PARM = "@Notes";

		private const string LOGO_PARM = "@Logo";

		private const string USELOGO_PARM = "@UseLogo";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string CURDECIMALPOINT_PARM = "@CurDecimalPoint";

		private const string ISTAX_PARM = "@IsTax";

		public const string TAXPERCENT_PARM = "@TaxPercent";

		private const string FISCALFIRSTMONTH_PARM = "@FiscalFirstMonth";

		private const string ISDNINVENTORY_PARM = "@IsDNInventory";

		private const string ITEMPRICE1NAME_PARM = "@ItemPrice1Name";

		private const string ITEMPRICE2NAME_PARM = "@ItemPrice2Name";

		private const string ITEMPRICE3NAME_PARM = "@ItemPrice3Name";

		private const string PDCRECEIVEDACCOUNTID_PARM = "@PDCReceivedAccountID";

		private const string PDCISSUEDACCOUNTID_PARM = "@PDCIssuedAccountID";

		private const string DISCOUNTWRITEOFFPERCENT_PARM = "@DiscountWriteoffPercent";

		private const string ACCOUNTUD1_PARM = "@AccountUD1";

		private const string ACCOUNTUD2_PARM = "@AccountUD2";

		private const string ACCOUNTUD3_PARM = "@AccountUD3";

		private const string ACCOUNTUD4_PARM = "@AccountUD4";

		private const string CUSTOMERUD1_PARM = "@CustomerUD1";

		private const string CUSTOMERUD2_PARM = "@CustomerUD2";

		private const string CUSTOMERUD3_PARM = "@CustomerUD3";

		private const string CUSTOMERUD4_PARM = "@CustomerUD4";

		private const string VENDORUD1_PARM = "@VendorUD1";

		private const string VENDORUD2_PARM = "@VendorUD2";

		private const string VENDORUD3_PARM = "@VendorUD3";

		private const string VENDORUD4_PARM = "@VendorUD4";

		private const string EMPLOYEEUD1_PARM = "@EMPLOYEEUD1";

		private const string EMPLOYEEUD2_PARM = "@EMPLOYEEUD2";

		private const string EMPLOYEEUD3_PARM = "@EMPLOYEEUD3";

		private const string EMPLOYEEUD4_PARM = "@EMPLOYEEUD4";

		private const string INVENTORYUD1_PARM = "@InventoryUD1";

		private const string INVENTORYUD2_PARM = "@InventoryUD2";

		private const string INVENTORYUD3_PARM = "@InventoryUD3";

		private const string INVENTORYUD4_PARM = "@InventoryUD4";

		private const string MINPRICESALEACTION_PARM = "@MinPriceSaleAction";

		private const string MINQTYPACKINGACTION_PARM = "@MinQtyPackingAction";

		private const string REMOVEALLOCATIONACTION_PARM = "@RemoveAllocationAction";

		private const string MINPRICESALEPASS_PARM = "@MinPriceSalePass";

		private const string OVERCLACTION_PARM = "@OverCLAction";

		private const string OVERCLPASS_PARM = "@OverCLPass";

		private const string NEGATIVEQUANTITYACTION_PARM = "@NegativeQuantityAction";

		private const string NEGATIVEQUANTITYPASS_PARM = "@NegativeQuantityPass";

		private const string PRICELESSCOSTACTION_PARM = "@PricelessCostAction";

		private const string PRICELESSCOSTPASS_PARM = "@PricelessCostPass";

		private const string INCLUDEPDC_PARM = "@IncludePDC";

		private const string AGINGBYDATE_PARM = "@AgingByDate";

		private const string USEMULTICURRENCY_PARM = "@UseMultiCurrency";

		private const string USEJOBCOSTING_PARM = "@UseJobCosting";

		private const string LOCALPURCHASEFLOW_PARM = "@LocalPurchaseFlow";

		private const string IMPORTPURCHASEFLOW_PARM = "@ImportPurchaseFlow";

		private const string SALESFLOW_PARM = "@SalesFlow";

		private const string FILESAVINGPATH_PARM = "@FileSavingPath";

		private const string TEMPLATEPATHLOCATION_PARM = "@TemplatePathLocation";

		private const string TEMPLATEPATHFOLDER_PARM = "@TemplatePathFolder";

		private const string TEMPLATEPATHSERVER_PARM = "@TemplatePathServer";

		private const string TOTALDAYHOURS_PARM = "@TotalWorkingDayHours";

		private const string TOTALMONTHHOURS_PARM = "@TotalWorkingMonthHours";

		private const string OFFDAY_PARM = "@OffDay";

		private const string LOADITEMDESCFROMPRICELIST_PARM = "@LoadItemDescFromPriceList";

		private const string DAYSINMONTH_PARM = "@DaysInMonth";

		private const string THIRTYDAYS_PARM = "@ThirtyDays";

		private const string ANNUAL_PARM = "@Annual";

		private const string AUTORESUMPTIONDAYS_PARM = "@AutoResumptionDays";

		private const string HRANALYSISGROUP_PARM = "@HRAnalysisGroup";

		private const string HRANALYSISPREFIX_PARM = "@HRAnalysisPrefix";

		private const string VEHICLEANALYSISGROUP_PARM = "@VehicleAnalysisGroup";

		private const string VEHICLEANALYSISPREFIX_PARM = "@VehicleAnalysisPrefix";

		private const string LEGALANALYSISGROUP_PARM = "@LegalAnalysisGroup";

		private const string LEGALANALYSISPREFIX_PARM = "@LegalAnalysisPrefix";

		private const string PATIENTANALYSISGROUP_PARM = "@PatientAnalysisGroup";

		private const string PATIENTANALYSISPREFIX_PARM = "@PatientAnalysisPrefix";

		private const string EMAILID_PARM = "@EmailID";

		private const string EMAILADDRESS_PARM = "@EmailAddress";

		private const string EMAILOUTGOINGSERVER_PARM = "@OutgoingServer";

		private const string EMAILINCOMMINGSERVER_PARM = "@IncommingServer";

		private const string EMAILPASS_PARM = "@EmailPass";

		private const string EMAILUSERNAME_PARM = "@UserName";

		private const string EMAILSENDERNAME_PARM = "@SenderName";

		private const string EMAILSMTPPORT_PARM = "@EmailSMTPPort";

		private const string EMAILUSESSL_PARM = "@EmailUseSSL";

		private const string CCSALESPERSON_PARM = "@CCSalesperson";

		private const string CC1_PARM = "@CC1";

		private const string CC2_PARM = "@CC2";

		private const string CC3_PARM = "@CC3";

		private const string CC4_PARM = "@CC4";

		private const string BODY1_PARM = "@Body1";

		private const string BODY2_PARM = "@Body2";

		private const string BODY3_PARM = "@Body3";

		private const string BODY4_PARM = "@Body4";

		private const string SMSUSERNAME_PARM = "@SMSUserName";

		private const string SMSPASSWORD_PARM = "@SMSPassword";

		private const string SMSMOBILENO_PARM = "@SMSMobileNo";

		private const string LOTNOIDENTITY_PARM = "@LotNoIdentity";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string TAXENTITYTYPES_PARM = "@TaxEntityTypes";

		private const string DEFAULTTAXOPTION_PARM = "@DefaultTaxOption";

		private const string DEFAULTTAXGROUP_PARM = "@DefaultTaxGroupID";

		public static bool isLocationCostingLoaded;

		public byte CurrencyDecimalPoints
		{
			get
			{
				byte b = 2;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Company", "CurDecimalPoint", "CompanyID", 1, base.DBConfig.CurrentTransaction);
				if (!fieldValue.IsNullOrEmpty())
				{
					return byte.Parse(fieldValue.ToString());
				}
				return 2;
			}
		}

		public CompanyInformations(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateEmailConfigText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Email_Config", new FieldValue("CompanyID", "@CompanyID", isUpdateConditionField: true), new FieldValue("EmailID", "@EmailID"), new FieldValue("EmailAddress", "@EmailAddress"), new FieldValue("UserName", "@UserName"), new FieldValue("OutgoingServer", "@OutgoingServer"), new FieldValue("IncommingServer", "@IncommingServer"), new FieldValue("EmailPass", "@EmailPass"), new FieldValue("EmailSMTPPort", "@EmailSMTPPort"), new FieldValue("EmailUseSSL", "@EmailUseSSL"), new FieldValue("CCSalesperson", "@CCSalesperson"), new FieldValue("CC1", "@CC1"), new FieldValue("CC2", "@CC2"), new FieldValue("CC3", "@CC3"), new FieldValue("CC4", "@CC4"), new FieldValue("Body1", "@Body1"), new FieldValue("Body2", "@Body2"), new FieldValue("Body3", "@Body3"), new FieldValue("Body4", "@Body4"), new FieldValue("SenderName", "@SenderName"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Company", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateEmailConfigCommand(bool isUpdate)
		{
			insertCommand = new SqlCommand(GetInsertUpdateEmailConfigText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@EmailID", SqlDbType.TinyInt);
			parameters.Add("@EmailAddress", SqlDbType.NVarChar);
			parameters.Add("@UserName", SqlDbType.NVarChar);
			parameters.Add("@OutgoingServer", SqlDbType.NVarChar);
			parameters.Add("@IncommingServer", SqlDbType.NVarChar);
			parameters.Add("@EmailPass", SqlDbType.NVarChar);
			parameters.Add("@SenderName", SqlDbType.NVarChar);
			parameters.Add("@EmailSMTPPort", SqlDbType.Int);
			parameters.Add("@EmailUseSSL", SqlDbType.Bit);
			parameters.Add("@CCSalesperson", SqlDbType.Bit);
			parameters.Add("@CC1", SqlDbType.NVarChar);
			parameters.Add("@CC2", SqlDbType.NVarChar);
			parameters.Add("@CC3", SqlDbType.NVarChar);
			parameters.Add("@CC4", SqlDbType.NVarChar);
			parameters.Add("@Body1", SqlDbType.NText);
			parameters.Add("@Body2", SqlDbType.NText);
			parameters.Add("@Body3", SqlDbType.NText);
			parameters.Add("@Body4", SqlDbType.NText);
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@EmailID"].SourceColumn = "EmailID";
			parameters["@EmailAddress"].SourceColumn = "EmailAddress";
			parameters["@UserName"].SourceColumn = "UserName";
			parameters["@OutgoingServer"].SourceColumn = "OutgoingServer";
			parameters["@IncommingServer"].SourceColumn = "IncommingServer";
			parameters["@EmailPass"].SourceColumn = "EmailPass";
			parameters["@SenderName"].SourceColumn = "SenderName";
			parameters["@EmailSMTPPort"].SourceColumn = "EmailSMTPPort";
			parameters["@EmailUseSSL"].SourceColumn = "EmailUseSSL";
			parameters["@CCSalesperson"].SourceColumn = "CCSalesperson";
			parameters["@CC1"].SourceColumn = "CC1";
			parameters["@CC2"].SourceColumn = "CC2";
			parameters["@CC3"].SourceColumn = "CC3";
			parameters["@CC4"].SourceColumn = "CC4";
			parameters["@Body1"].SourceColumn = "Body1";
			parameters["@Body2"].SourceColumn = "Body2";
			parameters["@Body3"].SourceColumn = "Body3";
			parameters["@Body4"].SourceColumn = "Body4";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Company", new FieldValue("CompanyID", "@CompanyID", isUpdateConditionField: true), new FieldValue("CompanyName", "@CompanyName"), new FieldValue("BaseCurrencyID", "@BaseCurrencyID"), new FieldValue("CurDecimalPoint", "@CurDecimalPoint"), new FieldValue("Notes", "@Notes"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Company", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				updateCommand = new SqlCommand(GetInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				insertCommand = new SqlCommand(GetInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@CompanyID", SqlDbType.NVarChar);
			parameters.Add("@CompanyName", SqlDbType.NVarChar);
			parameters.Add("@BaseCurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurDecimalPoint", SqlDbType.TinyInt);
			parameters.Add("@Notes", SqlDbType.NText);
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CompanyName"].SourceColumn = "CompanyName";
			parameters["@BaseCurrencyID"].SourceColumn = "BaseCurrencyID";
			parameters["@CurDecimalPoint"].SourceColumn = "CurDecimalPoint";
			parameters["@Notes"].SourceColumn = "Notes";
			if (isUpdate)
			{
				parameters.Add("@DateUpdated", SqlDbType.DateTime);
				parameters["@DateUpdated"].SourceColumn = "DateUpdated";
			}
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetCompanyOptionsInsertUpdateText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Company", new FieldValue("CompanyID", "@CompanyID", isUpdateConditionField: true), new FieldValue("IsTax", "@IsTax"), new FieldValue("TaxPercent", "@TaxPercent"), new FieldValue("FiscalFirstMonth", "@FiscalFirstMonth"), new FieldValue("IsDNInventory", "@IsDNInventory"), new FieldValue("ItemPrice1Name", "@ItemPrice1Name"), new FieldValue("ItemPrice2Name", "@ItemPrice2Name"), new FieldValue("ItemPrice3Name", "@ItemPrice3Name"), new FieldValue("PDCReceivedAccountID", "@PDCReceivedAccountID"), new FieldValue("PDCIssuedAccountID", "@PDCIssuedAccountID"), new FieldValue("AccountUD1", "@AccountUD1"), new FieldValue("AccountUD2", "@AccountUD2"), new FieldValue("AccountUD3", "@AccountUD3"), new FieldValue("AccountUD4", "@AccountUD4"), new FieldValue("CustomerUD1", "@CustomerUD1"), new FieldValue("CustomerUD2", "@CustomerUD2"), new FieldValue("CustomerUD3", "@CustomerUD3"), new FieldValue("CustomerUD4", "@CustomerUD4"), new FieldValue("VendorUD1", "@VendorUD1"), new FieldValue("VendorUD2", "@VendorUD2"), new FieldValue("VendorUD3", "@VendorUD3"), new FieldValue("VendorUD4", "@VendorUD4"), new FieldValue("EMPLOYEEUD1", "@EMPLOYEEUD1"), new FieldValue("EMPLOYEEUD2", "@EMPLOYEEUD2"), new FieldValue("EMPLOYEEUD3", "@EMPLOYEEUD3"), new FieldValue("EMPLOYEEUD4", "@EMPLOYEEUD4"), new FieldValue("InventoryUD1", "@InventoryUD1"), new FieldValue("InventoryUD2", "@InventoryUD2"), new FieldValue("InventoryUD3", "@InventoryUD3"), new FieldValue("InventoryUD4", "@InventoryUD4"), new FieldValue("MinPriceSaleAction", "@MinPriceSaleAction"), new FieldValue("RemoveAllocationAction", "@RemoveAllocationAction"), new FieldValue("MinPriceSalePass", "@MinPriceSalePass"), new FieldValue("OverCLAction", "@OverCLAction"), new FieldValue("OverCLPass", "@OverCLPass"), new FieldValue("NegativeQuantityAction", "@NegativeQuantityAction"), new FieldValue("NegativeQuantityPass", "@NegativeQuantityPass"), new FieldValue("PricelessCostAction", "@PricelessCostAction"), new FieldValue("PricelessCostPass", "@PricelessCostPass"), new FieldValue("IncludePDC", "@IncludePDC"), new FieldValue("AgingByDate", "@AgingByDate"), new FieldValue("DiscountWriteoffPercent", "@DiscountWriteoffPercent"), new FieldValue("LocalPurchaseFlow", "@LocalPurchaseFlow"), new FieldValue("ImportPurchaseFlow", "@ImportPurchaseFlow"), new FieldValue("SalesFlow", "@SalesFlow"), new FieldValue("UseMultiCurrency", "@UseMultiCurrency"), new FieldValue("UseJobCosting", "@UseJobCosting"), new FieldValue("TemplatePathLocation", "@TemplatePathLocation"), new FieldValue("TemplatePathFolder", "@TemplatePathFolder"), new FieldValue("TemplatePathServer", "@TemplatePathServer"), new FieldValue("FileSavingPath", "@FileSavingPath"), new FieldValue("SMSUserName", "@SMSUserName"), new FieldValue("SMSPassword", "@SMSPassword"), new FieldValue("SMSMobileNo", "@SMSMobileNo"), new FieldValue("LotNoIdentity", "@LotNoIdentity"), new FieldValue("Reference2", "@Reference2"), new FieldValue("DaysInMonth", "@DaysInMonth"), new FieldValue("ThirtyDays", "@ThirtyDays"), new FieldValue("Annual", "@Annual"), new FieldValue("HRAnalysisGroup", "@HRAnalysisGroup"), new FieldValue("HRAnalysisPrefix", "@HRAnalysisPrefix"), new FieldValue("VehicleAnalysisGroup", "@VehicleAnalysisGroup"), new FieldValue("VehicleAnalysisPrefix", "@VehicleAnalysisPrefix"), new FieldValue("PatientAnalysisGroup", "@PatientAnalysisGroup"), new FieldValue("PatientAnalysisPrefix", "@PatientAnalysisPrefix"), new FieldValue("LegalAnalysisGroup", "@LegalAnalysisGroup"), new FieldValue("LegalAnalysisPrefix", "@LegalAnalysisPrefix"), new FieldValue("AutoResumptionDays", "@AutoResumptionDays"), new FieldValue("TaxEntityTypes", "@TaxEntityTypes"), new FieldValue("DefaultTaxOption", "@DefaultTaxOption"), new FieldValue("DefaultTaxGroupID", "@DefaultTaxGroupID"), new FieldValue("MinQtyPackingAction", "@MinQtyPackingAction"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetCompanyOptionsInsertUpdateCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					return updateCommand;
				}
				updateCommand = new SqlCommand(GetCompanyOptionsInsertUpdateText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					return insertCommand;
				}
				insertCommand = new SqlCommand(GetCompanyOptionsInsertUpdateText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@CompanyID", SqlDbType.NVarChar);
			parameters.Add("@IsTax", SqlDbType.Bit);
			parameters.Add("@TaxPercent", SqlDbType.Decimal);
			parameters.Add("@FiscalFirstMonth", SqlDbType.TinyInt);
			parameters.Add("@IsDNInventory", SqlDbType.Bit);
			parameters.Add("@ItemPrice1Name", SqlDbType.NVarChar);
			parameters.Add("@ItemPrice2Name", SqlDbType.NVarChar);
			parameters.Add("@ItemPrice3Name", SqlDbType.NVarChar);
			parameters.Add("@PDCReceivedAccountID", SqlDbType.NVarChar);
			parameters.Add("@PDCIssuedAccountID", SqlDbType.NVarChar);
			parameters.Add("@DiscountWriteoffPercent", SqlDbType.Decimal);
			parameters.Add("@AccountUD1", SqlDbType.NVarChar);
			parameters.Add("@AccountUD2", SqlDbType.NVarChar);
			parameters.Add("@AccountUD3", SqlDbType.NVarChar);
			parameters.Add("@AccountUD4", SqlDbType.NVarChar);
			parameters.Add("@CustomerUD1", SqlDbType.NVarChar);
			parameters.Add("@CustomerUD2", SqlDbType.NVarChar);
			parameters.Add("@CustomerUD3", SqlDbType.NVarChar);
			parameters.Add("@CustomerUD4", SqlDbType.NVarChar);
			parameters.Add("@VendorUD1", SqlDbType.NVarChar);
			parameters.Add("@VendorUD2", SqlDbType.NVarChar);
			parameters.Add("@VendorUD3", SqlDbType.NVarChar);
			parameters.Add("@VendorUD4", SqlDbType.NVarChar);
			parameters.Add("@EMPLOYEEUD1", SqlDbType.NVarChar);
			parameters.Add("@EMPLOYEEUD2", SqlDbType.NVarChar);
			parameters.Add("@EMPLOYEEUD3", SqlDbType.NVarChar);
			parameters.Add("@EMPLOYEEUD4", SqlDbType.NVarChar);
			parameters.Add("@InventoryUD1", SqlDbType.NVarChar);
			parameters.Add("@InventoryUD2", SqlDbType.NVarChar);
			parameters.Add("@InventoryUD3", SqlDbType.NVarChar);
			parameters.Add("@InventoryUD4", SqlDbType.NVarChar);
			parameters.Add("@MinPriceSaleAction", SqlDbType.TinyInt);
			parameters.Add("@RemoveAllocationAction", SqlDbType.TinyInt);
			parameters.Add("@MinPriceSalePass", SqlDbType.NVarChar);
			parameters.Add("@OverCLAction", SqlDbType.TinyInt);
			parameters.Add("@OverCLPass", SqlDbType.NVarChar);
			parameters.Add("@NegativeQuantityAction", SqlDbType.TinyInt);
			parameters.Add("@NegativeQuantityPass", SqlDbType.NVarChar);
			parameters.Add("@PricelessCostAction", SqlDbType.TinyInt);
			parameters.Add("@PricelessCostPass", SqlDbType.NVarChar);
			parameters.Add("@IncludePDC", SqlDbType.Bit);
			parameters.Add("@UseMultiCurrency", SqlDbType.Bit);
			parameters.Add("@UseJobCosting", SqlDbType.Bit);
			parameters.Add("@AgingByDate", SqlDbType.Bit);
			parameters.Add("@LocalPurchaseFlow", SqlDbType.TinyInt);
			parameters.Add("@ImportPurchaseFlow", SqlDbType.TinyInt);
			parameters.Add("@SalesFlow", SqlDbType.TinyInt);
			parameters.Add("@FileSavingPath", SqlDbType.NVarChar);
			parameters.Add("@TemplatePathLocation", SqlDbType.TinyInt);
			parameters.Add("@TemplatePathFolder", SqlDbType.NVarChar);
			parameters.Add("@TemplatePathServer", SqlDbType.NVarChar);
			parameters.Add("@SMSUserName", SqlDbType.NVarChar);
			parameters.Add("@SMSPassword", SqlDbType.NVarChar);
			parameters.Add("@SMSMobileNo", SqlDbType.NVarChar);
			parameters.Add("@LotNoIdentity", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@DaysInMonth", SqlDbType.Bit);
			parameters.Add("@ThirtyDays", SqlDbType.Bit);
			parameters.Add("@Annual", SqlDbType.Bit);
			parameters.Add("@HRAnalysisGroup", SqlDbType.NVarChar);
			parameters.Add("@HRAnalysisPrefix", SqlDbType.NVarChar);
			parameters.Add("@PatientAnalysisGroup", SqlDbType.NVarChar);
			parameters.Add("@PatientAnalysisPrefix", SqlDbType.NVarChar);
			parameters.Add("@VehicleAnalysisGroup", SqlDbType.NVarChar);
			parameters.Add("@VehicleAnalysisPrefix", SqlDbType.NVarChar);
			parameters.Add("@LegalAnalysisGroup", SqlDbType.NVarChar);
			parameters.Add("@LegalAnalysisPrefix", SqlDbType.NVarChar);
			parameters.Add("@AutoResumptionDays", SqlDbType.Int);
			parameters.Add("@TaxEntityTypes", SqlDbType.NVarChar);
			parameters.Add("@DefaultTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@DefaultTaxOption", SqlDbType.TinyInt);
			parameters.Add("@MinQtyPackingAction", SqlDbType.TinyInt);
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@IsTax"].SourceColumn = "IsTax";
			parameters["@TaxPercent"].SourceColumn = "TaxPercent";
			parameters["@FiscalFirstMonth"].SourceColumn = "FiscalFirstMonth";
			parameters["@IsDNInventory"].SourceColumn = "IsDNInventory";
			parameters["@ItemPrice1Name"].SourceColumn = "ItemPrice1Name";
			parameters["@ItemPrice2Name"].SourceColumn = "ItemPrice2Name";
			parameters["@ItemPrice3Name"].SourceColumn = "ItemPrice3Name";
			parameters["@PDCReceivedAccountID"].SourceColumn = "PDCReceivedAccountID";
			parameters["@PDCIssuedAccountID"].SourceColumn = "PDCIssuedAccountID";
			parameters["@DiscountWriteoffPercent"].SourceColumn = "DiscountWriteoffPercent";
			parameters["@AccountUD1"].SourceColumn = "AccountUD1";
			parameters["@AccountUD2"].SourceColumn = "AccountUD2";
			parameters["@AccountUD3"].SourceColumn = "AccountUD3";
			parameters["@AccountUD4"].SourceColumn = "AccountUD4";
			parameters["@CustomerUD1"].SourceColumn = "CustomerUD1";
			parameters["@CustomerUD2"].SourceColumn = "CustomerUD2";
			parameters["@CustomerUD3"].SourceColumn = "CustomerUD3";
			parameters["@CustomerUD4"].SourceColumn = "CustomerUD4";
			parameters["@VendorUD1"].SourceColumn = "VendorUD1";
			parameters["@VendorUD2"].SourceColumn = "VendorUD2";
			parameters["@VendorUD3"].SourceColumn = "VendorUD3";
			parameters["@VendorUD4"].SourceColumn = "VendorUD4";
			parameters["@EMPLOYEEUD1"].SourceColumn = "EMPLOYEEUD1";
			parameters["@EMPLOYEEUD2"].SourceColumn = "EMPLOYEEUD2";
			parameters["@EMPLOYEEUD3"].SourceColumn = "EMPLOYEEUD3";
			parameters["@EMPLOYEEUD4"].SourceColumn = "EMPLOYEEUD4";
			parameters["@InventoryUD1"].SourceColumn = "InventoryUD1";
			parameters["@InventoryUD2"].SourceColumn = "InventoryUD2";
			parameters["@InventoryUD3"].SourceColumn = "InventoryUD3";
			parameters["@InventoryUD4"].SourceColumn = "InventoryUD4";
			parameters["@MinPriceSaleAction"].SourceColumn = "MinPriceSaleAction";
			parameters["@RemoveAllocationAction"].SourceColumn = "RemoveAllocationAction";
			parameters["@MinPriceSalePass"].SourceColumn = "MinPriceSalePass";
			parameters["@OverCLAction"].SourceColumn = "OverCLAction";
			parameters["@OverCLPass"].SourceColumn = "OverCLPass";
			parameters["@NegativeQuantityAction"].SourceColumn = "NegativeQuantityAction";
			parameters["@NegativeQuantityPass"].SourceColumn = "NegativeQuantityPass";
			parameters["@PricelessCostAction"].SourceColumn = "PricelessCostAction";
			parameters["@PricelessCostPass"].SourceColumn = "PricelessCostPass";
			parameters["@IncludePDC"].SourceColumn = "IncludePDC";
			parameters["@UseMultiCurrency"].SourceColumn = "UseMultiCurrency";
			parameters["@UseJobCosting"].SourceColumn = "UseJobCosting";
			parameters["@AgingByDate"].SourceColumn = "AgingByDate";
			parameters["@LocalPurchaseFlow"].SourceColumn = "LocalPurchaseFlow";
			parameters["@ImportPurchaseFlow"].SourceColumn = "ImportPurchaseFlow";
			parameters["@SalesFlow"].SourceColumn = "SalesFlow";
			parameters["@MinQtyPackingAction"].SourceColumn = "MinQtyPackingAction";
			parameters["@FileSavingPath"].SourceColumn = "FileSavingPath";
			parameters["@TemplatePathLocation"].SourceColumn = "TemplatePathLocation";
			parameters["@TemplatePathFolder"].SourceColumn = "TemplatePathFolder";
			parameters["@TemplatePathServer"].SourceColumn = "TemplatePathServer";
			parameters["@SMSUserName"].SourceColumn = "SMSUserName";
			parameters["@SMSPassword"].SourceColumn = "SMSPassword";
			parameters["@SMSMobileNo"].SourceColumn = "SMSMobileNo";
			parameters["@LotNoIdentity"].SourceColumn = "LotNoIdentity";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@DaysInMonth"].SourceColumn = "DaysInMonth";
			parameters["@ThirtyDays"].SourceColumn = "ThirtyDays";
			parameters["@Annual"].SourceColumn = "Annual";
			parameters["@HRAnalysisGroup"].SourceColumn = "HRAnalysisGroup";
			parameters["@HRAnalysisPrefix"].SourceColumn = "HRAnalysisPrefix";
			parameters["@PatientAnalysisGroup"].SourceColumn = "PatientAnalysisGroup";
			parameters["@PatientAnalysisPrefix"].SourceColumn = "PatientAnalysisPrefix";
			parameters["@VehicleAnalysisGroup"].SourceColumn = "VehicleAnalysisGroup";
			parameters["@VehicleAnalysisPrefix"].SourceColumn = "VehicleAnalysisPrefix";
			parameters["@LegalAnalysisGroup"].SourceColumn = "LegalAnalysisGroup";
			parameters["@LegalAnalysisPrefix"].SourceColumn = "LegalAnalysisPrefix";
			parameters["@AutoResumptionDays"].SourceColumn = "AutoResumptionDays";
			parameters["@TaxEntityTypes"].SourceColumn = "TaxEntityTypes";
			parameters["@DefaultTaxGroupID"].SourceColumn = "DefaultTaxGroupID";
			parameters["@DefaultTaxOption"].SourceColumn = "DefaultTaxOption";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		public bool CanChangeBaseCurrency()
		{
			string exp = "SELECT COUNT(*) FROM Journal WHERE CurrencyID IS NOT NULL";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				return int.Parse(obj.ToString()) == 0;
			}
			return true;
		}

		public bool UpdateCompanyInformation(CompanyInformationData companyInformationData)
		{
			bool flag = true;
			SqlCommand insertUpdateCommand = GetInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (insertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				flag = Update(companyInformationData, "Company", insertUpdateCommand);
				if (flag)
				{
					insertUpdateCommand = new CompanyAddresses(base.DBConfig).GetInsertUpdateCommand(isUpdate: true);
					insertUpdateCommand.Transaction = sqlTransaction;
					flag &= Update(companyInformationData, "Company_Address", insertUpdateCommand);
				}
				if (!flag)
				{
					return flag;
				}
				object obj = companyInformationData.CompanyInformationTable.Rows[0]["CompanyID"];
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				string text = companyInformationData.Tables[0].Rows[0]["BaseCurrencyID"].ToString();
				if (baseCurrencyID != text)
				{
					string exp = "UPDATE CURRENCY SET ISBASE = 'TRUE' WHERE CURRENCYID='" + text + "'";
					flag &= (ExecuteNonQuery(exp) > 0);
				}
				UpdateTableRowByID("Company", "CompanyID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				AddActivityLog("Company", "Company Information", ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Company", "CompanyID", obj, sqlTransaction, isInsert: false);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool UpdateCompanyOptions(CompanyInformationData companyInformationData)
		{
			bool flag = true;
			SqlCommand companyOptionsInsertUpdateCommand = GetCompanyOptionsInsertUpdateCommand(isUpdate: true);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = (companyOptionsInsertUpdateCommand.Transaction = base.DBConfig.StartNewTransaction());
				ConfigHelper configHelper = new ConfigHelper(base.CryptorID);
				if (companyInformationData == null || companyInformationData.CompanyInformationTable.Rows.Count == 0)
				{
					return true;
				}
				companyInformationData.CompanyInformationTable.Rows[0]["SMSPassword"] = configHelper.Cryptor.Encrypt(companyInformationData.CompanyInformationTable.Rows[0]["SMSPassword"].ToString());
				flag = Update(companyInformationData, "Company", companyOptionsInsertUpdateCommand);
				if (!flag)
				{
					return flag;
				}
				object obj = companyInformationData.CompanyInformationTable.Rows[0]["CompanyID"];
				UpdateTableRowByID("Company", "CompanyID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				AddActivityLog("Company", "Company Information", ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Company", "CompanyID", obj, sqlTransaction, isInsert: false);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool UpdateEmailConfig(CompanyInformationData companyInformationData)
		{
			bool flag = true;
			SqlCommand insertUpdateEmailConfigCommand = GetInsertUpdateEmailConfigCommand(isUpdate: false);
			SqlTransaction sqlTransaction = null;
			try
			{
				sqlTransaction = base.DBConfig.StartNewTransaction();
				object obj = 1;
				string exp = "DELETE FROM Email_Config WHERE CompanyID = " + obj;
				ExecuteNonQuery(exp, sqlTransaction);
				ConfigHelper configHelper = new ConfigHelper(base.CryptorID);
				foreach (DataRow row in companyInformationData.EmailConfigTable.Rows)
				{
					row["EmailPass"] = configHelper.Cryptor.Encrypt(row["EmailPass"].ToString());
				}
				insertUpdateEmailConfigCommand.Transaction = sqlTransaction;
				flag = Insert(companyInformationData, "Email_Config", insertUpdateEmailConfigCommand);
				if (!flag)
				{
					return flag;
				}
				UpdateTableRowByID("Email_Config", "CompanyID", "DateUpdated", obj, DateTime.Now, sqlTransaction);
				AddActivityLog("Email Config", obj.ToString(), ActivityTypes.Update, sqlTransaction);
				UpdateTableRowInsertUpdateInfo("Email_Config", "CompanyID", obj, sqlTransaction, isInsert: false);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public string GetCompanyName()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddColumn("Company", "CompanyName");
			sqlBuilder.UseDistinct = false;
			string result = "";
			try
			{
				object obj = ExecuteScalar(sqlBuilder.GetSelectExpression());
				if (obj != null)
				{
					return obj.ToString();
				}
				return result;
			}
			catch
			{
				throw;
			}
		}

		public CompanyInformationData GetCompanyInformation()
		{
			if (base.DBConfig.DatabaseName.ToLower() == "master")
			{
				return null;
			}
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddTable("Company");
			CompanyInformationData companyInformationData = new CompanyInformationData();
			sqlBuilder.UseDistinct = false;
			FillDataSet(companyInformationData, "Company", sqlBuilder);
			if (companyInformationData == null || companyInformationData.Tables.Count == 0 || companyInformationData.Tables[0].Rows.Count == 0)
			{
				return companyInformationData;
			}
			string text = "";
			text = companyInformationData.Tables["Company"].Rows[0]["PrimaryAddressID"].ToString();
			if (text == "")
			{
				text = "PRIMARY";
			}
			string textCommand = "SELECT * FROM Company_Address\r\n                            WHERE AddressID='" + text + "'";
			FillDataSet(companyInformationData, "Company_Address", textCommand);
			ConfigHelper configHelper = new ConfigHelper(base.CryptorID);
			companyInformationData.CompanyInformationTable.Rows[0]["SMSPassword"] = configHelper.Cryptor.Decrypt(companyInformationData.Tables["Company"].Rows[0]["SMSPassword"].ToString());
			return companyInformationData;
		}

		public CompanyInformationData GetEmailConfig(CompanyEmailConfigTypes type)
		{
			CompanyInformationData companyInformationData = new CompanyInformationData();
			string text = "SELECT * FROM Email_Config\r\n                            WHERE CompanyID = 1 ";
			if (type != 0)
			{
				text = text + " AND EmailID = " + (int)type;
			}
			FillDataSet(companyInformationData, "Email_Config", text);
			ConfigHelper configHelper = new ConfigHelper(base.CryptorID);
			foreach (DataRow row in companyInformationData.EmailConfigTable.Rows)
			{
				row["EmailPass"] = configHelper.Cryptor.Decrypt(row["EmailPass"].ToString());
			}
			return companyInformationData;
		}

		public byte GetFiscalStartMonth()
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddColumn("Company", "FiscalFirstMonth");
			sqlBuilder.UseDistinct = false;
			byte b = 1;
			try
			{
				object obj = ExecuteScalar(sqlBuilder.GetSelectExpression());
				if (obj != null)
				{
					b = byte.Parse(obj.ToString());
				}
				if (b >= 1)
				{
					return b;
				}
				if (b <= 12)
				{
					return b;
				}
				b = 1;
				return b;
			}
			catch
			{
				return b;
			}
		}

		public DateTime GetFiscalStartDate()
		{
			byte fiscalStartMonth = GetFiscalStartMonth();
			return new DateTime(DateTime.Today.Year, fiscalStartMonth, 1);
		}

		public bool UseLogo(int id)
		{
			bool result = false;
			try
			{
				result = bool.Parse(ExecuteSelectScalar("Company", "SetupID", id, "UseLogo").ToString());
				return result;
			}
			catch
			{
				return result;
			}
		}

		public bool UpdateClosingBookDate(DateTime closingDate, string closingPassword)
		{
			bool result = true;
			if (!base.DBConfig.IsCurrentUserAdministrator())
			{
				throw new ApplicationException("Only the administrator can close the book.");
			}
			try
			{
				SqlTransaction trans = base.DBConfig.StartNewTransaction();
				string commandText = "UPDATE Company SET ClosingDate='" + closingDate.ToString(StoreConfiguration.CurrentCulture) + "', ClosingPassword='" + closingPassword + "'";
				Update(commandText, trans);
				return result;
			}
			catch
			{
				result = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public DateTime GetClosingBookDate()
		{
			string exp = "SELECT TOP 1 ClosingDate FROM Company";
			object obj = ExecuteScalar(exp);
			if (obj != DBNull.Value && obj != null)
			{
				return DateTime.Parse(obj.ToString());
			}
			return DateTime.MinValue;
		}

		public string GetClosingBookPassword()
		{
			string exp = "SELECT TOP 1 ClosingPassword FROM Company";
			object obj = ExecuteScalar(exp);
			if (obj != DBNull.Value && obj != null)
			{
				return obj.ToString();
			}
			return "";
		}

		public bool IsTaxSystem()
		{
			try
			{
				string exp = "SELECT TOP 1 IsTax FROM Company";
				object obj = ExecuteScalar(exp);
				if (obj != DBNull.Value && obj != null)
				{
					return bool.Parse(obj.ToString());
				}
			}
			catch
			{
			}
			return false;
		}

		public DataSet GetCompanyPreferences()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT IsTax,IsDNInventory,MinPriceSaleAction,MinPriceSalePass,CurDecimalPoint,IncludePDC,\r\n                            OverCLAction,OverCLPass,NegativeQuantityAction,NegativeQuantityPass,PricelessCostAction,PricelessCostPass,UseMultiCurrency,AgingByDate,LocalPurchaseFlow,ImportPurchaseFlow,BaseCurrencyID,RemoveAllocationAction\r\n                           FROM Company WHERE CompanyID=1";
			FillDataSet(dataSet, "Company", textCommand);
			return dataSet;
		}

		public bool AddLogo(byte[] image)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Company SET Logo=@Logo WHERE CompanyID='1'");
				sqlCommand.Parameters.AddWithValue("@Logo", image);
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
				return result;
			}
			catch
			{
				result = false;
				return false;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool RemoveLogo()
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("Update Company SET Logo= Null WHERE CompanyID='1'");
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
				return result;
			}
			catch
			{
				result = false;
				return false;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public byte[] GetLogoThumbnailImage()
		{
			string exp = "SELECT Logo \r\n                           FROM Company WHERE CompanyID='1'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				return (byte[])obj;
			}
			return null;
		}

		public ApplicationUpdateConfig GetCurrentAxolonServerVersion()
		{
			string path = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\updateinfo.axo";
			ApplicationUpdateConfig applicationUpdateConfig = new ApplicationUpdateConfig();
			StreamReader streamReader = new StreamReader(path);
			applicationUpdateConfig.ProductName = streamReader.ReadLine();
			applicationUpdateConfig.ProductVersion = streamReader.ReadLine();
			applicationUpdateConfig.ClientUpdatePath = GetClientUpdatePath();
			return applicationUpdateConfig;
		}

		public string GetClientUpdatePath()
		{
			return new StreamReader(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)) + "\\clientupdatepath.axo").ReadLine();
		}

		public bool ClosePeriod(DateTime date, DateTime dateInventory, string remarks)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = null;
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				DataSet dataSet = new DataSet();
				DataTable dataTable = dataSet.Tables.Add("Period_Lock");
				dataTable.Columns.Add("CloseDate", typeof(DateTime));
				dataTable.Columns.Add("InventoryCloseDate", typeof(DateTime));
				dataTable.Columns.Add("Remarks", typeof(string));
				dataTable.Columns.Add("IsLocked", typeof(bool));
				dataTable.Rows.Add(date, dateInventory, remarks, true);
				SqlCommand sqlCommand = new SqlCommand();
				SqlBuilder sqlBuilder = new SqlBuilder();
				sqlBuilder.AddInsertUpdateParameters("Period_Lock", new FieldValue("CloseDate", "@CloseDate"), new FieldValue("InventoryCloseDate", "@InventoryCloseDate"), new FieldValue("IsLocked", "@IsLocked"), new FieldValue("Remarks", "@Remarks"));
				sqlCommand.Connection = base.DBConfig.Connection;
				sqlCommand.CommandText = sqlBuilder.GetInsertExpression();
				sqlCommand.CommandType = CommandType.Text;
				SqlParameterCollection parameters = sqlCommand.Parameters;
				parameters.Add("@CloseDate", SqlDbType.DateTime);
				parameters.Add("@InventoryCloseDate", SqlDbType.DateTime);
				parameters.Add("@IsLocked", SqlDbType.Bit);
				parameters.Add("@Remarks", SqlDbType.NVarChar);
				parameters["@CloseDate"].SourceColumn = "CloseDate";
				parameters["@InventoryCloseDate"].SourceColumn = "InventoryCloseDate";
				parameters["@IsLocked"].SourceColumn = "IsLocked";
				parameters["@Remarks"].SourceColumn = "Remarks";
				flag &= Insert(dataSet, "Period_Lock", sqlCommand, sqlTransaction);
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		internal DateTime GetLastClosingDate()
		{
			string exp = "SELECT TOP 1 d FROM \r\n                                (SELECT (CloseDate) D FROM Period_Lock WHERE ISNULL(IsLocked,'False')='True' \r\n                                UNION\r\n                                SELECT (EndDate) D FROM FiscalYear WHERE Status = 2) AS T ORDER BY D DESC ";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				return DateTime.Parse(obj.ToString());
			}
			return DateTime.MinValue;
		}

		public DataSet GetLastClosingPeriod()
		{
			string textCommand = "SELECT TOP 1 * FROM Period_Lock WHERE ISNULL(IsLocked,'False')='True' ORDER BY ISNULL(InventoryCloseDate,CloseDate) desc";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Period", textCommand);
			return dataSet;
		}

		public bool UnlockPeriod(int id)
		{
			bool flag = true;
			try
			{
				string commandText = "UPDATE Period_Lock SET IsLocked='False'\r\n                      WHERE PeriodID = " + id.ToString();
				return Delete(commandText, null);
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInstalledModules()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT * FROM Modules";
			FillDataSet(dataSet, "Modules", textCommand);
			return dataSet;
		}

		public bool AddModule(string moduleKey)
		{
			bool result = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("INSERT INTO Modules (ModuleKey) VALUES('" + moduleKey + "')");
				sqlCommand.Transaction = transaction;
				result = (ExecuteNonQuery(sqlCommand) > 0);
				return result;
			}
			catch
			{
				result = false;
				return false;
			}
			finally
			{
				base.DBConfig.EndTransaction(result);
			}
		}

		public bool DeleteModule(string moduleKey)
		{
			bool flag = true;
			try
			{
				SqlTransaction transaction = base.DBConfig.StartNewTransaction();
				SqlCommand sqlCommand = new SqlCommand("DELETE FROM Modules WHERE ModuleKey =  '" + moduleKey + "'");
				sqlCommand.Transaction = transaction;
				flag = (ExecuteNonQuery(sqlCommand) > 0);
				if (flag)
				{
					AddActivityLog("Company", moduleKey, ActivityTypes.Delete, null);
				}
				return flag;
			}
			catch
			{
				flag = false;
				return false;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		public bool ConfigureSchedulerAgent(string userID, string password, decimal intervalMinutes, decimal emailInterval, DateTime maintenanceTime, bool isActive)
		{
			try
			{
				string directoryName = Path.GetDirectoryName(Application.ExecutablePath);
				directoryName += "\\Agent";
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				string text = directoryName + "\\Agent_" + base.DBConfig.DatabaseName + ".xag";
				if (File.Exists(text))
				{
					File.Delete(text);
				}
				ConfigHelper configHelper = new ConfigHelper(base.CryptorID);
				XmlManager xmlManager = new XmlManager(text, "SchedulerAgent");
				xmlManager.Write("DBName", base.DBConfig.DatabaseName);
				xmlManager.Write("ServerName", base.DBConfig.ServerName);
				xmlManager.Write("UserName", configHelper.Cryptor.Encrypt(userID));
				xmlManager.Write("Pass", configHelper.Cryptor.Encrypt(password));
				xmlManager.Write("IsActive", isActive);
				xmlManager.Write("TaskInterval", intervalMinutes);
				xmlManager.Write("EmailInterval", emailInterval);
				xmlManager.Write("MaintenanceTime", maintenanceTime.Hour + ":" + maintenanceTime.Minute);
				return true;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetSchedulerAgentInfo()
		{
			try
			{
				DataSet dataSet = new DataSet();
				DataTable dataTable = dataSet.Tables.Add("Agent");
				dataTable.Columns.Add("DBName");
				dataTable.Columns.Add("ServerName");
				dataTable.Columns.Add("UserName");
				dataTable.Columns.Add("Password");
				dataTable.Columns.Add("IsActive", typeof(bool));
				dataTable.Columns.Add("TaskInterval", typeof(decimal));
				dataTable.Columns.Add("EmailInterval", typeof(decimal));
				dataTable.Columns.Add("MaintenanceTime", typeof(DateTime));
				string text = Path.GetDirectoryName(Application.ExecutablePath) + "\\Agent\\Agent_" + base.DBConfig.DatabaseName + ".xag";
				if (!File.Exists(text))
				{
					return null;
				}
				ConfigHelper configHelper = new ConfigHelper(base.CryptorID);
				XmlManager xmlManager = new XmlManager(text, "SchedulerAgent");
				DataRow dataRow = dataTable.NewRow();
				dataRow["DBName"] = xmlManager.GetValue("DBName", "");
				dataRow["ServerName"] = xmlManager.GetValue("ServerName", "");
				dataRow["UserName"] = configHelper.Cryptor.Decrypt(xmlManager.GetValue("UserName", "").ToString());
				dataRow["Password"] = configHelper.Cryptor.Decrypt(xmlManager.GetValue("Pass", "").ToString());
				dataRow["IsActive"] = xmlManager.GetValue("IsActive", bool.TrueString);
				dataRow["TaskInterval"] = xmlManager.GetValue("TaskInterval", 30);
				dataRow["EmailInterval"] = xmlManager.GetValue("EmailInterval", 30);
				dataRow["MaintenanceTime"] = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + xmlManager.GetValue("MaintenanceTime", "00:00"));
				dataTable.Rows.Add(dataRow);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool ExecuteScheduledJobs()
		{
			try
			{
				using (EventLog eventLog = new EventLog("Application"))
				{
					eventLog.Source = "Axolon";
					eventLog.WriteEntry("Task Scheduler Executed @" + DateTime.Now, EventLogEntryType.Information, 101, 1);
				}
				return true;
			}
			catch (Exception ex)
			{
				new EventLog("Application").WriteEntry("Task Scheduler execution exception: @" + ex.Message, EventLogEntryType.Information, 101, 1);
				throw ex;
			}
		}

		public bool ExecuteSystemMaintenanceJobs()
		{
			try
			{
				using (EventLog eventLog = new EventLog("Application"))
				{
					eventLog.Source = "Axolon";
					eventLog.WriteEntry("Task Scheduler Executed @" + DateTime.Now, EventLogEntryType.Information, 101, 1);
				}
				return true;
			}
			catch (Exception ex)
			{
				new EventLog("Application").WriteEntry("Task Scheduler execution exception: @" + ex.Message, EventLogEntryType.Information, 101, 1);
				throw ex;
			}
		}

		public bool GetIsLocationCosting(SqlTransaction sqlTransaction)
		{
			try
			{
				bool result = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Company", "IsLocationCosting", "CompanyID", 1, sqlTransaction);
				if (fieldValue != null)
				{
					bool.TryParse(fieldValue.ToString(), out result);
				}
				return result;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetFormMenuSubstitutes()
		{
			DataSet dataSet = new DataSet();
			string textCommand = "SELECT * FROM FormMenuSubstituteText ";
			FillDataSet(dataSet, "SubstituteText", textCommand);
			return dataSet;
		}

		public DateTime GetInitialFiscalYearDate()
		{
			string exp = " select  Top 1 StartDate from FiscalYear order by StartDate ";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				return DateTime.Parse(obj.ToString());
			}
			return DateTime.MinValue;
		}

		public DateTime GetLastFiscalYearDate()
		{
			string exp = " select top 1  EndDate from FiscalYear order by EndDate Desc ";
			object obj = ExecuteScalar(exp);
			if (obj != null)
			{
				return DateTime.Parse(obj.ToString());
			}
			return DateTime.MaxValue;
		}
	}
}
