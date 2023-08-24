using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IRouteGroupSystem
	{
		bool CreateRouteGroup(RouteGroupData routeGroupData);

		bool UpdateRouteGroup(RouteGroupData routeGroupData);

		bool DeleteRouteGroup(string ID);

		RouteGroupData GetRouteGroupByID(string id);

		DataSet GetRouteGroupComboList();

		DataSet GetRouteGroupList(bool isInactive);
	}
}
