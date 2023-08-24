using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IJobBOMSystem
	{
		bool CreateJobBOM(JobBOMData bomData);

		bool UpdateJobBOM(JobBOMData bomData);

		JobBOMData GetJobBOM();

		bool DeleteJobBOM(string ID);

		JobBOMData GetJobBOMByID(string id);

		JobBOMData GetJobBOMItemsByID(string id);

		DataSet GetJobBOMByFields(params string[] columns);

		DataSet GetJobBOMByFields(string[] ids, params string[] columns);

		DataSet GetJobBOMByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetJobBOMList(bool isInactive);

		DataSet GetJobBOMComboList();
	}
}
