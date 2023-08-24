using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CompanyOptionSystem : MarshalByRefObject, ICompanyOptionSystem, IDisposable
	{
		private Config config;

		public CompanyOptionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCompanyOption(CompanyOptionData data)
		{
			return new CompanyOption(config).InsertUpdateCompanyOption(data, isUpdate: false);
		}

		public bool CreateCompanyOption(CompanyOptionData data, int groupID)
		{
			return new CompanyOption(config).InsertUpdateCompanyOption(data, groupID, isUpdate: false);
		}

		public bool CreateSysDocCompanyOption(CompanyOptionData data, int groupID)
		{
			return new CompanyOption(config).InsertUpdateCompanyOption(data, groupID, isUpdate: false, isSysDocOption: true);
		}

		public CompanyOptionData GetCompanyOption()
		{
			using (CompanyOption companyOption = new CompanyOption(config))
			{
				return companyOption.GetCompanyOption();
			}
		}

		public CompanyOptionData GetCompanyOption(int groupID)
		{
			using (CompanyOption companyOption = new CompanyOption(config))
			{
				return companyOption.GetCompanyOption(groupID);
			}
		}

		public bool DeleteCompanyOption(string groupID)
		{
			using (CompanyOption companyOption = new CompanyOption(config))
			{
				return companyOption.DeleteCompanyOption(groupID);
			}
		}

		public CompanyOptionData GetCompanyOptionByID(string id)
		{
			using (CompanyOption companyOption = new CompanyOption(config))
			{
				return companyOption.GetCompanyOptionByID(id);
			}
		}

		public DataSet GetCompanyOptionList(int groupID)
		{
			using (CompanyOption companyOption = new CompanyOption(config))
			{
				return companyOption.GetCompanyOptionList(groupID);
			}
		}

		public DataSet GetCompanyOptionList()
		{
			using (CompanyOption companyOption = new CompanyOption(config))
			{
				return companyOption.GetCompanyOptionList();
			}
		}

		public DataSet GetSysDocCompanyOptionList(int groupID)
		{
			using (CompanyOption companyOption = new CompanyOption(config))
			{
				return companyOption.GetSysDocCompanyOptionList(groupID);
			}
		}

		public DataSet GetSysDocCompanyOptionList()
		{
			using (CompanyOption companyOption = new CompanyOption(config))
			{
				return companyOption.GetSysDocCompanyOptionList();
			}
		}
	}
}
