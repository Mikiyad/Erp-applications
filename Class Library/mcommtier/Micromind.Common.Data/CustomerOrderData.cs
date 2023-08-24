using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class CustomerOrderData : DataSet
	{
		public const string ORDERID_FIELD = "OrderID";

		public const string NUMBER_FIELD = "Number";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string ORDERDATE_FIELD = "OrderDate";

		public const string EMPLOYEEID_FIELD = "EmployeeID";

		public const string SALESFLOW_FIELD = "SalesFlow";

		public const string EMPLOYEENAME_FIELD = "SalesPersonName";

		public const string SHIPVIA_FIELD = "ShipVia";

		public const string FREIGHT_FIELD = "Freight";

		public const string DISCOUNTAMOUNT_FIELD = "DiscountAmount";

		public const string OTHERCHARGES_FIELD = "OtherCharges";

		public const string SUBTOTAL_FIELD = "SubTotal";

		public const string PAYMENTTERMID_FIELD = "TermID";

		public const string REFERENCENUMBER_FIELD = "ReferenceNumber";

		public const string STOREID_FIELD = "StoreID";

		public const string REQUIREDDATE_FIELD = "RequiredDate";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string SHIPPINGADDRESSNAME_FIELD = "ShippingAddressName";

		public const string BILLINGADDRESSID_FIELD = "BillingAddressID";

		public const string BILLINGADDRESSNAME_FIELD = "BillingAddressName";

		public const string STATUS_FIELD = "Status";

		public const string TYPE_FIELD = "Type";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string CURRENCYNAME_FIELD = "CurrencyName";

		public const string CURRENCYRATE_FIELD = "CurrencyRate";

		public const string DESCRIPTION_FIELD = "Description";

		public const string ISACTIVE_FIELD = "IsActive";

		public const string RECEIVEDBY_FIELD = "ReceivedBy";

		public const string DELIVEREDBY_FIELD = "DeliveredBy";

		public const string DATETIMESTAMP_FIELD = "DateTimeStamp";

		public const string JOBID_FIELD = "JobID";

		public const string NOTEID_FIELD = "NoteID";

		public const string JOBNAME_FIELD = "JobName";

		public const string INVOICENUMBER_FIELD = "InvoiceNumber";

		public const string CUSTOMERORDERS_TABLE = "[Customer Orders]";

		public const string ORDERDETAILSID_FIELD = "OrderDetailsID";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string PACKAGENUMBER_FIELD = "PackageNumber";

		public const string UNITSRESERVED_FIELD = "UnitsReserved";

		public const string ORDERDETAILS_TABLE = "[Customer Order Details]";

		public DataTable CustomerOrderTable => base.Tables["[Customer Orders]"];

		public DataTable CustomerOrderDetailTable => base.Tables["[Customer Order Details]"];

		public CustomerOrderData()
		{
			BuildDataTables();
		}

		public CustomerOrderData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("[Customer Orders]");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("OrderID", typeof(int));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = true;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Number", typeof(string));
			columns.Add("CustomerID", typeof(int));
			columns.Add("OrderDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("EmployeeID", typeof(int)).DefaultValue = DBNull.Value;
			columns.Add("ShipVia", typeof(int)).DefaultValue = DBNull.Value;
			columns.Add("Freight", typeof(decimal)).DefaultValue = 0;
			columns.Add("DiscountAmount", typeof(decimal)).DefaultValue = 0;
			columns.Add("SubTotal", typeof(decimal)).DefaultValue = 0;
			columns.Add("OtherCharges", typeof(decimal)).DefaultValue = 0;
			columns.Add("StoreID", typeof(int)).DefaultValue = DBNull.Value;
			columns.Add("RequiredDate", typeof(DateTime)).DefaultValue = DBNull.Value;
			columns.Add("Status", typeof(byte)).DefaultValue = (byte)1;
			columns.Add("Type", typeof(byte)).DefaultValue = (byte)1;
			columns.Add("JobID", typeof(int));
			columns.Add("TermID", typeof(byte));
			columns.Add("ReferenceNumber", typeof(string));
			columns.Add("ShippingAddressID", typeof(int)).AllowDBNull = true;
			columns.Add("BillingAddressID", typeof(int)).AllowDBNull = true;
			columns.Add("Description", typeof(string)).DefaultValue = DBNull.Value;
			columns.Add("CurrencyID", typeof(int));
			columns.Add("CurrencyRate", typeof(decimal));
			columns.Add("IsActive", typeof(bool)).DefaultValue = true;
			columns.Add("ReceivedBy", typeof(int));
			columns.Add("DeliveredBy", typeof(int));
			columns.Add("InvoiceNumber", typeof(string));
			columns.Add("DateTimeStamp", typeof(DateTime)).DefaultValue = DateTime.Now.ToString();
			columns.Add("NoteID", typeof(int));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("[Customer Order Details]");
			columns = dataTable.Columns;
			dataColumn = columns.Add("OrderDetailsID", typeof(int));
			columns.Add("OrderID", typeof(int)).AllowDBNull = true;
			columns.Add("ProductID", typeof(int)).AllowDBNull = false;
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("SubunitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("UnitID", typeof(short)).DefaultValue = DBNull.Value;
			columns.Add("UnitQuantity", typeof(float)).DefaultValue = DBNull.Value;
			columns.Add("PackageNumber", typeof(string));
			columns.Add("UnitsReserved", typeof(float)).DefaultValue = DBNull.Value;
			columns.Add("Description", typeof(string)).DefaultValue = DBNull.Value;
			columns.Add("StoreID", typeof(int)).DefaultValue = DBNull.Value;
			base.Tables.Add(dataTable);
		}
	}
}
