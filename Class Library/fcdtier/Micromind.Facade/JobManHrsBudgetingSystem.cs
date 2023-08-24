using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobManHrsBudgetingSystem : MarshalByRefObject, IJobManHrsBudgetingSystem, IDisposable
	{
		private Config config;

		public JobManHrsBudgetingSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobManHrsBudgeting(JobManHrsBudgetingData data, bool isUpdate)
		{
			return new JobManHrsBudgeting(config).InsertUpdateJobManHrsBudgeting(data, isUpdate);
		}

		public JobManHrsBudgetingData GetJobManHrsBudgetingByID(string sysDocID, string voucherID)
		{
			return new JobManHrsBudgeting(config).GetJobManHrsBudgetingByID(sysDocID, voucherID);
		}

		public DataSet GetJobManHrsBudgetingAll()
		{
			return new JobManHrsBudgeting(config).GetJobManHrsBudgetingAll();
		}

		public bool DeleteJobManHrsBudgeting(string sysDocID, string voucherID)
		{
			return new JobManHrsBudgeting(config).DeleteJobManHrsBudgeting(sysDocID, voucherID);
		}

		public DataSet GetJobManHrsBudgetingToPrint(string sysDocID, string[] voucherID)
		{
			return new JobManHrsBudgeting(config).GetJobManHrsBudgetingToPrint(sysDocID, voucherID);
		}

		public DataSet GetJobManHrsBudgetingToPrint(string sysDocID, string voucherID)
		{
			return new JobManHrsBudgeting(config).GetJobManHrsBudgetingToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new JobManHrsBudgeting(config).GetJobManHrsBudgetingList(fromDate, toDate, Isvoid: true);
		}
	}
}
