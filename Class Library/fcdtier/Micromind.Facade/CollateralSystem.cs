using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CollateralSystem : MarshalByRefObject, ICollateralSystem, IDisposable
	{
		private Config config;

		public CollateralSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCollateral(CollateralData data)
		{
			return new Collateral(config).InsertCollateral(data);
		}

		public bool UpdateCollateral(CollateralData data)
		{
			return UpdateCollateral(data, checkConcurrency: false);
		}

		public bool UpdateCollateral(CollateralData data, bool checkConcurrency)
		{
			return new Collateral(config).UpdateCollateral(data);
		}

		public CollateralData GetCollateral()
		{
			using (Collateral collateral = new Collateral(config))
			{
				return collateral.GetCollateral();
			}
		}

		public bool DeleteCollateral(string groupID)
		{
			using (Collateral collateral = new Collateral(config))
			{
				return collateral.DeleteCollateral(groupID);
			}
		}

		public CollateralData GetCollateralByID(string id)
		{
			using (Collateral collateral = new Collateral(config))
			{
				return collateral.GetCollateralByID(id);
			}
		}

		public DataSet GetCollateralByFields(params string[] columns)
		{
			using (Collateral collateral = new Collateral(config))
			{
				return collateral.GetCollateralByFields(columns);
			}
		}

		public DataSet GetCollateralByFields(string[] ids, params string[] columns)
		{
			using (Collateral collateral = new Collateral(config))
			{
				return collateral.GetCollateralByFields(ids, columns);
			}
		}

		public DataSet GetCollateralByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Collateral collateral = new Collateral(config))
			{
				return collateral.GetCollateralByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCollateralList()
		{
			using (Collateral collateral = new Collateral(config))
			{
				return collateral.GetCollateralList();
			}
		}

		public DataSet GetCollateralComboList()
		{
			using (Collateral collateral = new Collateral(config))
			{
				return collateral.GetCollateralComboList();
			}
		}

		public DataSet GetCollteralToPrint(string fromColID, string toColID, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive)
		{
			using (Collateral collateral = new Collateral(config))
			{
				return collateral.GetCollteralToPrint(fromColID, toColID, fromDepartment, toDepartment, fromLocation, toLocation, showInactive);
			}
		}
	}
}
