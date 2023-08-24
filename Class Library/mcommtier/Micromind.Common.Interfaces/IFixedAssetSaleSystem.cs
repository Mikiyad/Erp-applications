using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IFixedAssetSaleSystem
	{
		bool CreateFixedAssetSale(FixedAssetSaleData inventoryAdjustmentData, bool isUpdate);

		FixedAssetSaleData GetFixedAssetSaleByID(string sysDocID, string voucherID);

		bool DeleteFixedAssetSale(string sysDocID, string voucherID);

		bool VoidFixedAssetSale(string sysDocID, string voucherID, bool isVoid);

		DataSet GetFixedAssetSaleToPrint(string sysDocID, string voucherID);

		DataSet GetFixedAssetSaleToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
