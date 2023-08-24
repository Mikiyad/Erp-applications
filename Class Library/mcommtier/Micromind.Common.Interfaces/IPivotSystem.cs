using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPivotSystem
	{
		bool InsertUpdatePivot(PivotData pivotData, bool isUpdate);

		PivotData GetPivot();

		bool DeletePivot(string ID);

		PivotData GetPivotByID(string id);

		DataSet GetPivotByFields(params string[] columns);

		DataSet GetPivotByFields(string[] ids, params string[] columns);

		DataSet GetPivotByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPivotList();

		DataSet GetPivotComboList();
	}
}
