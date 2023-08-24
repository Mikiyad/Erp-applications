using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CostCenterSystem : MarshalByRefObject, ICostCenterSystem, IDisposable
	{
		private Config config;

		public CostCenterSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCostCenter(CostCenterData data)
		{
			return new CostCenter(config).InsertCostCenter(data);
		}

		public bool UpdateCostCenter(CostCenterData data)
		{
			return UpdateCostCenter(data, checkConcurrency: false);
		}

		public bool UpdateCostCenter(CostCenterData data, bool checkConcurrency)
		{
			return new CostCenter(config).UpdateCostCenter(data);
		}

		public CostCenterData GetCostCenter()
		{
			using (CostCenter costCenter = new CostCenter(config))
			{
				return costCenter.GetCostCenter();
			}
		}

		public bool DeleteCostCenter(string groupID)
		{
			using (CostCenter costCenter = new CostCenter(config))
			{
				return costCenter.DeleteCostCenter(groupID);
			}
		}

		public CostCenterData GetCostCenterByID(string id)
		{
			using (CostCenter costCenter = new CostCenter(config))
			{
				return costCenter.GetCostCenterByID(id);
			}
		}

		public DataSet GetCostCenterByFields(params string[] columns)
		{
			using (CostCenter costCenter = new CostCenter(config))
			{
				return costCenter.GetCostCenterByFields(columns);
			}
		}

		public DataSet GetCostCenterByFields(string[] ids, params string[] columns)
		{
			using (CostCenter costCenter = new CostCenter(config))
			{
				return costCenter.GetCostCenterByFields(ids, columns);
			}
		}

		public DataSet GetCostCenterByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CostCenter costCenter = new CostCenter(config))
			{
				return costCenter.GetCostCenterByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCostCenterList()
		{
			using (CostCenter costCenter = new CostCenter(config))
			{
				return costCenter.GetCostCenterList();
			}
		}

		public DataSet GetCostCenterComboList()
		{
			using (CostCenter costCenter = new CostCenter(config))
			{
				return costCenter.GetCostCenterComboList();
			}
		}

		public string GetDefaultAccountID(string costCenterID, string accountFieldIDName)
		{
			using (CostCenter costCenter = new CostCenter(config))
			{
				return costCenter.GetDefaultAccountID(costCenterID, accountFieldIDName);
			}
		}
	}
}
