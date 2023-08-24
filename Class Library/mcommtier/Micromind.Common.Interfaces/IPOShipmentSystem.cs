using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPOShipmentSystem
	{
		bool CreatePOShipment(POShipmentData inventoryAdjustmentData, bool isUpdate);

		POShipmentData GetPOShipmentByID(string sysDocID, string voucherID);

		bool DeletePOShipment(string sysDocID, string voucherID);

		bool VoidPOShipment(string sysDocID, string voucherID, bool isVoid);

		DataSet GetOpenShipmentsSummary(string vendorID);

		DataSet GetOpenShipmentsReport();

		DataSet GetPOShipmentToPrint(string sysDocID, string[] voucherID);

		DataSet GetPOShipmentToPrint(string sysDocID, string voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetLastPOShipment(string poSysDocID, string poVoucherID);

		bool SetOrderStatus(string sysDocID, string voucherID, PurchaseOrderStatus status);

		DataSet GetBOLListForPayment(bool POMultipleBOL);

		DataSet GetPackingList();

		DataSet GetContainerStatusList();

		DataSet GetBOLList();

		DataSet GetBOLContainerList();

		DataSet GetContainerList();

		POShipmentData GetContainerDetails(string containerNumber);

		DataSet GetContainerComboList();
	}
}
