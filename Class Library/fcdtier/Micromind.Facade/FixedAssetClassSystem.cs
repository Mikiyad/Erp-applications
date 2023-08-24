using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class FixedAssetClassSystem : MarshalByRefObject, IFixedAssetClassSystem, IDisposable
	{
		private Config config;

		public FixedAssetClassSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateAssetClass(FixedAssetClassData data)
		{
			return new FixedAssetClass(config).InsertAssetClass(data);
		}

		public bool UpdateAssetClass(FixedAssetClassData data)
		{
			return UpdateAssetClass(data, checkConcurrency: false);
		}

		public bool UpdateAssetClass(FixedAssetClassData data, bool checkConcurrency)
		{
			return new FixedAssetClass(config).UpdateAssetClass(data);
		}

		public FixedAssetClassData GetAssetClass()
		{
			using (FixedAssetClass fixedAssetClass = new FixedAssetClass(config))
			{
				return fixedAssetClass.GetAssetClass();
			}
		}

		public bool DeleteAssetClass(string classID)
		{
			using (FixedAssetClass fixedAssetClass = new FixedAssetClass(config))
			{
				return fixedAssetClass.DeleteAssetClass(classID);
			}
		}

		public FixedAssetClassData GetAssetClassByID(string id)
		{
			using (FixedAssetClass fixedAssetClass = new FixedAssetClass(config))
			{
				return fixedAssetClass.GetAssetClassByID(id);
			}
		}

		public DataSet GetAssetClassByFields(params string[] columns)
		{
			using (FixedAssetClass fixedAssetClass = new FixedAssetClass(config))
			{
				return fixedAssetClass.GetAssetClassByFields(columns);
			}
		}

		public DataSet GetAssetClassByFields(string[] ids, params string[] columns)
		{
			using (FixedAssetClass fixedAssetClass = new FixedAssetClass(config))
			{
				return fixedAssetClass.GetAssetClassByFields(ids, columns);
			}
		}

		public DataSet GetAssetClassByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (FixedAssetClass fixedAssetClass = new FixedAssetClass(config))
			{
				return fixedAssetClass.GetAssetClassByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetAssetClassList()
		{
			using (FixedAssetClass fixedAssetClass = new FixedAssetClass(config))
			{
				return fixedAssetClass.GetAssetClassList();
			}
		}

		public DataSet GetAssetClassComboList()
		{
			using (FixedAssetClass fixedAssetClass = new FixedAssetClass(config))
			{
				return fixedAssetClass.GetAssetClassComboList();
			}
		}
	}
}
