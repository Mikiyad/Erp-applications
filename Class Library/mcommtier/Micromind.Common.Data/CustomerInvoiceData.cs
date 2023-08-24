using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CustomerInvoiceData : DataSet
	{
		public const string INVOICEID_FIELD = "InvoiceID";

		public const string NUMBER_FIELD = "Number";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string INVOICEDATE_FIELD = "InvoiceDate";

		public const string DUEDATE_FIELD = "DueDate";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string SALESFLOW_FIELD = "SalesFlow";

		public const string EMPLOYEENAME_FIELD = "SalesPersonName";

		public const string SHIPVIA_FIELD = "ShipVia";

		public const string DISCOUNTAMOUNT_FIELD = "DiscountAmount";

		public const string DISCOUNTACCOUNT_FIELD = "DiscountAccount";

		public const string FREIGHT_FIELD = "Freight";

		public const string FREIGHTACCOUNT_FIELD = "FreightAccount";

		public const string OTHERCHARGES_FIELD = "OtherCharges";

		public const string SUBTOTAL_FIELD = "SubTotal";

		public const string COGS_FIELD = "COGS";

		public const string STOREID_FIELD = "StoreID";

		public const string REQUIREDDATE_FIELD = "RequiredDate";

		public const string SHIPMENTDATE_FIELD = "ShipmentDate";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string SHIPPINGADDRESSNAME_FIELD = "ShippingAddressName";

		public const string BILLINGADDRESSID_FIELD = "BillingAddressID";

		public const string BILLINGADDRESSNAME_FIELD = "BillingAddressName";

		public const string BALANCE_FIELD = "Balance";

		public const string PONUMBER_FIELD = "PONumber";

		public const string STATUS_FIELD = "Status";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYNAME_FIELD = "CurrencyName";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string ARACCOUNTID_FIELD = "ARAccountID";

		public const string ARACCOUNTNAME_FIELD = "ARAccountName";

		public const string DESCRIPTION_FIELD = "Description";

		public const string INVOICETYPE_FIELD = "InvoiceType";

		public const string PAYMENTTERMID_FIELD = "TermID";

		public const string ISOPENINGBALANCE_FIELD = "IsOpeningBalance";

		public const string ISCASHINVOICE_FIELD = "IsCashInvoice";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string COMMISSIONRATE_FIELD = "CommissionRate";

		public const string PURCHASEINVOICEID_FIELD = "PurchaseInvoiceID";

		public const string SUPPLIERID_FIELD = "SupplierID";

		public const string JOBID_FIELD = "JobID";

		public const string NOTEID_FIELD = "NoteID";

		public const string JOBNAME_FIELD = "JobName";

		public const string CUSTOMERINVOICES_TABLE = "[Customer Invoices]";

		public const string INVOICEDETAILSID_FIELD = "InvoiceDetailsID";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string QTYRESERVEDUSED_FIELD = "QtyReservedUsed";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string ISKIT_FIELD = "IsKit";

		public const string ORDERNUMBER_FIELD = "OrderNumber";

		public const string PURCHASEINVOICEDETAILSID_FIELD = "PurchaseInvoiceDetailsID";

		public const string INVOICEDETAILS_TABLE = "[Customer Invoice Details]";

		public const string ORDERDETAILSID_FIELD = "OrderDetailsID";

		public const string INVOICESTORE_REL = "InvoiceStoreRelation";

		public const string INVOICEPARTNER_REL = "InvoicePartnerRelation";

		public const string INVOICETYPES_TABLE = "[Invoice Types]";

		public const string INVOICETYPEID_FIELD = "TypeID";

		public const string INVOICETYPEDESCRIPTION_FIELD = "Description";

		public const string REFERENCE_FIELD = "InvoiceReference";

		public DataTable CustomerInvoiceTable => base.Tables["[Customer Invoices]"];

		public DataTable CustomerInvoiceDetailTable => base.Tables["[Customer Invoice Details]"];

		public CustomerInvoiceData()
		{
			BuildDataTables();
		}

		public CustomerInvoiceData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Customer Invoices]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("InvoiceID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Number", typeof(string));
			columns.Add("CustomerID", typeof(int));
			columns.Add("InvoiceDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("DueDate", typeof(DateTime));
			columns.Add("PONumber", typeof(string));
			columns.Add("EmployeeID", typeof(int)).DefaultValue = DBNull.Value;
			columns.Add("ShipVia", typeof(int)).DefaultValue = DBNull.Value;
			columns.Add("Freight", typeof(decimal)).DefaultValue = 0;
			columns.Add("Balance", typeof(decimal)).DefaultValue = 0;
			columns.Add("DiscountAmount", typeof(decimal)).DefaultValue = 0;
			columns.Add("DiscountAccount", typeof(int));
			columns.Add("FreightAccount", typeof(int));
			columns.Add("OtherCharges", typeof(decimal)).DefaultValue = 0;
			columns.Add("SubTotal", typeof(decimal)).DefaultValue = 0;
			columns.Add("COGS", typeof(decimal)).DefaultValue = 0;
			columns.Add("CommissionRate", typeof(decimal));
			columns.Add("PurchaseInvoiceID", typeof(int));
			columns.Add("SupplierID", typeof(int));
			columns.Add("StoreID", typeof(int)).DefaultValue = DBNull.Value;
			columns.Add("RequiredDate", typeof(DateTime)).DefaultValue = DBNull.Value;
			columns.Add("ShipmentDate", typeof(DateTime)).DefaultValue = DBNull.Value;
			columns.Add("Status", typeof(byte)).DefaultValue = (byte)1;
			columns.Add("ShippingAddressID", typeof(int)).AllowDBNull = true;
			columns.Add("BillingAddressID", typeof(int)).AllowDBNull = true;
			columns.Add("JobID", typeof(int));
			columns.Add("Description", typeof(string)).DefaultValue = DBNull.Value;
			columns.Add("InvoiceType", typeof(byte)).AllowDBNull = false;
			columns.Add("CurrencyID", typeof(int));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("ARAccountID", typeof(int));
			columns.Add("TermID", typeof(byte));
			columns.Add("InvoiceReference", typeof(string));
			columns.Add("IsOpeningBalance", typeof(bool)).DefaultValue = false;
			columns.Add("IsCashInvoice", typeof(bool)).DefaultValue = false;
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			columns.Add("NoteID", typeof(int));
			columns.Add("StoreName", typeof(string));
			columns.Add("JobName", typeof(string));
			columns.Add("EmployeeID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("[Customer Invoice Details]");
			columns = dataTable.Columns;
			dataColumn = columns.Add("InvoiceDetailsID", typeof(int));
			columns.Add("InvoiceID", typeof(int)).AllowDBNull = true;
			columns.Add("ProductID", typeof(int)).AllowDBNull = false;
			columns.Add("PurchaseInvoiceDetailsID", typeof(int)).AllowDBNull = true;
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("SubunitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("IsKit", typeof(bool)).DefaultValue = false;
			columns.Add("OrderNumber", typeof(string));
			columns.Add("OrderDetailsID", typeof(int));
			columns.Add("QtyReservedUsed", typeof(float)).DefaultValue = DBNull.Value;
			columns.Add("UnitID", typeof(short)).DefaultValue = DBNull.Value;
			columns.Add("UnitQuantity", typeof(float)).DefaultValue = DBNull.Value;
			columns.Add("ProductID", typeof(string));
			columns.Add("COGS", typeof(decimal));
			columns.Add("Description", typeof(string)).DefaultValue = DBNull.Value;
			columns.Add("StoreID", typeof(int)).DefaultValue = DBNull.Value;
			base.Tables.Add(dataTable);
		}
	}
}
