using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class NationalitySystem : MarshalByRefObject, INationalitySystem, IDisposable
	{
		private Config config;

		public NationalitySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateNationality(NationalityData data)
		{
			return new Nationality(config).InsertNationality(data);
		}

		public bool UpdateNationality(NationalityData data)
		{
			return UpdateNationality(data, checkConcurrency: false);
		}

		public bool UpdateNationality(NationalityData data, bool checkConcurrency)
		{
			return new Nationality(config).UpdateNationality(data);
		}

		public NationalityData GetNationality()
		{
			using (Nationality nationality = new Nationality(config))
			{
				return nationality.GetNationality();
			}
		}

		public bool DeleteNationality(string groupID)
		{
			using (Nationality nationality = new Nationality(config))
			{
				return nationality.DeleteNationality(groupID);
			}
		}

		public NationalityData GetNationalityByID(string id)
		{
			using (Nationality nationality = new Nationality(config))
			{
				return nationality.GetNationalityByID(id);
			}
		}

		public DataSet GetNationalityByFields(params string[] columns)
		{
			using (Nationality nationality = new Nationality(config))
			{
				return nationality.GetNationalityByFields(columns);
			}
		}

		public DataSet GetNationalityByFields(string[] ids, params string[] columns)
		{
			using (Nationality nationality = new Nationality(config))
			{
				return nationality.GetNationalityByFields(ids, columns);
			}
		}

		public DataSet GetNationalityByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Nationality nationality = new Nationality(config))
			{
				return nationality.GetNationalityByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetNationalityList()
		{
			using (Nationality nationality = new Nationality(config))
			{
				return nationality.GetNationalityList();
			}
		}

		public DataSet GetNationalityComboList()
		{
			using (Nationality nationality = new Nationality(config))
			{
				return nationality.GetNationalityComboList();
			}
		}
	}
}
