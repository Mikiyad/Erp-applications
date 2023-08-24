using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class TaskStepsSystem : MarshalByRefObject, ITaskStepsSystem, IDisposable
	{
		private Config config;

		public TaskStepsSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateTaskSteps(TaskStepsData data)
		{
			return new TaskSteps(config).InsertTaskSteps(data);
		}

		public bool UpdateTaskSteps(TaskStepsData data)
		{
			return UpdateTaskSteps(data, checkConcurrency: false);
		}

		public bool UpdateTaskSteps(TaskStepsData data, bool checkConcurrency)
		{
			return new TaskSteps(config).UpdateTaskSteps(data);
		}

		public TaskStepsData GetTaskSteps()
		{
			using (TaskSteps taskSteps = new TaskSteps(config))
			{
				return taskSteps.GetTaskSteps();
			}
		}

		public bool DeleteTaskSteps(string groupID)
		{
			using (TaskSteps taskSteps = new TaskSteps(config))
			{
				return taskSteps.DeleteTaskSteps(groupID);
			}
		}

		public TaskStepsData GetTaskStepsByID(string id)
		{
			using (TaskSteps taskSteps = new TaskSteps(config))
			{
				return taskSteps.GetTaskStepsByID(id);
			}
		}

		public DataSet GetTaskStepsByFields(params string[] columns)
		{
			using (TaskSteps taskSteps = new TaskSteps(config))
			{
				return taskSteps.GetTaskStepsByFields(columns);
			}
		}

		public DataSet GetTaskStepsByFields(string[] ids, params string[] columns)
		{
			using (TaskSteps taskSteps = new TaskSteps(config))
			{
				return taskSteps.GetTaskStepsByFields(ids, columns);
			}
		}

		public DataSet GetTaskStepsByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (TaskSteps taskSteps = new TaskSteps(config))
			{
				return taskSteps.GetTaskStepsByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetTaskStepsList()
		{
			using (TaskSteps taskSteps = new TaskSteps(config))
			{
				return taskSteps.GetTaskStepsList();
			}
		}

		public DataSet GetTaskStepsComboList()
		{
			using (TaskSteps taskSteps = new TaskSteps(config))
			{
				return taskSteps.GetTaskStepsComboList();
			}
		}
	}
}
