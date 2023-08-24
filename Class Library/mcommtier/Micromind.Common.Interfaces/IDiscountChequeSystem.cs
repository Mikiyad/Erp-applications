using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDiscountChequeSystem
	{
		bool CreateDiscountCheque(DiscountChequeData sendChequeData, bool isUpdate);

		DiscountChequeData GetDiscountChequeByID(string sysDocID, string voucherID);

		bool DeleteDiscountCheque(string sysDocID, string voucherID);

		DataSet GetDiscountChequeToPrint(string sysDocID, string voucherID);

		DataSet GetDiscountChequeToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetDiscountChequesReport(string CustomerID, string BankID, DateTime FromDate, DateTime ToDate, bool status);

		bool VoidDiscountCheque(string sysDocID, string voucherID, bool isVoid);
	}
}
