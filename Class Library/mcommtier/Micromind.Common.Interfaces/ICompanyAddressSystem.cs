using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICompanyAddressSystem
	{
		bool CreateCompanyAddress(CompanyAddressData companyAddressData);

		bool UpdateCompanyAddress(CompanyAddressData companyAddressData);

		CompanyAddressData GetCompanyAddress();

		bool DeleteCompanyAddress(string addressID);

		CompanyAddressData GetCompanyAddressByID(string addressID);

		DataSet GetCompanyAddressByFields(params string[] columns);

		DataSet GetCompanyAddressByFields(string[] ids, params string[] columns);

		DataSet GetCompanyAddressByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCompanyAddressList();

		bool IsPrimaryAddress(string addresssID);

		DataSet GetCompanyAddressComboList();
	}
}
