using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEquipmentWorkOrderSystem
	{
		bool CreateEquipmentWorkOrder(EquipmentWorkOrderData inventoryAdjustmentData, bool isUpdate);

		EquipmentWorkOrderData GetEquipmentWorkOrderByID(string sysDocID, string voucherID);

		bool DeleteEquipmentWorkOrder(string sysDocID, string voucherID);

		bool VoidEquipmentWorkOrder(string sysDocID, string voucherID, bool isVoid);

		DataSet GetEquipmentWorkOrderToPrint(string sysDocID, string voucherID);

		DataSet GetEquipmentWorkOrderToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetEquipmentWorkOrderList(string sysDocID);

		DataSet GetPurchaseList(string sysDocID, DateTime from, DateTime to);

		DataSet GetWorkorderByEquipmentLocationProjectReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetWorkOrderAll();

		DataSet GetInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string sysDocID, string VoucherID, bool showitemwithTansactions, bool showinactiveitems, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);
	}
}
