using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IMobilizationSystem
	{
		bool CreateMobilization(MobilizationData inventoryAdjustmentData, bool isUpdate);

		MobilizationData GetMobilizationByID(string sysDocID, string voucherID);

		bool DeleteMobilization(string sysDocID, string voucherID);

		bool VoidMobilization(string sysDocID, string voucherID, bool isVoid);

		DataSet GetInvoicesForDelivery(string customerID);

		DataSet GetMobilizationToPrint(string sysDocID, string voucherID);

		DataSet GetMobilizationToPrint(string sysDocID, string[] voucherID);

		DataSet GetMobilizationByEquipmentLocationProjectReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromEquipment, string toEquipment, string fromType, string toType, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromJob, string toJob, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetMobilizationList(string sysDocID);

		DataSet GetMobilizationList();

		DataSet GetPurchaseList(string sysDocID, DateTime from, DateTime to);
	}
}
