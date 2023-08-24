using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICitySystem
	{
		bool CreateCity(CityData cityData);

		bool UpdateCity(CityData cityData);

		CityData GetCity();

		bool DeleteCity(string ID);

		CityData GetCityByID(string id);

		DataSet GetCityByFields(params string[] columns);

		DataSet GetCityByFields(string[] ids, params string[] columns);

		DataSet GetCityByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCityList();

		DataSet GetCityComboList();
	}
}
