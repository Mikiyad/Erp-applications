using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPriceListSystem
	{
		bool CreatePriceList(PriceListData inventoryAdjustmentData, bool isUpdate);

		bool CreateVendorPriceList(PriceListData inventoryAdjustmentData, bool isUpdate);

		PriceListData GetPriceListByID(string sysDocID, string voucherID);

		PriceListData GetVendorPriceListByID(string sysDocID, string voucherID);

		DataSet GetPriceListByCustomerID(string customerID);

		DataSet GetPriceListByVendorID(string customerID);

		bool DeletePriceList(string sysDocID, string voucherID);

		bool DeleteVendorPriceList(string sysDocID, string voucherID);

		bool VoidPriceList(string sysDocID, string voucherID, bool isVoid);

		DataSet GetPriceListToPrint(string sysDocID, string voucherID);

		DataSet GetPriceListToPrint(string sysDocID, string[] voucherID);

		DataSet GetVendorPriceListToPrint(string sysDocID, string voucherID);

		DataSet GetVendorPriceListToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetVendorList(DateTime from, DateTime to, bool showVoid);

		DataSet GetPriceListAll();

		DataSet GetActivePriceListByCustomerID(string customerID);
	}
}
