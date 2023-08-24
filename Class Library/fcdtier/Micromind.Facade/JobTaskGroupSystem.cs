using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobTaskGroupSystem : MarshalByRefObject, IJobTaskGroupSystem, IDisposable
	{
		private Config config;

		public JobTaskGroupSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobTaskGroup(JobTaskGroupData data)
		{
			return new JobTaskGroup(config).InsertJobTaskGroup(data);
		}

		public bool UpdateJobTaskGroup(JobTaskGroupData data)
		{
			return UpdateJobTaskGroup(data, checkConcurrency: false);
		}

		public bool UpdateJobTaskGroup(JobTaskGroupData data, bool checkConcurrency)
		{
			return new JobTaskGroup(config).UpdateJobTaskGroup(data);
		}

		public JobTaskGroupData GetJobTaskGroup()
		{
			using (JobTaskGroup jobTaskGroup = new JobTaskGroup(config))
			{
				return jobTaskGroup.GetJobTaskGroup();
			}
		}

		public bool DeleteJobTaskGroup(string groupID)
		{
			using (JobTaskGroup jobTaskGroup = new JobTaskGroup(config))
			{
				return jobTaskGroup.DeleteJobTaskGroup(groupID);
			}
		}

		public JobTaskGroupData GetJobTaskGroupByID(string id)
		{
			using (JobTaskGroup jobTaskGroup = new JobTaskGroup(config))
			{
				return jobTaskGroup.GetJobTaskGroupByID(id);
			}
		}

		public DataSet GetJobTaskGroupByFields(params string[] columns)
		{
			using (JobTaskGroup jobTaskGroup = new JobTaskGroup(config))
			{
				return jobTaskGroup.GetJobTaskGroupByFields(columns);
			}
		}

		public DataSet GetJobTaskGroupByFields(string[] ids, params string[] columns)
		{
			using (JobTaskGroup jobTaskGroup = new JobTaskGroup(config))
			{
				return jobTaskGroup.GetJobTaskGroupByFields(ids, columns);
			}
		}

		public DataSet GetJobTaskGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (JobTaskGroup jobTaskGroup = new JobTaskGroup(config))
			{
				return jobTaskGroup.GetJobTaskGroupByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetJobTaskGroupList()
		{
			using (JobTaskGroup jobTaskGroup = new JobTaskGroup(config))
			{
				return jobTaskGroup.GetJobTaskGroupList();
			}
		}

		public DataSet GetJobTaskGroupComboList()
		{
			using (JobTaskGroup jobTaskGroup = new JobTaskGroup(config))
			{
				return jobTaskGroup.GetJobTaskGroupComboList();
			}
		}
	}
}
