using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalesReturnSystem : MarshalByRefObject, ISalesReturnSystem, IDisposable
	{
		private Config config;

		public SalesReturnSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalesReturn(SalesReturnData data, bool isUpdate)
		{
			return new SalesReturn(config).InsertUpdateSalesReturn(data, isUpdate);
		}

		public SalesReturnData GetSalesReturnByID(string sysDocID, string voucherID)
		{
			return new SalesReturn(config).GetSalesReturnByID(sysDocID, voucherID);
		}

		public bool DeleteSalesReturn(string sysDocID, string voucherID)
		{
			return new SalesReturn(config).DeleteSalesReturn(sysDocID, voucherID);
		}

		public bool VoidSalesReturn(string sysDocID, string voucherID, bool isVoid)
		{
			return new SalesReturn(config).VoidSalesReturn(sysDocID, voucherID, isVoid);
		}

		public DataSet GetSalesReturnToPrint(string sysDocID, string voucherID)
		{
			return new SalesReturn(config).GetSalesReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetSalesReturnToPrint(string sysDocID, string[] voucherID)
		{
			return new SalesReturn(config).GetSalesReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetInvoicesToReturn(string customerID, bool cashOnly, DateTime fromDate, DateTime toDate)
		{
			return new SalesReturn(config).GetInvoicesToReturn(customerID, cashOnly, fromDate, toDate, 0);
		}

		public DataSet GetInvoicesToReturn(string returnSysDocID, string customerID, bool cashOnly, DateTime fromDate, DateTime toDate, int backdaysAllowed)
		{
			return new SalesReturn(config).GetInvoicesToReturn(returnSysDocID, customerID, cashOnly, fromDate, toDate, backdaysAllowed);
		}

		public DataSet GetInvoicesToReturn(string returnSysDocID, string customerID, bool cashOnly, DateTime fromDate, DateTime toDate, int backdaysAllowed, string locationID)
		{
			return new SalesReturn(config).GetInvoicesToReturn(returnSysDocID, customerID, cashOnly, fromDate, toDate, backdaysAllowed, locationID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid, string sysDocID)
		{
			return new SalesReturn(config).GetList(from, to, showVoid, sysDocID);
		}

		public bool ModifyTransactions(string sysDocID, string voucherNumber, string userID, bool isModify, string toUpdate)
		{
			return new SalesReturn(config).ModifyTransactions(sysDocID, voucherNumber, userID, isModify, toUpdate);
		}
	}
}
