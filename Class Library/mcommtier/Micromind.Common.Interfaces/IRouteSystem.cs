using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IRouteSystem
	{
		bool CreateRoute(RouteData RouteData);

		bool UpdateRoute(RouteData RouteData);

		RouteData GetRoute();

		bool DeleteRoute(string ID);

		RouteData GetRouteByID(string id);

		DataSet GetJobTaskByFields(params string[] columns);

		DataSet GetJobTaskByFields(string[] ids, params string[] columns);

		DataSet GetJobTaskByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetRouteList(bool isactive);

		DataSet GetRouteComboList();
	}
}
