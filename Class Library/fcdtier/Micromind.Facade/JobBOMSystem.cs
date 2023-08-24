using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobBOMSystem : MarshalByRefObject, IJobBOMSystem, IDisposable
	{
		private Config config;

		public JobBOMSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobBOM(JobBOMData data)
		{
			return new JobBOMs(config).InsertUpdateJobBOM(data, isUpdate: false);
		}

		public bool UpdateJobBOM(JobBOMData data)
		{
			return UpdateJobBOM(data, checkConcurrency: false);
		}

		public bool UpdateJobBOM(JobBOMData data, bool checkConcurrency)
		{
			return new JobBOMs(config).InsertUpdateJobBOM(data, isUpdate: true);
		}

		public JobBOMData GetJobBOM()
		{
			using (JobBOMs jobBOMs = new JobBOMs(config))
			{
				return jobBOMs.GetJobBOM();
			}
		}

		public bool DeleteJobBOM(string groupID)
		{
			using (JobBOMs jobBOMs = new JobBOMs(config))
			{
				return jobBOMs.DeleteJobBOM(groupID);
			}
		}

		public JobBOMData GetJobBOMByID(string id)
		{
			using (JobBOMs jobBOMs = new JobBOMs(config))
			{
				return jobBOMs.GetJobBOMByID(id);
			}
		}

		public JobBOMData GetJobBOMItemsByID(string id)
		{
			using (JobBOMs jobBOMs = new JobBOMs(config))
			{
				return jobBOMs.GetJobBOMItemsByID(id);
			}
		}

		public DataSet GetJobBOMByFields(params string[] columns)
		{
			using (JobBOMs jobBOMs = new JobBOMs(config))
			{
				return jobBOMs.GetJobBOMByFields(columns);
			}
		}

		public DataSet GetJobBOMByFields(string[] ids, params string[] columns)
		{
			using (JobBOMs jobBOMs = new JobBOMs(config))
			{
				return jobBOMs.GetJobBOMByFields(ids, columns);
			}
		}

		public DataSet GetJobBOMByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (JobBOMs jobBOMs = new JobBOMs(config))
			{
				return jobBOMs.GetJobBOMByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetJobBOMList(bool inactive)
		{
			using (JobBOMs jobBOMs = new JobBOMs(config))
			{
				return jobBOMs.GetJobBOMList(inactive);
			}
		}

		public DataSet GetJobBOMComboList()
		{
			using (JobBOMs jobBOMs = new JobBOMs(config))
			{
				return jobBOMs.GetJobBOMComboList();
			}
		}
	}
}
