using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class SalesInvoiceData : DataSet
	{
		public const string SALESINVOICE_TABLE = "Sales_Invoice";

		public const string SALESINVOICEDETAIL_TABLE = "Sales_Invoice_Detail";

		public const string INVOICEDNOTE_TABLE = "Invoice_DNote";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string SALESFLOW_FIELD = "SalesFlow";

		public const string ISEXPORT_FIELD = "IsExport";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SALESPERSONID_FIELD = "SalespersonID";

		public const string REPORTTO_FIELD = "ReportTo";

		public const string REQUIREDDATE_FIELD = "RequiredDate";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string BILLINGADDRESSID_FIELD = "BillingAddressID";

		public const string SHIPTOADDRESS_FIELD = "ShipToAddress";

		public const string CUSTOMERADDRESS_FIELD = "CustomerAddress";

		public const string PAYEETAXGROUPID_FIELD = "PayeeTaxGroupID";

		public const string TAXOPTION_FIELD = "TaxOption";

		public const string STATUS_FIELD = "Status";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string TERMID_FIELD = "TermID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string REFERENCE2_FIELD = "Reference2";

		public const string NOTE_FIELD = "Note";

		public const string CLUSERID_FIELD = "CLUserID";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string ISWEIGHTINVOICE_FIELD = "IsWeightInvoice";

		public const string DISCOUNT_FIELD = "Discount";

		public const string DISCOUNTFC_FIELD = "DiscountFC";

		public const string TAXGROUPID_FIELD = "TaxGroupID";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string ROUNDOFF_FIELD = "RoundOff";

		public const string TAXAMOUNTFC_FIELD = "TaxAmountFC";

		public const string TOTAL_FIELD = "Total";

		public const string TOTALFC_FIELD = "TotalFC";

		public const string ISCASH_FIELD = "IsCash";

		public const string REGISTERID_FIELD = "RegisterID";

		public const string DUEDATE_FIELD = "DueDate";

		public const string SOURCEDOCTYPE_FIELD = "SourceDocType";

		public const string WEIGHTQUANTITY_FIELD = "WeightQuantity";

		public const string WEIGHTPRICE_FIELD = "WeightPrice";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string PRICEINCLUDETAX_FIELD = "PriceIncludeTax";

		public const string TEMPKEY_FIELD = "TempKey";

		public const string CURRENTUSER_FIELD = "CurrentUser";

		public const string AUTOKEYID_FIELD = "AutoKeyID";

		public const string DRIVERID_FIELD = "DriverID";

		public const string VEHICLEID_FIELD = "VehicleID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string FOCQUANTITY_FIELD = "FOCQuantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string UNITPRICEFC_FIELD = "UnitPriceFC";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REMARKS_FIELD = "Remarks";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string TAXPERCENTAGE_FIELD = "TaxPercentage";

		public const string QUANTITYSHIPPED_FIELD = "QuantityShipped";

		public const string QUANTITYRETURNED_FIELD = "QuantityReturned";

		public const string ORDERVOUCHERID_FIELD = "OrderVoucherID";

		public const string ORDERSYSDOCID_FIELD = "OrderSysDocID";

		public const string DNOTEVOUCHERID_FIELD = "DNoteVoucherID";

		public const string DNOTESYSDOCID_FIELD = "DNoteSysDocID";

		public const string ORDERROWINDEX_FIELD = "OrderRowIndex";

		public const string ISDNROW_FIELD = "IsDNRow";

		public const string ISRECOST_FIELD = "IsRecost";

		public const string ROWSOURCE_FIELD = "RowSource";

		public const string LISTVOUCHERID_FIELD = "ListVoucherID";

		public const string LISTSYSDOCID_FIELD = "ListSysDocID";

		public const string LISTROWINDEX_FIELD = "ListRowIndex";

		public const string COST_FIELD = "Cost";

		public const string JOBID_FIELD = "JobID";

		public const string CONSIGNMENTNO_FIELD = "ConsignmentNo";

		public const string SPECIFICATIONID_FIELD = "SpecificationID";

		public const string STYLEID_FIELD = "StyleID";

		public const string INVOICETAX_TABLE = "InvoiceTax";

		public const string REFSLNO_FIELD = "RefSlNo";

		public const string REFTEXT1_FIELD = "RefText1";

		public const string REFTEXT2_FIELD = "RefText2";

		public const string REFNUM1_FIELD = "RefNum1";

		public const string REFNUM2_FIELD = "RefNum2";

		public const string REFDATE1_FIELD = "RefDate1";

		public const string REFDATE2_FIELD = "RefDate2";

		public const string ACCOUNTID_FIELD = "AccountID";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string PAYMENTMETHODTYPE_FIELD = "PaymentMethodType";

		public const string EXPAMOUNT_FIELD = "ExpAmount";

		public const string EXPCODE_FIELD = "ExpCode";

		public const string EXPPERCENT_FIELD = "ExpPercent";

		public const string INVOICEPAYMENT_TABLE = "Invoice_Payment";

		public const string SALESINVOICEEXPENSETABLE_TABLE = "Sales_Invoice_Expense";

		public const string EXPENSEID_FIELD = "ExpenseID";

		public const string RATETYPE_FIELD = "RateType";

		public const string ISDEDUCT_FIELD = "IsDeduct";

		public const string INVOICESYSDOCID_FIELD = "InvoiceSysDocID";

		public const string INVOICEVOUCHERID_FIELD = "InvoiceVoucherID";

		public DataTable InvoiceTaxTable => base.Tables["InvoiceTax"];

		public DataTable PaymentTable => base.Tables["Invoice_Payment"];

		public DataTable InvoiceDNoteTable => base.Tables["Invoice_DNote"];

		public DataTable InvoiceExpenseTable => base.Tables["Sales_Invoice_Expense"];

		public DataTable SalesInvoiceTable => base.Tables["Sales_Invoice"];

		public DataTable SalesInvoiceDetailTable => base.Tables["Sales_Invoice_Detail"];

		public DataTable TaxDetailsTable => base.Tables["Tax_Detail"];

		public SalesInvoiceData()
		{
			BuildDataTables();
		}

		public SalesInvoiceData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Sales_Invoice");
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
			columns.Add("CompanyID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("CustomerID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("DueDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("SalespersonID", typeof(string));
			columns.Add("ReportTo", typeof(string));
			columns.Add("SalesFlow", typeof(byte));
			columns.Add("RequiredDate", typeof(DateTime));
			columns.Add("PriceIncludeTax", typeof(bool));
			columns.Add("ShippingAddressID", typeof(string));
			columns.Add("BillingAddressID", typeof(string));
			columns.Add("CustomerAddress", typeof(string));
			columns.Add("ShipToAddress", typeof(string));
			columns.Add("PayeeTaxGroupID", typeof(string));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("IsExport", typeof(bool));
			columns.Add("Status", typeof(byte));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("TermID", typeof(string));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PaymentMethodType", typeof(byte));
			columns.Add("ExpAmount", typeof(decimal));
			columns.Add("ExpPercent", typeof(decimal));
			columns.Add("ExpCode", typeof(string));
			columns.Add("RegisterID", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("IsWeightInvoice", typeof(bool));
			columns.Add("CLUserID", typeof(string));
			columns.Add("Discount", typeof(decimal));
			columns.Add("DiscountFC", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("TaxAmountFC", typeof(decimal));
			columns.Add("RoundOff", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("TotalFC", typeof(decimal));
			columns.Add("IsCash", typeof(bool));
			columns.Add("SourceDocType", typeof(byte));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("TempKey", typeof(string));
			columns.Add("CurrentUser", typeof(string));
			columns.Add("AutoKeyID", typeof(long));
			columns.Add("DriverID", typeof(string));
			columns.Add("VehicleID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Sales_Invoice_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("FOCQuantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AmountFC", typeof(decimal)).DefaultValue = 0;
			columns.Add("UnitPriceFC", typeof(decimal));
			columns.Add("Description", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxPercentage", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("WeightQuantity", typeof(decimal));
			columns.Add("WeightPrice", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("ConsignmentNo", typeof(string));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("Discount", typeof(decimal)).DefaultValue = 0;
			columns.Add("RowIndex", typeof(short));
			columns.Add("OrderVoucherID", typeof(string));
			columns.Add("OrderSysDocID", typeof(string));
			columns.Add("DNoteVoucherID", typeof(string));
			columns.Add("DNoteSysDocID", typeof(string));
			columns.Add("OrderRowIndex", typeof(int));
			columns.Add("ItemType", typeof(byte));
			columns.Add("IsDNRow", typeof(bool));
			columns.Add("IsRecost", typeof(bool));
			columns.Add("RowSource", typeof(short));
			columns.Add("JobID", typeof(string));
			columns.Add("Cost", typeof(decimal));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("SpecificationID", typeof(string));
			columns.Add("StyleID", typeof(string));
			columns.Add("ListVoucherID", typeof(string));
			columns.Add("ListSysDocID", typeof(string));
			columns.Add("ListRowIndex", typeof(short));
			columns.Add("RefSlNo", typeof(int));
			columns.Add("RefText1", typeof(string));
			columns.Add("RefText2", typeof(string));
			columns.Add("RefNum1", typeof(decimal));
			columns.Add("RefNum2", typeof(decimal));
			columns.Add("RefDate1", typeof(DateTime));
			columns.Add("RefDate2", typeof(DateTime));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Invoice_Payment");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("AccountID", typeof(string));
			columns.Add("RegisterID", typeof(string));
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("PaymentMethodType", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Invoice_DNote");
			columns = dataTable.Columns;
			columns.Add("InvoiceSysDocID", typeof(string));
			columns.Add("InvoiceVoucherID", typeof(string));
			columns.Add("DNoteSysDocID", typeof(string));
			columns.Add("DNoteVoucherID", typeof(string));
			columns.Add("SourceDocType", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Sales_Invoice_Expense");
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
			columns.Add("TaxOption", typeof(byte));
			columns.Add("TaxGroupID", typeof(string));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("RowIndex", typeof(int));
			columns.Add("IsDeduct", typeof(bool));
			base.Tables.Add(dataTable);
			Merge(new TaxTransactionData().TaxDetailTable);
			InventoryTransactionData.AddProductLotIssueDetailTable(this);
		}
	}
}
