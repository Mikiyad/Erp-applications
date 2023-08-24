using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IGarmentRentalReturnSystem
	{
		bool CreateGarmentRentalReturn(GarmentRentalReturnData consignOutData, bool isUpdate);

		GarmentRentalReturnData GetGarmentRentalReturnByID(string sysDocID, string voucherID);

		bool DeleteGarmentRentalReturn(string sysDocID, string voucherID);

		bool VoidGarmentRentalReturn(string sysDocID, string voucherID, bool isVoid);

		DataSet GetGarmentRentalReturnToPrint(string sysDocID, string voucherID);

		DataSet GetGarmentRentalReturnToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetOpenConsignments(string customerID);

		bool ConsignmentHasSettlement(string sysDocID, string voucherNumber);
	}
}
