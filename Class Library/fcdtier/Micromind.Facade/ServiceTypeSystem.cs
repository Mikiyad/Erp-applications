using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ServiceTypeSystem : MarshalByRefObject, IServiceTypeSystem, IDisposable
	{
		private Config config;

		public ServiceTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateServiceItem(ServiceItemData data)
		{
			return new ServiceItem(config).InsertServiceItem(data);
		}

		public bool UpdateServiceItem(ServiceItemData data)
		{
			return UpdateServiceItem(data, checkConcurrency: false);
		}

		public bool UpdateServiceItem(ServiceItemData data, bool checkConcurrency)
		{
			return new ServiceItem(config).UpdateServiceItem(data);
		}

		public ServiceItemData GetServiceItem()
		{
			using (ServiceItem serviceItem = new ServiceItem(config))
			{
				return serviceItem.GetServiceItem();
			}
		}

		public bool DeleteServiceItem(string groupID)
		{
			using (ServiceItem serviceItem = new ServiceItem(config))
			{
				return serviceItem.DeleteServiceItem(groupID);
			}
		}

		public ServiceItemData GetServiceItemByID(string id)
		{
			using (ServiceItem serviceItem = new ServiceItem(config))
			{
				return serviceItem.GetServiceItemByID(id);
			}
		}

		public DataSet GetServiceItemByFields(params string[] columns)
		{
			using (ServiceItem serviceItem = new ServiceItem(config))
			{
				return serviceItem.GetServiceItemByFields(columns);
			}
		}

		public DataSet GetServiceItemByFields(string[] ids, params string[] columns)
		{
			using (ServiceItem serviceItem = new ServiceItem(config))
			{
				return serviceItem.GetServiceItemByFields(ids, columns);
			}
		}

		public DataSet GetServiceItemByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ServiceItem serviceItem = new ServiceItem(config))
			{
				return serviceItem.GetServiceItemByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetServiceItemList()
		{
			using (ServiceItem serviceItem = new ServiceItem(config))
			{
				return serviceItem.GetServiceItemList();
			}
		}

		public DataSet GetServiceItemComboList()
		{
			using (ServiceItem serviceItem = new ServiceItem(config))
			{
				return serviceItem.GetServiceItemComboList();
			}
		}
	}
}
