using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IConsignOutSystem
	{
		bool CreateConsignOut(ConsignOutData consignOutData, bool isUpdate);

		ConsignOutData GetConsignOutByID(string sysDocID, string voucherID);

		bool DeleteConsignOut(string sysDocID, string voucherID);

		bool VoidConsignOut(string sysDocID, string voucherID, bool isVoid);

		DataSet GetUninvoicedConsignOuts(string customerID);

		DataSet GetConsignOutToPrint(string sysDocID, string voucherID);

		DataSet GetConsignOutToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetOpenConsignments(string customerID);

		bool ReCostTransaction(string sysDocID, string voucherID);

		DataSet GetConsignOutList(string sysDocID, DateTime from, DateTime to);

		DataSet GetConsignOutSummaryReport(string sysDocID, string voucherID, DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup);

		bool AllowModify(string sysDocID, string voucherNumber);

		DataSet GetPendingConsignmentsReport(string fromCustomer, string toCustomer, string customerIDs);

		DataSet GetConsignmentOutSettlementReport(DateTime to, string fromCustomer, string toCustomer, string customerIDs);

		DataSet GetConsignmentOutIssuedReport(DateTime settleto, DateTime from, DateTime to, string fromCustomer, string toCustomer, int intstatus, string customerIDs, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);
	}
}
