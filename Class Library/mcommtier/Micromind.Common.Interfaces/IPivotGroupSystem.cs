using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPivotGroupSystem
	{
		bool CreatePivotGroup(PivotGroupData chartGroupData);

		bool UpdatePivotGroup(PivotGroupData chartGroupData);

		PivotGroupData GetPivotGroup();

		bool DeletePivotGroup(string ID);

		PivotGroupData GetPivotGroupByID(string id);

		DataSet GetPivotGroupByFields(params string[] columns);

		DataSet GetPivotGroupByFields(string[] ids, params string[] columns);

		DataSet GetPivotGroupByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPivotGroupList();

		DataSet GetPivotGroupComboList();

		int CreateGroup(string groupName);
	}
}
