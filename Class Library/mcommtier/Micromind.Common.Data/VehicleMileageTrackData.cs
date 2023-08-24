using System;
using System.ComponentModel;
using System.Data;
using System.Runtime.Serialization;

namespace Micromind.Common.Data
{
	[Serializable]
	[DesignerCategory("Code")]
	public class VehicleMileageTrackData : DataSet
	{
		public const string SYSDOCID_FIELD = "SysDocID";

		public const string VOUCHERID_FIELD = "VoucherID";

		public const string TRANSACTIONDATE_FIELD = "TransactionDate";

		public const string ISDATEWISE_FIELD = "IsDateWise";

		public const string NOTE_FIELD = "Note";

		public const string CREATEDBY_FIELD = "CreatedBy";

		public const string DATECREATED_FIELD = "DateCreated";

		public const string UPDATEDBY_FIELD = "UpdatedBy";

		public const string DATEUPDATED_FIELD = "DateUpdated";

		public const string TRIPDATE_FIELD = "TripDate";

		public const string TRIPTIME_FIELD = "TripTime";

		public const string VEHICLEID_FIELD = "VehicleID";

		public const string NAME_FIELD = "Name";

		public const string PURPOSE_FIELD = "Purpose";

		public const string DRIVERID_FIELD = "DriverID";

		public const string PREVIOUSREADING_FIELD = "PreviousReading";

		public const string CURRENTREADING_FIELD = "CurrentReading";

		public const string MILEAGE_FIELD = "Mileage";

		public const string REMARKS_FIELD = "Remarks";

		public const string ROWINDEX_FIELD = "RowIndex";

		public const string VEHICLEMILEAGETRACK_TABLE = "Vehicle_Mileage_Track";

		public const string VEHICLEMILEAGETRACKDETAIL_TABLE = "Vehicle_Mileage_Track_Detail";

		public DataTable VehicleMileageTrackTable => base.Tables["Vehicle_Mileage_Track"];

		public DataTable VehicleMileageTrackDetailTable => base.Tables["Vehicle_Mileage_Track_Detail"];

		public VehicleMileageTrackData()
		{
			BuildDataTables();
		}

		public VehicleMileageTrackData(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		private void BuildDataTables()
		{
			DataTable dataTable = new DataTable("Vehicle_Mileage_Track");
			DataColumnCollection columns = dataTable.Columns;
			DataColumn dataColumn = columns.Add("VoucherID", typeof(string));
			dataColumn.AllowDBNull = false;
			DataColumn dataColumn2 = columns.Add("SysDocID", typeof(string));
			dataColumn2.AllowDBNull = false;
			dataTable.PrimaryKey = new DataColumn[2]
			{
				dataColumn,
				dataColumn2
			};
			columns.Add("TransactionDate", typeof(string));
			columns.Add("IsDateWise", typeof(bool)).DefaultValue = false;
			columns.Add("Note", typeof(string));
			base.Tables.Add(dataTable);
			dataTable = new DataTable("Vehicle_Mileage_Track_Detail");
			columns = dataTable.Columns;
			columns.Add("VoucherID", typeof(string));
			columns.Add("SysDocID", typeof(string));
			columns.Add("TripDate", typeof(DateTime));
			columns.Add("TripTime", typeof(DateTime));
			columns.Add("VehicleID", typeof(string));
			columns.Add("Name", typeof(string));
			columns.Add("Purpose", typeof(string));
			columns.Add("DriverID", typeof(string));
			columns.Add("PreviousReading", typeof(long));
			columns.Add("CurrentReading", typeof(long));
			columns.Add("Mileage", typeof(long));
			columns.Add("Remarks", typeof(string));
			columns.Add("RowIndex", typeof(short));
			base.Tables.Add(dataTable);
		}
	}
}
