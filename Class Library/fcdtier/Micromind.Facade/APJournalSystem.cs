using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class APJournalSystem : MarshalByRefObject, IAPJournalSystem, IDisposable
	{
		private Config config;

		public APJournalSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public DataSet GetUnallocatedInvoices(string vendorID)
		{
			return new APJournal(config).GetUnallocatedInvoices(vendorID);
		}

		public DataSet GetAPPaymentToAllocate(string sysDocID, string voucherID, string vendorID, int apID)
		{
			return new APJournal(config).GetAPPaymentToAllocate(sysDocID, voucherID, vendorID, apID);
		}

		public bool CreatePaymentAllocation(APJournalData journalData)
		{
			return new APJournal(config).InsertPaymentAllocation(journalData);
		}

		public DataSet GetUnallocatedPayments()
		{
			return new APJournal(config).GetUnallocatedPayments();
		}

		public DataSet GetAPAllocationList(DateTime fromDate, DateTime toDate)
		{
			return new APJournal(config).GetAPAllocationList(fromDate, toDate);
		}

		public bool DeleteAPAllocation(int id)
		{
			return new APJournal(config).DeleteAPAllocation(id);
		}
	}
}
