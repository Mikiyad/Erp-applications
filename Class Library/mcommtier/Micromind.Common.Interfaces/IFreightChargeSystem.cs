using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IFreightChargeSystem
	{
		bool CreateFreightCharge(FreightChargeData inventoryAdjustmentData, bool isUpdate);

		FreightChargeData GetFreightChargeByID(string sysDocID, string voucherID);

		DataSet GetFreightChargeByCustomerID(string customerID);

		bool DeleteFreightCharge(string sysDocID, string voucherID);

		bool VoidFreightCharge(string sysDocID, string voucherID, bool isVoid);

		DataSet GetFreightChargeToPrint(string sysDocID, string voucherID);

		DataSet GetFreightChargeToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetFreightChargeReport(DateTime fromDate, DateTime toDate, string fromPort, string toPort, string fromVendor, string toVendor, string fromClass, string toClass, string fromGroup, string toGroup, bool showInactive, string vendorIDs);

		DataSet GetFreightChargeAll();

		DataSet GetActiveFreightChargeByCustomerID(string customerID);
	}
}
