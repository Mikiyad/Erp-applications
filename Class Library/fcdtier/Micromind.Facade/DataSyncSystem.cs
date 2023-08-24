using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DataSyncSystem : MarshalByRefObject, IDataSyncSystem, IDisposable
	{
		private Config config;

		public DataSyncSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDataSync(DataSyncData data, bool isUpdate)
		{
			return new DataSync(config).InsertUpdateDataSync(data, isUpdate);
		}

		public DataSyncData GetDataSync()
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.GetDataSync();
			}
		}

		public bool DeleteDataSync(string groupID)
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.DeleteDataSync(groupID);
			}
		}

		public DataSet GetDataSyncByFields(params string[] columns)
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.GetDataSyncByFields(columns);
			}
		}

		public DataSet GetDataSyncByFields(string[] ids, params string[] columns)
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.GetDataSyncByFields(ids, columns);
			}
		}

		public DataSet GetDataSyncByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.GetDataSyncByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetDataSyncList()
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.GetDataSyncList();
			}
		}

		public DataSet GetDataSyncComboList()
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.GetDataSyncComboList();
			}
		}

		public DataSet GetDataSyncBySales(string docType, string code)
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.GetDataSyncBySales(docType, code);
			}
		}

		public DataSyncData GetDataSyncByID(string code)
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.GetDataSyncByID(code);
			}
		}

		public DataSet GetDataSyncByCollections(string paymentType, string code)
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.RunCollectionsSync(paymentType, code);
			}
		}

		public DataSet GetDataSyncByStockTransfer(string transferType, string code)
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.RunStockTransferSync(transferType, code);
			}
		}

		public DataSet GetDataSyncCustomers(string code)
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.RunSyncCustomers(code);
			}
		}

		public DataSet GetDataSyncItems(string code)
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.RunSyncItems(code);
			}
		}

		public bool UpdateLastSyncTime(string transferType, string code)
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.UpdateLastSyncTime(transferType, code);
			}
		}

		public DataSet GetConnected(string code)
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.GetConnected(code);
			}
		}

		public DataSet GetDataSyncNewCustomers(string code)
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.RunNewCustomerSyncron(code);
			}
		}

		public DataSet GetSyncList()
		{
			using (DataSync dataSync = new DataSync(config))
			{
				return dataSync.DataSyncList();
			}
		}
	}
}
