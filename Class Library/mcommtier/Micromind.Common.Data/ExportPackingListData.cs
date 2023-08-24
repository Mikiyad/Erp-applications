using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ExportPackingListData : DataSet
	{
		public const string EXPORTPACKINGLIST_TABLE = "Export_PackingList";

		public const string EXPORTPACKINGLISTDETAIL_TABLE = "Export_PackingList_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string SALESFLOW_FIELD = "SalesFlow";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string STATUS_FIELD = "Status";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string CONTAINERNUMBER_FIELD = "ContainerNumber";

		public const string PORT_FIELD = "Port";

		public const string ETA_FIELD = "ETA";

		public const string BOLNUMBER_FIELD = "BOLNumber";

		public const string SHIPPER_FIELD = "Shipper";

		public const string CLEARINGAGENT_FIELD = "ClearingAgent";

		public const string WEIGHT_FIELD = "Weight";

		public const string ISRECEIVED_FIELD = "IsReceived";

		public const string VALUE_FIELD = "Value";

		public const string ISSHIPMENT_FIELD = "IsShipment";

		public const string CONTAINERSIZEID_FIELD = "ContainerSizeID";

		public const string JOBID_FIELD = "JobID";

		public const string COSTCATEGORYID_FIELD = "CostCategoryID";

		public const string PACKINGLISTTAG_FIELD = "PackingListTag";

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

		public const string LICENSE_FIELD = "License";

		public const string BALANCE_FIELD = "Balance";

		public const string TERMS_FIELD = "Terms";

		public const string TOTALPACKAGES_FIELD = "TotalPackages";

		public const string COUNTRYOFORIGIN_FIELD = "CountryofOrigin";

		public const string BOX_FIELD = "Box";

		public const string NETWEIGHT_FIELD = "NetWeight";

		public const string GROSSWEIGHT_FIELD = "GrossWeight";

		public const string LENGTH_FIELD = "Length";

		public const string WIDTH_FIELD = "Width";

		public const string HEIGHT_FIELD = "Height";

		public const string CUBICMEASURE_FIELD = "CubicMeasure";

		public const string DRIVERID_FIELD = "DriverID";

		public const string VEHICLEID_FIELD = "VehicleID";

		public const string REMARKS_FIELD = "Remarks";

		public const string SPECIFICATIONID_FIELD = "SpecificationID";

		public const string STYLEID_FIELD = "StyleID";

		public DataTable ExportPackingListTable => base.Tables["Export_PackingList"];

		public DataTable ExportPackingListDetailTable => base.Tables["Export_PackingList_Detail"];

		public ExportPackingListData()
		{
			BuildDataTables();
		}

		public ExportPackingListData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Export_PackingList");
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
			columns.Add("Status", typeof(byte)).DefaultValue = 1;
			columns.Add("SalesFlow", typeof(byte));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("ContainerNumber", typeof(string));
			columns.Add("Port", typeof(string));
			columns.Add("ETA", typeof(DateTime));
			columns.Add("BOLNumber", typeof(string));
			columns.Add("Shipper", typeof(string));
			columns.Add("ClearingAgent", typeof(string));
			columns.Add("Weight", typeof(decimal));
			columns.Add("Value", typeof(decimal));
			columns.Add("IsReceived", typeof(bool));
			columns.Add("IsShipment", typeof(bool));
			columns.Add("ContainerSizeID", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("CostCategoryID", typeof(string));
			columns.Add("PackingListTag", typeof(string));
			columns.Add("License", typeof(string));
			columns.Add("Balance", typeof(string));
			columns.Add("Terms", typeof(string));
			columns.Add("TotalPackages", typeof(short));
			columns.Add("CountryofOrigin", typeof(string));
			columns.Add("Box", typeof(string));
			columns.Add("NetWeight", typeof(decimal));
			columns.Add("GrossWeight", typeof(decimal));
			columns.Add("Length", typeof(string));
			columns.Add("Width", typeof(decimal));
			columns.Add("Height", typeof(decimal));
			columns.Add("CubicMeasure", typeof(decimal));
			columns.Add("DriverID", typeof(string));
			columns.Add("VehicleID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Export_PackingList_Detail");
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
			columns.Add("Remarks", typeof(string));
			columns.Add("SpecificationID", typeof(string));
			columns.Add("StyleID", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
