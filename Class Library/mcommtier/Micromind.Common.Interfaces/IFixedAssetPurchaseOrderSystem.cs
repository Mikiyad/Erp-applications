using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IFixedAssetPurchaseOrderSystem
	{
		bool CreatePurchaseOrder(FixedAssetPurchaseOrderData fixedAssetPurchaseOrderData, bool isUpdate);

		FixedAssetPurchaseOrderData GetPurchaseOrderByID(string sysDocID, string voucherID);

		bool CanUpdate(string sysDocID, string voucherNumber);

		bool DeletePurchaseOrder(string sysDocID, string voucherID);

		bool VoidPurchaseOrder(string sysDocID, string voucherID, bool isVoid);

		DataSet GetPurchaseOrderToPrint(string sysDocID, string voucherID);

		DataSet GetPurchaseOrderToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
