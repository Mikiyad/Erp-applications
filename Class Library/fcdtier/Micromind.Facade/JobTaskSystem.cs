using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class JobTaskSystem : MarshalByRefObject, IJobTaskSystem, IDisposable
	{
		private Config config;

		public JobTaskSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateJobTask(JobTaskData data)
		{
			return new JobTask(config).InsertJobTask(data);
		}

		public bool UpdateJobTask(JobTaskData data)
		{
			return UpdateJobTask(data, checkConcurrency: false);
		}

		public bool UpdateJobTask(JobTaskData data, bool checkConcurrency)
		{
			return new JobTask(config).UpdateJobTask(data);
		}

		public JobTaskData GetJobTask()
		{
			using (JobTask jobTask = new JobTask(config))
			{
				return jobTask.GetJobTask();
			}
		}

		public bool DeleteJobTask(string groupID)
		{
			using (JobTask jobTask = new JobTask(config))
			{
				return jobTask.DeleteJobTask(groupID);
			}
		}

		public JobTaskData GetJobTaskByID(string id)
		{
			using (JobTask jobTask = new JobTask(config))
			{
				return jobTask.GetJobTaskByID(id);
			}
		}

		public DataSet GetJobTaskByFields(params string[] columns)
		{
			using (JobTask jobTask = new JobTask(config))
			{
				return jobTask.GetJobTaskByFields(columns);
			}
		}

		public DataSet GetJobTaskByFields(string[] ids, params string[] columns)
		{
			using (JobTask jobTask = new JobTask(config))
			{
				return jobTask.GetJobTaskByFields(ids, columns);
			}
		}

		public DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (JobTask jobTask = new JobTask(config))
			{
				return jobTask.GetJobTaskByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetJobTaskList()
		{
			using (JobTask jobTask = new JobTask(config))
			{
				return jobTask.GetJobTaskList();
			}
		}

		public DataSet GetJobTaskListToPrint()
		{
			using (JobTask jobTask = new JobTask(config))
			{
				return jobTask.GetJobTaskListToPrint();
			}
		}

		public DataSet GetJobTaskComboList()
		{
			using (JobTask jobTask = new JobTask(config))
			{
				return jobTask.GetJobTaskComboList();
			}
		}
	}
}
