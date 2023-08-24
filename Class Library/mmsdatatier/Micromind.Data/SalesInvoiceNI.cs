using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;

namespace Micromind.Data
{
	public sealed class SalesInvoiceNI : StoreObject
	{
		private const string SALESINVOICE_TABLE = "Sales_Invoice_NonInv";

		private const string SALESINVOICEDETAIL_TABLE = "Sales_Invoice_NonInv_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string CUSTOMERID_PARM = "@CustomerID";

		private const string SALESFLOW_PARM = "@SalesFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string SALESPERSONID_PARM = "@SalespersonID";

		private const string REPORTTO_PARM = "@ReportTo";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string ISEXPORT_PARM = "@IsExport";

		private const string REQUIREDDATE_PARM = "@RequiredDate";

		private const string SHIPPINGADDRESSID_PARM = "@ShippingAddressID";

		private const string PRICEINCLUDETAX_PARM = "@PriceIncludeTax";

		private const string BILLINGADDRESSID_PARM = "@BillingAddressID";

		private const string SHIPTOADDRESS_PARM = "@ShipToAddress";

		private const string CUSTOMERADDRESS_PARM = "@CustomerAddress";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string NOTE_PARM = "@Note";

		private const string ISVOID_PARM = "@IsVoid";

		private const string PONUMBER_PARM = "@PONumber";

		private const string DISCOUNT_PARM = "@Discount";

		private const string DISCOUNTFC_PARM = "@DiscountFC";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string ROUNDOFF_PARM = "@RoundOff";

		private const string TOTAL_PARM = "@Total";

		private const string TOTALFC_PARM = "@TotalFC";

		private const string ISCASH_PARM = "@IsCash";

		private const string DUEDATE_PARM = "@DueDate";

		private const string SOURCEDOCTYPE_PARM = "@SourceDocType";

		private const string ISWEIGHTINVOICE_PARM = "@IsWeightInvoice";

		private const string CLUSERID_PARM = "@CLUserID";

		private const string PROPERTYID_PARM = "@PropertyID";

		private const string PROPERTYUNITID_PARM = "@PropertyUnitID";

		private const string AGENTID_PARM = "@AgentID";

		private const string INVOICETYPE_PARM = "@InvoiceType";

		private const string DRIVERID_PARM = "@DriverID";

		private const string VEHICLEID_PARM = "@VehicleID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string FOCQUANTITY_PARM = "@FOCQuantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITPRICEFC_PARM = "@UnitPriceFC";

		private const string COST_PARM = "@Cost";

		private const string DESCRIPTION_PARM = "@Description";

		private const string REMARKS_PARM = "@Remarks";

		private const string UNITID_PARM = "@UnitID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string ORDERVOUCHERID_PARM = "@OrderVoucherID";

		private const string ORDERSYSDOCID_PARM = "@OrderSysDocID";

		private const string DNOTEVOUCHERID_PARM = "@DNoteVoucherID";

		private const string DNOTESYSDOCID_PARM = "@DNoteSysDocID";

		private const string ORDERROWINDEX_PARM = "@OrderRowIndex";

		private const string ISDNROW_PARM = "@IsDNRow";

		private const string ISRECOST_PARM = "@IsRecost";

		private const string ROWSOURCE_PARM = "@RowSource";

		private const string JOBID_PARM = "@JobID";

		private const string WEIGHTQUANTITY_PARM = "@WeightQuantity";

		private const string WEIGHTPRICE_PARM = "@WeightPrice";

		private const string SPECIFICATIONID_PARM = "@SpecificationID";

		private const string STYLEID_PARM = "@StyleID";

		private const string TAXPERCENTAGE_PARM = "@TaxPercentage";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string CONSIGNMENTNO_PARM = "@ConsignmentNo";

		private const string LISTVOUCHERID_PARM = "@ListVoucherID";

		private const string LISTSYSDOCID_PARM = "@ListSysDocID";

		private const string LISTROWINDEX_PARM = "@ListRowIndex";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string INVOICESYSDOCID_PARM = "@InvoiceSysDocID";

		private const string INVOICEVOUCHERID_PARM = "@InvoiceVoucherID";

		private const string ACCOUNTID_PARM = "@AccountID";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string PAYMENTMETHODTYPE_PARM = "@PaymentMethodType";

		private const string EXPAMOUNT_PARM = "@ExpAmount";

		private const string EXPCODE_PARM = "@ExpCode";

		private const string EXPPERCENT_PARM = "@ExpPercent";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string INVOICEPAYMENT_PARM = "@Invoice_Payment";

		private const string SALESINVOICEEXPENSETABLE_PARM = "@Sales_Invoice_Expense_NonInv";

		private const string EXPENSEID_PARM = "@ExpenseID";

		private const string RATETYPE_PARM = "@RateType";

		public SalesInvoiceNI(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSalesInvoiceText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Invoice_NonInv", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalesFlow", "@SalesFlow"), new FieldValue("IsExport", "@IsExport"), new FieldValue("DueDate", "@DueDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("ReportTo", "@ReportTo"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("BillingAddressID", "@BillingAddressID"), new FieldValue("ShipToAddress", "@ShipToAddress"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("RoundOff", "@RoundOff"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("ExpAmount", "@ExpAmount"), new FieldValue("ExpPercent", "@ExpPercent"), new FieldValue("ExpCode", "@ExpCode"), new FieldValue("TermID", "@TermID"), new FieldValue("IsWeightInvoice", "@IsWeightInvoice"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("IsCash", "@IsCash"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Note", "@Note"), new FieldValue("CLUserID", "@CLUserID"), new FieldValue("DriverID", "@DriverID"), new FieldValue("VehicleID", "@VehicleID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("PropertyID", "@PropertyID"), new FieldValue("PropertyUnitID", "@PropertyUnitID"), new FieldValue("AgentID", "@AgentID"), new FieldValue("InvoiceType", "@InvoiceType"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Sales_Invoice_NonInv", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesInvoiceCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesInvoiceText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesInvoiceText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@CustomerID", SqlDbType.NVarChar);
			parameters.Add("@SalesFlow", SqlDbType.TinyInt);
			parameters.Add("@IsExport", SqlDbType.Bit);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters.Add("@SalespersonID", SqlDbType.NVarChar);
			parameters.Add("@ReportTo", SqlDbType.NVarChar);
			parameters.Add("@RequiredDate", SqlDbType.DateTime);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@ShippingAddressID", SqlDbType.NVarChar);
			parameters.Add("@BillingAddressID", SqlDbType.NVarChar);
			parameters.Add("@ShipToAddress", SqlDbType.NVarChar);
			parameters.Add("@CustomerAddress", SqlDbType.NVarChar);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@IsWeightInvoice", SqlDbType.Bit);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters.Add("@ExpAmount", SqlDbType.Money);
			parameters.Add("@ExpPercent", SqlDbType.Decimal);
			parameters.Add("@ExpCode", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@DiscountFC", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@RoundOff", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@TotalFC", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@CLUserID", SqlDbType.NVarChar);
			parameters.Add("@IsCash", SqlDbType.Bit);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@DriverID", SqlDbType.NVarChar);
			parameters.Add("@VehicleID", SqlDbType.NVarChar);
			parameters.Add("@PropertyID", SqlDbType.NVarChar);
			parameters.Add("@PropertyUnitID", SqlDbType.NVarChar);
			parameters.Add("@AgentID", SqlDbType.NVarChar);
			parameters.Add("@InvoiceType", SqlDbType.Int);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@CustomerID"].SourceColumn = "CustomerID";
			parameters["@SalesFlow"].SourceColumn = "SalesFlow";
			parameters["@IsExport"].SourceColumn = "IsExport";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			parameters["@SalespersonID"].SourceColumn = "SalespersonID";
			parameters["@ReportTo"].SourceColumn = "ReportTo";
			parameters["@RequiredDate"].SourceColumn = "RequiredDate";
			parameters["@PriceIncludeTax"].SourceColumn = "PriceIncludeTax";
			parameters["@ShippingAddressID"].SourceColumn = "ShippingAddressID";
			parameters["@BillingAddressID"].SourceColumn = "BillingAddressID";
			parameters["@ShipToAddress"].SourceColumn = "ShipToAddress";
			parameters["@CustomerAddress"].SourceColumn = "CustomerAddress";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@IsWeightInvoice"].SourceColumn = "IsWeightInvoice";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			parameters["@ExpCode"].SourceColumn = "ExpCode";
			parameters["@ExpAmount"].SourceColumn = "ExpAmount";
			parameters["@ExpPercent"].SourceColumn = "ExpPercent";
			parameters["@CLUserID"].SourceColumn = "CLUserID";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@DiscountFC"].SourceColumn = "DiscountFC";
			parameters["@RoundOff"].SourceColumn = "RoundOff";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@TotalFC"].SourceColumn = "TotalFC";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@IsCash"].SourceColumn = "IsCash";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@DriverID"].SourceColumn = "DriverID";
			parameters["@VehicleID"].SourceColumn = "VehicleID";
			parameters["@PropertyID"].SourceColumn = "PropertyID";
			parameters["@PropertyUnitID"].SourceColumn = "PropertyUnitID";
			parameters["@AgentID"].SourceColumn = "AgentID";
			parameters["@InvoiceType"].SourceColumn = "InvoiceType";
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

		private string GetInsertUpdateSalesInvoiceDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Invoice_NonInv_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("FOCQuantity", "@FOCQuantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("WeightQuantity", "@WeightQuantity"), new FieldValue("WeightPrice", "@WeightPrice"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("Description", "@Description"), new FieldValue("Cost", "@Cost"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitID", "@UnitID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ConsignmentNo", "@ConsignmentNo"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxPercentage", "@TaxPercentage"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("OrderSysDocID", "@OrderSysDocID"), new FieldValue("OrderVoucherID", "@OrderVoucherID"), new FieldValue("DNoteSysDocID", "@DNoteSysDocID"), new FieldValue("DNoteVoucherID", "@DNoteVoucherID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("IsDNRow", "@IsDNRow"), new FieldValue("RowSource", "@RowSource"), new FieldValue("ListVoucherID", "@ListVoucherID"), new FieldValue("ListSysDocID", "@ListSysDocID"), new FieldValue("ListRowIndex", "@ListRowIndex"), new FieldValue("SpecificationID", "@SpecificationID"), new FieldValue("StyleID", "@StyleID"), new FieldValue("IsRecost", "@IsRecost"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateSalesInvoiceDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateSalesInvoiceDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateSalesInvoiceDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@FOCQuantity", SqlDbType.Real);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@UnitPriceFC", SqlDbType.Decimal);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@AmountFC", SqlDbType.Decimal);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@WeightQuantity", SqlDbType.Decimal);
			parameters.Add("@WeightPrice", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@ConsignmentNo", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Decimal);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@Cost", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@OrderSysDocID", SqlDbType.NVarChar);
			parameters.Add("@OrderVoucherID", SqlDbType.NVarChar);
			parameters.Add("@DNoteSysDocID", SqlDbType.NVarChar);
			parameters.Add("@DNoteVoucherID", SqlDbType.NVarChar);
			parameters.Add("@OrderRowIndex", SqlDbType.Int);
			parameters.Add("@IsDNRow", SqlDbType.Bit);
			parameters.Add("@IsRecost", SqlDbType.Bit);
			parameters.Add("@RowSource", SqlDbType.TinyInt);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@ListSysDocID", SqlDbType.NVarChar);
			parameters.Add("@ListVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ListRowIndex", SqlDbType.Int);
			parameters.Add("@SpecificationID", SqlDbType.NVarChar);
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxPercentage", SqlDbType.Decimal);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@FOCQuantity"].SourceColumn = "FOCQuantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@UnitPriceFC"].SourceColumn = "UnitPriceFC";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@WeightQuantity"].SourceColumn = "WeightQuantity";
			parameters["@WeightPrice"].SourceColumn = "WeightPrice";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Cost"].SourceColumn = "Cost";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@ConsignmentNo"].SourceColumn = "ConsignmentNo";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxPercentage"].SourceColumn = "TaxPercentage";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@OrderSysDocID"].SourceColumn = "OrderSysDocID";
			parameters["@OrderVoucherID"].SourceColumn = "OrderVoucherID";
			parameters["@DNoteSysDocID"].SourceColumn = "DNoteSysDocID";
			parameters["@DNoteVoucherID"].SourceColumn = "DNoteVoucherID";
			parameters["@OrderRowIndex"].SourceColumn = "OrderRowIndex";
			parameters["@IsDNRow"].SourceColumn = "IsDNRow";
			parameters["@IsRecost"].SourceColumn = "IsRecost";
			parameters["@RowSource"].SourceColumn = "RowSource";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@SpecificationID"].SourceColumn = "SpecificationID";
			parameters["@StyleID"].SourceColumn = "StyleID";
			parameters["@ListSysDocID"].SourceColumn = "ListSysDocID";
			parameters["@ListVoucherID"].SourceColumn = "ListVoucherID";
			parameters["@ListRowIndex"].SourceColumn = "ListRowIndex";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateInvoiceDNoteText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Invoice_DNote", new FieldValue("InvoiceSysDocID", "@InvoiceSysDocID"), new FieldValue("InvoiceVoucherID", "@InvoiceVoucherID"), new FieldValue("DNoteSysDocID", "@DNoteSysDocID"), new FieldValue("DNoteVoucherID", "@DNoteVoucherID"), new FieldValue("SourceDocType", "@SourceDocType"));
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInvoiceDNoteCommand(bool isUpdate)
		{
			if (insertCommand != null)
			{
				insertCommand = null;
			}
			insertCommand = new SqlCommand(GetInsertUpdateInvoiceDNoteText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@InvoiceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@InvoiceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@DNoteSysDocID", SqlDbType.NVarChar);
			parameters.Add("@DNoteVoucherID", SqlDbType.NVarChar);
			parameters.Add("@SourceDocType", SqlDbType.TinyInt);
			parameters["@InvoiceSysDocID"].SourceColumn = "InvoiceSysDocID";
			parameters["@InvoiceVoucherID"].SourceColumn = "InvoiceVoucherID";
			parameters["@DNoteSysDocID"].SourceColumn = "DNoteSysDocID";
			parameters["@DNoteVoucherID"].SourceColumn = "DNoteVoucherID";
			parameters["@SourceDocType"].SourceColumn = "SourceDocType";
			return insertCommand;
		}

		private string GetInsertUpdatePaymentText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Invoice_Payment", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("AccountID", "@AccountID"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Amount", "@Amount"), new FieldValue("PaymentMethodType", "@PaymentMethodType"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePaymentCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePaymentText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePaymentText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@AccountID", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@PaymentMethodType", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@AccountID"].SourceColumn = "AccountID";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@PaymentMethodType"].SourceColumn = "PaymentMethodType";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(SalesInvoiceNIData salesInvoiceNIData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = "";
				DataRow dataRow = salesInvoiceNIData.SalesInvoiceTable.Rows[0];
				dataRow["VoucherID"].ToString();
				dataRow["SysDocID"].ToString();
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (!isUpdate && itemSourceTypes == ItemSourceTypes.DeliveryNote)
				{
					ArrayList arrayList = new ArrayList();
					foreach (DataRow row in salesInvoiceNIData.SalesInvoiceDetailTable.Rows)
					{
						string text2 = row["OrderSysDocID"].ToString() + row["OrderVoucherID"].ToString();
						if (!arrayList.Contains(text2))
						{
							arrayList.Add(text2);
						}
					}
					string text3 = "";
					for (int i = 0; i < arrayList.Count; i++)
					{
						if (i != 0)
						{
							text3 += ",";
						}
						text3 = text3 + "'" + arrayList[i] + "'";
					}
					text = "select COUNT(*) FROM Sales_Invoice_NonInv_Detail SID INNER JOIN Sales_Invoice_NonInv SI \r\n                            ON SID.SysDocID = SI.SysDOcID AND SID.VoucherID = SI.VoucherID WHERE ISNULL(IsVoid,'False')='False' AND OrderSysDocID + ordervoucherid  IN (" + text3 + ")";
					if (ExecuteScalar(text, sqlTransaction).IsNumericGreaterThanZero())
					{
						throw new CompanyException("One or more of selected delivery notes are already invoiced.");
					}
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertUpdateSalesInvoice(SalesInvoiceNIData salesInvoiceNIData, bool isUpdate)
		{
			return InsertUpdateSalesInvoice(salesInvoiceNIData, isUpdate, null);
		}

		public bool InsertUpdateSalesInvoice(SalesInvoiceNIData salesInvoiceNIData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			SqlCommand sqlCommand = null;
			string text = "";
			string text2 = "";
			string text3 = "";
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = salesInvoiceNIData.SalesInvoiceTable.Rows[0];
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				string text4 = dataRow["VoucherID"].ToString();
				string sysDocID = dataRow["SysDocID"].ToString();
				flag &= ValidateData(salesInvoiceNIData, isUpdate, sqlTransaction);
				bool flag2 = false;
				if (dataRow["IsExport"] != DBNull.Value)
				{
					flag2 = bool.Parse(dataRow["IsExport"].ToString());
				}
				string text5 = "";
				if (flag2)
				{
					object companyOptionValue = new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString());
					if (companyOptionValue != null)
					{
						text5 = companyOptionValue.ToString();
					}
				}
				else
				{
					object companyOptionValue2 = new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString());
					if (companyOptionValue2 != null)
					{
						text5 = companyOptionValue2.ToString();
					}
				}
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text5 != "")
				{
					salesFlows = (SalesFlows)int.Parse(text5.ToString());
				}
				bool isDNoteInventory = false;
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					isDNoteInventory = true;
				}
				if (isUpdate && InvoiceHasShippedQuantity(sysDocID, text4, sqlTransaction))
				{
					throw new CompanyException("Unable to update. Some of the items in this invoice has been already shipped.");
				}
				bool result = false;
				bool.TryParse(dataRow["IsCash"].ToString(), out result);
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (!isUpdate && itemSourceTypes == ItemSourceTypes.DeliveryNote)
				{
					DeliveryNote deliveryNote = new DeliveryNote(base.DBConfig);
					foreach (DataRow row in salesInvoiceNIData.InvoiceDNoteTable.Rows)
					{
						string sysDocID2 = row["DNoteSysDocID"].ToString();
						string text6 = row["DNoteVoucherID"].ToString();
						if (deliveryNote.IsDNoteInvoiced(sysDocID2, text6, sqlTransaction))
						{
							throw new CompanyException("One or more delivery notes are already invoiced.\nDocument:" + text6);
						}
					}
				}
				decimal num = default(decimal);
				foreach (DataRow row2 in salesInvoiceNIData.SalesInvoiceDetailTable.Rows)
				{
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal.TryParse(row2["Quantity"].ToString(), out result3);
					decimal.TryParse(row2["UnitPrice"].ToString(), out result4);
					decimal.TryParse(row2["Amount"].ToString(), out result2);
					num += result2;
				}
				dataRow["Total"] = num;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Sales_Invoice_NonInv", "VoucherID", dataRow["SysDocID"].ToString(), text4, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				bool flag3 = false;
				decimal result5 = 1m;
				string a = "M";
				if (dataRow["CurrencyID"] != DBNull.Value && baseCurrencyID != dataRow["CurrencyID"].ToString())
				{
					flag3 = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result5);
					a = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
				}
				if (flag3)
				{
					decimal result6 = default(decimal);
					dataRow["TotalFC"] = dataRow["Total"];
					decimal.TryParse(dataRow["Total"].ToString(), out result6);
					result6 = ((!(a == "M")) ? Math.Round(result6 / result5, 4) : Math.Round(result6 * result5, 4));
					dataRow["Total"] = result6;
					result6 = default(decimal);
					dataRow["DiscountFC"] = dataRow["Discount"];
					decimal.TryParse(dataRow["DiscountFC"].ToString(), out result6);
					result6 = ((!(a == "M")) ? Math.Round(result6 / result5, 4) : Math.Round(result6 * result5, 4));
					dataRow["Discount"] = result6;
					result6 = default(decimal);
					dataRow["TaxAmountFC"] = dataRow["TaxAmount"];
					decimal.TryParse(dataRow["TaxAmount"].ToString(), out result6);
					result6 = ((!(a == "M")) ? Math.Round(result6 / result5, 4) : Math.Round(result6 * result5, 4));
					dataRow["TaxAmount"] = result6;
				}
				foreach (DataRow row3 in salesInvoiceNIData.SalesInvoiceDetailTable.Rows)
				{
					row3["SysDocID"] = dataRow["SysDocID"];
					row3["VoucherID"] = dataRow["VoucherID"];
					text3 = row3["ProductID"].ToString();
					string checkFieldValue = row3["LocationID"].ToString();
					decimal result7 = default(decimal);
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product_Location", "Quantity", "ProductID", text3, "LocationID", checkFieldValue, sqlTransaction);
					if (fieldValue != null)
					{
						decimal.TryParse(fieldValue.ToString(), out result7);
					}
					if (row3["FOCQuantity"] != DBNull.Value)
					{
						_ = (decimal.Parse(row3["FOCQuantity"].ToString()) > 0m);
					}
					float num2 = 0f;
					string text7 = "";
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text3, sqlTransaction);
					if (fieldValue != null)
					{
						text7 = fieldValue.ToString();
					}
					if (text7 != "" && row3["UnitID"] != DBNull.Value && row3["UnitID"].ToString() != text7)
					{
						DataRow obj3 = new Products(base.DBConfig).GetProductUnitRow(text3, row3["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text3 + "\nUnit:" + row3["UnitID"].ToString(), 1051);
						float num3 = float.Parse(obj3["Factor"].ToString());
						string text8 = obj3["FactorType"].ToString();
						num2 = float.Parse(row3["Quantity"].ToString());
						row3["UnitFactor"] = num3;
						row3["FactorType"] = text8;
						row3["UnitQuantity"] = row3["Quantity"];
						num2 = ((!(text8 == "M")) ? float.Parse(Math.Round(num2 * num3, 5).ToString()) : float.Parse(Math.Round(num2 / num3, 5).ToString()));
						row3["Quantity"] = num2;
					}
					if ((decimal)num2 > result7)
					{
						row3["IsRecost"] = true;
					}
					if (flag3)
					{
						decimal result8 = default(decimal);
						decimal result9 = default(decimal);
						row3["UnitPriceFC"] = row3["UnitPrice"];
						row3["AmountFC"] = row3["Amount"];
						decimal.TryParse(row3["UnitPrice"].ToString(), out result8);
						decimal.TryParse(row3["Amount"].ToString(), out result9);
						result8 = ((!(a == "M")) ? Math.Round(result8 / result5, 4) : Math.Round(result8 * result5, 4));
						row3["UnitPrice"] = result8;
						result9 = ((!(a == "M")) ? Math.Round(result9 / result5, currencyDecimalPoints) : Math.Round(result9 * result5, currencyDecimalPoints));
						row3["Amount"] = result9;
					}
				}
				if (isUpdate)
				{
					flag &= DeleteSalesInvoiceDetailsRows(sysDocID, text4, isDeleteTransaction: false, sqlTransaction);
				}
				sqlCommand = GetInsertUpdateSalesInvoiceCommand(isUpdate);
				sqlCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(salesInvoiceNIData, "Sales_Invoice_NonInv", sqlCommand)) : (flag & Insert(salesInvoiceNIData, "Sales_Invoice_NonInv", sqlCommand)));
				if (salesInvoiceNIData.Tables["Sales_Invoice_NonInv_Detail"].Rows.Count > 0)
				{
					sqlCommand = GetInsertUpdateSalesInvoiceDetailsCommand(isUpdate: false);
					sqlCommand.Transaction = sqlTransaction;
					flag &= Insert(salesInvoiceNIData, "Sales_Invoice_NonInv_Detail", sqlCommand);
				}
				if (result)
				{
					DataRow dataRow3 = salesInvoiceNIData.PaymentTable.Rows[0];
					PaymentMethodTypes paymentMethodTypes = (PaymentMethodTypes)byte.Parse(dataRow3["PaymentMethodType"].ToString());
					string registerID = dataRow3["RegisterID"].ToString();
					string text9 = "";
					text9 = (string)(dataRow3["AccountID"] = ((paymentMethodTypes != PaymentMethodTypes.CreditCard) ? new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID") : new Register(base.DBConfig).GetRegisterAccountID(registerID, "CardReceivedAccountID")));
					dataRow3.EndEdit();
				}
				if (itemSourceTypes == ItemSourceTypes.SalesOrder)
				{
					foreach (DataRow row4 in salesInvoiceNIData.SalesInvoiceDetailTable.Rows)
					{
						text3 = row4["ProductID"].ToString();
						text = row4["OrderVoucherID"].ToString();
						text2 = row4["OrderSysDocID"].ToString();
						int result10 = 0;
						if (!(text == "") && !(text2 == ""))
						{
							int.TryParse(row4["OrderRowIndex"].ToString(), out result10);
							float result11 = 0f;
							if (row4["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row4["UnitQuantity"].ToString(), out result11);
							}
							else
							{
								float.TryParse(row4["Quantity"].ToString(), out result11);
							}
							float num4 = new Products(base.DBConfig).GetReservedQuantity(text3, sqlTransaction) - result11;
							if (num4 < 0f)
							{
								num4 = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateReservedQuantity(text3, num4, sqlTransaction);
							flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text2, text, result10, result11, sqlTransaction);
						}
					}
					text = salesInvoiceNIData.SalesInvoiceDetailTable.Rows[0]["OrderVoucherID"].ToString();
					text2 = salesInvoiceNIData.SalesInvoiceDetailTable.Rows[0]["OrderSysDocID"].ToString();
					if (text != "")
					{
						flag &= new SalesOrder(base.DBConfig).CloseShippedOrder(text2, text, sqlTransaction);
						flag &= new SalesProformaInvoice(base.DBConfig).CloseShippedOrder(text2, text, sqlTransaction);
					}
				}
				if (salesInvoiceNIData.Tables.Contains("Tax_Detail"))
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(salesInvoiceNIData, sysDocID, text4, isUpdate, sqlTransaction);
				}
				switch (int.Parse(salesInvoiceNIData.SalesInvoiceTable.Rows[0]["InvoiceType"].ToString()))
				{
				case 1:
				{
					GLData journalData2 = CreateInvoiceGLData(salesInvoiceNIData, isDNoteInventory, sqlTransaction);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData2, isUpdate, sqlTransaction);
					break;
				}
				case 2:
				{
					GLData journalData = CreatePropertyServiceInvoiceGLData(salesInvoiceNIData, isDNoteInventory, sqlTransaction);
					flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
					break;
				}
				}
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Sales_Invoice_NonInv", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "";
				if (salesInvoiceNIData.SalesInvoiceTable.Rows.Count > 0)
				{
					NonInventoryInvoiceType nonInventoryInvoiceType = (NonInventoryInvoiceType)int.Parse(salesInvoiceNIData.SalesInvoiceTable.Rows[0]["InvoiceType"].ToString());
					entityName = ((nonInventoryInvoiceType != NonInventoryInvoiceType.SalesInvoice) ? "Property Service Invoice" : "Sales Invoice-Non Inv");
				}
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text4, sysDocID, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text4, sysDocID, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Sales_Invoice_NonInv", "VoucherID", sqlTransaction);
				}
				if (flag)
				{
					NonInventoryInvoiceType nonInventoryInvoiceType = (NonInventoryInvoiceType)int.Parse(salesInvoiceNIData.SalesInvoiceTable.Rows[0]["InvoiceType"].ToString());
					flag = ((nonInventoryInvoiceType != NonInventoryInvoiceType.SalesInvoice) ? (flag & new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PropertyServiceInvoice, sysDocID, text4, "Sales_Invoice_NonInv", sqlTransaction)) : (flag & new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SalesInvoiceNI, sysDocID, text4, "Sales_Invoice_NonInv", sqlTransaction)));
				}
				ModifyTransactions(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), dataRow["CurrentUser"].ToString(), isModify: true, "toupdate");
				return flag;
			}
			catch (Exception)
			{
				flag = false;
				throw;
			}
			finally
			{
				base.DBConfig.EndTransaction(flag);
			}
		}

		private GLData CreatePropertyServiceInvoiceGLData(SalesInvoiceNIData transactionData, bool isDNoteInventory, SqlTransaction sqlTransaction)
		{
			GLData gLData = new GLData();
			DataRow dataRow = transactionData.SalesInvoiceTable.Rows[0];
			int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
			decimal result = default(decimal);
			decimal.TryParse(dataRow["TaxAmount"].ToString(), out result);
			string text = dataRow["CustomerID"].ToString();
			string text2 = dataRow["SysDocID"].ToString();
			dataRow["VoucherID"].ToString();
			string value = dataRow["CompanyID"].ToString();
			string value2 = dataRow["DivisionID"].ToString();
			string value3 = dataRow["PropertyID"].ToString();
			string value4 = dataRow["PropertyUnitID"].ToString();
			string text3 = "";
			string text4 = "";
			string textCommand = "SELECT SD.LocationID,ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID,ISNULL(SD.COGSAccountID,LOC.COGSAccountID) AS COGSAccountID,\r\n                                ISNULL(SD.DiscountGivenAccountID,LOC.DiscountGivenAccountID) AS DiscountGivenAccountID,LOC.InventoryAccountID,ISNULL(SD.SalesAccountID,LOC.SalesAccountID) AS SalesAccountID,\r\n                                 LOC.UnInvoicedInventoryAccountID, ISNULL(SD.SalesTaxAccountID,LOC.SalesTaxAccountID) AS SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID,Loc.ConsignInAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
			DataSet dataSet = new DataSet();
			FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
			if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
			{
				throw new CompanyException("There is no location assigned to this system document or location record is missing.");
			}
			DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
			dataRow2["BaseCurrencyID"].ToString();
			text3 = dataRow2["ARAccountID"].ToString();
			DataRow dataRow3 = gLData.JournalTable.NewRow();
			SysDocTypes sysDocTypes = SysDocTypes.PropertyServiceInvoice;
			dataRow3["JournalID"] = 0;
			dataRow3["JournalDate"] = dataRow["TransactionDate"];
			dataRow3["SysDocID"] = dataRow["SysDocID"];
			dataRow3["SysDocType"] = (int)sysDocTypes;
			dataRow3["VoucherID"] = dataRow["VoucherID"];
			dataRow3["Reference"] = "";
			dataRow3["Narration"] = "Property Service Invoice" + dataRow["VoucherID"];
			dataRow3["Note"] = dataRow["Note"];
			dataRow3.EndEdit();
			gLData.JournalTable.Rows.Add(dataRow3);
			new Hashtable();
			new ArrayList();
			decimal num = default(decimal);
			decimal d = default(decimal);
			foreach (DataRow row in transactionData.SalesInvoiceDetailTable.Rows)
			{
				string text5 = row["ProductID"].ToString();
				num = decimal.Parse(row["Amount"].ToString());
				text4 = new PropertyIncomeCode(base.DBConfig).GetIncomeAccountID(text5, sqlTransaction);
				DataRow dataRow4 = gLData.JournalDetailsTable.NewRow();
				dataRow4.BeginEdit();
				decimal result2 = default(decimal);
				decimal.TryParse(row["TaxAmount"].ToString(), out result2);
				dataRow4["JournalID"] = 0;
				dataRow4["IsBaseOnly"] = true;
				dataRow4["AccountID"] = text4;
				if (num > 0m)
				{
					dataRow4["Debit"] = DBNull.Value;
					dataRow4["Credit"] = Math.Abs(num);
				}
				else
				{
					dataRow4["Debit"] = num;
					dataRow4["Credit"] = DBNull.Value;
				}
				dataRow4["Reference"] = text5;
				dataRow4["CompanyID"] = value;
				dataRow4["DivisionID"] = value2;
				dataRow4["AttributeID1"] = value3;
				dataRow4["AttributeID2"] = value4;
				dataRow4.EndEdit();
				gLData.JournalDetailsTable.Rows.Add(dataRow4);
				d += Math.Round(num, 4);
			}
			DataRow dataRow5 = gLData.JournalDetailsTable.NewRow();
			dataRow5.BeginEdit();
			dataRow5["JournalID"] = 0;
			dataRow5["AccountID"] = text3;
			dataRow5["PayeeID"] = text;
			dataRow5["Debit"] = d + result;
			dataRow5["Credit"] = DBNull.Value;
			dataRow5["IsBaseOnly"] = true;
			dataRow5["PayeeType"] = "C";
			dataRow5["Reference"] = "";
			dataRow5["IsARAP"] = true;
			dataRow5["CompanyID"] = value;
			dataRow5["DivisionID"] = value2;
			dataRow5["AttributeID1"] = value3;
			dataRow5["AttributeID2"] = value4;
			dataRow5.EndEdit();
			gLData.JournalDetailsTable.Rows.Add(dataRow5);
			if (result > 0m)
			{
				if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
				{
					throw new CompanyException("Tax details not found for the transaction.");
				}
				DataRow[] array = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
				decimal num2 = default(decimal);
				for (int i = 0; i < array.Length; i++)
				{
					num2 = default(decimal);
					DataRow obj2 = array[i];
					dataRow5 = gLData.JournalDetailsTable.NewRow();
					dataRow5.BeginEdit();
					dataRow5["JournalID"] = 0;
					string text6 = "";
					text6 = obj2["TaxItemID"].ToString();
					string text7 = "";
					textCommand = "SELECT SalesTaxAccountID FROM Tax WHERE  TaxCode = '" + text6.Trim() + "'";
					object obj3 = ExecuteScalar(textCommand);
					if (obj3 != null)
					{
						text7 = obj3.ToString();
					}
					if (text7 == "")
					{
						throw new CompanyException("AccountID is not set for tax item: " + text6 + ".");
					}
					decimal.TryParse(obj2["TaxAmount"].ToString(), out num2);
					dataRow5["AccountID"] = text7;
					dataRow5["PayeeID"] = text;
					dataRow5["PayeeType"] = "A";
					dataRow5["Debit"] = DBNull.Value;
					dataRow5["Credit"] = Math.Round(num2, currencyDecimalPoints, MidpointRounding.AwayFromZero);
					dataRow5["CompanyID"] = value;
					dataRow5["DivisionID"] = value2;
					dataRow5["AttributeID1"] = value3;
					dataRow5["AttributeID2"] = value4;
					dataRow5.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow5);
				}
			}
			return gLData;
		}

		private GLData CreateInvoiceGLData(SalesInvoiceNIData transactionData, bool isDNoteInventory, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.SalesInvoiceTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string text = dataRow["CustomerID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string voucherID = dataRow["VoucherID"].ToString();
				string value = dataRow["CompanyID"].ToString();
				string value2 = dataRow["DivisionID"].ToString();
				bool result = false;
				if (!dataRow["PriceIncludeTax"].IsDBNullOrEmpty())
				{
					bool.TryParse(dataRow["PriceIncludeTax"].ToString(), out result);
				}
				string value3 = dataRow["JobID"].ToString();
				string value4 = dataRow["CostCategoryID"].ToString();
				string text3 = "";
				if (isDNoteInventory)
				{
					foreach (DataRow row in transactionData.InvoiceDNoteTable.Rows)
					{
						if (text3 != "")
						{
							text3 += ",";
						}
						text3 += row["DNoteVoucherID"];
					}
				}
				string textCommand = "SELECT SD.LocationID,ISNULL(CUS.ARAccountID,ISNULL(CLS.ARAccountID, LOC.ARAccountID)) AS ARAccountID,ISNULL(SD.COGSAccountID,LOC.COGSAccountID) AS COGSAccountID,\r\n                                ISNULL(SD.DiscountGivenAccountID,LOC.DiscountGivenAccountID) AS DiscountGivenAccountID,LOC.InventoryAccountID,ISNULL(SD.SalesAccountID,LOC.SalesAccountID) AS SalesAccountID,\r\n                                 LOC.UnInvoicedInventoryAccountID, ISNULL(SD.SalesTaxAccountID,LOC.SalesTaxAccountID) AS SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID,Loc.ConsignInAccountID,LOC.RoundOffAccountID AS RoundOffAccountID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Customer CUS ON CustomerID='" + text + "'\r\n                                LEFT OUTER JOIN Customer_Class CLS ON CUS.CustomerClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow3 = dataSet.Tables["Accounts"].Rows[0];
				string docLocationID = dataRow3["LocationID"].ToString();
				string value5 = dataRow3["DiscountGivenAccountID"].ToString();
				dataRow3["SalesTaxAccountID"].ToString();
				string value6 = dataRow3["ARAccountID"].ToString();
				string text4 = dataRow3["UnInvoicedInventoryAccountID"].ToString();
				string value7 = dataRow3["RoundOffAccountID"].ToString();
				string text5 = dataRow3["BaseCurrencyID"].ToString();
				bool flag = false;
				decimal result2 = 1m;
				if (dataRow["CurrencyID"] != DBNull.Value && text5 != dataRow["CurrencyID"].ToString())
				{
					flag = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result2);
				}
				bool result3 = false;
				bool.TryParse(dataRow["IsCash"].ToString(), out result3);
				bool result4 = false;
				bool.TryParse(dataRow["IsExport"].ToString(), out result4);
				DataRow dataRow4 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.SalesInvoice;
				if (result3)
				{
					sysDocTypes = SysDocTypes.SalesReceipt;
				}
				else if (result4)
				{
					sysDocTypes = SysDocTypes.ExportSalesInvoice;
				}
				int.TryParse(dataRow["PaymentMethodType"].ToString(), out int result5);
				if (result3 && result5 == 5)
				{
					result3 = false;
				}
				dataRow4["JournalID"] = 0;
				dataRow4["JournalDate"] = dataRow["TransactionDate"];
				dataRow4["SysDocID"] = dataRow["SysDocID"];
				dataRow4["SysDocType"] = (byte)sysDocTypes;
				dataRow4["VoucherID"] = dataRow["VoucherID"];
				dataRow4["CurrencyID"] = dataRow["CurrencyID"];
				dataRow4["CurrencyRate"] = dataRow["CurrencyRate"];
				dataRow4["Reference"] = dataRow["Reference"];
				dataRow4.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow4);
				decimal result6 = default(decimal);
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				Hashtable hashtable2 = new Hashtable();
				ArrayList arrayList2 = new ArrayList();
				Hashtable hashtable3 = new Hashtable();
				ArrayList arrayList3 = new ArrayList();
				decimal d = default(decimal);
				decimal d2 = default(decimal);
				string text6 = dataRow["ExpCode"].ToString();
				if (flag)
				{
					decimal.TryParse(dataRow["TotalFC"].ToString(), out result6);
				}
				else
				{
					decimal.TryParse(dataRow["Total"].ToString(), out result6);
				}
				decimal result7 = default(decimal);
				decimal.TryParse(dataRow["ExpAmount"].ToString(), out result7);
				decimal result8 = default(decimal);
				decimal.TryParse(dataRow["ExpPercent"].ToString(), out result8);
				decimal result9 = default(decimal);
				if (dataRow["TaxAmountFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["TaxAmountFC"].ToString(), out result9);
				}
				else
				{
					decimal.TryParse(dataRow["TaxAmount"].ToString(), out result9);
				}
				decimal result10 = default(decimal);
				if (dataRow["RoundOff"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["RoundOff"].ToString(), out result10);
				}
				string text7 = "";
				decimal num = default(decimal);
				if (text6 != "" && result8 == 0m)
				{
					num = result6 - result7;
				}
				else if (text6 != "" && result8 > 0m)
				{
					num = result7;
				}
				if (text6 != "")
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
					text7 = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text6, sqlTransaction);
					base.DBConfig.EndTransaction(result: true);
				}
				foreach (DataRow row2 in transactionData.SalesInvoiceDetailTable.Rows)
				{
					string text8 = row2["ProductID"].ToString();
					string warehouseLocationID = row2["LocationID"].ToString();
					int rowIndex = int.Parse(row2["RowIndex"].ToString());
					dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(text8, docLocationID, warehouseLocationID, text2, sqlTransaction);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException("Product accounts information not found for product or location.");
					}
					DataRow dataRow6 = dataSet.Tables[0].Rows[0];
					string text9 = dataRow6["IncomeAccountID"].ToString();
					string text10 = dataRow6["ConsignInAccountID"].ToString();
					string text11 = dataRow6["COGSAccountID"].ToString();
					string text12 = dataRow6["InventoryAssetAccountID"].ToString();
					if (isDNoteInventory && !result3)
					{
						text12 = text4;
					}
					ItemTypes itemTypes = ItemTypes.Inventory;
					object obj = dataRow6["ItemType"].ToString();
					if (obj == null || !(obj.ToString() != ""))
					{
						throw new CompanyException("Item type is not selected for the product:" + text8);
					}
					itemTypes = (ItemTypes)byte.Parse(obj.ToString());
					decimal result11 = default(decimal);
					decimal.TryParse(dataRow6["AverageCost"].ToString(), out result11);
					decimal num2 = default(decimal);
					decimal result12 = default(decimal);
					decimal result13 = default(decimal);
					if (flag)
					{
						decimal.TryParse(row2["AmountFC"].ToString(), out result12);
					}
					else
					{
						decimal.TryParse(row2["Amount"].ToString(), out result12);
					}
					decimal.TryParse(row2["TaxAmount"].ToString(), out result13);
					num2 = ((row2["UnitQuantity"] == DBNull.Value) ? decimal.Parse(row2["Quantity"].ToString()) : decimal.Parse(row2["UnitQuantity"].ToString()));
					decimal num3 = default(decimal);
					bool flag2 = false;
					if (row2["IsDNRow"] != DBNull.Value)
					{
						flag2 = bool.Parse(row2["IsDNRow"].ToString());
					}
					if (itemTypes != ItemTypes.ConsignmentItem)
					{
						if (num2 >= 0m)
						{
							if (flag2)
							{
								string voucherID2 = row2["OrderVoucherID"].ToString();
								string sysDocID = row2["OrderSysDocID"].ToString();
								int rowIndex2 = int.Parse(row2["OrderRowIndex"].ToString());
								num3 = Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text8, sysDocID, voucherID2, rowIndex2, mergeWithRefRows: true, sqlTransaction));
							}
							else
							{
								num3 = Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text8, text2, voucherID, rowIndex, mergeWithRefRows: false, sqlTransaction));
							}
						}
						else
						{
							num3 = -1m * Math.Abs(new InventoryTransaction(base.DBConfig).GetRowAssetValue(text8, text2, voucherID, rowIndex, mergeWithRefRows: false, sqlTransaction));
						}
					}
					else
					{
						num3 = default(decimal);
					}
					decimal result14 = default(decimal);
					if (flag)
					{
						decimal.TryParse(row2["UnitPriceFC"].ToString(), out result14);
					}
					else
					{
						decimal.TryParse(row2["UnitPrice"].ToString(), out result14);
					}
					string text13;
					if (itemTypes == ItemTypes.Inventory || itemTypes == ItemTypes.Assembly)
					{
						if (text11 == "")
						{
							throw new CompanyException("COGS account is not set for the location.");
						}
						text13 = text11;
						if (hashtable.ContainsKey(text13))
						{
							result6 = decimal.Parse(hashtable[text13].ToString());
							result6 += Math.Round(num3, currencyDecimalPoints);
							hashtable[text13] = result6;
						}
						else
						{
							hashtable.Add(text13, Math.Round(num3, currencyDecimalPoints));
							arrayList.Add(text13);
						}
						text13 = text12;
						if (text12 == "")
						{
							throw new CompanyException("Asset account is not set for the location.");
						}
						if (hashtable3.ContainsKey(text13))
						{
							result6 = decimal.Parse(hashtable3[text13].ToString());
							result6 += Math.Round(num3, currencyDecimalPoints);
							hashtable3[text13] = result6;
						}
						else
						{
							hashtable3.Add(text13, Math.Round(num3, currencyDecimalPoints));
							arrayList3.Add(text13);
						}
						d += Math.Round(num3, currencyDecimalPoints);
					}
					if (itemTypes == ItemTypes.ConsignmentItem)
					{
						if (text10 == "")
						{
							throw new CompanyException("Consignment In account is not set for the location.");
						}
						text13 = text10;
					}
					else
					{
						if (text9 == "")
						{
							throw new CompanyException("Income account is not set for the location.");
						}
						text13 = text9;
					}
					if (hashtable2.ContainsKey(text13))
					{
						result6 = decimal.Parse(hashtable2[text13].ToString());
						if (result)
						{
							result6 += Math.Round(result12 - result13, 4);
						}
						else
						{
							result6 += Math.Round(result12, 4);
						}
						hashtable2[text13] = result6;
					}
					else
					{
						result6 = ((!result) ? Math.Round(result12, currencyDecimalPoints) : Math.Round(result12 - result13, 4));
						hashtable2.Add(text13, Math.Round(result6, 4));
						arrayList2.Add(text13);
					}
					d2 += Math.Round(result12, currencyDecimalPoints);
				}
				decimal result15 = default(decimal);
				if (dataRow["DiscountFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["DiscountFC"].ToString(), out result15);
				}
				else
				{
					decimal.TryParse(dataRow["Discount"].ToString(), out result15);
				}
				if (dataRow["TaxAmountFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["TaxAmountFC"].ToString(), out result9);
				}
				else
				{
					decimal.TryParse(dataRow["TaxAmount"].ToString(), out result9);
				}
				if (d != 0m)
				{
					for (int i = 0; i < hashtable3.Count; i++)
					{
						DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
						dataRow7.BeginEdit();
						string text13 = arrayList3[i].ToString();
						result6 = decimal.Parse(hashtable3[text13].ToString());
						dataRow7["JournalID"] = 0;
						dataRow7["AccountID"] = text13;
						dataRow7["PayeeID"] = text;
						dataRow7["JobID"] = value3;
						dataRow7["CostCategoryID"] = value4;
						dataRow7["Debit"] = DBNull.Value;
						dataRow7["Credit"] = result6;
						dataRow7["IsBaseOnly"] = true;
						dataRow7["Reference"] = dataRow["Reference"];
						dataRow7["JVEntryType"] = (byte)1;
						dataRow7["CompanyID"] = value;
						dataRow7["DivisionID"] = value2;
						dataRow7.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow7);
					}
				}
				if (d != 0m)
				{
					for (int j = 0; j < hashtable.Count; j++)
					{
						DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
						dataRow7.BeginEdit();
						string text13 = arrayList[j].ToString();
						result6 = decimal.Parse(hashtable[text13].ToString());
						dataRow7["JournalID"] = 0;
						dataRow7["AccountID"] = text13;
						dataRow7["PayeeID"] = text;
						dataRow7["JobID"] = value3;
						dataRow7["CostCategoryID"] = value4;
						dataRow7["Debit"] = result6;
						if (text6 != "")
						{
							dataRow7["Debit"] = num;
						}
						dataRow7["Credit"] = DBNull.Value;
						dataRow7["IsBaseOnly"] = true;
						dataRow7["Reference"] = dataRow["Reference"];
						dataRow7["JVEntryType"] = (byte)2;
						dataRow7["CompanyID"] = value;
						dataRow7["DivisionID"] = value2;
						dataRow7.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow7);
					}
				}
				if (text6 != "" && text7 != "")
				{
					DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					dataRow7["AccountID"] = text7;
					dataRow7["PayeeID"] = text;
					dataRow7["JobID"] = value3;
					dataRow7["CostCategoryID"] = value4;
					dataRow7["Debit"] = result7;
					dataRow7["Credit"] = DBNull.Value;
					dataRow7["IsBaseOnly"] = true;
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7["JVEntryType"] = (byte)4;
					dataRow7["CompanyID"] = value;
					dataRow7["DivisionID"] = value2;
					dataRow7.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow7);
				}
				for (int k = 0; k < hashtable2.Count; k++)
				{
					DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					string text13 = arrayList2[k].ToString();
					result6 = decimal.Parse(hashtable2[text13].ToString());
					result6 = Math.Round(result6, currencyDecimalPoints);
					dataRow7["JournalID"] = 0;
					dataRow7["AccountID"] = text13;
					dataRow7["PayeeID"] = text;
					dataRow7["JobID"] = value3;
					dataRow7["CostCategoryID"] = value4;
					if (flag)
					{
						if (result6 > 0m)
						{
							dataRow7["DebitFC"] = DBNull.Value;
							dataRow7["CreditFC"] = result6;
						}
						else
						{
							dataRow7["DebitFC"] = Math.Abs(result6);
							dataRow7["CreditFC"] = DBNull.Value;
						}
					}
					else if (result6 > 0m)
					{
						dataRow7["Debit"] = DBNull.Value;
						dataRow7["Credit"] = result6;
					}
					else
					{
						dataRow7["Debit"] = Math.Abs(result6);
						dataRow7["Credit"] = DBNull.Value;
					}
					dataRow7["JVEntryType"] = (byte)3;
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7["CompanyID"] = value;
					dataRow7["DivisionID"] = value2;
					dataRow7.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow7);
				}
				decimal d3 = default(decimal);
				foreach (DataRow row3 in transactionData.InvoiceExpenseTable.Rows)
				{
					string text14 = row3["ExpenseID"].ToString();
					result6 = decimal.Parse(row3["Amount"].ToString());
					decimal result16 = default(decimal);
					if (row3["AmountFC"] != DBNull.Value)
					{
						decimal.TryParse(row3["AmountFC"].ToString(), out result16);
					}
					string expenseAccountID = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text14, sqlTransaction);
					DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					dataRow7["AccountID"] = expenseAccountID;
					string a = (string)(dataRow7["CurrencyID"] = row3["CurrencyID"].ToString());
					dataRow7["CurrencyRate"] = row3["CurrencyRate"];
					dataRow7["JobID"] = value3;
					dataRow7["CostCategoryID"] = value4;
					if (a != text5)
					{
						dataRow7["DebitFC"] = DBNull.Value;
						dataRow7["CreditFC"] = result16;
						d3 += result16;
					}
					else
					{
						dataRow7["DebitFC"] = DBNull.Value;
						dataRow7["CreditFC"] = DBNull.Value;
						dataRow7["Debit"] = DBNull.Value;
						dataRow7["Credit"] = result6;
						d3 += result6;
					}
					dataRow7["JVEntryType"] = (byte)4;
					dataRow7["Reference"] = text14;
					dataRow7["CompanyID"] = value;
					dataRow7["DivisionID"] = value2;
					dataRow7.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow7);
				}
				if (result15 > 0m)
				{
					DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					dataRow7["AccountID"] = value5;
					dataRow7["PayeeID"] = text;
					dataRow7["PayeeType"] = "A";
					dataRow7["JobID"] = value3;
					dataRow7["CostCategoryID"] = value4;
					if (flag)
					{
						dataRow7["DebitFC"] = result15;
						dataRow7["CreditFC"] = DBNull.Value;
					}
					else
					{
						dataRow7["Debit"] = result15;
						dataRow7["Credit"] = DBNull.Value;
					}
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7["JVEntryType"] = (byte)5;
					dataRow7["CompanyID"] = value;
					dataRow7["DivisionID"] = value2;
					dataRow7.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow7);
				}
				if (result10 != 0m)
				{
					DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					dataRow7["AccountID"] = value7;
					dataRow7["PayeeID"] = text;
					dataRow7["PayeeType"] = "A";
					dataRow7["JobID"] = value3;
					dataRow7["CostCategoryID"] = value4;
					if (result10 < 0m)
					{
						dataRow7["Debit"] = Math.Abs(result10);
						dataRow7["Credit"] = DBNull.Value;
					}
					else
					{
						dataRow7["Credit"] = result10;
						dataRow7["Debit"] = DBNull.Value;
					}
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7["JVEntryType"] = (byte)10;
					dataRow7["CompanyID"] = value;
					dataRow7["DivisionID"] = value2;
					dataRow7.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow7);
				}
				if (result9 > 0m)
				{
					if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
					{
						throw new CompanyException("Tax details not found for the transaction.");
					}
					DataRow[] array = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
					decimal num4 = default(decimal);
					for (int l = 0; l < array.Length; l++)
					{
						num4 = default(decimal);
						DataRow obj2 = array[l];
						DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
						dataRow7.BeginEdit();
						dataRow7["JournalID"] = 0;
						string text16 = "";
						text16 = obj2["TaxItemID"].ToString();
						string text17 = "";
						textCommand = "SELECT SalesTaxAccountID FROM Tax WHERE  TaxCode = '" + text16.Trim() + "'";
						object obj = ExecuteScalar(textCommand);
						if (obj != null)
						{
							text17 = obj.ToString();
						}
						if (text17 == "")
						{
							throw new CompanyException("AccountID is not set for tax item: " + text16 + ".");
						}
						decimal.TryParse(obj2["TaxAmount"].ToString(), out num4);
						dataRow7["AccountID"] = text17;
						dataRow7["PayeeID"] = text;
						dataRow7["PayeeType"] = "A";
						dataRow7["JobID"] = value3;
						dataRow7["CostCategoryID"] = value4;
						if (flag)
						{
							dataRow7["DebitFC"] = DBNull.Value;
							dataRow7["CreditFC"] = Math.Round(num4, currencyDecimalPoints, MidpointRounding.AwayFromZero);
						}
						else
						{
							dataRow7["Debit"] = DBNull.Value;
							dataRow7["Credit"] = Math.Round(num4, currencyDecimalPoints, MidpointRounding.AwayFromZero);
						}
						dataRow7["Reference"] = dataRow["Reference"];
						dataRow7["JVEntryType"] = (byte)6;
						dataRow7["CompanyID"] = value;
						dataRow7["DivisionID"] = value2;
						dataRow7.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow7);
					}
				}
				if (result3)
				{
					DataRow dataRow9 = transactionData.PaymentTable.Rows[0];
					DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					dataRow7["AccountID"] = dataRow9["AccountID"].ToString();
					dataRow7["PayeeID"] = text;
					dataRow7["PayeeType"] = "A";
					dataRow7["IsARAP"] = false;
					dataRow7["JobID"] = value3;
					dataRow7["CostCategoryID"] = value4;
					if (flag)
					{
						dataRow7["DebitFC"] = dataRow9["Amount"];
						dataRow7["CreditFC"] = DBNull.Value;
					}
					else
					{
						dataRow7["Debit"] = dataRow9["Amount"];
						dataRow7["Credit"] = DBNull.Value;
					}
					if (text6 != "")
					{
						dataRow7["Debit"] = num;
					}
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7["CompanyID"] = value;
					dataRow7["DivisionID"] = value2;
					dataRow7.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow7);
				}
				else
				{
					DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					dataRow7["DueDate"] = dataRow["DueDate"];
					dataRow7["AccountID"] = value6;
					dataRow7["PayeeID"] = text;
					dataRow7["PayeeType"] = "C";
					dataRow7["IsARAP"] = true;
					dataRow7["JobID"] = value3;
					dataRow7["CostCategoryID"] = value4;
					if (flag)
					{
						if (result)
						{
							dataRow7["DebitFC"] = d2 - result15 + d3 + result10;
						}
						else
						{
							dataRow7["DebitFC"] = d2 - result15 + result9 + d3 + result10;
						}
						dataRow7["CreditFC"] = DBNull.Value;
					}
					else
					{
						if (result)
						{
							dataRow7["Debit"] = d2 - result15 + d3 + result10;
						}
						else
						{
							dataRow7["Debit"] = d2 - result15 + result9 + d3 + result10;
						}
						dataRow7["Credit"] = DBNull.Value;
					}
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7["JVEntryType"] = (byte)7;
					dataRow7["CompanyID"] = value;
					dataRow7["DivisionID"] = value2;
					dataRow7.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow7);
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowReturnedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReturned FROM Sales_Invoice_NonInv_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Product", textCommand);
				if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables[0].Rows[0];
					if (dataRow["UnitQuantity"] != DBNull.Value)
					{
						float.TryParse(dataRow["UnitQuantity"].ToString(), out result);
					}
					else
					{
						float.TryParse(dataRow["Quantity"].ToString(), out result);
					}
					float.TryParse(dataRow["QuantityReturned"].ToString(), out result2);
				}
				result2 += quantity;
				textCommand = "UPDATE Sales_Invoice_NonInv_Detail SET QuantityReturned=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public SalesInvoiceNIData GetSalesInvoiceByID(string sysDocID, string voucherID)
		{
			return GetSalesInvoiceByID(sysDocID, voucherID, null);
		}

		internal SalesInvoiceNIData GetSalesInvoiceByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				SalesInvoiceNIData salesInvoiceNIData = new SalesInvoiceNIData();
				string text = "SELECT INV.* FROM Sales_Invoice_NonInv INV  WHERE INV.VoucherID='" + voucherID + "' AND INV.SysDocID='" + sysDocID + "'";
				new SqlCommand(text).Transaction = sqlTransaction;
				FillDataSet(salesInvoiceNIData, "Sales_Invoice_NonInv", text, sqlTransaction);
				if (salesInvoiceNIData == null || salesInvoiceNIData.Tables.Count == 0 || salesInvoiceNIData.Tables["Sales_Invoice_NonInv"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT distinct SID.* ,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID,SID.TaxOption,\r\n                            CASE WHEN ItemType = 5 THEN 'True' ELSE IsTrackLot END AS IsTrackLot,IsTrackSerial,BrandName AS Brand,\r\n                            CON.LotNumber, CON.ReceiptNumber AS ConsignNumber\r\n                            FROM Sales_Invoice_NonInv_Detail SID INNER JOIN Product ON SID.ProductID=Product.ProductID\r\n                            LEFT OUTER JOIN  Product_Lot_Issue_Detail PLS ON PLS.SysDocID = SID.OrderSysDocID AND PLS.VoucherID = SID.OrderVoucherID AND SID.OrderRowIndex = PLS.RowIndex\r\n                            LEFT OUTER JOIN Product_Lot CON ON CON.LotNumber = (SELECT TOP 1 CASE WHEN SourceLotNumber IS NULL THEN LotNumber ELSE SourceLotNumber END FROM Product_Lot_Issue_Detail PLS2 WHERE PLS2.SysDocID = SID.OrderSysDocID AND PLS2.VoucherID = SID.OrderVoucherID AND SID.OrderRowIndex = PLS2.RowIndex)\r\n                            LEFT OUTER JOIN Product_Brand Brand ON Brand.BrandID = Product.BrandID\r\n                        WHERE SID.VoucherID='" + voucherID + "' AND SID.SysDocID='" + sysDocID + "' ORDER BY SID.RowIndex ";
				FillDataSet(salesInvoiceNIData, "Sales_Invoice_NonInv_Detail", text, sqlTransaction);
				text = "SELECT DISTINCT SO.VoucherID, SO.SysDocID from Sales_order SO INNER JOIN Sales_Invoice_NonInv_Detail dnd ON SO.SysDocID=dnd.OrderSysDocID AND SO.VoucherID=dnd.OrderVoucherID\r\n\t\t\t\t\t\tWHERE dnd.VoucherID='" + voucherID + "' AND dnd.SysDocID='" + sysDocID + "'";
				FillDataSet(salesInvoiceNIData, "Sales_Order", text);
				text = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesInvoiceNIData, "Tax_Detail", text);
				return salesInvoiceNIData;
			}
			catch
			{
				throw;
			}
		}

		public SalesInvoiceNIData GetServiceInvoiceByID(string sysDocID, string voucherID)
		{
			return GetServiceInvoiceByID(sysDocID, voucherID, null);
		}

		internal SalesInvoiceNIData GetServiceInvoiceByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				SalesInvoiceNIData salesInvoiceNIData = new SalesInvoiceNIData();
				string text = "SELECT INV.* FROM Sales_Invoice_NonInv INV  WHERE INV.VoucherID='" + voucherID + "' AND INV.SysDocID='" + sysDocID + "'";
				new SqlCommand(text).Transaction = sqlTransaction;
				FillDataSet(salesInvoiceNIData, "Sales_Invoice_NonInv", text, sqlTransaction);
				if (salesInvoiceNIData == null || salesInvoiceNIData.Tables.Count == 0 || salesInvoiceNIData.Tables["Sales_Invoice_NonInv"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT distinct SID.* ,PIC.IncomeID,PIC.IncomeName,PIC.Description as IncomeDescription,SID.TaxOption\r\n                           FROM Sales_Invoice_NonInv_Detail SID INNER JOIN PropertyIncome_Code PIC ON SID.ProductID=PIC.IncomeID\r\n                            WHERE SID.VoucherID='" + voucherID + "' AND SID.SysDocID='" + sysDocID + "' ORDER BY SID.RowIndex ";
				FillDataSet(salesInvoiceNIData, "Sales_Invoice_NonInv_Detail", text, sqlTransaction);
				text = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesInvoiceNIData, "Tax_Detail", text);
				return salesInvoiceNIData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteSalesInvoiceDetailsRows(string sysDocID, string voucherID, bool isDeleteTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				SalesInvoiceNIData salesInvoiceNIData = new SalesInvoiceNIData();
				string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid,ISNULL(IsCash,'False')AS IsCash,ISNULL(IsExport,'False') AS IsExport FROM Sales_Invoice_NonInv_Detail SOD INNER JOIN Sales_Invoice_NonInv SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(salesInvoiceNIData, "Sales_Invoice_NonInv_Detail", textCommand, sqlTransaction);
				if (salesInvoiceNIData.SalesInvoiceDetailTable.Rows.Count == 0)
				{
					return true;
				}
				bool result = false;
				bool.TryParse(salesInvoiceNIData.SalesInvoiceDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(salesInvoiceNIData.SalesInvoiceDetailTable.Rows[0]["IsCash"].ToString(), out result2);
				if (!result)
				{
					string text = "";
					string text2 = "";
					string text3 = "";
					foreach (DataRow row in salesInvoiceNIData.SalesInvoiceDetailTable.Rows)
					{
						text3 = row["ProductID"].ToString();
						text = row["OrderVoucherID"].ToString();
						text2 = row["OrderSysDocID"].ToString();
						int result3 = 0;
						if (!(text == "") && !(text2 == ""))
						{
							int.TryParse(row["OrderRowIndex"].ToString(), out result3);
							float result4 = 0f;
							if (row["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row["UnitQuantity"].ToString(), out result4);
							}
							else
							{
								float.TryParse(row["Quantity"].ToString(), out result4);
							}
							float num = new Products(base.DBConfig).GetReservedQuantity(text3, sqlTransaction) + result4;
							if (num < 0f)
							{
								num = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateReservedQuantity(text3, num, sqlTransaction);
							flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text2, text, result3, -1f * result4, sqlTransaction);
							flag &= new SalesOrder(base.DBConfig).ReOpenOrder(text2, text, sqlTransaction);
						}
					}
				}
				textCommand = "DELETE FROM Sales_Invoice_NonInv_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				return flag & new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidSalesInvoice(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidSalesInvoice(sysDocID, voucherID, isVoid, null);
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

		private bool VoidSalesInvoice(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				bool flag2 = true;
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				else
				{
					flag2 = false;
				}
				bool flag3 = false;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Sales_Invoice_NonInv", "IsExport", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag3 = bool.Parse(fieldValue.ToString());
				}
				string text = "";
				text = ((!flag3) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				if (text != "")
				{
					int.Parse(text.ToString());
				}
				SalesInvoiceNIData salesInvoiceNIData = new SalesInvoiceNIData();
				string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid,ISNULL(IsCash,'False') AS IsCash FROM Sales_Invoice_NonInv_Detail SOD INNER JOIN Sales_Invoice_NonInv SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(salesInvoiceNIData, "Sales_Invoice_NonInv_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(salesInvoiceNIData.SalesInvoiceDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(salesInvoiceNIData.SalesInvoiceDetailTable.Rows[0]["IsCash"].ToString(), out result2);
				if (InvoiceHasShippedQuantity(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to modify. Some of the items in this invoice has been already shipped.");
				}
				if (result == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				string text2 = "";
				string text3 = "";
				string text4 = "";
				foreach (DataRow row in salesInvoiceNIData.SalesInvoiceDetailTable.Rows)
				{
					text4 = row["ProductID"].ToString();
					text2 = row["OrderVoucherID"].ToString();
					text3 = row["OrderSysDocID"].ToString();
					int result3 = 0;
					if (!(text2 == "") && !(text3 == ""))
					{
						int.TryParse(row["OrderRowIndex"].ToString(), out result3);
						float result4 = 0f;
						if (row["UnitQuantity"] != DBNull.Value)
						{
							float.TryParse(row["UnitQuantity"].ToString(), out result4);
						}
						else
						{
							float.TryParse(row["Quantity"].ToString(), out result4);
						}
						float num = new Products(base.DBConfig).GetReservedQuantity(text4, sqlTransaction) + result4;
						if (num < 0f)
						{
							num = 0f;
						}
						flag &= new Products(base.DBConfig).UpdateReservedQuantity(text4, num, sqlTransaction);
						if (new SalesOrder(base.DBConfig).IsPOOrder(text3, text2, sqlTransaction))
						{
							flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text3, text2, result3, -1f * result4, sqlTransaction);
							flag &= new SalesOrder(base.DBConfig).ReOpenOrder(text3, text2, sqlTransaction);
						}
						flag &= new SalesProformaInvoice(base.DBConfig).ReOpenOrder(text3, text2, sqlTransaction);
					}
				}
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				textCommand = "UPDATE Sales_Invoice_NonInv SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				if (!flag || !flag2)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Sales Invoice-Non Inv", voucherID, sysDocID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeleteSalesInvoice(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Sales_Invoice_NonInv", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidSalesInvoice(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeleteSalesInvoiceDetailsRows(sysDocID, voucherID, isDeleteTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Sales_Invoice_NonInv WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Sales Invoice-Non Inv", voucherID, sysDocID, activityType, sqlTransaction);
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

		internal bool CloseShippedInvoice(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM Sales_Invoice_NonInv_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                                AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Sales_Invoice_NonInv_Detail SOD2 WHERE SOD.SysDocID=SOD2.SysDocID AND SOD.VoucherID=SOD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityShipped,0) ) FROM Sales_Invoice_NonInv_Detail SOD3 WHERE SOD.SysDocID=SOD3.SysDocID AND SOD.VoucherID=SOD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Sales_Invoice_NonInv SET IsDelivered = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool UpdateRowShippedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityShipped FROM Sales_Invoice_NonInv_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				FillDataSet(dataSet, "Product", textCommand);
				if (dataSet != null && dataSet.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow = dataSet.Tables[0].Rows[0];
					if (dataRow["UnitQuantity"] != DBNull.Value)
					{
						float.TryParse(dataRow["UnitQuantity"].ToString(), out result);
					}
					else
					{
						float.TryParse(dataRow["Quantity"].ToString(), out result);
					}
					float.TryParse(dataRow["QuantityShipped"].ToString(), out result2);
				}
				result2 += quantity;
				textCommand = "UPDATE Sales_Invoice_NonInv_Detail SET QuantityShipped=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price, string LocationID)
		{
			decimal result = default(decimal);
			string str = "SELECT ISNULL(PPD.MinPrice,P.MinPrice)AS MinPrice FROM Product_PriceList_Detail PPD  LEFT JOIN Product P ON \r\n                        PPD.ProductID = P.ProductID WHERE PPD.ProductID='" + productID + "'";
			str = str + " AND (PPD.LocationID = '" + LocationID + "' OR ISNULL(PPD.LocationID,'')='')";
			DataSet dataSet = new DataSet();
			object obj = ExecuteScalar(str);
			if (obj != null && obj.ToString() != "")
			{
				decimal.TryParse(obj.ToString(), out result);
				if (price < result)
				{
					return true;
				}
				return false;
			}
			if (obj == null || obj.ToString() == "")
			{
				str = "SELECT MinPrice FROM Product WHERE ProductID='" + productID + "'";
				obj = ExecuteScalar(str);
			}
			if (obj != null && obj.ToString() != "")
			{
				decimal.TryParse(obj.ToString(), out result);
			}
			if (unitID != "")
			{
				str = "SELECT FactorType,Factor FROM Product_Unit WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "'";
				dataSet = new DataSet();
				FillDataSet(dataSet, "Unit", str);
				if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
				{
					string a = dataSet.Tables[0].Rows[0]["FactorType"].ToString();
					decimal num = decimal.Parse(dataSet.Tables[0].Rows[0]["Factor"].ToString());
					if (a == "M")
					{
						result /= num;
					}
					else
					{
						result *= num;
					}
				}
			}
			if (currencyID != "")
			{
				result /= currencyRate;
			}
			if (price < result)
			{
				return true;
			}
			return false;
		}

		public bool IsBelowMinAllowedPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price, string LocationID)
		{
			decimal result = default(decimal);
			string str = "SELECT ISNULL(PPD.UnitPrice3,P.UnitPrice3)AS MinPrice FROM Product_PriceList_Detail PPD  LEFT JOIN Product P ON \r\n                        PPD.ProductID = P.ProductID WHERE PPD.ProductID='" + productID + "'";
			str = str + " AND PPD.LocationID = '" + LocationID + "'";
			DataSet dataSet = new DataSet();
			object obj = ExecuteScalar(str);
			if (obj == null || obj.ToString() == "")
			{
				str = "SELECT UnitPrice3 FROM Product WHERE ProductID='" + productID + "'";
				obj = ExecuteScalar(str);
			}
			if (obj != null && obj.ToString() != "")
			{
				decimal.TryParse(obj.ToString(), out result);
			}
			if (unitID != "")
			{
				str = "SELECT FactorType,Factor FROM Product_Unit WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "'";
				dataSet = new DataSet();
				FillDataSet(dataSet, "Unit", str);
				if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
				{
					string a = dataSet.Tables[0].Rows[0]["FactorType"].ToString();
					decimal num = decimal.Parse(dataSet.Tables[0].Rows[0]["Factor"].ToString());
					if (a == "M")
					{
						result /= num;
					}
					else
					{
						result *= num;
					}
				}
			}
			if (currencyID != "")
			{
				result /= currencyRate;
			}
			if (price < result)
			{
				return true;
			}
			return false;
		}

		public DataSet GetProductPriceList(string productID, string customerID, string LocationID, string UnitID)
		{
			DataSet dataSet = new DataSet();
			DataSet dataSet2 = new DataSet();
			DataSet dataSet3 = new DataSet();
			string str = "SELECT '' AS UnitID,ISNULL(PPD.UnitPrice1,P.UnitPrice1)AS UnitPrice1,ISNULL(PPD.UnitPrice2,P.UnitPrice2)AS UnitPrice2,ISNULL(PPD.UnitPrice3,P.UnitPrice3)AS \r\n                        UnitPrice3,ISNULL(PPD.MinPrice,P.MinPrice)AS MinPrice FROM Product_PriceList_Detail PPD  LEFT JOIN Product P ON \r\n                        PPD.ProductID=P.ProductID WHERE PPD.ProductID='" + productID + "'";
			str = str + " AND PPD.LocationID = '" + LocationID + "'";
			if (UnitID != "")
			{
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product_PriceList_Detail", "UnitID", "ProductID", productID, "UnitID", UnitID, null);
				UnitID = ((fieldValue == null) ? "" : fieldValue.ToString());
			}
			if (UnitID != "")
			{
				str = str + " AND PPD.UnitID = '" + UnitID + "'";
			}
			FillDataSet(dataSet2, "PriceList", str);
			if (dataSet2.Tables[0].Rows.Count > 0)
			{
				dataSet2.Tables[0].Rows[0][0] = UnitID;
			}
			if (dataSet2.Tables[0].Rows.Count == 0 || UnitID == "" || customerID == "")
			{
				dataSet2.Tables.Clear();
				str = "SELECT UnitID,UnitPrice1,UnitPrice2,UnitPrice3,MinPrice FROM Product WHERE ProductID='" + productID + "'";
				FillDataSet(dataSet2, "PriceList", str);
			}
			str = "SELECT ItemPrice1Name,ItemPrice2Name,ItemPrice3Name\r\n\t\t\t\t\t\t   FROM Company WHERE CompanyID=1";
			FillDataSet(dataSet3, "PriceName", str);
			dataSet.Tables.Add();
			dataSet.Tables[0].Columns.Add("PriceID");
			dataSet.Tables[0].Columns.Add("Price Name");
			dataSet.Tables[0].Columns.Add("Amount", typeof(decimal));
			dataSet.Tables[0].Columns.Add("UnitID");
			if (dataSet2.Tables[0].Rows[0]["UnitPrice1"] != DBNull.Value && dataSet2.Tables[0].Rows[0]["UnitPrice1"].ToString() != "")
			{
				dataSet.Tables[0].Rows.Add("UnitPrice1", dataSet3.Tables[0].Rows[0]["ItemPrice1Name"].ToString(), dataSet2.Tables[0].Rows[0]["UnitPrice1"].ToString());
			}
			if (dataSet2.Tables[0].Rows[0]["UnitPrice2"] != DBNull.Value && dataSet2.Tables[0].Rows[0]["UnitPrice2"].ToString() != "")
			{
				dataSet.Tables[0].Rows.Add("UnitPrice2", dataSet3.Tables[0].Rows[0]["ItemPrice2Name"].ToString(), dataSet2.Tables[0].Rows[0]["UnitPrice2"].ToString());
			}
			if (dataSet2.Tables[0].Rows[0]["UnitPrice3"] != DBNull.Value && dataSet2.Tables[0].Rows[0]["UnitPrice3"].ToString() != "")
			{
				dataSet.Tables[0].Rows.Add("UnitPrice3", dataSet3.Tables[0].Rows[0]["ItemPrice3Name"].ToString(), dataSet2.Tables[0].Rows[0]["UnitPrice3"].ToString());
			}
			if (dataSet2.Tables[0].Rows[0]["MinPrice"] != DBNull.Value && dataSet2.Tables[0].Rows[0]["MinPrice"].ToString() != "")
			{
				dataSet.Tables[0].Rows.Add("MinPrice", "Min Price", dataSet2.Tables[0].Rows[0]["MinPrice"].ToString());
			}
			if (dataSet2.Tables[0].Rows[0]["UnitID"] != DBNull.Value && dataSet2.Tables[0].Rows[0]["UnitID"].ToString() != "")
			{
				dataSet.Tables[0].Rows[0]["UnitID"] = dataSet2.Tables[0].Rows[0]["UnitID"].ToString();
			}
			if (dataSet.Tables[0].Rows.Count > 0 && dataSet.Tables[0].Rows[0]["Price Name"].ToString() == "")
			{
				dataSet.Tables[0].Rows[0]["Price Name"] = "Price";
			}
			if (dataSet.Tables[0].Rows.Count > 1 && dataSet.Tables[0].Rows[1]["Price Name"].ToString() == "")
			{
				dataSet.Tables[0].Rows[1]["Price Name"] = "Price";
			}
			if (dataSet.Tables[0].Rows.Count > 2 && dataSet.Tables[0].Rows[2]["Price Name"].ToString() == "")
			{
				dataSet.Tables[0].Rows[2]["Price Name"] = "Price";
			}
			return dataSet;
		}

		public bool IsBelowAverageCost(string productID, string unitID, string currencyID, decimal currencyRate, decimal price)
		{
			decimal result = default(decimal);
			string exp = "SELECT TOP 1 Averagecost FROM Inventory_Transactions  WHERE ProductID='" + productID + "' ORDER BY TransactionID DESC";
			DataSet dataSet = new DataSet();
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "")
			{
				decimal.TryParse(obj.ToString(), out result);
			}
			if (unitID != "")
			{
				exp = "SELECT FactorType,Factor FROM Product_Unit WHERE ProductID='" + productID + "' AND UnitID='" + unitID + "'";
				dataSet = new DataSet();
				FillDataSet(dataSet, "Unit", exp);
				if (dataSet.Tables.Count > 0 && dataSet.Tables[0].Rows.Count > 0)
				{
					string a = dataSet.Tables[0].Rows[0]["FactorType"].ToString();
					decimal num = decimal.Parse(dataSet.Tables[0].Rows[0]["Factor"].ToString());
					if (a == "M")
					{
						result /= num;
					}
					else
					{
						result *= num;
					}
				}
			}
			if (currencyID != "")
			{
				result /= currencyRate;
			}
			if (price <= result)
			{
				return true;
			}
			return false;
		}

		public DataSet GetSalesInvoiceToPrint(string sysDocID, string voucherID, bool mergeMatrixItems, bool showLotDetail, NonInventoryInvoiceType invoiceType)
		{
			return GetSalesInvoiceToPrint(sysDocID, new string[1]
			{
				voucherID
			}, mergeMatrixItems, showLotDetail, invoiceType);
		}

		public DataSet GetSalesInvoiceToPrint(string sysDocID, string voucherID, bool showLotDetail, NonInventoryInvoiceType invoiceType)
		{
			return GetSalesInvoiceToPrint(sysDocID, new string[1]
			{
				voucherID
			}, mergeMatrixItems: false, showLotDetail, invoiceType);
		}

		public DataSet GetSalesInvoiceToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems, bool showLotDetail, NonInventoryInvoiceType invoiceType)
		{
			try
			{
				string text = "";
				for (int i = 0; i < voucherID.Length; i++)
				{
					text = "'" + voucherID[i] + "'";
					if (i < voucherID.Length - 1)
					{
						text += ",";
					}
				}
				DataSet dataSet = new DataSet();
				switch (invoiceType)
				{
				case NonInventoryInvoiceType.SalesInvoice:
				{
					string cmdText2 = "SELECT  DISTINCT   SI.SysDocID,SI.VoucherID,SD.LocationID AS SysLocID,SI.DueDate, L.LocationName AS SysLocationName, SI.CustomerID,Customer.CreditAmount,CustomerName,Customer.TaxIDNumber as CTaxIDNo, SI.PONumber,CA.ContactName,CustomerAddress,ISNULL(SI.IsWeightInvoice,'False') AS IsWeightInvoice, SP.FullName AS [Sales Person], (select ISNULL(ISNULL(sum(SE.Amount),sum(SE.AmountFC)),0)  FROM Sales_Invoice_Expense SE  \r\n                                WHERE SE.InvoiceSysDocID = SI.SysDocID AND SE.InvoiceVoucherID = SI.VoucherID GROUP BY SE.InvoiceVoucherID) [Total Expense], TransactionDate,'' AS 'GTransactionDate', \r\n                                SUBSTRING((SELECT DISTINCT ' ' + D.VoucherID + ' / ' +  convert(varchar,  ( D.TransactionDate) , 106) + ', ' \r\n                                FROM Sales_Invoice_NonInv_Detail S  INNER JOIN Delivery_Note  D ON S.OrderSysDocID = D.SysDocID AND S.OrderVoucherID = D.VoucherID \r\n                                WHERE S.VoucherID = SI.VoucherID AND S.SysDocID = SI.SysDocID FOR XML PATH('')),2,20000) AS  DElvery_detials,\r\n                                SUBSTRING((SELECT DISTINCT  ' ' + convert(varchar,  ( D.TransactionDate) , 106) + ', ' \r\n                                FROM Sales_Invoice_NonInv_Detail S  INNER JOIN Delivery_Note  D ON S.OrderSysDocID = D.SysDocID AND S.OrderVoucherID = D.VoucherID \r\n                                WHERE S.VoucherID = SI.VoucherID AND S.SysDocID = SI.SysDocID FOR XML PATH('')),2,20000) AS  DeliveryDate,\r\n                                (SELECT DISTINCT  D.TransactionDate \r\n                                FROM Sales_Invoice_NonInv_Detail S  INNER JOIN SalesProforma_Invoice  D ON S.OrderSysDocID = D.SysDocID AND S.OrderVoucherID = D.VoucherID \r\n                                WHERE S.VoucherID = SI.VoucherID AND S.SysDocID = SI.SysDocID ) AS  ProformaDate,\r\n                                SUBSTRING((SELECT DISTINCT ' ' + D.VoucherID +  ', ' \r\n                                FROM Sales_Invoice_NonInv_Detail S  INNER JOIN Delivery_Note  D ON S.OrderSysDocID = D.SysDocID AND S.OrderVoucherID = D.VoucherID \r\n                                WHERE S.VoucherID = SI.VoucherID  AND S.SysDocID = SI.SysDocID FOR XML PATH('')),2,20000) AS  Delivery_No,\r\n                                (SELECT TOP 1 V.RegistrationNumber FROM Delivery_Note DL LEFT JOIN Vehicle V ON DL.VehicleID=V.VehicleID INNER JOIN Sales_Invoice_NonInv_Detail SLD ON SLD.OrderSysDocID = DL.SysDocID AND SLD.OrderVoucherID = DL.VoucherID\r\n                                WHERE SLD.VoucherID = SI.VoucherID AND SLD.SysDocID=SI.SysDocID )  AS [Vehicle No],\r\n                                (SELECT TOP 1 D.DriverName  FROM Delivery_Note DL LEFT JOIN Driver D ON DL.DriverID=D.DriverID INNER JOIN Sales_Invoice_NonInv_Detail SLD ON SLD.OrderSysDocID = DL.SysDocID AND SLD.OrderVoucherID = DL.VoucherID\r\n                                WHERE SLD.VoucherID = SI.VoucherID AND SLD.SysDocID=SI.SysDocID ) AS [Driver Name],\r\n                                IsCash,SI.SalesPersonID,RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                (SELECT Top 1 Total FROM Sales_Order WHERE VoucherID = SI.Reference) AS [Order Amount],\r\n                                (SELECT Top 1 TransactionDate FROM Sales_Order WHERE VoucherID =SI.Reference) AS [Order Date],(SELECT  DISTINCT TOP 1 SO.TransactionDate FROM Sales_Invoice_NonInv_Detail SND \r\n                                LEFT JOIN Delivery_Note_Detail DND ON SND.OrderSysDocID=DND.SysDocID AND SND.OrderVoucherID=DND.VoucherID\r\n                                LEFT OUTER JOIN  Sales_Order SO ON DND.SourceSysDocID=SO.SysDocID AND DND.SourceVoucherID=SO.VoucherID\r\n                                WHERE SND.SysDocID=SI.SysDocID AND SND.VoucherID=SI.VoucherID) AS [SalesOrderDate],\r\n                                (SELECT  DISTINCT TOP 1 SO.TransactionDate FROM Sales_Invoice_NonInv_Detail SND \r\n                                \r\n                                LEFT OUTER JOIN  Sales_Order SO ON SND.OrderSysDocID=SO.SysDocID AND SND.OrderVoucherID=SO.VoucherID\r\n                                WHERE SND.SysDocID=SI.SysDocID AND SND.VoucherID=SI.VoucherID) AS [SODate],\r\n                                (SELECT  DISTINCT TOP 1 SO.Reference FROM Sales_Invoice_NonInv_Detail SND \r\n                                LEFT JOIN Delivery_Note_Detail DND ON SND.OrderSysDocID=DND.SysDocID AND SND.OrderVoucherID=DND.VoucherID\r\n                                LEFT OUTER JOIN  Sales_Order SO ON DND.SourceSysDocID=SO.SysDocID AND DND.SourceVoucherID=SO.VoucherID\r\n                                WHERE SND.SysDocID=SI.SysDocID AND SND.VoucherID=SI.VoucherID) AS [Quotation No],\r\n                                (SELECT  DISTINCT TOP 1 SO.SysDocID+'-'+ SO.VoucherID FROM Sales_Invoice_NonInv_Detail SND \r\n                                LEFT JOIN Delivery_Note_Detail DND ON SND.OrderSysDocID=DND.SysDocID AND SND.OrderVoucherID=DND.VoucherID\r\n                                LEFT OUTER JOIN  Sales_Order SO ON DND.SourceSysDocID=SO.SysDocID AND DND.SourceVoucherID=SO.VoucherID\r\n                                WHERE SND.SysDocID=SI.SysDocID AND SND.VoucherID=SI.VoucherID) AS [SalesOrder No],\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                                SI.TermID,TermName,IsVoid,SI.Reference,ISNULL(SI.DiscountFC,SI.Discount) AS Discount,\r\n                                ISNULL(ISNULL(TaxAmountFC,TaxAmount) ,0) AS Tax,ISNULL(RoundOff,0) as RoundOff,ISNULL(TotalFC,Total) AS Total,ISNULL(TotalFC,Total) - ISNULL(ISNULL(SI.DiscountFC,SI.Discount),0) AS GrandTotal,SI.PONumber,SI.Note, \r\n                                Reference2, ISNULL(CA.Phone1, CA.Phone2) CustomerPhoneNo, CA.Email,CA.Fax, SI.SalespersonID,CA.Mobile,CA.PostalCode,\r\n                                SI.DateCreated,SI.DateUpdated,SI.CreatedBy,SI.UpdatedBy,J.JobName,JC.CostCategoryName,\r\n\r\n                                ISNULL((SELECT  SUM(ISNULL(Debit,0)- ISNULL(Credit,0))  FROM ARJournal \r\n                                WHERE CustomerID = SI.CustomerID and ISNULL(isvoid,'False') = 'False' AND ISNULL(IsPDCRow,'False') = 'False'),0) +\r\n                                ISNULL((SELECT  SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) AS Balance FROM ARJournal  WHERE CustomerID IN \r\n                                (Select CustomerID FROM Customer WHERE ParentCustomerID = SI.CustomerID)  and ISNULL(isvoid,'False') = 'False' AND ISNULL(IsPDCRow,'False') = 'False'),0) AS Balance,\r\n                                ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) AS Amount FROM Cheque_Received ChqRec\r\n                                WHERE Status IN (1,3,4,8) AND ISNULL(IsVoid,'False')='False' AND PayeeType='C' AND PayeeID = SI.CustomerID),0) + \r\n                                ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) AS Amount FROM Cheque_Received ChqRec\r\n                                WHERE Status IN (1,3,4,8) AND ISNULL(IsVoid,'False')='False' AND PayeeType='C' AND PayeeID IN (Select CustomerID FROM Customer WHERE ParentCustomerID = SI.CustomerID)),0)  AS PDCAmount,SI.DueDate,SI.PaymentMethodType,SI.ShipToAddress,CA.Comment, CA.AddressName,SI.PriceIncludeTax,\r\n                                SI.DriverID, D.DriverName, D.Note, SI.VehicleID, V.VehicleName, Customer.BankName,Customer.BankBranch,Customer.BankAccountNumber\r\n                                FROM  Sales_Invoice_NonInv SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT OUTER JOIN Salesperson SP ON SP.SalespersonID=SI.SalespersonID\r\n                                LEFT JOIN Job J ON J.JobID=SI.JobID\r\n                                LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=SI.CostCategoryID\r\n                                LEFT JOIN System_Document SD On SD.SysDocID=SI.SysDocID\r\n                                LEFT JOIN Location L On SD.LocationID=L.LocationID\r\n                                Left outer join Driver D on SI.DriverID=D.DriverID\r\n\t\t\t\t\t\t\t\tLeft Outer JOIN Vehicle V ON SI.VehicleID=V.VehicleID\r\n                                WHERE SI.SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
					SqlCommand sqlCommand2 = new SqlCommand(cmdText2);
					FillDataSet(dataSet, "Sales_Invoice_NonInv", sqlCommand2);
					DateTime dateTime = Convert.ToDateTime(dataSet.Tables["Sales_Invoice_NonInv"].Rows[0]["TransactionDate"].ToString());
					dataSet.Tables["Sales_Invoice_NonInv"].Rows[0]["GTransactionDate"] = dateTime;
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Sales_Invoice_NonInv"].Rows.Count == 0)
					{
						return null;
					}
					cmdText2 = ((!mergeMatrixItems) ? ("SELECT DISTINCT SINVD.SysDocID, SINVD.VoucherID, SINVD.ProductID,SINVD.Remarks,SINVD.TaxAmount, SINVD.TaxGroupID,TG.TaxGroupName, P.CategoryID,PB.BrandName,P.BrandID, PPC.ProductParentID,P.ItemType,\r\n                            PP.Description AS ParentDescription, P.Description2, convert(nvarchar(max),P.Note) AS Note,\r\n                       \r\n                            (SELECT TOP 1 PLD.CustomerProductID FROM Price_List_Detail PLD INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID \r\n                             WHERE ISNULL(PL.Inactive, 'True') = 'False' AND PL.CustomerID = SI.CustomerID AND  PLD.ProductID = SINVD.ProductID) CustomerProductID, \r\n                            P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,C.CountryName,PC.CategoryName,\r\n                            (SELECT TOP 1 CASE WHEN PU.FactorType='M' \r\n                            THEN ('1 '+'  '+ P.UnitID +'= '+CAST (PU.Factor AS varchar)+' '+PU.UnitID)  \r\n                            ELSE ('1'+'  '+PU.UnitID +'= '+ CAST(PU.Factor AS varchar)+' '+P.UnitID) END \r\n                            AS PACKING from Product_Unit PU WHERE P.ProductID=PU.ProductID )  AS [Packing]\r\n                            ,P.UnitID AS MainUnit,\r\n                            (SELECT TOP 1  PU.Factor from Product_Unit PU WHERE P.ProductID=PU.ProductID ) AS Factor,(SELECT TOP 1  PU.UnitID from Product_Unit PU WHERE P.ProductID=PU.ProductID ) AS SubUnit,P.Size,\r\n                            (SELECT CASE WHEN FactorType='D' THEN ISNULL(UnitQuantity,SINVD.Quantity)*Factor ELSE ISNULL(UnitQuantity,SINVD.Quantity)/Factor END \r\n                            FROM Product_Unit PU WHERE PU.UnitID=SINVD.UnitID AND PU.ProductID=SINVD.ProductID ) AS Weight, \r\n                            SINVD.Description,ISNULL(SINVD.UnitQuantity, SINVD.Quantity) - ISNULL(FOCQuantity, 0) AS Quantity, ISNULL(SINVD.UnitPriceFC,SINVD.UnitPrice) AS UnitPrice, SINVD.FOCQuantity, SINVD.ConsignmentNo,\r\n                            (ISNULL(SINVD.UnitQuantity,SINVD.Quantity) - ISNULL(FOCQuantity, 0)) * ISNULL(SINVD.UnitPriceFC,SINVD.UnitPrice) AS Total,SINVD.UnitID,LocationID, \r\n                            SINVD.WeightQuantity, SINVD.WeightPrice, SINVD.RowIndex,ISNULL(SINVD.OrderSysdocID,SINVD.SysDocID) as OrderSysdocID,\r\n                            ISNULL(SINVD.OrderVoucherID,SINVD.VoucherID) as OrderVoucherID,J.JobName,JC.CostCategoryName, \r\n                            (select TransactionDate from Delivery_Note where SysDocID=SINVD.OrderSysDocID AND VoucherID=SINVD.OrderVoucherID) AS  DeliveryDate, SINVD.SpecificationID, SpecificationName,(SELECT TOP 1 PLD.Description  FROM Price_List_Detail PLD INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID \r\n                             WHERE ISNULL(PL.Inactive, 'True') = 'False' AND PL.CustomerID = SI.CustomerID AND  PLD.ProductID = SINVD.ProductID) CustomerProductDesc, \r\n\r\n                            (SELECT TOP 1 PLD.Remarks  FROM Price_List_Detail PLD INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID \r\n                             WHERE ISNULL(PL.Inactive, 'True') = 'False' AND PL.CustomerID = SI.CustomerID AND  PLD.ProductID = SINVD.ProductID) remarks, P.UPC\r\n                            FROM   Sales_Invoice_NonInv_Detail SINVD INNER JOIN Sales_Invoice_NonInv SI ON SINVD.SysDocID = SI.SysDocID AND SINVD.VoucherID = SI.VoucherID\r\n                            INNER JOIN Product P ON P.ProductID = SINVD.ProductID\r\n                            LEFT OUTER JOIN Product_Parent_Components PPC ON SINVD.ProductID=PPC.ProductID\r\n                            LEFT OUTER JOIN Product_Parent PP ON PP.ProductParentID = PPC.ProductParentID\r\n                            LEFT OUTER JOIN Product_Category PC ON PC.CategoryID=P.CategoryID\r\n                            LEFT OUTER JOIN Country C ON P.Origin=C.CountryID \r\n                            LEFT OUTER JOIN Product_Brand PB ON P.BrandID=PB.BrandID\r\n                            LEFT JOIN Job J ON J.JobID=SINVD.JobID\r\n                            LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=SINVD.CostCategoryID\r\n                            LEFT JOIn Tax_Group TG ON SINVD.TaxGroupID=TG.TaxGroupID\r\n\t\t\t\t\t\t\tLEFT JOIN Product_Specification PS ON PS.SpecificationID=SINVD.SpecificationID\r\n                            WHERE SINVD.SysDocID='" + sysDocID + "' AND SINVD.VoucherID IN (" + text + ")  ORDER BY SINVD.RowIndex") : ("SELECT SysDocID,VoucherID, ISNULL(PPC.ProductParentID,SINVD.ProductID) AS ProductID,ISNULL(PP.Description,SINVD.Description) AS Description, \r\n                            SUM(ISNULL(UnitQuantity, SINVD.Quantity)) -  SUM(ISNULL(FOCQuantity, 0)) AS Quantity,SUM(ISNULL(UnitQuantity, SINVD.Quantity)) AS DeliveredQty,\r\n                            \r\n                            WeightQuantity,WeightPrice, ISNULL(UnitPriceFC,UnitPrice) AS UnitPrice, SINVD.FOCQuantity, SINVD.ConsignmentNo,\r\n                            (SUM(ISNULL(UnitQuantity,SINVD.Quantity)) -  SUM(ISNULL(FOCQuantity, 0))) * ISNULL(UnitPriceFC,UnitPrice) AS Total, SINVD.UnitID,LocationID,P.Size, RowIndex\r\n                            FROM   Sales_Invoice_NonInv_Detail SINVD\r\n\t\t\t\t\t\t    INNER JOIN Product P ON SINVD.ProductID = P.ProductID\r\n\t\t\t\t\t\t    LEFT OUTER JOIN Product_Parent_Components PPC ON SINVD.ProductID= PPC.ProductID AND P.MatrixParentID = PPC.ProductParentID\r\n\t\t\t\t\t\t    LEFT OUTER JOIN Product_Parent PP ON PP.ProductParentID = PPC.ProductParentID\r\n                            WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")\r\n                            AND PP.IsInactive <> 1\r\n\t\t\t\t\t\t    GROUP BY SysDocID,RowIndex,VoucherID,PPC.ProductParentID,ISNULL(UnitPriceFC,SINVD.UnitPrice),ISNULL(PPC.ProductParentID,SINVD.ProductID),\r\n                            ISNULL(PP.Description,SINVD.Description),SINVD.UnitID,LocationID,SINVD.WeightQuantity,SINVD.WeightPrice, FOCQuantity, ConsignmentNo  \r\n                            ORDER BY RowIndex"));
					FillDataSet(dataSet, "Sales_Invoice_NonInv_Detail", cmdText2);
					dataSet.Relations.Add("CustomerInvoice", new DataColumn[2]
					{
						dataSet.Tables["Sales_Invoice_NonInv"].Columns["SysDocID"],
						dataSet.Tables["Sales_Invoice_NonInv"].Columns["VoucherID"]
					}, new DataColumn[2]
					{
						dataSet.Tables["Sales_Invoice_NonInv_Detail"].Columns["SysDocID"],
						dataSet.Tables["Sales_Invoice_NonInv_Detail"].Columns["VoucherID"]
					}, createConstraints: false);
					decimal d2 = default(decimal);
					dataSet.Tables["Sales_Invoice_NonInv"].Columns.Add("TotalInWords", typeof(string));
					foreach (DataRow row in dataSet.Tables["Sales_Invoice_NonInv"].Rows)
					{
						decimal result4 = default(decimal);
						decimal result5 = default(decimal);
						decimal result6 = default(decimal);
						decimal num2 = default(decimal);
						decimal.TryParse(row["Total"].ToString(), out result4);
						decimal.TryParse(row["Discount"].ToString(), out result5);
						decimal.TryParse(row["Tax"].ToString(), out result6);
						num2 = decimal.Parse(row["RoundOff"].ToString(), NumberStyles.Any);
						row["TotalInWords"] = NumToWord.GetNumInWords(decimalPoints: new CompanyInformations(base.DBConfig).CurrencyDecimalPoints, amount: result4 + d2 - result5 + result6 + num2);
					}
					if (dataSet != null && dataSet.Tables["Sales_Invoice_NonInv_Detail"].Rows.Count > 0)
					{
						decimal result7 = default(decimal);
						decimal result8 = default(decimal);
						int currencyDecimalPoints3 = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
						decimal.TryParse(dataSet.Tables["Sales_Invoice_NonInv"].Rows[0]["Discount"].ToString(), out result7);
						decimal.TryParse(dataSet.Tables["Sales_Invoice_NonInv_Detail"].Rows[0]["Total"].ToString(), out result8);
						decimal d3 = result7 / result8 * 100m / 100m;
						dataSet.Tables["Sales_Invoice_NonInv_Detail"].Columns.Add("TotalWithDiscount");
						dataSet.Tables["Sales_Invoice_NonInv_Detail"].Columns.Add("DistributeDiscount");
						foreach (DataRow row2 in dataSet.Tables["Sales_Invoice_NonInv_Detail"].Rows)
						{
							decimal result9 = default(decimal);
							decimal result10 = default(decimal);
							decimal result11 = default(decimal);
							decimal.TryParse(row2["UnitPrice"].ToString(), out result9);
							decimal.TryParse(row2["Quantity"].ToString(), out result10);
							decimal.TryParse(row2["TaxAmount"].ToString(), out result11);
							decimal num3 = Math.Round(result10 * result9 - d3 * result9 * result10 + result11, currencyDecimalPoints3);
							row2["DistributeDiscount"] = Math.Round(d3 * result9 * result10, currencyDecimalPoints3);
							row2["TotalWithDiscount"] = num3;
						}
					}
					break;
				}
				case NonInventoryInvoiceType.PropertySalesInvoice:
				{
					string cmdText = "SELECT INV.SysDocID,VoucherID,INV.CustomerID [Customer Code],CustomerName [Customer Name],P.PropertyName [Property],PU.PropertyUnitName [PropertyUnit],PA.PropertyAgentName  [Agent],CustomerAddress [Address],TransactionDate [Invoice Date], DueDate AS [Due Date],INV.Note,\r\n\t\t\t\t\t\t\tINV.TermID [Term],INV.Reference AS Ref1,INV.Reference2 AS Ref2,ShippingAddressID,BillingAddressID,CustomerAddress,ShipToAddress,INV.CurrencyID,INV.CurrencyRate,\r\n                            CASE ISNULL(IsCash,'False') WHEN 'True' THEN 'Cash' ELSE 'Credit' END AS [Type],Total - ISNULL(Discount,0) AS [Total],TotalFC,INV.TaxAmount [Tax],TaxAmountFC,Discount,DiscountFC,RoundOff\r\n                            ,ISNULL(CA.Phone1, CA.Phone2) CustomerPhoneNo, CA.Email,CA.Fax,CA.Mobile,CA.PostalCode FROM   Sales_Invoice_NonInv INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID\r\n                            LEFT JOIN Job J ON INV.JobID=J.JobID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Salesperson SP ON SP.SalespersonID = INV.SalespersonID\r\n                            LEFT JOIN Property P ON P.PropertyID=INV.PropertyID\r\n                            LEFT JOIN Property_Unit PU ON PU.PropertyUnitID=INV.PropertyUnitID\r\n                            LEFT JOIN Property_Agent PA ON PA.PropertyAgentID=INV.AgentID \r\n                            LEFT OUTER JOIN Customer_Address CA ON  CA.CustomerID=INV.CustomerID\r\n                            WHERE INV.SysDocID='" + sysDocID + "' AND INV.VoucherID IN (" + text + ")";
					SqlCommand sqlCommand = new SqlCommand(cmdText);
					FillDataSet(dataSet, "Property_Service_Invoice", sqlCommand);
					cmdText = "SELECT distinct SID.SysDocID,SID.VoucherID,SID.ProductID as IncomeID,SID.Description,SID.UnitPrice,SID.TaxOption,SID.TaxGroupID,SID.TaxAmount,SID.TaxPercentage,SID.Amount,SID.AmountFC,SID.Discount,SID.Remarks,SID.RowIndex,PIC.IncomeID,PIC.IncomeName,PIC.Description as IncomeDescription,SID.TaxOption\r\n                           FROM Sales_Invoice_NonInv_Detail SID INNER JOIN PropertyIncome_Code PIC ON SID.ProductID=PIC.IncomeID\r\n                            WHERE SID.VoucherID IN (" + text + ") AND SID.SysDocID='" + sysDocID + "' ORDER BY SID.RowIndex ";
					FillDataSet(dataSet, "Property_Service_Invoice_Detail", cmdText);
					dataSet.Relations.Add("PropertyServiceInvoice", new DataColumn[2]
					{
						dataSet.Tables["Property_Service_Invoice"].Columns["SysDocID"],
						dataSet.Tables["Property_Service_Invoice"].Columns["VoucherID"]
					}, new DataColumn[2]
					{
						dataSet.Tables["Property_Service_Invoice_Detail"].Columns["SysDocID"],
						dataSet.Tables["Property_Service_Invoice_Detail"].Columns["VoucherID"]
					}, createConstraints: false);
					dataSet.Tables["Property_Service_Invoice"].Columns.Add("TotalInWords", typeof(string));
					decimal d = default(decimal);
					foreach (DataRow row3 in dataSet.Tables["Property_Service_Invoice"].Rows)
					{
						decimal result = default(decimal);
						decimal result2 = default(decimal);
						decimal result3 = default(decimal);
						decimal num = default(decimal);
						decimal.TryParse(row3["Total"].ToString(), out result);
						decimal.TryParse(row3["Discount"].ToString(), out result2);
						decimal.TryParse(row3["Tax"].ToString(), out result3);
						num = decimal.Parse(row3["RoundOff"].ToString(), NumberStyles.Any);
						row3["TotalInWords"] = NumToWord.GetNumInWords(decimalPoints: new CompanyInformations(base.DBConfig).CurrencyDecimalPoints, amount: result + d - result2 + result3 + num);
					}
					break;
				}
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool isExport, bool showVoid, string sysDocID, bool isCash, NonInventoryInvoiceType invoiceType)
		{
			DataSet dataSet = new DataSet();
			switch (invoiceType)
			{
			case NonInventoryInvoiceType.SalesInvoice:
			{
				string text4 = StoreConfiguration.ToSqlDateTimeString(from);
				string text5 = StoreConfiguration.ToSqlDateTimeString(to);
				string text6 = "SELECT   ISNULL(IsVoid,'False') AS 'V', INV.SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],CustomerAddress [Address],TransactionDate [Invoice Date], DueDate AS [Due Date],\r\n\t\t\t\t\t\t\tINV.TermID [Term],INV.Reference AS Ref1,INV.Reference2 AS Ref2,J.JobID,J.JobName,\r\n                            CASE ISNULL(IsCash,'False') WHEN 'True' THEN 'Cash' ELSE 'Credit' END AS [Type],INV.SalespersonID + '-' + SP.FullName AS [Salesperon],Total - ISNULL(Discount,0) AS [Amount],INV.TaxAmount\r\n                            FROM   Sales_Invoice_NonInv INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID\r\n                            LEFT JOIN Job J ON INV.JobID=J.JobID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Salesperson SP ON SP.SalespersonID = INV.SalespersonID WHERE 1=1 AND InvoiceType=" + (byte)invoiceType;
				if (from != DateTime.MinValue)
				{
					text6 = text6 + " AND TransactionDate Between '" + text4 + "' AND '" + text5 + "'";
				}
				if (!showVoid)
				{
					text6 += " AND ISNULL(IsVoid,'False')='False' ";
				}
				if (sysDocID != "")
				{
					text6 = text6 + " AND INV.SysDocID = '" + sysDocID + "'";
				}
				text6 = text6 + " AND ISNULL(IsExport, 'False') = '" + isExport.ToString() + "' AND ISNULL(IsCash, 'False') = '" + isCash.ToString() + "'";
				SqlCommand sqlCommand2 = new SqlCommand(text6);
				FillDataSet(dataSet, "Sales_Invoice_NonInv", sqlCommand2);
				break;
			}
			case NonInventoryInvoiceType.PropertySalesInvoice:
			{
				string text = StoreConfiguration.ToSqlDateTimeString(from);
				string text2 = StoreConfiguration.ToSqlDateTimeString(to);
				string text3 = "SELECT   ISNULL(IsVoid,'False') AS 'V', INV.SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],P.PropertyName [Property],PU.PropertyUnitName [PropertyUnit],PA.PropertyAgentName  [Agent],CustomerAddress [Address],TransactionDate [Invoice Date], DueDate AS [Due Date],\r\n\t\t\t\t\t\t\tINV.TermID [Term],INV.Reference AS Ref1,INV.Reference2 AS Ref2,\r\n                            CASE ISNULL(IsCash,'False') WHEN 'True' THEN 'Cash' ELSE 'Credit' END AS [Type],PA.PropertyAgentName [Agent],Total - ISNULL(Discount,0) AS [Amount],INV.TaxAmount\r\n                            FROM   Sales_Invoice_NonInv INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID\r\n                            LEFT JOIN Job J ON INV.JobID=J.JobID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Salesperson SP ON SP.SalespersonID = INV.SalespersonID\r\n                            LEFT JOIN Property P ON P.PropertyID=INV.PropertyID\r\n                            LEFT JOIN Property_Unit PU ON PU.PropertyUnitID=INV.PropertyUnitID\r\n                            LEFT JOIN Property_Agent PA ON PA.PropertyAgentID=INV.AgentID  WHERE 1=1 AND InvoiceType=" + (byte)invoiceType;
				if (from != DateTime.MinValue)
				{
					text3 = text3 + " AND TransactionDate Between '" + text + "' AND '" + text2 + "'";
				}
				if (!showVoid)
				{
					text3 += " AND ISNULL(IsVoid,'False')='False' ";
				}
				if (sysDocID != "")
				{
					text3 = text3 + " AND INV.SysDocID = '" + sysDocID + "'";
				}
				text3 = text3 + " AND ISNULL(IsExport, 'False') = '" + isExport.ToString() + "' AND ISNULL(IsCash, 'False') = '" + isCash.ToString() + "'";
				SqlCommand sqlCommand = new SqlCommand(text3);
				FillDataSet(dataSet, "Sales_Invoice_NonInv", sqlCommand);
				break;
			}
			}
			return dataSet;
		}

		public bool InvoiceHasShippedQuantity(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Sales_Invoice_NonInv_Detail SID\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityShipped,0))>0";
			object obj = ExecuteScalar(exp, sqlTransaction);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}

		internal bool ReOpenInvoice(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "SELECT COUNT(RowIndex)FROM Sales_Invoice_NonInv_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                                AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Sales_Invoice_NonInv_Detail SOD2 WHERE SOD.SysDocID=SOD2.SysDocID AND SOD.VoucherID=SOD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityShipped,0) ) FROM Sales_Invoice_NonInv_Detail SOD3 WHERE SOD.SysDocID=SOD3.SysDocID AND SOD.VoucherID=SOD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) > 0)
				{
					return true;
				}
				exp = "UPDATE Sales_Invoice_NonInv SET IsDelivered = 'False' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public bool ModifyTransactions(string sysDocID, string voucherID, string userID, bool isModify, string toUpdate)
		{
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				if (isModify)
				{
					isModify = true;
					object obj = null;
					if (toUpdate == "")
					{
						text = "INSERT INTO Modify_Transactions  Values( '" + sysDocID + "' , '" + voucherID + "', '" + userID + "', '" + isModify.ToString() + "')";
						obj = ExecuteScalar(text, sqlTransaction);
						base.DBConfig.EndTransaction(result: true);
					}
					if (toUpdate != "")
					{
						isModify = false;
						text = "UPDATE Modify_Transactions   SET IsModify='" + isModify.ToString() + "' WHERE  SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'AND UserID='" + userID + "'";
						bool result = ExecuteNonQuery(text, sqlTransaction) > 0;
						base.DBConfig.EndTransaction(result: true);
						return result;
					}
					if (obj == null || int.Parse(obj.ToString()) > 0)
					{
						return true;
					}
					return false;
				}
				text = "SELECT  COUNT(ISNull(IsModify,0)) FROM Modify_Transactions  WHERE IsModify='1' AND SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND UserID='" + userID + "'";
				object obj2 = ExecuteScalar(text, sqlTransaction);
				base.DBConfig.EndTransaction(result: true);
				if (obj2 == null || int.Parse(obj2.ToString()) > 0)
				{
					return true;
				}
				return false;
			}
			catch
			{
				base.DBConfig.EndTransaction(result: true);
				return false;
			}
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			string exp = "SELECT COUNT(*) FROM Sales_Return_Detail ID WHERE  SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherNumber + "'";
			object obj = ExecuteScalar(exp);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return false;
			}
			return true;
		}

		private DataSet GetServiceInvoiceList(string sysDocID, string voucherID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT INV.* FROM Sales_Invoice_NonInv INV  WHERE INV.VoucherID='" + voucherID + "' AND INV.SysDocID='" + sysDocID + "'";
				new SqlCommand(text);
				FillDataSet(dataSet, "Sales_Invoice_NonInv", text);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Sales_Invoice_NonInv"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT distinct SID.* ,PIC.IncomeID,PIC.IncomeName,PIC.Description as IncomeDescription,SID.TaxOption\r\n                           FROM Sales_Invoice_NonInv_Detail SID INNER JOIN PropertyIncome_Code PIC ON SID.ProductID=PIC.IncomeID\r\n                            WHERE SID.VoucherID='" + voucherID + "' AND SID.SysDocID='" + sysDocID + "' ORDER BY SID.RowIndex ";
				FillDataSet(dataSet, "Sales_Invoice_NonInv_Detail", text);
				text = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(dataSet, "Tax_Detail", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertBasePropertyInvoiceData(PropertyRentData rentalData, bool isUpdate)
		{
			bool flag = true;
			SalesInvoiceNIData salesInvoiceNIData = new SalesInvoiceNIData();
			if (salesInvoiceNIData == null || !isUpdate)
			{
				salesInvoiceNIData = new SalesInvoiceNIData();
			}
			string text = "";
			string text2 = "";
			DataRow dataRow = rentalData.PropertyRentTable.Rows[0];
			DataRow dataRow2 = salesInvoiceNIData.SalesInvoiceTable.NewRow();
			if (!isUpdate)
			{
				text = dataRow["PeriodicSysDocID"].ToString();
				text2 = new SystemDocuments(base.DBConfig).GetNextDocumentNumber(text);
			}
			else
			{
				text = dataRow["PeriodicSysDocID"].ToString();
				text2 = dataRow["PeriodicVoucherID"].ToString();
			}
			dataRow2["SysDocID"] = text;
			dataRow2["VoucherID"] = text2;
			dataRow2["DivisionID"] = dataRow["DivisionID"].ToString();
			dataRow2["CompanyID"] = dataRow["CompanyID"].ToString();
			dataRow2["TransactionDate"] = dataRow["InvoiceStartDate"].ToString();
			dataRow2["DueDate"] = dataRow["InvoiceStartDate"].ToString();
			dataRow2["SalesFlow"] = DBNull.Value;
			dataRow2["Reference"] = DBNull.Value;
			dataRow2["Reference2"] = DBNull.Value;
			dataRow2["Note"] = dataRow["Note"].ToString() + dataRow["InvoiceNote"].ToString();
			dataRow2["CustomerID"] = dataRow["CustomerID"].ToString();
			dataRow2["CustomerAddress"] = DBNull.Value;
			dataRow2["ShipToAddress"] = DBNull.Value;
			dataRow2["IsCash"] = false;
			dataRow2["IsWeightInvoice"] = DBNull.Value;
			dataRow2["SourceDocType"] = ItemSourceTypes.None;
			dataRow2["ShippingAddressID"] = DBNull.Value;
			dataRow2["BillingAddressID"] = DBNull.Value;
			dataRow2["TermID"] = DBNull.Value;
			if (dataRow["PayeeTaxGroupID"].ToString() != "")
			{
				dataRow2["PayeeTaxGroupID"] = dataRow["PayeeTaxGroupID"].ToString();
			}
			else
			{
				dataRow2["PayeeTaxGroupID"] = DBNull.Value;
			}
			dataRow2["TaxOption"] = dataRow["TaxOption"].ToString();
			dataRow2["PriceIncludeTax"] = false;
			dataRow2["ShippingMethodID"] = DBNull.Value;
			dataRow2["SalespersonID"] = DBNull.Value;
			dataRow2["ReportTo"] = DBNull.Value;
			dataRow2["CurrentUser"] = DBNull.Value;
			dataRow2["CurrencyID"] = dataRow["CurrencyID"].ToString();
			dataRow2["CurrencyRate"] = dataRow["CurrencyRate"].ToString();
			dataRow2["JobID"] = DBNull.Value;
			dataRow2["CostCategoryID"] = DBNull.Value;
			if (dataRow["PropertyID"].ToString() != "")
			{
				dataRow2["PropertyID"] = dataRow["PropertyID"].ToString();
			}
			else
			{
				dataRow2["PropertyID"] = DBNull.Value;
			}
			if (dataRow["UnitID"].ToString() != "")
			{
				dataRow2["PropertyUnitID"] = dataRow["UnitID"].ToString();
			}
			else
			{
				dataRow2["PropertyUnitID"] = DBNull.Value;
			}
			if (dataRow["PropertyAgentID"].ToString() != "")
			{
				dataRow2["AgentID"] = dataRow["PropertyAgentID"].ToString();
			}
			else
			{
				dataRow2["AgentID"] = DBNull.Value;
			}
			dataRow2["InvoiceType"] = (byte)2;
			dataRow2["PONumber"] = DBNull.Value;
			dataRow2["Discount"] = DBNull.Value;
			dataRow2["TaxAmount"] = dataRow["TaxAmount"].ToString();
			dataRow2["RoundOff"] = default(decimal);
			dataRow2["Total"] = dataRow["Total"].ToString();
			dataRow2.EndEdit();
			salesInvoiceNIData.SalesInvoiceTable.Rows.Add(dataRow2);
			salesInvoiceNIData.SalesInvoiceDetailTable.Rows.Clear();
			int num = 0;
			foreach (DataRow row in rentalData.PropertyRentDetailTable.Rows)
			{
				DataRow dataRow4 = salesInvoiceNIData.SalesInvoiceDetailTable.NewRow();
				dataRow4.BeginEdit();
				dataRow4["SysDocID"] = text;
				dataRow4["VoucherID"] = text2;
				dataRow4["ProductID"] = row["IncomeID"].ToString();
				dataRow4["Quantity"] = 1;
				dataRow4["FOCQuantity"] = 0;
				dataRow4["UnitPrice"] = row["Amount"].ToString();
				if (dataRow4["TaxOption"].ToString() != "")
				{
					dataRow4["TaxOption"] = dataRow4["TaxOption"].ToString();
				}
				else
				{
					dataRow4["TaxOption"] = (byte)2;
				}
				if (dataRow4["TaxAmount"].ToString() != "")
				{
					dataRow4["TaxAmount"] = dataRow4["TaxAmount"];
				}
				else
				{
					dataRow4["TaxAmount"] = DBNull.Value;
				}
				if (dataRow4["TaxGroupID"].ToString() != "")
				{
					dataRow4["TaxGroupID"] = dataRow4["TaxGroupID"].ToString();
				}
				else
				{
					dataRow4["TaxGroupID"] = DBNull.Value;
				}
				dataRow4["Amount"] = row["Amount"].ToString();
				dataRow4["Description"] = row["Description"].ToString();
				dataRow4["Remarks"] = DBNull.Value;
				dataRow4["RowSource"] = DBNull.Value;
				dataRow4["RowIndex"] = num;
				num++;
				dataRow4["JobID"] = DBNull.Value;
				dataRow4["CostCategoryID"] = DBNull.Value;
				dataRow4.EndEdit();
				salesInvoiceNIData.SalesInvoiceDetailTable.Rows.Add(dataRow4);
			}
			salesInvoiceNIData.Tables["Tax_Detail"].Rows.Clear();
			foreach (DataRow row2 in rentalData.Tables["Tax_Detail"].Rows)
			{
				DataRow dataRow6 = salesInvoiceNIData.TaxDetailsTable.NewRow();
				dataRow6.BeginEdit();
				dataRow6["SysDocID"] = text;
				dataRow6["VoucherID"] = text2;
				dataRow6["TaxGroupID"] = row2["TaxGroupID"];
				dataRow6["TaxItemID"] = row2["TaxItemID"];
				dataRow6["TaxLevel"] = row2["TaxLevel"];
				dataRow6["TaxRate"] = row2["TaxRate"];
				dataRow6["CalculationMethod"] = row2["CalculationMethod"];
				dataRow6["TaxAmount"] = row2["TaxAmount"];
				dataRow6["CurrencyID"] = row2["CurrencyID"];
				dataRow6["CurrencyRate"] = row2["CurrencyRate"];
				dataRow6["RowIndex"] = row2["RowIndex"];
				dataRow6["OrderIndex"] = row2["OrderIndex"];
				dataRow6.EndEdit();
				salesInvoiceNIData.TaxDetailsTable.Rows.Add(dataRow6);
			}
			return flag & InsertUpdateSalesInvoice(salesInvoiceNIData, isUpdate);
		}

		public bool GenerateRecurringPropertyInvoiceData(RecurringInvoiceData recurringInvoiceData)
		{
			bool flag = true;
			try
			{
				foreach (DataRow row in recurringInvoiceData.RecurringTransactionDetailsTable.Rows)
				{
					string value = row["CreatedSysDocID"].ToString();
					string value2 = row["CreatedVoucherID"].ToString();
					string sysDocID = row["SysDocID"].ToString();
					string voucherID = row["VoucherID"].ToString();
					DataSet serviceInvoiceList = GetServiceInvoiceList(sysDocID, voucherID);
					SalesInvoiceNIData salesInvoiceNIData = new SalesInvoiceNIData();
					DataRow dataRow2 = serviceInvoiceList.Tables[0].Rows[0];
					DataRow dataRow3 = salesInvoiceNIData.SalesInvoiceTable.NewRow();
					dataRow3.BeginEdit();
					dataRow3["SysDocID"] = value;
					dataRow3["VoucherID"] = value2;
					dataRow3["TransactionDate"] = DateTime.Parse(row["TransactionDate"].ToString());
					dataRow3["DueDate"] = DateTime.Parse(row["TransactionDate"].ToString());
					dataRow3["SalesFlow"] = DBNull.Value;
					dataRow3["Reference"] = DBNull.Value;
					dataRow3["Reference2"] = DBNull.Value;
					dataRow3["Note"] = DBNull.Value;
					dataRow3["CustomerID"] = dataRow2["CustomerID"].ToString();
					dataRow3["CustomerAddress"] = DBNull.Value;
					dataRow3["ShipToAddress"] = DBNull.Value;
					dataRow3["IsCash"] = false;
					dataRow3["IsWeightInvoice"] = DBNull.Value;
					dataRow3["SourceDocType"] = ItemSourceTypes.None;
					dataRow3["ShippingAddressID"] = DBNull.Value;
					dataRow3["BillingAddressID"] = DBNull.Value;
					dataRow3["TermID"] = DBNull.Value;
					dataRow3["DivisionID"] = DBNull.Value;
					dataRow3["CompanyID"] = 1;
					if (dataRow2["PayeeTaxGroupID"].ToString() != "")
					{
						dataRow3["PayeeTaxGroupID"] = dataRow2["PayeeTaxGroupID"].ToString();
					}
					else
					{
						dataRow3["PayeeTaxGroupID"] = DBNull.Value;
					}
					dataRow3["TaxOption"] = PayeeTaxOptions.NonTaxable;
					dataRow3["PriceIncludeTax"] = false;
					dataRow3["ShippingMethodID"] = DBNull.Value;
					dataRow3["SalespersonID"] = DBNull.Value;
					dataRow3["ReportTo"] = DBNull.Value;
					dataRow3["CurrentUser"] = dataRow2["CLUserID"].ToString();
					dataRow3["CurrencyID"] = dataRow2["CurrencyID"].ToString();
					dataRow3["CurrencyRate"] = dataRow2["CurrencyRate"].ToString();
					dataRow3["JobID"] = DBNull.Value;
					dataRow3["CostCategoryID"] = DBNull.Value;
					if (dataRow2["PropertyID"].ToString() != "")
					{
						dataRow3["PropertyID"] = dataRow2["PropertyID"].ToString();
					}
					else
					{
						dataRow3["PropertyID"] = DBNull.Value;
					}
					if (dataRow2["PropertyUnitID"].ToString() != "")
					{
						dataRow3["PropertyUnitID"] = dataRow2["PropertyUnitID"].ToString();
					}
					else
					{
						dataRow3["PropertyUnitID"] = DBNull.Value;
					}
					if (dataRow2["AgentID"].ToString() != "")
					{
						dataRow3["AgentID"] = dataRow2["AgentID"].ToString();
					}
					else
					{
						dataRow3["AgentID"] = DBNull.Value;
					}
					dataRow3["InvoiceType"] = (byte)2;
					dataRow3["PONumber"] = DBNull.Value;
					dataRow3["Discount"] = DBNull.Value;
					dataRow3["TaxAmount"] = dataRow2["TaxAmount"].ToString();
					dataRow3["RoundOff"] = default(decimal);
					dataRow3["Total"] = dataRow2["Total"].ToString();
					dataRow3.EndEdit();
					salesInvoiceNIData.SalesInvoiceTable.Rows.Add(dataRow3);
					salesInvoiceNIData.SalesInvoiceDetailTable.Rows.Clear();
					int num = 0;
					foreach (DataRow row2 in serviceInvoiceList.Tables["Sales_Invoice_NonInv_Detail"].Rows)
					{
						DataRow dataRow5 = salesInvoiceNIData.SalesInvoiceDetailTable.NewRow();
						dataRow5.BeginEdit();
						if (num == 0)
						{
							dataRow5["Description"] = row["InvoiceNote"].ToString();
						}
						else
						{
							dataRow5["Description"] = row2["Description"].ToString();
						}
						dataRow5["SysDocID"] = value;
						dataRow5["VoucherID"] = value2;
						dataRow5["ProductID"] = row2["ProductID"].ToString();
						dataRow5["Quantity"] = 1;
						dataRow5["FOCQuantity"] = 0;
						dataRow5["UnitPrice"] = row2["UnitPrice"].ToString();
						if (row2["TaxOption"].ToString() != "")
						{
							dataRow5["TaxOption"] = row2["TaxOption"].ToString();
						}
						else
						{
							dataRow5["TaxOption"] = (byte)2;
						}
						if (row2["TaxAmount"].ToString() != "")
						{
							dataRow5["TaxAmount"] = row2["TaxAmount"].ToString();
						}
						else
						{
							dataRow5["TaxAmount"] = DBNull.Value;
						}
						if (row2["TaxGroupID"].ToString() != "")
						{
							dataRow5["TaxGroupID"] = row2["TaxGroupID"].ToString();
						}
						else
						{
							dataRow5["TaxGroupID"] = DBNull.Value;
						}
						dataRow5["Amount"] = row2["Amount"].ToString();
						dataRow5["Remarks"] = DBNull.Value;
						dataRow5["RowSource"] = DBNull.Value;
						dataRow5["RowIndex"] = num;
						dataRow5["JobID"] = DBNull.Value;
						dataRow5["CostCategoryID"] = DBNull.Value;
						dataRow5.EndEdit();
						salesInvoiceNIData.SalesInvoiceDetailTable.Rows.Add(dataRow5);
						num++;
					}
					salesInvoiceNIData.Tables["Tax_Detail"].Rows.Clear();
					foreach (DataRow row3 in serviceInvoiceList.Tables["Tax_Detail"].Rows)
					{
						DataRow dataRow7 = salesInvoiceNIData.TaxDetailsTable.NewRow();
						dataRow7.BeginEdit();
						dataRow7["SysDocID"] = value;
						dataRow7["VoucherID"] = value2;
						dataRow7["TaxGroupID"] = row3["TaxGroupID"];
						dataRow7["TaxItemID"] = row3["TaxItemID"];
						dataRow7["TaxLevel"] = row3["TaxLevel"];
						dataRow7["TaxRate"] = row3["TaxRate"];
						dataRow7["CalculationMethod"] = row3["CalculationMethod"];
						dataRow7["TaxAmount"] = row3["TaxAmount"];
						dataRow7["CurrencyID"] = row3["CurrencyID"];
						dataRow7["CurrencyRate"] = row3["CurrencyRate"];
						dataRow7["RowIndex"] = row3["RowIndex"];
						dataRow7["OrderIndex"] = row3["OrderIndex"];
						dataRow7.EndEdit();
						salesInvoiceNIData.TaxDetailsTable.Rows.Add(dataRow7);
					}
					flag &= InsertUpdateSalesInvoice(salesInvoiceNIData, isUpdate: false);
				}
				return flag;
			}
			catch
			{
				flag = false;
				throw;
			}
		}
	}
}
