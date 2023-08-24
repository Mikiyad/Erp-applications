using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobTypeSystem
	{
		bool CreateJobType(JobTypeData jobTypeData);

		bool UpdateJobType(JobTypeData jobTypeData);

		JobTypeData GetJobType();

		bool DeleteJobType(string ID);

		JobTypeData GetJobTypeByID(string id);

		DataSet GetJobTypeByFields(params string[] columns);

		DataSet GetJobTypeByFields(string[] ids, params string[] columns);

		DataSet GetJobTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetJobTypeList();

		DataSet GetJobTypeComboList();
	}
}
