using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class VehicleMileageTrackSystem : MarshalByRefObject, IVehicleMileageTrackSystem, IDisposable
	{
		private Config config;

		public VehicleMileageTrackSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateVehicleMileageTrack(VehicleMileageTrackData data, bool isUpdate)
		{
			return new VehicleMileageTrack(config).InsertUpdateVehicleMileageTrack(data, isUpdate);
		}

		public VehicleMileageTrackData GetVehicleMileageTrackByID(string sysDocID, string voucherID)
		{
			return new VehicleMileageTrack(config).GetVehicleMileageTrackByID(sysDocID, voucherID);
		}

		public bool DeleteVehicleMileageTrack(string voucherID)
		{
			return new VehicleMileageTrack(config).DeleteVehicleMileageTrack(voucherID);
		}

		public DataSet GetVehicleMileageTrackToPrint(string sysDocID, string voucherID)
		{
			return new VehicleMileageTrack(config).GetVehicleMileageTrackToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new VehicleMileageTrack(config).GetList(from, to, showVoid);
		}

		public DataSet GetPreviousReadingValue(string VehicleID)
		{
			return new VehicleMileageTrack(config).GetPreviousReadingValue(VehicleID);
		}
	}
}
