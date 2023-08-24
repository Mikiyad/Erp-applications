using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EquipmentCategorySystem : MarshalByRefObject, IEquipmentCategorySystem, IDisposable
	{
		private Config config;

		public EquipmentCategorySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEquipmentCategory(EquipmentCategoryData data)
		{
			return new EquipmentCategory(config).InsertEquipmentCategory(data);
		}

		public bool UpdateEquipmentCategory(EquipmentCategoryData data)
		{
			return UpdateEquipmentCategory(data, checkConcurrency: false);
		}

		public bool UpdateEquipmentCategory(EquipmentCategoryData data, bool checkConcurrency)
		{
			return new EquipmentCategory(config).UpdateEquipmentCategory(data);
		}

		public EquipmentCategoryData GetEquipmentCategory()
		{
			using (EquipmentCategory equipmentCategory = new EquipmentCategory(config))
			{
				return equipmentCategory.GetEquipmentCategory();
			}
		}

		public bool DeleteEquipmentCategory(string groupID)
		{
			using (EquipmentCategory equipmentCategory = new EquipmentCategory(config))
			{
				return equipmentCategory.DeleteEquipmentCategory(groupID);
			}
		}

		public EquipmentCategoryData GetEquipmentCategoryByID(string id)
		{
			using (EquipmentCategory equipmentCategory = new EquipmentCategory(config))
			{
				return equipmentCategory.GetEquipmentCategoryByID(id);
			}
		}

		public DataSet GetEquipmentCategoryByFields(params string[] columns)
		{
			using (EquipmentCategory equipmentCategory = new EquipmentCategory(config))
			{
				return equipmentCategory.GetEquipmentCategoryByFields(columns);
			}
		}

		public DataSet GetEquipmentCategoryByFields(string[] ids, params string[] columns)
		{
			using (EquipmentCategory equipmentCategory = new EquipmentCategory(config))
			{
				return equipmentCategory.GetEquipmentCategoryByFields(ids, columns);
			}
		}

		public DataSet GetEquipmentCategoryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EquipmentCategory equipmentCategory = new EquipmentCategory(config))
			{
				return equipmentCategory.GetEquipmentCategoryByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEquipmentCategoryList()
		{
			using (EquipmentCategory equipmentCategory = new EquipmentCategory(config))
			{
				return equipmentCategory.GetEquipmentCategoryList();
			}
		}

		public DataSet GetEquipmentCategoryComboList()
		{
			using (EquipmentCategory equipmentCategory = new EquipmentCategory(config))
			{
				return equipmentCategory.GetEquipmentCategoryComboList();
			}
		}
	}
}
