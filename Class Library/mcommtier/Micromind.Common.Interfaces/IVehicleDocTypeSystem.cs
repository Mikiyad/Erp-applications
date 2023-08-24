using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IVehicleDocTypeSystem
	{
		bool CreateVehicleDocType(VehicleDocTypeData typeData);

		bool UpdateVehicleDocType(VehicleDocTypeData typeData);

		VehicleDocTypeData GetVehicleDocType();

		bool DeleteVehicleDocType(string ID);

		VehicleDocTypeData GetVehicleDocTypeByID(string id);

		DataSet GetVehicleDocTypeByFields(params string[] columns);

		DataSet GetVehicleDocTypeByFields(string[] ids, params string[] columns);

		DataSet GetVehicleDocTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetVehicleDocTypeList();

		DataSet GetVehicleDocTypeComboList();
	}
}
