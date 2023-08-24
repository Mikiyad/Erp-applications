using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProvisionTypeSystem
	{
		bool CreateProvisionType(EmployeeProvisionTypeData sponsorData);

		bool UpdateProvisionType(EmployeeProvisionTypeData sponsorData);

		EmployeeProvisionTypeData GetProvisionType();

		bool DeleteProvisionType(string ID);

		EmployeeProvisionTypeData GetProvisionTypeByID(string id);

		DataSet GetProvisionTypeByFields(params string[] columns);

		DataSet GetProvisionTypeByFields(string[] ids, params string[] columns);

		DataSet GetProvisionTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetProvisionTypeList();

		DataSet GetProvisionTypeComboList();
	}
}
