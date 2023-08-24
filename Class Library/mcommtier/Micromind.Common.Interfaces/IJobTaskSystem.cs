using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobTaskSystem
	{
		bool CreateJobTask(JobTaskData jobTaskData);

		bool UpdateJobTask(JobTaskData jobTaskData);

		JobTaskData GetJobTask();

		bool DeleteJobTask(string ID);

		JobTaskData GetJobTaskByID(string id);

		DataSet GetJobTaskByFields(params string[] columns);

		DataSet GetJobTaskByFields(string[] ids, params string[] columns);

		DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetJobTaskList();

		DataSet GetJobTaskListToPrint();

		DataSet GetJobTaskComboList();
	}
}
