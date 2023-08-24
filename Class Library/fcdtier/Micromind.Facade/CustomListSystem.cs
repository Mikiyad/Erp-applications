using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CustomListSystem : MarshalByRefObject, ICustomListSystem, IDisposable
	{
		private Config config;

		public CustomListSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool InsertUpdateCustomList(DataSet data, bool isUpdate)
		{
			return new CustomLists(config).InsertUpdateCustomList(data, isUpdate);
		}

		public bool DeleteCustomList(string listCode)
		{
			return new CustomLists(config).DeleteCustomList(listCode);
		}

		public DataSet GetCustomListItems(string listCode)
		{
			return new CustomLists(config).GetCustomListItems(listCode);
		}

		public DataSet GetCustomListByID(string listCode)
		{
			return new CustomLists(config).GetCustomListByID(listCode);
		}

		public DataSet GetCustomListComboList(string listCode)
		{
			return new CustomLists(config).GetCustomListComboList(listCode);
		}

		public DataSet GetCustomListCodes()
		{
			return new CustomLists(config).GetCustomListCodes();
		}
	}
}
