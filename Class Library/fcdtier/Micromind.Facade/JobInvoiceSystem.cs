using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobInvoiceSystem : MarshalByRefObject, IJobInvoiceSystem, IDisposable
	{
		private Config config;

		public JobInvoiceSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobInvoice(JobInvoiceData data, bool isUpdate)
		{
			return new JobInvoice(config).InsertUpdateJobInvoice(data, isUpdate);
		}

		public JobInvoiceData GetJobInvoiceByID(string sysDocID, string voucherID)
		{
			return new JobInvoice(config).GetJobInvoiceByID(sysDocID, voucherID);
		}

		public bool DeleteJobInvoice(string sysDocID, string voucherID)
		{
			return new JobInvoice(config).DeleteJobInvoice(sysDocID, voucherID);
		}

		public bool VoidJobInvoice(string sysDocID, string voucherID, bool isVoid)
		{
			return new JobInvoice(config).VoidJobInvoice(sysDocID, voucherID, isVoid);
		}

		public DataSet GetInvoicesForDelivery(string customerID, bool isExport)
		{
			return new JobInvoice(config).GetInvoicesForDelivery(customerID, isExport);
		}

		public bool IsBelowMinPrice(string productID, string unitID, string currencyID, decimal currencyRate, decimal price)
		{
			return new JobInvoice(config).IsBelowMinPrice(productID, unitID, currencyID, currencyRate, price);
		}

		public DataSet GetJobInvoiceToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems)
		{
			return new JobInvoice(config).GetJobInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems);
		}

		public DataSet GetJobInvoiceToPrint(string sysDocID, string voucherID, bool mergeMatrixItems)
		{
			return new JobInvoice(config).GetJobInvoiceToPrint(sysDocID, voucherID, mergeMatrixItems);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new JobInvoice(config).GetList(from, to, showVoid);
		}
	}
}
