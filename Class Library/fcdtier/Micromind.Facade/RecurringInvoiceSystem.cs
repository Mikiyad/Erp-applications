using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class RecurringInvoiceSystem : MarshalByRefObject, IRecurringInvoiceSystem, IDisposable
	{
		private Config config;

		public RecurringInvoiceSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateRecurringInvoice(RecurringInvoiceData data, bool isUpdate, bool isPosting)
		{
			return new RecurringInvoice(config).CreateRecurringInvoice(data, isUpdate, isPosting);
		}

		public RecurringInvoiceData GetRecurringInvoice()
		{
			using (RecurringInvoice recurringInvoice = new RecurringInvoice(config))
			{
				return recurringInvoice.GetRecurringInvoice();
			}
		}

		public bool DeleteRecurringInvoice(string id)
		{
			using (RecurringInvoice recurringInvoice = new RecurringInvoice(config))
			{
				return recurringInvoice.DeleteRecurringInvoice(id);
			}
		}

		public RecurringInvoiceData GetRecurringInvoice(string id)
		{
			using (RecurringInvoice recurringInvoice = new RecurringInvoice(config))
			{
				return recurringInvoice.GetRecurringInvoice(id);
			}
		}

		public RecurringInvoiceData GetRecurringInvoiceByID(string sysDocID, string voucherID)
		{
			using (RecurringInvoice recurringInvoice = new RecurringInvoice(config))
			{
				return recurringInvoice.GetRecurringInvoiceByID(sysDocID, voucherID);
			}
		}

		public RecurringInvoiceData GetRecurringInvoiceByID(string transactionID)
		{
			using (RecurringInvoice recurringInvoice = new RecurringInvoice(config))
			{
				return recurringInvoice.GetRecurringInvoiceByID(transactionID);
			}
		}

		public DataSet GetPeriodicInvoice()
		{
			using (RecurringInvoice recurringInvoice = new RecurringInvoice(config))
			{
				return recurringInvoice.GetPeriodicInvoice();
			}
		}

		public DateTime GetLastPostedDate(string transactionDate)
		{
			using (RecurringInvoice recurringInvoice = new RecurringInvoice(config))
			{
				return recurringInvoice.GetLastPostedDate(transactionDate);
			}
		}

		public string GetNextRecurringInvoiceDocumentNumber(string sysDocID, string lastNumber)
		{
			using (RecurringInvoice recurringInvoice = new RecurringInvoice(config))
			{
				return recurringInvoice.GetNextRecurringInvoiceDocumentNumber(sysDocID, lastNumber);
			}
		}
	}
}
