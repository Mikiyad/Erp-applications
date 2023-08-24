using Micromind.Common;
using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Data
{
	public sealed class PurchaseInvoiceNI : StoreObject
	{
		private const string PURCHASEINVOICE_TABLE = "Purchase_Invoice_NonInv";

		private const string PURCHASEINVOICEDETAIL_TABLE = "Purchase_Invoice_NonInv_Detail";

		private const string SYSDOCID_PARM = "@SysDocID";

		private const string VOUCHERID_PARM = "@VoucherID";

		private const string COMPANYID_PARM = "@CompanyID";

		private const string DIVISIONID_PARM = "@DivisionID";

		private const string VENDORID_PARM = "@VendorID";

		private const string ISIMPORT_PARM = "@IsImport";

		private const string PURCHASEFLOW_PARM = "@PurchaseFlow";

		private const string TRANSACTIONDATE_PARM = "@TransactionDate";

		private const string BUYERID_PARM = "@BuyerID";

		private const string PRICEINCLUDETAX_PARM = "@PriceIncludeTax";

		private const string ROWINDEX_PARM = "@RowIndex";

		private const string STATUS_PARM = "@Status";

		private const string CURRENCYID_PARM = "@CurrencyID";

		private const string CURRENCYRATE_PARM = "@CurrencyRate";

		private const string TERMID_PARM = "@TermID";

		private const string SHIPPINGMETHODID_PARM = "@ShippingMethodID";

		private const string REFERENCE_PARM = "@Reference";

		private const string REFERENCE2_PARM = "@Reference2";

		private const string NOTE_PARM = "@Note";

		private const string PONUMBER_PARM = "@PONumber";

		private const string BOLNO_PARM = "@BOLNo";

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

		private const string SUPPLIERINVOICENUMBER_PARM = "@SupplierInvoiceNumber";

		private const string CONTAINERNUMBER_PARM = "@ContainerNumber";

		private const string PORT_PARM = "@Port";

		private const string BOLNUMBER_PARM = "@BOLNumber";

		private const string SHIPPER_PARM = "@Shipper";

		private const string CLEARINGAGENT_PARM = "@ClearingAgent";

		private const string JOBID_PARM = "@JobID";

		private const string COSTCATEGORYID_PARM = "@CostCategoryID";

		private const string PAYEETAXGROUPID_PARM = "@PayeeTaxGroupID";

		private const string TAXOPTION_PARM = "@TaxOption";

		private const string TAXGROUPID_PARM = "@TaxGroupID";

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

		private const string REMARKS_PARM = "@Remarks";

		private const string ANALYSISID_PARM = "@AnalysisID";

		private const string ATTRIBUTEID1_PARM = "@AttributeID1";

		private const string ATTRIBUTEID2_PARM = "@AttributeID2";

		private const string PURCHASEINVOICEEXPENSETABLE_PARM = "@Purchase_Invoice_NonInv_Expense";

		private const string INVOICEVOUCHERID_PARM = "@InvoiceVoucherID";

		private const string EXPENSEID_PARM = "@ExpenseID";

		private const string RATETYPE_PARM = "@RateType";

		public PurchaseInvoiceNI(Config config)
			: base(config)
		{
		}

		private string GetInsertUpdatePurchaseInvoiceText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Invoice_NonInv", new FieldValue("SysDocID", "@SysDocID", isUpdateConditionField: true), new FieldValue("VoucherID", "@VoucherID", isUpdateConditionField: true), new FieldValue("DivisionID", "@DivisionID"), new FieldValue("CompanyID", "@CompanyID"), new FieldValue("VendorID", "@VendorID"), new FieldValue("PurchaseFlow", "@PurchaseFlow"), new FieldValue("DueDate", "@DueDate"), new FieldValue("IsImport", "@IsImport"), new FieldValue("TransactionDate", "@TransactionDate"), new FieldValue("BuyerID", "@BuyerID"), new FieldValue("ShippingMethodID", "@ShippingMethodID"), new FieldValue("PriceIncludeTax", "@PriceIncludeTax"), new FieldValue("Status", "@Status"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("CurrencyRate", "@CurrencyRate"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxAmountFC", "@TaxAmountFC"), new FieldValue("Discount", "@Discount"), new FieldValue("DiscountFC", "@DiscountFC"), new FieldValue("Total", "@Total"), new FieldValue("TotalFC", "@TotalFC"), new FieldValue("PONumber", "@PONumber"), new FieldValue("BOLNo", "@BOLNo"), new FieldValue("SourceDocType", "@ItemSourceTypes"), new FieldValue("TermID", "@TermID"), new FieldValue("Reference", "@Reference"), new FieldValue("Reference2", "@Reference2"), new FieldValue("RegisterID", "@RegisterID"), new FieldValue("IsCash", "@IsCash"), new FieldValue("ContainerNumber", "@ContainerNumber"), new FieldValue("Port", "@Port"), new FieldValue("BOLNumber", "@BOLNumber"), new FieldValue("Shipper", "@Shipper"), new FieldValue("ClearingAgent", "@ClearingAgent"), new FieldValue("Note", "@Note"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("PayeeTaxGroupID", "@PayeeTaxGroupID"), new FieldValue("JobID", "@JobID"), new FieldValue("CostCategoryID", "@CostCategoryID"), new FieldValue("SupplierInvoiceNumber", "@SupplierInvoiceNumber"));
			if (isUpdate)
			{
				sqlBuilder.AddInsertUpdateParameters("Purchase_Invoice_NonInv", new FieldValue("DateUpdated", "@DateUpdated", isUpdateConditionField: true, checkForNullValue: true));
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
			parameters.Add("@SysDocID", SqlDbType.NVarChar);
			parameters.Add("@VoucherID", SqlDbType.NVarChar);
			parameters.Add("@DivisionID", SqlDbType.NVarChar);
			parameters.Add("@CompanyID", SqlDbType.TinyInt);
			parameters.Add("@VendorID", SqlDbType.NVarChar);
			parameters.Add("@IsImport", SqlDbType.Bit);
			parameters.Add("@PurchaseFlow", SqlDbType.TinyInt);
			parameters.Add("@TransactionDate", SqlDbType.DateTime);
			parameters.Add("@BuyerID", SqlDbType.NVarChar);
			parameters.Add("@ShippingMethodID", SqlDbType.NVarChar);
			parameters.Add("@Status", SqlDbType.TinyInt);
			parameters.Add("@CurrencyID", SqlDbType.NVarChar);
			parameters.Add("@CurrencyRate", SqlDbType.Decimal);
			parameters.Add("@PriceIncludeTax", SqlDbType.Bit);
			parameters.Add("@TermID", SqlDbType.NVarChar);
			parameters.Add("@ItemSourceTypes", SqlDbType.NVarChar);
			parameters.Add("@Reference", SqlDbType.NVarChar);
			parameters.Add("@Reference2", SqlDbType.NVarChar);
			parameters.Add("@PONumber", SqlDbType.NVarChar);
			parameters.Add("@BOLNo", SqlDbType.NVarChar);
			parameters.Add("@SupplierInvoiceNumber", SqlDbType.NVarChar);
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
			parameters.Add("@JobID", SqlDbType.NVarChar);
			parameters.Add("@CostCategoryID", SqlDbType.NVarChar);
			parameters.Add("@PayeeTaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@DivisionID"].SourceColumn = "DivisionID";
			parameters["@CompanyID"].SourceColumn = "CompanyID";
			parameters["@VendorID"].SourceColumn = "VendorID";
			parameters["@PurchaseFlow"].SourceColumn = "PurchaseFlow";
			parameters["@IsImport"].SourceColumn = "IsImport";
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
			parameters["@ItemSourceTypes"].SourceColumn = "SourceDocType";
			parameters["@PONumber"].SourceColumn = "PONumber";
			parameters["@BOLNo"].SourceColumn = "BOLNo";
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
			parameters["@SupplierInvoiceNumber"].SourceColumn = "SupplierInvoiceNumber";
			parameters["@JobID"].SourceColumn = "JobID";
			parameters["@CostCategoryID"].SourceColumn = "CostCategoryID";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@PayeeTaxGroupID"].SourceColumn = "PayeeTaxGroupID";
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
			sqlBuilder.AddInsertUpdateParameters("Purchase_Invoice_NonInv_Detail", new FieldValue("SysDocID", "@SysDocID"), new FieldValue("VoucherID", "@VoucherID"), new FieldValue("ProductID", "@ProductID"), new FieldValue("RowIndex", "@RowIndex"), new FieldValue("Quantity", "@Quantity"), new FieldValue("LCost", "@LCost"), new FieldValue("LCostAmount", "@LCostAmount"), new FieldValue("UnitPrice", "@UnitPrice"), new FieldValue("UnitPriceFC", "@UnitPriceFC"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("Description", "@Description"), new FieldValue("UnitID", "@UnitID"), new FieldValue("JobID", "@JobID"), new FieldValue("LocationID", "@LocationID"), new FieldValue("UnitQuantity", "@UnitQuantity"), new FieldValue("UnitFactor", "@UnitFactor"), new FieldValue("FactorType", "@FactorType"), new FieldValue("OrderSysDocID", "@OrderSysDocID"), new FieldValue("OrderVoucherID", "@OrderVoucherID"), new FieldValue("PORSysDocID", "@PORSysDocID"), new FieldValue("PORVoucherID", "@PORVoucherID"), new FieldValue("OrderRowIndex", "@OrderRowIndex"), new FieldValue("SubunitPrice", "@SubunitPrice"), new FieldValue("LotNumber", "@LotNumber"), new FieldValue("Remarks", "@Remarks"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("RowSource", "@RowSource"), new FieldValue("IsPORRow", "@IsPORRow"), new FieldValue("AttributeID1", "@AttributeID1"), new FieldValue("AttributeID2", "@AttributeID2"), new FieldValue("AnalysisID", "@AnalysisID"));
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
			parameters.Add("@PORSysDocID", SqlDbType.NVarChar);
			parameters.Add("@PORVoucherID", SqlDbType.NVarChar);
			parameters.Add("@OrderRowIndex", SqlDbType.Int);
			parameters.Add("@IsPORRow", SqlDbType.Bit);
			parameters.Add("@LotNumber", SqlDbType.Int);
			parameters.Add("@RowSource", SqlDbType.Int);
			parameters.Add("@Remarks", SqlDbType.NVarChar);
			parameters.Add("@AnalysisID", SqlDbType.NVarChar);
			parameters.Add("@TaxAmount", SqlDbType.Decimal);
			parameters.Add("@TaxGroupID", SqlDbType.NVarChar);
			parameters.Add("@TaxOption", SqlDbType.TinyInt);
			parameters.Add("@AttributeID1", SqlDbType.VarChar);
			parameters.Add("@AttributeID2", SqlDbType.VarChar);
			parameters["@SysDocID"].SourceColumn = "SysDocID";
			parameters["@VoucherID"].SourceColumn = "VoucherID";
			parameters["@ProductID"].SourceColumn = "ProductID";
			parameters["@RowIndex"].SourceColumn = "RowIndex";
			parameters["@Quantity"].SourceColumn = "Quantity";
			parameters["@UnitPrice"].SourceColumn = "UnitPrice";
			parameters["@UnitPriceFC"].SourceColumn = "UnitPriceFC";
			parameters["@LCost"].SourceColumn = "LCost";
			parameters["@LCostAmount"].SourceColumn = "LCostAmount";
			parameters["@Amount"].SourceColumn = "Amount";
			parameters["@AmountFC"].SourceColumn = "AmountFC";
			parameters["@Description"].SourceColumn = "Description";
			parameters["@UnitID"].SourceColumn = "UnitID";
			parameters["@JobID"].SourceColumn = "JobID";
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
			parameters["@RowSource"].SourceColumn = "RowSource";
			parameters["@Remarks"].SourceColumn = "Remarks";
			parameters["@AnalysisID"].SourceColumn = "AnalysisID";
			parameters["@TaxGroupID"].SourceColumn = "TaxGroupID";
			parameters["@TaxAmount"].SourceColumn = "TaxAmount";
			parameters["@TaxOption"].SourceColumn = "TaxOption";
			parameters["@AttributeID1"].SourceColumn = "AttributeID1";
			parameters["@AttributeID2"].SourceColumn = "AttributeID2";
			if (isUpdate)
			{
				return updateCommand;
			}
			return insertCommand;
		}

		private string GetInsertUpdateExpenseText(bool isUpdate)
		{
			SqlBuilder sqlBuilder = new SqlBuilder();
			sqlBuilder.AddInsertUpdateParameters("Purchase_Invoice_NonInv_Expense", new FieldValue("InvoiceSysDocID", "@InvoiceSysDocID"), new FieldValue("InvoiceVoucherID", "@InvoiceVoucherID"), new FieldValue("ExpenseID", "@ExpenseID"), new FieldValue("Description", "@Description"), new FieldValue("Amount", "@Amount"), new FieldValue("AmountFC", "@AmountFC"), new FieldValue("CurrencyID", "@CurrencyID"), new FieldValue("RateType", "@RateType"), new FieldValue("TaxOption", "@TaxOption"), new FieldValue("TaxGroupID", "@TaxGroupID"), new FieldValue("TaxAmount", "@TaxAmount"), new FieldValue("Reference", "@Reference"), new FieldValue("CurrencyRate", "@CurrencyRate"));
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

		private bool ValidateData(PurchaseInvoiceNIData purchaseInvoiceData, bool isUpdate, SqlTransaction sqlTransaction)
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
					text = "select COUNT(*) FROM Purchase_Invoice_NonInv_Detail \r\n                            WHERE EXISTS (SELECT *  FROM #TMP_GR TMP WHERE OrderSysDocID = TMP.SysDocID AND ordervoucherid = TMP.VoucherID) ";
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

		public bool InsertUpdatePurchaseInvoice(PurchaseInvoiceNIData purchaseInvoiceData, bool isUpdate)
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
				if (!isUpdate && new SystemDocuments(base.DBConfig).ExistDocumentNumber("Purchase_Invoice_NonInv", "VoucherID", dataRow["SysDocID"].ToString(), text5, sqlTransaction))
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
				string commaSeperatedIDs = GetCommaSeperatedIDs(purchaseInvoiceData, "Purchase_Invoice_NonInv_Detail", "ProductID");
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
				flag = (isUpdate ? (flag & Update(purchaseInvoiceData, "Purchase_Invoice_NonInv", insertUpdatePurchaseInvoiceCommand)) : (flag & Insert(purchaseInvoiceData, "Purchase_Invoice_NonInv", insertUpdatePurchaseInvoiceCommand)));
				if (!flag)
				{
					throw new CompanyException("Faild to save the transaction. Please reload the transaction and try again.");
				}
				if (purchaseInvoiceData.Tables["Purchase_Invoice_NonInv_Detail"].Rows.Count > 0)
				{
					insertUpdatePurchaseInvoiceCommand = GetInsertUpdatePurchaseInvoiceDetailsCommand(isUpdate: false);
					insertUpdatePurchaseInvoiceCommand.Transaction = sqlTransaction;
					flag &= Insert(purchaseInvoiceData, "Purchase_Invoice_NonInv_Detail", insertUpdatePurchaseInvoiceCommand);
				}
				if (purchaseInvoiceData.Tables["Purchase_Invoice_NonInv_Expense"].Rows.Count > 0)
				{
					insertUpdatePurchaseInvoiceCommand = GetInsertUpdateExpenseCommand(isUpdate: false);
					insertUpdatePurchaseInvoiceCommand.Transaction = sqlTransaction;
					flag &= Insert(purchaseInvoiceData, "Purchase_Invoice_NonInv_Expense", insertUpdatePurchaseInvoiceCommand);
				}
				InventoryTransactionData inventoryTransactionData = new InventoryTransactionData();
				foreach (DataRow row4 in purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows)
				{
					ItemSourceTypes itemSourceTypes2 = ItemSourceTypes.None;
					if (row4["RowSource"] != DBNull.Value)
					{
						itemSourceTypes2 = (ItemSourceTypes)byte.Parse(row4["RowSource"].ToString());
					}
					if (itemSourceTypes2 != ItemSourceTypes.GRN)
					{
						DataRow dataRow5 = inventoryTransactionData.InventoryTransactionTable.NewRow();
						dataRow5.BeginEdit();
						dataRow5["SysDocID"] = row4["SysDocID"];
						dataRow5["VoucherID"] = row4["VoucherID"];
						if (row4["LocationID"].ToString() == "")
						{
							throw new Exception("Location cannot be empty.");
						}
						dataRow5["LocationID"] = row4["LocationID"];
						dataRow5["ProductID"] = row4["ProductID"];
						dataRow5["Quantity"] = decimal.Parse(row4["Quantity"].ToString());
						dataRow5["Reference"] = dataRow["Reference"];
						if (result)
						{
							dataRow5["SysDocType"] = (byte)34;
						}
						else if (result2)
						{
							dataRow5["SysDocType"] = (byte)39;
						}
						else
						{
							dataRow5["SysDocType"] = (byte)116;
						}
						dataRow5["TransactionDate"] = dataRow["TransactionDate"];
						dataRow5["TransactionType"] = (byte)1;
						decimal result12 = default(decimal);
						decimal result13 = default(decimal);
						decimal.TryParse(row4["UnitPrice"].ToString(), out result12);
						decimal.TryParse(row4["LCost"].ToString(), out result13);
						dataRow5["UnitID"] = row4["UnitID"];
						dataRow5["UnitPrice"] = result12 + result13;
						dataRow5["Cost"] = result12;
						dataRow5["RowIndex"] = row4["RowIndex"];
						dataRow5["PayeeType"] = "V";
						dataRow5["PayeeID"] = dataRow["VendorID"];
						dataRow5["RowIndex"] = row4["RowIndex"];
						dataRow5["DivisionID"] = dataRow["DivisionID"];
						dataRow5["CompanyID"] = dataRow["CompanyID"];
						if (row4["UnitQuantity"] != DBNull.Value && row4["UnitFactor"] != DBNull.Value)
						{
							dataRow5["UnitQuantity"] = row4["UnitQuantity"];
							dataRow5["Factor"] = row4["UnitFactor"];
							dataRow5["FactorType"] = row4["FactorType"];
							decimal.Parse(row4["UnitFactor"].ToString());
							row4["FactorType"].ToString();
							decimal d2 = decimal.Parse(row4["UnitQuantity"].ToString());
							decimal num4 = decimal.Parse(row4["Quantity"].ToString());
							decimal d3 = decimal.Parse(row4["UnitPrice"].ToString());
							decimal num5 = default(decimal);
							num5 = ((!(num4 != 0m)) ? default(decimal) : (d2 * d3 / num4));
							dataRow5["UnitPrice"] = num5;
						}
						dataRow5.EndEdit();
						inventoryTransactionData.InventoryTransactionTable.Rows.Add(dataRow5);
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
					flag &= new InventoryTransaction(base.DBConfig).UpdateGRNCosting(text6, text5, sqlTransaction);
				}
				foreach (DataRow row5 in purchaseInvoiceData.InvoiceGRNTable.Rows)
				{
					row5["InvoiceSysDocID"] = dataRow["SysDocID"];
					row5["InvoiceNumber"] = dataRow["VoucherID"];
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
					foreach (DataRow row6 in purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows)
					{
						text4 = row6["ProductID"].ToString();
						text2 = row6["OrderVoucherID"].ToString();
						text3 = row6["OrderSysDocID"].ToString();
						int result16 = 0;
						if (!(text2 == "") && !(text3 == ""))
						{
							int.TryParse(row6["OrderRowIndex"].ToString(), out result16);
							float result17 = 0f;
							if (row6["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row6["UnitQuantity"].ToString(), out result17);
							}
							else
							{
								float.TryParse(row6["Quantity"].ToString(), out result17);
							}
							float num9 = new Products(base.DBConfig).GetOrderedQuantity(text4, sqlTransaction) - result17;
							if (num9 < 0f)
							{
								num9 = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateOrderedQuantity(text4, num9, sqlTransaction);
							flag &= new PurchaseOrderNI(base.DBConfig).UpdateRowReceivedQuantity(text3, text2, result16, result17, sqlTransaction);
						}
					}
					text2 = purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows[0]["OrderVoucherID"].ToString();
					text3 = purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows[0]["OrderSysDocID"].ToString();
					if (text2 != "")
					{
						flag &= new PurchaseOrderNI(base.DBConfig).CloseReceivedOrder(text3, text2, sqlTransaction);
					}
					break;
				case ItemSourceTypes.PackingList:
					foreach (DataRow row7 in purchaseInvoiceData.PurchaseInvoiceDetailTable.Rows)
					{
						text4 = row7["ProductID"].ToString();
						string text10 = row7["SourceVoucherID"].ToString();
						string text11 = row7["SourceSysDocID"].ToString();
						int num6 = int.Parse(row7["SourceRowIndex"].ToString());
						int num7 = 0;
						text3 = new Databases(base.DBConfig).GetFieldValue("PO_Shipment_Detail", "SourceSysDocID", "SysDocID", text11, "VoucherID", text10, "RowIndex", num6, sqlTransaction).ToString();
						text2 = new Databases(base.DBConfig).GetFieldValue("PO_Shipment_Detail", "SourceVoucherID", "SysDocID", text11, "VoucherID", text10, "RowIndex", num6, sqlTransaction).ToString();
						if (!(text2 == "") && !(text3 == ""))
						{
							num7 = int.Parse(new Databases(base.DBConfig).GetFieldValue("PO_Shipment_Detail", "SourceRowIndex", "SysDocID", text11, "VoucherID", text10, "RowIndex", num6, sqlTransaction).ToString());
							float result15 = 0f;
							if (row7["UnitQuantity"] != DBNull.Value)
							{
								float.TryParse(row7["UnitQuantity"].ToString(), out result15);
							}
							else
							{
								float.TryParse(row7["Quantity"].ToString(), out result15);
							}
							float num8 = new Products(base.DBConfig).GetOrderedQuantity(text4, sqlTransaction) - result15;
							if (num8 < 0f)
							{
								num8 = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateOrderedQuantity(text4, num8, sqlTransaction);
							flag &= new PurchaseOrderNI(base.DBConfig).UpdateRowReceivedQuantity(text3, text2, num7, result15, sqlTransaction);
							flag &= new PurchaseOrderNI(base.DBConfig).UpdateRowShippedQuantity(text3, text2, num7, text4, -1f * result15, validateQuantity: false, sqlTransaction);
							flag &= new POShipment(base.DBConfig).UpdateRowReceivedQuantity(text11, text10, num6, result15, sqlTransaction);
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
						foreach (DataRow row8 in purchaseInvoiceData.Tables["GRN"].Rows)
						{
							bool result14 = false;
							object fieldValue2 = new Databases(base.DBConfig).GetFieldValue("Purchase_Receipt", "IsInvoiced", "SysDocID", row8["SysDocID"].ToString(), "VoucherID", row8["VoucherID"].ToString(), sqlTransaction);
							if (fieldValue2 != null)
							{
								bool.TryParse(fieldValue2.ToString(), out result14);
							}
							if (result14)
							{
								throw new CompanyException("One or more goods received notes are previously invoice.");
							}
							flag &= (ExecuteNonQuery("UPDATE Purchase_Receipt SET IsInvoiced='True',InvoiceSysDocID='" + text6 + "',InvoiceVoucherID='" + text5 + "' WHERE SysDocID='" + row8["SysDocID"].ToString() + "' AND VoucherID='" + row8["VoucherID"].ToString() + "'", sqlTransaction) >= 0);
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
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Purchase_Invoice_NonInv", "SysDocID", dataRow["SysDocID"].ToString(), "VoucherID", dataRow["VoucherID"].ToString(), sqlTransaction, !isUpdate);
				string entityName = "Purchase Invoice-Non Inv";
				flag = (isUpdate ? (flag & AddActivityLog(entityName, text5, text6, ActivityTypes.Update, sqlTransaction)) : (flag & AddActivityLog(entityName, text5, text6, ActivityTypes.Add, sqlTransaction)));
				if (!isUpdate)
				{
					flag &= new SystemDocuments(base.DBConfig).UpdateNextDocumentNumber(dataRow["SysDocID"].ToString(), dataRow["VoucherID"].ToString(), "Purchase_Invoice_NonInv", "VoucherID", sqlTransaction);
				}
				if (!flag)
				{
					return flag;
				}
				if (!result2)
				{
					flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.PurchaseInvoiceNI, text6, text5, "Purchase_Invoice_NonInv", sqlTransaction);
					return flag;
				}
				flag &= new Approval(base.DBConfig).CreateTransactionApprovalTasks(SysDocTypes.ImportPurchaseInvoice, text6, text5, "Purchase_Invoice_NonInv", sqlTransaction);
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

		private GLData CreateInvoiceGLData(PurchaseInvoiceNIData transactionData, SqlTransaction sqlTransaction)
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
				string text3 = dataRow["JobID"].ToString();
				bool result = false;
				if (!dataRow["PriceIncludeTax"].IsDBNullOrEmpty())
				{
					bool.TryParse(dataRow["PriceIncludeTax"].ToString(), out result);
				}
				string textCommand = "SELECT SD.LocationID,ISNULL(VEN.APAccountID,ISNULL(CLS.APAccountID, LOC.APAccountID)) AS APAccountID ,LOC.COGSAccountID,LOC.DiscountReceivedAccountID,\r\n                                LOC.InventoryAccountID,LOC.SalesAccountID,LOC.SalesTaxAccountID,Cur.CurrencyID AS BaseCurrencyID\r\n                                FROM System_Document SD INNER JOIN Location LOC ON SD.LocationID = LOC.LocationID\r\n                                LEFT OUTER JOIN Vendor VEN ON VendorID='" + text + "'\r\n                                 LEFT OUTER JOIN Vendor_Class CLS ON VEN.VendorClassID = CLS.ClassID\r\n                                LEFT OUTER JOIN Currency CUR ON CUR.IsBase = 'True'\r\n                                WHERE SysDocID = '" + text2 + "'";
				DataSet dataSet = new DataSet();
				FillDataSet(dataSet, "Accounts", textCommand, sqlTransaction);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
				{
					throw new CompanyException("There is no location assigned to this system document or location record is missing.");
				}
				DataRow dataRow2 = dataSet.Tables["Accounts"].Rows[0];
				string docLocationID = dataRow2["LocationID"].ToString();
				string text4 = dataRow2["DiscountReceivedAccountID"].ToString();
				dataRow2["SalesTaxAccountID"].ToString();
				string text5 = dataRow2["APAccountID"].ToString();
				string text6 = dataRow2["BaseCurrencyID"].ToString();
				bool flag = false;
				decimal result2 = 1m;
				if (dataRow["CurrencyID"] != DBNull.Value && text6 != dataRow["CurrencyID"].ToString())
				{
					flag = true;
					decimal.TryParse(dataRow["CurrencyRate"].ToString(), out result2);
				}
				bool result3 = false;
				bool.TryParse(dataRow["IsCash"].ToString(), out result3);
				bool result4 = false;
				bool.TryParse(dataRow["IsImport"].ToString(), out result4);
				if (!result3 && text5 == "")
				{
					throw new CompanyException("Account payable is not selected for this vendor. Please select an account payable for the location or this vendor.", 5000);
				}
				DataRow dataRow3 = gLData.JournalTable.NewRow();
				SysDocTypes sysDocTypes = SysDocTypes.PurchaseInvoiceNI;
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
				Hashtable hashtable5 = new Hashtable();
				Hashtable hashtable6 = new Hashtable();
				Hashtable hashtable7 = new Hashtable();
				string text7 = "";
				string text8 = "";
				string text9 = "";
				string text10 = "";
				string value3 = "";
				string value4 = "";
				decimal d = default(decimal);
				decimal d2 = default(decimal);
				decimal num2 = default(decimal);
				DataTable dataTable = new DataTable();
				DataColumnCollection columns = dataTable.Columns;
				DataColumn dataColumn = columns.Add("AccountID", typeof(string));
				DataColumn dataColumn2 = columns.Add("AttributeID1", typeof(string));
				DataColumn dataColumn3 = columns.Add("AttributeID2", typeof(string));
				DataColumn dataColumn4 = columns.Add("AnalysisID", typeof(string));
				DataColumn dataColumn5 = columns.Add("JobID", typeof(string));
				columns.Add("Amount", typeof(decimal));
				dataTable.PrimaryKey = new DataColumn[5]
				{
					dataColumn,
					dataColumn2,
					dataColumn3,
					dataColumn4,
					dataColumn5
				};
				foreach (DataRow row in transactionData.PurchaseInvoiceDetailTable.Rows)
				{
					decimal num3 = default(decimal);
					string text11 = row["ProductID"].ToString();
					string warehouseLocationID = row["LocationID"].ToString();
					int.Parse(row["RowIndex"].ToString());
					dataSet = new Products(base.DBConfig).GetProductTransactionAccounts(text11, docLocationID, warehouseLocationID, text2, sqlTransaction);
					if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables[0].Rows.Count == 0)
					{
						throw new CompanyException("Product accounts information not found for product or location.");
					}
					DataRow dataRow5 = dataSet.Tables[0].Rows[0];
					string text12 = dataRow5["InventoryAssetAccountID"].ToString();
					string text13 = dataRow5["COGSAccountID"].ToString();
					text8 = "";
					decimal result5 = default(decimal);
					decimal result6 = default(decimal);
					decimal result7 = default(decimal);
					decimal.TryParse(row["TaxAmount"].ToString(), out result7);
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
					object fieldValue = new Databases(base.DBConfig).GetFieldValue("Product", "ItemType", "ProductID", text11, sqlTransaction);
					if (fieldValue == null || !(fieldValue.ToString() != ""))
					{
						throw new CompanyException("Item type is not selected for the product:" + text11);
					}
					itemTypes = (ItemTypes)byte.Parse(fieldValue.ToString());
					num3 = ((row["UnitQuantity"] == DBNull.Value) ? decimal.Parse(row["Quantity"].ToString()) : decimal.Parse(row["UnitQuantity"].ToString()));
					decimal result8 = default(decimal);
					if (flag)
					{
						decimal.TryParse(row["UnitPriceFC"].ToString(), out result8);
					}
					else
					{
						decimal.TryParse(row["UnitPrice"].ToString(), out result8);
					}
					decimal num4 = default(decimal);
					num4 = ((!(num3 >= 0m)) ? Math.Round(result5, currencyDecimalPoints) : Math.Round(result5, currencyDecimalPoints));
					string value5 = "";
					if (string.IsNullOrEmpty(row["AnalysisID"].ToString()))
					{
						value5 = ((!string.IsNullOrEmpty(value5)) ? "AND AnalysisID='' " : "AnalysisID='' ");
					}
					else
					{
						text8 = row["AnalysisID"].ToString();
						value5 = ((!string.IsNullOrEmpty(value5)) ? ("AND AnalysisID='" + text8 + "'") : ("AnalysisID='" + text8 + "'"));
					}
					if (!string.IsNullOrEmpty(row["AttributeID1"].ToString()))
					{
						text9 = row["AttributeID1"].ToString();
						value5 = ((!string.IsNullOrEmpty(value5)) ? (value5 + " AND  AttributeID1 = '" + text9 + "'") : (value5 + "   AttributeID1 = '" + text9 + "'"));
					}
					if (!string.IsNullOrEmpty(row["AttributeID2"].ToString()))
					{
						text10 = row["AttributeID2"].ToString();
						value5 = ((!string.IsNullOrEmpty(value5)) ? (value5 + " AND AttributeID2 = '" + text10 + "'") : (value5 + " AttributeID2 = '" + text10 + "'"));
					}
					if (!string.IsNullOrEmpty(row["JobID"].ToString()))
					{
						text3 = row["JobID"].ToString();
						value5 = ((!string.IsNullOrEmpty(value5)) ? (value5 + " AND JobID = '" + text3 + "'") : (value5 + " JobID = '" + text3 + "'"));
					}
					if (string.IsNullOrEmpty(value5))
					{
						value5 = null;
					}
					switch (itemTypes)
					{
					case ItemTypes.Inventory:
						text7 = text12;
						if (hashtable2.ContainsKey(text7))
						{
							num = decimal.Parse(hashtable2[text7].ToString());
							num += Math.Round(num4, currencyDecimalPoints);
							hashtable2[text7] = num;
							num = decimal.Parse(hashtable3[text7].ToString());
							num += Math.Round(result6, currencyDecimalPoints);
							hashtable3[text7] = num;
						}
						else
						{
							hashtable2.Add(text7, Math.Round(num4, currencyDecimalPoints));
							arrayList2.Add(text7);
							hashtable3.Add(text7, Math.Round(result6, currencyDecimalPoints));
							arrayList3.Add(text7);
						}
						d += Math.Round(result5, currencyDecimalPoints);
						d2 += Math.Round(result6, currencyDecimalPoints);
						break;
					case ItemTypes.NonInventory:
					case ItemTypes.Service:
					case ItemTypes.Discount:
						num = Math.Round(result5, currencyDecimalPoints);
						text7 = text13;
						if (hashtable.ContainsKey(text7))
						{
							num = decimal.Parse(hashtable[text7].ToString());
							num += Math.Round(num4, currencyDecimalPoints);
							hashtable[text7] = num;
						}
						else
						{
							hashtable.Add(text7, Math.Round(num4, currencyDecimalPoints));
							arrayList.Add(text7);
							hashtable3.Add(text7, Math.Round(result6, currencyDecimalPoints));
							arrayList3.Add(text7);
							if (text8 != "")
							{
								hashtable5.Add(text7, text8);
							}
							if (text9 != "")
							{
								hashtable6.Add(text7, text9);
							}
							if (text10 != "")
							{
								hashtable7.Add(text7, text10);
							}
						}
						if ((itemTypes == ItemTypes.Service || itemTypes == ItemTypes.NonInventory) && !hashtable4.ContainsKey(text7))
						{
							value4 = transactionData.PurchaseInvoiceDetailTable.Rows[0]["JobID"].ToString();
							text3 = row["JobID"].ToString();
							hashtable4.Add(text7, text3);
							arrayList4.Add(text3);
						}
						num2 += Math.Round(result5, currencyDecimalPoints);
						d2 += Math.Round(result6, currencyDecimalPoints);
						break;
					}
					transactionData.PurchaseInvoiceDetailTable.Select(value5);
					object obj = "";
					obj = ((!flag) ? transactionData.PurchaseInvoiceDetailTable.Compute("Sum(Amount)", value5) : transactionData.PurchaseInvoiceDetailTable.Compute("Sum(AmountFC)", value5));
					DataRow dataRow6 = dataTable.NewRow();
					dataRow6["AccountID"] = text7;
					dataRow6["AttributeID1"] = text9;
					dataRow6["AttributeID2"] = text10;
					dataRow6["JobID"] = text3;
					dataRow6["AnalysisID"] = text8;
					dataRow6["Amount"] = obj;
					if (dataTable.Rows.Count == 0)
					{
						dataTable.Rows.Add(dataRow6);
					}
					else
					{
						if (dataTable.Select(value5).Length == 0)
						{
							dataTable.Rows.Add(dataRow6);
						}
						dataTable = dataTable.DefaultView.ToTable(true, "AttributeID1", "AttributeID2", "AccountID", "AnalysisID", "JobID", "Amount");
					}
				}
				if (d != 0m)
				{
					for (int i = 0; i < hashtable2.Count; i++)
					{
						DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
						dataRow7.BeginEdit();
						text7 = arrayList2[i].ToString();
						num = decimal.Parse(hashtable2[text7].ToString());
						dataRow7["JournalID"] = 0;
						dataRow7["AccountID"] = text7;
						dataRow7["PayeeID"] = text;
						if (flag)
						{
							dataRow7["DebitFC"] = num;
							dataRow7["CreditFC"] = DBNull.Value;
						}
						else
						{
							dataRow7["Debit"] = num;
							dataRow7["Credit"] = DBNull.Value;
						}
						dataRow7["Reference"] = dataRow["Reference"];
						dataRow7["JobID"] = text3;
						dataRow7["CompanyID"] = value;
						dataRow7["DivisionID"] = value2;
						dataRow7.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow7);
					}
				}
				if (num2 != 0m)
				{
					if (dataTable.Rows.Count > 0)
					{
						for (int j = 0; j < dataTable.Rows.Count; j++)
						{
							DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
							dataRow7.BeginEdit();
							text7 = dataTable.Rows[j]["AccountID"].ToString();
							num = decimal.Parse(dataTable.Rows[j]["Amount"].ToString());
							text8 = "";
							text9 = "";
							text10 = "";
							text8 = dataTable.Rows[j]["AnalysisID"].ToString();
							text9 = dataTable.Rows[j]["AttributeID1"].ToString();
							text10 = dataTable.Rows[j]["AttributeID2"].ToString();
							value3 = dataTable.Rows[j]["JobID"].ToString();
							dataRow7["JournalID"] = 0;
							dataRow7["AccountID"] = text7;
							dataRow7["PayeeID"] = text;
							dataRow7["AnalysisID"] = text8;
							dataRow7["AttributeID1"] = text9;
							dataRow7["AttributeID2"] = text10;
							if (flag)
							{
								if (num < 0m)
								{
									dataRow7["DebitFC"] = DBNull.Value;
									dataRow7["CreditFC"] = Math.Abs(num);
								}
								else
								{
									dataRow7["DebitFC"] = num;
									dataRow7["CreditFC"] = DBNull.Value;
								}
							}
							else if (num < 0m)
							{
								dataRow7["Debit"] = DBNull.Value;
								dataRow7["Credit"] = Math.Abs(num);
							}
							else
							{
								dataRow7["Debit"] = num;
								dataRow7["Credit"] = DBNull.Value;
							}
							dataRow7["Reference"] = dataRow["Reference"];
							value3 = (string)(dataRow7["JobID"] = hashtable4[text7].ToString());
							dataRow7["CompanyID"] = value;
							dataRow7["DivisionID"] = value2;
							dataRow7.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow7);
						}
					}
					else
					{
						for (int k = 0; k < hashtable.Count; k++)
						{
							DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
							dataRow7.BeginEdit();
							text7 = arrayList[k].ToString();
							num = decimal.Parse(hashtable[text7].ToString());
							text8 = "";
							text9 = "";
							text10 = "";
							if (hashtable5.ContainsKey(text7))
							{
								text8 = hashtable5[text7].ToString();
							}
							if (hashtable6.ContainsKey(text7))
							{
								text9 = hashtable6[text7].ToString();
							}
							if (hashtable7.ContainsKey(text7))
							{
								text10 = hashtable7[text7].ToString();
							}
							dataRow7["JournalID"] = 0;
							dataRow7["AccountID"] = text7;
							dataRow7["PayeeID"] = text;
							dataRow7["AnalysisID"] = text8;
							dataRow7["AttributeID1"] = text9;
							dataRow7["AttributeID2"] = text10;
							if (flag)
							{
								if (num < 0m)
								{
									dataRow7["DebitFC"] = DBNull.Value;
									dataRow7["CreditFC"] = Math.Abs(num);
								}
								else
								{
									dataRow7["DebitFC"] = num;
									dataRow7["CreditFC"] = DBNull.Value;
								}
							}
							else if (num < 0m)
							{
								dataRow7["Debit"] = DBNull.Value;
								dataRow7["Credit"] = Math.Abs(num);
							}
							else
							{
								dataRow7["Debit"] = num;
								dataRow7["Credit"] = DBNull.Value;
							}
							dataRow7["Reference"] = dataRow["Reference"];
							value3 = (string)(dataRow7["JobID"] = hashtable4[text7].ToString());
							dataRow7["CompanyID"] = value;
							dataRow7["DivisionID"] = value2;
							dataRow7.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow7);
						}
					}
				}
				if (d2 != 0m)
				{
					for (int l = 0; l < hashtable3.Count; l++)
					{
						DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
						dataRow7.BeginEdit();
						text7 = arrayList3[l].ToString();
						num = decimal.Parse(hashtable3[text7].ToString());
						dataRow7["JournalID"] = 0;
						dataRow7["AccountID"] = text7;
						dataRow7["PayeeID"] = DBNull.Value;
						dataRow7["IsBaseOnly"] = true;
						dataRow7["Debit"] = num;
						dataRow7["Credit"] = DBNull.Value;
						dataRow7["Reference"] = dataRow["Reference"];
						dataRow7["JobID"] = text3;
						dataRow7["CompanyID"] = value;
						dataRow7["DivisionID"] = value2;
						dataRow7.EndEdit();
						gLData.JournalDetailsTable.Rows.Add(dataRow7);
					}
				}
				decimal num5 = default(decimal);
				foreach (DataRow row2 in transactionData.InvoiceExpenseTable.Rows)
				{
					string text16 = row2["ExpenseID"].ToString();
					num = decimal.Parse(row2["Amount"].ToString());
					decimal result9 = default(decimal);
					if (row2["AmountFC"] != DBNull.Value)
					{
						decimal.TryParse(row2["AmountFC"].ToString(), out result9);
					}
					string expenseAccountID = new ExpenseCode(base.DBConfig).GetExpenseAccountID(text16, sqlTransaction);
					DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					dataRow7["AccountID"] = expenseAccountID;
					string a = (string)(dataRow7["CurrencyID"] = row2["CurrencyID"].ToString());
					dataRow7["CurrencyRate"] = row2["CurrencyRate"];
					if (a != text6)
					{
						dataRow7["DebitFC"] = DBNull.Value;
						dataRow7["CreditFC"] = result9;
						dataRow7["Debit"] = DBNull.Value;
						dataRow7["Credit"] = num;
					}
					else
					{
						dataRow7["DebitFC"] = DBNull.Value;
						dataRow7["CreditFC"] = result9;
						dataRow7["Debit"] = DBNull.Value;
						dataRow7["Credit"] = num;
					}
					dataRow7["JobID"] = text3;
					dataRow7["Reference"] = text16;
					dataRow7["CompanyID"] = value;
					dataRow7["DivisionID"] = value2;
					dataRow7.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow7);
					num5 += num;
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
					DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					if (text4 == "")
					{
						throw new CompanyException("You have entered a discount amount for this transaction but there is no discount account selected for this location.\nPlease select the 'Discount Received' account for this location.", 1040);
					}
					dataRow7["AccountID"] = text4;
					dataRow7["PayeeID"] = text;
					dataRow7["PayeeType"] = "A";
					if (flag)
					{
						dataRow7["DebitFC"] = DBNull.Value;
						dataRow7["CreditFC"] = result10;
					}
					else
					{
						dataRow7["Debit"] = DBNull.Value;
						dataRow7["Credit"] = result10;
					}
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7["JobID"] = value3;
					dataRow7["CompanyID"] = value;
					dataRow7["DivisionID"] = value2;
					dataRow7.EndEdit();
					gLData.JournalDetailsTable.Rows.Add(dataRow7);
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
						decimal num6 = default(decimal);
						for (int m = 0; m < array.Length; m++)
						{
							num6 = default(decimal);
							DataRow obj2 = array[m];
							DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
							dataRow7.BeginEdit();
							dataRow7["JournalID"] = 0;
							string text18 = "";
							text18 = obj2["TaxItemID"].ToString();
							string text19 = "";
							textCommand = "SELECT PurchaseTaxAccountID FROM Tax WHERE  TaxCode = '" + text18.Trim() + "'";
							object obj3 = ExecuteScalar(textCommand);
							if (obj3 != null)
							{
								text19 = obj3.ToString();
							}
							if (text19 == "")
							{
								throw new CompanyException("AccountID is not set for tax item: " + text18 + ".");
							}
							decimal.TryParse(obj2["TaxAmount"].ToString(), out num6);
							dataRow7["AccountID"] = text19;
							dataRow7["PayeeID"] = text;
							dataRow7["PayeeType"] = "A";
							if (flag)
							{
								dataRow7["DebitFC"] = Math.Round(num6, currencyDecimalPoints, MidpointRounding.AwayFromZero);
								dataRow7["CreditFC"] = DBNull.Value;
							}
							else
							{
								dataRow7["Debit"] = Math.Round(num6, currencyDecimalPoints, MidpointRounding.AwayFromZero);
								dataRow7["Credit"] = DBNull.Value;
							}
							dataRow7["Reference"] = dataRow["Reference"];
							dataRow7["CompanyID"] = value;
							dataRow7["DivisionID"] = value2;
							dataRow7.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow7);
						}
					}
					if (payeeTaxOptions == PayeeTaxOptions.ReverseCharge)
					{
						DataRow[] array2 = transactionData.Tables["Tax_Detail"].Select("RowIndex = -1");
						decimal num7 = default(decimal);
						for (int n = 0; n < array2.Length; n++)
						{
							num7 = default(decimal);
							DataRow obj4 = array2[n];
							DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
							dataRow7.BeginEdit();
							dataRow7["JournalID"] = 0;
							string text20 = "";
							text20 = obj4["TaxItemID"].ToString();
							string text21 = "";
							textCommand = "SELECT TaxReverseChargeAccountID FROM Tax WHERE  TaxCode = '" + text20.Trim() + "'";
							object obj5 = ExecuteScalar(textCommand);
							if (obj5 != null)
							{
								text21 = obj5.ToString();
							}
							if (text21 == "")
							{
								throw new CompanyException("Reverse tax account is not set for tax item: " + text20 + ".");
							}
							decimal.TryParse(obj4["TaxAmount"].ToString(), out num7);
							dataRow7["AccountID"] = text21;
							dataRow7["PayeeID"] = text;
							dataRow7["PayeeType"] = "A";
							if (flag)
							{
								dataRow7["DebitFC"] = DBNull.Value;
								dataRow7["CreditFC"] = Math.Round(num7, currencyDecimalPoints, MidpointRounding.AwayFromZero);
							}
							else
							{
								dataRow7["Debit"] = DBNull.Value;
								dataRow7["Credit"] = Math.Round(num7, currencyDecimalPoints, MidpointRounding.AwayFromZero);
							}
							dataRow7["Reference"] = "Tax Reverse Charge " + dataRow["Reference"];
							dataRow7["JobID"] = text3;
							dataRow7["JVEntryType"] = (byte)6;
							dataRow7["CompanyID"] = value;
							dataRow7["DivisionID"] = value2;
							dataRow7.EndEdit();
							gLData.JournalDetailsTable.Rows.Add(dataRow7);
						}
					}
				}
				if (result3)
				{
					DataRow dataRow7 = gLData.JournalDetailsTable.NewRow();
					dataRow7.BeginEdit();
					dataRow7["JournalID"] = 0;
					string registerID = dataRow["RegisterID"].ToString();
					text7 = (string)(dataRow7["AccountID"] = new Register(base.DBConfig).GetRegisterAccountID(registerID, "CashAccountID"));
					dataRow7["PayeeID"] = text;
					dataRow7["PayeeType"] = "A";
					dataRow7["IsARAP"] = false;
					decimal num8 = d + num2 - result10 + result11;
					if ((payeeTaxOptions == PayeeTaxOptions.ReverseCharge) | result)
					{
						num8 = d + num2 - result10;
					}
					if (flag)
					{
						dataRow7["DebitFC"] = DBNull.Value;
						dataRow7["CreditFC"] = num8;
					}
					else
					{
						dataRow7["Debit"] = DBNull.Value;
						dataRow7["Credit"] = num8;
					}
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7["JobID"] = value4;
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
					dataRow7["AccountID"] = text5;
					dataRow7["PayeeID"] = text;
					dataRow7["PayeeType"] = "V";
					dataRow7["IsARAP"] = true;
					decimal num9 = d + num2 - result10 + result11;
					if ((payeeTaxOptions == PayeeTaxOptions.ReverseCharge) | result)
					{
						num9 = d + num2 - result10;
					}
					if (flag)
					{
						dataRow7["DebitFC"] = DBNull.Value;
						dataRow7["CreditFC"] = num9;
					}
					else
					{
						dataRow7["Debit"] = DBNull.Value;
						dataRow7["Credit"] = num9;
					}
					dataRow7["Reference"] = dataRow["Reference"];
					dataRow7["JobID"] = value4;
					dataRow7["Description"] = dataRow["Reference2"];
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

		public PurchaseInvoiceNIData GetPurchaseInvoiceByID(string sysDocID, string voucherID)
		{
			try
			{
				PurchaseInvoiceNIData purchaseInvoiceNIData = new PurchaseInvoiceNIData();
				string textCommand = "SELECT * FROM Purchase_Invoice_NonInv WHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceNIData, "Purchase_Invoice_NonInv", textCommand);
				if (purchaseInvoiceNIData == null || purchaseInvoiceNIData.Tables.Count == 0 || purchaseInvoiceNIData.Tables["Purchase_Invoice_NonInv"].Rows.Count == 0)
				{
					return null;
				}
				textCommand = "SELECT TD.*,Product.Description,Product.ItemType,Product.IsTrackLot,Product.IsTrackSerial,Product.Weight, Product.Attribute1,Product.Attribute2,Product.Attribute3,Product.MatrixParentID\r\n\t\t\t\t\t\tFROM Purchase_Invoice_NonInv_Detail TD INNER JOIN Product ON TD.ProductID=Product.ProductID\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceNIData, "Purchase_Invoice_NonInv_Detail", textCommand);
				textCommand = "SELECT * FROM Invoice_GRN\r\n\t\t\t\t\t\tWHERE InvoiceNumber='" + voucherID + "' AND InvoiceSysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceNIData, "Invoice_GRN", textCommand);
				textCommand = "SELECT * FROM Purchase_Invoice_NonInv_Expense\r\n\t\t\t\t\t\tWHERE InvoiceVoucherID='" + voucherID + "' AND InvoiceSysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceNIData, "Purchase_Invoice_NonInv_Expense", textCommand);
				textCommand = "SELECT * FROM Product_Lot_Receiving_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceNIData, "Product_Lot_Receiving_Detail", textCommand);
				textCommand = "SELECT Distinct OrderVoucherId as VoucherId,OrdersysDocId as SysDocId FROM Purchase_Order_NonInv_Detail POD LEFT JOIN Purchase_Invoice_NonInv_Detail PID ON POD.SysDocID=PID.OrderSysDocID AND POD.VoucherID=PID.OrderVoucherID\r\n\t\t\t\t\t\tWHERE PID.VoucherID='" + voucherID + "' AND PID.SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceNIData, "Purchase_Order_NonInv_Detail", textCommand);
				textCommand = "SELECT * FROM   Tax_Detail\r\n\t\t\t\t\t\tWHERE VoucherID='" + voucherID + "' AND SysDocID='" + sysDocID + "'";
				FillDataSet(purchaseInvoiceNIData, "Tax_Detail", textCommand);
				return purchaseInvoiceNIData;
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
				PurchaseInvoiceNIData purchaseInvoiceNIData = new PurchaseInvoiceNIData();
				string textCommand = "SELECT SOD.*,ISNULL(ISVOID,'False') AS IsVoid,ISNULL(IsCash,'False') AS IsCash, ISNULL(IsImport,'False') AS IsImport FROM Purchase_Invoice_NonInv_Detail SOD INNER JOIN Purchase_Invoice_NonInv SO ON SO.SysDocID=SOD.SysDocID AND SO.VOucherID=SOD.VoucherID\r\n\t\t\t\t\t\t\t  WHERE SOD.SysDocID = '" + sysDocID + "' AND SOD.VoucherID = '" + voucherID + "'";
				FillDataSet(purchaseInvoiceNIData, "Purchase_Invoice_NonInv_Detail", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(purchaseInvoiceNIData.PurchaseInvoiceDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(purchaseInvoiceNIData.PurchaseInvoiceDetailTable.Rows[0]["IsCash"].ToString(), out result2);
				bool result3 = false;
				bool.TryParse(purchaseInvoiceNIData.PurchaseInvoiceDetailTable.Rows[0]["IsImport"].ToString(), out result3);
				if (!result)
				{
					flag = (result2 ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(34, sysDocID, voucherID, isDeletingTransaction, sqlTransaction)) : ((!result3) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(116, sysDocID, voucherID, isDeletingTransaction, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(39, sysDocID, voucherID, isDeletingTransaction, sqlTransaction))));
					string text = "";
					string text2 = "";
					string text3 = "";
					foreach (DataRow row in purchaseInvoiceNIData.PurchaseInvoiceDetailTable.Rows)
					{
						text3 = row["ProductID"].ToString();
						text = row["OrderVoucherID"].ToString();
						text2 = row["OrderSysDocID"].ToString();
						int result4 = 0;
						if (!(text == "") && !(text2 == ""))
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
							float num = new Products(base.DBConfig).GetOrderedQuantity(text3, sqlTransaction) + result5;
							if (num < 0f)
							{
								num = 0f;
							}
							flag &= new Products(base.DBConfig).UpdateOrderedQuantity(text3, num, sqlTransaction);
							flag &= new PurchaseOrderNI(base.DBConfig).UpdateRowReceivedQuantity(text2, text, result4, -1f * result5, sqlTransaction);
						}
					}
				}
				textCommand = "DELETE FROM Product_Lot_Receiving_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= Delete(textCommand, sqlTransaction);
				textCommand = "DELETE FROM Purchase_Invoice_NonInv_Detail WHERE SysDocID = '" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				return flag & Delete(textCommand, sqlTransaction);
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
				string commandText = "DELETE FROM Purchase_Invoice_NonInv_Expense WHERE InvoiceSysDocID = '" + sysDocID + "' AND InvoiceVoucherID = '" + voucherID + "'";
				return Delete(commandText, sqlTransaction);
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
				PurchaseInvoiceNIData purchaseInvoiceNIData = new PurchaseInvoiceNIData();
				string textCommand = "SELECT PID.*,ISVOID,IsCash,IsImport,TransactionDate FROM Purchase_Invoice_NonInv_Detail PID INNER JOIN Purchase_Invoice_NonInv PI ON PI.SysDocID=PID.SysDocID AND PI.VOucherID=PID.VoucherID\r\n\t\t\t\t\t\t\t  WHERE PID.SysDocID = '" + sysDocID + "' AND PID.VoucherID = '" + voucherID + "'";
				FillDataSet(purchaseInvoiceNIData, "Purchase_Invoice_NonInv_Detail", textCommand, sqlTransaction);
				textCommand = "SELECT * FROM Invoice_GRN WHERE InvoiceSysDocID='" + sysDocID + "' AND InvoiceNumber='" + voucherID + "'";
				FillDataSet(purchaseInvoiceNIData, "Invoice_GRN", textCommand, sqlTransaction);
				textCommand = "SELECT * FROM Purchase_Invoice_NonInv WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				FillDataSet(purchaseInvoiceNIData, "Purchase_Invoice_NonInv", textCommand, sqlTransaction);
				bool result = false;
				bool.TryParse(purchaseInvoiceNIData.PurchaseInvoiceDetailTable.Rows[0]["IsVoid"].ToString(), out result);
				bool result2 = false;
				bool.TryParse(purchaseInvoiceNIData.PurchaseInvoiceDetailTable.Rows[0]["IsCash"].ToString(), out result2);
				bool result3 = false;
				bool.TryParse(purchaseInvoiceNIData.PurchaseInvoiceDetailTable.Rows[0]["IsImport"].ToString(), out result3);
				DateTime.Parse(purchaseInvoiceNIData.PurchaseInvoiceDetailTable.Rows[0]["TransactionDate"].ToString());
				if (result == isVoid)
				{
					throw new CompanyException("The transaction is already voided.");
				}
				flag = (result2 ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(34, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : ((!result3) ? (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(116, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction)) : (flag & new InventoryTransaction(base.DBConfig).DeleteInventoryTransaction(39, sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction))));
				string text = "";
				string text2 = "";
				string text3 = "";
				foreach (DataRow row in purchaseInvoiceNIData.PurchaseInvoiceDetailTable.Rows)
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
							flag &= new PurchaseOrderNI(base.DBConfig).UpdateRowReceivedQuantity(text2, text, result4, -1f * result5, sqlTransaction);
							flag &= new PurchaseOrderNI(base.DBConfig).ReOpenOrder(text2, text, sqlTransaction);
						}
					}
				}
				ItemSourceTypes itemSourceTypes2 = ItemSourceTypes.None;
				DataRow dataRow2 = purchaseInvoiceNIData.Tables["Purchase_Invoice_NonInv"].Rows[0];
				if (dataRow2["SourceDocType"] != DBNull.Value)
				{
					itemSourceTypes2 = (ItemSourceTypes)byte.Parse(dataRow2["SourceDocType"].ToString());
				}
				if (itemSourceTypes2 == ItemSourceTypes.GRN)
				{
					textCommand = " UPDATE IT SET IsNonCostedGRN = 'True' FROM Inventory_Transactions IT \r\n                                INNER JOIN Purchase_Invoice_NonInv_Detail PID ON IT.SysdocID = PID.OrderSysDocID AND IT.VoucherID = PID.OrderVoucherID AND IT.RowIndex = PID.OrderRowIndex\r\n                                WHERE PID.SysDocID = '" + sysDocID + "' AND PID.VoucherID = '" + voucherID + "'";
				}
				textCommand = "UPDATE Purchase_Receipt SET IsInvoiced='False',InvoiceSysDocID=NULL,InvoiceVoucherID=NULL WHERE InvoiceSysDocID='" + sysDocID + "' AND InvoiceVoucherID='" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) >= 0);
				flag &= new Journal(base.DBConfig).VoidJournal(sysDocID, voucherID, isVoid, sqlTransaction);
				textCommand = "UPDATE Purchase_Invoice_NonInv SET IsVoid = '" + isVoid.ToString() + "' WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(textCommand, sqlTransaction) > 0);
				if (!flag)
				{
					throw new Exception("Failed to save the transaction because of an unexpected error. Please try again.");
				}
				flag &= UpdateTableRowInsertUpdateInfo("Purchase_Invoice_NonInv", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction, isInsert: false);
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
				bool.TryParse(new Databases(base.DBConfig).GetFieldValue("Purchase_Invoice_NonInv", "IsVoid", "SysDocID", sysDocID, "VoucherID", voucherID, sqlTransaction).ToString(), out result);
				if (!result)
				{
					flag &= VoidPurchaseInvoice(sysDocID, voucherID, isVoid: true, sqlTransaction);
				}
				flag &= DeletePurchaseInvoiceDetailsRows(sysDocID, voucherID, isDeletingTransaction: true, sqlTransaction);
				flag &= new Journal(base.DBConfig).DeleteJournal(sysDocID, voucherID, sqlTransaction);
				text = "DELETE FROM Purchase_Invoice_NonInv WHERE SysDocID='" + sysDocID + "' AND VoucherID = '" + voucherID + "'";
				flag &= (ExecuteNonQuery(text, sqlTransaction) >= 0);
				flag &= DeletePurchaseInvoiceExpenseRows(sysDocID, voucherID, sqlTransaction);
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
				string str = "SELECT COUNT(RowIndex)FROM Purchase_Invoice_NonInv_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
				str += " AND CASE WHEN UnitQuantity IS NULL THEN Quantity ELSE UnitQuantity END - ISNULL(QuantityReceived,0)=0";
				object obj = ExecuteScalar(str, sqlTransaction);
				if (obj == null || int.Parse(obj.ToString()) == 0)
				{
					return true;
				}
				str = "UPDATE Purchase_Invoice_NonInv SET IsDelivered = 1 WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "'";
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
				string textCommand = "SELECT Quantity,UnitQuantity,QuantityReceived FROM Purchase_Invoice_NonInv_Detail WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
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
				textCommand = "UPDATE Purchase_Invoice_NonInv_Detail SET QuantityReceived=" + num.ToString() + " WHERE SysDocID='" + sysDocID + "' AND VoucherID='" + voucherID + "' AND RowIndex=" + rowIndex.ToString();
				return ExecuteNonQuery(textCommand, sqlTransaction) > 0;
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
				string exp = "  UPDATE  PID  SET QuantityReturned = \r\n                                     (SELECT SUM(Quantity) FROM Purchase_Return_Detail PRD  INNER JOIN Purchase_Return PR                                      \r\n                                     ON PR.SysDocID = PRD.SysDocID AND PR.VoucherID = PRD.VoucherID\r\n\t                                 WHERE ISNULL(IsVoid,'False') = 'False' AND PRD.SourceSysDocID = PID.SysDocID AND PRD.SourceVoucherID = PID.VoucherID AND PRD.SourceRowIndex = PID.RowIndex )\r\n\t\t\t\t\t\t\t\t\t    FROM Purchase_Invoice_NonInv_Detail PID WHERE PID.SysDocID = '" + sysDocID + "' AND PID.VoucherID = '" + voucherID + "'";
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
				string text = "SELECT SO.SysDocID [Doc ID],SO.VoucherID [Number],TransactionDate AS [Date],SO.VendorID + '-' + C.VendorName AS [Vendor] FROM Purchase_Invoice_NonInv SO\r\n\t\t\t\t\t\t\t INNER JOIN Vendor C ON SO.VendorID=C.VendorID  WHERE ISNULL(IsDelivered,0)=0";
				if (vendorID != "")
				{
					text = text + " AND SO.VendorID='" + vendorID + "'";
				}
				FillDataSet(dataSet, "Purchase_Invoice_NonInv", text);
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
				string cmdText = "SELECT  DISTINCT SI.*,Vendor.VendorName,B.FullName,\r\n                            (SELECT SUM(Amount) from [dbo].Purchase_Invoice_NonInv_Expense where InvoiceSysDocID=  '" + sysDocID + "' AND InvoiceVoucherID = " + text + ") AS EXPENSE,\r\n                            VA.AddressPrintFormat AS VendorAddress,ISNULL(SI.CurrencyID,(SELECT CurrencyID FROM Currency WHERE IsBase='True')) AS CurrencyID,\r\n                            ISNULL(SI.DiscountFC,SI.Discount) AS Discount,ISNULL(SI.TotalFC,SI.Total) - ISNULL(ISNULL(SI.DiscountFC,SI.Discount),0) AS GrandTotal,\r\n                            ISNULL(ISNULL(SI.TaxAmountFC,SI.TaxAmount) ,0) AS Tax,ISNULL(SI.TotalFC,SI.Total) AS Total,\r\n                            VA.Country as VendorCountry,SI.DueDate as InvoiceDueDate,CU.ExchangeRate as ExchangeRate,SM.ShippingMethodName,PT.TermName,\r\n                            (SELECT COUNT(PL.BOLNumber) FROM PO_Shipment PL WHERE PL.BOLNumber=SI.BOLNo AND ISNULL(PL.BOLNumber,'') <>'') AS [BOL_PL],\r\n                            (SELECT COUNT(PL.ContainerNumber) FROM PO_Shipment PL WHERE PL.BOLNumber=SI.BOLNo AND ISNULL(PL.BOLNumber,'') <>'') AS [BOL_Con],\r\n                            (SELECT SUM(PLD.Quantity) FROM PO_Shipment_Detail PLD INNER JOIN PO_Shipment PL ON PLD.SysDocID=PL.SysDocID \r\n                            AND PLD.VoucherID=PL.VoucherID WHERE PL.BOLNumber=SI.BOLNo AND ISNULL(PL.BOLNumber,'') <>'')AS [No. Box],\r\n                            (SELECT SUBSTRING((SELECT  DISTINCT ',' + PC.CategoryName \r\n                            FROM PO_Shipment_Detail PLD INNER JOIN \r\n                            PO_Shipment PL ON PLD.SysDocID=PL.SysDocID AND PLD.VoucherID=PL.VoucherID\r\n                            INNER JOIN Product P ON P.ProductID=PLD.ProductID\r\n                            LEFT OUTER JOIN Product_Category PC ON P.CategoryID=PC.CategoryID\r\n                            WHERE PL.BOLNumber= SI.BOLNo FOR XML PATH('')),2,20000) )AS  Category,\r\n                            (SELECT  DISTINCT  PL.VendorID +' - '+V.VendorName FROM  PO_Shipment PL INNER JOIN Vendor V ON \r\n                            PL.VendorID=V.VendorID WHERE PL.BOLNumber= SI.BOLNo AND ISNULL(PL.BOLNumber,'') <>'')AS [Supplier],\r\n                            (SELECT SUBSTRING((SELECT DISTINCT ',' + C.CountryName \r\n                            FROM PO_Shipment_Detail PLD INNER JOIN \r\n                            PO_Shipment PL ON PLD.SysDocID=PL.SysDocID AND PLD.VoucherID=PL.VoucherID\r\n                            INNER JOIN Product P ON P.ProductID=PLD.ProductID\r\n                            LEFT OUTER JOIN Country C ON P.Origin=C.CountryID\r\n                            WHERE PL.BOLNumber= SI.BOLNo FOR XML PATH('')),2,20000) )AS Orgin,Vendor.TaxIDNumber as VTaxIDNo\r\n                            FROM  Purchase_Invoice_NonInv SI INNER JOIN Vendor ON SI.VendorID=Vendor.VendorID\r\n                            LEFT OUTER JOIN Payment_Term PT ON SI.TermID=PT.PaymentTermID\r\n                            LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=SI.VendorID AND VA.AddressID='PRIMARY'\r\n                            LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=SI.ShippingMethodID\r\n                            LEFT OUTER JOIN Currency CU ON CU.CurrencyID=SI.CurrencyID\r\n                            LEFT OUTER JOIN Buyer B ON B.BuyerID=SI.BuyerID\r\n                            WHERE SI.SysDocID = '" + sysDocID + "' AND SI.VoucherID IN (" + text + ")";
				SqlCommand sqlCommand = new SqlCommand(cmdText);
				FillDataSet(dataSet, "Purchase_Invoice_NonInv", sqlCommand);
				if (dataSet == null || dataSet.Tables.Count == 0 || dataSet.Tables["Purchase_Invoice_NonInv"].Rows.Count == 0)
				{
					return null;
				}
				cmdText = "SELECT     SysDocID,VoucherID,PID.ProductID,PID.Description,P.COGSAccount,P.IncomeAccount,P.AssetAccount,ACT1.AccountName AS [COGS Act],ACT2.AccountName AS [Sales Act],ISNULL(UnitQuantity,PID.Quantity) AS Quantity,P.Attribute1,P.Attribute2,P.Attribute3,P.MatrixParentID,PID.Remarks,\r\n\t\t\t\t\t\tISNULL(UnitPriceFC,PID.UnitPrice) AS UnitPrice,UnitPrice as UnitPriceLC,\r\n\t\t\t\t\t\tISNULL(UnitQuantity,PID.Quantity)*ISNULL(UnitPriceFC,PID.UnitPrice) AS Total,\r\n                        ISNULL(UnitQuantity,PID.Quantity)*ISNULL(PID.UnitPrice,0) AS TotalLC,\r\n                        PID.UnitID,PID.LocationID, P.BrandID,PID.AttributeID1 AS [AttributeID1ID], PID.AttributeID2 AS [AttributeID2ID], PropertyName AS [AttributeID1Name], PropertyUnitName AS [AttributeID2Name],\r\n                        PID.AnalysisID, A.AnalysisName,J.JobID,J.JobName \r\n\t\t\t\t\t\tFROM   Purchase_Invoice_NonInv_Detail PID \r\n\t\t\t\t\t\tINNER JOIN Product P ON P.ProductID=PID.ProductID\r\n                        LEFT OUTER JOIN Property Pr ON Pr.PropertyID=PID.AttributeID1\r\n                        LEFT OUTER JOIN Property_Unit PU ON PU.PropertyUnitID=PID.AttributeID2\r\n                        LEFT OUTER JOIN Analysis A ON A.AnalysisID=PID.AnalysisID\r\n                        LEFT OUTER JOIN Account ACT1 ON ACT1.AccountID=P.COGSAccount\r\n                        LEFT OUTER JOIN Account ACT2 ON ACT2.AccountID=P.COGSAccount\r\n                        LEFT OUTER JOIN Job J ON J.JobID=PID.JobID \r\n\t\t\t\t\t\tWHERE SysDocID='" + sysDocID + "' AND VoucherID IN (" + text + ")  ORDER BY RowIndex";
				FillDataSet(dataSet, "Purchase_Invoice_NonInv_Detail", cmdText);
				cmdText = "SELECT  * FROM Purchase_Invoice_NonInv_Expense WHERE InvoiceSysDocID = '" + sysDocID + "' AND InvoiceVoucherID IN (" + text + ")";
				FillDataSet(dataSet, "Purchase_Invoice_NonInv_Expense", cmdText);
				string str = "";
				if (dataSet.Tables["Purchase_Invoice_NonInv"].Rows.Count > 0)
				{
					str = dataSet.Tables["Purchase_Invoice_NonInv"].Rows[0]["BOLNo"].ToString();
				}
				cmdText = "SELECT  DISTINCT  PL.*,V.VendorName,VA.AddressPrintFormat AS VendorAddress,ShippingMethodName\r\n                        FROM  PO_Shipment PL INNER JOIN Vendor V ON PL.VendorID=V.VendorID   \r\n                        LEFT OUTER JOIN Vendor_Address VA ON VA.VendorID=PL.VendorID AND VA.AddressID='PRIMARY'     \r\n                        LEFT OUTER JOIN Shipping_Method SM ON SM.ShippingMethodID=PL.ShippingMethodID  \r\n                        WHERE PL.BOLNumber='" + str + "' AND ISNULL(PL.BOLNumber,'') <>''";
				FillDataSet(dataSet, "PO_Shipment", cmdText);
				dataSet.Relations.Add("VendorInvoice", new DataColumn[2]
				{
					dataSet.Tables["Purchase_Invoice_NonInv"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Invoice_NonInv"].Columns["VoucherID"]
				}, new DataColumn[2]
				{
					dataSet.Tables["Purchase_Invoice_NonInv_Detail"].Columns["SysDocID"],
					dataSet.Tables["Purchase_Invoice_NonInv_Detail"].Columns["VoucherID"]
				}, createConstraints: false);
				dataSet.Tables["Purchase_Invoice_NonInv"].Columns.Add("TotalInWords", typeof(string));
				foreach (DataRow row in dataSet.Tables["Purchase_Invoice_NonInv"].Rows)
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
				if (dataSet.Tables["Purchase_Invoice_NonInv_Expense"].Rows.Count > 0)
				{
					dataSet.Relations.Add("VendorInvoiceExpense", new DataColumn[2]
					{
						dataSet.Tables["Purchase_Invoice_NonInv"].Columns["SysDocID"],
						dataSet.Tables["Purchase_Invoice_NonInv"].Columns["VoucherID"]
					}, new DataColumn[2]
					{
						dataSet.Tables["Purchase_Invoice_NonInv_Expense"].Columns["InvoiceSysDocID"],
						dataSet.Tables["Purchase_Invoice_NonInv_Expense"].Columns["InvoiceVoucherID"]
					}, createConstraints: false);
				}
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetPurchaseExpenseAllocationReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string sysDocID, string voucherID)
		{
			try
			{
				string text = StoreConfiguration.ToSqlDateTimeString(fromDate);
				string text2 = StoreConfiguration.ToSqlDateTimeString(toDate);
				string empty = string.Empty;
				DataSet dataSet = new DataSet();
				empty = "SELECT PID.SysDocID, PID.VoucherID, PID.ProductID, PID.Description, PID.Quantity [Qty Purchased], P.Weight, PID.UnitID, UnitPrice [Factory Cost], UnitPriceFC [Factory Cost FC], PID.Amount [Purchased Amount], PID.AmountFC [Purchased Amount FC], \r\n\t\t\t\t        LCost * PID.Quantity [Direct Cost Weightwise], LCost [Direct Cost per Unit], UnitPrice + LCost [Per Unit Cost], V.VendorID + ' - ' + VendorName [Vendor] \r\n\t                  FROM Purchase_Invoice_NonInv_Detail PID\r\n\t                  INNER JOIN Purchase_Invoice_NonInv PI ON PID.SysDocID = PI.SysDocID AND PID.VoucherID = PI.VoucherID\r\n\t                  INNER JOIN Product P ON PID.ProductID = P.ProductID \r\n                      INNER JOIN Vendor V ON PI.VendorID = V.VendorID \r\n                        WHERE TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' AND ISNULL(PI.IsVoid, 'False') = 'False'";
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
				FillDataSet(dataSet, "Purchase_Invoice_NonInv_Detail", empty);
				return dataSet;
			}
			catch
			{
				throw;
			}
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			DataSet dataSet = new DataSet();
			string text = StoreConfiguration.ToSqlDateTimeString(from);
			string text2 = StoreConfiguration.ToSqlDateTimeString(to);
			string text3 = "SELECT   ISNULL(IsVoid,'False') AS V,  SysDocID [Doc ID],VoucherID [Doc Number],INV.VendorID [Vendor Code],VendorName [Vendor Name],\r\n\t\t\t\t\t\t\tINV.Reference AS Ref1,INV.Reference2 AS Ref2, TransactionDate [Invoice Date], DueDate,INV.ShippingMethodID AS [Shipping Method],\r\n\t\t\t\t\t\t\tINV.TermID Term,\r\n\t\t\t\t\t\t\tCASE ISNULL(IsCash,'False') WHEN 'True' THEN 'Cash' ELSE CASE ISNULL(IsImport,'False') WHEN 'True' THEN 'Import' ELSE 'Credit' END END AS [Type],\r\n\t\t\t\t\t\t\tINV.BuyerID [Purchaseperson],INV.CurrencyID AS Currency, INV.CurrencyRate AS [Cur Rate],TotalFC - ISNULL(DiscountFC,0) AS [Amount FC],Total - ISNULL(Discount,0) [Amount],J.JobID,J.JobName, ISNULL((CASE INV.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge' END) ,(CASE Vendor.TaxOption WHEN 0 THEN 'BasedOnClass'  WHEN 1 THEN 'TAXABLE' when 2 then 'NON TAXABLE' WHEN 3 THEN 'ReverseCharge'  END))AS TAXOPTION,INV.TaxAmount\r\n\t\t\t\t\t\t\tFROM         Purchase_Invoice_NonInv INV\r\n                            LEFT JOIN Job J ON INV.JobID=J.JobID\r\n\t\t\t\t\t\t\tInner JOIN Vendor ON VENDOR.VendorID=INV.VendorID";
			if (from != DateTime.MinValue)
			{
				text3 = text3 + " WHERE TransactionDate Between '" + text + "' AND '" + text2 + "'";
			}
			if (!showVoid)
			{
				text3 += " AND ISNULL(IsVoid,'False')='False'";
			}
			SqlCommand sqlCommand = new SqlCommand(text3);
			FillDataSet(dataSet, "Purchase_Invoice_NonInv", sqlCommand);
			return dataSet;
		}

		public DataSet GetPurchaseInvoiceList(string sysDocID)
		{
			DataSet dataSet = new DataSet();
			SqlCommand sqlCommand = new SqlCommand("SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date]    \r\n                            FROM Purchase_Invoice_NonInv \r\n                            WHERE ISNULL(IsVoid,'False')='False'" + " ORDER BY TransactionDate, VoucherID ");
			FillDataSet(dataSet, "Purchase_Invoice_NonInv", sqlCommand);
			return dataSet;
		}

		public DataSet GetPurchaseList(string sysDocID, DateTime from, DateTime to)
		{
			string text = CommonLib.ToSqlDateTimeString(from);
			string text2 = CommonLib.ToSqlDateTimeString(to);
			DataSet dataSet = new DataSet();
			string str = "SELECT SysDocID [Doc ID], VoucherID [Number], TransactionDate AS [Date],Ven.VendorName AS [Vendor],PI.ContainerNumber AS [Container#]\r\n                            FROM Purchase_Invoice_NonInv  PI INNER JOIN Vendor VEN ON PI.VendorID = Ven.VendorID\r\n                            WHERE ISNULL(IsVoid,'False')='False'  AND TransactionDate BETWEEN '" + text + "' AND '" + text2 + "' ";
			if (!string.IsNullOrEmpty(sysDocID))
			{
				str = str + " AND SysDocID='" + sysDocID + "'";
			}
			str += " ORDER BY TransactionDate, VoucherID ";
			FillDataSet(dataSet, "Purchase_Invoice_NonInv", str);
			return dataSet;
		}

		public bool IsAlreadyExisting(string sysDocID, string voucherID, string vendorID, string supplierDocNo)
		{
			SqlTransaction sqlTransaction = base.DBConfig.StartNewTransaction();
			string exp = "SELECT COUNT(*) FROM Purchase_Invoice_NonInv SID  WHERE ISNULL(IsVoid,'False')='False' AND SysDocID = '" + sysDocID + "' AND VoucherID <>'" + voucherID + "' AND VendorID= '" + vendorID + "' AND SupplierInvoiceNumber='" + supplierDocNo + "'";
			object obj = ExecuteScalar(exp, sqlTransaction);
			base.DBConfig.EndTransaction(result: true);
			if (obj != null && obj.ToString() != "" && int.Parse(obj.ToString()) > 0)
			{
				return true;
			}
			return false;
		}
	}
}
