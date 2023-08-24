using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICostCenterSystem
	{
		bool CreateCostCenter(CostCenterData costCenterData);

		bool UpdateCostCenter(CostCenterData costCenterData);

		CostCenterData GetCostCenter();

		bool DeleteCostCenter(string ID);

		CostCenterData GetCostCenterByID(string id);

		DataSet GetCostCenterByFields(params string[] columns);

		DataSet GetCostCenterByFields(string[] ids, params string[] columns);

		DataSet GetCostCenterByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCostCenterList();

		DataSet GetCostCenterComboList();

		string GetDefaultAccountID(string costCenterID, string accountFieldIDName);
	}
}
