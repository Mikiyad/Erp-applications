using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobTypeSystem : MarshalByRefObject, IJobTypeSystem, IDisposable
	{
		private Config config;

		public JobTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobType(JobTypeData data)
		{
			return new JobType(config).InsertJobType(data);
		}

		public bool UpdateJobType(JobTypeData data)
		{
			return UpdateJobType(data, checkConcurrency: false);
		}

		public bool UpdateJobType(JobTypeData data, bool checkConcurrency)
		{
			return new JobType(config).UpdateJobType(data);
		}

		public JobTypeData GetJobType()
		{
			using (JobType jobType = new JobType(config))
			{
				return jobType.GetJobType();
			}
		}

		public bool DeleteJobType(string groupID)
		{
			using (JobType jobType = new JobType(config))
			{
				return jobType.DeleteJobType(groupID);
			}
		}

		public JobTypeData GetJobTypeByID(string id)
		{
			using (JobType jobType = new JobType(config))
			{
				return jobType.GetJobTypeByID(id);
			}
		}

		public DataSet GetJobTypeByFields(params string[] columns)
		{
			using (JobType jobType = new JobType(config))
			{
				return jobType.GetJobTypeByFields(columns);
			}
		}

		public DataSet GetJobTypeByFields(string[] ids, params string[] columns)
		{
			using (JobType jobType = new JobType(config))
			{
				return jobType.GetJobTypeByFields(ids, columns);
			}
		}

		public DataSet GetJobTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (JobType jobType = new JobType(config))
			{
				return jobType.GetJobTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetJobTypeList()
		{
			using (JobType jobType = new JobType(config))
			{
				return jobType.GetJobTypeList();
			}
		}

		public DataSet GetJobTypeComboList()
		{
			using (JobType jobType = new JobType(config))
			{
				return jobType.GetJobTypeComboList();
			}
		}
	}
}
