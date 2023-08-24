using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EntityCommentSystem : MarshalByRefObject, IEntityCommentSystem, IDisposable
	{
		private Config config;

		public EntityCommentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEntityComment(EntityCommentData data)
		{
			return new EntityComments(config).InsertEntityComment(data);
		}

		public bool UpdateEntityComment(EntityCommentData data)
		{
			return UpdateEntityComment(data, checkConcurrency: true);
		}

		public bool UpdateEntityComment(EntityCommentData data, bool checkConcurrency)
		{
			return new EntityComments(config).UpdateEntityComment(data);
		}

		public EntityCommentData GetEntityComment()
		{
			using (EntityComments entityComments = new EntityComments(config))
			{
				return entityComments.GetEntityComment();
			}
		}

		public EntityCommentData GetEntityCommentByID(string id)
		{
			using (EntityComments entityComments = new EntityComments(config))
			{
				return entityComments.GetEntityCommentByID(id);
			}
		}

		public EntityCommentData GetEntityCommentByID(EntityTypesEnum entityType, string entityID, string fileName)
		{
			using (EntityComments entityComments = new EntityComments(config))
			{
				return entityComments.GetEntityCommentByID(entityType, entityID, fileName);
			}
		}

		public DataSet GetEntityCommentByFields(params string[] columns)
		{
			using (EntityComments entityComments = new EntityComments(config))
			{
				return entityComments.GetEntityCommentByFields(columns);
			}
		}

		public DataSet GetEntityCommentByFields(string[] ids, params string[] columns)
		{
			using (EntityComments entityComments = new EntityComments(config))
			{
				return entityComments.GetEntityCommentByFields(ids, columns);
			}
		}

		public DataSet GetEntityCommentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EntityComments entityComments = new EntityComments(config))
			{
				return entityComments.GetEntityCommentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEntityCommentList(EntityTypesEnum entityType, string entityID)
		{
			using (EntityComments entityComments = new EntityComments(config))
			{
				return entityComments.GetEntityCommentList(entityType, entityID);
			}
		}

		public DataSet GetEntityCommentList(EntityTypesEnum entityType, string entityID, int entityRowIndex)
		{
			using (EntityComments entityComments = new EntityComments(config))
			{
				return entityComments.GetEntityCommentList(entityType, entityID);
			}
		}

		public bool DeleteEntityComment(int commentID)
		{
			using (EntityComments entityComments = new EntityComments(config))
			{
				return entityComments.DeleteEntityComment(commentID);
			}
		}
	}
}
