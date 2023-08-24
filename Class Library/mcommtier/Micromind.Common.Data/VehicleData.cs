using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class VehicleData : DataSet
	{
		public const string VEHICLE_TABLE = "Vehicle";

		public const string VEHICLEID_FIELD = "VehicleID";

		public const string VEHICLENAME_FIELD = "VehicleName";

		public const string REGISTRATIONNO_FIELD = "RegistrationNumber";

		public const string CITYID_FIELD = "RegistrationCityID";

		public const string COUNTRYID_FIELD = "CountryID";

		public const string WORKINGAREA_FIELD = "WorkingAreaID";

		public const string CHASISNO_FIELD = "ChasisNumber";

		public const string VEHICLETYPE_FIELD = "VehicleTypeID";

		public const string COLOR_FIELD = "Color";

		public const string YEAR_FIELD = "Year";

		public const string FUEL_FIELD = "Fuel";

		public const string MODEL_FIELD = "Model";

		public const string FIXEDASSET_FIELD = "FixedAssetID";

		public const string INSURANCEVENDOR_FIELD = "InsuranceVendorID";

		public const string INSPOLICYNO_FIELD = "InsurancePolicyNumber";

		public const string INSEXPIRYDATE_FIELD = "InsuranceExpiryDate";

		public const string REGEXPIRYDATE_FIELD = "RegistrationExpiryDate";

		public const string TRACKINGNO_FIELD = "TrackingNumber";

		public const string LOCATION_FIELD = "LocationID";

		public const string DIVISION_FIELD = "DivisionID";

		public const string DRIVERID_FIELD = "DriverID";

		public const string OWNEDBY_FIELD = "OwnedBy";

		public const string WEIGHTCAPACITY_FIELD = "WeightCapacity";

		public const string VEHICLEWEIGHT_FIELD = "VehicleWeight";

		public const string TRAFFICFILENO_FIELD = "TrafficFileNo";

		public const string PLATENO_FIELD = "PlateNo";

		public const string ORIGINID_FIELD = "Origin";

		public const string INACTIVE_FIELD = "IsInactive";

		public const string NOTE_FIELD = "Note";

		public const string ANALYSISID_FIELD = "AnalysisID";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public DataTable VehicleTable => base.Tables["Vehicle"];

		public VehicleData()
		{
			BuildDataTables();
		}

		public VehicleData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Vehicle");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("VehicleID", typeof(string));
			dataColumn.AllowDBNull = false;
			dataColumn.AutoIncrement = false;
			dataTable.PrimaryKey = new DataColumn[1]
			{
				dataColumn
			};
			columns.Add("VehicleName", typeof(string)).AllowDBNull = false;
			columns.Add("RegistrationNumber", typeof(string));
			columns.Add("RegistrationCityID", typeof(string));
			columns.Add("CountryID", typeof(string));
			columns.Add("WorkingAreaID", typeof(string));
			columns.Add("ChasisNumber", typeof(string));
			columns.Add("VehicleTypeID", typeof(string));
			columns.Add("Color", typeof(string));
			columns.Add("Year", typeof(short));
			columns.Add("Fuel", typeof(string));
			columns.Add("Model", typeof(string));
			columns.Add("FixedAssetID", typeof(string));
			columns.Add("InsuranceVendorID", typeof(string));
			columns.Add("InsurancePolicyNumber", typeof(string));
			columns.Add("InsuranceExpiryDate", typeof(DateTime));
			columns.Add("RegistrationExpiryDate", typeof(DateTime));
			columns.Add("TrackingNumber", typeof(string));
			columns.Add("LocationID", typeof(string));
			columns.Add("DivisionID", typeof(string));
			columns.Add("DriverID", typeof(string));
			columns.Add("OwnedBy", typeof(string));
			columns.Add("WeightCapacity", typeof(decimal));
			columns.Add("VehicleWeight", typeof(decimal));
			columns.Add("TrafficFileNo", typeof(string));
			columns.Add("PlateNo", typeof(string));
			columns.Add("Origin", typeof(string));
			columns.Add("AnalysisID", typeof(string));
			columns.Add("Note", typeof(string));
			columns.Add("IsInactive", typeof(bool)).DefaultValue = false;
			base.Tables.Add(dataTable);
		}
	}
}
