using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IInsuranceProviderSystem
	{
		bool CreateInsuranceProvider(InsuranceProviderData InsuranceProviderData);

		bool UpdateInsuranceProvider(InsuranceProviderData InsuranceProviderData);

		InsuranceProviderData GetInsuranceProvider();

		bool DeleteInsuranceProvider(string ID);

		InsuranceProviderData GetInsuranceProviderByID(string id);

		DataSet GetInsuranceProviderByFields(params string[] columns);

		DataSet GetInsuranceProviderByFields(string[] ids, params string[] columns);

		DataSet GetInsuranceProviderByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetInsuranceProviderList();

		DataSet GetInsuranceProviderComboList();

		DataSet GetMedicalInsuranceProviderComboList();
	}
}
