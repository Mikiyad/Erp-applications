using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IAPJournalSystem
	{
		DataSet GetUnallocatedInvoices(string vendorID);

		DataSet GetAPPaymentToAllocate(string sysDocID, string voucherID, string vendorID, int apID);

		bool CreatePaymentAllocation(APJournalData journalData);

		DataSet GetUnallocatedPayments();

		DataSet GetAPAllocationList(DateTime fromDate, DateTime toDate);

		bool DeleteAPAllocation(int id);
	}
}
