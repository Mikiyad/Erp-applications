using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class ServiceCallTrackData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string JOBID_FIELD = "JobID";

		public const string CUSTOMERID_FIELD = "CustomerID";

		public const string SHIPPINGADDRESSID_FIELD = "ShippingAddressID";

		public const string CUSTOMERADDRESS_FIELD = "CustomerAddress";

		public const string CONTACTNAME_FIELD = "ContactName";

		public const string CONTACTNO_FIELD = "ContactNo";

		public const string LOCATION_FIELD = "Location";

		public const string REQRECEIVEDDATE_FIELD = "ReqReceivedDate";

		public const string REQRECEIVEDTIME_FIELD = "ReqReceivedTime";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string SERVICEEMPLOYEEID_FIELD = "ServiceEmployeeID";

		public const string SERVICEASSIGNDATE_FIELD = "ServiceAssignDate";

		public const string SERVICEASSIGNTIME_FIELD = "ServiceAssignTime";

		public const string SERVICEUNDER_FIELD = "ServiceUnder";

		public const string SERVICESTARTDATE_FIELD = "ServiceStartDate";

		public const string SERVICESTARTTIME_FIELD = "ServiceStartTime";

		public const string SERVICEFINISHEDDATE_FIELD = "ServiceFinishedDate";

		public const string SERVICEFINISHEDTIME_FIELD = "ServiceFinishedTime";

		public const string TRAVELHOURS_FIELD = "TravelHours";

		public const string TRAVELMINS_FIELD = "TravelMins";

		public const string LABOURHOURS_FIELD = "LabourHours";

		public const string LABOURMINS_FIELD = "LabourMins";

		public const string SERVICEHOURS_FIELD = "ServiceHours";

		public const string SERVICETOTAL_FIELD = "ServiceTotal";

		public const string PARTSTOTAL_FIELD = "PartsTotal";

		public const string LABOURTOTAL_FIELD = "LabourTotal";

		public const string TRAVELTOTAL_FIELD = "TravelTotal";

		public const string TOTALCHARGES_FIELD = "TotalCharges";

		public const string REPAIRDETAILS_FIELD = "RepairDetails";

		public const string STATUS_FIELD = "Status";

		public const string ISVOID_FIELD = "IsVoid";

		public const string SERVICECALLTRACK_TABLE = "Service_CallTrack";

		public const string SERVICECLIENTASSETDETAIL_TABLE = "Service_ClientAsset_Detail";

		public const string SERVICEPARTSREPLACEDDETAIL_TABLE = "Service_PartsReplaced_Detail";

		public const string CLIENTASSETID_FIELD = "ClientAssetID";

		public const string SERIALNO_FIELD = "SerialNo";

		public const string PROBLEMDESCRIPTION_FIELD = "ProblemDescription";

		public const string PRODUCTID_FIELD = "ProductID";

		public const string QUANTITY_FIELD = "Quantity";

		public const string DESCRIPTION_FIELD = "Description";

		public const string CHARGEABLESTATUS_FIELD = "ChargeableStatus";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable ServiceCallTrackTable => base.Tables["Service_CallTrack"];

		public DataTable ServiceClientAssetDetail => base.Tables["Service_ClientAsset_Detail"];

		public DataTable ServicePartsReplacedDetail => base.Tables["Service_PartsReplaced_Detail"];

		public ServiceCallTrackData()
		{
			BuildDataTables();
		}

		public ServiceCallTrackData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Service_CallTrack");
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
			columns.Add("TransactionDate", typeof(DateTime));
			columns.Add("JobID", typeof(string));
			columns.Add("CustomerID", typeof(string));
			columns.Add("ShippingAddressID", typeof(string));
			columns.Add("CustomerAddress", typeof(string));
			columns.Add("ContactName", typeof(string));
			columns.Add("ContactNo", typeof(string));
			columns.Add("Location", typeof(string));
			columns.Add("ReqReceivedDate", typeof(DateTime));
			columns.Add("ReqReceivedTime", typeof(TimeSpan));
			columns.Add("ServiceEmployeeID", typeof(string));
			columns.Add("ServiceAssignDate", typeof(DateTime));
			columns.Add("ServiceAssignTime", typeof(TimeSpan));
			columns.Add("ServiceUnder", typeof(string));
			columns.Add("ServiceStartDate", typeof(DateTime));
			columns.Add("ServiceStartTime", typeof(TimeSpan));
			columns.Add("ServiceFinishedDate", typeof(DateTime));
			columns.Add("ServiceFinishedTime", typeof(TimeSpan));
			columns.Add("TravelHours", typeof(int));
			columns.Add("TravelMins", typeof(int));
			columns.Add("LabourHours", typeof(int));
			columns.Add("LabourMins", typeof(int));
			columns.Add("ServiceHours", typeof(int));
			columns.Add("ServiceTotal", typeof(decimal));
			columns.Add("PartsTotal", typeof(decimal));
			columns.Add("LabourTotal", typeof(decimal));
			columns.Add("TravelTotal", typeof(decimal));
			columns.Add("TotalCharges", typeof(decimal));
			columns.Add("RepairDetails", typeof(string));
			columns.Add("Status", typeof(int));
			columns.Add("IsVoid", typeof(byte));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Service_ClientAsset_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ClientAssetID", typeof(string));
			columns.Add("SerialNo", typeof(string));
			columns.Add("ProblemDescription", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Service_PartsReplaced_Detail");
			columns = dataTable.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("ProductID", typeof(string));
			columns.Add("Quantity", typeof(int));
			columns.Add("Description", typeof(string));
			columns.Add("ChargeableStatus", typeof(byte));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
