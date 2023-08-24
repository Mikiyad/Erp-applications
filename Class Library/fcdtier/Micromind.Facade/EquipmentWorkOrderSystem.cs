using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EquipmentWorkOrderSystem : MarshalByRefObject, IEquipmentWorkOrderSystem, IDisposable
	{
		private Config config;

		public EquipmentWorkOrderSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEquipmentWorkOrder(EquipmentWorkOrderData data, bool isUpdate)
		{
			return new EquipmentWorkOrder(config).InsertUpdateEquipmentWorkOrder(data, isUpdate);
		}

		public EquipmentWorkOrderData GetEquipmentWorkOrderByID(string sysDocID, string voucherID)
		{
			return new EquipmentWorkOrder(config).GetEquipmentWorkOrderByID(sysDocID, voucherID);
		}

		public bool DeleteEquipmentWorkOrder(string sysDocID, string voucherID)
		{
			return new EquipmentWorkOrder(config).DeleteEquipmentWorkOrder(sysDocID, voucherID);
		}

		public bool VoidEquipmentWorkOrder(string sysDocID, string voucherID, bool isVoid)
		{
			return new EquipmentWorkOrder(config).VoidEquipmentWorkOrder(sysDocID, voucherID, isVoid);
		}

		public DataSet GetEquipmentWorkOrderToPrint(string sysDocID, string voucherID)
		{
			return new EquipmentWorkOrder(config).GetEquipmentWorkOrderToPrint(sysDocID, voucherID);
		}

		public DataSet GetEquipmentWorkOrderToPrint(string sysDocID, string[] voucherID)
		{
			return new EquipmentWorkOrder(config).GetEquipmentWorkOrderToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new EquipmentWorkOrder(config).GetList(from, to, showVoid);
		}

		public DataSet GetPurchaseExpenseAllocationReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string sysDocID, string voucherID)
		{
			return new EquipmentWorkOrder(config).GetPurchaseExpenseAllocationReport(fromDate, toDate, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, sysDocID, voucherID);
		}

		public DataSet GetEquipmentWorkOrderList(string sysDocID)
		{
			return new EquipmentWorkOrder(config).GetEquipmentWorkOrderList(sysDocID);
		}

		public DataSet GetPurchaseList(string sysDocID, DateTime fromDate, DateTime endDate)
		{
			return new EquipmentWorkOrder(config).GetPurchaseList(sysDocID, fromDate, endDate);
		}

		public DataSet GetWorkorderByEquipmentLocationProjectReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			return new EquipmentWorkOrder(config).GetWorkorderByEquipmentLocationProjectReport(fromDate, toDate, fromItem, toItem, fromItemClass, toItemClass, fromItemCategory, toItemCategory, fromEquipment, toEquipment, fromType, toType, fromCategory, toCategory, fromLocation, toLocation, fromJob, toJob, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
		}

		public DataSet GetWorkOrderAll()
		{
			return new EquipmentWorkOrder(config).GetWorkOrderAll();
		}

		public DataSet GetInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string sysDocID, string voucherID, bool showitemwithTansactions, bool showinactiveitems, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			using (EquipmentWorkOrder equipmentWorkOrder = new EquipmentWorkOrder(config))
			{
				return equipmentWorkOrder.GetInventoryTransactionReport(from, to, fromWarehouse, toWarehouse, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, sysDocID, voucherID, showitemwithTansactions, showinactiveitems, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
			}
		}
	}
}
