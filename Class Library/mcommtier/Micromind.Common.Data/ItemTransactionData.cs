using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ItemTransactionData : DataSet
	{
		public const string ITEMTRANSACTION_TABLE = "Item_Transaction";

		public const string ITEMTRANSACTIONDETAIL_TABLE = "Item_Transaction_Detail";

		public const string INVOICEDNOTE_TABLE = "Invoice_DNote";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string SYSDOCTYPE_FIELD = "SysDocType";

		public const string PARTYTYPE_FIELD = "PartyType";

		public const string PARTYTID_FIELD = "PartyID";

		public const string SALESFLOW_FIELD = "SalesFlow";

		public const string ISEXPORT_FIELD = "IsExport";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SALESPERSONID_FIELD = "SalespersonID";

		public const string REQUIREDDATE_FIELD = "RequiredDate";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string BILLINGADDRESSID_FIELD = "BillingAddressID";

		public const string SHIPTOADDRESS_FIELD = "ShipToAddress";

		public const string CUSTOMERADDRESS_FIELD = "CustomerAddress";

		public const string STATUS_FIELD = "Status";

		public const string CURRENCYID_FIELD = "CurrencyID";

		public const string TERMID_FIELD = "TermID";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string REFERENCE2_FIELD = "Reference2";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string DISCOUNT_FIELD = "Discount";

		public const string TOTAL_FIELD = "Total";

		public const string SOURCEDOCTYPE_FIELD = "SourceDocType";

		public const string CLUSERID_FIELD = "CLUserID";

		public const string PORT_FIELD = "Port";

		public const string INVOICESYSDOCID_FIELD = "InvoiceSysDocID";

		public const string INVOICEVOUCHERID_FIELD = "InvoiceVoucherID";

		public const string DNOTESYSDOCID_FIELD = "DNoteSysDocID";

		public const string DNOTEVOUCHERID_FIELD = "DNoteVoucherID";

		public const string DRIVERID_FIELD = "DriverID";

		public const string VEHICLEID_FIELD = "VehicleID";

		public const string CONTAINERNO_FIELD = "ContainerNumber";

		public const string CONTAINERSIZEID_FIELD = "ContainerSizeID";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string DESCRIPTION_FIELD = "Description";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string QUANTITYRETURNED_FIELD = "QuantityReturned";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string ROWSOURCE_FIELD = "RowSource";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEROWINDEX_FIELD = "SourceRowIndex";

		public DataTable ItemTransactionTable => base.Tables["Item_Transaction"];

		public DataTable ItemTransactionDetailTable => base.Tables["Item_Transaction_Detail"];

		public DataTable InvoiceDNoteTable => base.Tables["Invoice_DNote"];

		public ItemTransactionData()
		{
			BuildDataTables();
		}

		public ItemTransactionData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Item_Transaction");
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
			columns.Add("PartyID", typeof(string));
			columns.Add("PartyType", typeof(string));
			columns.Add("SysDocType", typeof(short));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("SalespersonID", typeof(string));
			columns.Add("SalesFlow", typeof(byte));
			columns.Add("RequiredDate", typeof(DateTime));
			columns.Add("IsExport", typeof(bool));
			columns.Add("ShippingAddressID", typeof(string));
			columns.Add("CustomerAddress", typeof(string));
			columns.Add("BillingAddressID", typeof(string));
			columns.Add("ShipToAddress", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("CurrencyID", typeof(string));
			columns.Add("TermID", typeof(string));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("CLUserID", typeof(string));
			columns.Add("Port", typeof(string));
			columns.Add("Reference2", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("Discount", typeof(decimal));
			columns.Add("Total", typeof(decimal));
			columns.Add("SourceDocType", typeof(byte));
			columns.Add("DriverID", typeof(string));
			columns.Add("VehicleID", typeof(string));
			columns.Add("ContainerNumber", typeof(string));
			columns.Add("ContainerSizeID", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("LocationID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Item_Transaction_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceRowIndex", typeof(int));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("RowSource", typeof(short));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Invoice_DNote");
			columns = dataTable.Columns;
			columns.Add("InvoiceSysDocID", typeof(string));
			columns.Add("InvoiceVoucherID", typeof(string));
			columns.Add("DNoteSysDocID", typeof(string));
			columns.Add("DNoteVoucherID", typeof(string));
			columns.Add("SourceDocType", typeof(byte));
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotIssueDetailTable(this);
		}
	}
}
