using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IGarmentRentalSystem
	{
		bool CreateGarmentRental(GarmentRentalData consignOutData, bool isUpdate);

		GarmentRentalData GetGarmentRentalByID(string sysDocID, string voucherID);

		bool DeleteGarmentRental(string sysDocID, string voucherID);

		bool VoidGarmentRental(string sysDocID, string voucherID, bool isVoid);

		DataSet GetUninvoicedGarmentRentals(string customerID);

		DataSet GetGarmentRentalToPrint(string sysDocID, string voucherID);

		DataSet GetGarmentRentalToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetOpenGarmentRentals(string customerID);

		DataSet GetGarmentRentalList(string sysDocID, DateTime from, DateTime to);

		DataSet GetGarmentRentalSummaryReport(string sysDocID, string voucherID, DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup);

		bool AllowModify(string sysDocID, string voucherNumber);

		DataSet GetPendingConsignmentsReport(string fromCustomer, string toCustomer);

		DataSet GetConsignmentOutSettlementReport(DateTime to, string fromCustomer, string toCustomer);

		DataSet GetConsignmentOutIssuedReport(DateTime settleto, DateTime from, DateTime to, string fromCustomer, string toCustomer, int intstatus);

		bool AllowDelete(string sysDocID, string voucherNumber);

		DataSet GetGarmentRentalAgreement(string sysDocID, string voucherID, string customerID);

		DataSet GetGarmentRentalAgreement(string sysDocID, string[] voucherID, string customerID);
	}
}
