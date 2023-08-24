using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobEstimationSystem : MarshalByRefObject, IJobEstimationSystem, IDisposable
	{
		private Config config;

		public JobEstimationSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobEstimation(JobEstimationData data, bool isUpdate, bool IsRevised)
		{
			return new JobEstimation(config).InsertUpdateJobEstimation(data, isUpdate, IsRevised);
		}

		public JobEstimationData GetJobEstimationByID(string sysDocID, string voucherID, bool IsRevised, int RevisedNo)
		{
			if (!IsRevised)
			{
				return new JobEstimation(config).GetJobEstimationByID(sysDocID, voucherID);
			}
			return new JobEstimation(config).GetJobEstimationHistoryByID(sysDocID, voucherID, RevisedNo);
		}

		public bool DeleteJobEstimation(string sysDocID, string voucherID)
		{
			return new JobEstimation(config).DeleteJobEstimation(sysDocID, voucherID);
		}

		public DataSet GetJobEstimationToPrint(string sysDocID, string[] voucherID)
		{
			return new JobEstimation(config).GetJobEstimationToPrint(sysDocID, voucherID);
		}

		public DataSet GetJobEstimationToPrint(string sysDocID, string voucherID)
		{
			return new JobEstimation(config).GetJobEstimationToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new JobEstimation(config).GetJobEstimationList(fromDate, toDate, Isvoid);
		}

		public DataSet GetLoadRevisionCombo(string sysDocID, string voucherID)
		{
			return new JobEstimation(config).GetLoadRevisionCombo(sysDocID, voucherID);
		}

		public int GetNextRevsionNo(string sysDocID, string voucherID)
		{
			return new JobEstimation(config).GetNextRevsionNo(sysDocID, voucherID);
		}

		public DataSet GetJobEstimationRevToPrint(string sysDocID, string voucherID, int RevisedNo)
		{
			return new JobEstimation(config).GetJobEstimationRevToPrint(sysDocID, voucherID, RevisedNo);
		}

		public bool VoidJobEstimation(string sysDocID, string voucherID, bool isVoid)
		{
			return new JobEstimation(config).VoidJobEstimation(sysDocID, voucherID, isVoid);
		}
	}
}
