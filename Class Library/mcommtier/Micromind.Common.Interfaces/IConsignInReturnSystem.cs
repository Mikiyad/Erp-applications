using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IConsignInReturnSystem
	{
		bool CreateConsignInReturn(ConsignInReturnData consignInData, bool isUpdate);

		ConsignInReturnData GetConsignInReturnByID(string sysDocID, string voucherID);

		bool DeleteConsignInReturn(string sysDocID, string voucherID);

		bool VoidConsignInReturn(string sysDocID, string voucherID, bool isVoid);

		DataSet GetConsignInReturnToPrint(string sysDocID, string voucherID);

		DataSet GetConsignInReturnToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetOpenConsignments(string customerID);

		bool ConsignmentHasSettlement(string sysDocID, string voucherNumber);
	}
}
