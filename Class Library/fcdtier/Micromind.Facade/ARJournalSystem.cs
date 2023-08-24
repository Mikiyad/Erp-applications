using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ARJournalSystem : MarshalByRefObject, IARJournalSystem, IDisposable
	{
		private Config config;

		public ARJournalSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public DataSet GetUnallocatedInvoices(string customerID)
		{
			return new ARJournal(config).GetUnallocatedInvoices(customerID);
		}

		public DataSet GetARPaymentToAllocate(string sysDocID, string voucherID, string customerID, int arID, bool isPDC)
		{
			return new ARJournal(config).GetARPaymentToAllocate(sysDocID, voucherID, customerID, arID, isPDC);
		}

		public DataSet GetARPaymentToAllocate(string customerID, int arID, bool isPDC)
		{
			return new ARJournal(config).GetARPaymentToAllocate(customerID, arID, isPDC);
		}

		public bool CreatePaymentAllocation(ARJournalData journalData)
		{
			return new ARJournal(config).InsertPaymentAllocation(journalData);
		}

		public DataSet GetUnallocatedPayments()
		{
			return new ARJournal(config).GetUnallocatedPayments();
		}

		public DataSet GetARAllocationList(DateTime from, DateTime to)
		{
			return new ARJournal(config).GetARAllocationList(from, to);
		}

		public bool DeleteARAllocation(int id)
		{
			return new ARJournal(config).DeleteARAllocation(id);
		}
	}
}
