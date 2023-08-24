using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class InventoryTransferTypeSystem : MarshalByRefObject, IInventoryTransferTypeSystem, IDisposable
	{
		private Config config;

		public InventoryTransferTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateInventoryTransferType(InventoryTransferTypeData data)
		{
			return new InventoryTransferType(config).InsertInventoryTransferType(data);
		}

		public bool UpdateInventoryTransferType(InventoryTransferTypeData data)
		{
			return UpdateInventoryTransferType(data, checkConcurrency: false);
		}

		public bool UpdateInventoryTransferType(InventoryTransferTypeData data, bool checkConcurrency)
		{
			return new InventoryTransferType(config).UpdateInventoryTransferType(data);
		}

		public InventoryTransferTypeData GetInventoryTransferType()
		{
			using (InventoryTransferType inventoryTransferType = new InventoryTransferType(config))
			{
				return inventoryTransferType.GetInventoryTransferType();
			}
		}

		public bool DeleteInventoryTransferType(string groupID)
		{
			using (InventoryTransferType inventoryTransferType = new InventoryTransferType(config))
			{
				return inventoryTransferType.DeleteInventoryTransferType(groupID);
			}
		}

		public InventoryTransferTypeData GetInventoryTransferTypeByID(string id)
		{
			using (InventoryTransferType inventoryTransferType = new InventoryTransferType(config))
			{
				return inventoryTransferType.GetInventoryTransferTypeByID(id);
			}
		}

		public DataSet GetInventoryTransferTypeByFields(params string[] columns)
		{
			using (InventoryTransferType inventoryTransferType = new InventoryTransferType(config))
			{
				return inventoryTransferType.GetInventoryTransferTypeByFields(columns);
			}
		}

		public DataSet GetInventoryTransferTypeByFields(string[] ids, params string[] columns)
		{
			using (InventoryTransferType inventoryTransferType = new InventoryTransferType(config))
			{
				return inventoryTransferType.GetInventoryTransferTypeByFields(ids, columns);
			}
		}

		public DataSet GetInventoryTransferTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (InventoryTransferType inventoryTransferType = new InventoryTransferType(config))
			{
				return inventoryTransferType.GetInventoryTransferTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetInventoryTransferTypeList()
		{
			using (InventoryTransferType inventoryTransferType = new InventoryTransferType(config))
			{
				return inventoryTransferType.GetInventoryTransferTypeList();
			}
		}

		public DataSet GetInventoryTransferTypeComboList()
		{
			using (InventoryTransferType inventoryTransferType = new InventoryTransferType(config))
			{
				return inventoryTransferType.GetInventoryTransferTypeComboList();
			}
		}
	}
}
