using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IFixedAssetPurchaseSystem
	{
		bool CreateFixedAssetPurchase(FixedAssetPurchaseData inventoryAdjustmentData, bool isUpdate);

		FixedAssetPurchaseData GetFixedAssetPurchaseByID(string sysDocID, string voucherID);

		bool DeleteFixedAssetPurchase(string sysDocID, string voucherID);

		bool VoidFixedAssetPurchase(string sysDocID, string voucherID, bool isVoid);

		DataSet GetFixedAssetPurchaseToPrint(string sysDocID, string voucherID);

		DataSet GetFixedAssetPurchaseToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
