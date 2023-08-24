using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class POShipmentData : DataSet
	{
		public const string POSHIPMENT_TABLE = "PO_Shipment";

		public const string POSHIPMENTDETAIL_TABLE = "PO_Shipment_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string VENDORID_FIELD = "VendorID";

		public const string PURCHASEFLOW_FIELD = "PurchaseFlow";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string STATUS_FIELD = "Status";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string VENDORREFERENCENUMBER_FIELD = "VendorReferenceNo";

		public const string CONTAINERNUMBER_FIELD = "ContainerNumber";

		public const string CONTAINERSIZEID_FIELD = "ContainerSizeID";

		public const string ATD_FIELD = "ATD";

		public const string PORT_FIELD = "Port";

		public const string LOADINGPORT_FIELD = "LoadingPort";

		public const string ETA_FIELD = "ETA";

		public const string BOLNUMBER_FIELD = "BOLNumber";

		public const string SHIPPER_FIELD = "Shipper";

		public const string CLEARINGAGENT_FIELD = "ClearingAgent";

		public const string WEIGHT_FIELD = "Weight";

		public const string ISRECEIVED_FIELD = "IsReceived";

		public const string VALUE_FIELD = "Value";

		public const string TRANSPORTERID_FIELD = "TransporterID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string DESCRIPTION_FIELD = "Description";

		public const string UNITID_FIELD = "UnitID";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string UNITFACTOR_FIELD = "UnitFactor";

		public const string UNITPRICE_FIELD = "UnitPrice";

		public const string FACTORTYPE_FIELD = "FactorType";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string QUANTITYRECEIVED_FIELD = "QuantityReceived";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEROWINDEX_FIELD = "SourceRowIndex";

		public const string ISSOURCEDROW_FIELD = "IsSourcedRow";

		public const string SOURCEDOCTYPE_FIELD = "SourceDocType";

		public DataTable POShipmentTable => base.Tables["PO_Shipment"];

		public DataTable POShipmentDetailTable => base.Tables["PO_Shipment_Detail"];

		public POShipmentData()
		{
			BuildDataTables();
		}

		public POShipmentData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("PO_Shipment");
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
			columns.Add("DivisionID", typeof(string));
			columns.Add("CompanyID", typeof(string));
			columns.Add("VendorID", typeof(string));
			columns.Add("TransactionDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("Status", typeof(byte)).DefaultValue = 1;
			columns.Add("PurchaseFlow", typeof(byte));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("VendorReferenceNo", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("ContainerNumber", typeof(string));
			columns.Add("Port", typeof(string));
			columns.Add("LoadingPort", typeof(string));
			columns.Add("ETA", typeof(DateTime));
			columns.Add("ATD", typeof(DateTime));
			columns.Add("BOLNumber", typeof(string));
			columns.Add("Shipper", typeof(string));
			columns.Add("ClearingAgent", typeof(string));
			columns.Add("Weight", typeof(decimal));
			columns.Add("Value", typeof(decimal));
			columns.Add("IsReceived", typeof(bool));
			columns.Add("TransporterID", typeof(string));
			columns.Add("ContainerSizeID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("PO_Shipment_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("Description", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("RowIndex", typeof(short));
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("UnitFactor", typeof(decimal));
			columns.Add("UnitPrice", typeof(decimal)).DefaultValue = 0;
			columns.Add("FactorType", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceRowIndex", typeof(short));
			columns.Add("IsSourcedRow", typeof(bool));
			columns.Add("SourceDocType", typeof(byte));
			columns.Add("QuantityReceived", typeof(float));
			base.Tables.Add(dataTable);
		}
	}
}
