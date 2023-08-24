using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DiscountBillSystem : MarshalByRefObject, IDiscountBillSystem, IDisposable
	{
		private Config config;

		public DiscountBillSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDiscountBill(DiscountBillData data, bool isUpdate)
		{
			return new DiscountBills(config).InsertUpdateDiscountBills(data, isUpdate);
		}

		public DiscountBillData GetDiscountBillByID(string sysDocID, string voucherID)
		{
			return new DiscountBills(config).GetDiscountBillsByID(sysDocID, voucherID);
		}

		public bool DeleteDiscountBill(string sysDocID, string voucherID)
		{
			return new DiscountBills(config).DeleteDiscountBills(sysDocID, voucherID);
		}

		public DataSet GetDiscountBillToPrint(string sysDocID, string voucherID)
		{
			return new DiscountBills(config).GetDiscountBillsToPrint(sysDocID, voucherID);
		}

		public DataSet GetDiscountBillToPrint(string sysDocID, string[] voucherID)
		{
			return new DiscountBills(config).GetDiscountBillsToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new DiscountBills(config).GetList(from, to, showVoid);
		}

		public DataSet GetDiscountBillsReport(string CustomerID, string BankID, DateTime FromDate, DateTime ToDate, bool Status)
		{
			return new DiscountBills(config).GetDiscountBillssReport(CustomerID, BankID, FromDate, ToDate, Status);
		}

		public bool VoidDiscountBill(string sysDocID, string voucherID, bool isVoid)
		{
			return new DiscountBills(config).VoidDiscountBills(sysDocID, voucherID, isVoid);
		}
	}
}
