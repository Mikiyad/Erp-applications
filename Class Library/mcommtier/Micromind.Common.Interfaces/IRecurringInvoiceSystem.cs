using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IRecurringInvoiceSystem
	{
		bool CreateRecurringInvoice(RecurringInvoiceData typeData, bool isUpdate, bool isPosting);

		bool DeleteRecurringInvoice(string ID);

		RecurringInvoiceData GetRecurringInvoice(string id);

		RecurringInvoiceData GetRecurringInvoice();

		RecurringInvoiceData GetRecurringInvoiceByID(string sysDocID, string voucherID);

		RecurringInvoiceData GetRecurringInvoiceByID(string transactionID);

		DataSet GetPeriodicInvoice();

		DateTime GetLastPostedDate(string transactionID);

		string GetNextRecurringInvoiceDocumentNumber(string sysDocID, string lastNumber);
	}
}
