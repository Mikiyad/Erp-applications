using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class LoanEntrySystem : MarshalByRefObject, ILoanEntrySystem, IDisposable
	{
		private Config config;

		public LoanEntrySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateLoanEntry(LoanEntryData data, bool isUpdate)
		{
			return new LoanEntry(config).InsertUpdateLoanEntry(data, isUpdate);
		}

		public DataSet GetListLoanEntry(DateTime from, DateTime to, bool showVoid)
		{
			return new LoanEntry(config).GetListLoanEntry(from, to, showVoid);
		}

		public LoanEntryData GetLoanEntryByID(string sysDocID, string voucherID)
		{
			return new LoanEntry(config).GetLoanEntryByID(sysDocID, voucherID);
		}

		public bool DeleteLoanEntry(string sysDocID, string voucherID)
		{
			return new LoanEntry(config).DeleteLoanEntry(sysDocID, voucherID);
		}

		public DataSet GetLoanEntryToPrint(string sysDocID, string voucherID)
		{
			return new LoanEntry(config).GetLoanEntryToPrint(sysDocID, voucherID);
		}
	}
}
