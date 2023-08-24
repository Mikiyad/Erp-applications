using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ClientAssetSystem : MarshalByRefObject, IClientAssetSystem, IDisposable
	{
		private Config config;

		public ClientAssetSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateClientAsset(ClientAssetData data)
		{
			return new ClientAsset(config).InsertClientAsset(data);
		}

		public bool UpdateClientAsset(ClientAssetData data)
		{
			return UpdateClientAsset(data, checkConcurrency: false);
		}

		public bool UpdateClientAsset(ClientAssetData data, bool checkConcurrency)
		{
			return new ClientAsset(config).UpdateClientAsset(data);
		}

		public ClientAssetData GetClientAsset()
		{
			using (ClientAsset clientAsset = new ClientAsset(config))
			{
				return clientAsset.GetClientAsset();
			}
		}

		public bool DeleteClientAsset(string groupID)
		{
			using (ClientAsset clientAsset = new ClientAsset(config))
			{
				return clientAsset.DeleteClientAsset(groupID);
			}
		}

		public ClientAssetData GetClientAssetByID(string id)
		{
			using (ClientAsset clientAsset = new ClientAsset(config))
			{
				return clientAsset.GetClientAssetByID(id);
			}
		}

		public DataSet GetJobTaskByFields(params string[] columns)
		{
			using (ClientAsset clientAsset = new ClientAsset(config))
			{
				return clientAsset.GetJobTaskByFields(columns);
			}
		}

		public DataSet GetJobTaskByFields(string[] ids, params string[] columns)
		{
			using (ClientAsset clientAsset = new ClientAsset(config))
			{
				return clientAsset.GetJobTaskByFields(ids, columns);
			}
		}

		public DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ClientAsset clientAsset = new ClientAsset(config))
			{
				return clientAsset.GetJobTaskByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetClientAssetList(bool isInactive)
		{
			using (ClientAsset clientAsset = new ClientAsset(config))
			{
				return clientAsset.GetClientAssetList(isInactive);
			}
		}

		public DataSet GetClientAssetComboList()
		{
			using (ClientAsset clientAsset = new ClientAsset(config))
			{
				return clientAsset.GetClientAssetComboList();
			}
		}
	}
}
