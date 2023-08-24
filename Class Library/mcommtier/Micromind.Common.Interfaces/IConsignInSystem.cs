using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IConsignInSystem
	{
		bool CreateConsignIn(ConsignInData consignInData, bool isUpdate);

		ConsignInData GetConsignInByID(string sysDocID, string voucherID);

		bool DeleteConsignIn(string sysDocID, string voucherID);

		bool VoidConsignIn(string sysDocID, string voucherID, bool isVoid);

		DataSet GetUninvoicedConsignIns(string vendorID);

		DataSet GetConsignInToPrint(string sysDocID, string voucherID);

		DataSet GetConsignInToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetOpenConsignments(string vendorID);

		DataSet GetSettledConsignments(string vendorID);

		bool AllowModify(string sysDocID, string voucherNumber);

		DataSet GetPendingConsignmentsReport(string fromVendor, string toVendor, string vendorIDs);

		DataSet GetBillableItems(string consignSysDocID, string consignVoucherID);

		DataSet GetConsignInComboList();

		DataSet GetConsignInClosingSummary(string sysDocID, string voucherID);

		bool CloseConsignIn(ConsignInData consignInData);

		DataSet GetConsignInReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup);

		DataSet GetConsignInList(string sysDocID, DateTime from, DateTime to);

		DataSet GetConsignInItemMovementReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromLocationID, string toLocationID, string sysDocID, string voucherID, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetConsignmentInSettlementDetailReport(string fromVendor, string toVendor, string vendorIDs);

		DataSet GetConsignmentInSettlementSummaryReport(string fromVendor, string toVendor, string vendorID);

		DataSet GetConsignInSummaryReport(string sysDocID, string voucherID);
	}
}
