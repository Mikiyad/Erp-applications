using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class InventoryDamageSystem : MarshalByRefObject, IInventoryDamageSystem, IDisposable
	{
		private Config config;

		public InventoryDamageSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateInventoryDamage(InventoryDamageData data, bool isUpdate)
		{
			return new InventoryDamage(config).InsertUpdateInventoryDamage(data, isUpdate);
		}

		public InventoryDamageData GetInventoryDamageByID(string sysDocID, string voucherID)
		{
			return new InventoryDamage(config).GetInventoryDamageByID(sysDocID, voucherID);
		}

		public bool DeleteInventoryDamage(string sysDocID, string voucherID)
		{
			return new InventoryDamage(config).DeleteInventoryDamage(sysDocID, voucherID);
		}

		public DataSet GetInventoryDamageToPrint(string sysDocID, string[] voucherID)
		{
			return new InventoryDamage(config).GetInventoryDamageToPrint(sysDocID, voucherID);
		}

		public DataSet GetInventoryDamageToPrint(string sysDocID, string voucherID)
		{
			return new InventoryDamage(config).GetInventoryDamageToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid, string sysDocID)
		{
			return new InventoryDamage(config).GetList(fromDate, toDate, showVoid: true, sysDocID);
		}

		public bool VoidInventoryDamage(string sysDocID, string voucherID, bool isVoid)
		{
			return new InventoryDamage(config).VoidInventoryDamage(sysDocID, voucherID, isVoid);
		}
	}
}
