using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEntityDocSystem
	{
		bool CreateEntityDoc(EntityDocData entityDocData);

		bool UpdateEntityDoc(EntityDocData entityDocData);

		EntityDocData GetEntityDoc();

		EntityDocData GetEntityDocByID(string id);

		EntityDocData GetEntityDocByID(EntityTypesEnum entityName, string entityID, string fileName);

		DataSet GetEntityDocByFields(params string[] columns);

		DataSet GetEntityDocByFields(string[] ids, params string[] columns);

		DataSet GetEntityDocByFields(string[] ids, bool isInactive, params string[] columns);

		bool IsFileExist(EntityTypesEnum entityType, string entityID, string fileName);

		bool SaveEntityDoc(EntityDocData entityDocData);

		bool SaveOCRDocs(EntityDocData entityDocData);

		byte[] GetEntityDocByPath(string path);

		byte[] GetEntityFile(EntityTypesEnum entityType, string entityID, string entitySysDocID, string fileName);

		bool DeleteEntityDoc(EntityDocData entityDocData);

		DataSet GetEntityDocList(EntityTypesEnum entityType, string entityID, int entityRowIndex);

		DataSet GetEntityDocList(EntityTypesEnum entityType, string EntitySysDocID, string entityID, int entityRowIndex);

		DataSet GetEntityDocList(EntityTypesEnum entityType, string entityID);

		DataSet GetNotExistingDocs(DataSet dsTransactions);

		DataSet TempDataset();

		DataSet GetEntityApprovalStatus(DataSet dsTransaction, SysDocTypes systype, string UserID, bool includeApproveUser);
	}
}
