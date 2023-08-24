using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class INCOSystem : MarshalByRefObject, IINCOSystem, IDisposable
	{
		private Config config;

		public INCOSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateINCO(INCOData data)
		{
			return new INCO(config).InsertINCO(data);
		}

		public bool UpdateINCO(INCOData data)
		{
			return UpdateINCO(data, checkConcurrency: false);
		}

		public bool UpdateINCO(INCOData data, bool checkConcurrency)
		{
			return new INCO(config).UpdateINCO(data);
		}

		public INCOData GetINCO()
		{
			using (INCO iNCO = new INCO(config))
			{
				return iNCO.GetINCO();
			}
		}

		public bool DeleteINCO(string groupID)
		{
			using (INCO iNCO = new INCO(config))
			{
				return iNCO.DeleteINCO(groupID);
			}
		}

		public INCOData GetINCOByID(string id)
		{
			using (INCO iNCO = new INCO(config))
			{
				return iNCO.GetINCOByID(id);
			}
		}

		public DataSet GetINCOByFields(params string[] columns)
		{
			using (INCO iNCO = new INCO(config))
			{
				return iNCO.GetINCOByFields(columns);
			}
		}

		public DataSet GetINCOByFields(string[] ids, params string[] columns)
		{
			using (INCO iNCO = new INCO(config))
			{
				return iNCO.GetINCOByFields(ids, columns);
			}
		}

		public DataSet GetINCOByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (INCO iNCO = new INCO(config))
			{
				return iNCO.GetINCOByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetINCOList()
		{
			using (INCO iNCO = new INCO(config))
			{
				return iNCO.GetINCOList();
			}
		}

		public DataSet GetINCOComboList()
		{
			using (INCO iNCO = new INCO(config))
			{
				return iNCO.GetINCOComboList();
			}
		}
	}
}
