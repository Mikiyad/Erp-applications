using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IOpeningBalanceBatchSystem
	{
		bool CreateOpeningBalanceBatch(OpeningBalanceBatchData openingBalanceBatchData, bool isUpdate);

		OpeningBalanceBatchData GetOpeningBalanceBatchByID(string sysDocID, string voucherID);

		DataSet GetOpeningBalanceBatchToPrint(string sysDocID, string[] voucherID);

		DataSet GetOpeningBalanceBatchToPrint(string sysDocID, string voucherID);

		bool DeleteOpeningBalanceBatch(string sysDocID, string voucherID);

		string GetNextBatchNumber(string sysDocID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid, string SysDocID);
	}
}
