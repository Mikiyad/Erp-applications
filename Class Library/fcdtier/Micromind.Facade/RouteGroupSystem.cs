using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class RouteGroupSystem : MarshalByRefObject, IRouteGroupSystem, IDisposable
	{
		private Config config;

		public RouteGroupSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateRouteGroup(RouteGroupData data)
		{
			return new RouteGroup(config).InsertRouteGroup(data);
		}

		public bool UpdateRouteGroup(RouteGroupData data)
		{
			return UpdateRouteGroup(data, checkConcurrency: false);
		}

		public bool UpdateRouteGroup(RouteGroupData data, bool checkConcurrency)
		{
			return new RouteGroup(config).UpdateRouteGroup(data);
		}

		public bool DeleteRouteGroup(string groupID)
		{
			using (RouteGroup routeGroup = new RouteGroup(config))
			{
				return routeGroup.DeleteRouteGroup(groupID);
			}
		}

		public RouteGroupData GetRouteGroupByID(string id)
		{
			using (RouteGroup routeGroup = new RouteGroup(config))
			{
				return routeGroup.GetRouteGroupByID(id);
			}
		}

		public DataSet GetRouteGroupComboList()
		{
			using (RouteGroup routeGroup = new RouteGroup(config))
			{
				return routeGroup.GetRouteGroupComboList();
			}
		}

		public DataSet GetRouteGroupList(bool isInactive)
		{
			using (RouteGroup routeGroup = new RouteGroup(config))
			{
				return routeGroup.GetRouteGroupList(isInactive);
			}
		}
	}
}
