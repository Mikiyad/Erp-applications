using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEntityFlagSystem
	{
		bool CreateEntityFlag(EntityFlagData departmentData);

		bool UpdateEntityFlag(EntityFlagData departmentData);

		EntityFlagData GetEntityFlag();

		bool DeleteEntityFlag(string ID, EntityTypesEnum entityType);

		EntityFlagData GetEntityFlagByID(string id);

		DataSet GetEntityFlagByFields(params string[] columns);

		DataSet GetEntityFlagByFields(string[] ids, params string[] columns);

		DataSet GetEntityFlagByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetActiveEntityFlagList(EntityTypesEnum entityType);

		DataSet GetEntityFlagList(EntityTypesEnum entityType);

		DataSet GetEntityFlagComboList();

		DataSet GetEntityAssignedFlagsList(string entityID, EntityTypesEnum entityType);

		bool InsertEntityFlagAssignment(EntityFlagData data, string entityID);

		bool SetFlag(int entityType, string entityID, int flagID, bool removeFlag);
	}
}
