using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IInventoryTransferTypeSystem
	{
		bool CreateInventoryTransferType(InventoryTransferTypeData inventoryTransferTypeData);

		bool UpdateInventoryTransferType(InventoryTransferTypeData inventoryTransferTypeData);

		InventoryTransferTypeData GetInventoryTransferType();

		bool DeleteInventoryTransferType(string ID);

		InventoryTransferTypeData GetInventoryTransferTypeByID(string id);

		DataSet GetInventoryTransferTypeByFields(params string[] columns);

		DataSet GetInventoryTransferTypeByFields(string[] ids, params string[] columns);

		DataSet GetInventoryTransferTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetInventoryTransferTypeList();

		DataSet GetInventoryTransferTypeComboList();
	}
}
