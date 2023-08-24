using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IGRNReturnSystem
	{
		bool CreateGRNReturn(GRNReturnData inventoryAdjustmentData, bool isUpdate);

		GRNReturnData GetGRNReturnByID(string sysDocID, string voucherID);

		bool DeleteGRNReturn(string sysDocID, string voucherID);

		bool VoidGRNReturn(string sysDocID, string voucherID, bool isVoid);

		DataSet GetGRNReturnToPrint(string sysDocID, string[] voucherID);

		DataSet GetGRNReturnToPrint(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetGRNsToReturn(string customerID, DateTime fromDate, DateTime toDate);
	}
}
