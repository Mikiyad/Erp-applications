using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDashboardSystem
	{
		bool CreateDashboard(DashboardData dashboardData);

		bool UpdateDashboard(DashboardData dashboardData);

		DashboardData GetDashboard();

		bool SaveLayout(string dashboardID, byte[] layout);

		bool SaveLayoutTemplate(string dashboardID, byte[] layout, string templateName);

		bool DeleteDashboard(string ID, string userID);

		bool UpdateDashboardWithTemplate(string ID, string userID);

		bool UpdateDashboardWithTemplate(string currentdashboardID, string newDashboardID, string userID);

		DashboardData GetDashboardByID(string id, string userID);

		DashboardData GetAvailableDashboardTemplates();

		DataSet GetDashboardByFields(params string[] columns);

		DataSet GetDashboardByFields(string[] ids, params string[] columns);

		DataSet GetDashboardByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetDashboardList();

		DataSet GetDashboardComboList();

		DataSet GetDashboardsByUser(string userID);
	}
}
