using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICountrySystem
	{
		bool CreateCountry(CountryData countryData);

		bool UpdateCountry(CountryData countryData);

		CountryData GetCountry();

		bool DeleteCountry(string ID);

		CountryData GetCountryByID(string id);

		DataSet GetCountryByFields(params string[] columns);

		DataSet GetCountryByFields(string[] ids, params string[] columns);

		DataSet GetCountryByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCountryList();

		DataSet GetCountryComboList();
	}
}
