using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EntityDocSystem : MarshalByRefObject, IEntityDocSystem, IDisposable
	{
		private Config config;

		public EntityDocSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEntityDoc(EntityDocData data)
		{
			return new EntityDoc(config).InsertEntityDoc(data);
		}

		public bool UpdateEntityDoc(EntityDocData data)
		{
			return UpdateEntityDoc(data, checkConcurrency: true);
		}

		public bool UpdateEntityDoc(EntityDocData data, bool checkConcurrency)
		{
			return new EntityDoc(config).UpdateEntityDoc(data);
		}

		public EntityDocData GetEntityDoc()
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.GetEntityDoc();
			}
		}

		public EntityDocData GetEntityDocByID(string id)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.GetEntityDocByID(id);
			}
		}

		public EntityDocData GetEntityDocByID(EntityTypesEnum entityType, string entityID, string fileName)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.GetEntityDocByID(entityType, entityID, fileName);
			}
		}

		public DataSet GetEntityDocByFields(params string[] columns)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.GetEntityDocByFields(columns);
			}
		}

		public DataSet GetEntityDocByFields(string[] ids, params string[] columns)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.GetEntityDocByFields(ids, columns);
			}
		}

		public DataSet GetEntityDocByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.GetEntityDocByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEntityDocList(EntityTypesEnum entityType, string entityID)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.GetEntityDocList(entityType, entityID, -1);
			}
		}

		public DataSet GetEntityDocList(EntityTypesEnum entityType, string entityID, int entityRowIndex)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.GetEntityDocList(entityType, entityID, entityRowIndex);
			}
		}

		public DataSet GetEntityDocList(EntityTypesEnum entityType, string entitySysDocID, string entityID, int entityRowIndex)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.GetEntityDocList(entityType, entitySysDocID, entityID, entityRowIndex);
			}
		}

		public bool IsFileExist(EntityTypesEnum entityType, string entityID, string fileName)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.IsFileExist(entityType, entityID, fileName);
			}
		}

		public bool SaveEntityDoc(EntityDocData entityDocData)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.SaveEntityDoc(entityDocData);
			}
		}

		public bool SaveOCRDocs(EntityDocData entityDocData)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.SaveOCRDocs(entityDocData);
			}
		}

		public byte[] GetEntityDocByPath(string path)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.GetEntityDocByPath(path);
			}
		}

		public byte[] GetEntityFile(EntityTypesEnum entityType, string entityID, string entitySysDocID, string fileName)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.GetEntityFile(entityType, entityID, entitySysDocID, fileName);
			}
		}

		public bool DeleteEntityDoc(EntityDocData entityDocData)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.DeleteEntityDoc(entityDocData);
			}
		}

		public DataSet GetNotExistingDocs(DataSet dsTransactions)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.GetNotExistingDocs(dsTransactions);
			}
		}

		public DataSet TempDataset()
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.TempDataset();
			}
		}

		public DataSet GetEntityApprovalStatus(DataSet dsTransaction, SysDocTypes docType, string UserID, bool includeApproveUser)
		{
			using (EntityDoc entityDoc = new EntityDoc(config))
			{
				return entityDoc.GetEntityApprovalStatus(dsTransaction, docType, UserID, includeApproveUser);
			}
		}
	}
}
