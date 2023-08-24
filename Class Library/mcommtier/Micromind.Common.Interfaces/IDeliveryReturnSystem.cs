using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDeliveryReturnSystem
	{
		bool CreateDeliveryReturn(DeliveryReturnData deliveryReturnData, bool isUpdate);

		DeliveryReturnData GetDeliveryReturnByID(string sysDocID, string voucherID);

		bool DeleteDeliveryReturn(string sysDocID, string voucherID);

		bool VoidDeliveryReturn(string sysDocID, string voucherID, bool isVoid);

		DataSet GetUninvoicedDeliveryReturns(string customerID);

		DataSet GetDeliveryReturnToPrint(string sysDocID, string voucherID);

		DataSet GetDeliveryReturnToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
