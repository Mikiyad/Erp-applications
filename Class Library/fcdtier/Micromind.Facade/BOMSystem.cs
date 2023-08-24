using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class BOMSystem : MarshalByRefObject, IBOMSystem, IDisposable
	{
		private Config config;

		public BOMSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateBOM(BOMData data)
		{
			return new BOMs(config).InsertUpdateBOM(data, isUpdate: false);
		}

		public bool UpdateBOM(BOMData data)
		{
			return UpdateBOM(data, checkConcurrency: false);
		}

		public bool UpdateBOM(BOMData data, bool checkConcurrency)
		{
			return new BOMs(config).InsertUpdateBOM(data, isUpdate: true);
		}

		public BOMData GetBOM()
		{
			using (BOMs bOMs = new BOMs(config))
			{
				return bOMs.GetBOM();
			}
		}

		public bool DeleteBOM(string groupID)
		{
			using (BOMs bOMs = new BOMs(config))
			{
				return bOMs.DeleteBOM(groupID);
			}
		}

		public BOMData GetBOMByID(string id)
		{
			using (BOMs bOMs = new BOMs(config))
			{
				return bOMs.GetBOMByID(id);
			}
		}

		public BOMData GetBOMItemsByID(string id)
		{
			using (BOMs bOMs = new BOMs(config))
			{
				return bOMs.GetBOMItemsByID(id);
			}
		}

		public DataSet GetBOMByFields(params string[] columns)
		{
			using (BOMs bOMs = new BOMs(config))
			{
				return bOMs.GetBOMByFields(columns);
			}
		}

		public DataSet GetBOMByFields(string[] ids, params string[] columns)
		{
			using (BOMs bOMs = new BOMs(config))
			{
				return bOMs.GetBOMByFields(ids, columns);
			}
		}

		public DataSet GetBOMByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (BOMs bOMs = new BOMs(config))
			{
				return bOMs.GetBOMByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetBOMList(bool inactive)
		{
			using (BOMs bOMs = new BOMs(config))
			{
				return bOMs.GetBOMList(inactive);
			}
		}

		public DataSet GetBOMComboList()
		{
			using (BOMs bOMs = new BOMs(config))
			{
				return bOMs.GetBOMComboList();
			}
		}

		public DataSet GetPackageComboList()
		{
			using (BOMs bOMs = new BOMs(config))
			{
				return bOMs.GetPackageComboList();
			}
		}

		public DataSet GetPackageList(bool inactive)
		{
			using (BOMs bOMs = new BOMs(config))
			{
				return bOMs.GetPackageList(inactive);
			}
		}

		public BOMData GetPackageByID(string id)
		{
			using (BOMs bOMs = new BOMs(config))
			{
				return bOMs.GetPackageByID(id);
			}
		}
	}
}
