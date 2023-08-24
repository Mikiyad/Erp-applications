using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class TaskTypeSystem : MarshalByRefObject, ITaskTypeSystem, IDisposable
	{
		private Config config;

		public TaskTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateTaskType(TaskTypeData data)
		{
			return new TaskType(config).InsertTaskType(data);
		}

		public bool UpdateTaskType(TaskTypeData data)
		{
			return UpdateTaskType(data, checkConcurrency: false);
		}

		public bool UpdateTaskType(TaskTypeData data, bool checkConcurrency)
		{
			return new TaskType(config).UpdateTaskType(data);
		}

		public TaskTypeData GetTaskType()
		{
			using (TaskType taskType = new TaskType(config))
			{
				return taskType.GetTaskType();
			}
		}

		public bool DeleteTaskType(string groupID)
		{
			using (TaskType taskType = new TaskType(config))
			{
				return taskType.DeleteTaskType(groupID);
			}
		}

		public TaskTypeData GetTaskTypeByID(string id)
		{
			using (TaskType taskType = new TaskType(config))
			{
				return taskType.GetTaskTypeByID(id);
			}
		}

		public DataSet GetTaskTypeByFields(params string[] columns)
		{
			using (TaskType taskType = new TaskType(config))
			{
				return taskType.GetTaskTypeByFields(columns);
			}
		}

		public DataSet GetTaskTypeByFields(string[] ids, params string[] columns)
		{
			using (TaskType taskType = new TaskType(config))
			{
				return taskType.GetTaskTypeByFields(ids, columns);
			}
		}

		public DataSet GetTaskTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (TaskType taskType = new TaskType(config))
			{
				return taskType.GetTaskTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetTaskTypeList()
		{
			using (TaskType taskType = new TaskType(config))
			{
				return taskType.GetTaskTypeList();
			}
		}

		public DataSet GetTaskTypeComboList()
		{
			using (TaskType taskType = new TaskType(config))
			{
				return taskType.GetTaskTypeComboList();
			}
		}
	}
}
