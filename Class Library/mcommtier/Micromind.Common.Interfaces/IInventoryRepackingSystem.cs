using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IInventoryRepackingSystem
	{
		bool CreateInventoryRepacking(InventoryRepackingData packedItemData, bool isUpdate);

		InventoryRepackingData GetInventoryRepackingByID(string sysDocID, string voucherID);

		DataSet GetInventoryRepackingToPrint(string sysDocID, string[] voucherID);

		DataSet GetInventoryRepackingToPrint(string sysDocID, string voucherID);

		bool DeleteInventoryRepacking(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
