using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class InventoryAdjustmentSystem : MarshalByRefObject, IInventoryAdjustmentSystem, IDisposable
	{
		private Config config;

		public InventoryAdjustmentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateInventoryAdjustment(InventoryAdjustmentData data, bool isUpdate)
		{
			return new InventoryAdjustment(config).InsertUpdateInventoryAdjustment(data, isUpdate);
		}

		public InventoryAdjustmentData GetInventoryAdjustmentByID(string sysDocID, string voucherID)
		{
			return new InventoryAdjustment(config).GetInventoryAdjustmentByID(sysDocID, voucherID);
		}

		public bool DeleteInventoryAdjustment(string sysDocID, string voucherID, bool isupdatecosting)
		{
			return new InventoryAdjustment(config).DeleteInventoryAdjustment(sysDocID, voucherID, isupdatecosting);
		}

		public DataSet GetInventoryAdjustmentToPrint(string sysDocID, string[] voucherID)
		{
			return new InventoryAdjustment(config).GetInventoryAdjustmentToPrint(sysDocID, voucherID);
		}

		public DataSet GetInventoryAdjustmentToPrint(string sysDocID, string voucherID)
		{
			return new InventoryAdjustment(config).GetInventoryAdjustmentToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetInventoryAdjustmentReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin)
		{
			return new InventoryAdjustment(config).GetInventoryAdjustmentReport(from, to, fromWarehouse, toWarehouse, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, fromBrand, toBrand, fromManufacturer, toManufacturer, fromStyle, toStyle, fromOrigin, toOrigin);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new InventoryAdjustment(config).GetList(from, to, showVoid);
		}
	}
}
