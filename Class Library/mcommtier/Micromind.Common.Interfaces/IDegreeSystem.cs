using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDegreeSystem
	{
		bool CreateDegree(DegreeData degreeData);

		bool UpdateDegree(DegreeData degreeData);

		DegreeData GetDegree();

		bool DeleteDegree(string ID);

		DegreeData GetDegreeByID(string id);

		DataSet GetDegreeByFields(params string[] columns);

		DataSet GetDegreeByFields(string[] ids, params string[] columns);

		DataSet GetDegreeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetDegreeList();

		DataSet GetDegreeComboList();
	}
}
