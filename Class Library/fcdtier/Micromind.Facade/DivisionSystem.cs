using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DivisionSystem : MarshalByRefObject, IDivisionSystem, IDisposable
	{
		private Config config;

		public DivisionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDivision(DivisionData data)
		{
			return new Division(config).InsertDivision(data);
		}

		public bool UpdateDivision(DivisionData data)
		{
			return UpdateDivision(data, checkConcurrency: false);
		}

		public bool UpdateDivision(DivisionData data, bool checkConcurrency)
		{
			return new Division(config).UpdateDivision(data);
		}

		public DivisionData GetDivision()
		{
			using (Division division = new Division(config))
			{
				return division.GetDivision();
			}
		}

		public bool DeleteDivision(string groupID)
		{
			using (Division division = new Division(config))
			{
				return division.DeleteDivision(groupID);
			}
		}

		public DivisionData GetDivisionByID(string id)
		{
			using (Division division = new Division(config))
			{
				return division.GetDivisionByID(id);
			}
		}

		public DataSet GetDivisionByFields(params string[] columns)
		{
			using (Division division = new Division(config))
			{
				return division.GetDivisionByFields(columns);
			}
		}

		public DataSet GetDivisionByFields(string[] ids, params string[] columns)
		{
			using (Division division = new Division(config))
			{
				return division.GetDivisionByFields(ids, columns);
			}
		}

		public DataSet GetDivisionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Division division = new Division(config))
			{
				return division.GetDivisionByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetDivisionList()
		{
			using (Division division = new Division(config))
			{
				return division.GetDivisionList();
			}
		}

		public DataSet GetDivisionComboList()
		{
			using (Division division = new Division(config))
			{
				return division.GetDivisionComboList();
			}
		}
	}
}
