using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEntityTypeSystem
	{
		int CreateEntityType(string entityName, EntityTypesEnum entityType);

		int CreateEntityType(EntityTypeData entityTypeData);

		bool UpdateEntityType(EntityTypeData entityTypeData);

		bool UpdateEntityType(EntityTypeData entityTypeData, bool checkConcurrency);

		bool DeleteEntityType(int entityTypeID);

		DataSet GetEntityTypesByFields(EntityTypesEnum[] entityTypesEnum, int[] entityTypeIDs, params string[] columns);

		EntityTypeData GetEntityTypeByID(int entityTypeID);

		bool ExistEntityType(string name);

		string GetEntityTypeNameByID(int id);
	}
}
