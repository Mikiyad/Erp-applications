using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CompanyDivisionSystem : MarshalByRefObject, ICompanyDivisionSystem, IDisposable
	{
		private Config config;

		public CompanyDivisionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDivision(CompanyDivisionData data)
		{
			return new CompanyDivision(config).InsertDivision(data);
		}

		public bool UpdateDivision(CompanyDivisionData data)
		{
			return UpdateDivision(data, checkConcurrency: false);
		}

		public bool UpdateDivision(CompanyDivisionData data, bool checkConcurrency)
		{
			return new CompanyDivision(config).UpdateDivision(data);
		}

		public CompanyDivisionData GetDivision()
		{
			using (CompanyDivision companyDivision = new CompanyDivision(config))
			{
				return companyDivision.GetDivision();
			}
		}

		public bool DeleteDivision(string groupID)
		{
			using (CompanyDivision companyDivision = new CompanyDivision(config))
			{
				return companyDivision.DeleteDivision(groupID);
			}
		}

		public CompanyDivisionData GetDivisionByID(string id)
		{
			using (CompanyDivision companyDivision = new CompanyDivision(config))
			{
				return companyDivision.GetDivisionByID(id);
			}
		}

		public DataSet GetDivisionByFields(params string[] columns)
		{
			using (CompanyDivision companyDivision = new CompanyDivision(config))
			{
				return companyDivision.GetDivisionByFields(columns);
			}
		}

		public DataSet GetDivisionByFields(string[] ids, params string[] columns)
		{
			using (CompanyDivision companyDivision = new CompanyDivision(config))
			{
				return companyDivision.GetDivisionByFields(ids, columns);
			}
		}

		public DataSet GetDivisionByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CompanyDivision companyDivision = new CompanyDivision(config))
			{
				return companyDivision.GetDivisionByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetDivisionList()
		{
			using (CompanyDivision companyDivision = new CompanyDivision(config))
			{
				return companyDivision.GetDivisionList();
			}
		}

		public DataSet GetDivisionComboList()
		{
			using (CompanyDivision companyDivision = new CompanyDivision(config))
			{
				return companyDivision.GetDivisionComboList();
			}
		}
	}
}
