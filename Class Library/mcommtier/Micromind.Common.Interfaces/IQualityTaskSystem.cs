using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IQualityTaskSystem
	{
		bool CreateQualityTask(QualityTaskData qualityTaskData);

		bool UpdateQualityTask(QualityTaskData qualityTaskData);

		QualityTaskData GetQualityTask();

		bool DeleteQualityTask(string ID);

		QualityTaskData GetQualityTaskByID(string id);

		DataSet GetQualityTaskByFields(params string[] columns);

		DataSet GetQualityTaskByFields(string[] ids, params string[] columns);

		DataSet GetQualityTaskByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetQualityTaskList(bool includeClosedTasks);

		DataSet GetQualityTaskComboList();
	}
}
