using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class TRApplicationSystem : MarshalByRefObject, ITRApplicationSystem, IDisposable
	{
		private Config config;

		public TRApplicationSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool UpdateTRApplication(TRApplicationData applicationData, string closingPassword)
		{
			return new TRApplication(config).InsertUpdateTRApplication(applicationData, isUpdate: true);
		}

		public bool UpdateTRApplication(TRApplicationData applicationData)
		{
			return UpdateTRApplication(applicationData, string.Empty);
		}

		public bool CreateTRApplication(TRApplicationData applicationData, string closingPassword)
		{
			return new TRApplication(config).InsertUpdateTRApplication(applicationData, isUpdate: false);
		}

		public bool CreateTRApplication(TRApplicationData applicationData)
		{
			return new TRApplication(config).InsertUpdateTRApplication(applicationData, isUpdate: false);
		}

		public TRApplicationData GetTRApplicationByID(string sysDocID, string voucherID)
		{
			using (TRApplication tRApplication = new TRApplication(config))
			{
				return tRApplication.GetTransactionByID(sysDocID, voucherID);
			}
		}

		public bool DeleteTRApplication(string sysDocID, string voucherID)
		{
			using (TRApplication tRApplication = new TRApplication(config))
			{
				return tRApplication.DeleteTransaction(sysDocID, voucherID);
			}
		}

		public bool VoidTRApplication(string sysDocID, string voucherID, bool isVoid)
		{
			using (TRApplication tRApplication = new TRApplication(config))
			{
				return tRApplication.VoidTransaction(sysDocID, voucherID, isVoid);
			}
		}

		public bool DeleteTRApplication(string transactionID)
		{
			return DeleteTRApplication(transactionID, string.Empty);
		}

		public DataSet GetTRApplicationToPrint(string sysDocID, string[] voucherID)
		{
			return new TRApplication(config).GetTRApplicationToPrint(sysDocID, voucherID);
		}

		public DataSet GetTRApplicationToPrint(string sysDocID, string voucherID)
		{
			return new TRApplication(config).GetTRApplicationToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetOpenTransactions(string filtertype)
		{
			return new TRApplication(config).GetOpenTransactions(filtertype);
		}

		public DataSet GetList(DateTime from, DateTime to, BankFacilityTypes facilityType, bool showVoid)
		{
			return new TRApplication(config).GetList(from, to, facilityType, showVoid);
		}

		public bool AllowModify(string sysDocID, string voucherNumber)
		{
			return new TRApplication(config).AllowModify(sysDocID, voucherNumber);
		}
	}
}
