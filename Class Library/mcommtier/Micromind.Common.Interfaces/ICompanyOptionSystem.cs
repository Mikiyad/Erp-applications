using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICompanyOptionSystem
	{
		bool CreateCompanyOption(CompanyOptionData companyOptionData);

		bool CreateCompanyOption(CompanyOptionData companyOptionData, int groupID);

		bool CreateSysDocCompanyOption(CompanyOptionData companyOptionData, int groupID);

		CompanyOptionData GetCompanyOption();

		CompanyOptionData GetCompanyOption(int groupID);

		bool DeleteCompanyOption(string ID);

		CompanyOptionData GetCompanyOptionByID(string id);

		DataSet GetCompanyOptionList();

		DataSet GetCompanyOptionList(int groupID);

		DataSet GetSysDocCompanyOptionList(int groupID);

		DataSet GetSysDocCompanyOptionList();
	}
}
