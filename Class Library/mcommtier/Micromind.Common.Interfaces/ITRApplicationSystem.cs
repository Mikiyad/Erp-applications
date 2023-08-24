using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ITRApplicationSystem
	{
		bool UpdateTRApplication(TRApplicationData applicationData);

		bool UpdateTRApplication(TRApplicationData applicationData, string closingPasword);

		bool CreateTRApplication(TRApplicationData applicationData);

		bool CreateTRApplication(TRApplicationData applicationData, string closingPasword);

		TRApplicationData GetTRApplicationByID(string sysDocID, string voucherID);

		bool VoidTRApplication(string sysDocID, string voucherID, bool isVoid);

		bool DeleteTRApplication(string sysDocID, string voucherID);

		DataSet GetTRApplicationToPrint(string sysDocID, string[] voucherID);

		DataSet GetTRApplicationToPrint(string sysDocID, string voucherID);

		DataSet GetOpenTransactions(string filter);

		DataSet GetList(DateTime from, DateTime to, BankFacilityTypes facilityType, bool showVoid);

		bool AllowModify(string sysDocID, string voucherNumber);
	}
}
