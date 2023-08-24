using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface INationalitySystem
	{
		bool CreateNationality(NationalityData nationalityData);

		bool UpdateNationality(NationalityData nationalityData);

		NationalityData GetNationality();

		bool DeleteNationality(string ID);

		NationalityData GetNationalityByID(string id);

		DataSet GetNationalityByFields(params string[] columns);

		DataSet GetNationalityByFields(string[] ids, params string[] columns);

		DataSet GetNationalityByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetNationalityList();

		DataSet GetNationalityComboList();
	}
}
