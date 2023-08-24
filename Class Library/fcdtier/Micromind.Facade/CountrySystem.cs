using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CountrySystem : MarshalByRefObject, ICountrySystem, IDisposable
	{
		private Config config;

		public CountrySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCountry(CountryData data)
		{
			return new Country(config).InsertCountry(data);
		}

		public bool UpdateCountry(CountryData data)
		{
			return UpdateCountry(data, checkConcurrency: false);
		}

		public bool UpdateCountry(CountryData data, bool checkConcurrency)
		{
			return new Country(config).UpdateCountry(data);
		}

		public CountryData GetCountry()
		{
			using (Country country = new Country(config))
			{
				return country.GetCountry();
			}
		}

		public bool DeleteCountry(string groupID)
		{
			using (Country country = new Country(config))
			{
				return country.DeleteCountry(groupID);
			}
		}

		public CountryData GetCountryByID(string id)
		{
			using (Country country = new Country(config))
			{
				return country.GetCountryByID(id);
			}
		}

		public DataSet GetCountryByFields(params string[] columns)
		{
			using (Country country = new Country(config))
			{
				return country.GetCountryByFields(columns);
			}
		}

		public DataSet GetCountryByFields(string[] ids, params string[] columns)
		{
			using (Country country = new Country(config))
			{
				return country.GetCountryByFields(ids, columns);
			}
		}

		public DataSet GetCountryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Country country = new Country(config))
			{
				return country.GetCountryByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCountryList()
		{
			using (Country country = new Country(config))
			{
				return country.GetCountryList();
			}
		}

		public DataSet GetCountryComboList()
		{
			using (Country country = new Country(config))
			{
				return country.GetCountryComboList();
			}
		}
	}
}
