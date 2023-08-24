using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ILoanEntrySystem
	{
		bool CreateLoanEntry(LoanEntryData loanData, bool isUpdate);

		LoanEntryData GetLoanEntryByID(string sysDocID, string voucherID);

		bool DeleteLoanEntry(string sysDocID, string voucherID);

		DataSet GetListLoanEntry(DateTime from, DateTime to, bool showVoid);

		DataSet GetLoanEntryToPrint(string sysDocID, string voucherID);
	}
}
