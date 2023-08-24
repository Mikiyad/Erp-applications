using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IReligionSystem
	{
		bool CreateReligion(ReligionData religionData);

		bool UpdateReligion(ReligionData religionData);

		ReligionData GetReligion();

		bool DeleteReligion(string ID);

		ReligionData GetReligionByID(string id);

		DataSet GetReligionByFields(params string[] columns);

		DataSet GetReligionByFields(string[] ids, params string[] columns);

		DataSet GetReligionByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetReligionList();

		DataSet GetReligionComboList();
	}
}
