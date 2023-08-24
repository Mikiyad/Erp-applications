using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IConsignOutReturnSystem
	{
		bool CreateConsignOutReturn(ConsignOutReturnData consignOutData, bool isUpdate);

		ConsignOutReturnData GetConsignOutReturnByID(string sysDocID, string voucherID);

		bool DeleteConsignOutReturn(string sysDocID, string voucherID);

		bool VoidConsignOutReturn(string sysDocID, string voucherID, bool isVoid);

		DataSet GetConsignOutReturnToPrint(string sysDocID, string voucherID);

		DataSet GetConsignOutReturnToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetOpenConsignments(string customerID);

		bool ConsignmentHasSettlement(string sysDocID, string voucherNumber);
	}
}
