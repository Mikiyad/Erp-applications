using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EquipmentSystem : MarshalByRefObject, IEquipmentSystem, IDisposable
	{
		private Config config;

		public EquipmentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEquipment(EquipmentData data)
		{
			return new Equipment(config).InsertEquipment(data);
		}

		public bool UpdateEquipment(EquipmentData data)
		{
			return UpdateEquipment(data, checkConcurrency: false);
		}

		public bool UpdateEquipment(EquipmentData data, bool checkConcurrency)
		{
			return new Equipment(config).UpdateEquipment(data);
		}

		public EquipmentData GetEquipment()
		{
			using (Equipment equipment = new Equipment(config))
			{
				return equipment.GetEquipment();
			}
		}

		public bool DeleteEquipment(string groupID)
		{
			using (Equipment equipment = new Equipment(config))
			{
				return equipment.DeleteEquipment(groupID);
			}
		}

		public EquipmentData GetEquipmentByID(string id)
		{
			using (Equipment equipment = new Equipment(config))
			{
				return equipment.GetEquipmentByID(id);
			}
		}

		public DataSet GetEquipmentByFields(params string[] columns)
		{
			using (Equipment equipment = new Equipment(config))
			{
				return equipment.GetEquipmentByFields(columns);
			}
		}

		public DataSet GetEquipmentByFields(string[] ids, params string[] columns)
		{
			using (Equipment equipment = new Equipment(config))
			{
				return equipment.GetEquipmentByFields(ids, columns);
			}
		}

		public DataSet GetEquipmentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Equipment equipment = new Equipment(config))
			{
				return equipment.GetEquipmentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEquipmentList()
		{
			using (Equipment equipment = new Equipment(config))
			{
				return equipment.GetEquipmentList();
			}
		}

		public DataSet GetEquipmentComboList()
		{
			using (Equipment equipment = new Equipment(config))
			{
				return equipment.GetEquipmentComboList();
			}
		}
	}
}
