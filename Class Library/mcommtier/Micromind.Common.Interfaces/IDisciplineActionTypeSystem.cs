using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDisciplineActionTypeSystem
	{
		bool CreateActionType(DisciplineActionTypeData actionTypeData);

		bool UpdateActionType(DisciplineActionTypeData actionTypeData);

		DisciplineActionTypeData GetActionType();

		bool DeleteActionType(string ID);

		DisciplineActionTypeData GetActionTypeByID(string id);

		DataSet GetActionTypeByFields(params string[] columns);

		DataSet GetActionTypeByFields(string[] ids, params string[] columns);

		DataSet GetActionTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetActionTypeList();

		DataSet GetActionTypeComboList();
	}
}
