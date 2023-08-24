using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CompanyAddressSystem : MarshalByRefObject, ICompanyAddressSystem, IDisposable
	{
		private Config config;

		public CompanyAddressSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCompanyAddress(CompanyAddressData data)
		{
			return new CompanyAddresses(config).InsertCompanyAddress(data);
		}

		public bool UpdateCompanyAddress(CompanyAddressData data)
		{
			return UpdateCompanyAddress(data, checkConcurrency: false);
		}

		public bool UpdateCompanyAddress(CompanyAddressData data, bool checkConcurrency)
		{
			return new CompanyAddresses(config).UpdateCompanyAddress(data);
		}

		public CompanyAddressData GetCompanyAddress()
		{
			using (CompanyAddresses companyAddresses = new CompanyAddresses(config))
			{
				return companyAddresses.GetCompanyAddress();
			}
		}

		public bool DeleteCompanyAddress(string addressID)
		{
			using (CompanyAddresses companyAddresses = new CompanyAddresses(config))
			{
				return companyAddresses.DeleteCompanyAddress(addressID);
			}
		}

		public CompanyAddressData GetCompanyAddressByID(string addressID)
		{
			using (CompanyAddresses companyAddresses = new CompanyAddresses(config))
			{
				return companyAddresses.GetCompanyAddressByID(addressID);
			}
		}

		public DataSet GetCompanyAddressByFields(params string[] columns)
		{
			using (CompanyAddresses companyAddresses = new CompanyAddresses(config))
			{
				return companyAddresses.GetCompanyAddressByFields(columns);
			}
		}

		public DataSet GetCompanyAddressByFields(string[] ids, params string[] columns)
		{
			using (CompanyAddresses companyAddresses = new CompanyAddresses(config))
			{
				return companyAddresses.GetCompanyAddressByFields(ids, columns);
			}
		}

		public DataSet GetCompanyAddressByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CompanyAddresses companyAddresses = new CompanyAddresses(config))
			{
				return companyAddresses.GetCompanyAddressByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCompanyAddressList()
		{
			using (CompanyAddresses companyAddresses = new CompanyAddresses(config))
			{
				return companyAddresses.GetCompanyAddressList();
			}
		}

		public bool IsPrimaryAddress(string addresssID)
		{
			using (CompanyAddresses companyAddresses = new CompanyAddresses(config))
			{
				return companyAddresses.IsPrimaryAddress(addresssID);
			}
		}

		public DataSet GetCompanyAddressComboList()
		{
			using (CompanyAddresses companyAddresses = new CompanyAddresses(config))
			{
				return companyAddresses.GetCompanyAddressComboList();
			}
		}
	}
}
