using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IInventoryAdjustmentSystem
	{
		bool CreateInventoryAdjustment(InventoryAdjustmentData inventoryAdjustmentData, bool isUpdate);

		InventoryAdjustmentData GetInventoryAdjustmentByID(string sysDocID, string voucherID);

		DataSet GetInventoryAdjustmentToPrint(string sysDocID, string[] voucherID);

		DataSet GetInventoryAdjustmentToPrint(string sysDocID, string voucherID);

		bool DeleteInventoryAdjustment(string sysDocID, string voucherID, bool isupdatecosting);

		DataSet GetInventoryAdjustmentReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
