using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EquipmentTypeSystem : MarshalByRefObject, IEquipmentTypeSystem, IDisposable
	{
		private Config config;

		public EquipmentTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEquipmentType(EquipmentTypeData data)
		{
			return new EquipmentType(config).InsertEquipmentType(data);
		}

		public bool UpdateEquipmentType(EquipmentTypeData data)
		{
			return UpdateEquipmentType(data, checkConcurrency: false);
		}

		public bool UpdateEquipmentType(EquipmentTypeData data, bool checkConcurrency)
		{
			return new EquipmentType(config).UpdateEquipmentType(data);
		}

		public EquipmentTypeData GetEquipmentType()
		{
			using (EquipmentType equipmentType = new EquipmentType(config))
			{
				return equipmentType.GetEquipmentType();
			}
		}

		public bool DeleteEquipmentType(string groupID)
		{
			using (EquipmentType equipmentType = new EquipmentType(config))
			{
				return equipmentType.DeleteEquipmentType(groupID);
			}
		}

		public EquipmentTypeData GetEquipmentTypeByID(string id)
		{
			using (EquipmentType equipmentType = new EquipmentType(config))
			{
				return equipmentType.GetEquipmentTypeByID(id);
			}
		}

		public DataSet GetEquipmentTypeByFields(params string[] columns)
		{
			using (EquipmentType equipmentType = new EquipmentType(config))
			{
				return equipmentType.GetEquipmentTypeByFields(columns);
			}
		}

		public DataSet GetEquipmentTypeByFields(string[] ids, params string[] columns)
		{
			using (EquipmentType equipmentType = new EquipmentType(config))
			{
				return equipmentType.GetEquipmentTypeByFields(ids, columns);
			}
		}

		public DataSet GetEquipmentTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EquipmentType equipmentType = new EquipmentType(config))
			{
				return equipmentType.GetEquipmentTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEquipmentTypeList()
		{
			using (EquipmentType equipmentType = new EquipmentType(config))
			{
				return equipmentType.GetEquipmentTypeList();
			}
		}

		public DataSet GetEquipmentTypeComboList()
		{
			using (EquipmentType equipmentType = new EquipmentType(config))
			{
				return equipmentType.GetEquipmentTypeComboList();
			}
		}
	}
}
