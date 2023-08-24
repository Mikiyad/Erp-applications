using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CompanyDocTypeSystem : MarshalByRefObject, ICompanyDocTypeSystem, IDisposable
	{
		private Config config;

		public CompanyDocTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCompanyDocType(CompanyDocTypeData data)
		{
			return new CompanyDocType(config).InsertCompanyDocType(data);
		}

		public bool UpdateCompanyDocType(CompanyDocTypeData data)
		{
			return UpdateCompanyDocType(data, checkConcurrency: false);
		}

		public bool UpdateCompanyDocType(CompanyDocTypeData data, bool checkConcurrency)
		{
			return new CompanyDocType(config).UpdateCompanyDocType(data);
		}

		public CompanyDocTypeData GetCompanyDocType()
		{
			using (CompanyDocType companyDocType = new CompanyDocType(config))
			{
				return companyDocType.GetCompanyDocType();
			}
		}

		public bool DeleteCompanyDocType(string groupID)
		{
			using (CompanyDocType companyDocType = new CompanyDocType(config))
			{
				return companyDocType.DeleteCompanyDocType(groupID);
			}
		}

		public CompanyDocTypeData GetCompanyDocTypeByID(string id)
		{
			using (CompanyDocType companyDocType = new CompanyDocType(config))
			{
				return companyDocType.GetCompanyDocTypeByID(id);
			}
		}

		public DataSet GetCompanyDocTypeByFields(params string[] columns)
		{
			using (CompanyDocType companyDocType = new CompanyDocType(config))
			{
				return companyDocType.GetCompanyDocTypeByFields(columns);
			}
		}

		public DataSet GetCompanyDocTypeByFields(string[] ids, params string[] columns)
		{
			using (CompanyDocType companyDocType = new CompanyDocType(config))
			{
				return companyDocType.GetCompanyDocTypeByFields(ids, columns);
			}
		}

		public DataSet GetCompanyDocTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CompanyDocType companyDocType = new CompanyDocType(config))
			{
				return companyDocType.GetCompanyDocTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCompanyDocTypeList()
		{
			using (CompanyDocType companyDocType = new CompanyDocType(config))
			{
				return companyDocType.GetCompanyDocTypeList();
			}
		}

		public DataSet GetCompanyDocTypeComboList()
		{
			using (CompanyDocType companyDocType = new CompanyDocType(config))
			{
				return companyDocType.GetCompanyDocTypeComboList();
			}
		}
	}
}
