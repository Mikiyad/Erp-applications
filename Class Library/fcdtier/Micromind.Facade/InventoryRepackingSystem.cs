using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class InventoryRepackingSystem : MarshalByRefObject, IInventoryRepackingSystem, IDisposable
	{
		private Config config;

		public InventoryRepackingSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateInventoryRepacking(InventoryRepackingData data, bool isUpdate)
		{
			return new InventoryRepacking(config).InsertUpdateInventoryRepacking(data, isUpdate);
		}

		public InventoryRepackingData GetInventoryRepackingByID(string sysDocID, string voucherID)
		{
			return new InventoryRepacking(config).GetInventoryRepackingByID(sysDocID, voucherID);
		}

		public bool DeleteInventoryRepacking(string sysDocID, string voucherID)
		{
			return new InventoryRepacking(config).DeleteInventoryRepacking(sysDocID, voucherID);
		}

		public DataSet GetInventoryRepackingToPrint(string sysDocID, string[] voucherID)
		{
			return new InventoryRepacking(config).GetInventoryRepackingToPrint(sysDocID, voucherID);
		}

		public DataSet GetInventoryRepackingToPrint(string sysDocID, string voucherID)
		{
			return new InventoryRepacking(config).GetInventoryRepackingToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new InventoryRepacking(config).GetList(from, to, showVoid);
		}
	}
}
