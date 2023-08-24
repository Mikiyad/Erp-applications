using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICostCategorySystem
	{
		bool CreateCostCategory(CostCategoryData costCategoryData);

		bool UpdateCostCategory(CostCategoryData costCategoryData);

		CostCategoryData GetCostCategory();

		bool DeleteCostCategory(string ID);

		CostCategoryData GetCostCategoryByID(string id);

		DataSet GetCostCategoryByFields(params string[] columns);

		DataSet GetCostCategoryByFields(string[] ids, params string[] columns);

		DataSet GetCostCategoryByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCostCategoryList();

		DataSet GetCostCategoryComboList();
	}
}
