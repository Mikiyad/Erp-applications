using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISendChequeSystem
	{
		bool CreateSendCheque(SendChequeData sendChequeData, bool isUpdate);

		SendChequeData GetSendChequeByID(string sysDocID, string voucherID);

		bool DeleteSendCheque(string sysDocID, string voucherID);

		DataSet GetSendChequeToPrint(string sysDocID, string voucherID);

		DataSet GetSendChequeToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
