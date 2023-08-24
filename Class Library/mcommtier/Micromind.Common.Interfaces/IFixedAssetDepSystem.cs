using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IFixedAssetDepSystem
	{
		bool CreateFixedAssetDep(FixedAssetDepData fixedAssetDepData, bool isUpdate);

		FixedAssetDepData GetFixedAssetDepByID(string sysDocID, string voucherID);

		bool DeleteFixedAssetDep(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetFixedAssetDepToPrint(string sysDocID, string voucherID);

		DataSet GetFixedAssetDepToPrint(string sysDocID, string[] voucherID);
	}
}
