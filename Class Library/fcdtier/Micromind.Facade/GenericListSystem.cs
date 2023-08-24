using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class GenericListSystem : MarshalByRefObject, IGenericListSystem, IDisposable
	{
		private Config config;

		public GenericListSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateGenericList(GenericListData data)
		{
			return new GenericList(config).InsertGenericList(data);
		}

		public bool UpdateGenericList(GenericListData data)
		{
			return UpdateGenericList(data, checkConcurrency: false);
		}

		public bool UpdateGenericList(GenericListData data, bool checkConcurrency)
		{
			return new GenericList(config).UpdateGenericList(data);
		}

		public GenericListData GetGenericList(GenericListTypes listType)
		{
			using (GenericList genericList = new GenericList(config))
			{
				return genericList.GetGenericList(listType);
			}
		}

		public bool DeleteGenericList(string groupID, GenericListTypes listType)
		{
			using (GenericList genericList = new GenericList(config))
			{
				return genericList.DeleteGenericList(groupID, listType);
			}
		}

		public GenericListData GetGenericListByID(string id, GenericListTypes listType)
		{
			using (GenericList genericList = new GenericList(config))
			{
				return genericList.GetGenericListByID(id, listType);
			}
		}

		public DataSet GetGenericListByFields(params string[] columns)
		{
			using (GenericList genericList = new GenericList(config))
			{
				return genericList.GetGenericListByFields(columns);
			}
		}

		public DataSet GetGenericListByFields(string[] ids, params string[] columns)
		{
			using (GenericList genericList = new GenericList(config))
			{
				return genericList.GetGenericListByFields(ids, columns);
			}
		}

		public DataSet GetGenericListByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (GenericList genericList = new GenericList(config))
			{
				return genericList.GetGenericListByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetGenericListList(GenericListTypes listType)
		{
			using (GenericList genericList = new GenericList(config))
			{
				return genericList.GetGenericListList(listType);
			}
		}

		public DataSet GetGenericListList(GenericListTypes listType, bool islistType)
		{
			using (GenericList genericList = new GenericList(config))
			{
				return genericList.GetGenericListList(listType, islistType);
			}
		}

		public DataSet GetGenericListComboList(GenericListTypes listType)
		{
			using (GenericList genericList = new GenericList(config))
			{
				return genericList.GetGenericListComboList(listType);
			}
		}
	}
}
