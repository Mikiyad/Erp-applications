using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IInventoryDismantleSystem
	{
		bool CreateInventoryDismantle(InventoryDismantleData packedItemData, bool isUpdate);

		InventoryDismantleData GetInventoryDismantleByID(string sysDocID, string voucherID);

		DataSet GetInventoryDismantleToPrint(string sysDocID, string[] voucherID);

		DataSet GetInventoryDismantleToPrint(string sysDocID, string voucherID);

		bool DeleteInventoryDismantle(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
