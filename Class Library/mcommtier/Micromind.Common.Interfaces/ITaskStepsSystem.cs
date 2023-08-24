using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ITaskStepsSystem
	{
		bool CreateTaskSteps(TaskStepsData TaskStepsData);

		bool UpdateTaskSteps(TaskStepsData TaskStepsData);

		TaskStepsData GetTaskSteps();

		bool DeleteTaskSteps(string ID);

		TaskStepsData GetTaskStepsByID(string id);

		DataSet GetTaskStepsByFields(params string[] columns);

		DataSet GetTaskStepsByFields(string[] ids, params string[] columns);

		DataSet GetTaskStepsByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetTaskStepsList();

		DataSet GetTaskStepsComboList();
	}
}
