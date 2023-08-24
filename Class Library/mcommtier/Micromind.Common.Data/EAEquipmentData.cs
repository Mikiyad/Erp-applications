using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class EAEquipmentData : DataSet
	{
		public const string EA_EQUIPMENT_TABLE = "EA_Equipment";

		public const string EQUIPMENTID_FIELD = "EquipmentID";

		public const string DESCRIPTION_FIELD = "Description";

		public const string REGISTRATIONNO_FIELD = "RegistrationNumber";

		public const string JOBID_FIELD = "JobID";

		public const string LOCATIONID_FIELD = "LocationID";

		public const string EQUIPMENTCATEGORYID_FIELD = "EquipmentCategoryID";

		public const string EQUIPMENTTYPEID_FIELD = "EquipmentTypeID";

		public const string EXPIRYDATE_FIELD = "ExpiryDate";

		public const string OWNERSHIP_FIELD = "OwnerShip";

		public const string VENDORID_FIELD = "VendorID";

		public const string PARENTEQUIPMENTID_FIELD = "ParentEquipmentID";

		public const string MODEL_FIELD = "Model";

		public const string COLOR_FIELD = "Color";

		public const string YEAR_FIELD = "Year";

		public const string FUEL_FIELD = "Fuel";

		public const string CAPACITY_FIELD = "Capacity";

		public const string CAPACITYTYPE_FIELD = "CapacityType";

		public const string POWER_FIELD = "Power";

		public const string SERIALNO_FIELD = "SerialNo";

		public const string PLATENO_FIELD = "PlateNo";

		public const string TRACKINGID_FIELD = "TrackingID";

		public const string ENGINENUMBER_FIELD = "EngineNumber";

		public const string OWNEDBY_FIELD = "OwnedBy";

		public const string MAINTENANCEINCHARGE_FIELD = "MaintenanceInCharge";

		public const string NOTIFICATIONEMAIL_FIELD = "NotificationEmail";

		public const string FIXEDASSETGROUPID_FIELD = "FixedAssetGroupID";

		public const string FIXEDASSETID_FIELD = "FixedAssetID";

		public const string ISMETER_FIELD = "IsMeter";

		public const string ISMILEAGE_FIELD = "IsMileage";

		public const string ISHOURS_FIELD = "IsHours";

		public const string INACTIVE_FIELD = "IsInactive";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable EA_EquipmentTable => base.Tables["EA_Equipment"];

		public EAEquipmentData()
		{
			BuildDataTables();
		}

		public EAEquipmentData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("EA_Equipment");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("EquipmentID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("Description", typeof(string)).AllowDBNull = false;
			columns.Add("RegistrationNumber", typeof(string));
			columns.Add("JobID", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("EquipmentCategoryID", typeof(string));
			columns.Add("EquipmentTypeID", typeof(string));
			columns.Add("ExpiryDate", typeof(DateTime));
			columns.Add("OwnerShip", typeof(string));
			columns.Add("VendorID", typeof(string));
			columns.Add("ParentEquipmentID", typeof(string));
			columns.Add("Model", typeof(string));
			columns.Add("Color", typeof(string));
			columns.Add("Year", typeof(short));
			columns.Add("Fuel", typeof(string));
			columns.Add("Capacity", typeof(string));
			columns.Add("CapacityType", typeof(string));
			columns.Add("Power", typeof(string));
			columns.Add("SerialNo", typeof(string));
			columns.Add("PlateNo", typeof(string));
			columns.Add("TrackingID", typeof(string));
			columns.Add("EngineNumber", typeof(string));
			columns.Add("IsMeter", typeof(bool));
			columns.Add("IsMileage", typeof(bool));
			columns.Add("IsHours", typeof(bool));
			columns.Add("OwnedBy", typeof(string));
			columns.Add("MaintenanceInCharge", typeof(string));
			columns.Add("NotificationEmail", typeof(string));
			columns.Add("FixedAssetGroupID", typeof(string));
			columns.Add("FixedAssetID", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
