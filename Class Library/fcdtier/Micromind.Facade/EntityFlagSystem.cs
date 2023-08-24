using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EntityFlagSystem : MarshalByRefObject, IEntityFlagSystem, IDisposable
	{
		private Config config;

		public EntityFlagSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEntityFlag(EntityFlagData data)
		{
			return new EntityFlag(config).InsertEntityFlag(data);
		}

		public bool UpdateEntityFlag(EntityFlagData data)
		{
			return UpdateEntityFlag(data, checkConcurrency: false);
		}

		public bool UpdateEntityFlag(EntityFlagData data, bool checkConcurrency)
		{
			return new EntityFlag(config).UpdateEntityFlag(data);
		}

		public EntityFlagData GetEntityFlag()
		{
			using (EntityFlag entityFlag = new EntityFlag(config))
			{
				return entityFlag.GetEntityFlag();
			}
		}

		public bool DeleteEntityFlag(string flagID, EntityTypesEnum entityType)
		{
			using (EntityFlag entityFlag = new EntityFlag(config))
			{
				return entityFlag.DeleteEntityFlag(flagID, entityType);
			}
		}

		public EntityFlagData GetEntityFlagByID(string id)
		{
			using (EntityFlag entityFlag = new EntityFlag(config))
			{
				return entityFlag.GetEntityFlagByID(id);
			}
		}

		public DataSet GetEntityFlagByFields(params string[] columns)
		{
			using (EntityFlag entityFlag = new EntityFlag(config))
			{
				return entityFlag.GetEntityFlagByFields(columns);
			}
		}

		public DataSet GetEntityFlagByFields(string[] ids, params string[] columns)
		{
			using (EntityFlag entityFlag = new EntityFlag(config))
			{
				return entityFlag.GetEntityFlagByFields(ids, columns);
			}
		}

		public DataSet GetEntityFlagByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EntityFlag entityFlag = new EntityFlag(config))
			{
				return entityFlag.GetEntityFlagByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEntityFlagList(EntityTypesEnum entityType)
		{
			using (EntityFlag entityFlag = new EntityFlag(config))
			{
				return entityFlag.GetEntityFlagList(entityType);
			}
		}

		public DataSet GetActiveEntityFlagList(EntityTypesEnum entityType)
		{
			using (EntityFlag entityFlag = new EntityFlag(config))
			{
				return entityFlag.GetActiveEntityFlagList(entityType);
			}
		}

		public DataSet GetEntityAssignedFlagsList(string entityID, EntityTypesEnum entityType)
		{
			using (EntityFlag entityFlag = new EntityFlag(config))
			{
				return entityFlag.GetEntityAssignedFlagsList(entityID, entityType);
			}
		}

		public DataSet GetEntityAssignedFlagsList(EntityTypesEnum entityType)
		{
			using (EntityFlag entityFlag = new EntityFlag(config))
			{
				return entityFlag.GetEntityAssignedFlagsList(entityType);
			}
		}

		public DataSet GetEntityFlagComboList()
		{
			using (EntityFlag entityFlag = new EntityFlag(config))
			{
				return entityFlag.GetEntityFlagComboList();
			}
		}

		public bool InsertEntityFlagAssignment(EntityFlagData data, string entityID)
		{
			using (EntityFlag entityFlag = new EntityFlag(config))
			{
				return entityFlag.InsertEntityFlagAssignment(data, entityID);
			}
		}

		public bool SetFlag(int entityType, string entityID, int flagID, bool removeFlag)
		{
			using (EntityFlag entityFlag = new EntityFlag(config))
			{
				return entityFlag.SetFlag(entityType, entityID, flagID, removeFlag);
			}
		}
	}
}
