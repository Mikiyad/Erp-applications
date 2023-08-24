using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IVehicleMileageTrackSystem
	{
		bool CreateVehicleMileageTrack(VehicleMileageTrackData vehiclemileagetrackData, bool isUpdate);

		VehicleMileageTrackData GetVehicleMileageTrackByID(string sysDocID, string voucherID);

		DataSet GetVehicleMileageTrackToPrint(string sysDocID, string voucherID);

		bool DeleteVehicleMileageTrack(string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetPreviousReadingValue(string VehicleID);
	}
}
