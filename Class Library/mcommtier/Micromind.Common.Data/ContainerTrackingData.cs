using System;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	public class ContainerTrackingData : DataSet
	{
		public const string CONTAINERTRACKING_TABLE = "Container_Tracking";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string VENDORID_FIELD = "VendorID";

		public const string PURCHASEFLOW_FIELD = "PurchaseFlow";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string STATUS_FIELD = "Status";

		public const string SHIPPINGMETHODID_FIELD = "ShippingMethodID";

		public const string REFERENCE_FIELD = "Reference";

		public const string NOTE_FIELD = "Note";

		public const string PONUMBER_FIELD = "PONumber";

		public const string ISVOID_FIELD = "IsVoid";

		public const string CONTAINERNUMBER_FIELD = "ContainerNumber";

		public const string CONTAINERSIZEID_FIELD = "ContainerSizeID";

		public const string ATD_FIELD = "ATD";

		public const string PORT_FIELD = "DestinationPort";

		public const string LOADINGPORT_FIELD = "LoadingPort";

		public const string ETA_FIELD = "ETA";

		public const string BOLNUMBER_FIELD = "BOLNumber";

		public const string SHIPPER_FIELD = "Shipper";

		public const string DELIVERYFREETIME_FIELD = "FreeTimeTODeliver";

		public const string DELIVERYFREEDATE_FIELD = "DeliveryDate";

		public const string WEIGHT_FIELD = "Weight";

		public const string ISRECEIVED_FIELD = "IsReceived";

		public const string TRUCKNUMBER_FIELD = "TruckNumber";

		public const string TRANSPORTCOMPANY_FIELD = "TransportCompany";

		public const string ISBL_FIELD = "IsBL";

		public const string ISINVOICE_FIELD = "IsInvoice";

		public const string ISPL_FIELD = "IsPL";

		public const string ISHEALTHCERTIFICATE_FIELD = "IsHealthCertficate";

		public const string ISCERTIFICATEOFORIGIN_FIELD = "IsCertificateOfOrigin";

		public const string TRANSPORTERID_FIELD = "TransporterID";

		public const string DRIVERID_FIELD = "DriverID";

		public const string SOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string CONTAINERSTATUS_FIELD = "Container_Status";

		public const string REMARKS_FIELD = "Remarks";

		public const string ACTIVITYDONEBY_FIELD = "ActivityDoneBy";

		public const string ACTIVITYTIME_FIELD = "DeliveryTime";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ContainerTrackingTable => base.Tables["Container_Tracking"];

		public ContainerTrackingData()
		{
			BuildDataTables();
		}

		public ContainerTrackingData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Container_Tracking");
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
			columns.Add("Status", typeof(byte)).DefaultValue = 1;
			columns.Add("ShippingMethodID", typeof(string));
			columns.Add("Reference", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("PONumber", typeof(string));
			columns.Add("IsVoid", typeof(bool));
			columns.Add("ContainerNumber", typeof(string));
			columns.Add("DestinationPort", typeof(string));
			columns.Add("LoadingPort", typeof(string));
			columns.Add("ETA", typeof(DateTime));
			columns.Add("ATD", typeof(DateTime));
			columns.Add("BOLNumber", typeof(string));
			columns.Add("Shipper", typeof(string));
			columns.Add("FreeTimeTODeliver", typeof(DateTime));
			columns.Add("DeliveryDate", typeof(DateTime));
			columns.Add("TransportCompany", typeof(string));
			columns.Add("DriverID", typeof(string));
			columns.Add("Weight", typeof(decimal));
			columns.Add("IsReceived", typeof(bool));
			columns.Add("TransporterID", typeof(string));
			columns.Add("Container_Status", typeof(string));
			columns.Add("ActivityDoneBy", typeof(string));
			columns.Add("Remarks", typeof(string));
			columns.Add("ContainerSizeID", typeof(string));
			columns.Add("DeliveryTime", typeof(TimeSpan));
			columns.Add("TruckNumber", typeof(string));
			columns.Add("IsBL", typeof(bool));
			columns.Add("IsInvoice", typeof(bool));
			columns.Add("IsPL", typeof(bool));
			columns.Add("IsHealthCertficate", typeof(bool));
			columns.Add("IsCertificateOfOrigin", typeof(bool));
			base.Tables.Add(dataTable);
		}
	}
}
