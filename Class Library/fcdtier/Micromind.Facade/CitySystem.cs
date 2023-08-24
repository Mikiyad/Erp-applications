using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CitySystem : MarshalByRefObject, ICitySystem, IDisposable
	{
		private Config config;

		public CitySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCity(CityData data)
		{
			return new City(config).InsertCity(data);
		}

		public bool UpdateCity(CityData data)
		{
			return UpdateCity(data, checkConcurrency: false);
		}

		public bool UpdateCity(CityData data, bool checkConcurrency)
		{
			return new City(config).UpdateCity(data);
		}

		public CityData GetCity()
		{
			using (City city = new City(config))
			{
				return city.GetCity();
			}
		}

		public bool DeleteCity(string groupID)
		{
			using (City city = new City(config))
			{
				return city.DeleteCity(groupID);
			}
		}

		public CityData GetCityByID(string id)
		{
			using (City city = new City(config))
			{
				return city.GetCityByID(id);
			}
		}

		public DataSet GetCityByFields(params string[] columns)
		{
			using (City city = new City(config))
			{
				return city.GetCityByFields(columns);
			}
		}

		public DataSet GetCityByFields(string[] ids, params string[] columns)
		{
			using (City city = new City(config))
			{
				return city.GetCityByFields(ids, columns);
			}
		}

		public DataSet GetCityByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (City city = new City(config))
			{
				return city.GetCityByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCityList()
		{
			using (City city = new City(config))
			{
				return city.GetCityList();
			}
		}

		public DataSet GetCityComboList()
		{
			using (City city = new City(config))
			{
				return city.GetCityComboList();
			}
		}
	}
}
