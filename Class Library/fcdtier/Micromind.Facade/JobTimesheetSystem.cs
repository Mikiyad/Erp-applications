using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobTimesheetSystem : MarshalByRefObject, IJobTimesheetSystem, IDisposable
	{
		private Config config;

		public JobTimesheetSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobTimesheet(JobTimesheetData data, bool isUpdate)
		{
			return new JobTimesheet(config).InsertUpdateJobTimesheet(data, isUpdate);
		}

		public JobTimesheetData GetJobTimesheetByID(string sysDocID, string voucherID)
		{
			return new JobTimesheet(config).GetJobTimesheetByID(sysDocID, voucherID);
		}

		public bool DeleteJobTimesheet(string sysDocID, string voucherID)
		{
			return new JobTimesheet(config).DeleteJobTimesheet(sysDocID, voucherID);
		}

		public DataSet GetJobTimesheetToPrint(string sysDocID, string[] voucherID)
		{
			return new JobTimesheet(config).GetJobTimesheetToPrint(sysDocID, voucherID);
		}

		public DataSet GetJobTimesheetToPrint(string sysDocID, string voucherID)
		{
			return new JobTimesheet(config).GetJobTimesheetToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new JobTimesheet(config).GetJobTimesheetList(fromDate, toDate, Isvoid: true);
		}
	}
}
