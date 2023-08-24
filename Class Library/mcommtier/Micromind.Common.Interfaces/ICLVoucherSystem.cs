using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICLVoucherSystem
	{
		bool CreateCLVoucher(CLVoucherData CLVoucherData, bool isInactive, bool isHold, bool isUpdate);

		CLVoucherData GetCLVoucherByID(string sysDocID, string voucherID);

		bool DeleteCLVoucher(string sysDocID, string voucherID);

		DataSet GetCLVoucherToPrint(string sysDocID, string voucherID);

		DataSet GetCLVoucherToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		bool VoidCLVoucher(string sysDocID, string voucherID, bool isVoid);
	}
}
