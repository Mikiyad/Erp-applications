using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDiscountBillSystem
	{
		bool CreateDiscountBill(DiscountBillData sendChequeData, bool isUpdate);

		DiscountBillData GetDiscountBillByID(string sysDocID, string voucherID);

		bool DeleteDiscountBill(string sysDocID, string voucherID);

		DataSet GetDiscountBillToPrint(string sysDocID, string voucherID);

		DataSet GetDiscountBillToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetDiscountBillsReport(string CustomerID, string BankID, DateTime FromDate, DateTime ToDate, bool status);

		bool VoidDiscountBill(string sysDocID, string voucherID, bool isVoid);
	}
}
