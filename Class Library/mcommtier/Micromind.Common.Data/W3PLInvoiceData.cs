using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class W3PLInvoiceData : DataSet
	{
		public const string W3PLINVOICE_TABLE = "W3PL_Invoice";

		public const string W3PLINVOICEDETAIL_TABLE = "W3PL_Invoice_Detail";

		public const string W3PLEXPENSE_TABLE = "W3PL_Expense";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SALESPERSONID_FIELD = "SalespersonID";

		public const string REQUIREDDATE_FIELD = "RequiredDate";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string CUSTOMERADDRESS_FIELD = "CustomerAddress";

		public const string STATUS_FIELD = "Status";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string TERMID_FIELD = "TermID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DISCOUNT_FIELD = "Discount";

		public const string DISCOUNTFC_FIELD = "DiscountFC";

		public const string TAXAMOUNT_FIELD = "TaxAmount";

		public const string TAXAMOUNTFC_FIELD = "TaxAmountFC";

		public const string TOTAL_FIELD = "Total";

		public const string TOTALFC_FIELD = "TotalFC";

		public const string ISCASH_FIELD = "IsCash";

		public const string REGISTERID_FIELD = "RegisterID";

		public const string DUEDATE_FIELD = "DueDate";

		public const string PAYMENTMETHODTYPE_FIELD = "PaymentMethodType";

		public const string CONSIGNSYSDOCID_FIELD = "ConsignSysDocID";

		public const string CONSIGNVOUCHERID_FIELD = "ConsignVoucherID";

		public const string CONSIGNROWINDEX_FIELD = "ConsignRowIndex";

		public const string COMMISSIONPERCENT_FIELD = "CommissionPercent";

		public const string COMMISSIONAMOUNT_FIELD = "CommissionAmount";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string STOREQUANTITY_FIELD = "StoreQuantity";

		public const string DLQUANTITY_FIELD = "DLQuantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string UNITPRICEFC_FIELD = "UnitPriceFC";

		public const string UNITWEIGHT_FIELD = "UnitWeight";

		public const string TOTALWEIGHT_FIELD = "TotalWeight";

		public const string UNITEXPENSE_FIELD = "UnitExpense";

		public const string EXPENSEAMOUNT_FIELD = "ExpenseAmount";

		public const string DESCRIPTION_FIELD = "Description";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string QUANTITYSHIPPED_FIELD = "QuantityShipped";

		public const string ORDERVOUCHERID_FIELD = "OrderVoucherID";

		public const string ORDERSYSDOCID_FIELD = "OrderSysDocID";

		public const string DNOTEVOUCHERID_FIELD = "DNoteVoucherID";

		public const string DNOTESYSDOCID_FIELD = "DNoteSysDocID";

		public const string ORDERROWINDEX_FIELD = "OrderRowIndex";

		public const string ISDNROW_FIELD = "IsDNRow";

		public const string ISRECOST_FIELD = "IsRecost";

		public const string QUANTITYBALANCE_FIELD = "QuantityBalance";

		public const string STARTDATE_FIELD = "StartDate";

		public const string ENDDATE_FIELD = "EndDate";

		public const string EXPENSEID_FIELD = "ExpenseID";

		public const string EXPENSENAME_FIELD = "ExpenseName";

		public const string AMOUNT_FIELD = "Amount";

		public const string AMOUNTFC_FIELD = "AmountFC";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCEROWINDEX_FIELD = "SourceRowIndex";

		public const string W3PLINVOICESETTLEDITEMS_TABLE = "W3PLInvoice_Settled_Items";

		public const string SALESROWID_FIELD = "SalesRowID";

		public DataTable ExpenseTable => base.Tables["W3PL_Expense"];

		public DataTable W3PLInvoiceSettledItemsTable => base.Tables["W3PLInvoice_Settled_Items"];

		public DataTable W3PLInvoiceTable => base.Tables["W3PL_Invoice"];

		public DataTable W3PLInvoiceDetailTable => base.Tables["W3PL_Invoice_Detail"];

		public W3PLInvoiceData()
		{
			BuildDataTables();
		}

		public W3PLInvoiceData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("W3PL_Invoice");
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
			columns.Add("CustomerID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("DueDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("SalespersonID", typeof(string));
			columns.Add("RequiredDate", typeof(DateTime));
			columns.Add("ShippingAddressID", typeof(string));
			columns.Add("CustomerAddress", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("TermID", typeof(string));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PaymentMethodType", typeof(byte));
			columns.Add("RegisterID", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("DiscountFC", typeof(decimal));
			columns.Add("TaxAmount", typeof(decimal));
			columns.Add("TaxAmountFC", typeof(decimal));
			columns.Add("CommissionPercent", typeof(decimal));
			columns.Add("CommissionAmount", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("TotalFC", typeof(decimal));
			columns.Add("IsCash", typeof(bool));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("W3PL_Invoice_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceRowIndex", typeof(int));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("DLQuantity", typeof(float)).DefaultValue = 0;
			columns.Add("StoreQuantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Amount", typeof(decimal)).DefaultValue = 0;
			columns.Add("AmountFC", typeof(decimal)).DefaultValue = 0;
			columns.Add("UnitExpense", typeof(decimal)).DefaultValue = 0;
			columns.Add("ExpenseAmount", typeof(decimal)).DefaultValue = 0;
			columns.Add("UnitPriceFC", typeof(decimal));
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitWeight", typeof(float));
			columns.Add("TotalWeight", typeof(float));
			columns.Add("StartDate", typeof(DateTime));
			columns.Add("EndDate", typeof(DateTime));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("W3PL_Expense");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ExpenseID", typeof(string));
			columns.Add("Description", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceRowIndex", typeof(int));
			columns.Add("Amount", typeof(decimal));
			columns.Add("AmountFC", typeof(decimal));
			columns.Add("Reference", typeof(string));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("RateType", typeof(string));
			columns.Add("RowIndex", typeof(int));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("W3PLInvoice_Settled_Items");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("SalesRowID", typeof(int));
			columns.Add("ProductID", typeof(string));
			columns.Add("UnitPrice", typeof(decimal));
			columns.Add("Quantity", typeof(decimal));
			base.Tables.Add(dataTable);
		}
	}
}
