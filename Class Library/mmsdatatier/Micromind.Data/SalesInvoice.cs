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
	public sealed class SalesInvoice : StoreObject
	{
		private const string SALESINVOICE_TABLE = "Sales_Invoice";

		private const string SALESINVOICEDETAIL_TABLE = "Sales_Invoice_Detail";

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

		private const string REFSLNO_PARM = "@RefSlNo";

		private const string REFTEXT1_PARM = "@RefText1";

		private const string REFTEXT2_PARM = "@RefText2";

		private const string REFNUM1_PARM = "@RefNum1";

		private const string REFNUM2_PARM = "@RefNum2";

		private const string REFDATE1_PARM = "@RefDate1";

		private const string REFDATE2_PARM = "@RefDate2";

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

		private const string SALESINVOICEEXPENSETABLE_PARM = "@Sales_Invoice_Expense";

		private const string EXPENSEID_PARM = "@ExpenseID";

		private const string RATETYPE_PARM = "@RateType";

		private const string ISDEDUCT_PARM = "@IsDeduct";

		public SalesInvoice(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdateSalesInvoiceText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Invoice", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("CustomerID", "@CustomerID"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("SalesFlow", "@SalesFlow"), new FieldValue("IsExport", "@IsExport"), new FieldValue("DueDate", "@DueDate"), new FieldValue("SalespersonID", "@SalespersonID"), new FieldValue("ReportTo", "@ReportTo"), new FieldValue("RequiredDate", "@RequiredDate"), new FieldValue("SourceDocType", "@SourceDocType"), new FieldValue("ShippingAddressID", "@ShippingAddressID"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("BillingAddressID", "@BillingAddressID"), new FieldValue("ShipToAddress", "@ShipToAddress"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("CustomerAddress", "@CustomerAddress"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("RoundOff", "@RoundOff"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("PaymentMethodType", "@PaymentMethodType"), new FieldValue("ExpAmount", "@ExpAmount"), new FieldValue("ExpPercent", "@ExpPercent"), new FieldValue("ExpCode", "@ExpCode"), new FieldValue("TermID", "@TermID"), new FieldValue("IsWeightInvoice", "@IsWeightInvoice"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("IsCash", "@IsCash"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("Note", "@Note"), new FieldValue("CLUserID", "@CLUserID"), new FieldValue("DriverID", "@DriverID"), new FieldValue("VehicleID", "@VehicleID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Sales_Invoice", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			sqlBuilder.AddInsertUpdateParameters("Sales_Invoice_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("FOCQuantity", "@FOCQuantity"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("WeightQuantity", "@WeightQuantity"), new FieldValue("WeightPrice", "@WeightPrice"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("Description", "@Description"), new FieldValue("Cost", "@Cost"), new FieldValue("Remarks", "@Remarks"), new FieldValue("UnitID", "@UnitID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("ConsignmentNo", "@ConsignmentNo"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxPercentage", "@TaxPercentage"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("OrderSysDocID", "@OrderSysDocID"), new FieldValue("OrderVoucherID", "@OrderVoucherID"), new FieldValue("DNoteSysDocID", "@DNoteSysDocID"), new FieldValue("DNoteVoucherID", "@DNoteVoucherID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("IsDNRow", "@IsDNRow"), new FieldValue("RowSource", "@RowSource"), new FieldValue("ListVoucherID", "@ListVoucherID"), new FieldValue("ListSysDocID", "@ListSysDocID"), new FieldValue("ListRowIndex", "@ListRowIndex"), new FieldValue("SpecificationID", "@SpecificationID"), new FieldValue("StyleID", "@StyleID"), new FieldValue("IsRecost", "@IsRecost"), new FieldValue("RefSlNo", "@RefSlNo"), new FieldValue("RefText1", "@RefText1"), new FieldValue("RefText2", "@RefText2"), new FieldValue("RefNum1", "@RefNum1"), new FieldValue("RefNum2", "@RefNum2"), new FieldValue("RefDate1", "@RefDate1"), new FieldValue("RefDate2", "@RefDate2"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"));
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
			parameters.Add("@RefSlNo", SqlDbType.NVarChar);
			parameters.Add("@RefText1", SqlDbType.NVarChar);
			parameters.Add("@RefText2", SqlDbType.NVarChar);
			parameters.Add("@RefNum1", SqlDbType.Decimal);
			parameters.Add("@RefNum2", SqlDbType.Decimal);
			parameters.Add("@RefDate1", SqlDbType.DateTime);
			parameters.Add("@RefDate2", SqlDbType.DateTime);
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
			parameters["@OrderVoucherID"].SourceColumn = "OrderVoucherID";
			parameters["@OrderSysDocID"].SourceColumn = "OrderSysDocID";
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
			parameters["@RefSlNo"].SourceColumn = "RefSlNo";
			parameters["@RefText1"].SourceColumn = "RefText1";
			parameters["@RefText2"].SourceColumn = "RefText2";
			parameters["@RefNum1"].SourceColumn = "RefNum1";
			parameters["@RefNum2"].SourceColumn = "RefNum2";
			parameters["@RefDate1"].SourceColumn = "RefDate1";
			parameters["@RefDate2"].SourceColumn = "RefDate2";
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

		private string GetInsertUpdateExpenseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Sales_Invoice_Expense", new FieldValue("InvoiceSysDocID", "@InvoiceSysDocID"), new FieldValue("InvoiceVoucherID", "@InvoiceVoucherID"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("RateType", "@RateType"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Reference", "@Reference"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("IsDeduct", "@IsDeduct"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateExpenseCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdateExpenseText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdateExpenseText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@InvoiceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@InvoiceVoucherID", SqlDbType.NVarChar);
			parameters.Add("@ExpenseID", SqlDbType.NVarChar);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Money);
			parameters.Add("@AmountFC", SqlDbType.Money);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@RateType", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxAmount", SqlDbType.Money);
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters.Add("@IsDeduct", SqlDbType.Bit);
			parameters["@InvoiceSysDocID"].SourceColumn = "InvoiceSysDocID";
			parameters["@InvoiceVoucherID"].SourceColumn = "InvoiceVoucherID";
			parameters["@ExpenseID"].SourceColumn = "ExpenseID";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@RateType"].SourceColumn = "RateType";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@IsDeduct"].SourceColumn = "IsDeduct";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private bool ValidateData(SalesInvoiceData salesInvoiceData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = "";
				DataRow dataRow = salesInvoiceData.SalesInvoiceTable.Rows[0];
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
					foreach (DataRow row in salesInvoiceData.SalesInvoiceDetailTable.Rows)
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
					text = "select COUNT(*) FROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI \r\n                            ON SID.SysDocID = SI.SysDOcID AND SID.VoucherID = SI.VoucherID WHERE ISNULL(IsVoid,'False')='False' AND OrderSysDocID + ordervoucherid  IN (" + text3 + ")";
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

		public bool RecostInvoice(string productID, string locationID, decimal purchaseQuantity, decimal shortQuantity, decimal oldAvgCost, decimal newAvgCost, SqlTransaction sqlTransaction)
		{
			return true;
		}

		public bool InsertUpdateSalesInvoice(SalesInvoiceData salesInvoiceData, bool isUpdate, bool TempSave)
		{
			return InsertUpdateSalesInvoice(salesInvoiceData, isUpdate, null, TempSave);
		}

		public bool InsertUpdateSalesInvoice(SalesInvoiceData salesInvoiceData, bool isUpdate, SqlTransaction sqlTransaction, bool TempSave = false)
		{
			string text = "";
			bool flag = true;
			SqlCommand sqlCommand = null;
			string text2 = "";
			string text3 = "";
			string text4 = "";
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = salesInvoiceData.SalesInvoiceTable.Rows[0];
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				string text5 = dataRow["VoucherID"].ToString();
				string text6 = dataRow["SysDocID"].ToString();
				flag &= ValidateData(salesInvoiceData, isUpdate, sqlTransaction);
				bool flag2 = false;
				if (dataRow["IsExport"] != DBNull.Value)
				{
					flag2 = bool.Parse(dataRow["IsExport"].ToString());
				}
				string text7 = "";
				if (flag2)
				{
					object companyOptionValue = new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString());
					if (companyOptionValue != null)
					{
						text7 = companyOptionValue.ToString();
					}
				}
				else
				{
					object companyOptionValue2 = new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString());
					if (companyOptionValue2 != null)
					{
						text7 = companyOptionValue2.ToString();
					}
				}
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text7 != "")
				{
					salesFlows = (SalesFlows)int.Parse(text7.ToString());
				}
				bool isDNoteInventory = false;
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					isDNoteInventory = true;
				}
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					flag &= ValidateDate(salesInvoiceData, isUpdate, sqlTransaction);
					flag &= ValidateSoftClosePeriod(salesInvoiceData, isUpdate, sqlTransaction);
				}
				if (isUpdate && InvoiceHasShippedQuantity(text6, text5, sqlTransaction))
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
					foreach (DataRow row in salesInvoiceData.InvoiceDNoteTable.Rows)
					{
						string sysDocID = row["DNoteSysDocID"].ToString();
						string text8 = row["DNoteVoucherID"].ToString();
						if (deliveryNote.IsDNoteInvoiced(sysDocID, text8, sqlTransaction))
						{
							throw new CompanyException("One or more delivery notes are already invoiced.\nDocument:" + text8);
						}
					}
				}
				decimal num = default(decimal);
				foreach (DataRow row2 in salesInvoiceData.SalesInvoiceDetailTable.Rows)
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
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Sales_Invoice", "VoucherID", dataRow["SysDocID"].ToString(), text5, sqlTransaction))
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
				if (isUpdate && !flag3 && dataRow["TotalFC"] != null && dataRow["TotalFC"].ToString() != "")
				{
					dataRow["TotalFC"] = DBNull.Value;
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
					foreach (DataRow row3 in salesInvoiceData.InvoiceExpenseTable.Rows)
					{
						decimal result7 = default(decimal);
						decimal.TryParse(row3["Amount"].ToString(), out result7);
						if (a == "M")
						{
							row3["Amount"] = Math.Round(result7 * result5, currencyDecimalPoints);
						}
						else
						{
							row3["Amount"] = Math.Round(result7 / result5, currencyDecimalPoints);
						}
						row3["AmountFC"] = result7;
					}
				}
				bool flag4 = false;
				foreach (DataRow row4 in salesInvoiceData.SalesInvoiceDetailTable.Rows)
				{
					row4["SysDocID"] = dataRow["SysDocID"];
					row4["VoucherID"] = dataRow["VoucherID"];
					text4 = row4["ProductID"].ToString();
					string checkFieldValue = row4["LocationID"].ToString();
					decimal result8 = default(decimal);
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product_Location", "Quantity", "ProductID", text4, "LocationID", checkFieldValue, sqlTransaction);
					if (fieldValue != null)
					{
						decimal.TryParse(fieldValue.ToString(), out result8);
					}
					if (row4["FOCQuantity"] != DBNull.Value && decimal.Parse(row4["FOCQuantity"].ToString()) > 0m)
					{
						flag4 = true;
					}
					float num2 = 0f;
					string text9 = "";
					fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text4, sqlTransaction);
					if (fieldValue != null)
					{
						text9 = fieldValue.ToString();
					}
					if (text9 != "" && row4["UnitID"] != DBNull.Value && row4["UnitID"].ToString() != text9)
					{
						DataRow obj3 = new Products(base.DBConfig).GetProductUnitRow(text4, row4["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text4 + "\nUnit:" + row4["UnitID"].ToString(), 1051);
						float num3 = float.Parse(obj3["Factor"].ToString());
						string text10 = obj3["FactorType"].ToString();
						num2 = float.Parse(row4["Quantity"].ToString());
						row4["UnitFactor"] = num3;
						row4["FactorType"] = text10;
						row4["UnitQuantity"] = row4["Quantity"];
						num2 = ((!(text10 == "M")) ? float.Parse(Math.Round(num2 * num3, 5).ToString()) : float.Parse(Math.Round(num2 / num3, 5).ToString()));
						row4["Quantity"] = num2;
					}
					if ((decimal)num2 > result8)
					{
						row4["IsRecost"] = true;
					}
					if (flag3)
					{
						decimal result9 = default(decimal);
						decimal result10 = default(decimal);
						row4["UnitPriceFC"] = row4["UnitPrice"];
						row4["AmountFC"] = row4["Amount"];
						decimal.TryParse(row4["UnitPrice"].ToString(), out result9);
						decimal.TryParse(row4["Amount"].ToString(), out result10);
						result9 = ((!(a == "M")) ? Math.Round(result9 / result5, 4) : Math.Round(result9 * result5, 4));
						row4["UnitPrice"] = result9;
						result10 = ((!(a == "M")) ? Math.Round(result10 / result5, currencyDecimalPoints) : Math.Round(result10 * result5, currencyDecimalPoints));
						row4["Amount"] = result10;
					}
				}
				if (isUpdate)
				{
					flag &= DeleteSalesInvoiceDetailsRows(text6, text5, isDeleteTransaction: false, sqlTransaction);
					flag &= DeleteSalesInvoiceExpenseRows(text6, text5, sqlTransaction);
				}
				sqlCommand = GetInsertUpdateSalesInvoiceCommand(isUpdate);
				sqlCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(salesInvoiceData, "Sales_Invoice", sqlCommand)) : (flag & Insert(salesInvoiceData, "Sales_Invoice", sqlCommand)));
				if (salesInvoiceData.Tables["Sales_Invoice_Detail"].Rows.Count > 0)
				{
					sqlCommand = GetInsertUpdateSalesInvoiceDetailsCommand(isUpdate: false);
					sqlCommand.Transaction = sqlTransaction;
					flag &= Insert(salesInvoiceData, "Sales_Invoice_Detail", sqlCommand);
				}
				if (salesInvoiceData.Tables["Sales_Invoice_Expense"].Rows.Count > 0)
				{
					sqlCommand = GetInsertUpdateExpenseCommand(isUpdate: false);
					sqlCommand.Transaction = sqlTransaction;
					flag &= Insert(salesInvoiceData, "Sales_Invoice_Expense", sqlCommand);
				}
				if (result)
				{
					DataRow dataRow4 = salesInvoiceData.PaymentTable.Rows[0];
					PaymentMethodTypes paymentMethodTypes = (PaymentMethodTypes)byte.Parse(dataRow4["PaymentMethodType"].ToString());
					string registerID = dataRow4["RegisterID"].ToString();
					string text11 = "";
					text11 = (string)(dataRow4["AccountID"] = ((paymentMethodTypes != PaymentMethodTypes.CreditCard) ? new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID") : new Register(base.DBConfig).GetRegisterAccountID(registerID, "CardReceivedAccountID")));
					dataRow4.EndEdit();
				}
				DataSet dataSet = new DataSet();
				if ((salesFlows != SalesFlows.SOThenDNThenInvoice) | result)
				{
					InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
					foreach (DataRow row5 in salesInvoiceData.SalesInvoiceDetailTable.Rows)
					{
						DataRow dataRow6 = inventoryTransactionData.InventoryTransactionTable.NewRow();
						dataRow6.BeginEdit();
						dataRow6["SysDocID"] = row5["SysDocID"];
						dataRow6["VoucherID"] = row5["VoucherID"];
						if (row5["LocationID"].ToString() == "")
						{
							throw new Exception("Location cannot be empty.");
						}
						dataRow6["LocationID"] = row5["LocationID"];
						dataRow6["JobID"] = row5["JobID"];
						dataRow6["CostCategoryID"] = row5["CostCategoryID"];
						dataRow6["ProductID"] = row5["ProductID"];
						dataRow6["Quantity"] = -1m * decimal.Parse(row5["Quantity"].ToString());
						dataRow6["Reference"] = dataRow["Reference"];
						if (result)
						{
							dataRow6["SysDocType"] = (byte)26;
						}
						else if (flag2)
						{
							dataRow6["SysDocType"] = (byte)51;
						}
						else
						{
							dataRow6["SysDocType"] = (byte)25;
						}
						dataRow6["TransactionDate"] = dataRow["TransactionDate"];
						dataRow6["TransactionType"] = (byte)2;
						dataRow6["UnitPrice"] = row5["UnitPrice"];
						dataRow6["Discount"] = row5["Discount"];
						dataRow6["FOCQuantity"] = row5["FOCQuantity"];
						dataRow6["RowIndex"] = row5["RowIndex"];
						dataRow6["StyleID"] = row5["StyleID"];
						dataRow6["SpecificationID"] = row5["SpecificationID"];
						dataRow6["PayeeType"] = "C";
						dataRow6["PayeeID"] = dataRow["CustomerID"];
						dataRow6["DivisionID"] = dataRow["DivisionID"];
						dataRow6["CompanyID"] = dataRow["CompanyID"];
						if (isUpdate)
						{
							dataRow6["CreatedBy"] = dataRow["CreatedBy"];
							dataRow6["DateCreated"] = dataRow["DateCreated"];
						}
						if (row5["UnitQuantity"] != DBNull.Value && row5["UnitFactor"] != DBNull.Value)
						{
							dataRow6["UnitQuantity"] = row5["UnitQuantity"];
							dataRow6["Factor"] = row5["UnitFactor"];
							dataRow6["FactorType"] = row5["FactorType"];
							decimal.Parse(row5["UnitFactor"].ToString());
							row5["FactorType"].ToString();
							decimal d = decimal.Parse(row5["UnitQuantity"].ToString());
							decimal num4 = decimal.Parse(row5["Quantity"].ToString());
							decimal d2 = decimal.Parse(row5["UnitPrice"].ToString());
							decimal num5 = default(decimal);
							num5 = ((!(num4 != 0m)) ? default(decimal) : (d * d2 / num4));
							dataRow6["UnitPrice"] = num5;
						}
						dataRow6.EndEdit();
						inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow6);
					}
					inventoryTransactionData.Merge(salesInvoiceData.Tables["Product_Lot_Issue_Detail"]);
					flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(salesInvoiceData, isUpdate: false, sqlTransaction);
					flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				}
				else
				{
					text = "SELECT IT.RecordID,IT.DocID,IT.InvoiceNumber,IT.RowIndex,ItemCode,SoldQty,IT.FOCQuantity FROM Product_Lot_Sales IT \r\n                                INNER JOIN Sales_Invoice_Detail SID ON IT.DocID = SID.OrderSysDocID AND IT.InvoiceNumber = SID.OrderVoucherID AND IT.RowIndex = SID.OrderRowIndex\r\n                                WHERE SID.SysDocID = '" + text6 + "' AND SID.VoucherID = '" + text5 + "'";
					FillDataSet(dataSet, "FOC", text, sqlTransaction);
					if (!isUpdate)
					{
						foreach (DataRow row6 in salesInvoiceData.Tables["Invoice_DNote"].Rows)
						{
							flag &= (ExecuteNonQuery("UPDATE Delivery_Note SET IsInvoiced='True',InvoiceSysDocID='" + text6 + "',InvoiceVoucherID='" + text5 + "' WHERE SysDocID='" + row6["DNoteSysDocID"].ToString() + "' AND VoucherID='" + row6["DNoteVoucherID"].ToString() + "'", sqlTransaction) >= 0);
						}
					}
					InventoryTransactionData inventoryTransactionData2 = new InventoryTransactionData();
					foreach (DataRow row7 in salesInvoiceData.SalesInvoiceDetailTable.Rows)
					{
						if (row7["IsDNRow"] == DBNull.Value || !bool.Parse(row7["IsDNRow"].ToString()))
						{
							DataRow dataRow9 = inventoryTransactionData2.InventoryTransactionTable.NewRow();
							dataRow9.BeginEdit();
							dataRow9["SysDocID"] = row7["SysDocID"];
							dataRow9["VoucherID"] = row7["VoucherID"];
							if (row7["LocationID"].ToString() == "")
							{
								throw new Exception("Location cannot be empty.");
							}
							dataRow9["SpecificationID"] = row7["SpecificationID"];
							dataRow9["StyleID"] = row7["StyleID"];
							dataRow9["LocationID"] = row7["LocationID"];
							dataRow9["ProductID"] = row7["ProductID"];
							dataRow9["Quantity"] = -1m * decimal.Parse(row7["Quantity"].ToString());
							dataRow9["Reference"] = dataRow["Reference"];
							if (result)
							{
								dataRow9["SysDocType"] = (byte)26;
							}
							else if (flag2)
							{
								dataRow9["SysDocType"] = (byte)51;
							}
							else
							{
								dataRow9["SysDocType"] = (byte)25;
							}
							dataRow9["TransactionDate"] = dataRow["TransactionDate"];
							dataRow9["TransactionType"] = (byte)2;
							dataRow9["UnitPrice"] = row7["UnitPrice"];
							dataRow9["RowIndex"] = row7["RowIndex"];
							dataRow9["DivisionID"] = dataRow["DivisionID"];
							dataRow9["CompanyID"] = dataRow["CompanyID"];
							if (isUpdate)
							{
								dataRow9["CreatedBy"] = dataRow["CreatedBy"];
								dataRow9["DateCreated"] = dataRow["DateCreated"];
							}
							if (row7["UnitQuantity"] != DBNull.Value && row7["UnitFactor"] != DBNull.Value)
							{
								dataRow9["UnitQuantity"] = row7["UnitQuantity"];
								dataRow9["Factor"] = row7["UnitFactor"];
								dataRow9["FactorType"] = row7["FactorType"];
								decimal.Parse(row7["UnitFactor"].ToString());
								row7["FactorType"].ToString();
								decimal d3 = decimal.Parse(row7["UnitQuantity"].ToString());
								decimal num6 = decimal.Parse(row7["Quantity"].ToString());
								decimal d4 = decimal.Parse(row7["UnitPrice"].ToString());
								decimal num7 = default(decimal);
								num7 = ((!(num6 != 0m)) ? default(decimal) : (d3 * d4 / num6));
								dataRow9["UnitPrice"] = num7;
							}
							dataRow9.EndEdit();
							inventoryTransactionData2.InventoryTransactionTable.Rows.Add(dataRow9);
						}
					}
					if (inventoryTransactionData2.InventoryTransactionTable.Rows.Count > 0)
					{
						inventoryTransactionData2.Merge(salesInvoiceData.Tables["Product_Lot_Issue_Detail"]);
						flag &= new Products(base.DBConfig).InsertUpdateProductLotIssueDetail(salesInvoiceData, isUpdate: false, sqlTransaction);
						flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData2, isUpdate, sqlTransaction);
					}
				}
				if (itemSourceTypes == ItemSourceTypes.DeliveryNote)
				{
					text = "UPDATE IT SET UnitPrice= CASE WHEN SID.UnitQuantity IS NOT NULL THEN SID.UnitPrice * SID.UnitQuantity / SID.Quantity ELSE SID.UnitPrice END ,\r\n                                IT.Discount = CASE WHEN SID.UnitQuantity IS NOT NULL THEN SID.Discount * SID.UnitQuantity / SID.Quantity ELSE SID.Discount END ,IT.FOCQuantity = SID.FOCQuantity FROM Inventory_Transactions IT \r\n                                INNER JOIN Sales_Invoice_Detail SID ON IT.SysdocID = SID.OrderSysDocID AND IT.VoucherID = SID.OrderVoucherID AND IT.RowIndex = SID.OrderRowIndex\r\n                                WHERE SID.SysDocID = '" + text6 + "' AND SID.VoucherID = '" + text5 + "'";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
					text = "UPDATE IT SET UnitPrice= CASE WHEN SID.UnitQuantity IS NOT NULL THEN SID.UnitPrice * SID.UnitQuantity / SID.Quantity ELSE SID.UnitPrice END ,\r\n                                IT.Discount = CASE WHEN SID.UnitQuantity IS NOT NULL THEN SID.Discount * SID.UnitQuantity / SID.Quantity ELSE SID.Discount END ,FOCQuantity=NULL FROM Product_Lot_Sales IT \r\n                                INNER JOIN Sales_Invoice_Detail SID ON IT.DocID = SID.OrderSysDocID AND IT.InvoiceNumber = SID.OrderVoucherID AND IT.RowIndex = SID.OrderRowIndex\r\n                                WHERE SID.SysDocID = '" + text6 + "' AND SID.VoucherID = '" + text5 + "'";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				}
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					flag &= ValidateInventoryLockDate(salesInvoiceData, sqlTransaction);
				}
				if (flag4)
				{
					foreach (DataRow row8 in salesInvoiceData.SalesInvoiceDetailTable.Rows)
					{
						row8["RowIndex"].ToString();
						decimal num8 = default(decimal);
						if (row8["FOCQuantity"] != DBNull.Value)
						{
							num8 = decimal.Parse(row8["FOCQuantity"].ToString());
						}
						if (!(num8 == 0m))
						{
							string text12 = row8["OrderSysDocID"].ToString();
							string text13 = row8["OrderVoucherID"].ToString();
							int result11 = -1;
							int.TryParse(row8["OrderRowIndex"].ToString(), out result11);
							if (string.IsNullOrEmpty(text12) && string.IsNullOrEmpty(text13))
							{
								int.TryParse(row8["RowIndex"].ToString(), out result11);
							}
							DataRow[] array = null;
							if (!string.IsNullOrEmpty(text12) && !string.IsNullOrEmpty(text13))
							{
								array = dataSet.Tables[0].Select("DocID = '" + text12 + "' AND InvoiceNumber = '" + text13 + "' AND RowIndex = " + result11);
							}
							else
							{
								text = "SELECT IT.RecordID,IT.DocID,IT.InvoiceNumber,IT.RowIndex,ItemCode,SoldQty,IT.FOCQuantity FROM Product_Lot_Sales IT                                 \r\n                                WHERE IT.DocID = '" + text6 + "' AND IT.InvoiceNumber = '" + text5 + "'";
								FillDataSet(dataSet, "FOC", text, sqlTransaction);
								array = dataSet.Tables[0].Select("RowIndex = " + result11);
							}
							for (int i = 0; i < array.Length; i++)
							{
								if (num8 == 0m)
								{
									array[i]["FOCQuantity"] = 0;
								}
								decimal num9 = decimal.Parse(array[i]["SoldQty"].ToString());
								if (num9 > num8)
								{
									array[i]["FOCQuantity"] = num8;
									num8 = default(decimal);
								}
								else
								{
									array[i]["FOCQuantity"] = num9;
									num8 -= num9;
								}
							}
						}
					}
					text = "UPDATE Product_Lot_Sales SET FOCQuantity = @FOCQuantity WHERE RecordID = @RecordID AND DocID = @DocID AND InvoiceNumber = @InvoiceNumber AND RowIndex= @RowIndex";
					sqlCommand = new SqlCommand(text, base.DBConfig.Connection, sqlTransaction);
					sqlCommand.Parameters.Add("@RecordID", SqlDbType.Int, 18, "RecordID");
					sqlCommand.Parameters.Add("@DocID", SqlDbType.NVarChar, 7, "DocID");
					sqlCommand.Parameters.Add("@InvoiceNumber", SqlDbType.NVarChar, 15, "InvoiceNumber");
					sqlCommand.Parameters.Add("@RowIndex", SqlDbType.Int, 18, "RowIndex");
					sqlCommand.Parameters.Add("@FOCQuantity", SqlDbType.Decimal, 18, "FOCQuantity");
					flag &= Update(dataSet, "FOC", sqlCommand);
				}
				foreach (DataRow row9 in salesInvoiceData.InvoiceDNoteTable.Rows)
				{
					row9.BeginEdit();
					row9["InvoiceSysDocID"] = dataRow["SysDocID"];
					row9["InvoiceVoucherID"] = dataRow["VoucherID"];
					row9["SourceDocType"] = (byte)6;
					row9.EndEdit();
				}
				if (itemSourceTypes == ItemSourceTypes.DeliveryNote)
				{
					text = "DELETE FROM Invoice_DNote WHERE InvoiceSysDocID='" + text6 + "' AND InvoiceVoucherID='" + text5 + "'";
					flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
					if (salesInvoiceData.Tables["Invoice_DNote"].Rows.Count > 0)
					{
						sqlCommand = GetInsertUpdateInvoiceDNoteCommand(isUpdate: false);
						sqlCommand.Transaction = sqlTransaction;
						flag &= Insert(salesInvoiceData, "Invoice_DNote", sqlCommand);
					}
				}
				if (itemSourceTypes == ItemSourceTypes.SalesOrder)
				{
					foreach (DataRow row10 in salesInvoiceData.SalesInvoiceDetailTable.Rows)
					{
						text4 = row10["ProductID"].ToString();
						text2 = row10["OrderVoucherID"].ToString();
						text3 = row10["OrderSysDocID"].ToString();
						int result12 = 0;
						if (!(text2 == "") && !(text3 == ""))
						{
							int.TryParse(row10["OrderRowIndex"].ToString(), out result12);
							float result13 = 0f;
							if (row10["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row10["UnitQuantity"].ToString(), out result13);
							}
							else
							{
								float.TryParse(row10["Quantity"].ToString(), out result13);
							}
							float num10 = new Products(base.DBConfig).GetReservedQuantity(text4, sqlTransaction) - result13;
							if (num10 < 0f)
							{
								num10 = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateReservedQuantity(text4, num10, sqlTransaction);
							flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text3, text2, result12, result13, sqlTransaction);
						}
					}
					text2 = salesInvoiceData.SalesInvoiceDetailTable.Rows[0]["OrderVoucherID"].ToString();
					text3 = salesInvoiceData.SalesInvoiceDetailTable.Rows[0]["OrderSysDocID"].ToString();
					if (text2 != "")
					{
						flag &= new SalesOrder(base.DBConfig).CloseShippedOrder(text3, text2, sqlTransaction);
						flag &= new SalesProformaInvoice(base.DBConfig).CloseShippedOrder(text3, text2, sqlTransaction);
					}
				}
				if (itemSourceTypes == ItemSourceTypes.SalesProformaInvoice)
				{
					text2 = salesInvoiceData.SalesInvoiceDetailTable.Rows[0]["OrderVoucherID"].ToString();
					text3 = salesInvoiceData.SalesInvoiceDetailTable.Rows[0]["OrderSysDocID"].ToString();
					if (text2 != "")
					{
						flag &= new SalesProformaInvoice(base.DBConfig).CloseShippedOrder(text3, text2, sqlTransaction);
					}
				}
				if (salesInvoiceData.Tables.Contains("Tax_Detail"))
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(salesInvoiceData, text6, text5, isUpdate, sqlTransaction);
				}
				GLData journalData = CreateInvoiceGLData(salesInvoiceData, isDNoteInventory, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateInventoryTransactionRowID(text6, text5, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Sales_Invoice", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Sales Invoice";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text5, text6, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text5, text6, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Sales_Invoice", "VoucherID", sqlTransaction);
				}
				if (!isUpdate && TempSave)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateToLastDocumentNumberWithTemporary(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Sales_Invoice", "VoucherID", sqlTransaction);
				}
				if (TempSave && !string.IsNullOrEmpty(dataRow["TempKey"].ToString()))
				{
					flag &= new Settings(base.DBConfig).insertTempDeleteActvity(dataRow["TempKey"].ToString(), flag, sqlTransaction, dataRow["VoucherID"].ToString(), int.Parse(dataRow["AutoKeyID"].ToString()));
				}
				if (flag)
				{
					flag = ((!flag2) ? (flag & new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.SalesInvoice, text6, text5, "Sales_Invoice", sqlTransaction)) : (flag & new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ExportSalesInvoice, text6, text5, "Sales_Invoice", sqlTransaction)));
				}
				flag &= ModifyTransactions(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), dataRow["CurrentUser"].ToString(), isModify: true, "toupdate", sqlTransaction);
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

		private bool ValidateDate(SalesInvoiceData salesInvoiceData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(DateTime.Parse(DateTime.Parse(salesInvoiceData.SalesInvoiceTable.Rows[0]["TransactionDate"].ToString()).ToShortDateString()));
				string text2 = "";
				foreach (DataRow row in salesInvoiceData.InvoiceDNoteTable.Rows)
				{
					string text3 = row["DNoteSysDocID"].ToString();
					string text4 = row["DNoteVoucherID"].ToString();
					text2 = "Select Case When CONVERT(date,TransactionDate) > CONVERT(date,'" + text + "') Then 'True' Else 'False' End From Delivery_Note Where SysDocID='" + text3 + "' AND VoucherID='" + text4 + "'";
					if (bool.Parse(ExecuteScalar(text2, sqlTransaction).ToString()))
					{
						throw new CompanyException("One of the DN date is greater than sales invoice date.");
					}
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		private bool ValidateSoftClosePeriod(SalesInvoiceData salesInvoiceData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				DateTime t = DateTime.Parse(DateTime.Parse(salesInvoiceData.SalesInvoiceTable.Rows[0]["TransactionDate"].ToString()).ToShortDateString());
				new CompanyInformations(base.DBConfig).GetFiscalStartDate();
				SoftClosePeriod softClosePeriod = SoftClosePeriod.Yearly;
				object companyOptionValue = new CompanyOption(base.DBConfig).GetCompanyOptionValue(10102.ToString());
				if (companyOptionValue != null)
				{
					softClosePeriod = (SoftClosePeriod)int.Parse(companyOptionValue.ToString());
				}
				new SqlCommand();
				foreach (DataRow row in salesInvoiceData.InvoiceDNoteTable.Rows)
				{
					bool flag = false;
					string text = row["DNoteSysDocID"].ToString();
					string text2 = row["DNoteVoucherID"].ToString();
					SqlCommand sqlCommand = new SqlCommand("Select CONVERT(date,TransactionDate)  From Delivery_Note Where SysDocID=@SysDocID AND VoucherID=@VoucherID", base.DBConfig.Connection);
					sqlCommand.Parameters.Add("@SysDocID", SqlDbType.VarChar).Value = text;
					sqlCommand.Parameters.Add("@VoucherID", SqlDbType.VarChar).Value = text2;
					sqlCommand.CommandType = CommandType.Text;
					sqlCommand.Transaction = sqlTransaction;
					object obj2 = sqlCommand.ExecuteScalar();
					if (obj2.ToString() != null)
					{
						DateTime dateTime = DateTime.Parse(obj2.ToString());
						switch (softClosePeriod)
						{
						case SoftClosePeriod.Yearly:
							if (dateTime.Year == t.Year)
							{
								flag = true;
							}
							break;
						case SoftClosePeriod.HalfYear:
						{
							int num = (dateTime.Month - 1) / 6 + 1;
							DateTime t4 = new DateTime(dateTime.Year, (num - 1) * 6 + 1, 1);
							DateTime t5 = t4.AddMonths(6).AddDays(-1.0);
							if (t >= t4 && t <= t5)
							{
								flag = true;
							}
							break;
						}
						case SoftClosePeriod.Quarterly:
						{
							int num2 = (dateTime.Month - 1) / 3 + 1;
							DateTime t6 = new DateTime(dateTime.Year, (num2 - 1) * 3 + 1, 1);
							DateTime t7 = t6.AddMonths(3).AddDays(-1.0);
							if (t >= t6 && t <= t7)
							{
								flag = true;
							}
							break;
						}
						case SoftClosePeriod.Monthly:
						{
							DateTime t2 = new DateTime(dateTime.Year, dateTime.Month, 1);
							DateTime t3 = new DateTime(dateTime.Year, dateTime.Month, DateTime.DaysInMonth(dateTime.Year, dateTime.Month));
							if (t > t2 && t < t3)
							{
								flag = true;
							}
							break;
						}
						}
						if (!flag)
						{
							throw new CompanyException("The DN  " + text + "/" + text2 + "   date is out of soft close period.");
						}
					}
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		public bool ValidateInventoryLockDate(SalesInvoiceData salesInvoiceData, SqlTransaction sqlTransaction)
		{
			try
			{
				DataRow dataRow = salesInvoiceData.SalesInvoiceTable.Rows[0];
				string idFieldValue = dataRow["SysDocID"].ToString();
				string checkFieldValue = dataRow["VoucherID"].ToString();
				DateTime dateTime = DateTime.Parse(dataRow["TransactionDate"].ToString());
				DateTime t = dateTime;
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Inventory_Transactions", "TransactionDate", "SysDocID", idFieldValue, "VoucherID", checkFieldValue, sqlTransaction);
				if (!fieldValue.IsNullOrEmpty())
				{
					t = DateTime.Parse(fieldValue.ToString());
				}
				DataSet lastClosingPeriod = new CompanyInformations(base.DBConfig).GetLastClosingPeriod();
				if (lastClosingPeriod.Tables.Count > 0 && lastClosingPeriod.Tables[0].Rows.Count > 0)
				{
					DataRow dataRow2 = lastClosingPeriod.Tables[0].Rows[0];
					DateTime t2 = default(DateTime);
					if (!string.IsNullOrEmpty(dataRow2["InventoryCloseDate"].ToString()))
					{
						t2 = DateTime.Parse(dataRow2["InventoryCloseDate"].ToString());
						t2 = new DateTime(t2.Year, t2.Month, t2.Day, 23, 59, 59);
					}
					if (dateTime <= t2 || t <= t2)
					{
						throw new CompanyException("Cannot record a transaction in a closed period.", 1038);
					}
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		private bool UpdateInventoryTransactionRowID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "UPDATE SID SET ITRowID = CASE WHEN ISNULL(IsDNRow,'False') = 'True' THEN  (SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.OrderSysDocID AND IT.VoucherID = SID.OrderVoucherID AND IT.RowIndex = SID.OrderRowIndex) \r\n                                    ELSE(SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = SID.SysDocID AND IT.VoucherID = SID.VoucherID AND IT.RowIndex = SID.RowIndex) END\r\n                                    FROM Sales_Invoice_Detail SID INNER JOIN Sales_Invoice SI ON SI.SysDocID = SID.SysDocID AND SI.VoucherID = SID.VoucherID\r\n                                     where sid.SysDocID = '" + sysDocID + "' and sid.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		private GLData CreateInvoiceGLData(SalesInvoiceData transactionData, bool isDNoteInventory, SqlTransaction sqlTransaction)
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
				string value4 = dataRow3["DiscountGivenAccountID"].ToString();
				dataRow3["SalesTaxAccountID"].ToString();
				string value5 = dataRow3["ARAccountID"].ToString();
				string text4 = dataRow3["UnInvoicedInventoryAccountID"].ToString();
				string value6 = dataRow3["RoundOffAccountID"].ToString();
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
					isDNoteInventory = false;
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
					num = result6 + result9 - result7;
				}
				else if (text6 != "" && result8 > 0m)
				{
					num = result7;
				}
				if (text6 != "")
				{
					text7 = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text6, sqlTransaction);
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
					bool flag3 = true;
					if (!row3["IsDeduct"].IsDBNullOrEmpty())
					{
						flag3 = Convert.ToBoolean(row3["IsDeduct"].ToString());
					}
					DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					dataRow7["AccountID"] = expenseAccountID;
					string a = (string)(dataRow7["CurrencyID"] = row3["CurrencyID"].ToString());
					dataRow7["CurrencyRate"] = row3["CurrencyRate"];
					dataRow7["JobID"] = value3;
					if (a != text5)
					{
						dataRow7["DebitFC"] = DBNull.Value;
						dataRow7["CreditFC"] = result16;
						if (flag3)
						{
							d3 += result16;
						}
					}
					else
					{
						dataRow7["DebitFC"] = DBNull.Value;
						dataRow7["CreditFC"] = DBNull.Value;
						dataRow7["Debit"] = DBNull.Value;
						dataRow7["Credit"] = result6;
						if (flag3)
						{
							d3 += result6;
						}
					}
					dataRow7["JVEntryType"] = (byte)4;
					dataRow7["Reference"] = text14;
					dataRow7["CompanyID"] = value;
					dataRow7["DivisionID"] = value2;
					dataRow7.EndEdit();
					if (flag3)
					{
						gLData.JournalDetailsTable.Rows.Add(dataRow7);
					}
				}
				if (result15 > 0m)
				{
					DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					dataRow7["AccountID"] = value4;
					dataRow7["PayeeID"] = text;
					dataRow7["PayeeType"] = "A";
					dataRow7["JobID"] = value3;
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
					dataRow7["AccountID"] = value6;
					dataRow7["PayeeID"] = text;
					dataRow7["PayeeType"] = "A";
					dataRow7["JobID"] = value3;
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
					dataRow7["AccountID"] = value5;
					dataRow7["PayeeID"] = text;
					dataRow7["PayeeType"] = "C";
					dataRow7["IsARAP"] = true;
					dataRow7["JobID"] = value3;
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

		public bool ReCostTransaction(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				SalesInvoiceData salesInvoiceByID = GetSalesInvoiceByID(sysDocID, voucherID, sqlTransaction);
				object obj = null;
				DataRow dataRow = salesInvoiceByID.SalesInvoiceTable.Rows[0];
				bool flag2 = false;
				if (dataRow["IsExport"] != DBNull.Value)
				{
					flag2 = bool.Parse(dataRow["IsExport"].ToString());
				}
				string text = "";
				if (flag2)
				{
					obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString());
					if (obj != null)
					{
						text = obj.ToString();
					}
				}
				else
				{
					obj = new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString());
					if (obj != null)
					{
						text = obj.ToString();
					}
				}
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text != "")
				{
					salesFlows = (SalesFlows)int.Parse(text.ToString());
				}
				bool isDNoteInventory = false;
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					isDNoteInventory = true;
				}
				GLData gLData = CreateInvoiceGLData(salesInvoiceByID, isDNoteInventory, sqlTransaction);
				string exp = "SELECT JournalID FROM Journal WHERE SysDocID = '" + sysDocID + "' AND VOucherID = '" + voucherID + "'";
				obj = ExecuteScalar(exp, sqlTransaction);
				if (obj.IsDBNullOrEmpty())
				{
					throw new CompanyException("JournalID not found for invoice '" + voucherID + "'");
				}
				int.Parse(obj.ToString());
				if (!new Journal(base.DBConfig).IsJournalInOpenPeriod(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Transaction date is in a period which is locked or closed.");
				}
				exp = " SELECT JournalID,JournalDetailID,SysDocID,VoucherID,JD.AccountID,AC.AccountName,Debit,Credit FROM Journal_Details JD INNER JOIN Account AC ON JD.AccountID = AC.AccountID\r\n                         WHERE AC.subType IN (6, 8) AND SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Invoice", exp, sqlTransaction);
				if (dataSet.Tables[0].Rows.Count != 2)
				{
					throw new CompanyException("Could not match the COGS and Inventory Asset accounts.");
				}
				decimal d = default(decimal);
				foreach (DataRow row in dataSet.Tables[0].Rows)
				{
					int num = int.Parse(row["JournalDetailID"].ToString());
					string text2 = row["AccountID"].ToString();
					DataRow[] array = gLData.JournalDetailsTable.Select("AccountID = '" + text2 + "'");
					if (array.Length == 0)
					{
						throw new CompanyException("Accounts not found. maybe changed.");
					}
					DataRow dataRow2 = array[0];
					if (!dataRow2["Debit"].IsDBNullOrEmpty())
					{
						d += decimal.Parse(dataRow2["Debit"].ToString());
					}
					if (!dataRow2["Credit"].IsDBNullOrEmpty())
					{
						d -= decimal.Parse(dataRow2["Credit"].ToString());
					}
					exp = " UPDATE Journal_Details SET Debit = ";
					exp = ((!dataRow2["Debit"].IsDBNullOrEmpty()) ? (exp + dataRow2["Debit"].ToString()) : (exp + " NULL "));
					exp = ((!dataRow2["Credit"].IsDBNullOrEmpty()) ? (exp + " , Credit = " + dataRow2["Credit"].ToString()) : (exp + " , Credit = NULL "));
					exp = exp + "  WHERE JournalDetailID = " + num + " and AccountID = '" + text2 + "'";
					flag &= (ExecuteNonQuery(exp, sqlTransaction) > 0);
				}
				bool flag3 = new Journal(base.DBConfig).IsJournalInBalance(sysDocID, voucherID, sqlTransaction);
				if (d != 0m || !flag3)
				{
					flag = false;
					throw new CompanyException("Debit and credit not in balance.");
				}
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

		internal bool UpdateRowReturnedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float result2 = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReturned FROM Sales_Invoice_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				textCommand = "UPDATE Sales_Invoice_Detail SET QuantityReturned=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public SalesInvoiceData GetSalesInvoiceByID(string sysDocID, string voucherID)
		{
			return GetSalesInvoiceByID(sysDocID, voucherID, null);
		}

		internal SalesInvoiceData GetSalesInvoiceByID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				SalesInvoiceData salesInvoiceData = new SalesInvoiceData();
				string text = "SELECT INV.* FROM Sales_Invoice INV  WHERE INV.VoucherID='" + voucherID + "' AND INV.SysDocID='" + sysDocID + "'";
				new SqlCommand(text).Transaction = sqlTransaction;
				FillDataSet(salesInvoiceData, "Sales_Invoice", text, sqlTransaction);
				if (salesInvoiceData == null || salesInvoiceData.Tables.Count == 0 || salesInvoiceData.Tables["Sales_Invoice"].Rows.Count == 0)
				{
					return null;
				}
				text = "SELECT distinct SID.* ,Product.Description,Product.ItemType,Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID,SID.TaxOption,\r\n                            CASE WHEN ItemType = 5 THEN 'True' ELSE IsTrackLot END AS IsTrackLot,IsTrackSerial,BrandName AS Brand,\r\n                            CON.LotNumber, CON.ReceiptNumber AS ConsignNumber\r\n                            FROM Sales_Invoice_Detail SID INNER JOIN Product ON SID.ProductID=Product.ProductID\r\n                            LEFT OUTER JOIN  Product_Lot_Issue_Detail PLS ON PLS.SysDocID = SID.OrderSysDocID AND PLS.VoucherID = SID.OrderVoucherID AND SID.OrderRowIndex = PLS.RowIndex\r\n                            LEFT OUTER JOIN Product_Lot CON ON CON.LotNumber = (SELECT TOP 1 CASE WHEN SourceLotNumber IS NULL THEN LotNumber ELSE SourceLotNumber END FROM Product_Lot_Issue_Detail PLS2 WHERE PLS2.SysDocID = SID.OrderSysDocID AND PLS2.VoucherID = SID.OrderVoucherID AND SID.OrderRowIndex = PLS2.RowIndex)\r\n                            LEFT OUTER JOIN Product_Brand Brand ON Brand.BrandID = Product.BrandID\r\n                        WHERE SID.VoucherID='" + voucherID + "' AND SID.SysDocID='" + sysDocID + "' ORDER BY SID.RowIndex ";
				FillDataSet(salesInvoiceData, "Sales_Invoice_Detail", text, sqlTransaction);
				text = "SELECT SI.SysDocID,SI.VoucherID,TransactionDate,RegisterID,ISNULL(TotalFC,Total) As Amount,PaymentMethodType\r\n                        FROM Sales_Invoice SI\r\n                        WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesInvoiceData, "Invoice_Payment", text, sqlTransaction);
				text = "SELECT * FROM Invoice_DNote\r\n                        WHERE InvoiceVoucherID='" + voucherID + "' AND InvoiceSysDocID='" + sysDocID + "'";
				FillDataSet(salesInvoiceData, "Invoice_DNote", text);
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (salesInvoiceData.SalesInvoiceTable.Rows[0]["SalesFlow"] != DBNull.Value)
				{
					salesFlows = (SalesFlows)int.Parse(salesInvoiceData.SalesInvoiceTable.Rows[0]["SalesFlow"].ToString());
				}
				if (salesFlows == SalesFlows.SOThenDNThenInvoice)
				{
					text = "select SysDocID, VoucherID FROM Delivery_Note DN with (nolock)where   DN.InvoiceSysDocID = '" + sysDocID + "' AND DN.InvoiceVoucherID = '" + voucherID + "'";
					DataSet dataSet = new DataSet();
					FillDataSet(dataSet, "DN", text, sqlTransaction);
					if (dataSet != null && dataSet.Tables["DN"].Rows.Count != 0)
					{
						string text2 = dataSet.Tables["DN"].Rows[0]["SysDocID"].ToString();
						string text3 = dataSet.Tables["DN"].Rows[0]["VoucherID"].ToString();
						text = "SELECT PL.Reference AS LotReference,PLD.*,PL.ProductionDate,PL.ExpiryDate,PL.ReceiptDate,\r\n                        CASE WHEN PL.SourceLotNumber IS NULL THEN PL.ReceiptNumber ELSE \r\n\t\t\t\t\t\t(SELECT ReceiptNumber FROM Product_Lot PL2 WHERE PL2.LotNumber = PL.SourceLotNumber) END  AS Consign#\r\n                        FROM Product_Lot_Issue_Detail PLD INNER JOIN Product_Lot PL ON PL.LotNumber = PLD.LotNumber\r\n\t\t\t\t\t\tWHERE  SysDocID = '" + text2 + "' AND  VoucherID = '" + text3 + "'";
						FillDataSet(salesInvoiceData, "Product_Lot_Issue_Detail", text);
					}
				}
				else
				{
					DataSet transactionIssuesProductLots = new Products(base.DBConfig).GetTransactionIssuesProductLots(sysDocID, voucherID);
					if (salesInvoiceData.Tables.Contains("Product_Lot_Issue_Detail"))
					{
						salesInvoiceData.Tables.Remove("Product_Lot_Issue_Detail");
					}
					salesInvoiceData.Merge(transactionIssuesProductLots, preserveChanges: false);
				}
				text = "SELECT *,ISNULL(IsDeduct,1) AS Deductable  FROM   Sales_Invoice_Expense\r\n\t\t\t\t\t\tWHERE InvoiceVoucherID='" + voucherID + "' AND InvoiceSysDocID='" + sysDocID + "'";
				FillDataSet(salesInvoiceData, "Sales_Invoice_Expense", text);
				text = "SELECT DISTINCT PO.VoucherID,PO.SysDocID FROM SalesProforma_Invoice PO INNER JOIN Sales_Invoice_Detail PQD ON PQD.OrderVoucherID=PO.VoucherID AND PQD.OrderSysDocID=PO.SysDocID WHERE PQD.VoucherID='" + voucherID + "' AND PQD.SysDocID='" + sysDocID + "'";
				FillDataSet(salesInvoiceData, "SalesProforma_Invoice", text);
				text = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(salesInvoiceData, "Tax_Detail", text);
				return salesInvoiceData;
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
				SalesInvoiceData salesInvoiceData = new SalesInvoiceData();
				string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid,ISNULL(IsCash,'False')AS IsCash,ISNULL(IsExport,'False') AS IsExport FROM Sales_Invoice_Detail SOD INNER JOIN Sales_Invoice SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(salesInvoiceData, "Sales_Invoice_Detail", textCommand, sqlTransaction);
				if (salesInvoiceData.SalesInvoiceDetailTable.Rows.Count == 0)
				{
					return true;
				}
				bool result = false;
				bool.TryParse(salesInvoiceData.SalesInvoiceDetailTable.Rows[0]["IsExport"].ToString(), out result);
				string text = "";
				text = ((!result) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text != "")
				{
					salesFlows = (SalesFlows)int.Parse(text.ToString());
				}
				bool result2 = false;
				bool.TryParse(salesInvoiceData.SalesInvoiceDetailTable.Rows[0]["IsVoid"].ToString(), out result2);
				bool result3 = false;
				bool.TryParse(salesInvoiceData.SalesInvoiceDetailTable.Rows[0]["IsCash"].ToString(), out result3);
				if (!result2)
				{
					flag = (result3 ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(26, sysDocID, voucherID, isDeleteTransaction, sqlTransaction)) : ((!result) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(25, sysDocID, voucherID, isDeleteTransaction, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(51, sysDocID, voucherID, isDeleteTransaction, sqlTransaction))));
					string text2 = "";
					string text3 = "";
					string text4 = "";
					foreach (DataRow row in salesInvoiceData.SalesInvoiceDetailTable.Rows)
					{
						text4 = row["ProductID"].ToString();
						text2 = row["OrderVoucherID"].ToString();
						text3 = row["OrderSysDocID"].ToString();
						int result4 = 0;
						if (!(text2 == "") && !(text3 == ""))
						{
							int.TryParse(row["OrderRowIndex"].ToString(), out result4);
							float result5 = 0f;
							if (row["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row["UnitQuantity"].ToString(), out result5);
							}
							else
							{
								float.TryParse(row["Quantity"].ToString(), out result5);
							}
							float num = new Products(base.DBConfig).GetReservedQuantity(text4, sqlTransaction) + result5;
							if (num < 0f)
							{
								num = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateReservedQuantity(text4, num, sqlTransaction);
							if (salesFlows == SalesFlows.SOThenInvoiceThenDN)
							{
								flag &= new SalesOrder(base.DBConfig).UpdateRowShippedQuantity(text3, text2, result4, -1f * result5, sqlTransaction);
								flag &= new SalesOrder(base.DBConfig).ReOpenOrder(text3, text2, sqlTransaction);
							}
						}
					}
				}
				foreach (DataRow row2 in salesInvoiceData.SalesInvoiceDetailTable.Rows)
				{
					string text5 = "";
					string text6 = "";
					text5 = row2["OrderVoucherID"].ToString();
					text6 = row2["OrderSysDocID"].ToString();
					if (!(text5 == "") && !(text6 == ""))
					{
						flag &= new SalesProformaInvoice(base.DBConfig).ReOpenOrder(text6, text5, sqlTransaction);
					}
				}
				textCommand = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				string exp = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(exp, sqlTransaction) >= 0);
				textCommand = "DELETE FROM Sales_Invoice_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				return flag & new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeleteSalesInvoiceExpenseRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				string commandText = "DELETE FROM Sales_Invoice_Expense WHERE InvoiceSysDocID = '" + sysDocID + "' AND InvoiceVoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
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
				object fieldValue = new Databases(base.DBConfig).GetFieldValue("Sales_Invoice", "IsExport", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction);
				if (fieldValue != null && fieldValue.ToString() != "")
				{
					flag3 = bool.Parse(fieldValue.ToString());
				}
				string text = "";
				text = ((!flag3) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(56.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(57.ToString()).ToString());
				SalesFlows salesFlows = SalesFlows.DirectInvoice;
				if (text != "")
				{
					salesFlows = (SalesFlows)int.Parse(text.ToString());
				}
				SalesInvoiceData salesInvoiceData = new SalesInvoiceData();
				string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid,ISNULL(IsCash,'False') AS IsCash FROM Sales_Invoice_Detail SOD INNER JOIN Sales_Invoice SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n                              WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(salesInvoiceData, "Sales_Invoice_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(salesInvoiceData.SalesInvoiceDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(salesInvoiceData.SalesInvoiceDetailTable.Rows[0]["IsCash"].ToString(), out result2);
				if (InvoiceHasShippedQuantity(sysDocID, voucherID, sqlTransaction))
				{
					throw new CompanyException("Unable to modify. Some of the items in this invoice has been already shipped.");
				}
				if (result == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				if (salesFlows != SalesFlows.SOThenDNThenInvoice)
				{
					flag = (result2 ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(26, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : ((!flag3) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(25, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(51, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction))));
					string text2 = "";
					string text3 = "";
					string text4 = "";
					foreach (DataRow row in salesInvoiceData.SalesInvoiceDetailTable.Rows)
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
				}
				else
				{
					flag = (result2 ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(26, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : ((!flag3) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(25, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(51, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction))));
					textCommand = "UPDATE Delivery_Note SET IsInvoiced='False',InvoiceSysDocID=NULL,InvoiceVoucherID=NULL WHERE InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "'";
					flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				}
				textCommand = "DELETE FROM Invoice_DNote WHERE InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				textCommand = "DELETE FROM Product_Lot_Issue_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Unallocated_Lot_Items WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				textCommand = "UPDATE Sales_Invoice SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
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
				AddActivityLog("Sales Invoice", voucherID, sysDocID, activityType, sqlTransaction);
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
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Sales_Invoice", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidSalesInvoice(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeleteSalesInvoiceDetailsRows(sysDocID, voucherID, isDeleteTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Sales_Invoice WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= DeleteSalesInvoiceExpenseRows(sysDocID, voucherID, sqlTransaction);
				flag &= new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Sales Invoice", voucherID, sysDocID, activityType, sqlTransaction);
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
				string exp = "SELECT COUNT(RowIndex)FROM Sales_Invoice_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                                AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Sales_Invoice_Detail SOD2 WHERE SOD.SysDocID=SOD2.SysDocID AND SOD.VoucherID=SOD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityShipped,0) ) FROM Sales_Invoice_Detail SOD3 WHERE SOD.SysDocID=SOD3.SysDocID AND SOD.VoucherID=SOD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				exp = "UPDATE Sales_Invoice SET IsDelivered = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityShipped FROM Sales_Invoice_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				textCommand = "UPDATE Sales_Invoice_Detail SET QuantityShipped=" + result2.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetInvoicesForDeliveryOnLocation(string customerID, bool isExport, string locationID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer] FROM Sales_Invoice SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID  WHERE ISNULL(IsDelivered,0)=0 AND ISNULL(IsExport,'False')= '" + isExport.ToString() + "' ";
				if (customerID != "")
				{
					text = text + " AND SO.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "Sales_Invoice", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInvoicesForDelivery(string customerID, bool isExport)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.CustomerID + '-' + C.CustomerName AS [Customer] FROM Sales_Invoice SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID  WHERE ISNULL(IsDelivered,0)=0 AND ISNULL(IsExport,'False')= '" + isExport.ToString() + "' ";
				if (customerID != "")
				{
					text = text + " AND SO.CustomerID='" + customerID + "'";
				}
				FillDataSet(dataSet, "Sales_Invoice", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetInvoicesForBillDiscountOnLocation(bool isExport, DateTime fromDate, DateTime toDate)
		{
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(fromDate);
				string text2 = StoreConfiguration.ToSqlDateTimeString(toDate);
				DataSet dataSet = new DataSet();
				string textCommand = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.DueDate,SO.CurrencyID, SO.Total,SO.CustomerID, C.CustomerName AS [Customer] FROM Sales_Invoice SO\r\n                             INNER JOIN Customer C ON SO.CustomerID=C.CustomerID  WHERE ISNULL(IsDelivered,0)=0 AND ISNULL(IsExport,'False')= '" + isExport.ToString() + "' AND SO.TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
				FillDataSet(dataSet, "Sales_Invoice", textCommand);
				return dataSet;
			}
			catch
			{
				throw;
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

		public DataSet GetSalesInvoiceToPrint(string sysDocID, string voucherID, bool mergeMatrixItems, bool showLotDetail)
		{
			return GetSalesInvoiceToPrint(sysDocID, new string[1]
			{
				voucherID
			}, mergeMatrixItems, showLotDetail);
		}

		public DataSet GetSalesInvoiceToPrint(string sysDocID, string voucherID, bool showLotDetail)
		{
			return GetSalesInvoiceToPrint(sysDocID, new string[1]
			{
				voucherID
			}, mergeMatrixItems: false, showLotDetail);
		}

		public DataSet GetSalesInvoiceToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems, bool showLotDetail)
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
				string cmdText = "SELECT  DISTINCT   SI.SysDocID,SI.VoucherID,SD.LocationID AS SysLocID,SI.DueDate, L.LocationName AS SysLocationName, SI.CustomerID,Customer.CreditAmount,CustomerName,Customer.TaxIDNumber as CTaxIDNo, SI.PONumber,CA.ContactName,CustomerAddress,ISNULL(SI.IsWeightInvoice,'False') AS IsWeightInvoice, SP.FullName AS [Sales Person], (select ISNULL(ISNULL(sum(SE.Amount),sum(SE.AmountFC)),0)  FROM Sales_Invoice_Expense SE  \r\n                                WHERE SE.InvoiceSysDocID = SI.SysDocID AND SE.InvoiceVoucherID = SI.VoucherID GROUP BY SE.InvoiceVoucherID) [Total Expense], TransactionDate,'' AS 'GTransactionDate', \r\n                                SUBSTRING((SELECT DISTINCT ' ' + D.VoucherID + ' / ' +  convert(varchar,  ( D.TransactionDate) , 106) + ', ' \r\n                                FROM Sales_Invoice_Detail S  INNER JOIN Delivery_Note  D ON S.OrderSysDocID = D.SysDocID AND S.OrderVoucherID = D.VoucherID \r\n                                WHERE S.VoucherID = SI.VoucherID AND S.SysDocID = SI.SysDocID FOR XML PATH('')),2,20000) AS  DElvery_detials,\r\n                                SUBSTRING((SELECT DISTINCT  ' ' + convert(varchar,  ( D.TransactionDate) , 106) + ', ' \r\n                                FROM Sales_Invoice_Detail S  INNER JOIN Delivery_Note  D ON S.OrderSysDocID = D.SysDocID AND S.OrderVoucherID = D.VoucherID \r\n                                WHERE S.VoucherID = SI.VoucherID AND S.SysDocID = SI.SysDocID FOR XML PATH('')),2,20000) AS  DeliveryDate,\r\n                                (SELECT DISTINCT  D.TransactionDate \r\n                                FROM Sales_Invoice_Detail S  INNER JOIN SalesProforma_Invoice  D ON S.OrderSysDocID = D.SysDocID AND S.OrderVoucherID = D.VoucherID \r\n                                WHERE S.VoucherID = SI.VoucherID AND S.SysDocID = SI.SysDocID ) AS  ProformaDate,\r\n                                SUBSTRING((SELECT DISTINCT ' ' + D.VoucherID +  ', ' \r\n                                FROM Sales_Invoice_Detail S  INNER JOIN Delivery_Note  D ON S.OrderSysDocID = D.SysDocID AND S.OrderVoucherID = D.VoucherID \r\n                                WHERE S.VoucherID = SI.VoucherID  AND S.SysDocID = SI.SysDocID FOR XML PATH('')),2,20000) AS  Delivery_No,\r\n                                (SELECT TOP 1 V.RegistrationNumber FROM Delivery_Note DL LEFT JOIN Vehicle V ON DL.VehicleID=V.VehicleID INNER JOIN Sales_Invoice_Detail SLD ON SLD.OrderSysDocID = DL.SysDocID AND SLD.OrderVoucherID = DL.VoucherID\r\n                                WHERE SLD.VoucherID = SI.VoucherID AND SLD.SysDocID=SI.SysDocID )  AS [Vehicle No],\r\n                                (SELECT TOP 1 D.DriverName  FROM Delivery_Note DL LEFT JOIN Driver D ON DL.DriverID=D.DriverID INNER JOIN Sales_Invoice_Detail SLD ON SLD.OrderSysDocID = DL.SysDocID AND SLD.OrderVoucherID = DL.VoucherID\r\n                                WHERE SLD.VoucherID = SI.VoucherID AND SLD.SysDocID=SI.SysDocID ) AS [Driver Name],\r\n                                IsCash,SI.SalesPersonID,RequiredDate,CA.AddressPrintFormat AS ShippingAddress,ShippingMethodName,\r\n                                (SELECT Top 1 Total FROM Sales_Order WHERE VoucherID = SI.Reference) AS [Order Amount],\r\n                                (SELECT Top 1 TransactionDate FROM Sales_Order WHERE VoucherID =SI.Reference) AS [Order Date],(SELECT  DISTINCT TOP 1 SO.TransactionDate FROM Sales_Invoice_Detail SND \r\n                                LEFT JOIN Delivery_Note_Detail DND ON SND.OrderSysDocID=DND.SysDocID AND SND.OrderVoucherID=DND.VoucherID\r\n                                LEFT OUTER JOIN  Sales_Order SO ON DND.SourceSysDocID=SO.SysDocID AND DND.SourceVoucherID=SO.VoucherID\r\n                                WHERE SND.SysDocID=SI.SysDocID AND SND.VoucherID=SI.VoucherID) AS [SalesOrderDate],\r\n                                (SELECT  DISTINCT TOP 1 SO.Reference FROM Sales_Invoice_Detail SND \r\n                                LEFT JOIN Delivery_Note_Detail DND ON SND.OrderSysDocID=DND.SysDocID AND SND.OrderVoucherID=DND.VoucherID\r\n                                LEFT OUTER JOIN  Sales_Order SO ON DND.SourceSysDocID=SO.SysDocID AND DND.SourceVoucherID=SO.VoucherID\r\n                                WHERE SND.SysDocID=SI.SysDocID AND SND.VoucherID=SI.VoucherID) AS [Quotation No],\r\n                                (SELECT  DISTINCT TOP 1 SO.SysDocID+'-'+ SO.VoucherID FROM Sales_Invoice_Detail SND \r\n                                LEFT JOIN Delivery_Note_Detail DND ON SND.OrderSysDocID=DND.SysDocID AND SND.OrderVoucherID=DND.VoucherID\r\n                                LEFT OUTER JOIN  Sales_Order SO ON DND.SourceSysDocID=SO.SysDocID AND DND.SourceVoucherID=SO.VoucherID\r\n                                WHERE SND.SysDocID=SI.SysDocID AND SND.VoucherID=SI.VoucherID) AS [SalesOrder No],\r\n                                ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,SI.CurrencyRate,\r\n                                SI.TermID,TermName,IsVoid,SI.Reference,ISNULL(SI.DiscountFC,SI.Discount) AS Discount,\r\n                                ISNULL(ISNULL(TaxAmountFC,TaxAmount) ,0) AS Tax,ISNULL(RoundOff,0) as RoundOff,ISNULL(TotalFC,Total) AS Total,ISNULL(TotalFC,Total) - ISNULL(ISNULL(SI.DiscountFC,SI.Discount),0) AS GrandTotal,SI.PONumber,SI.Note, \r\n                                Reference2, ISNULL(CA.Phone1, CA.Phone2) CustomerPhoneNo, CA.Email,CA.Fax, SI.SalespersonID,CA.Mobile,CA.PostalCode,\r\n                                SI.DateCreated,SI.DateUpdated,SI.CreatedBy,SI.UpdatedBy,J.JobName,JC.CostCategoryName,\r\n\r\n                                ISNULL((SELECT  SUM(ISNULL(Debit,0)- ISNULL(Credit,0))  FROM ARJournal \r\n                                WHERE CustomerID = SI.CustomerID and ISNULL(isvoid,'False') = 'False' AND ISNULL(IsPDCRow,'False') = 'False'),0) +\r\n                                ISNULL((SELECT  SUM(ISNULL(Debit,0)- ISNULL(Credit,0)) AS Balance FROM ARJournal  WHERE CustomerID IN \r\n                                (Select CustomerID FROM Customer WHERE ParentCustomerID = SI.CustomerID)  and ISNULL(isvoid,'False') = 'False' AND ISNULL(IsPDCRow,'False') = 'False'),0) AS Balance,\r\n                                ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) AS Amount FROM Cheque_Received ChqRec\r\n                                WHERE Status IN (1,3,4,8) AND ISNULL(IsVoid,'False')='False' AND PayeeType='C' AND PayeeID = SI.CustomerID),0) + \r\n                                ISNULL((SELECT SUM(ISNULL(AmountFC,Amount)) AS Amount FROM Cheque_Received ChqRec\r\n                                WHERE Status IN (1,3,4,8) AND ISNULL(IsVoid,'False')='False' AND PayeeType='C' AND PayeeID IN (Select CustomerID FROM Customer WHERE ParentCustomerID = SI.CustomerID)),0)  AS PDCAmount,SI.DueDate,SI.PaymentMethodType,SI.ShipToAddress,CA.Comment, CA.AddressName,SI.PriceIncludeTax,\r\n                                SI.DriverID, D.DriverName, D.Note, SI.VehicleID, V.VehicleName,Si.PayeeTaxGroupID AS PayeeTaxGrpID, TG.TaxGroupName,Customer.ShortName,J.Note [Job Note] \r\n                                FROM  Sales_Invoice SI INNER JOIN Customer ON SI.CustomerID=Customer.CustomerID\r\n                                LEFT OUTER Join Tax_Group TG ON TG.TaxGroupID=SI.PayeeTaxGroupID\r\n                                LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                                LEFT OUTER JOIN Customer_Address CA ON CA.AddressID=ShippingAddressID AND CA.CustomerID=SI.CustomerID\r\n                                LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                                LEFT OUTER JOIN Salesperson SP ON SP.SalespersonID=SI.SalespersonID\r\n                                LEFT JOIN Job J ON J.JobID=SI.JobID\r\n                                LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=SI.CostCategoryID\r\n                                LEFT JOIN System_Document SD On SD.SysDocID=SI.SysDocID\r\n                                LEFT JOIN Location L On SD.LocationID=L.LocationID\r\n                                Left outer join Driver D on SI.DriverID=D.DriverID\r\n\t\t\t\t\t\t\t\tLeft Outer JOIN Vehicle V ON SI.VehicleID=V.VehicleID\r\n                                WHERE SI.SysDocID = '" + sysDocID + "' AND VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Sales_Invoice", sqlCommand);
				DateTime dateTime = Convert.ToDateTime(dataSet.Tables["Sales_Invoice"].Rows[0]["TransactionDate"].ToString());
				dataSet.Tables["Sales_Invoice"].Rows[0]["GTransactionDate"] = dateTime;
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Sales_Invoice"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = ((!mergeMatrixItems) ? ("SELECT DISTINCT SINVD.SysDocID, SINVD.VoucherID, SINVD.ProductID,SINVD.Remarks,SINVD.TaxAmount, SINVD.TaxGroupID,TG.TaxGroupName, P.CategoryID,PB.BrandName,P.BrandID, PPC.ProductParentID,P.ItemType,\r\n                            PP.Description AS ParentDescription, P.Description2, convert(nvarchar(max),P.Note) AS Note,\r\n                       \r\n                            (SELECT TOP 1 PLD.CustomerProductID FROM Price_List_Detail PLD INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID \r\n                             WHERE ISNULL(PL.Inactive, 'True') = 'False' AND PL.CustomerID = SI.CustomerID AND  PLD.ProductID = SINVD.ProductID) CustomerProductID, \r\n                            P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,C.CountryName,PC.CategoryName,\r\n                            (SELECT TOP 1 CASE WHEN PU.FactorType='M' \r\n                            THEN ('1 '+'  '+ P.UnitID +'= '+CAST (PU.Factor AS varchar)+' '+PU.UnitID)  \r\n                            ELSE ('1'+'  '+PU.UnitID +'= '+ CAST(PU.Factor AS varchar)+' '+P.UnitID) END \r\n                            AS PACKING from Product_Unit PU WHERE P.ProductID=PU.ProductID )  AS [Packing]\r\n                            ,P.UnitID AS MainUnit,\r\n                            (SELECT TOP 1  PU.Factor from Product_Unit PU WHERE P.ProductID=PU.ProductID ) AS Factor,(SELECT TOP 1  PU.UnitID from Product_Unit PU WHERE P.ProductID=PU.ProductID ) AS SubUnit,P.Size,\r\n                            (SELECT CASE WHEN FactorType='D' THEN ISNULL(UnitQuantity,SINVD.Quantity)*Factor ELSE ISNULL(UnitQuantity,SINVD.Quantity)/Factor END \r\n                            FROM Product_Unit PU WHERE PU.UnitID=SINVD.UnitID AND PU.ProductID=SINVD.ProductID ) AS Weight, \r\n                            SINVD.Description,ISNULL(SINVD.UnitQuantity, SINVD.Quantity) - ISNULL(FOCQuantity, 0) AS Quantity, ISNULL(SINVD.UnitPriceFC,SINVD.UnitPrice) AS UnitPrice, SINVD.FOCQuantity, SINVD.ConsignmentNo,\r\n                            (ISNULL(SINVD.UnitQuantity,SINVD.Quantity) - ISNULL(FOCQuantity, 0)) * ISNULL(SINVD.UnitPriceFC,SINVD.UnitPrice) AS Total,SINVD.UnitID,LocationID, \r\n                            SINVD.WeightQuantity, SINVD.WeightPrice, SINVD.RowIndex,ISNULL(SINVD.OrderSysdocID,SINVD.SysDocID) as OrderSysdocID,\r\n                            ISNULL(SINVD.OrderVoucherID,SINVD.VoucherID) as OrderVoucherID,J.JobName,JC.CostCategoryName, \r\n                            (select TransactionDate from Delivery_Note where SysDocID=SINVD.OrderSysDocID AND VoucherID=SINVD.OrderVoucherID) AS  DeliveryDate, SINVD.SpecificationID, SpecificationName,(SELECT TOP 1 PLD.Description  FROM Price_List_Detail PLD INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID \r\n                             WHERE ISNULL(PL.Inactive, 'True') = 'False' AND PL.CustomerID = SI.CustomerID AND  PLD.ProductID = SINVD.ProductID) CustomerProductDesc, \r\n\r\n                            (SELECT TOP 1 PLD.Remarks  FROM Price_List_Detail PLD INNER JOIN Price_List PL ON PLD.SysDocID = PL.SysDocID AND PLD.VoucherID = PL.VoucherID \r\n                             WHERE ISNULL(PL.Inactive, 'True') = 'False' AND PL.CustomerID = SI.CustomerID AND  PLD.ProductID = SINVD.ProductID) remarks, P.UPC,CAST(P.photo AS VARBINARY(Max)) AS photo,PST.StyleName [Style],F.GenericListName as Finish,SINVD.Amount as TotalRounded ,\r\n                                SINVD.RefDate1, SINVD.RefDate2, SINVD.RefNum1, SINVD.RefNum2,\r\n                                SINVD.RefSlNo, SINVD.RefText1, SINVD.RefText2\r\n                            FROM   Sales_Invoice_Detail SINVD INNER JOIN Sales_Invoice SI ON SINVD.SysDocID = SI.SysDocID AND SINVD.VoucherID = SI.VoucherID\r\n                            INNER JOIN Product P ON P.ProductID = SINVD.ProductID\r\n                            LEFT OUTER JOIN Product_Parent_Components PPC ON SINVD.ProductID=PPC.ProductID\r\n                            LEFT OUTER JOIN Product_Parent PP ON PP.ProductParentID = PPC.ProductParentID\r\n                            LEFT OUTER JOIN Product_Category PC ON PC.CategoryID=P.CategoryID\r\n                            LEFT OUTER JOIN Country C ON P.Origin=C.CountryID \r\n                            LEFT OUTER JOIN Product_Brand PB ON P.BrandID=PB.BrandID\r\n                            LEFT JOIN Job J ON J.JobID=SINVD.JobID\r\n                            LEFT JOIN Job_Cost_Category JC ON JC.CostCategoryID=SINVD.CostCategoryID\r\n                            LEFT JOIn Tax_Group TG ON SINVD.TaxGroupID=TG.TaxGroupID\r\n\t\t\t\t\t\t\tLEFT JOIN Product_Specification PS ON PS.SpecificationID=SINVD.SpecificationID\r\n                            LEFT JOIN Product_Style PST ON PST.StyleID=SINVD.StyleID\r\n                            LEFT JOIN (SELECT GenericListID,GenericListName FROM Generic_List WHERE GenericListtYPE=33) AS F ON P.FinishingID= F.GenericListID\r\n                            WHERE SINVD.SysDocID='" + sysDocID + "' AND SINVD.VoucherID IN (" + text + ")  ORDER BY SINVD.RowIndex") : ("SELECT SysDocID,VoucherID, ISNULL(PPC.ProductParentID,SINVD.ProductID) AS ProductID,ISNULL(PP.Description,SINVD.Description) AS Description, \r\n                            SUM(ISNULL(UnitQuantity, SINVD.Quantity)) -  SUM(ISNULL(FOCQuantity, 0)) AS Quantity,SUM(ISNULL(UnitQuantity, SINVD.Quantity)) AS DeliveredQty,\r\n                            \r\n                            WeightQuantity,WeightPrice, ISNULL(UnitPriceFC,UnitPrice) AS UnitPrice, SINVD.FOCQuantity, SINVD.ConsignmentNo,\r\n                            (SUM(ISNULL(UnitQuantity,SINVD.Quantity)) -  SUM(ISNULL(FOCQuantity, 0))) * ISNULL(UnitPriceFC,UnitPrice) AS Total, SINVD.UnitID,LocationID,P.Size, RowIndex\r\n                            FROM   Sales_Invoice_Detail SINVD\r\n\t\t\t\t\t\t    INNER JOIN Product P ON SINVD.ProductID = P.ProductID\r\n\t\t\t\t\t\t    LEFT OUTER JOIN Product_Parent_Components PPC ON SINVD.ProductID= PPC.ProductID AND P.MatrixParentID = PPC.ProductParentID\r\n\t\t\t\t\t\t    LEFT OUTER JOIN Product_Parent PP ON PP.ProductParentID = PPC.ProductParentID\r\n                            WHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")\r\n                            AND PP.IsInactive <> 1\r\n\t\t\t\t\t\t    GROUP BY SysDocID,RowIndex,VoucherID,PPC.ProductParentID,ISNULL(UnitPriceFC,SINVD.UnitPrice),ISNULL(PPC.ProductParentID,SINVD.ProductID),\r\n                            ISNULL(PP.Description,SINVD.Description),SINVD.UnitID,LocationID,SINVD.WeightQuantity,SINVD.WeightPrice, FOCQuantity, ConsignmentNo  \r\n                            ORDER BY RowIndex"));
				FillDataSet(dataSet, "Sales_Invoice_Detail", cmdText);
				cmdText = "SELECT  * FROM Sales_Invoice_Expense\r\n                                WHERE InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID IN (" + text + ") ";
				FillDataSet(dataSet, "Sales_Invoice_Expense", cmdText);
				dataSet.Relations.Add("CustomerInvoice", new DataColumn[2]
				{
					dataSet.Tables["Sales_Invoice"].Columns["SysDocID"],
					dataSet.Tables["Sales_Invoice"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Sales_Invoice_Detail"].Columns["SysDocID"],
					dataSet.Tables["Sales_Invoice_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				decimal result = default(decimal);
				decimal d = default(decimal);
				foreach (DataRow row in dataSet.Tables["Sales_Invoice_Expense"].Rows)
				{
					decimal.TryParse(row["Amount"].ToString(), out result);
					d += result;
				}
				dataSet.Tables["Sales_Invoice"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row2 in dataSet.Tables["Sales_Invoice"].Rows)
				{
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal num = default(decimal);
					bool result5 = false;
					decimal.TryParse(row2["Total"].ToString(), out result2);
					decimal.TryParse(row2["Discount"].ToString(), out result3);
					decimal.TryParse(row2["Tax"].ToString(), out result4);
					bool.TryParse(row2["PriceIncludeTax"].ToString(), out result5);
					num = decimal.Parse(row2["RoundOff"].ToString(), NumberStyles.Any);
					int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
					if (!result5)
					{
						row2["TotalInWords"] = NumToWord.GetNumInWords(result2 + d - result3 + result4 + num, currencyDecimalPoints);
					}
					else
					{
						row2["TotalInWords"] = NumToWord.GetNumInWords(result2 + d - result3 + num, currencyDecimalPoints);
					}
				}
				if (dataSet != null && dataSet.Tables["Sales_Invoice_Detail"].Rows.Count > 0)
				{
					decimal d2 = default(decimal);
					decimal result6 = default(decimal);
					decimal result7 = default(decimal);
					int currencyDecimalPoints2 = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
					decimal.TryParse(dataSet.Tables["Sales_Invoice"].Rows[0]["Discount"].ToString(), out result6);
					decimal.TryParse(dataSet.Tables["Sales_Invoice"].Rows[0]["Total"].ToString(), out result7);
					if (result7 != 0m)
					{
						d2 = result6 / result7 * 100m / 100m;
					}
					if (!dataSet.Tables["Sales_Invoice_Detail"].Columns.Contains("TotalWithDiscount"))
					{
						dataSet.Tables["Sales_Invoice_Detail"].Columns.Add("TotalWithDiscount");
						dataSet.Tables["Sales_Invoice_Detail"].Columns.Add("DistributeDiscount");
					}
					foreach (DataRow row3 in dataSet.Tables["Sales_Invoice_Detail"].Rows)
					{
						decimal result8 = default(decimal);
						decimal result9 = default(decimal);
						decimal result10 = default(decimal);
						decimal.TryParse(row3["UnitPrice"].ToString(), out result8);
						decimal.TryParse(row3["Quantity"].ToString(), out result9);
						decimal.TryParse(row3["TaxAmount"].ToString(), out result10);
						decimal num2 = Math.Round(result9 * result8 - d2 * result8 * result9 + result10, currencyDecimalPoints2);
						row3["DistributeDiscount"] = Math.Round(d2 * result8 * result9, currencyDecimalPoints2);
						row3["TotalWithDiscount"] = num2;
					}
				}
				if (dataSet.Tables["Sales_Invoice_Expense"].Rows.Count > 0)
				{
					dataSet.Relations.Add("CustomerInvoiceExpense", new DataColumn[2]
					{
						dataSet.Tables["Sales_Invoice"].Columns["SysDocID"],
						dataSet.Tables["Sales_Invoice"].Columns["VoucherID"]
					}, new DataColumn[2]
					{
						dataSet.Tables["Sales_Invoice_Expense"].Columns["InvoiceSysDocID"],
						dataSet.Tables["Sales_Invoice_Expense"].Columns["InvoiceVoucherID"]
					}, createConstraints: false);
				}
				if (showLotDetail)
				{
					string text2 = "";
					string text3 = "";
					if (dataSet.Tables["Sales_Invoice_Detail"].Rows.Count > 0)
					{
						text2 = dataSet.Tables["Sales_Invoice_Detail"].Rows[0]["OrderSysdocID"].ToString();
						text3 = dataSet.Tables["Sales_Invoice_Detail"].Rows[0]["OrderVoucherID"].ToString();
					}
					cmdText = "SELECT PL.*,PLS.SoldQty FROM Product_Lot PL LEFT JOIN Product_Lot_Sales PLS ON PL.LotNumber=PLS.LotNo WHERE PLS.DocID=\r\n                            '" + text2 + "' AND PLS.InvoiceNumber IN ('" + text3 + "')";
					FillDataSet(dataSet, "ProductLot", cmdText);
					dataSet.Relations.Add("ProductLotRel", new DataColumn[1]
					{
						dataSet.Tables["Sales_Invoice_Detail"].Columns["ProductID"]
					}, new DataColumn[1]
					{
						dataSet.Tables["ProductLot"].Columns["ItemCode"]
					}, createConstraints: false);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool isExport, bool showVoid, string sysDocID, bool isCash)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   ISNULL(IsVoid,'False') AS 'V', INV.SysDocID [Doc ID],VoucherID [Doc Number],INV.CustomerID [Customer Code],CustomerName [Customer Name],CustomerAddress [Address],TransactionDate [Invoice Date], DueDate AS [Due Date],\r\n\t\t\t\t\t\t\tINV.TermID [Term],INV.Reference AS Ref1,INV.Reference2 AS Ref2,INV.CurrencyID[Currency], J.JobID,J.JobName,\r\n                            CASE ISNULL(IsCash,'False') WHEN 'True' THEN 'Cash' ELSE 'Credit' END AS [Type],INV.SalespersonID + '-' + SP.FullName AS [Salesperon],Total - ISNULL(Discount,0) AS [Amount],INV.TaxAmount\r\n                            FROM   Sales_Invoice INV\r\n                            Inner JOIN Customer ON CUSTOMER.CustomerID=INV.CustomerID\r\n                            LEFT JOIN Job J ON INV.JobID=J.JobID\r\n\t\t\t\t\t\t\tLEFT OUTER JOIN Salesperson SP ON SP.SalespersonID = INV.SalespersonID\r\n                             ";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
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
			FillDataSet(dataSet, "Sales_Invoice", sqlCommand);
			return dataSet;
		}

		public bool InvoiceHasShippedQuantity(string sysDocID, string voucherNumber, SqlTransaction sqlTransaction)
		{
			string exp = "Select Count(*) FROM Sales_Invoice_Detail SID\r\n                                WHERE SysdocID='" + sysDocID + "' AND VoucherID='" + voucherNumber + "' Having SUM(ISNULL(QuantityShipped,0))>0";
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
				string exp = "SELECT COUNT(RowIndex)FROM Sales_Invoice_Detail SOD WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'\r\n                                AND (SELECT SUM((CASE WHEN UnitQuantity IS NULL \r\n                                THEN Quantity ELSE UnitQuantity END) ) FROM Sales_Invoice_Detail SOD2 WHERE SOD.SysDocID=SOD2.SysDocID AND SOD.VoucherID=SOD2.VoucherID)-\r\n                                 (SELECT SUM(ISNULL(QuantityShipped,0) ) FROM Sales_Invoice_Detail SOD3 WHERE SOD.SysDocID=SOD3.SysDocID AND SOD.VoucherID=SOD3.VoucherID) <= 0";
				object obj = ExecuteScalar(exp, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) > 0)
				{
					return true;
				}
				exp = "UPDATE Sales_Invoice SET IsDelivered = 'False' WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public bool ModifyTransactions(string sysDocID, string voucherID, string userID, bool isModify, string toUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = "";
				bool flag = false;
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
					flag = true;
				}
				if (isModify)
				{
					isModify = true;
					object obj = null;
					if (toUpdate == "")
					{
						text = "INSERT INTO Modify_Transactions  Values( '" + sysDocID + "' , '" + voucherID + "', '" + userID + "', '" + isModify.ToString() + "')";
						obj = ExecuteScalar(text, sqlTransaction);
					}
					if (toUpdate != "")
					{
						isModify = false;
						text = "UPDATE Modify_Transactions   SET IsModify='" + isModify.ToString() + "' WHERE  SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'AND UserID='" + userID + "'";
						ExecuteNonQuery(text, sqlTransaction);
						return true;
					}
					if (flag)
					{
						base.DBConfig.EndTransaction(result: true);
					}
					if (obj == null || int.Parse(obj.ToString()) > 0)
					{
						return true;
					}
					return false;
				}
				text = "SELECT  COUNT(ISNull(IsModify,0)) FROM Modify_Transactions  WHERE IsModify='1' AND SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND UserID='" + userID + "'";
				object obj2 = ExecuteScalar(text, sqlTransaction);
				if (flag)
				{
					base.DBConfig.EndTransaction(result: true);
				}
				if (obj2 == null || int.Parse(obj2.ToString()) > 0)
				{
					return true;
				}
				return false;
			}
			catch
			{
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

		public DataSet GetPaymentAllocationDetails(string sysDocID, string voucherID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT  ARP.CustomerID, ARJ.Reference,ARJ.ChequeNumber,ARJ.ARAccountID,\r\n                            InvoiceVoucherID [Invoice No],PaymentSysDocID,PaymentVoucherID,ISNULL(SI.TotalFC,SI.Total)-ISNULL(SI.DiscountFC,SI.Discount)+ISNULL(SI.TaxAmountFC,SI.TaxAmount)+ISNULL(SI.RoundOff,0) AS Total,PaymentAmount,ARJ.ARDate AS [Invoice Date], ARJ.ARDueDate [Due Date]\r\n                    \r\n                            FROM dbo.AR_Payment_Allocation ARP INNER JOIN Customer CUS ON Cus.CustomerID = ARP.CustomerID\r\n                            LEFT JOIN Sales_Invoice SI ON ARP.InvoiceSysDocID=SI.SysDocID and ARP.InvoiceVoucherID=SI.VoucherID\r\n                            INNER JOIN ARJournal ARJ ON ARP.ARJournalID = ARJ.ARID\r\n                            where ARP.InvoiceSysDocID= '" + sysDocID + "' AND ARP.InvoiceVoucherID='" + voucherID + "' GROUP BY ARP.CustomerID,CUS.CustomerName,InvoiceVoucherID,ARJ.ARDate, ARJ.ARDueDate,ARP.CurrencyID,ARP.CurrencyRate,ARJ.Debit,ARJ.Reference,CUS.CustomerID, cus.Balance, cus.PDCAmount,\r\n                            PaymentVoucherID,PaymentAmount,SI.Total ,ARJournalID,ARJ.ChequeNumber,ARJ.ARAccountID,PaymentSysDocID,ARJ.PaymentMethodType,SI.Discount,SI.RoundOff,SI.TotalFC,SI.DiscountFC,SI.TaxAmountFC,SI.TaxAmount ");
			FillDataSet(dataSet, "paymentallocationDetails", sqlCommand);
			return dataSet;
		}

		public DataSet GetSalesTransactionsbyID(string sysDocID, string voucherID)
		{
			return GetSalesTransactionsbyID(sysDocID, voucherID, null);
		}

		internal DataSet GetSalesTransactionsbyID(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			SysDocTypes sysDocTypes = SysDocTypes.None;
			object fieldValue = new Databases(base.DBConfig).GetFieldValue("System_Document", "SysDocType", "SysDocID", sysDocID, sqlTransaction);
			if (fieldValue != null)
			{
				sysDocTypes = (SysDocTypes)int.Parse(fieldValue.ToString());
			}
			if (sysDocID == "")
			{
				return null;
			}
			string text = "SELECT DISTINCT SQ.Reference AS [Enquiry No.] , convert( varchar ,SQ.TransactionDate,103) AS [Quote Date] , SQ.VoucherID AS [Quote No.] ,convert( varchar , SO.TransactionDate,103) AS [Order Date] , SO.VoucherID [Order No.] \r\n                        ,convert( varchar , DN.TransactionDate,103) AS [DO Date] , DN.VoucherID [DO No.] , convert( varchar ,DR.TransactionDate,103) AS [Return Date] , DR.VoucherID [Return No.] \r\n                        , convert( varchar ,SN.TransactionDate,103) AS [Invoice Date] , SN.VoucherID [Invoice No.] ,( 'Inv Date:'+'Amount: '+ \r\n                        convert( varchar , SN.TransactionDate,103) + CHAR(13) + CHAR(10) + convert ( nvarchar , sn.Total ) ) AS Total , \r\n                        PaymentVoucherID [Receipt No.] \r\n                        FROM Sales_Quote SQ \r\n                        FULL OUTER JOIN Sales_Order_Detail SOD ON SOD.SourceSysDocID = SQ.SysDocID AND SOD.SourceVoucherID = SQ.VoucherID \r\n                        LEFT OUTER JOIN Sales_Order SO ON SOD.SysDocID = SO.SysDocID AND SOD.VoucherID = SO.VoucherID \r\n                        LEFT OUTER JOIN Delivery_Note_Detail DND ON DND.SourceSysDocID = SO.SysDocID AND DND.SourceVoucherID = SO.VoucherID \r\n                        LEFT OUTER JOIN Delivery_Note DN ON DND.SysDocID = DN.SysDocID AND DND.VoucherID = DN.VoucherID \r\n                        LEFT OUTER JOIN Delivery_Return DR ON DR.Reference = DN.VoucherID \r\n                        LEFT OUTER JOIN Sales_Invoice_Detail SND ON SND.OrderSysDocID = DN.SysDocID AND SND.OrderVoucherID = DN.VoucherID \r\n                        LEFT OUTER JOIN Sales_Invoice SN ON SND.SysDocID = SN.SysDocID AND SND.VoucherID = SN.VoucherID \r\n                        LEFT OUTER JOIN AR_Payment_Allocation AP ON AP.InvoiceSysDocID = SN.SysDocID AND AP.InvoiceVoucherID = SN.VoucherID                           \r\n                        where 1=1 AND  ";
			switch (sysDocTypes)
			{
			case SysDocTypes.DeliveryNote:
			case SysDocTypes.ExportDeliveryNote:
				text = text + " DN.SysDocID= '" + sysDocID + "' AND DN.VoucherID='" + voucherID + "' ";
				break;
			case SysDocTypes.SalesInvoice:
			case SysDocTypes.ExportSalesInvoice:
				text = text + " SN.SysDocID= '" + sysDocID + "' AND SN.VoucherID='" + voucherID + "' ";
				break;
			case SysDocTypes.SalesOrder:
			case SysDocTypes.ExportSalesOrder:
				text = text + " SO.SysDocID= '" + sysDocID + "' AND SO.VoucherID='" + voucherID + "' ";
				break;
			case SysDocTypes.SalesEnquiry:
				text = text + " DN.SysDocID= '" + sysDocID + "' AND DN.VoucherID='" + voucherID + "' ";
				break;
			case SysDocTypes.SalesQuote:
				text = text + " SQ.SysDocID= '" + sysDocID + "' AND SQ.VoucherID='" + voucherID + "' ";
				break;
			case SysDocTypes.DeliveryReturn:
				text = text + " DR.SysDocID= '" + sysDocID + "' AND DR.VoucherID='" + voucherID + "' ";
				break;
			}
			SqlCommand sqlCommand = new SqlCommand(text);
			FillDataSet(dataSet, "transactionDetails", sqlCommand);
			return dataSet;
		}
	}
}
