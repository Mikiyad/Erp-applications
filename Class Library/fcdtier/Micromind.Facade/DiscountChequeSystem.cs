using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DiscountChequeSystem : MarshalByRefObject, IDiscountChequeSystem, IDisposable
	{
		private Config config;

		public DiscountChequeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDiscountCheque(DiscountChequeData data, bool isUpdate)
		{
			return new DiscountCheques(config).InsertUpdateDiscountCheque(data, isUpdate);
		}

		public DiscountChequeData GetDiscountChequeByID(string sysDocID, string voucherID)
		{
			return new DiscountCheques(config).GetDiscountChequeByID(sysDocID, voucherID);
		}

		public bool DeleteDiscountCheque(string sysDocID, string voucherID)
		{
			return new DiscountCheques(config).DeleteDiscountCheque(sysDocID, voucherID);
		}

		public DataSet GetDiscountChequeToPrint(string sysDocID, string voucherID)
		{
			return new DiscountCheques(config).GetDiscountChequeToPrint(sysDocID, voucherID);
		}

		public DataSet GetDiscountChequeToPrint(string sysDocID, string[] voucherID)
		{
			return new DiscountCheques(config).GetDiscountChequeToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new DiscountCheques(config).GetList(from, to, showVoid);
		}

		public DataSet GetDiscountChequesReport(string CustomerID, string BankID, DateTime FromDate, DateTime ToDate, bool Status)
		{
			return new DiscountCheques(config).GetDiscountChequesReport(CustomerID, BankID, FromDate, ToDate, Status);
		}

		public bool VoidDiscountCheque(string sysDocID, string voucherID, bool isVoid)
		{
			return new DiscountCheques(config).VoidDiscountCheque(sysDocID, voucherID, isVoid);
		}
	}
}
