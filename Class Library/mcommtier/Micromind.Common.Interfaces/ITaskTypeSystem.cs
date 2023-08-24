using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ITaskTypeSystem
	{
		bool CreateTaskType(TaskTypeData TaskTypeData);

		bool UpdateTaskType(TaskTypeData TaskTypeData);

		TaskTypeData GetTaskType();

		bool DeleteTaskType(string ID);

		TaskTypeData GetTaskTypeByID(string id);

		DataSet GetTaskTypeByFields(params string[] columns);

		DataSet GetTaskTypeByFields(string[] ids, params string[] columns);

		DataSet GetTaskTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetTaskTypeList();

		DataSet GetTaskTypeComboList();
	}
}
