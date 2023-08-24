using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IWebDashboardSystem
	{
		bool CreateWebDashboard(WebDashboardData dashboardData);

		bool UpdateWebDashboard(WebDashboardData dashboardData);

		WebDashboardData GetWebDashboard();

		bool SaveLayout(string dashboardID, string layout);

		bool SaveZoneLayout(string dashboardID, string layout);

		bool DeleteWebDashboard(string ID, string userID);

		WebDashboardData GetWebDashboardByID(string id, string userID);

		DataSet GetWebDashboardByFields(params string[] columns);

		DataSet GetWebDashboardByFields(string[] ids, params string[] columns);

		DataSet GetWebDashboardByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetWebDashboardList();

		DataSet GetWebDashboardComboList();

		DataSet GetWebDashboardsByUser(string userID);
	}
}
