using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobMaterialEstimateSystem : MarshalByRefObject, IJobMaterialEstimateSystem, IDisposable
	{
		private Config config;

		public JobMaterialEstimateSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobMaterialEstimate(JobMaterialEstimateData data, bool isUpdate)
		{
			return new JobMaterialEstimate(config).InsertUpdateJobMaterialEstimate(data, isUpdate);
		}

		public JobMaterialEstimateData GetJobMaterialEstimateByID(string sysDocID, string voucherID)
		{
			return new JobMaterialEstimate(config).GetJobMaterialEstimateByID(sysDocID, voucherID);
		}

		public DataSet GetJobMaterialEstimateAll()
		{
			return new JobMaterialEstimate(config).GetJobMaterialEstimateAll();
		}

		public bool DeleteJobMaterialEstimate(string sysDocID, string voucherID)
		{
			return new JobMaterialEstimate(config).DeleteJobMaterialEstimate(sysDocID, voucherID);
		}

		public DataSet GetJobMaterialEstimateToPrint(string sysDocID, string[] voucherID)
		{
			return new JobMaterialEstimate(config).GetJobMaterialEstimateToPrint(sysDocID, voucherID);
		}

		public DataSet GetJobMaterialEstimateToPrint(string sysDocID, string voucherID)
		{
			return new JobMaterialEstimate(config).GetJobMaterialEstimateToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new JobMaterialEstimate(config).GetJobMaterialEstimateList(fromDate, toDate, Isvoid: true);
		}

		public JobMaterialEstimateData GetJobMaterialEstimateByJobIDCostCategoryID(string sysDocID, string voucherID, string jobID, string costCategoryID)
		{
			return new JobMaterialEstimate(config).GetJobMaterialEstimateByJobIDCostCategoryID(sysDocID, voucherID, jobID, costCategoryID);
		}

		public DataSet GetJobMaterialEstimationList()
		{
			return new JobMaterialEstimate(config).GetJobMaterialEstimationList();
		}

		public decimal GetJobMaterialEstimationQty(string jobID)
		{
			return new JobMaterialEstimate(config).GetJobMaterialEstimationQty(jobID);
		}
	}
}
