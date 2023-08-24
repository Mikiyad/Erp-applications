using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IVehicleDocumentSystem
	{
		bool CreateVehicleDocument(VehicleDocumentData employeeDocumentData);

		bool UpdateVehicleDocument(VehicleDocumentData employeeDocumentData);

		VehicleDocumentData GetVehicleDocument();

		bool DeleteVehicleDocument(string ID);

		VehicleDocumentData GetVehicleDocumentByID(string id);

		VehicleDocumentData GetVehicleDocumentsByVehicleID(string employeeID);

		DataSet GetVehicleDocumentByFields(params string[] columns);

		DataSet GetVehicleDocumentByFields(string[] ids, params string[] columns);

		DataSet GetVehicleDocumentByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetVehicleDocumentList();

		DataSet GetVehicleDocumentComboList();
	}
}
