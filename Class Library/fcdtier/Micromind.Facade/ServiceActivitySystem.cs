using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ServiceActivitySystem : MarshalByRefObject, IServiceActivitySystem, IDisposable
	{
		private Config config;

		public ServiceActivitySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateServiceActivity(ServiceActivityData data)
		{
			return new ServiceActivity(config).InsertServiceActivity(data);
		}

		public bool UpdateServiceActivity(ServiceActivityData data)
		{
			return UpdateServiceActivity(data, checkConcurrency: false);
		}

		public bool UpdateServiceActivity(ServiceActivityData data, bool checkConcurrency)
		{
			return new ServiceActivity(config).UpdateServiceActivity(data);
		}

		public ServiceActivityData GetServiceActivity()
		{
			using (ServiceActivity serviceActivity = new ServiceActivity(config))
			{
				return serviceActivity.GetServiceActivity();
			}
		}

		public bool DeleteServiceActivity(string groupID)
		{
			using (ServiceActivity serviceActivity = new ServiceActivity(config))
			{
				return serviceActivity.DeleteServiceActivity(groupID);
			}
		}

		public ServiceActivityData GetServiceActivityByID(string id)
		{
			using (ServiceActivity serviceActivity = new ServiceActivity(config))
			{
				return serviceActivity.GetServiceActivityByID(id);
			}
		}

		public DataSet GetServiceActivityByFields(params string[] columns)
		{
			using (ServiceActivity serviceActivity = new ServiceActivity(config))
			{
				return serviceActivity.GetServiceActivityByFields(columns);
			}
		}

		public DataSet GetServiceActivityByFields(string[] ids, params string[] columns)
		{
			using (ServiceActivity serviceActivity = new ServiceActivity(config))
			{
				return serviceActivity.GetServiceActivityByFields(ids, columns);
			}
		}

		public DataSet GetServiceActivityByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ServiceActivity serviceActivity = new ServiceActivity(config))
			{
				return serviceActivity.GetServiceActivityByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetServiceActivityList()
		{
			using (ServiceActivity serviceActivity = new ServiceActivity(config))
			{
				return serviceActivity.GetServiceActivityList();
			}
		}

		public DataSet GetServiceActivityComboList()
		{
			using (ServiceActivity serviceActivity = new ServiceActivity(config))
			{
				return serviceActivity.GetServiceActivityComboList();
			}
		}
	}
}
