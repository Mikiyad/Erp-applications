using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class RouteSystem : MarshalByRefObject, IRouteSystem, IDisposable
	{
		private Config config;

		public RouteSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateRoute(RouteData data)
		{
			return new Route(config).InsertRoute(data);
		}

		public bool UpdateRoute(RouteData data)
		{
			return UpdateRoute(data, checkConcurrency: false);
		}

		public bool UpdateRoute(RouteData data, bool checkConcurrency)
		{
			return new Route(config).UpdateRoute(data);
		}

		public RouteData GetRoute()
		{
			using (Route route = new Route(config))
			{
				return route.GetRoute();
			}
		}

		public bool DeleteRoute(string groupID)
		{
			using (Route route = new Route(config))
			{
				return route.DeleteRoute(groupID);
			}
		}

		public RouteData GetRouteByID(string id)
		{
			using (Route route = new Route(config))
			{
				return route.GetRouteByID(id);
			}
		}

		public DataSet GetJobTaskByFields(params string[] columns)
		{
			using (Route route = new Route(config))
			{
				return route.GetJobTaskByFields(columns);
			}
		}

		public DataSet GetJobTaskByFields(string[] ids, params string[] columns)
		{
			using (Route route = new Route(config))
			{
				return route.GetJobTaskByFields(ids, columns);
			}
		}

		public DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Route route = new Route(config))
			{
				return route.GetJobTaskByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetRouteList(bool isInactive)
		{
			using (Route route = new Route(config))
			{
				return route.GetRouteList(isInactive);
			}
		}

		public DataSet GetRouteComboList()
		{
			using (Route route = new Route(config))
			{
				return route.GetRouteComboList();
			}
		}
	}
}
