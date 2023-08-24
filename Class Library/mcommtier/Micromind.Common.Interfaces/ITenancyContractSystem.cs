using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ITenancyContractSystem
	{
		bool CreateTenancyContract(TenancyContractData tenancyContractData);

		bool UpdateTenancyContract(TenancyContractData tenancyContractData);

		TenancyContractData GetTenancyContract();

		bool DeleteTenancyContract(string ID);

		TenancyContractData GetTenancyContractByID(string id);

		DataSet GetTenancyContractByFields(params string[] columns);

		DataSet GetTenancyContractByFields(string[] ids, params string[] columns);

		DataSet GetTenancyContractByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetTenancyContractList();

		DataSet GetTenancyContractComboList();
	}
}
