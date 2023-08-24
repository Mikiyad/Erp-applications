using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDimensionSystem
	{
		bool CreateDimension(DimensionData productDimensionData);

		bool UpdateDimension(DimensionData productDimensionData);

		DimensionData GetDimension();

		bool DeleteDimension(string ID);

		DimensionData GetDimensionByID(string id);

		DataSet GetDimensionByFields(params string[] columns);

		DataSet GetDimensionByFields(string[] ids, params string[] columns);

		DataSet GetDimensionByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetDimensionList();

		DataSet GetDimensionComboList();

		DataSet GetDimensionAttributes(string dimensionID);
	}
}
