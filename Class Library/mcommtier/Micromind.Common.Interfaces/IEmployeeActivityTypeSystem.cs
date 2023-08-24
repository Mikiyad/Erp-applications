using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeActivityTypeSystem
	{
		bool CreateActivityType(EmployeeActivityTypeData activityTypeData);

		bool UpdateActivityType(EmployeeActivityTypeData activityTypeData);

		EmployeeActivityTypeData GetActivityType();

		bool DeleteActivityType(string ID);

		EmployeeActivityTypeData GetActivityTypeByID(string id);

		DataSet GetActivityTypeByFields(params string[] columns);

		DataSet GetActivityTypeByFields(string[] ids, params string[] columns);

		DataSet GetActivityTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetActivityTypeList();

		DataSet GetActivityTypeComboList();
	}
}
