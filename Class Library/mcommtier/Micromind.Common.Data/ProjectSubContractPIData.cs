using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ProjectSubContractPIData : DataSet
	{
		public const string PROJECTSUBCONTRACTPI_TABLE = "Project_SubContract_PI";

		public const string PROJECTSUBCONTRACTPIDETAIL_TABLE = "Project_SubContract_PI_Detail";

		public const string INVOICEGRN_TABLE = "Invoice_GRN";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string VENDORID_FIELD = "VendorID";

		public const string PURCHASEFLOW_FIELD = "PurchaseFlow";

		public const string ISIMPORT_FIELD = "IsImport";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string BUYERID_FIELD = "BuyerID";

		public const string STATUS_FIELD = "Status";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string TERMID_FIELD = "TermID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string REFERENCE2_FIELD = "Reference2";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string BOLNO_FIELD = "BOLNo";

		public const string SOURCEDOCTYPE_FIELD = "SourceDocType";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DISCOUNT_FIELD = "Discount";

		public const string DISCOUNTFC_FIELD = "DiscountFC";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TAXAMOUNTFC_FIELD = "TaxAmountFC";

		public const string TOTAL_FIELD = "Total";

		public const string TOTALFC_FIELD = "TotalFC";

		public const string REGISTERID_FIELD = "RegisterID";

		public const string ISCASH_FIELD = "IsCash";

		public const string DUEDATE_FIELD = "DueDate";

		public const string CONTAINERNUMBER_FIELD = "ContainerNumber";

		public const string PORT_FIELD = "Port";

		public const string BOLNUMBER_FIELD = "BOLNumber";

		public const string SHIPPER_FIELD = "Shipper";

		public const string CLEARINGAGENT_FIELD = "ClearingAgent";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string ORDERVALUE_FIELD = "OrderValue";

		public const string INVOICED_FIELD = "Invoiced";

		public const string COMPLETEDPERCENT_FIELD = "PercentCompleted";

		public const string CURRENTVALUE_FIELD = "CurrentValue";

		public const string CURRENTPERCENT_FIELD = "CurrentPercent";

		public const string PAYEETAXGROUPID_FIELD = "PayeeTaxGroupID";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string PRICEINCLUDETAX_FIELD = "PriceIncludeTax";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string INVOICESYSDOCID_FIELD = "InvoiceSysDocID";

		public const string INVOICENUMBER_FIELD = "InvoiceNumber";

		public const string GRNSYSDOCID_FIELD = "GRNSysDocID";

		public const string GRNNUMBER_FIELD = "GRNNumber";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string UNITPRICEFC_FIELD = "UnitPriceFC";

		public const string DESCRIPTION_FIELD = "Description";

		public const string LCOST_FIELD = "LCost";

		public const string LCOSTAMOUNT_FIELD = "LCostAmount";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string QUANTITYSHIPPED_FIELD = "QuantityShipped";

		public const string ORDERVOUCHERID_FIELD = "OrderVoucherID";

		public const string ORDERSYSDOCID_FIELD = "OrderSysDocID";

		public const string PORVOUCHERID_FIELD = "PORVoucherID";

		public const string PORSYSDOCID_FIELD = "PORSysDocID";

		public const string ORDERROWINDEX_FIELD = "OrderRowIndex";

		public const string ISPORROW_FIELD = "IsPORRow";

		public const string LOTNUMBER_FIELD = "LotNumber";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string ROWSOURCE_FIELD = "RowSource";

		public const string REMARKS_FIELD = "Remarks";

		public const string PURCHASEINVOICEEXPENSETABLE_FIELD = "Purchase_Invoice_NonInv_Expense";

		public const string INVOICEVOUCHERID_FIELD = "InvoiceVoucherID";

		public const string EXPENSEID_FIELD = "ExpenseID";

		public const string RATETYPE_FIELD = "RateType";

		public DataTable InvoiceGRNTable => base.Tables["Invoice_GRN"];

		public DataTable InvoiceExpenseTable => base.Tables["Purchase_Invoice_NonInv_Expense"];

		public DataTable ProjectSubContractPITable => base.Tables["Project_SubContract_PI"];

		public DataTable ProjectSubContractPIDetailTable => base.Tables["Project_SubContract_PI_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public ProjectSubContractPIData()
		{
			BuildDataTables();
		}

		public ProjectSubContractPIData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Project_SubContract_PI");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("SysDocID", typeof(string));
			DataColumn dataColumn2 = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("VendorID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("DueDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("BuyerID", typeof(string));
			columns.Add("PurchaseFlow", typeof(byte));
			columns.Add("IsImport", typeof(bool)).DefaultValue = false;
			columns.Add("Status", typeof(byte));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("TermID", typeof(string));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("BOLNo", typeof(string));
			columns.Add("SourceDocType", typeof(byte));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("DiscountFC", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("TaxAmountFC", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("TotalFC", typeof(decimal));
			columns.Add("RegisterID", typeof(string));
			columns.Add("IsCash", typeof(bool));
			columns.Add("ContainerNumber", typeof(string));
			columns.Add("Port", typeof(string));
			columns.Add("BOLNumber", typeof(string));
			columns.Add("Shipper", typeof(string));
			columns.Add("ClearingAgent", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("PayeeTaxGroupID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Project_SubContract_PI_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("UnitPriceFC", typeof(decimal));
			columns.Add("Description", typeof(string));
			columns.Add("LCost", typeof(decimal));
			columns.Add("LCostAmount", typeof(decimal));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AmountFC", typeof(decimal)).DefaultValue = 0;
			columns.Add("FactorType", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("OrderVoucherID", typeof(string));
			columns.Add("OrderSysDocID", typeof(string));
			columns.Add("PORVoucherID", typeof(string));
			columns.Add("PORSysDocID", typeof(string));
			columns.Add("OrderRowIndex", typeof(int));
			columns.Add("IsPORRow", typeof(bool));
			columns.Add("LotNumber", typeof(int));
			columns.Add("RowSource", typeof(byte));
			columns.Add("OrderValue", typeof(decimal)).DefaultValue = 0;
			columns.Add("Invoiced", typeof(decimal)).DefaultValue = 0;
			columns.Add("PercentCompleted", typeof(decimal)).DefaultValue = 0;
			columns.Add("CurrentValue", typeof(decimal)).DefaultValue = 0;
			columns.Add("CurrentPercent", typeof(decimal)).DefaultValue = 0;
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("TaxAmountFC", typeof(decimal));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Invoice_GRN");
			columns = dataTable.Columns;
			columns.Add("InvoiceSysDocID", typeof(string));
			columns.Add("InvoiceNumber", typeof(string));
			columns.Add("GRNSysDocID", typeof(string));
			columns.Add("GRNNumber", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Purchase_Invoice_NonInv_Expense");
			columns = dataTable.Columns;
			columns.Add("InvoiceSysDocID", typeof(string));
			columns.Add("InvoiceVoucherID", typeof(string));
			columns.Add("ExpenseID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("Reference", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("RateType", typeof(string));
			columns.Add("RowIndex", typeof(int));
			columns.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotReceivingDetailTable(this);
			Merge(new TaxTransactionData().TaxDetailTable);
		}
	}
}
