using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class BillOfLadingData : DataSet
	{
		public const string BILLOFLADING_TABLE = "Bill_Of_Lading";

		public const string BILLOFLADINGDETAIL_TABLE = "Bill_Of_Lading_Detail";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string DIVISIONID_FIELD = "DivisionID";

		public const string COMPANYID_FIELD = "CompanyID";

		public const string VENDORID_FIELD = "VendorID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string STATUS_FIELD = "Status";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string BOLNUMBER_FIELD = "BOLNumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string VENDORREFERENCENUMBER_FIELD = "VendorReferenceNo";

		public const string PURCHASEFLOW_FIELD = "PurchaseFlow";

		public const string ATD_FIELD = "ATD";

		public const string PORT_FIELD = "Port";

		public const string LOADINGPORT_FIELD = "LoadingPort";

		public const string ETA_FIELD = "ETA";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SHIPPER_FIELD = "Shipper";

		public const string CLEARINGAGENT_FIELD = "ClearingAgent";

		public const string VALUE_FIELD = "Value";

		public const string WEIGHT_FIELD = "Weight";

		public const string QUANTITY_FIELD = "Quantity";

		public const string UNITQUANTITY_FIELD = "UnitQuantity";

		public const string TRANSPORTERID_FIELD = "TransporterID";

		public const string CONTAINERNUMBER_FIELD = "ContainerNumber";

		public const string CONTAINERSIZEID_FIELD = "ContainerSizeID";

		public const string TYPE_FIELD = "Type";

		public const string REMARKS_FIELD = "Remarks";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable BillofLadingTable => base.Tables["Bill_Of_Lading"];

		public DataTable BillOfLadingDetailTable => base.Tables["Bill_Of_Lading_Detail"];

		public BillOfLadingData()
		{
			BuildDataTables();
		}

		public BillOfLadingData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Bill_Of_Lading");
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
			columns.Add("Note", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("BOLNumber", typeof(string));
			columns.Add("PurchaseFlow", typeof(byte));
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("VendorReferenceNo", typeof(string));
			columns.Add("Port", typeof(string));
			columns.Add("LoadingPort", typeof(string));
			columns.Add("ETA", typeof(DateTime));
			columns.Add("ATD", typeof(DateTime));
			columns.Add("Shipper", typeof(string));
			columns.Add("ClearingAgent", typeof(string));
			columns.Add("Value", typeof(decimal));
			columns.Add("TransporterID", typeof(string));
			columns.Add("SourceSysDocID", typeof(string));
			columns.Add("SourceVoucherID", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Bill_Of_Lading_Detail");
			columns = dataTable.Columns;
			dataColumn = columns.Add("SysDocID", typeof(string));
			dataColumn2 = columns.Add("VoucherID", typeof(string));
			columns.Add("ContainerNumber", typeof(string));
			columns.Add("ContainerSizeID", typeof(string));
			columns.Add("Quantity", typeof(float)).DefaultValue = 0;
			columns.Add("UnitQuantity", typeof(float));
			columns.Add("Weight", typeof(decimal));
			columns.Add("Type", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("RowIndex", typeof(string));
			base.Tables.Add(dataTable);
		}
	}
}
