using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class FixedAssetSystem : MarshalByRefObject, IFixedAssetSystem, IDisposable
	{
		private Config config;

		public FixedAssetSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateAsset(FixedAssetData data)
		{
			return new FixedAsset(config).InsertAsset(data);
		}

		public bool UpdateAsset(FixedAssetData data)
		{
			return UpdateAsset(data, checkConcurrency: false);
		}

		public bool UpdateAsset(FixedAssetData data, bool checkConcurrency)
		{
			return new FixedAsset(config).UpdateAsset(data);
		}

		public FixedAssetData GetAsset()
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAsset();
			}
		}

		public bool DeleteAsset(string groupID)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.DeleteAsset(groupID);
			}
		}

		public FixedAssetData GetAssetByID(string id)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetByID(id);
			}
		}

		public DataSet GetAssetByFields(params string[] columns)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetByFields(columns);
			}
		}

		public DataSet GetAssetByFields(string[] ids, params string[] columns)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetByFields(ids, columns);
			}
		}

		public DataSet GetAssetByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetAssetList()
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetList();
			}
		}

		public DataSet GetAssetList(string fromAsset, string toAsset, string fromGroup, string toGroup, string fromClass, string toClass, string fromLocation, string toLocation)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetList(fromAsset, toAsset, fromGroup, toGroup, fromClass, toClass, fromLocation, toLocation);
			}
		}

		public DataSet GetAssetComboList()
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetComboList();
			}
		}

		public bool InsertFixedAssetDepSchedule(string assetID)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.InsertDepreciationSchedule(assetID, null);
			}
		}

		public DataSet GetAssetDepSchedule(string assetID)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetDepSchedule(assetID);
			}
		}

		public DataSet CalculateDepreciation(int year, int month, string assetID)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.CalculateDepreciation(year, month, assetID);
			}
		}

		public DataSet GetAssetListReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string FromDivision, string ToDivision, string FromDepartment, string ToDepartment, string FromCountry, string ToCountry, string FromCompany, string ToCompany, bool showInactive)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetListReport(fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, FromDivision, ToDivision, FromDepartment, ToDepartment, FromCountry, ToCountry, FromCompany, ToCompany, showInactive);
			}
		}

		public DataSet GetAssetPurchaseDetailReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetPurchaseDetailReport(from, to, fromItem, toItem, fromClass, toClass, fromCategory, toCategory);
			}
		}

		public DataSet GetAssetSaleDetailReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetSaleDetailReport(from, to, fromItem, toItem, fromClass, toClass, fromCategory, toCategory);
			}
		}

		public DataSet GetAssetTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showitemwithTansactions, bool showinactiveitems)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetTransactionReport(from, to, fromWarehouse, toWarehouse, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, showitemwithTansactions, showinactiveitems);
			}
		}

		public DataSet GetAssetDepreciationReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showitemwithTansactions, bool showinactiveitems)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetDepreciationReport(from, to, fromWarehouse, toWarehouse, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, showitemwithTansactions, showinactiveitems);
			}
		}

		public DataSet GetAssetTransferReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showitemwithTansactions, bool showinactiveitems)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetTransferReport(from, to, fromWarehouse, toWarehouse, fromItem, toItem, fromClass, toClass, fromCategory, toCategory, showitemwithTansactions, showinactiveitems);
			}
		}

		public FixedAssetData GetAssetByClassID(string id)
		{
			using (FixedAsset fixedAsset = new FixedAsset(config))
			{
				return fixedAsset.GetAssetByClassID(id);
			}
		}
	}
}
