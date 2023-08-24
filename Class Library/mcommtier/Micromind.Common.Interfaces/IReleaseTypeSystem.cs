using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IReleaseTypeSystem
	{
		bool CreateReleaseType(ReleaseTypeData ReleaseTypeData);

		bool UpdateReleaseType(ReleaseTypeData ReleaseTypeData);

		ReleaseTypeData GetReleaseType();

		bool DeleteReleaseType(string ID);

		ReleaseTypeData GetReleaseTypeByID(string id);

		DataSet GetReleaseTypeByFields(params string[] columns);

		DataSet GetReleaseTypeByFields(string[] ids, params string[] columns);

		DataSet GetReleaseTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetReleaseTypeList();

		DataSet GetReleaseTypeComboList();
	}
}
