using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalesReturnSystem
	{
		bool CreateSalesReturn(SalesReturnData inventoryAdjustmentData, bool isUpdate);

		SalesReturnData GetSalesReturnByID(string sysDocID, string voucherID);

		bool DeleteSalesReturn(string sysDocID, string voucherID);

		bool VoidSalesReturn(string sysDocID, string voucherID, bool isVoid);

		DataSet GetSalesReturnToPrint(string sysDocID, string voucherID);

		DataSet GetSalesReturnToPrint(string sysDocID, string[] voucherID);

		DataSet GetInvoicesToReturn(string customerID, bool cashOnly, DateTime fromDate, DateTime toDate);

		DataSet GetInvoicesToReturn(string returnSysDocID, string customerID, bool cashOnly, DateTime fromDate, DateTime toDate, int backdaysAllowed);

		bool ModifyTransactions(string sysDocID, string voucherNumber, string userID, bool isModify, string toUpdate);

		DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID);

		DataSet GetInvoicesToReturn(string returnSysDocID, string customerID, bool cashOnly, DateTime fromDate, DateTime toDate, int backdaysAllowed, string locationID);
	}
}
