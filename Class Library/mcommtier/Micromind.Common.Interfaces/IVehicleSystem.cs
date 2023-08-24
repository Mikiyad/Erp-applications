using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IVehicleSystem
	{
		bool CreateVehicle(VehicleData vehicleData);

		bool UpdateVehicle(VehicleData vehicleData);

		VehicleData GetVehicle();

		bool DeleteVehicle(string ID);

		VehicleData GetVehicleByID(string id);

		DataSet GetVehicleByFields(params string[] columns);

		DataSet GetVehicleByFields(string[] ids, params string[] columns);

		DataSet GetVehicleByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetVehicleList();

		DataSet GetVehicleComboList();

		DataSet GetVehicleList(string fromVehicle, string toVehicle);

		bool AddVehiclePhoto(string employeeID, byte[] image);

		bool RemoveVehiclePhoto(string productID);

		byte[] GetVehicleThumbnailImage(string contactID);
	}
}
