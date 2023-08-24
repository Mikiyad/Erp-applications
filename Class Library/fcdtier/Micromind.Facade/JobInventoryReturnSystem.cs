using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobInventoryReturnSystem : MarshalByRefObject, IJobInventoryReturnSystem, IDisposable
	{
		private Config config;

		public JobInventoryReturnSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobInventoryReturn(JobInventoryReturnData data, bool isUpdate)
		{
			return new JobInventoryReturn(config).InsertUpdateJobInventoryReturn(data, isUpdate);
		}

		public JobInventoryReturnData GetJobInventoryReturnByID(string sysDocID, string voucherID)
		{
			return new JobInventoryReturn(config).GetJobInventoryReturnByID(sysDocID, voucherID);
		}

		public bool DeleteJobInventoryReturn(string sysDocID, string voucherID)
		{
			return new JobInventoryReturn(config).DeleteJobInventoryReturn(sysDocID, voucherID);
		}

		public DataSet GetJobInventoryReturnToPrint(string sysDocID, string[] voucherID)
		{
			return new JobInventoryReturn(config).GetJobInventoryReturnToPrint(sysDocID, voucherID);
		}

		public DataSet GetJobInventoryReturnToPrint(string sysDocID, string voucherID)
		{
			return new JobInventoryReturn(config).GetJobInventoryReturnToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new JobInventoryReturn(config).GetJobInventoryReturnList(fromDate, toDate, Isvoid: true);
		}
	}
}
