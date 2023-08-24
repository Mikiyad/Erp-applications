using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobTaskGroupSystem
	{
		bool CreateJobTaskGroup(JobTaskGroupData jobTaskGroupData);

		bool UpdateJobTaskGroup(JobTaskGroupData jobTaskGroupData);

		JobTaskGroupData GetJobTaskGroup();

		bool DeleteJobTaskGroup(string ID);

		JobTaskGroupData GetJobTaskGroupByID(string id);

		DataSet GetJobTaskGroupByFields(params string[] columns);

		DataSet GetJobTaskGroupByFields(string[] ids, params string[] columns);

		DataSet GetJobTaskGroupByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetJobTaskGroupList();

		DataSet GetJobTaskGroupComboList();
	}
}
