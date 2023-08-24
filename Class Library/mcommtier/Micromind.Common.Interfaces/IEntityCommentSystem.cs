using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEntityCommentSystem
	{
		bool CreateEntityComment(EntityCommentData entityDocData);

		bool UpdateEntityComment(EntityCommentData entityDocData);

		EntityCommentData GetEntityComment();

		EntityCommentData GetEntityCommentByID(string id);

		EntityCommentData GetEntityCommentByID(EntityTypesEnum entityName, string entityID, string fileName);

		DataSet GetEntityCommentByFields(params string[] columns);

		DataSet GetEntityCommentByFields(string[] ids, params string[] columns);

		DataSet GetEntityCommentByFields(string[] ids, bool isInactive, params string[] columns);

		bool DeleteEntityComment(int commentID);

		DataSet GetEntityCommentList(EntityTypesEnum entityType, string entityID, int entityRowIndex);

		DataSet GetEntityCommentList(EntityTypesEnum entityType, string entityID);
	}
}
