using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class InventoryDismantleSystem : MarshalByRefObject, IInventoryDismantleSystem, IDisposable
	{
		private Config config;

		public InventoryDismantleSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateInventoryDismantle(InventoryDismantleData data, bool isUpdate)
		{
			return new InventoryDismantle(config).InsertUpdateInventoryDismantle(data, isUpdate);
		}

		public InventoryDismantleData GetInventoryDismantleByID(string sysDocID, string voucherID)
		{
			return new InventoryDismantle(config).GetInventoryDismantleByID(sysDocID, voucherID);
		}

		public bool DeleteInventoryDismantle(string sysDocID, string voucherID)
		{
			return new InventoryDismantle(config).DeleteInventoryDismantle(sysDocID, voucherID);
		}

		public DataSet GetInventoryDismantleToPrint(string sysDocID, string[] voucherID)
		{
			return new InventoryDismantle(config).GetInventoryDismantleToPrint(sysDocID, voucherID);
		}

		public DataSet GetInventoryDismantleToPrint(string sysDocID, string voucherID)
		{
			return new InventoryDismantle(config).GetInventoryDismantleToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new InventoryDismantle(config).GetList(from, to, showVoid);
		}
	}
}
