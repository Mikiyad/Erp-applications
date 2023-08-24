using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IFixedAssetTransferSystem
	{
		bool CreateFixedAssetTransfer(FixedAssetTransferData fixedAssetTransferData, bool isUpdate);

		FixedAssetTransferData GetFixedAssetTransferByID(string sysDocID, string voucherID);

		bool DeleteFixedAssetTransfer(string sysDocID, string voucherID);

		bool VoidFixedAssetTransfer(string sysDocID, string voucherID, bool isVoid);

		DataSet GetFixedAssetTransferReport(DateTime from, DateTime to, string warehouseCode, bool isTransferOut);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
