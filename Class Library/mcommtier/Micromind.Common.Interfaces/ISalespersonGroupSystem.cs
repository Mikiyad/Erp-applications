using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalespersonGroupSystem
	{
		bool CreateSalespersonGroup(SalespersonGroupData departmentData);

		bool UpdateSalespersonGroup(SalespersonGroupData departmentData);

		SalespersonGroupData GetSalespersonGroup();

		bool DeleteSalespersonGroup(string ID);

		SalespersonGroupData GetSalespersonGroupByID(string id);

		DataSet GetSalespersonGroupByFields(params string[] columns);

		DataSet GetSalespersonGroupByFields(string[] ids, params string[] columns);

		DataSet GetSalespersonGroupByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetSalespersonGroupList();

		DataSet GetSalespersonGroupComboList();

		DataSet GetSalespersonAssignedGroupsList(string customerID);
	}
}
