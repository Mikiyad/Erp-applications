using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IARJournalSystem
	{
		DataSet GetUnallocatedInvoices(string customerID);

		DataSet GetARPaymentToAllocate(string sysDocID, string voucherID, string customerID, int arID, bool isPDC);

		DataSet GetARPaymentToAllocate(string customerID, int arID, bool isPDC);

		bool CreatePaymentAllocation(ARJournalData journalData);

		DataSet GetUnallocatedPayments();

		DataSet GetARAllocationList(DateTime fromDate, DateTime toDate);

		bool DeleteARAllocation(int id);
	}
}
