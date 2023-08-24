using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class QualityTaskSystem : MarshalByRefObject, IQualityTaskSystem, IDisposable
	{
		private Config config;

		public QualityTaskSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateQualityTask(QualityTaskData data)
		{
			return new QualityTask(config).InsertQualityTask(data);
		}

		public bool UpdateQualityTask(QualityTaskData data)
		{
			return UpdateQualityTask(data, checkConcurrency: false);
		}

		public bool UpdateQualityTask(QualityTaskData data, bool checkConcurrency)
		{
			return new QualityTask(config).UpdateQualityTask(data);
		}

		public QualityTaskData GetQualityTask()
		{
			using (QualityTask qualityTask = new QualityTask(config))
			{
				return qualityTask.GetQualityTask();
			}
		}

		public bool DeleteQualityTask(string taskID)
		{
			using (QualityTask qualityTask = new QualityTask(config))
			{
				return qualityTask.DeleteQualityTask(taskID);
			}
		}

		public QualityTaskData GetQualityTaskByID(string id)
		{
			using (QualityTask qualityTask = new QualityTask(config))
			{
				return qualityTask.GetQualityTaskByID(id);
			}
		}

		public DataSet GetQualityTaskByFields(params string[] columns)
		{
			using (QualityTask qualityTask = new QualityTask(config))
			{
				return qualityTask.GetQualityTaskByFields(columns);
			}
		}

		public DataSet GetQualityTaskByFields(string[] ids, params string[] columns)
		{
			using (QualityTask qualityTask = new QualityTask(config))
			{
				return qualityTask.GetQualityTaskByFields(ids, columns);
			}
		}

		public DataSet GetQualityTaskByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (QualityTask qualityTask = new QualityTask(config))
			{
				return qualityTask.GetQualityTaskByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetQualityTaskList(bool includeClosedTasks)
		{
			using (QualityTask qualityTask = new QualityTask(config))
			{
				return qualityTask.GetQualityTaskList(includeClosedTasks);
			}
		}

		public DataSet GetQualityTaskComboList()
		{
			using (QualityTask qualityTask = new QualityTask(config))
			{
				return qualityTask.GetQualityTaskComboList();
			}
		}
	}
}
