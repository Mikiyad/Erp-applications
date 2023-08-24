using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class PropertyServiceData : DataSet
	{
		public const string PROPERTYSERVICEREQUEST_TABLE = "Property_Service_Request";

		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string REPORTINGDATE_FIELD = "ReportingDate";

		public const string PROPERTYID_FIELD = "PropertyID";

		public const string UNITID_FIELD = "UnitID";

		public const string TENANTID_FIELD = "TenantID";

		public const string PRIORITYSTATUS_FIELD = "PriorityStatus";

		public const string REQUIREDDATETIME_FIELD = "RequiredDatetime";

		public const string CONVENIENTDATETIME_FIELD = "ConvenientDatetime";

		public const string REQUESTNOTES_FIELD = "RequestNotes";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string PROPERTYSERVICEFACILITYDETAIL_TABLE = "Property_ServiceFacility_Detail";

		public const string SERVICEFACILITYROWINDEX_FIELD = "RowIndex";

		public const string FACILITYID_FIELD = "FacilityID";

		public const string PROPERTYSERVICETYPEDETAIL_TABLE = "Property_ServiceType_Detail";

		public const string SERVICETYPEROWINDEX_FIELD = "RowIndex";

		public const string SERVICETYPEID_FIELD = "ServiceTypeID";

		public const string SERVICEFACILITYSYSDOCID_FIELD = "SysDocID";

		public const string SERVICEFACILITYVOUCHERID_FIELD = "VoucherID";

		public const string SERVICETYPESYSDOCID_FIELD = "SysDocID";

		public const string SERVICETYPEVOUCHERID_FIELD = "VoucherID";

		public const string PROPERTYSERVICEASSIGNDETAIL_TABLE = "Property_Service_Assign";

		public const string SERVICEASSIGNSYSDOCID_FIELD = "SysDocID";

		public const string SERVICEASSIGNVOUCHERID_FIELD = "VoucherID";

		public const string SERVICEASSIGNSOURCESYSDOCID_FIELD = "SourceSysDocID";

		public const string SERVICEASSIGNSOURCEVOUCHERID_FIELD = "SourceVoucherID";

		public const string SERVICEASSIGNSERVICEPROVIDERID_FIELD = "ServiceProviderID";

		public const string SERVICEASSIGNPLANNEDDATE_FIELD = "PlannedDate";

		public const string SERVICEASSIGNSTATUSDATE_FIELD = "StatusDate";

		public const string SERVICEASSIGNSTATUS_FIELD = "Status";

		public const string SERVICEASSIGNAMOUNT_FIELD = "Amount";

		public const string SERVICEASSIGNREMARKS_FIELD = "Remarks";

		public const string SERVICEASSIGNCREATEDBY_FIELD = "CreatedBy";

		public const string SERVICEASSIGNDATECREATED_FIELD = "DateCreated";

		public const string SERVICEASSIGNUPDATEDBY_FIELD = "UpdatedBy";

		public const string SERVICEASSIGNDATEUPDATED_FIELD = "DateUpdated";

		public DataTable PropertyServiceDetailTable => base.Tables["Property_Service_Request"];

		public DataTable PropertyServiceFacilityDetailTable => base.Tables["Property_ServiceFacility_Detail"];

		public DataTable PropertyServiceTypeDetailTable => base.Tables["Property_ServiceType_Detail"];

		public DataTable PropertyServiceAssignDetailTable => base.Tables["Property_Service_Assign"];

		public PropertyServiceData()
		{
			BuildDataTables();
		}

		public PropertyServiceData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Property_Service_Request");
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
			columns.Add("ReportingDate", typeof(DateTime)).DefaultValue = DateTime.Now;
			columns.Add("PropertyID", typeof(string));
			columns.Add("UnitID", typeof(string));
			columns.Add("TenantID", typeof(string));
			columns.Add("PriorityStatus", typeof(byte));
			columns.Add("RequiredDatetime", typeof(DateTime));
			columns.Add("ConvenientDatetime", typeof(DateTime));
			columns.Add("RequestNotes", typeof(string));
			base.Tables.Add(dataTable);
			DataTable dataTable2 = new DataTable("Property_ServiceFacility_Detail");
			columns = dataTable2.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("RowIndex", typeof(int));
			columns.Add("FacilityID", typeof(string));
			base.Tables.Add(dataTable2);
			DataTable dataTable3 = new DataTable("Property_ServiceType_Detail");
			columns = dataTable3.Columns;
			columns.Add("SysDocID", typeof(string));
			columns.Add("VoucherID", typeof(string));
			columns.Add("RowIndex", typeof(int));
			columns.Add("ServiceTypeID", typeof(string));
			base.Tables.Add(dataTable3);
			DataTable dataTable4 = new DataTable("Property_Service_Assign");
			DataColumnCollection columns2 = dataTable4.Columns;
			DataColumn dataColumn3 = columns2.Add("SysDocID", typeof(string));
			DataColumn dataColumn4 = columns2.Add("VoucherID", typeof(string));
			dataColumn3.AllowDBNull = false;
			dataColumn4.AllowDBNull = false;
			dataTable4.PrimaryKey = new DataColumn[2]
			{
				dataColumn3,
				dataColumn4
			};
			columns2.Add("SourceSysDocID", typeof(string));
			columns2.Add("SourceVoucherID", typeof(string));
			columns2.Add("ServiceProviderID", typeof(string));
			columns2.Add("PlannedDate", typeof(DateTime));
			columns2.Add("StatusDate", typeof(DateTime));
			columns2.Add("Status", typeof(byte));
			columns2.Add("Amount", typeof(decimal));
			columns2.Add("Remarks", typeof(string));
			base.Tables.Add(dataTable4);
		}
	}
}
