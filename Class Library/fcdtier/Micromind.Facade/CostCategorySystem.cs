using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CostCategorySystem : MarshalByRefObject, ICostCategorySystem, IDisposable
	{
		private Config config;

		public CostCategorySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCostCategory(CostCategoryData data)
		{
			return new CostCategory(config).InsertCostCategory(data);
		}

		public bool UpdateCostCategory(CostCategoryData data)
		{
			return UpdateCostCategory(data, checkConcurrency: false);
		}

		public bool UpdateCostCategory(CostCategoryData data, bool checkConcurrency)
		{
			return new CostCategory(config).UpdateCostCategory(data);
		}

		public CostCategoryData GetCostCategory()
		{
			using (CostCategory costCategory = new CostCategory(config))
			{
				return costCategory.GetCostCategory();
			}
		}

		public bool DeleteCostCategory(string groupID)
		{
			using (CostCategory costCategory = new CostCategory(config))
			{
				return costCategory.DeleteCostCategory(groupID);
			}
		}

		public CostCategoryData GetCostCategoryByID(string id)
		{
			using (CostCategory costCategory = new CostCategory(config))
			{
				return costCategory.GetCostCategoryByID(id);
			}
		}

		public DataSet GetCostCategoryByFields(params string[] columns)
		{
			using (CostCategory costCategory = new CostCategory(config))
			{
				return costCategory.GetCostCategoryByFields(columns);
			}
		}

		public DataSet GetCostCategoryByFields(string[] ids, params string[] columns)
		{
			using (CostCategory costCategory = new CostCategory(config))
			{
				return costCategory.GetCostCategoryByFields(ids, columns);
			}
		}

		public DataSet GetCostCategoryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CostCategory costCategory = new CostCategory(config))
			{
				return costCategory.GetCostCategoryByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCostCategoryList()
		{
			using (CostCategory costCategory = new CostCategory(config))
			{
				return costCategory.GetCostCategoryList();
			}
		}

		public DataSet GetCostCategoryComboList()
		{
			using (CostCategory costCategory = new CostCategory(config))
			{
				return costCategory.GetCostCategoryComboList();
			}
		}
	}
}
