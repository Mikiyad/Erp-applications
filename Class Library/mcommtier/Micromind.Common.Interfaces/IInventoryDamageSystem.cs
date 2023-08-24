using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IInventoryDamageSystem
	{
		bool CreateInventoryDamage(InventoryDamageData inventoryDamageData, bool isUpdate);

		InventoryDamageData GetInventoryDamageByID(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID);

		DataSet GetInventoryDamageToPrint(string sysDocID, string[] voucherID);

		DataSet GetInventoryDamageToPrint(string sysDocID, string voucherID);

		bool VoidInventoryDamage(string sysDocID, string voucherID, bool isVoid);

		bool DeleteInventoryDamage(string sysDocID, string voucherID);
	}
}
