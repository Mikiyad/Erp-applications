using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IOpeningBalanceLeaveSystem
	{
		bool CreateOpeningBalanceLeave(OpeningBalanceLeaveData openingBalanceLeaveData, bool isUpdate);

		OpeningBalanceLeaveData GetOpeningBalanceLeaveByID(string sysDocID, string voucherID);

		DataSet GetOpeningBalanceLeaveToPrint(string sysDocID, string[] voucherID);

		DataSet GetOpeningBalanceLeaveToPrint(string sysDocID, string voucherID);

		bool DeleteOpeningBalanceLeave(string sysDocID, string voucherID);

		string GetNextLeaveNumber(string sysDocID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
