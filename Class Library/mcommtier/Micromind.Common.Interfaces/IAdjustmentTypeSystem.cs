using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IAdjustmentTypeSystem
	{
		bool CreateAdjustmentType(AdjustmentTypeData adjustmentTypeData);

		bool UpdateAdjustmentType(AdjustmentTypeData adjustmentTypeData);

		AdjustmentTypeData GetAdjustmentType();

		bool DeleteAdjustmentType(string ID);

		AdjustmentTypeData GetAdjustmentTypeByID(string id);

		DataSet GetAdjustmentTypeByFields(params string[] columns);

		DataSet GetAdjustmentTypeByFields(string[] ids, params string[] columns);

		DataSet GetAdjustmentTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetAdjustmentTypeList();

		DataSet GetAdjustmentTypeComboList();
	}
}
