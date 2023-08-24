using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EntityCategorySystem : MarshalByRefObject, IEntityCategorySystem, IDisposable
	{
		private Config config;

		public EntityCategorySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEntityCategory(EntityCategoryData data)
		{
			return new EntityCategory(config).InsertEntityCategory(data);
		}

		public bool UpdateEntityCategory(EntityCategoryData data)
		{
			return UpdateEntityCategory(data, checkConcurrency: false);
		}

		public bool UpdateEntityCategory(EntityCategoryData data, bool checkConcurrency)
		{
			return new EntityCategory(config).UpdateEntityCategory(data);
		}

		public EntityCategoryData GetEntityCategory()
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.GetEntityCategory();
			}
		}

		public bool DeleteEntityCategory(string categoryID, EntityTypesEnum entityType)
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.DeleteEntityCategory(categoryID, entityType);
			}
		}

		public EntityCategoryData GetEntityCategoryByID(string id)
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.GetEntityCategoryByID(id);
			}
		}

		public DataSet GetEntityCategoryByFields(params string[] columns)
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.GetEntityCategoryByFields(columns);
			}
		}

		public DataSet GetEntityCategoryByFields(string[] ids, params string[] columns)
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.GetEntityCategoryByFields(ids, columns);
			}
		}

		public DataSet GetEntityCategoryByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.GetEntityCategoryByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEntityCategoryList(EntityTypesEnum entityType)
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.GetEntityCategoryList(entityType);
			}
		}

		public DataSet GetActiveEntityCategoryList(EntityTypesEnum entityType)
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.GetActiveEntityCategoryList(entityType);
			}
		}

		public DataSet GetEntityAssignedCategorysList(string entityID, EntityTypesEnum entityType)
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.GetEntityAssignedCategorysList(entityID, entityType);
			}
		}

		public DataSet GetEntityAssignedCategorysTreeViewList(string entityID, EntityTypesEnum entityType)
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.GetEntityAssignedCategorysTreeViewList(entityID, entityType);
			}
		}

		public DataSet GetEntityCategoryComboList()
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.GetEntityCategoryComboList();
			}
		}

		public bool InsertEntityCategoryAssignment(EntityCategoryData data, string entityID)
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.InsertEntityCategoryAssignment(data, entityID);
			}
		}

		public DataSet GetEntityCategoryTree(bool includeInactive, bool isHierarchy)
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.GetCustomerCategoryTreeview(includeInactive, isHierarchy);
			}
		}

		public DataSet GetEntityCategoryCombosDataList()
		{
			using (EntityCategory entityCategory = new EntityCategory(config))
			{
				return entityCategory.GetEntityCategoryCombosDataList();
			}
		}
	}
}
