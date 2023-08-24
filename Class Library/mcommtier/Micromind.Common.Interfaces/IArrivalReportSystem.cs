using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IArrivalReportSystem
	{
		bool CreateArrivalReport(ArrivalReportData inventoryAdjustmentData, bool isUpdate);

		ArrivalReportData GetArrivalReportByID(string sysDocID, string voucherID);

		bool DeleteArrivalReport(string sysDocID, string voucherID);

		DataSet GetArrivalReportToPrint(string sysDocID, string voucherID);

		DataSet GetArrivalReportToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool isImport, bool showVoid);

		bool CanUpdate(string sysDocID, string voucherNumber);

		DataSet GetClaimableArrivalReports();
	}
}
