using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PurchaseInvoice : StoreObject
	{
		private const string PURCHASEINVOICE_TABLE = "Purchase_Invoice";

		private const string PURCHASEINVOICEDETAIL_TABLE = "Purchase_Invoice_Detail";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string VENDORID_PARM = "@VendorID";

		private const string ISIMPORT_PARM = "@IsImport";

		private const string PURCHASEFLOW_PARM = "@PurchaseFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string BUYERID_PARM = "@BuyerID";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string STATUS_PARM = "@Status";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string VENDORREFERENCENUMBER_PARM = "@VendorReferenceNo";

		private const string PRICEINCLUDETAX_PARM = "@PriceIncludeTax";

		private const string NOTE_PARM = "@Note";

		private const string PONUMBER_PARM = "@PONumber";

		private const string SOURCEDOCTYPE_PARM = "@ItemSourceTypes";

		private const string ISVOID_PARM = "@IsVoid";

		private const string DISCOUNT_PARM = "@Discount";

		private const string DISCOUNTFC_PARM = "@DiscountFC";

		private const string TAXAMOUNT_PARM = "@TaxAmount";

		private const string TAXAMOUNTFC_PARM = "@TaxAmountFC";

		private const string TOTAL_PARM = "@Total";

		private const string TOTALFC_PARM = "@TotalFC";

		private const string REGISTERID_PARM = "@RegisterID";

		private const string ISCASH_PARM = "@IsCash";

		private const string DUEDATE_PARM = "@DueDate";

		private const string ISTAXINCLUDED_PARM = "@IsTaxIncluded";

		private const string CONTAINERNUMBER_PARM = "@ContainerNumber";

		private const string PORT_PARM = "@Port";

		private const string BOLNUMBER_PARM = "@BOLNumber";

		private const string SHIPPER_PARM = "@Shipper";

		private const string CLEARINGAGENT_PARM = "@ClearingAgent";

		private const string SPECIFICATIONID_PARM = "@SpecificationID";

		private const string STYLEID_PARM = "@StyleID";

		private const string CREATEDBY_PARM = "@CreatedBy";

		private const string DATECREATED_PARM = "@DateCreated";

		private const string UPDATEDBY_PARM = "@UpdatedBy";

		private const string DATEUPDATED_PARM = "@DateUpdated";

		private const string INVOICESYSDOCID_PARM = "@InvoiceSysDocID";

		private const string INVOICENUMBER_PARM = "@InvoiceNumber";

		private const string GRNSYSDOCID_PARM = "@GRNSysDocID";

		private const string GRNNUMBER_PARM = "@GRNNumber";

		private const string PRODUCTID_PARM = "@ProductID";

		private const string QUANTITY_PARM = "@Quantity";

		private const string UNITPRICE_PARM = "@UnitPrice";

		private const string UNITPRICEFC_PARM = "@UnitPriceFC";

		private const string LCOST_PARM = "@LCost";

		private const string LCOSTAMOUNT_PARM = "@LCostAmount";

		private const string DESCRIPTION_PARM = "@Description";

		private const string UNITID_PARM = "@UnitID";

		private const string LOCATIONID_PARM = "@LocationID";

		private const string UNITQUANTITY_PARM = "@UnitQuantity";

		private const string UNITFACTOR_PARM = "@UnitFactor";

		private const string FACTORTYPE_PARM = "@FactorType";

		private const string SUBUNITPRICE_PARM = "@SubunitPrice";

		private const string ORDERVOUCHERID_PARM = "@OrderVoucherID";

		private const string ORDERSYSDOCID_PARM = "@OrderSysDocID";

		private const string PORVOUCHERID_PARM = "@PORVoucherID";

		private const string PORSYSDOCID_PARM = "@PORSysDocID";

		private const string ORDERROWINDEX_PARM = "@OrderRowIndex";

		private const string ISPORROW_PARM = "@IsPORRow";

		private const string LOTNUMBER_PARM = "@LotNumber";

		private const string ROWSOURCE_PARM = "@RowSource";

		private const string AMOUNT_PARM = "@Amount";

		private const string AMOUNTFC_PARM = "@AmountFC";

		private const string JOBID_PARM = "@JobID";

		private const string REMARKS_PARM = "@Remarks";

		private const string TAXPERCENTAGE_PARM = "@TaxPercentage";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

		private const string REFSLNO_PARM = "@RefSlNo";

		private const string REFTEXT1_PARM = "@RefText1";

		private const string REFTEXT2_PARM = "@RefText2";

		private const string REFNUM1_PARM = "@RefNum1";

		private const string REFNUM2_PARM = "@RefNum2";

		private const string REFDATE1_PARM = "@RefDate1";

		private const string REFDATE2_PARM = "@RefDate2";

		private const string PURCHASEINVOICEEXPENSETABLE_PARM = "@Purchase_Invoice_Expense";

		private const string INVOICEVOUCHERID_PARM = "@InvoiceVoucherID";

		private const string EXPENSEID_PARM = "@ExpenseID";

		private const string RATETYPE_PARM = "@RateType";

		private const string PCVOUCHERID_PARM = "@PCVoucherID";

		private const string PCSYSDOCID_PARM = "@PCSysDocID";

		private const string PCROWINDEX_PARM = "@PCRowIndex";

		public PurchaseInvoice(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePurchaseInvoiceText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Invoice", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("PurchaseFlow", "@PurchaseFlow"), new FieldValue("DueDate", "@DueDate"), new FieldValue("IsImport", "@IsImport"), new FieldValue("IsTaxIncluded", "@IsTaxIncluded"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("SourceDocType", "@ItemSourceTypes"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("VendorReferenceNo", "@VendorReferenceNo"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("IsCash", "@IsCash"), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("Port", "@Port"), new FieldValue("BOLNumber", "@BOLNumber"), new FieldValue("Shipper", "@Shipper"), new FieldValue("ClearingAgent", "@ClearingAgent"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("Note", "@Note"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Purchase_Invoice", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
			}
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseInvoiceCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseInvoiceText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseInvoiceText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@IsImport", SqlDbType.Bit);
			parameters.Add("@IsTaxIncluded", SqlDbType.Bit);
			parameters.Add("@PurchaseFlow", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@BuyerID", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@ItemSourceTypes", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@VendorReferenceNo", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@DiscountFC", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxAmountFC", SqlDbType.Decimal);
			parameters.Add("@Total", SqlDbType.Decimal);
			parameters.Add("@TotalFC", SqlDbType.Decimal);
			parameters.Add("@Note", SqlDbType.NVarChar);
			parameters.Add("@RegisterID", SqlDbType.NVarChar);
			parameters.Add("@IsCash", SqlDbType.Bit);
			parameters.Add("@ContainerNumber", SqlDbType.NVarChar);
			parameters.Add("@Port", SqlDbType.NVarChar);
			parameters.Add("@BOLNumber", SqlDbType.NVarChar);
			parameters.Add("@Shipper", SqlDbType.NVarChar);
			parameters.Add("@ClearingAgent", SqlDbType.NVarChar);
			parameters.Add("@DueDate", SqlDbType.DateTime);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@PurchaseFlow"].SourceColumn = "PurchaseFlow";
			parameters["@IsImport"].SourceColumn = "IsImport";
			parameters["@IsTaxIncluded"].SourceColumn = "IsTaxIncluded";
			parameters["@TransactionDate"].SourceColumn = "TransactionDate";
			parameters["@BuyerID"].SourceColumn = "BuyerID";
			parameters["@ShippingMethodID"].SourceColumn = "ShippingMethodID";
			parameters["@Status"].SourceColumn = "Status";
			parameters["@CurrencyID"].SourceColumn = "CurrencyID";
			parameters["@CurrencyRate"].SourceColumn = "CurrencyRate";
			parameters["@TermID"].SourceColumn = "TermID";
			parameters["@Reference"].SourceColumn = "Reference";
			parameters["@PriceIncludeTax"].SourceColumn = "PriceIncludeTax";
			parameters["@Reference2"].SourceColumn = "Reference2";
			parameters["@VendorReferenceNo"].SourceColumn = "VendorReferenceNo";
			parameters["@ItemSourceTypes"].SourceColumn = "SourceDocType";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxAmountFC"].SourceColumn = "TaxAmountFC";
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@DiscountFC"].SourceColumn = "DiscountFC";
			parameters["@Total"].SourceColumn = "Total";
			parameters["@TotalFC"].SourceColumn = "TotalFC";
			parameters["@Note"].SourceColumn = "Note";
			parameters["@RegisterID"].SourceColumn = "RegisterID";
			parameters["@IsCash"].SourceColumn = "IsCash";
			parameters["@ContainerNumber"].SourceColumn = "ContainerNumber";
			parameters["@Port"].SourceColumn = "Port";
			parameters["@BOLNumber"].SourceColumn = "BOLNumber";
			parameters["@Shipper"].SourceColumn = "Shipper";
			parameters["@ClearingAgent"].SourceColumn = "ClearingAgent";
			parameters["@DueDate"].SourceColumn = "DueDate";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
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

		private string GetInsertUpdatePurchaseInvoiceDetailsText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Invoice_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("Discount", "@Discount"), new FieldValue("LCost", "@LCost"), new FieldValue("LCostAmount", "@LCostAmount"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("JobID", "@JobID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxPercentage", "@TaxPercentage"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("OrderSysDocID", "@OrderSysDocID"), new FieldValue("OrderVoucherID", "@OrderVoucherID"), new FieldValue("PORSysDocID", "@PORSysDocID"), new FieldValue("PORVoucherID", "@PORVoucherID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("LotNumber", "@LotNumber"), new FieldValue("RowSource", "@RowSource"), new FieldValue("SpecificationID", "@SpecificationID"), new FieldValue("StyleID", "@StyleID"), new FieldValue("Remarks", "@Remarks"), new FieldValue("RefSlNo", "@RefSlNo"), new FieldValue("RefText1", "@RefText1"), new FieldValue("RefText2", "@RefText2"), new FieldValue("RefNum1", "@RefNum1"), new FieldValue("RefNum2", "@RefNum2"), new FieldValue("RefDate1", "@RefDate1"), new FieldValue("RefDate2", "@RefDate2"), new FieldValue("IsPORRow", "@IsPORRow"));
			if (isUpdate)
			{
				return sqlBuilder.GetUpdateExpression();
			}
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdatePurchaseInvoiceDetailsCommand(bool isUpdate)
		{
			SqlParameterCollection parameters;
			if (isUpdate)
			{
				if (updateCommand != null)
				{
					updateCommand = null;
				}
				updateCommand = new SqlCommand(GetInsertUpdatePurchaseInvoiceDetailsText(isUpdate: true), base.DBConfig.Connection);
				updateCommand.CommandType = CommandType.Text;
				parameters = updateCommand.Parameters;
			}
			else
			{
				if (insertCommand != null)
				{
					insertCommand = null;
				}
				insertCommand = new SqlCommand(GetInsertUpdatePurchaseInvoiceDetailsText(isUpdate: false), base.DBConfig.Connection);
				insertCommand.CommandType = CommandType.Text;
				parameters = insertCommand.Parameters;
			}
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@ProductID", SqlDbType.NVarChar);
			parameters.Add("@RowIndex", SqlDbType.Int);
			parameters.Add("@Quantity", SqlDbType.Real);
			parameters.Add("@Discount", SqlDbType.Decimal);
			parameters.Add("@UnitPrice", SqlDbType.Decimal);
			parameters.Add("@UnitPriceFC", SqlDbType.Decimal);
			parameters.Add("@LCost", SqlDbType.Decimal);
			parameters.Add("@LCostAmount", SqlDbType.Decimal);
			parameters.Add("@Description", SqlDbType.NVarChar);
			parameters.Add("@Amount", SqlDbType.Decimal);
			parameters.Add("@AmountFC", SqlDbType.Decimal);
			parameters.Add("@UnitID", SqlDbType.NVarChar);
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@LocationID", SqlDbType.NVarChar);
			parameters.Add("@UnitQuantity", SqlDbType.Real);
			parameters.Add("@UnitFactor", SqlDbType.Decimal);
			parameters.Add("@FactorType", SqlDbType.NVarChar);
			parameters.Add("@SubunitPrice", SqlDbType.Decimal);
			parameters.Add("@OrderSysDocID", SqlDbType.NVarChar);
			parameters.Add("@OrderVoucherID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxPercentage", SqlDbType.Decimal);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxGroupID", SqlDbType.VarChar);
			parameters.Add("@PORSysDocID", SqlDbType.NVarChar);
			parameters.Add("@PORVoucherID", SqlDbType.NVarChar);
			parameters.Add("@OrderRowIndex", SqlDbType.Int);
			parameters.Add("@IsPORRow", SqlDbType.Bit);
			parameters.Add("@LotNumber", SqlDbType.Int);
			parameters.Add("@SpecificationID", SqlDbType.NVarChar);
			parameters.Add("@StyleID", SqlDbType.NVarChar);
			parameters.Add("@RowSource", SqlDbType.Int);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
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
			parameters["@Discount"].SourceColumn = "Discount";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@UnitPriceFC"].SourceColumn = "UnitPriceFC";
			parameters["@LCost"].SourceColumn = "LCost";
			parameters["@LCostAmount"].SourceColumn = "LCostAmount";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@TaxPercentage"].SourceColumn = "TaxPercentage";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@LocationID"].SourceColumn = "LocationID";
			parameters["@UnitQuantity"].SourceColumn = "UnitQuantity";
			parameters["@UnitFactor"].SourceColumn = "UnitFactor";
			parameters["@FactorType"].SourceColumn = "FactorType";
			parameters["@SubunitPrice"].SourceColumn = "SubunitPrice";
			parameters["@OrderVoucherID"].SourceColumn = "OrderVoucherID";
			parameters["@OrderSysDocID"].SourceColumn = "OrderSysDocID";
			parameters["@PORSysDocID"].SourceColumn = "PORSysDocID";
			parameters["@PORVoucherID"].SourceColumn = "PORVoucherID";
			parameters["@OrderRowIndex"].SourceColumn = "OrderRowIndex";
			parameters["@IsPORRow"].SourceColumn = "IsPORRow";
			parameters["@LotNumber"].SourceColumn = "LotNumber";
			parameters["@SpecificationID"].SourceColumn = "SpecificationID";
			parameters["@StyleID"].SourceColumn = "StyleID";
			parameters["@RowSource"].SourceColumn = "RowSource";
			parameters["@Remarks"].SourceColumn = "Remarks";
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

		private string GetInsertUpdateExpenseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Invoice_Expense", new FieldValue("InvoiceSysDocID", "@InvoiceSysDocID"), new FieldValue("InvoiceVoucherID", "@InvoiceVoucherID"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("RateType", "@RateType"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Reference", "@Reference"), new FieldValue("PCVoucherID", "@PCVoucherID"), new FieldValue("PCSysDocID", "@PCSysDocID"), new FieldValue("PCRowIndex", "@PCRowIndex"), new FieldValue("CurrencyRate", "@CurrencyRate"));
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
			parameters.Add("@RowIndex", SqlDbType.TinyInt);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxAmount", SqlDbType.Money);
			parameters.Add("@PCRowIndex", SqlDbType.Int);
			parameters.Add("@PCSysDocID", SqlDbType.NVarChar);
			parameters.Add("@PCVoucherID", SqlDbType.NVarChar);
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
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@PCRowIndex"].SourceColumn = "PCRowIndex";
			parameters["@PCSysDocID"].SourceColumn = "PCSysDocID";
			parameters["@PCVoucherID"].SourceColumn = "PCVoucherID";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateInvoiceGRNText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Invoice_GRN", new FieldValue("InvoiceSysDocID", "@InvoiceSysDocID"), new FieldValue("InvoiceNumber", "@InvoiceNumber"), new FieldValue("GRNSysDocID", "@GRNSysDocID"), new FieldValue("GRNNumber", "@GRNNumber"));
			return sqlBuilder.GetInsertExpression();
		}

		private SqlCommand GetInsertUpdateInvoiceGRNCommand(bool isUpdate)
		{
			if (insertCommand != null)
			{
				insertCommand = null;
			}
			insertCommand = new SqlCommand(GetInsertUpdateInvoiceGRNText(isUpdate: false), base.DBConfig.Connection);
			insertCommand.CommandType = CommandType.Text;
			SqlParameterCollection parameters = insertCommand.Parameters;
			parameters.Add("@InvoiceSysDocID", SqlDbType.NVarChar);
			parameters.Add("@InvoiceNumber", SqlDbType.NVarChar);
			parameters.Add("@GRNSysDocID", SqlDbType.NVarChar);
			parameters.Add("@GRNNumber", SqlDbType.NVarChar);
			parameters["@InvoiceSysDocID"].SourceColumn = "InvoiceSysDocID";
			parameters["@InvoiceNumber"].SourceColumn = "InvoiceNumber";
			parameters["@GRNSysDocID"].SourceColumn = "GRNSysDocID";
			parameters["@GRNNumber"].SourceColumn = "GRNNumber";
			return insertCommand;
		}

		private bool ValidateData(PurchaseInvoiceData purchaseInvoiceData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = "";
				DataRow dataRow = purchaseInvoiceData.PurchaseInvoiceTable.Rows[0];
				dataRow["VoucherID"].ToString();
				dataRow["SysDocID"].ToString();
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				if (!isUpdate && itemSourceTypes == ItemSourceTypes.GRN)
				{
					ArrayList arrayList = new ArrayList();
					foreach (DataRow row in purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows)
					{
						string text2 = row["OrderSysDocID"].ToString() + row["OrderVoucherID"].ToString();
						if (!arrayList.Contains(text2))
						{
							arrayList.Add(text2);
						}
					}
					SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(base.DBConfig.Connection, SqlBulkCopyOptions.Default, sqlTransaction);
					string exp = "Create Table #TMP_GR (SysDocID nvarchar(7),VoucherID nvarchar(15)) ";
					ExecuteNonQuery(exp, sqlTransaction);
					sqlBulkCopy.DestinationTableName = "#TMP_GR";
					sqlBulkCopy.ColumnMappings.Add("OrderSysDocID", "SysDocID");
					sqlBulkCopy.ColumnMappings.Add("OrderVoucherID", "VoucherID");
					sqlBulkCopy.WriteToServer(purchaseInvoiceData.PurchaseInvoiceDetailTable);
					text = "select COUNT(*) FROM Purchase_Invoice_Detail \r\n                            WHERE EXISTS (SELECT *  FROM #TMP_GR TMP WHERE OrderSysDocID = TMP.SysDocID AND ordervoucherid = TMP.VoucherID) ";
					if (ExecuteScalar(text, sqlTransaction).IsNumericGreaterThanZero())
					{
						throw new CompanyException("One or more of selected GRNs are already invoiced.");
					}
				}
				return true;
			}
			catch
			{
				throw;
			}
			finally
			{
				try
				{
					string exp2 = "DROP  Table #TMP_GR";
					ExecuteNonQuery(exp2, sqlTransaction);
				}
				catch
				{
				}
			}
		}

		private bool ValidateDate(PurchaseInvoiceData purchaseInvoiceData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				string text = CommonLib.ToSqlDateTimeString(DateTime.Parse(DateTime.Parse(purchaseInvoiceData.PurchaseInvoiceTable.Rows[0]["TransactionDate"].ToString()).ToShortDateString()));
				string text2 = "";
				foreach (DataRow row in purchaseInvoiceData.InvoiceGRNTable.Rows)
				{
					string text3 = row["GrnSysDocID"].ToString();
					string text4 = row["GRNNumber"].ToString();
					text2 = "Select Case When CONVERT(date,TransactionDate) > CONVERT(date,'" + text + "') Then 'True' Else 'False' End From Purchase_Receipt Where SysDocID='" + text3 + "' AND VoucherID='" + text4 + "'";
					if (bool.Parse(ExecuteScalar(text2, sqlTransaction).ToString()))
					{
						throw new CompanyException("One of the GRN is having greater date than purchase invoice date.");
					}
				}
				return true;
			}
			catch
			{
				throw;
			}
		}

		public bool InsertUpdatePurchaseInvoice(PurchaseInvoiceData purchaseInvoiceData, bool isUpdate)
		{
			bool flag = true;
			string text = "";
			SqlCommand insertUpdatePurchaseInvoiceCommand = GetInsertUpdatePurchaseInvoiceCommand(isUpdate);
			string text2 = "";
			string text3 = "";
			string text4 = "";
			try
			{
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				DataRow dataRow = purchaseInvoiceData.PurchaseInvoiceTable.Rows[0];
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				string text5 = dataRow["VoucherID"].ToString();
				string text6 = dataRow["SysDocID"].ToString();
				flag &= ValidateData(purchaseInvoiceData, isUpdate, sqlTransaction);
				flag &= ValidateDate(purchaseInvoiceData, isUpdate, sqlTransaction);
				flag &= ValidateSoftClosePeriod(purchaseInvoiceData, isUpdate, sqlTransaction);
				bool result = false;
				bool.TryParse(dataRow["IsCash"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(dataRow["IsImport"].ToString(), out result2);
				ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
				if (dataRow["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes = (ItemSourceTypes)byte.Parse(dataRow["SourceDocType"].ToString());
				}
				decimal num = default(decimal);
				foreach (DataRow row in purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows)
				{
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal result5 = default(decimal);
					decimal.TryParse(row["Quantity"].ToString(), out result4);
					decimal.TryParse(row["UnitPrice"].ToString(), out result5);
					decimal.TryParse(row["Amount"].ToString(), out result3);
					num += result3;
				}
				dataRow["Total"] = num;
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Purchase_Invoice", "VoucherID", dataRow["SysDocID"].ToString(), text5, sqlTransaction))
				{
					throw new CompanyException("Document number already exist.", 1046);
				}
				string baseCurrencyID = new Currencies(base.DBConfig).GetBaseCurrencyID();
				bool flag2 = false;
				decimal result6 = 1m;
				string a = "M";
				if (dataRow["CurrencyID"] != DBNull.Value && baseCurrencyID != dataRow["CurrencyID"].ToString())
				{
					flag2 = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result6);
					a = new Currencies(base.DBConfig).GetCurrencyRateType(dataRow["CurrencyID"].ToString());
				}
				if (flag2)
				{
					decimal result7 = default(decimal);
					dataRow["TotalFC"] = dataRow["Total"];
					decimal.TryParse(dataRow["Total"].ToString(), out result7);
					result7 = ((!(a == "M")) ? Math.Round(result7 / result6, 4) : Math.Round(result7 * result6, 4));
					dataRow["Total"] = result7;
					result7 = default(decimal);
					dataRow["DiscountFC"] = dataRow["Discount"];
					decimal.TryParse(dataRow["DiscountFC"].ToString(), out result7);
					result7 = ((!(a == "M")) ? Math.Round(result7 / result6, 4) : Math.Round(result7 * result6, 4));
					dataRow["Discount"] = result7;
					result7 = default(decimal);
					dataRow["TaxAmountFC"] = dataRow["TaxAmount"];
					decimal.TryParse(dataRow["TaxAmount"].ToString(), out result7);
					result7 = ((!(a == "M")) ? Math.Round(result7 / result6, 4) : Math.Round(result7 * result6, 4));
					dataRow["TaxAmount"] = result7;
				}
				string commaSeperatedIDs = GetCommaSeperatedIDs(purchaseInvoiceData, "Purchase_Invoice_Detail", "ProductID");
				DataSet dataSet = new DataSet();
				text = "SELECT P.ProductID,P.ItemType,P.LastCost, P.Quantity AS TotalQuantity,AverageCost FROM Product P  \r\n\t\t\t\t\t\t\tWHERE P.ProductID IN  (" + commaSeperatedIDs + ")";
				FillDataSet(dataSet, "Product", text, sqlTransaction);
				text = "SELECT PL.ProductID, PL.LocationID, PL.Quantity AS LocationQuantity FROM Product_Location  PL  \r\n\t\t\t\t\t\t\tWHERE PL.ProductID IN  (" + commaSeperatedIDs + ")";
				FillDataSet(dataSet, "Product_Location", text, sqlTransaction);
				foreach (DataRow row2 in purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows)
				{
					row2["SysDocID"] = dataRow["SysDocID"];
					row2["VoucherID"] = dataRow["VoucherID"];
					text4 = row2["ProductID"].ToString();
					string text7 = row2["LocationID"].ToString();
					_ = dataSet.Tables["Product"].Select("ProductID = '" + text4 + "'")[0];
					float result8 = 0f;
					DataRow[] array = dataSet.Tables["Product_Location"].Select("ProductID = '" + text4 + "' AND LocationID IS NOT NULL AND LocationID = '" + text7 + "'");
					if (array.Length != 0)
					{
						float.TryParse(array[0]["LocationQuantity"].ToString(), out result8);
						_ = array[0];
					}
					float num2 = 0f;
					string text8 = "";
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "UnitID", "ProductID", text4, sqlTransaction);
					if (fieldValue != null)
					{
						text8 = fieldValue.ToString();
					}
					if (text8 != "" && row2["UnitID"] != DBNull.Value && row2["UnitID"].ToString() != text8)
					{
						DataRow obj2 = new Products(base.DBConfig).GetProductUnitRow(text4, row2["UnitID"].ToString()) ?? throw new CompanyException("One of the selected units is not assigned to item.\nItem:" + text4 + "\nUnit:" + row2["UnitID"].ToString());
						float num3 = float.Parse(obj2["Factor"].ToString());
						string text9 = obj2["FactorType"].ToString();
						num2 = float.Parse(row2["Quantity"].ToString());
						row2["UnitFactor"] = num3;
						row2["FactorType"] = text9;
						row2["UnitQuantity"] = row2["Quantity"];
						num2 = ((!(text9 == "M")) ? float.Parse(Math.Round(num2 * num3, 5).ToString()) : float.Parse(Math.Round(num2 / num3, 5).ToString()));
						row2["Quantity"] = num2;
					}
					if (flag2)
					{
						decimal result9 = default(decimal);
						decimal result10 = default(decimal);
						row2["UnitPriceFC"] = row2["UnitPrice"];
						row2["AmountFC"] = row2["Amount"];
						decimal.TryParse(row2["UnitPrice"].ToString(), out result9);
						decimal.TryParse(row2["Amount"].ToString(), out result10);
						result9 = ((!(a == "M")) ? Math.Round(result9 / result6, 4) : Math.Round(result9 * result6, 4));
						row2["UnitPrice"] = result9;
						result10 = ((!(a == "M")) ? Math.Round(result10 / result6, currencyDecimalPoints) : Math.Round(result10 * result6, currencyDecimalPoints));
						row2["Amount"] = result10;
					}
				}
				foreach (DataRow row3 in purchaseInvoiceData.InvoiceExpenseTable.Rows)
				{
					row3["InvoiceSysDocID"] = dataRow["SysDocID"];
					row3["InvoiceVoucherID"] = dataRow["VoucherID"];
					string a2 = row3["CurrencyID"].ToString();
					if (a2 != "" && a2 != baseCurrencyID)
					{
						decimal d = decimal.Parse(row3["Amount"].ToString());
						row3["AmountFC"] = row3["Amount"];
						decimal result11 = 1m;
						decimal.TryParse(row3["CurrencyRate"].ToString(), out result11);
						d = ((!(row3["RateType"].ToString() == "M")) ? Math.Round(d / result11, currencyDecimalPoints) : Math.Round(d * result11, currencyDecimalPoints));
						row3["Amount"] = d;
					}
					else
					{
						row3["CurrencyRate"] = 1;
					}
				}
				if (isUpdate)
				{
					flag &= DeletePurchaseInvoiceDetailsRows(text6, text5, isDeletingTransaction: false, sqlTransaction);
					flag &= DeletePurchaseInvoiceExpenseRows(text6, text5, sqlTransaction);
				}
				insertUpdatePurchaseInvoiceCommand.Transaction = sqlTransaction;
				flag = (isUpdate ? (flag & Update(purchaseInvoiceData, "Purchase_Invoice", insertUpdatePurchaseInvoiceCommand)) : (flag & Insert(purchaseInvoiceData, "Purchase_Invoice", insertUpdatePurchaseInvoiceCommand)));
				if (!flag)
				{
					throw new CompanyException("Faild to save the transaction. Please reload the transaction and try again.");
				}
				if (purchaseInvoiceData.Tables["Purchase_Invoice_Detail"].Rows.Count > 0)
				{
					insertUpdatePurchaseInvoiceCommand = GetInsertUpdatePurchaseInvoiceDetailsCommand(isUpdate: false);
					insertUpdatePurchaseInvoiceCommand.Transaction = sqlTransaction;
					flag &= Insert(purchaseInvoiceData, "Purchase_Invoice_Detail", insertUpdatePurchaseInvoiceCommand);
				}
				if (purchaseInvoiceData.Tables["Purchase_Invoice_Expense"].Rows.Count > 0)
				{
					insertUpdatePurchaseInvoiceCommand = GetInsertUpdateExpenseCommand(isUpdate: false);
					insertUpdatePurchaseInvoiceCommand.Transaction = sqlTransaction;
					flag &= Insert(purchaseInvoiceData, "Purchase_Invoice_Expense", insertUpdatePurchaseInvoiceCommand);
				}
				if (purchaseInvoiceData.Tables["Purchase_Invoice_Expense"].Rows.Count > 0)
				{
					foreach (DataRow row4 in purchaseInvoiceData.InvoiceExpenseTable.Rows)
					{
						row4["ExpenseID"].ToString();
						string text10 = row4["PCVoucherID"].ToString();
						string text11 = row4["PCSysDocID"].ToString();
						int result12 = 0;
						double result13 = 0.0;
						if (!(text10 == "") && !(text11 == ""))
						{
							int.TryParse(row4["PCRowIndex"].ToString(), out result12);
							double.TryParse(row4["Amount"].ToString(), out result13);
							if (isUpdate)
							{
								flag &= new PurchaseCostEntry(base.DBConfig).UpdateRowReceivedAmount(text11, text10, result12, result13, isDelete: true, sqlTransaction);
							}
							flag &= new PurchaseCostEntry(base.DBConfig).UpdateRowReceivedAmount(text11, text10, result12, result13, isDelete: false, sqlTransaction);
						}
					}
				}
				bool flag3 = false;
				if (dataRow["PriceIncludeTax"] != DBNull.Value)
				{
					flag3 = bool.Parse(dataRow["PriceIncludeTax"].ToString());
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row5 in purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows)
				{
					decimal result14 = default(decimal);
					ItemSourceTypes itemSourceTypes2 = ItemSourceTypes.None;
					if (row5["RowSource"] != DBNull.Value)
					{
						itemSourceTypes2 = (ItemSourceTypes)byte.Parse(row5["RowSource"].ToString());
					}
					if (itemSourceTypes2 != ItemSourceTypes.GRN)
					{
						decimal.TryParse(row5["TaxAmount"].ToString(), out result14);
						DataRow dataRow6 = inventoryTransactionData.InventoryTransactionTable.NewRow();
						dataRow6.BeginEdit();
						dataRow6["SysDocID"] = row5["SysDocID"];
						dataRow6["VoucherID"] = row5["VoucherID"];
						if (row5["LocationID"].ToString() == "")
						{
							throw new Exception("Location cannot be empty.");
						}
						dataRow6["LocationID"] = row5["LocationID"];
						dataRow6["ProductID"] = row5["ProductID"];
						dataRow6["Quantity"] = decimal.Parse(row5["Quantity"].ToString());
						dataRow6["Reference"] = dataRow["Reference"];
						if (result)
						{
							dataRow6["SysDocType"] = (byte)34;
						}
						else if (result2)
						{
							dataRow6["SysDocType"] = (byte)39;
						}
						else
						{
							dataRow6["SysDocType"] = (byte)33;
						}
						dataRow6["TransactionDate"] = dataRow["TransactionDate"];
						dataRow6["TransactionType"] = (byte)1;
						decimal result15 = default(decimal);
						decimal result16 = default(decimal);
						decimal result17 = default(decimal);
						decimal.TryParse(row5["UnitPrice"].ToString(), out result15);
						decimal.TryParse(row5["LCost"].ToString(), out result16);
						decimal.TryParse(row5["Quantity"].ToString(), out result17);
						if (flag3)
						{
							result15 -= result14 / result17;
						}
						dataRow6["UnitID"] = row5["UnitID"];
						dataRow6["UnitPrice"] = result15 + result16;
						dataRow6["Cost"] = result15;
						dataRow6["RowIndex"] = row5["RowIndex"];
						dataRow6["PayeeType"] = "V";
						dataRow6["PayeeID"] = dataRow["VendorID"];
						dataRow6["RowIndex"] = row5["RowIndex"];
						dataRow6["SpecificationID"] = row5["SpecificationID"];
						dataRow6["StyleID"] = row5["StyleID"];
						dataRow6["CompanyID"] = dataRow["CompanyID"];
						dataRow6["DivisionID"] = dataRow["DivisionID"];
						if (row5["UnitQuantity"] != DBNull.Value && row5["UnitFactor"] != DBNull.Value)
						{
							dataRow6["UnitQuantity"] = row5["UnitQuantity"];
							dataRow6["Factor"] = row5["UnitFactor"];
							dataRow6["FactorType"] = row5["FactorType"];
							decimal.Parse(row5["UnitFactor"].ToString());
							row5["FactorType"].ToString();
							decimal d2 = decimal.Parse(row5["UnitQuantity"].ToString());
							decimal num4 = decimal.Parse(row5["Quantity"].ToString());
							decimal d3 = decimal.Parse(row5["UnitPrice"].ToString());
							decimal num5 = default(decimal);
							num5 = ((!(num4 != 0m)) ? default(decimal) : (d2 * d3 / num4));
							dataRow6["UnitPrice"] = num5;
						}
						dataRow6.EndEdit();
						inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow6);
					}
				}
				inventoryTransactionData.Merge(purchaseInvoiceData.Tables["Product_Lot_Receiving_Detail"]);
				flag &= new Products(base.DBConfig).InsertUpdateProductLotReceivingDetail(purchaseInvoiceData, isUpdate: false, sqlTransaction);
				if (inventoryTransactionData.InventoryTransactionTable.Rows.Count > 0)
				{
					flag &= new InventoryTransaction(base.DBConfig).InsertUpdateInventoryTransaction(inventoryTransactionData, isUpdate, sqlTransaction);
				}
				if (itemSourceTypes == ItemSourceTypes.GRN)
				{
					flag &= ValidateInventoryLockDate(purchaseInvoiceData, sqlTransaction);
				}
				if (itemSourceTypes == ItemSourceTypes.GRN)
				{
					flag &= new InventoryTransaction(base.DBConfig).UpdateGRNCosting(text6, text5, sqlTransaction);
				}
				foreach (DataRow row6 in purchaseInvoiceData.InvoiceGRNTable.Rows)
				{
					row6["InvoiceSysDocID"] = dataRow["SysDocID"];
					row6["InvoiceNumber"] = dataRow["VoucherID"];
				}
				text = "DELETE FROM Invoice_GRN WHERE InvoiceSysDocID='" + text6 + "' AND InvoiceNumber='" + text5 + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				if (purchaseInvoiceData.Tables["Invoice_GRN"].Rows.Count > 0)
				{
					insertUpdatePurchaseInvoiceCommand = GetInsertUpdateInvoiceGRNCommand(isUpdate: false);
					insertUpdatePurchaseInvoiceCommand.Transaction = sqlTransaction;
					flag &= Insert(purchaseInvoiceData, "Invoice_GRN", insertUpdatePurchaseInvoiceCommand);
				}
				switch (itemSourceTypes)
				{
				case ItemSourceTypes.PurchaseOrder:
					foreach (DataRow row7 in purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows)
					{
						text4 = row7["ProductID"].ToString();
						text2 = row7["OrderVoucherID"].ToString();
						text3 = row7["OrderSysDocID"].ToString();
						int result20 = 0;
						if (!(text2 == "") && !(text3 == ""))
						{
							int.TryParse(row7["OrderRowIndex"].ToString(), out result20);
							float result21 = 0f;
							if (row7["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row7["UnitQuantity"].ToString(), out result21);
							}
							else
							{
								float.TryParse(row7["Quantity"].ToString(), out result21);
							}
							float num9 = new Products(base.DBConfig).GetOrderedQuantity(text4, sqlTransaction) - result21;
							if (num9 < 0f)
							{
								num9 = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateOrderedQuantity(text4, num9, sqlTransaction);
							flag &= new PurchaseOrder(base.DBConfig).UpdateRowReceivedQuantity(text3, text2, result20, result21, sqlTransaction);
						}
					}
					text2 = purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows[0]["OrderVoucherID"].ToString();
					text3 = purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows[0]["OrderSysDocID"].ToString();
					if (text2 != "")
					{
						flag &= new PurchaseOrder(base.DBConfig).CloseReceivedOrder(text3, text2, sqlTransaction);
					}
					break;
				case ItemSourceTypes.PackingList:
					foreach (DataRow row8 in purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows)
					{
						text4 = row8["ProductID"].ToString();
						string text12 = row8["SourceVoucherID"].ToString();
						string text13 = row8["SourceSysDocID"].ToString();
						int num6 = int.Parse(row8["SourceRowIndex"].ToString());
						int num7 = 0;
						text3 = new Databases(base.DBConfig).GetFieldValue("PO_Shipment_Detail", "SourceSysDocID", "SysDocID", text13, "VoucherID", text12, "RowIndex", num6, sqlTransaction).ToString();
						text2 = new Databases(base.DBConfig).GetFieldValue("PO_Shipment_Detail", "SourceVoucherID", "SysDocID", text13, "VoucherID", text12, "RowIndex", num6, sqlTransaction).ToString();
						if (!(text2 == "") && !(text3 == ""))
						{
							num7 = int.Parse(new Databases(base.DBConfig).GetFieldValue("PO_Shipment_Detail", "SourceRowIndex", "SysDocID", text13, "VoucherID", text12, "RowIndex", num6, sqlTransaction).ToString());
							float result19 = 0f;
							if (row8["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row8["UnitQuantity"].ToString(), out result19);
							}
							else
							{
								float.TryParse(row8["Quantity"].ToString(), out result19);
							}
							float num8 = new Products(base.DBConfig).GetOrderedQuantity(text4, sqlTransaction) - result19;
							if (num8 < 0f)
							{
								num8 = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateOrderedQuantity(text4, num8, sqlTransaction);
							flag &= new PurchaseOrder(base.DBConfig).UpdateRowReceivedQuantity(text3, text2, num7, result19, sqlTransaction);
							flag &= new PurchaseOrder(base.DBConfig).UpdateRowShippedQuantity(text3, text2, num7, text4, -1f * result19, validateQuantity: false, sqlTransaction);
							flag &= new POShipment(base.DBConfig).UpdateRowReceivedQuantity(text13, text12, num6, result19, sqlTransaction);
						}
					}
					text2 = purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows[0]["OrderVoucherID"].ToString();
					text3 = purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows[0]["OrderSysDocID"].ToString();
					if (text2 != "")
					{
						flag &= new POShipment(base.DBConfig).CloseReceivedShipment(text3, text2, sqlTransaction);
					}
					break;
				default:
					if (!isUpdate && purchaseInvoiceData.Tables.Contains("GRN"))
					{
						foreach (DataRow row9 in purchaseInvoiceData.Tables["GRN"].Rows)
						{
							bool result18 = false;
							object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Purchase_Receipt", "IsInvoiced", "SysDocID", row9["SysDocID"].ToString(), "VoucherID", row9["VoucherID"].ToString(), sqlTransaction);
							if (fieldValue2 != null)
							{
								bool.TryParse(fieldValue2.ToString(), out result18);
							}
							if (result18)
							{
								throw new CompanyException("One or more goods received notes are previously invoice.");
							}
							flag &= (ExecuteNonQuery("UPDATE Purchase_Receipt SET IsInvoiced='True',InvoiceSysDocID='" + text6 + "',InvoiceVoucherID='" + text5 + "' WHERE SysDocID='" + row9["SysDocID"].ToString() + "' AND VoucherID='" + row9["VoucherID"].ToString() + "'", sqlTransaction) >= 0);
						}
					}
					break;
				}
				flag &= new InventoryTransaction(base.DBConfig).AllocateUnallocatedItemsToLot(text6, text5, sqlTransaction);
				if (purchaseInvoiceData.Tables.Contains("Tax_Detail") && purchaseInvoiceData.Tables["Tax_Detail"].Rows.Count > 0)
				{
					flag &= new TaxTransaction(base.DBConfig).InsertUpdateTaxTransaction(purchaseInvoiceData, text6, text5, isUpdate, sqlTransaction);
				}
				GLData journalData = CreateInvoiceGLData(purchaseInvoiceData, sqlTransaction);
				flag &= new Journal(base.DBConfig).InsertUpdateJournal(journalData, isUpdate, sqlTransaction);
				flag &= UpdateInventoryTransactionRowID(text6, text5, sqlTransaction);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Purchase_Invoice", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Purchase Invoice";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text5, text6, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text5, text6, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Purchase_Invoice", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				if (!result2)
				{
					if (!result)
					{
						flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PurchaseInvoice, text6, text5, "Purchase_Invoice", sqlTransaction);
						return flag;
					}
					flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.CashPurchase, text6, text5, "Purchase_Invoice", sqlTransaction);
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ImportPurchaseInvoice, text6, text5, "Purchase_Invoice", sqlTransaction);
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

		private bool ValidateSoftClosePeriod(PurchaseInvoiceData purchaseInvoiceData, bool isUpdate, SqlTransaction sqlTransaction)
		{
			try
			{
				DateTime t = DateTime.Parse(DateTime.Parse(purchaseInvoiceData.PurchaseInvoiceTable.Rows[0]["TransactionDate"].ToString()).ToShortDateString());
				new CompanyInformations(base.DBConfig).GetFiscalStartDate();
				SoftClosePeriod softClosePeriod = SoftClosePeriod.Yearly;
				object companyOptionValue = new CompanyOption(base.DBConfig).GetCompanyOptionValue(10102.ToString());
				if (companyOptionValue != null)
				{
					softClosePeriod = (SoftClosePeriod)int.Parse(companyOptionValue.ToString());
				}
				new SqlCommand();
				foreach (DataRow row in purchaseInvoiceData.InvoiceGRNTable.Rows)
				{
					bool flag = false;
					string text = row["GRNSysDocID"].ToString();
					string text2 = row["GRNNumber"].ToString();
					SqlCommand sqlCommand = new SqlCommand("Select CONVERT(date,TransactionDate)  From Purchase_Receipt Where SysDocID=@SysDocID AND VoucherID=@VoucherID", base.DBConfig.Connection);
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
							throw new CompanyException("The GRN  " + text + "/" + text2 + "   date is out of soft close period.");
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

		private GLData CreateInvoiceGLData(PurchaseInvoiceData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.PurchaseInvoiceTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string text = dataRow["VendorID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				string value = dataRow["CompanyID"].ToString();
				string value2 = dataRow["DivisionID"].ToString();
				bool result = false;
				if (!dataRow["PriceIncludeTax"].IsDBNullOrEmpty())
				{
					bool.TryParse(dataRow["PriceIncludeTax"].ToString(), out result);
				}
				string textCommand = "SELECT SD.LocationID,ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID ,LOC.COGSAccountID,LOC.DiscountReceivedAccountID,\r\n                                      LOC.InventoryAccountID,LOC.SalesAccountID,LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID\r\n                                      FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                      LEFT OUTER JOIN Vendor VEN ON VendorID='" + text + "'\r\n                                       LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                      LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                      WHERE SysDocID = '" + text2 + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				string docLocationID = dataRow2["LocationID"].ToString();
				string text3 = dataRow2["DiscountReceivedAccountID"].ToString();
				dataRow2["SalesTaxAccountID"].ToString();
				string text4 = dataRow2["APAccountID"].ToString();
				string text5 = dataRow2["BaseCurrencyID"].ToString();
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
				bool.TryParse(dataRow["IsImport"].ToString(), out result4);
				if (!result3 && text4 == "")
				{
					throw new CompanyException("Account payable is not selected for this vendor. Please select an account payable for the location or this vendor.", 5000);
				}
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.PurchaseInvoice;
				if (result3)
				{
					sysDocTypes = SysDocTypes.CashPurchase;
				}
				else if (result4)
				{
					sysDocTypes = SysDocTypes.ImportPurchaseInvoice;
				}
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["CurrencyID"] = dataRow["CurrencyID"];
				dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Purchase Invoice - ";
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				decimal num = default(decimal);
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				new Hashtable();
				new ArrayList();
				Hashtable hashtable2 = new Hashtable();
				ArrayList arrayList2 = new ArrayList();
				Hashtable hashtable3 = new Hashtable();
				ArrayList arrayList3 = new ArrayList();
				Hashtable hashtable4 = new Hashtable();
				ArrayList arrayList4 = new ArrayList();
				string value3 = "";
				string value4 = "";
				decimal d = default(decimal);
				decimal d2 = default(decimal);
				decimal num2 = default(decimal);
				foreach (DataRow row in transactionData.PurchaseInvoiceDetailTable.Rows)
				{
					decimal result5 = default(decimal);
					string text6 = row["ProductID"].ToString();
					string warehouseLocationID = row["LocationID"].ToString();
					int.Parse(row["RowIndex"].ToString());
					dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(text6, docLocationID, warehouseLocationID, text2, sqlTransaction);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException("Product accounts information not found for product or location.");
					}
					DataRow dataRow5 = dataSet.Tables[0].Rows[0];
					string text7 = dataRow5["InventoryAssetAccountID"].ToString();
					string text8 = dataRow5["COGSAccountID"].ToString();
					decimal result6 = default(decimal);
					decimal result7 = default(decimal);
					decimal result8 = default(decimal);
					decimal.TryParse(row["TaxAmount"].ToString(), out result8);
					if (flag)
					{
						decimal.TryParse(row["AmountFC"].ToString(), out result6);
					}
					else
					{
						decimal.TryParse(row["Amount"].ToString(), out result6);
					}
					if (row["LCostAmount"] != DBNull.Value)
					{
						decimal.TryParse(row["LCostAmount"].ToString(), out result7);
					}
					ItemTypes itemTypes = ItemTypes.Inventory;
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "ItemType", "ProductID", text6, sqlTransaction);
					if (fieldValue == null || !(fieldValue.ToString() != ""))
					{
						throw new CompanyException("Item type is not selected for the product:" + text6);
					}
					itemTypes = (ItemTypes)byte.Parse(fieldValue.ToString());
					if (row["UnitQuantity"] != DBNull.Value)
					{
						decimal.TryParse(row["UnitQuantity"].ToString(), out result5);
					}
					else
					{
						decimal.TryParse(row["Quantity"].ToString(), out result5);
					}
					decimal result9 = default(decimal);
					if (flag)
					{
						decimal.TryParse(row["UnitPriceFC"].ToString(), out result9);
					}
					else
					{
						decimal.TryParse(row["UnitPrice"].ToString(), out result9);
					}
					decimal num3 = default(decimal);
					num3 = ((!(result5 >= 0m)) ? Math.Round(result6, currencyDecimalPoints) : Math.Round(result6, currencyDecimalPoints));
					if (result)
					{
						num3 -= Math.Round(result8, 4);
					}
					switch (itemTypes)
					{
					case ItemTypes.Inventory:
					case ItemTypes.Assembly:
					{
						string text9 = text7;
						if (hashtable2.ContainsKey(text9))
						{
							num = decimal.Parse(hashtable2[text9].ToString());
							if (result)
							{
								num += Math.Round(num3, 4);
							}
							else
							{
								num += Math.Round(num3, currencyDecimalPoints);
							}
							hashtable2[text9] = Math.Round(num, 4);
							num = decimal.Parse(hashtable3[text9].ToString());
							num += Math.Round(result7, currencyDecimalPoints);
							hashtable3[text9] = num;
						}
						else
						{
							if (result)
							{
								hashtable2.Add(text9, Math.Round(num3, 4));
							}
							else
							{
								hashtable2.Add(text9, Math.Round(num3, currencyDecimalPoints));
							}
							arrayList2.Add(text9);
							hashtable3.Add(text9, Math.Round(result7, currencyDecimalPoints));
							arrayList3.Add(text9);
						}
						if (result)
						{
							d += Math.Round(result6, currencyDecimalPoints);
						}
						else
						{
							d += Math.Round(result6, currencyDecimalPoints);
						}
						d2 += Math.Round(result7, currencyDecimalPoints);
						break;
					}
					case ItemTypes.NonInventory:
					case ItemTypes.Service:
					case ItemTypes.Discount:
					{
						num = Math.Round(result6, currencyDecimalPoints);
						string text9 = text8;
						if (hashtable.ContainsKey(text9))
						{
							num = decimal.Parse(hashtable[text9].ToString());
							num += Math.Round(num3, currencyDecimalPoints);
							hashtable[text9] = num;
						}
						else
						{
							hashtable.Add(text9, Math.Round(num3, currencyDecimalPoints));
							arrayList.Add(text9);
						}
						num2 += Math.Round(result6, currencyDecimalPoints);
						if ((itemTypes == ItemTypes.Service || itemTypes == ItemTypes.NonInventory) && !hashtable4.ContainsKey(text9))
						{
							value4 = transactionData.PurchaseInvoiceDetailTable.Rows[0]["JobID"].ToString();
							string value5 = row["JobID"].ToString();
							hashtable4.Add(text9, value5);
							arrayList4.Add(value5);
						}
						break;
					}
					}
				}
				if (d != 0m)
				{
					for (int i = 0; i < hashtable2.Count; i++)
					{
						DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
						dataRow6.BeginEdit();
						string text9 = arrayList2[i].ToString();
						num = decimal.Parse(hashtable2[text9].ToString());
						num = Math.Round(num, currencyDecimalPoints);
						dataRow6["JournalID"] = 0;
						dataRow6["AccountID"] = text9;
						dataRow6["PayeeID"] = text;
						if (flag)
						{
							dataRow6["DebitFC"] = num;
							dataRow6["CreditFC"] = DBNull.Value;
						}
						else
						{
							dataRow6["Debit"] = num;
							dataRow6["Credit"] = DBNull.Value;
						}
						dataRow6["JVEntryType"] = (byte)1;
						dataRow6["Reference"] = dataRow["Reference"];
						dataRow6["CompanyID"] = value;
						dataRow6["DivisionID"] = value2;
						dataRow6.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow6);
					}
				}
				if (num2 != 0m)
				{
					for (int j = 0; j < hashtable.Count; j++)
					{
						DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
						dataRow6.BeginEdit();
						string text9 = arrayList[j].ToString();
						num = decimal.Parse(hashtable[text9].ToString());
						value3 = hashtable4[text9].ToString();
						dataRow6["JournalID"] = 0;
						dataRow6["AccountID"] = text9;
						dataRow6["PayeeID"] = text;
						dataRow6["JobID"] = value3;
						if (flag)
						{
							if (num < 0m)
							{
								dataRow6["DebitFC"] = DBNull.Value;
								dataRow6["CreditFC"] = Math.Abs(num);
							}
							else
							{
								dataRow6["DebitFC"] = num;
								dataRow6["CreditFC"] = DBNull.Value;
							}
						}
						else if (num < 0m)
						{
							dataRow6["Debit"] = DBNull.Value;
							dataRow6["Credit"] = Math.Abs(num);
						}
						else
						{
							dataRow6["Debit"] = num;
							dataRow6["Credit"] = DBNull.Value;
						}
						dataRow6["Reference"] = dataRow["Reference"];
						dataRow6["JVEntryType"] = (byte)4;
						dataRow6["CompanyID"] = value;
						dataRow6["DivisionID"] = value2;
						dataRow6.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow6);
					}
				}
				if (d2 != 0m)
				{
					for (int k = 0; k < hashtable3.Count; k++)
					{
						DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
						dataRow6.BeginEdit();
						string text9 = arrayList3[k].ToString();
						num = decimal.Parse(hashtable3[text9].ToString());
						dataRow6["JournalID"] = 0;
						dataRow6["AccountID"] = text9;
						dataRow6["PayeeID"] = DBNull.Value;
						dataRow6["IsBaseOnly"] = true;
						dataRow6["Debit"] = num;
						dataRow6["Credit"] = DBNull.Value;
						dataRow6["Reference"] = dataRow["Reference"];
						dataRow6["JVEntryType"] = (byte)9;
						dataRow6["CompanyID"] = value;
						dataRow6["DivisionID"] = value2;
						dataRow6.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow6);
					}
				}
				decimal num4 = default(decimal);
				foreach (DataRow row2 in transactionData.InvoiceExpenseTable.Rows)
				{
					string text10 = row2["ExpenseID"].ToString();
					num = decimal.Parse(row2["Amount"].ToString());
					decimal result10 = default(decimal);
					if (row2["AmountFC"] != DBNull.Value)
					{
						decimal.TryParse(row2["AmountFC"].ToString(), out result10);
					}
					string expenseAccountID = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text10, sqlTransaction);
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					dataRow6["JournalID"] = 0;
					dataRow6["AccountID"] = expenseAccountID;
					string a = (string)(dataRow6["CurrencyID"] = row2["CurrencyID"].ToString());
					dataRow6["CurrencyRate"] = row2["CurrencyRate"];
					if (a != text5)
					{
						dataRow6["DebitFC"] = DBNull.Value;
						dataRow6["CreditFC"] = result10;
						dataRow6["Debit"] = DBNull.Value;
						dataRow6["Credit"] = num;
					}
					else
					{
						dataRow6["DebitFC"] = DBNull.Value;
						dataRow6["CreditFC"] = result10;
						dataRow6["Debit"] = DBNull.Value;
						dataRow6["Credit"] = num;
					}
					dataRow6["JVEntryType"] = (byte)9;
					dataRow6["CompanyID"] = value;
					dataRow6["DivisionID"] = value2;
					dataRow6["Reference"] = text10;
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
					num4 += num;
				}
				decimal result11 = default(decimal);
				decimal result12 = default(decimal);
				if (dataRow["DiscountFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["DiscountFC"].ToString(), out result11);
				}
				else
				{
					decimal.TryParse(dataRow["Discount"].ToString(), out result11);
				}
				if (dataRow["TaxAmountFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["TaxAmountFC"].ToString(), out result12);
				}
				else
				{
					decimal.TryParse(dataRow["TaxAmount"].ToString(), out result12);
				}
				PayeeTaxOptions payeeTaxOptions = PayeeTaxOptions.NonTaxable;
				if (dataRow["TaxOption"] != DBNull.Value)
				{
					payeeTaxOptions = (PayeeTaxOptions)byte.Parse(dataRow["TaxOption"].ToString());
				}
				if (payeeTaxOptions == PayeeTaxOptions.ReverseCharge && result)
				{
					throw new CompanyException("Price inclusive of tax is not allowed for reverse charge calculation.");
				}
				if (result11 > 0m)
				{
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					dataRow6["JournalID"] = 0;
					if (text3 == "")
					{
						throw new CompanyException("You have entered a discount amount for this transaction but there is no discount account selected for this location.\nPlease select the 'Discount Received' account for this location.", 1040);
					}
					dataRow6["AccountID"] = text3;
					dataRow6["PayeeID"] = text;
					dataRow6["PayeeType"] = "A";
					dataRow6["JobID"] = value3;
					if (flag)
					{
						dataRow6["DebitFC"] = DBNull.Value;
						dataRow6["CreditFC"] = result11;
					}
					else
					{
						dataRow6["Debit"] = DBNull.Value;
						dataRow6["Credit"] = result11;
					}
					dataRow6["Reference"] = dataRow["Reference"];
					dataRow6["JVEntryType"] = (byte)5;
					dataRow6["CompanyID"] = value;
					dataRow6["DivisionID"] = value2;
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
				}
				if (result12 > 0m)
				{
					if (result12 > 0m)
					{
						if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
						{
							throw new CompanyException("Tax details not found for the transaction.");
						}
						DataRow[] array = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
						decimal num5 = default(decimal);
						for (int l = 0; l < array.Length; l++)
						{
							num5 = default(decimal);
							DataRow obj = array[l];
							DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
							dataRow6.BeginEdit();
							dataRow6["JournalID"] = 0;
							string text12 = "";
							text12 = obj["TaxItemID"].ToString();
							string text13 = "";
							textCommand = "SELECT PurchaseTaxAccountID FROM Tax WHERE  TaxCode = '" + text12.Trim() + "'";
							object obj2 = ExecuteScalar(textCommand);
							if (obj2 != null)
							{
								text13 = obj2.ToString();
							}
							if (text13 == "")
							{
								throw new CompanyException("Purchase tax account is not set for tax item: " + text12 + ".");
							}
							decimal.TryParse(obj["TaxAmount"].ToString(), out num5);
							dataRow6["AccountID"] = text13;
							dataRow6["PayeeID"] = text;
							dataRow6["PayeeType"] = "A";
							if (flag)
							{
								dataRow6["DebitFC"] = Math.Round(num5, currencyDecimalPoints, MidpointRounding.AwayFromZero);
								dataRow6["CreditFC"] = DBNull.Value;
							}
							else
							{
								dataRow6["Debit"] = Math.Round(num5, currencyDecimalPoints, MidpointRounding.AwayFromZero);
								dataRow6["Credit"] = DBNull.Value;
							}
							dataRow6["Reference"] = dataRow["Reference"];
							dataRow6["JVEntryType"] = (byte)6;
							dataRow6["CompanyID"] = value;
							dataRow6["DivisionID"] = value2;
							dataRow6.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow6);
						}
					}
					if (payeeTaxOptions == PayeeTaxOptions.ReverseCharge)
					{
						DataRow[] array2 = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
						decimal num6 = default(decimal);
						for (int m = 0; m < array2.Length; m++)
						{
							num6 = default(decimal);
							DataRow obj3 = array2[m];
							DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
							dataRow6.BeginEdit();
							dataRow6["JournalID"] = 0;
							string text14 = "";
							text14 = obj3["TaxItemID"].ToString();
							string text15 = "";
							textCommand = "SELECT TaxReverseChargeAccountID FROM Tax WHERE  TaxCode = '" + text14.Trim() + "'";
							object obj4 = ExecuteScalar(textCommand);
							if (obj4 != null)
							{
								text15 = obj4.ToString();
							}
							if (text15 == "")
							{
								throw new CompanyException("Reverse tax account is not set for tax item: " + text14 + ".");
							}
							decimal.TryParse(obj3["TaxAmount"].ToString(), out num6);
							dataRow6["AccountID"] = text15;
							dataRow6["PayeeID"] = text;
							dataRow6["PayeeType"] = "A";
							if (flag)
							{
								dataRow6["DebitFC"] = DBNull.Value;
								dataRow6["CreditFC"] = Math.Round(num6, currencyDecimalPoints, MidpointRounding.AwayFromZero);
							}
							else
							{
								dataRow6["Debit"] = DBNull.Value;
								dataRow6["Credit"] = Math.Round(num6, currencyDecimalPoints, MidpointRounding.AwayFromZero);
							}
							dataRow6["Reference"] = "Tax Reverse Charge " + dataRow["Reference"];
							dataRow6["JVEntryType"] = (byte)6;
							dataRow6["CompanyID"] = value;
							dataRow6["DivisionID"] = value2;
							dataRow6.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow6);
						}
					}
				}
				if (result3)
				{
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					dataRow6["JournalID"] = 0;
					string registerID = dataRow["RegisterID"].ToString();
					string text9 = (string)(dataRow6["AccountID"] = new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID"));
					dataRow6["PayeeID"] = text;
					dataRow6["PayeeType"] = "A";
					dataRow6["IsARAP"] = false;
					decimal num7 = d + num2 - result11 + result12;
					if ((payeeTaxOptions == PayeeTaxOptions.ReverseCharge) | result)
					{
						num7 = d + num2 - result11;
					}
					if (flag)
					{
						dataRow6["DebitFC"] = DBNull.Value;
						dataRow6["CreditFC"] = num7;
					}
					else
					{
						dataRow6["Debit"] = DBNull.Value;
						dataRow6["Credit"] = num7;
					}
					dataRow6["Reference"] = dataRow["Reference"];
					string text16 = "";
					text16 = ((!string.IsNullOrWhiteSpace(dataRow["Note"].ToString()) && dataRow["Note"].ToString().Length >= 20) ? dataRow["Note"].ToString().Substring(0, 20) : dataRow["Note"].ToString());
					string text17 = new Databases(base.DBConfig).GetFieldValue("Vendor", "VendorName", "VendorID", text, sqlTransaction).ToString();
					dataRow6["Description"] = "Ref1: " + dataRow["Reference"] + " Ref2: " + dataRow["Reference2"] + " Party: " + text + "- " + text17 + " Note: " + text16;
					dataRow6["CompanyID"] = value;
					dataRow6["DivisionID"] = value2;
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
				}
				else
				{
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					dataRow6["JournalID"] = 0;
					dataRow6["AccountID"] = text4;
					dataRow6["PayeeID"] = text;
					dataRow6["PayeeType"] = "V";
					dataRow6["IsARAP"] = true;
					dataRow6["JobID"] = value4;
					dataRow6["JVEntryType"] = (byte)8;
					dataRow6["CompanyID"] = value;
					dataRow6["DivisionID"] = value2;
					decimal num8 = d + num2 - result11 + result12;
					if ((payeeTaxOptions == PayeeTaxOptions.ReverseCharge) | result)
					{
						num8 = d + num2 - result11;
					}
					if (flag)
					{
						dataRow6["DebitFC"] = DBNull.Value;
						dataRow6["CreditFC"] = num8;
					}
					else
					{
						dataRow6["Debit"] = DBNull.Value;
						dataRow6["Credit"] = num8;
					}
					dataRow6["Reference"] = dataRow["Reference"];
					string text18 = "";
					text18 = ((!string.IsNullOrWhiteSpace(dataRow["Note"].ToString()) && dataRow["Note"].ToString().Length >= 20) ? dataRow["Note"].ToString().Substring(0, 20) : dataRow["Note"].ToString());
					string text19 = new Databases(base.DBConfig).GetFieldValue("Vendor", "VendorName", "VendorID", text, sqlTransaction).ToString();
					dataRow6["Description"] = "Ref1: " + dataRow["Reference"] + " Ref2: " + dataRow["Reference2"] + " Party: " + text + "- " + text19 + " Note: " + text18;
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		private GLData OLD_CreateInvoiceGLDataNew(PurchaseInvoiceData transactionData, SqlTransaction sqlTransaction)
		{
			try
			{
				GLData gLData = new GLData();
				DataRow dataRow = transactionData.PurchaseInvoiceTable.Rows[0];
				int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
				string text = dataRow["VendorID"].ToString();
				string text2 = dataRow["SysDocID"].ToString();
				bool result = false;
				if (!dataRow["PriceIncludeTax"].IsDBNullOrEmpty())
				{
					bool.TryParse(dataRow["PriceIncludeTax"].ToString(), out result);
				}
				decimal num = default(decimal);
				string textCommand = "SELECT SD.LocationID,ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID ,LOC.COGSAccountID,LOC.DiscountReceivedAccountID,\r\n                                      LOC.InventoryAccountID,LOC.SalesAccountID,LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID\r\n                                      FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                      LEFT OUTER JOIN Vendor VEN ON VendorID='" + text + "'\r\n                                       LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                      LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                      WHERE SysDocID = '" + text2 + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				string docLocationID = dataRow2["LocationID"].ToString();
				string text3 = dataRow2["DiscountReceivedAccountID"].ToString();
				dataRow2["SalesTaxAccountID"].ToString();
				string text4 = dataRow2["APAccountID"].ToString();
				string text5 = dataRow2["BaseCurrencyID"].ToString();
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
				bool.TryParse(dataRow["IsImport"].ToString(), out result4);
				if (!result3 && text4 == "")
				{
					throw new CompanyException("Account payable is not selected for this vendor. Please select an account payable for the location or this vendor.", 5000);
				}
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.PurchaseInvoice;
				if (result3)
				{
					sysDocTypes = SysDocTypes.CashPurchase;
				}
				else if (result4)
				{
					sysDocTypes = SysDocTypes.ImportPurchaseInvoice;
				}
				dataRow3["JournalID"] = 0;
				dataRow3["JournalDate"] = dataRow["TransactionDate"];
				dataRow3["SysDocID"] = dataRow["SysDocID"];
				dataRow3["SysDocType"] = (byte)sysDocTypes;
				dataRow3["VoucherID"] = dataRow["VoucherID"];
				dataRow3["CurrencyID"] = dataRow["CurrencyID"];
				dataRow3["CurrencyRate"] = dataRow["CurrencyRate"];
				dataRow3["Reference"] = dataRow["Reference"];
				dataRow3["Narration"] = "Purchase Invoice - ";
				dataRow3.EndEdit();
				gLData.JournalTable.Rows.Add(dataRow3);
				decimal num2 = default(decimal);
				Hashtable hashtable = new Hashtable();
				ArrayList arrayList = new ArrayList();
				new Hashtable();
				new ArrayList();
				Hashtable hashtable2 = new Hashtable();
				ArrayList arrayList2 = new ArrayList();
				Hashtable hashtable3 = new Hashtable();
				ArrayList arrayList3 = new ArrayList();
				Hashtable hashtable4 = new Hashtable();
				ArrayList arrayList4 = new ArrayList();
				string value = "";
				string value2 = "";
				decimal d = default(decimal);
				decimal d2 = default(decimal);
				decimal num3 = default(decimal);
				foreach (DataRow row in transactionData.PurchaseInvoiceDetailTable.Rows)
				{
					decimal num4 = default(decimal);
					string text6 = row["ProductID"].ToString();
					string warehouseLocationID = row["LocationID"].ToString();
					int.Parse(row["RowIndex"].ToString());
					dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(text6, docLocationID, warehouseLocationID, text2, sqlTransaction);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException("Product accounts information not found for product or location.");
					}
					DataRow dataRow5 = dataSet.Tables[0].Rows[0];
					string text7 = dataRow5["InventoryAssetAccountID"].ToString();
					string text8 = dataRow5["COGSAccountID"].ToString();
					decimal result5 = default(decimal);
					decimal result6 = default(decimal);
					decimal result7 = default(decimal);
					decimal.TryParse(row["TaxAmount"].ToString(), out result7);
					num += result7;
					if (flag)
					{
						decimal.TryParse(row["AmountFC"].ToString(), out result5);
					}
					else
					{
						decimal.TryParse(row["Amount"].ToString(), out result5);
					}
					if (row["LCostAmount"] != DBNull.Value)
					{
						decimal.TryParse(row["LCostAmount"].ToString(), out result6);
					}
					ItemTypes itemTypes = ItemTypes.Inventory;
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "ItemType", "ProductID", text6, sqlTransaction);
					if (fieldValue == null || !(fieldValue.ToString() != ""))
					{
						throw new CompanyException("Item type is not selected for the product:" + text6);
					}
					itemTypes = (ItemTypes)byte.Parse(fieldValue.ToString());
					num4 = ((row["UnitQuantity"] == DBNull.Value) ? decimal.Parse(row["Quantity"].ToString()) : decimal.Parse(row["UnitQuantity"].ToString()));
					decimal result8 = default(decimal);
					if (flag)
					{
						decimal.TryParse(row["UnitPriceFC"].ToString(), out result8);
					}
					else
					{
						decimal.TryParse(row["UnitPrice"].ToString(), out result8);
					}
					decimal num5 = default(decimal);
					num5 = ((!(num4 >= 0m)) ? Math.Round(result5, currencyDecimalPoints) : Math.Round(result5, currencyDecimalPoints));
					if (result)
					{
						num5 -= Math.Round(result7, 4);
					}
					switch (itemTypes)
					{
					case ItemTypes.Inventory:
					case ItemTypes.Assembly:
					{
						string text9 = text7;
						if (hashtable2.ContainsKey(text9))
						{
							num2 = decimal.Parse(hashtable2[text9].ToString());
							if (result)
							{
								num2 += Math.Round(num5, 4);
							}
							else
							{
								num2 += Math.Round(num5, currencyDecimalPoints);
							}
							hashtable2[text9] = Math.Round(num2, 4);
							num2 = decimal.Parse(hashtable3[text9].ToString());
							num2 += Math.Round(result6, currencyDecimalPoints);
							hashtable3[text9] = num2;
						}
						else
						{
							if (result)
							{
								hashtable2.Add(text9, Math.Round(num5, 4));
							}
							else
							{
								hashtable2.Add(text9, Math.Round(num5, currencyDecimalPoints));
							}
							arrayList2.Add(text9);
							hashtable3.Add(text9, Math.Round(result6, currencyDecimalPoints));
							arrayList3.Add(text9);
						}
						if (result)
						{
							d += Math.Round(result5, currencyDecimalPoints);
						}
						else
						{
							d += Math.Round(result5, currencyDecimalPoints);
						}
						d2 += Math.Round(result6, currencyDecimalPoints);
						break;
					}
					case ItemTypes.NonInventory:
					case ItemTypes.Service:
					case ItemTypes.Discount:
					{
						num2 = Math.Round(result5, currencyDecimalPoints);
						string text9 = text8;
						if (hashtable.ContainsKey(text9))
						{
							num2 = decimal.Parse(hashtable[text9].ToString());
							num2 += Math.Round(num5, currencyDecimalPoints);
							hashtable[text9] = num2;
						}
						else
						{
							hashtable.Add(text9, Math.Round(num5, currencyDecimalPoints));
							arrayList.Add(text9);
						}
						num3 += Math.Round(result5, currencyDecimalPoints);
						if ((itemTypes == ItemTypes.Service || itemTypes == ItemTypes.NonInventory) && !hashtable4.ContainsKey(text9))
						{
							value2 = transactionData.PurchaseInvoiceDetailTable.Rows[0]["JobID"].ToString();
							string value3 = row["JobID"].ToString();
							hashtable4.Add(text9, value3);
							arrayList4.Add(value3);
						}
						break;
					}
					}
				}
				if (d != 0m)
				{
					for (int i = 0; i < hashtable2.Count; i++)
					{
						DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
						dataRow6.BeginEdit();
						string text9 = arrayList2[i].ToString();
						num2 = decimal.Parse(hashtable2[text9].ToString());
						num2 = Math.Round(num2, currencyDecimalPoints);
						dataRow6["JournalID"] = 0;
						dataRow6["AccountID"] = text9;
						dataRow6["PayeeID"] = text;
						if (flag)
						{
							dataRow6["DebitFC"] = num2;
							dataRow6["CreditFC"] = DBNull.Value;
						}
						else
						{
							dataRow6["Debit"] = num2;
							dataRow6["Credit"] = DBNull.Value;
						}
						dataRow6["Reference"] = dataRow["Reference"];
						dataRow6.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow6);
					}
				}
				if (num3 != 0m)
				{
					for (int j = 0; j < hashtable.Count; j++)
					{
						DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
						dataRow6.BeginEdit();
						string text9 = arrayList[j].ToString();
						num2 = decimal.Parse(hashtable[text9].ToString());
						value = hashtable4[text9].ToString();
						dataRow6["JournalID"] = 0;
						dataRow6["AccountID"] = text9;
						dataRow6["PayeeID"] = text;
						dataRow6["JobID"] = value;
						if (flag)
						{
							if (num2 < 0m)
							{
								dataRow6["DebitFC"] = DBNull.Value;
								dataRow6["CreditFC"] = Math.Abs(num2);
							}
							else
							{
								dataRow6["DebitFC"] = num2;
								dataRow6["CreditFC"] = DBNull.Value;
							}
						}
						else if (num2 < 0m)
						{
							dataRow6["Debit"] = DBNull.Value;
							dataRow6["Credit"] = Math.Abs(num2);
						}
						else
						{
							dataRow6["Debit"] = num2;
							dataRow6["Credit"] = DBNull.Value;
						}
						dataRow6["Reference"] = dataRow["Reference"];
						dataRow6.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow6);
					}
				}
				if (d2 != 0m)
				{
					for (int k = 0; k < hashtable3.Count; k++)
					{
						DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
						dataRow6.BeginEdit();
						string text9 = arrayList3[k].ToString();
						num2 = decimal.Parse(hashtable3[text9].ToString());
						dataRow6["JournalID"] = 0;
						dataRow6["AccountID"] = text9;
						dataRow6["PayeeID"] = DBNull.Value;
						dataRow6["IsBaseOnly"] = true;
						dataRow6["Debit"] = num2;
						dataRow6["Credit"] = DBNull.Value;
						dataRow6["Reference"] = dataRow["Reference"];
						dataRow6.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow6);
					}
				}
				decimal num6 = default(decimal);
				foreach (DataRow row2 in transactionData.InvoiceExpenseTable.Rows)
				{
					string text10 = row2["ExpenseID"].ToString();
					num2 = decimal.Parse(row2["Amount"].ToString());
					decimal result9 = default(decimal);
					if (row2["AmountFC"] != DBNull.Value)
					{
						decimal.TryParse(row2["AmountFC"].ToString(), out result9);
					}
					string expenseAccountID = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text10, sqlTransaction);
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					dataRow6["JournalID"] = 0;
					dataRow6["AccountID"] = expenseAccountID;
					string a = (string)(dataRow6["CurrencyID"] = row2["CurrencyID"].ToString());
					dataRow6["CurrencyRate"] = row2["CurrencyRate"];
					if (a != text5)
					{
						dataRow6["DebitFC"] = DBNull.Value;
						dataRow6["CreditFC"] = result9;
						dataRow6["Debit"] = DBNull.Value;
						dataRow6["Credit"] = num2;
					}
					else
					{
						dataRow6["DebitFC"] = DBNull.Value;
						dataRow6["CreditFC"] = result9;
						dataRow6["Debit"] = DBNull.Value;
						dataRow6["Credit"] = num2;
					}
					dataRow6["Reference"] = text10;
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
					num6 += num2;
				}
				decimal result10 = default(decimal);
				decimal result11 = default(decimal);
				if (dataRow["DiscountFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["DiscountFC"].ToString(), out result10);
				}
				else
				{
					decimal.TryParse(dataRow["Discount"].ToString(), out result10);
				}
				if (dataRow["TaxAmountFC"] != DBNull.Value)
				{
					decimal.TryParse(dataRow["TaxAmountFC"].ToString(), out result11);
				}
				else
				{
					decimal.TryParse(dataRow["TaxAmount"].ToString(), out result11);
				}
				PayeeTaxOptions payeeTaxOptions = PayeeTaxOptions.NonTaxable;
				if (dataRow["TaxOption"] != DBNull.Value)
				{
					payeeTaxOptions = (PayeeTaxOptions)byte.Parse(dataRow["TaxOption"].ToString());
				}
				if (payeeTaxOptions == PayeeTaxOptions.ReverseCharge && result)
				{
					throw new CompanyException("Price inclusive of tax is not allowed for reverse charge calculation.");
				}
				if (result10 > 0m)
				{
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					dataRow6["JournalID"] = 0;
					if (text3 == "")
					{
						throw new CompanyException("You have entered a discount amount for this transaction but there is no discount account selected for this location.\nPlease select the 'Discount Received' account for this location.", 1040);
					}
					dataRow6["AccountID"] = text3;
					dataRow6["PayeeID"] = text;
					dataRow6["PayeeType"] = "A";
					dataRow6["JobID"] = value;
					if (flag)
					{
						dataRow6["DebitFC"] = DBNull.Value;
						dataRow6["CreditFC"] = result10;
					}
					else
					{
						dataRow6["Debit"] = DBNull.Value;
						dataRow6["Credit"] = result10;
					}
					dataRow6["Reference"] = dataRow["Reference"];
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
				}
				if (result11 > 0m)
				{
					if (result11 > 0m)
					{
						if (transactionData.Tables["Tax_Detail"].Rows.Count <= 0)
						{
							throw new CompanyException("Tax details not found for the transaction.");
						}
						DataRow[] array = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
						decimal num7 = default(decimal);
						for (int l = 0; l < array.Length; l++)
						{
							num7 = default(decimal);
							DataRow obj = array[l];
							DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
							dataRow6.BeginEdit();
							dataRow6["JournalID"] = 0;
							string text12 = "";
							text12 = obj["TaxItemID"].ToString();
							string text13 = "";
							textCommand = "SELECT PurchaseTaxAccountID FROM Tax WHERE  TaxCode = '" + text12.Trim() + "'";
							object obj2 = ExecuteScalar(textCommand);
							if (obj2 != null)
							{
								text13 = obj2.ToString();
							}
							if (text13 == "")
							{
								throw new CompanyException("Purchase tax account is not set for tax item: " + text12 + ".");
							}
							decimal.TryParse(obj["TaxAmount"].ToString(), out num7);
							dataRow6["AccountID"] = text13;
							dataRow6["PayeeID"] = text;
							dataRow6["PayeeType"] = "A";
							if (flag)
							{
								dataRow6["DebitFC"] = Math.Round(num, currencyDecimalPoints, MidpointRounding.AwayFromZero);
								dataRow6["CreditFC"] = DBNull.Value;
							}
							else
							{
								dataRow6["Debit"] = Math.Round(num, currencyDecimalPoints, MidpointRounding.AwayFromZero);
								dataRow6["Credit"] = DBNull.Value;
							}
							dataRow6["Reference"] = dataRow["Reference"];
							dataRow6.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow6);
						}
					}
					if (payeeTaxOptions == PayeeTaxOptions.ReverseCharge)
					{
						DataRow[] array2 = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
						decimal num8 = default(decimal);
						for (int m = 0; m < array2.Length; m++)
						{
							num8 = default(decimal);
							DataRow obj3 = array2[m];
							DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
							dataRow6.BeginEdit();
							dataRow6["JournalID"] = 0;
							string text14 = "";
							text14 = obj3["TaxItemID"].ToString();
							string text15 = "";
							textCommand = "SELECT TaxReverseChargeAccountID FROM Tax WHERE  TaxCode = '" + text14.Trim() + "'";
							object obj4 = ExecuteScalar(textCommand);
							if (obj4 != null)
							{
								text15 = obj4.ToString();
							}
							if (text15 == "")
							{
								throw new CompanyException("Reverse tax account is not set for tax item: " + text14 + ".");
							}
							decimal.TryParse(obj3["TaxAmount"].ToString(), out num8);
							dataRow6["AccountID"] = text15;
							dataRow6["PayeeID"] = text;
							dataRow6["PayeeType"] = "A";
							if (flag)
							{
								dataRow6["DebitFC"] = DBNull.Value;
								dataRow6["CreditFC"] = Math.Round(num, 2, MidpointRounding.AwayFromZero);
							}
							else
							{
								dataRow6["Debit"] = DBNull.Value;
								dataRow6["Credit"] = Math.Round(num, 2, MidpointRounding.AwayFromZero);
							}
							dataRow6["Reference"] = "Tax Reverse Charge " + dataRow["Reference"];
							dataRow6.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow6);
						}
					}
				}
				if (result3)
				{
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					dataRow6["JournalID"] = 0;
					string registerID = dataRow["RegisterID"].ToString();
					string text9 = (string)(dataRow6["AccountID"] = new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID"));
					dataRow6["PayeeID"] = text;
					dataRow6["PayeeType"] = "A";
					dataRow6["IsARAP"] = false;
					decimal num9 = d + num3 - result10 + num;
					if ((payeeTaxOptions == PayeeTaxOptions.ReverseCharge) | result)
					{
						num9 = d + num3 - result10;
					}
					if (flag)
					{
						dataRow6["DebitFC"] = DBNull.Value;
						dataRow6["CreditFC"] = num9;
					}
					else
					{
						dataRow6["Debit"] = DBNull.Value;
						dataRow6["Credit"] = num9;
					}
					dataRow6["Reference"] = dataRow["Reference"];
					string text16 = "";
					text16 = ((!string.IsNullOrWhiteSpace(dataRow["Note"].ToString()) && dataRow["Note"].ToString().Length >= 20) ? dataRow["Note"].ToString().Substring(0, 20) : dataRow["Note"].ToString());
					string text17 = new Databases(base.DBConfig).GetFieldValue("Vendor", "VendorName", "VendorID", text, sqlTransaction).ToString();
					dataRow6["Description"] = "Ref1: " + dataRow["Reference"] + " Ref2: " + dataRow["Reference2"] + " Party: " + text + "- " + text17 + " Note: " + text16;
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
				}
				else
				{
					DataRow dataRow6 = gLData.JournalDetailsTable.NewRow();
					dataRow6.BeginEdit();
					dataRow6["JournalID"] = 0;
					dataRow6["AccountID"] = text4;
					dataRow6["PayeeID"] = text;
					dataRow6["PayeeType"] = "V";
					dataRow6["IsARAP"] = true;
					dataRow6["JobID"] = value2;
					decimal num10 = d + num3 - result10 + num;
					if ((payeeTaxOptions == PayeeTaxOptions.ReverseCharge) | result)
					{
						num10 = d + num3 - result10;
					}
					if (flag)
					{
						dataRow6["DebitFC"] = DBNull.Value;
						dataRow6["CreditFC"] = num10;
					}
					else
					{
						dataRow6["Debit"] = DBNull.Value;
						dataRow6["Credit"] = num10;
					}
					dataRow6["Reference"] = dataRow["Reference"];
					string text18 = "";
					text18 = ((!string.IsNullOrWhiteSpace(dataRow["Note"].ToString()) && dataRow["Note"].ToString().Length >= 20) ? dataRow["Note"].ToString().Substring(0, 20) : dataRow["Note"].ToString());
					string text19 = new Databases(base.DBConfig).GetFieldValue("Vendor", "VendorName", "VendorID", text, sqlTransaction).ToString();
					dataRow6["Description"] = "Ref1: " + dataRow["Reference"] + " Ref2: " + dataRow["Reference2"] + " Party: " + text + "- " + text19 + " Note: " + text18;
					dataRow6.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow6);
				}
				return gLData;
			}
			catch
			{
				throw;
			}
		}

		public bool ValidateInventoryLockDate(PurchaseInvoiceData purchaseInvoiceData, SqlTransaction sqlTransaction)
		{
			DataRow dataRow = purchaseInvoiceData.PurchaseInvoiceTable.Rows[0];
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

		public PurchaseInvoiceData GetPurchaseInvoiceByID(string sysDocID, string voucherID)
		{
			try
			{
				PurchaseInvoiceData purchaseInvoiceData = new PurchaseInvoiceData();
				string textCommand = "SELECT *, V.TaxIDNumber FROM Purchase_Invoice PI INNER JOIN Vendor V ON PI.VendorID = V.VendorID WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceData, "Purchase_Invoice", textCommand);
				if (purchaseInvoiceData == null || purchaseInvoiceData.Tables.Count == 0 || purchaseInvoiceData.Tables["Purchase_Invoice"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.ItemType,Product.IsTrackLot,Product.IsTrackSerial,Product.Weight, Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID\r\n\t\t\t\t\t\tFROM Purchase_Invoice_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "' ORDER BY TD.RowIndex ";
				FillDataSet(purchaseInvoiceData, "Purchase_Invoice_Detail", textCommand);
				textCommand = "SELECT * FROM Invoice_GRN\r\n\t\t\t\t\t\tWHERE InvoiceNumber='" + voucherID + "' AND InvoiceSysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceData, "Invoice_GRN", textCommand);
				textCommand = "SELECT PE.*,PC.Amount as 'Allowed' FROM Purchase_Invoice_Expense PE\r\n                        LEFT  JOIN Purchase_Cost_Entry_Detail PC ON PE.PCVoucherID=PC.VoucherID AND  PE.PCSysDocID=PC.SysDocID AND PE.PCRowIndex=PC.RowIndex\r\n\t\t\t\t\t\tWHERE PE.InvoiceVoucherID='" + voucherID + "' AND PE.InvoiceSysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceData, "Purchase_Invoice_Expense", textCommand);
				textCommand = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceData, "Product_Lot_Receiving_Detail", textCommand);
				textCommand = "SELECT * FROM Purchase_Order_Detail POD LEFT JOIN Purchase_Invoice_Detail PID ON POD.SysDocID=PID.OrderSysDocID AND POD.VoucherID=PID.OrderVoucherID\r\n\t\t\t\t\t\tWHERE PID.VoucherID='" + voucherID + "' AND PID.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceData, "Purchase_Order_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceData, "Tax_Detail", textCommand);
				return purchaseInvoiceData;
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePurchaseInvoiceDetailsRows(string sysDocID, string voucherID, bool isDeletingTransaction, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				PurchaseInvoiceData purchaseInvoiceData = new PurchaseInvoiceData();
				string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid,ISNULL(IsCash,'False') AS IsCash, ISNULL(IsImport,'False') AS IsImport FROM Purchase_Invoice_Detail SOD INNER JOIN Purchase_Invoice SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n\t\t\t\t\t\t\t  WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(purchaseInvoiceData, "Purchase_Invoice_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows[0]["IsCash"].ToString(), out result2);
				bool result3 = false;
				bool.TryParse(purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows[0]["IsImport"].ToString(), out result3);
				string text = "";
				text = ((!result3) ? new CompanyOption(base.DBConfig).GetCompanyOptionValue(58.ToString()).ToString() : new CompanyOption(base.DBConfig).GetCompanyOptionValue(59.ToString()).ToString());
				PurchaseFlows purchaseFlows = PurchaseFlows.DirectInvoice;
				if (text != "")
				{
					purchaseFlows = (PurchaseFlows)int.Parse(text.ToString());
				}
				if (!result)
				{
					flag = (result2 ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(34, sysDocID, voucherID, isDeletingTransaction, sqlTransaction)) : ((!result3) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(33, sysDocID, voucherID, isDeletingTransaction, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(39, sysDocID, voucherID, isDeletingTransaction, sqlTransaction))));
					string text2 = "";
					string text3 = "";
					string text4 = "";
					foreach (DataRow row in purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows)
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
							float num = new Products(base.DBConfig).GetOrderedQuantity(text4, sqlTransaction) + result5;
							if (num < 0f)
							{
								num = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateOrderedQuantity(text4, num, sqlTransaction);
							if (purchaseFlows == PurchaseFlows.POThenInvoice)
							{
								flag &= new PurchaseOrder(base.DBConfig).UpdateRowReceivedQuantity(text3, text2, result4, -1f * result5, sqlTransaction);
							}
						}
					}
				}
				textCommand = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Purchase_Invoice_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				return flag & new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePurchaseInvoiceExpenseRows(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				flag = DeletePurchaseCostDetails(sysDocID, voucherID, sqlTransaction);
				string commandText = "DELETE FROM Purchase_Invoice_Expense WHERE InvoiceSysDocID = '" + sysDocID + "' AND InvoiceVoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
			}
			catch
			{
				throw;
			}
		}

		internal bool DeletePurchaseCostDetails(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				PurchaseInvoiceData purchaseInvoiceData = new PurchaseInvoiceData();
				string textCommand = "SELECT * FROM Purchase_Invoice_Expense\r\n\t\t\t\t\t\tWHERE InvoiceVoucherID='" + voucherID + "' AND InvoiceSysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceData, "Purchase_Invoice_Expense", textCommand);
				if (purchaseInvoiceData.Tables["Purchase_Invoice_Expense"].Rows.Count > 0)
				{
					foreach (DataRow row in purchaseInvoiceData.InvoiceExpenseTable.Rows)
					{
						row["ExpenseID"].ToString();
						string text = row["PCVoucherID"].ToString();
						string text2 = row["PCSysDocID"].ToString();
						int result = 0;
						double result2 = 0.0;
						if (!(text == "") && !(text2 == ""))
						{
							int.TryParse(row["PCRowIndex"].ToString(), out result);
							double.TryParse(row["Amount"].ToString(), out result2);
							flag &= new PurchaseCostEntry(base.DBConfig).UpdateRowReceivedAmount(text2, text, result, result2, isDelete: true, sqlTransaction);
						}
					}
					return flag;
				}
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool HoldPurchaseInvoice(string sysDocID, string voucherID, bool isHold)
		{
			bool result = true;
			try
			{
				result = HoldPurchaseInvoice(sysDocID, voucherID, isHold, null);
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

		private bool HoldPurchaseInvoice(string sysDocID, string voucherID, bool isHold, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				PurchaseInvoiceData purchaseInvoiceData = new PurchaseInvoiceData();
				string textCommand = "SELECT * FROM Purchase_Invoice WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(purchaseInvoiceData, "Purchase_Invoice", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(purchaseInvoiceData.PurchaseInvoiceTable.Rows[0]["IsHoldForPayment"].ToString(), out result);
				textCommand = "UPDATE Purchase_Invoice SET IsHoldForPayment = '" + isHold.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
			}
			catch
			{
				throw;
			}
		}

		public bool VoidPurchaseInvoice(string sysDocID, string voucherID, bool isVoid)
		{
			bool result = true;
			try
			{
				result = VoidPurchaseInvoice(sysDocID, voucherID, isVoid, null);
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

		private bool VoidPurchaseInvoice(string sysDocID, string voucherID, bool isVoid, SqlTransaction sqlTransaction)
		{
			bool flag = true;
			try
			{
				if (sqlTransaction == null)
				{
					sqlTransaction = base.DBConfig.StartNewTransaction();
				}
				if (new Products(base.DBConfig).HasInUseLots(sysDocID, voucherID))
				{
					throw new CompanyException("This transaction cannot be modifed because some of items are refered by other transactions.");
				}
				PurchaseInvoiceData purchaseInvoiceData = new PurchaseInvoiceData();
				string textCommand = "SELECT PID.*,ISVOID,IsCash,IsImport,TransactionDate FROM Purchase_Invoice_Detail PID INNER JOIN Purchase_Invoice PI ON PI.SysDocID=PID.SysDocID AND PI.VOucherID=PID.VoucherID\r\n\t\t\t\t\t\t\t  WHERE PID.SysDocID = '" + sysDocID + "' AND PID.VoucherID = '" + voucherID + "'";
				FillDataSet(purchaseInvoiceData, "Purchase_Invoice_Detail", textCommand, sqlTransaction);
				textCommand = "SELECT * FROM Invoice_GRN WHERE InvoiceSysDocID='" + sysDocID + "' AND InvoiceNumber='" + voucherID + "'";
				FillDataSet(purchaseInvoiceData, "Invoice_GRN", textCommand, sqlTransaction);
				textCommand = "SELECT * FROM Purchase_Invoice WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(purchaseInvoiceData, "Purchase_Invoice", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows[0]["IsCash"].ToString(), out result2);
				bool result3 = false;
				bool.TryParse(purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows[0]["IsImport"].ToString(), out result3);
				DateTime.Parse(purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows[0]["TransactionDate"].ToString());
				if (result == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				flag = (result2 ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(34, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : ((!result3) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(33, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(39, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction))));
				string text = "";
				string text2 = "";
				string text3 = "";
				foreach (DataRow row in purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows)
				{
					text3 = row["ProductID"].ToString();
					text = row["OrderVoucherID"].ToString();
					text2 = row["OrderSysDocID"].ToString();
					int result4 = 0;
					if (!(text == "") && !(text2 == ""))
					{
						ItemSourceTypes itemSourceTypes = ItemSourceTypes.None;
						object obj = row["RowSource"].ToString();
						if (obj != null && obj.ToString() != "")
						{
							itemSourceTypes = (ItemSourceTypes)byte.Parse(obj.ToString());
						}
						if (itemSourceTypes == ItemSourceTypes.PurchaseOrder)
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
							float num = new Products(base.DBConfig).GetReservedQuantity(text3, sqlTransaction) + result5;
							if (num < 0f)
							{
								num = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateReservedQuantity(text3, num, sqlTransaction);
							flag &= new PurchaseOrder(base.DBConfig).UpdateRowReceivedQuantity(text2, text, result4, -1f * result5, sqlTransaction);
							flag &= new PurchaseOrder(base.DBConfig).ReOpenOrder(text2, text, sqlTransaction);
						}
					}
				}
				ItemSourceTypes itemSourceTypes2 = ItemSourceTypes.None;
				DataRow dataRow2 = purchaseInvoiceData.Tables["Purchase_Invoice"].Rows[0];
				if (dataRow2["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes2 = (ItemSourceTypes)byte.Parse(dataRow2["SourceDocType"].ToString());
				}
				if (itemSourceTypes2 == ItemSourceTypes.GRN)
				{
					textCommand = " UPDATE IT SET IsNonCostedGRN = 'True' FROM Inventory_Transactions IT \r\n                                INNER JOIN Purchase_Invoice_Detail PID ON IT.SysdocID = PID.OrderSysDocID AND IT.VoucherID = PID.OrderVoucherID AND IT.RowIndex = PID.OrderRowIndex\r\n                                WHERE PID.SysDocID = '" + sysDocID + "' AND PID.VoucherID = '" + voucherID + "'";
				}
				textCommand = "UPDATE Purchase_Receipt SET IsInvoiced='False',InvoiceSysDocID=NULL,InvoiceVoucherID=NULL WHERE InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				textCommand = "UPDATE Purchase_Invoice SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Purchase_Invoice", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction, isInsert: false);
				ActivityTypes activityType = ActivityTypes.Void;
				if (!isVoid)
				{
					activityType = ActivityTypes.Unvoid;
				}
				AddActivityLog("Purchase Invoice", voucherID, sysDocID, activityType, sqlTransaction);
				return flag;
			}
			catch
			{
				throw;
			}
		}

		public bool DeletePurchaseInvoice(string sysDocID, string voucherID)
		{
			bool flag = true;
			try
			{
				string text = "";
				SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
				bool result = false;
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Purchase_Invoice", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidPurchaseInvoice(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeletePurchaseInvoiceDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Purchase_Invoice WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= DeletePurchaseInvoiceExpenseRows(sysDocID, voucherID, sqlTransaction);
				flag &= new TaxTransaction(base.DBConfig).DeleteTaxTransactionDetailsRows(sysDocID, voucherID, sqlTransaction);
				if (!flag)
				{
					return flag;
				}
				ActivityTypes activityType = ActivityTypes.Delete;
				AddActivityLog("Purchase Invoice", voucherID, sysDocID, activityType, sqlTransaction);
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

		internal bool CloseReceivedInvoice(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string str = "SELECT COUNT(RowIndex)FROM Purchase_Invoice_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				str += " AND CASE WHEN UnitQuantity IS NULL THEN Quantity ELSE UnitQuantity END - ISNULL(QuantityReceived,0)=0";
				object obj = ExecuteScalar(str, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				str = "UPDATE Purchase_Invoice SET IsDelivered = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				return ExecuteNonQuery(str, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		internal bool UpdateRowReceivedQuantity(string sysDocID, string voucherID, int rowIndex, float quantity, SqlTransaction sqlTransaction)
		{
			DataSet dataSet = new DataSet();
			float result = 0f;
			float num = 0f;
			try
			{
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReceived FROM Purchase_Invoice_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				}
				num += quantity;
				textCommand = "UPDATE Purchase_Invoice_Detail SET QuantityReceived=" + num.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
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
				string exp = "UPDATE PID SET ITRowID = CASE WHEN ISNULL(RowSource,1) = 2  THEN  (SELECT TransactionID FROM Inventory_Transactions IT \r\n                                WHERE IT.SysDocID = PID.OrderSysDocID AND IT.VoucherID = PID.OrderVoucherID AND IT.RowIndex = PID.OrderRowIndex) \r\n                                    ELSE(SELECT TransactionID FROM Inventory_Transactions IT WHERE IT.SysDocID = PID.SysDocID AND IT.VoucherID = PID.VoucherID AND IT.RowIndex = PID.RowIndex) END\r\n                                    FROM Purchase_Invoice_Detail PID INNER JOIN Purchase_Invoice PI ON PI.SysDocID = PID.SysDocID AND PI.VoucherID = PID.VoucherID\r\n                                     where PID.SysDocID = '" + sysDocID + "' and PID.voucherid = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				throw;
			}
		}

		internal bool UpdateRowReturnedQuantity(string sysDocID, string voucherID, SqlTransaction sqlTransaction)
		{
			try
			{
				string exp = "  UPDATE  PID  SET QuantityReturned = \r\n                                     (SELECT SUM(Quantity) FROM Purchase_Return_Detail PRD  INNER JOIN Purchase_Return PR                                      \r\n                                     ON PR.SysDocID = PRD.SysDocID AND PR.VoucherID = PRD.VoucherID\r\n\t                                 WHERE ISNULL(IsVoid,'False') = 'False' AND PRD.SourceSysDocID = PID.SysDocID AND PRD.SourceVoucherID = PID.VoucherID AND PRD.SourceRowIndex = PID.RowIndex )\r\n\t\t\t\t\t\t\t\t\t    FROM Purchase_Invoice_Detail PID WHERE PID.SysDocID = '" + sysDocID + "' AND PID.VoucherID = '" + voucherID + "'";
				return ExecuteNonQuery(exp, sqlTransaction) > 0;
			}
			catch
			{
				return false;
			}
		}

		public DataSet GetInvoicesForDelivery(string vendorID)
		{
			try
			{
				DataSet dataSet = new DataSet();
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor] FROM Purchase_Invoice SO\r\n\t\t\t\t\t\t\t INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsDelivered,0)=0";
				if (vendorID != "")
				{
					text = text + " AND SO.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "Purchase_Invoice", text);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseInvoiceToPrint(string sysDocID, string voucherID)
		{
			return GetPurchaseInvoiceToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetPurchaseInvoiceToPrint(string sysDocID, string[] voucherID)
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
				string cmdText = "SELECT  DISTINCT SI.*,Vendor.VendorName,B.FullName,TaxGroupName,\r\n                            (SELECT SUM(Amount) from [dbo].Purchase_Invoice_Expense where InvoiceSysDocID= '" + sysDocID + "' AND InvoiceVoucherID = " + text + ") AS EXPENSE,\r\n                            VA.AddressPrintFormat AS VendorAddress,ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                            ISNULL(SI.DiscountFC,SI.Discount) AS Discount,ISNULL(SI.TotalFC,SI.Total) - ISNULL(ISNULL(SI.DiscountFC,SI.Discount),0) AS GrandTotal,\r\n                            ISNULL(ISNULL(SI.TaxAmountFC,SI.TaxAmount) ,0) AS Tax,ISNULL(SI.TotalFC,SI.Total) AS Total,\r\n                            VA.Country as VendorCountry,SI.DueDate as InvoiceDueDate,CU.ExchangeRate as ExchangeRate,SM.ShippingMethodName,PT.TermName,\r\n                            (SELECT SUBSTRING(\r\n                            (SELECT  ',' + S.VoucherID FROM  Purchase_Receipt S  INNER JOIN Purchase_Invoice  D ON s.InvoiceVoucherID = D.VoucherID AND S.InvoiceSysDocID = D.SysDocID \r\n                                WHERE D.SysDocID = '" + sysDocID + "' AND D.VoucherID = " + text + " FOR XML PATH('')),2,20000)) AS  VoucherID,\r\n                            (SELECT SUBSTRING(\r\n                            (SELECT  ',' + CONVERT(varchar,  ( S.TransactionDate) , 106)  FROM  Purchase_Receipt S  INNER JOIN Purchase_Invoice  D ON s.InvoiceVoucherID = D.VoucherID AND S.InvoiceSysDocID = D.SysDocID \r\n                            WHERE D.SysDocID = '" + sysDocID + "' AND D.VoucherID = " + text + " FOR XML PATH('')),2,20000)) AS  TransactionDate,\r\n                            (SELECT SUBSTRING(\r\n                            (SELECT  ',' + S.VoucherID + ' / ' +  CONVERT(varchar,  ( S.TransactionDate) , 106) + ''  FROM  Purchase_Receipt S  INNER JOIN Purchase_Invoice  D ON s.InvoiceVoucherID = D.VoucherID AND S.InvoiceSysDocID = D.SysDocID \r\n                            WHERE D.SysDocID = '" + sysDocID + "' AND D.VoucherID = " + text + " FOR XML PATH('')),2,20000)) AS  [GRN DETAILS],\r\n                            (SELECT SUBSTRING(\r\n                            (SELECT  ',' + S.SourceVoucherID + ''  FROM  Purchase_Receipt S  INNER JOIN Purchase_Invoice  D ON s.InvoiceVoucherID = D.VoucherID AND S.InvoiceSysDocID = D.SysDocID \r\n                            WHERE D.SysDocID = '" + sysDocID + "' AND D.VoucherID = " + text + " FOR XML PATH('')),2,20000)) AS  [PO DETAILS], VA.Phone1, VA.fax, VA.Mobile, Va.Email,Vendor.TaxIDNumber as VTaxIDNo,\r\n                               (SELECT  DISTINCT TOP 1 SO.VoucherID FROM Purchase_Invoice_Detail SND \r\n\t\t\t\t\t\t\t    LEFT JOIN Purchase_Receipt_Detail DND ON SND.OrderSysDocID=DND.SysDocID AND SND.OrderVoucherID=DND.VoucherID\r\n\t\t\t\t\t\t\t    LEFT OUTER JOIN  Purchase_Order SO ON DND.OrderSysDocID=SO.SysDocID AND DND.OrderVoucherID=SO.VoucherID\r\n\t\t\t\t\t\t\t    WHERE SND.SysDocID=SI.SysDocID AND SND.VoucherID=SI.VoucherID) AS [LPONo],\r\n\t\t\t\t\t\t\t\t(SELECT  DISTINCT TOP 1  convert(varchar,  ( SO.TransactionDate) , 106) FROM Purchase_Invoice_Detail SND \r\n\t\t\t\t\t\t\t\tLEFT JOIN Purchase_Receipt_Detail DND ON SND.OrderSysDocID=DND.SysDocID AND SND.OrderVoucherID=DND.VoucherID\r\n\t\t\t\t\t\t\t\tLEFT OUTER JOIN  Purchase_Order SO ON DND.OrderSysDocID=SO.SysDocID AND DND.OrderVoucherID=SO.VoucherID\r\n\t\t\t\t\t\t\t\tWHERE SND.SysDocID=SI.SysDocID AND SND.VoucherID=SI.VoucherID) AS [LPODate],\r\n                                SUBSTRING((SELECT DISTINCT  ' ' +convert(varchar,  ( D.TransactionDate) , 106) + ', ' \r\n                                FROM Purchase_Invoice_Detail S  INNER JOIN Purchase_receipt  D ON S.OrderSysDocID = D.SysDocID AND S.OrderVoucherID = D.VoucherID \r\n                                WHERE S.VoucherID = SI.VoucherID AND S.SysDocID = SI.SysDocID FOR XML PATH('')),2,20000) AS  GRNDate,\r\n                                \r\n                                SUBSTRING((SELECT DISTINCT ' ' + D.VoucherID +  ', ' \r\n                                FROM Purchase_Invoice_Detail S  INNER JOIN Purchase_Receipt  D ON S.OrderSysDocID = D.SysDocID AND S.OrderVoucherID = D.VoucherID \r\n                                WHERE S.VoucherID = SI.VoucherID  AND S.SysDocID = SI.SysDocID FOR XML PATH('')),2,20000) AS  GRN_No\r\n\r\n                            FROM  Purchase_Invoice SI INNER JOIN Vendor ON SI.VendorID=Vendor.VendorID\r\n                            LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                            LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'\r\n                            LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                            LEFT OUTER JOIN Currency CU ON CU.CurrencyID=SI.CurrencyID\r\n                            LEFT OUTER JOIN Buyer B ON B.BuyerID=SI.BuyerID\r\n                            LEFT OUTER JOIN Tax_Group TX ON SI.PayeeTaxGroupID=TX.TaxGroupID\r\n                            WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Purchase_Invoice", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Purchase_Invoice"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,PID.ProductID,PID.Description,ISNULL(UnitQuantity,PID.Quantity) AS Quantity,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,\r\n                        ISNULL(UnitPriceFC,PID.UnitPrice) AS UnitPrice,UnitPrice as UnitPriceLC,\r\n                        ISNULL(UnitQuantity,PID.Quantity)*ISNULL(UnitPriceFC,PID.UnitPrice) AS Total,PID.LCost,PID.LCostAmount,\r\n                        ISNULL(UnitQuantity,PID.Quantity)*ISNULL(PID.UnitPrice,0) AS TotalLC,\r\n                        PID.UnitID,LocationID, P.BrandID, PID.TaxAmount, PID.TaxGroupID, TG.TaxGroupName, PB.BrandName, P.Description2, PID.Remarks,PID.OrderSysDocID,PID.OrderVoucherID,PID.Discount\r\n                        FROM   Purchase_Invoice_Detail PID \r\n                        INNER JOIN Product P ON P.ProductID=PID.ProductID\r\n                        LEFT JOIN Tax_Group TG ON PID.TaxGroupID=TG.TaxGroupID\r\n\t\t\t\t\t\tLEFT JOIN Product_Brand PB ON P.BrandID=PB.BrandID\r\n\t\t\t\t\t\tWHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Purchase_Invoice_Detail", cmdText);
				cmdText = "SELECT  * FROM Purchase_Invoice_Expense WHERE InvoiceSysDocID = '" + sysDocID + "' AND InvoiceVoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Purchase_Invoice_Expense", cmdText);
				dataSet.Relations.Add("VendorInvoice", new DataColumn[2]
				{
					dataSet.Tables["Purchase_Invoice"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Invoice"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Purchase_Invoice_Detail"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Invoice_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Purchase_Invoice"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Purchase_Invoice"].Rows)
				{
					decimal result = default(decimal);
					decimal result2 = default(decimal);
					decimal result3 = default(decimal);
					decimal result4 = default(decimal);
					decimal.TryParse(row["Total"].ToString(), out result);
					decimal.TryParse(row["Discount"].ToString(), out result2);
					decimal.TryParse(row["GrandTotal"].ToString(), out result3);
					decimal.TryParse(row["Tax"].ToString(), out result4);
					int currencyDecimalPoints = new CompanyInformations(base.DBConfig).CurrencyDecimalPoints;
					if ((PayeeTaxOptions)row["TaxOption"] != PayeeTaxOptions.ReverseCharge)
					{
						row["TotalInWords"] = NumToWord.GetNumInWords(result3 + result4, currencyDecimalPoints);
					}
					else
					{
						row["TotalInWords"] = NumToWord.GetNumInWords(result3, currencyDecimalPoints);
					}
				}
				if (dataSet.Tables["Purchase_Invoice_Expense"].Rows.Count > 0)
				{
					dataSet.Relations.Add("VendorInvoiceExpense", new DataColumn[2]
					{
						dataSet.Tables["Purchase_Invoice"].Columns["SysDocID"],
						dataSet.Tables["Purchase_Invoice"].Columns["VoucherID"]
					}, new DataColumn[2]
					{
						dataSet.Tables["Purchase_Invoice_Expense"].Columns["InvoiceSysDocID"],
						dataSet.Tables["Purchase_Invoice_Expense"].Columns["InvoiceVoucherID"]
					}, createConstraints: false);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseExpenseAllocationReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string sysDocID, string voucherID, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(fromDate);
				string text2 = StoreConfiguration.ToSqlDateTimeString(toDate);
				string empty = string.Empty;
				DataSet dataSet = new DataSet();
				empty = "SELECT PID.SysDocID, PID.VoucherID, PID.ProductID, PID.Description, PID.Quantity [Qty Purchased], P.Weight, PID.UnitID, UnitPrice [Factory Cost], UnitPriceFC [Factory Cost FC], PID.Amount [Purchased Amount], PID.AmountFC [Purchased Amount FC], \r\n\t\t\t\t        LCost * PID.Quantity [Direct Cost Weightwise], LCost [Direct Cost per Unit], UnitPrice + LCost [Per Unit Cost], V.VendorID + ' - ' + VendorName [Vendor] ,\r\n                        PM.ManufacturerName,PS.StyleName,C.CountryName\r\n\t                  FROM Purchase_Invoice_Detail PID\r\n\t                  INNER JOIN Purchase_Invoice PI ON PID.SysDocID = PI.SysDocID AND PID.VoucherID = PI.VoucherID\r\n\t                  INNER JOIN Product P ON PID.ProductID = P.ProductID \r\n                      INNER JOIN Vendor V ON PI.VendorID = V.VendorID \r\n                                    LEFT OUTER JOIN Product_Manufacturer PM On PM.ManufacturerId=P.ManufacturerId\r\n\t\t\t\t\t\t            LEFT OUTER JOIN Product_Style PS On PS.StyleId=P.StyleId\r\n\t\t\t\t\t\t            LEFT OUTER JOIN Country C On C.CountryId=P.Origin \r\n                        WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND ISNULL(PI.IsVoid, 'False') = 'False'";
				if (sysDocID != "")
				{
					empty = empty + " AND PI.SysDocID = '" + sysDocID + "' ";
				}
				if (voucherID != "")
				{
					empty = empty + " AND PI.VoucherID  = '" + voucherID + "' ";
				}
				if (fromItem != "")
				{
					empty = empty + " AND P.ProductID >= '" + fromItem + "' ";
				}
				if (toItem != "")
				{
					empty = empty + " AND P.ProductID <= '" + toItem + "' ";
				}
				if (fromClass != "")
				{
					empty = empty + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE ClassID >= '" + fromClass + "')";
				}
				if (toClass != "")
				{
					empty = empty + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE ClassID <= '" + toClass + "')";
				}
				if (fromCategory != "")
				{
					empty = empty + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID >= '" + fromCategory + "')";
				}
				if (toCategory != "")
				{
					empty = empty + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE CategoryID <= '" + toCategory + "')";
				}
				if (fromManufacturer != "")
				{
					empty = empty + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE ManufacturerID BETWEEN '" + fromManufacturer + "' AND '" + toManufacturer + "') ";
				}
				if (fromStyle != "")
				{
					empty = empty + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE StyleID BETWEEN '" + fromStyle + "' AND '" + toStyle + "') ";
				}
				if (fromOrigin != "")
				{
					empty = empty + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE Origin BETWEEN '" + fromOrigin + "' AND '" + toOrigin + "') ";
				}
				if (fromBrand != "")
				{
					empty = empty + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE BrandID >= '" + fromBrand + "')";
				}
				if (toBrand != "")
				{
					empty = empty + " AND P.ProductID IN (SELECT ProductID FROM Product WHERE BrandID <= '" + toBrand + "')";
				}
				FillDataSet(dataSet, "Purchase_Invoice_Detail", empty);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   ISNULL(IsVoid,'False') AS V,  SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],\r\n\t\t\t\t\t\t\tReference,TransactionDate [Invoice Date], DueDate,INV.ShippingMethodID AS [Shipping Method],\r\n\t\t\t\t\t\t\tINV.TermID Term,\r\n\t\t\t\t\t\t\tCASE ISNULL(IsCash,'False') WHEN 'True' THEN 'Cash' ELSE CASE ISNULL(IsImport,'False') WHEN 'True' THEN 'Import' ELSE 'Credit' END END AS [Type],\r\n\t\t\t\t\t\t\tINV.BuyerID [Purchaseperson],INV.CurrencyID AS Currency, INV.CurrencyRate AS [Cur Rate],TotalFC - ISNULL(DiscountFC,0) AS [Amount FC],Total - ISNULL(Discount,0) [Amount], Reference, Reference2, ISNULL((CASE INV.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge'  END) ,(CASE Vendor.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge'  END))AS TAXOPTION,INV.TaxAmount\r\n\t\t\t\t\t\t\tFROM         Purchase_Invoice INV\r\n\t\t\t\t\t\t\tInner JOIN Vendor ON VENDOR.VendorID=INV.VendorID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			if (sysDocID != "")
			{
				text3 = text3 + " AND INV.SysDocID = '" + sysDocID + "'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Invoice", sqlCommand);
			return dataSet;
		}

		public DataSet GetPurchaseInvoiceList(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date]    \r\n                            FROM Purchase_Invoice \r\n                            WHERE ISNULL(IsVoid,'False')='False'";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + " AND SysDocID='" + sysDocID + "'";
			}
			str += " ORDER BY TransactionDate, VoucherID ";
			SqlCommand sqlCommand = new SqlCommand(str);
			FillDataSet(dataSet, "Purchase_Invoice", sqlCommand);
			return dataSet;
		}

		public DataSet GetPurchaseList(string sysDocID, DateTime from, DateTime to)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date],Ven.VendorName AS [Vendor],PI.ContainerNumber AS [Container#]\r\n                            FROM Purchase_Invoice  PI INNER JOIN Vendor VEN ON PI.VendorID = Ven.VendorID\r\n                            WHERE ISNULL(IsVoid,'False')='False'  AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + " AND SysDocID='" + sysDocID + "'";
			}
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "Purchase_Invoice", str);
			return dataSet;
		}

		public DataSet GetPaymentAllocationDetails(string sysDocID, string VoucherID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT APP.VendorID, APJ.Reference,APJ.ChequeNumber,APJ.APAccountID,\r\n                            InvoiceVoucherID [Invoice No],PaymentSysDocID,PaymentVoucherID,ISNULL(PI.TotalFC,PI.Total)-ISNULL(PI.DiscountFC,PI.Discount)+ISNULL(PI.TaxAmountFC,PI.TaxAmount) AS Total,PaymentAmount,APJ.APDate AS [Invoice Date], APJ.APDueDate [Due Date]\r\n                            FROM dbo.AP_Payment_Allocation APP INNER JOIN Vendor V ON V.VendorID = APP.VendorID\r\n                            LEFT JOIN Purchase_Invoice PI ON APP.InvoiceSysDocID=PI.SysDocID and APP.InvoiceVoucherID=PI.VoucherID\r\n                            INNER JOIN APJournal APJ ON APP.APJournalID = APJ.APID\r\n                          \r\n                            where APP.InvoiceSysDocID= '" + sysDocID + "' AND APP.InvoiceVoucherID='" + VoucherID + "' GROUP BY PaymentSysDocID,APP.VendorID,InvoiceVoucherID,APJ.APDate, APJ.APDueDate,APJ.CurrencyID,APJ.CurrencyRate,APJ.Debit,APJ.Reference, V.Balance, V.PDCAmount,PaymentVoucherID,PaymentAmount,PI.Total ,APJournalID,APJ.ChequeNumber,APJ.APAccountID,APJ.PaymentMethodType,PI.Discount,PI.TotalFC,PI.DiscountFC,PI.TaxAmountFC,PI.TaxAmount");
			FillDataSet(dataSet, "paymentallocationDetails", sqlCommand);
			return dataSet;
		}

		public bool IsAlreadyExisting(string sysDocID, string voucherID, string vendorID, string supplierDocNo)
		{
			SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
			string exp = "SELECT COUNT(*) FROM Purchase_Invoice PI  WHERE ISNULL(IsVoid,'False')='False' AND SysDocID = '" + sysDocID + "' AND VoucherID <>'" + voucherID + "' AND VendorID= '" + vendorID + "' AND VendorReferenceNo='" + supplierDocNo + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			base.DBConfig.EndTransaction(result: true);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}

		public DataSet GetPaymentAadviceDetails(string sysDocID, string voucherID)
		{
			DataSet dataSet = new DataSet();
			string text = " SELECT * ,(SELECT count(*) FROM AP_Payment_Allocation APL \r\n                    inner join System_Document SD ON SD.SysDocID=APL.PaymentSysDocID \r\n                    WHERE APL.InvoiceSysDocID = APA.InvoiceSysDocID AND APL.InvoiceVoucherID = APA.InvoiceVoucherID AND SD.SysDocType='248') as [IsTRAppOnly] FROM AP_Payment_Advice APA WHERE APA.InvoiceSysDocID='" + sysDocID + "' AND APA.InvoiceVoucherID = '" + voucherID + "'";
			new SqlCommand(text);
			FillDataSet(dataSet, "AP_Payment_Advice", text);
			return dataSet;
		}

		public DataSet GetPrePaymentDetails(string sysDocID, string voucherID)
		{
			DataSet dataSet = new DataSet();
			string text = "SELECT SysDocID,VoucherID,TransactionDate [Date],V.VendorName [Vendor],ISNULL(PPI.AMOUNTFC, PPI.Amount) AS InvoiceAmount,ISNULL(PPI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) Cur\r\n                ,SourceSysDocID [POSysDocID],SourceVoucherID [POVoucherID],POAmount [PO Amount],(  ISNULL((SELECT CASE WHEN CurrencyID IS NULL OR CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n                                THEN SUM(PaymentAmount) + SUM(ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) + SUM(ISNULL(ISNULL(DiscountAmountFC,DiscountAmount),0)) END   \r\n\t\t\t\t\t\t\t\tFROM AP_Payment_Allocation PA WHERE Pa.InvoiceVoucherID= PPI.VoucherID AND PA.InvoiceSysDocID = PPI.SysDocID AND PA.VendorID= PPI.VendorID   GROUP BY CurrencyID),0)) AS Paid,\r\n\r\n                                 ( ISNULL(PPI.AMOUNTFC, PPI.Amount) - ISNULL((SELECT CASE WHEN CurrencyID IS NULL OR CurrencyID =(SELECT CurrencyID FROM Currency WHERE IsBase='True')\r\n                                THEN SUM(PaymentAmount) + SUM(ISNULL(DiscountAmount,0)) ELSE SUM(ISNULL(ISNULL(PaymentAmountFC,PaymentAmount),0)) + SUM(ISNULL(ISNULL(DiscountAmountFC,DiscountAmount),0)) END   \r\n\t\t\t\t\t\t\t\tFROM AP_Payment_Allocation PA WHERE Pa.PaymentVoucherID= PPI.VoucherID AND PA.VendorID= PPI.VendorID   GROUP BY CurrencyID),0)) AS Unallocated,\r\n\t\t\t\t\t\t\t\tISNULL(Status,0) as Status\r\n                FROM Purchase_PrePayment_Invoice PPI LEFT JOIN Vendor V ON V.VendorID=PPI.VendorID WHERE SourceSysDocID = '" + sysDocID + "' AND SourceVoucherID = '" + voucherID + "'";
			new SqlCommand(text);
			FillDataSet(dataSet, "Purchase_PrePayment_Invoice", text);
			return dataSet;
		}

		public DataSet FindPrePaymentSourceDetails(string sysDocID, string voucherID, bool isInvoiced)
		{
			DataSet dataSet = new DataSet();
			string text = "";
			dataSet = GetPrePaymentDetails(sysDocID, voucherID);
			if (dataSet.Tables[0].Rows.Count == 0)
			{
				if (isInvoiced)
				{
					text = "SELECT * FROM Purchase_Receipt WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					new SqlCommand(text);
					FillDataSet(dataSet, "Purchase_Receipt", text);
					if (dataSet.Tables["Purchase_Receipt"].Rows.Count > 0)
					{
						sysDocID = dataSet.Tables["Purchase_Receipt"].Rows[0]["SourceSysDocID"].ToString();
						voucherID = dataSet.Tables["Purchase_Receipt"].Rows[0]["SourceVoucherID"].ToString();
					}
				}
				dataSet = GetPrePaymentDetails(sysDocID, voucherID);
				if (dataSet.Tables[0].Rows.Count == 0)
				{
					text = "SELECT * FROM PO_Shipment_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
					new SqlCommand(text);
					FillDataSet(dataSet, "PO_Shipment_Detail", text);
					if (dataSet.Tables["PO_Shipment_Detail"].Rows.Count > 0)
					{
						string sysDocID2 = dataSet.Tables["PO_Shipment_Detail"].Rows[0]["SourceSysDocID"].ToString();
						string voucherID2 = dataSet.Tables["PO_Shipment_Detail"].Rows[0]["SourceVoucherID"].ToString();
						dataSet = GetPrePaymentDetails(sysDocID2, voucherID2);
					}
				}
			}
			return dataSet;
		}
	}
}
