using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class W3PLGRNData : DataSet
	{
		public const string W3PL_GRN_LOCATIONID = "CON_IN";

		public const string W3PLGRN_TABLE = "W3PL_GRN";

		public const string W3PLGRNDETAIL_TABLE = "W3PL_GRN_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SALESPERSONID_FIELD = "SalespersonID";

		public const string REQUIREDDATE_FIELD = "RequiredDate";

		public const string CUSTOMERADDRESS_FIELD = "CustomerAddress";

		public const string STATUS_FIELD = "Status";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string CLOSEDATE_FIELD = "CloseDate";

		public const string CLOSENOTE_FIELD = "CloseNote";

		public const string TRANSPORTERID_FIELD = "TransporterID";

		public const string ARRIVALPORT_FIELD = "ArrivalPort";

		public const string ARRIVALDATE_FIELD = "ArrivalDate";

		public const string CONTAINERNO_FIELD = "ContainerNo";

		public const string BLNO_FIELD = "BLNo";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string QUANTITYSETTLED_FIELD = "QuantitySettled";

		public const string QUANTITYRETURNED_FIELD = "QuantityReturned";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string DESCRIPTION_FIELD = "Description";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string SUBUNITPRICE_FIELD = "SubunitPrice";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string CONSIGNLOCATIONID_FIELD = "ConsignLocationID";

		public const string INVOICEVOUCHERID_FIELD = "InvoiceVoucherID";

		public const string INVOICESYSDOCID_FIELD = "InvoiceSysDocID";

		public const string ORDERVOUCHERID_FIELD = "OrderVoucherID";

		public const string ORDERSYSDOCID_FIELD = "OrderSysDocID";

		public const string ORDERROWINDEX_FIELD = "InvoiceRowIndex";

		public const string ISNEWROW_FIELD = "IsNewRow";

		public DataTable W3PLGRNTable => base.Tables["W3PL_GRN"];

		public DataTable W3PLGRNDetailTable => base.Tables["W3PL_GRN_Detail"];

		public W3PLGRNData()
		{
			BuildDataTables();
		}

		public W3PLGRNData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("W3PL_GRN");
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
			columns.Add("SalespersonID", typeof(string));
			columns.Add("RequiredDate", typeof(DateTime));
			columns.Add("CustomerAddress", typeof(string));
			columns.Add("Status", typeof(byte));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("TransporterID", typeof(string));
			columns.Add("ArrivalPort", typeof(string));
			columns.Add("ArrivalDate", typeof(DateTime));
			columns.Add("ContainerNo", typeof(string));
			columns.Add("BLNo", typeof(string));
			columns.Add("CloseDate", typeof(DateTime));
			columns.Add("CloseNote", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("W3PL_GRN_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("QuantitySettled", typeof(float)).DefaultValue = 0;
			columns.Add("QuantityReturned", typeof(float)).DefaultValue = 0;
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("FactorType", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("ConsignLocationID", typeof(string));
			columns.Add("OrderVoucherID", typeof(string));
			columns.Add("OrderSysDocID", typeof(string));
			columns.Add("InvoiceVoucherID", typeof(string));
			columns.Add("InvoiceSysDocID", typeof(string));
			columns.Add("InvoiceRowIndex", typeof(int));
			columns.Add("SubunitPrice", typeof(decimal));
			columns.Add("RowIndex", typeof(short));
			columns.Add("IsNewRow", typeof(bool));
			base.Tables.Add(dataTable);
			InventoryTransactionData.AddProductLotReceivingDetailTable(this);
		}
	}
}
