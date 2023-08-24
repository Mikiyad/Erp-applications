using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IW3PLGRNSystem
	{
		bool CreateGRN(W3PLGRNData consignInData, bool isUpdate);

		W3PLGRNData GetGRNByID(string sysDocID, string voucherID);

		bool DeleteGRN(string sysDocID, string voucherID);

		bool VoidGRN(string sysDocID, string voucherID, bool isVoid);

		DataSet GetUninvoicedGRNs(string vendorID);

		DataSet GetGRNToPrint(string sysDocID, string voucherID);

		DataSet GetGRNToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetOpenGRNs(string vendorID);

		bool AllowModify(string sysDocID, string voucherNumber);

		DataSet GetGRNComboList();

		DataSet GetGRNClosingSummary(string sysDocID, string voucherID);

		bool CloseGRN(W3PLGRNData consignInData);

		DataSet GetGRNReport(DateTime from, DateTime to, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup);

		DataSet GetGRNList(string sysDocID, DateTime from, DateTime to);

		DataSet GetGRNItemMovementReport(DateTime from, DateTime to, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromCategory, string toCategory, string fromLocationID, string toLocationID, string sysDocID, string voucherID);

		DataSet GetGRNsItemsToInvoice(string sysDocID, string voucherID, DateTime date);

		DataSet GetGRNSummaryReport(string sysDocID, string voucherID);
	}
}
