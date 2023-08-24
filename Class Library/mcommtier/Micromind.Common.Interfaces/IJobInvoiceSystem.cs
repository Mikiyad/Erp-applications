using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobInvoiceSystem
	{
		bool CreateJobInvoice(JobInvoiceData inventoryAdjustmentData, bool isUpdate);

		JobInvoiceData GetJobInvoiceByID(string sysDocID, string voucherID);

		bool DeleteJobInvoice(string sysDocID, string voucherID);

		bool VoidJobInvoice(string sysDocID, string voucherID, bool isVoid);

		DataSet GetInvoicesForDelivery(string customerID, bool isExport);

		bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price);

		DataSet GetJobInvoiceToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems);

		DataSet GetJobInvoiceToPrint(string sysDocID, string voucherID, bool mergeMatrixItems);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
