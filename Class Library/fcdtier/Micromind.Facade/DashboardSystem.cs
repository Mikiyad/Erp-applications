using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DashboardSystem : MarshalByRefObject, IDashboardSystem, IDisposable
	{
		private Config config;

		public DashboardSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDashboard(DashboardData data)
		{
			return new Dashboard(config).InsertDashboard(data);
		}

		public bool UpdateDashboard(DashboardData data)
		{
			return UpdateDashboard(data, checkConcurrency: false);
		}

		public bool UpdateDashboard(DashboardData data, bool checkConcurrency)
		{
			return new Dashboard(config).UpdateDashboard(data);
		}

		public bool SaveLayout(string dashboardID, byte[] layout)
		{
			return new Dashboard(config).SaveLayout(dashboardID, layout);
		}

		public bool SaveLayoutTemplate(string dashboardID, byte[] layout, string templateName)
		{
			return new Dashboard(config).SaveLayoutTemplate(dashboardID, layout, templateName);
		}

		public DashboardData GetDashboard()
		{
			using (Dashboard dashboard = new Dashboard(config))
			{
				return dashboard.GetDashboard();
			}
		}

		public bool DeleteDashboard(string id, string userID)
		{
			using (Dashboard dashboard = new Dashboard(config))
			{
				return dashboard.DeleteDashboard(id, userID);
			}
		}

		public bool UpdateDashboardWithTemplate(string id, string userID)
		{
			using (Dashboard dashboard = new Dashboard(config))
			{
				return dashboard.UpdateDashboardWithTemplate(id, userID);
			}
		}

		public bool UpdateDashboardWithTemplate(string currentdashboardID, string newDashboardID, string userID)
		{
			using (Dashboard dashboard = new Dashboard(config))
			{
				return dashboard.UpdateDashboardWithTemplate(currentdashboardID, newDashboardID, userID);
			}
		}

		public DashboardData GetDashboardByID(string id, string userID)
		{
			using (Dashboard dashboard = new Dashboard(config))
			{
				return dashboard.GetDashboardByID(id, userID);
			}
		}

		public DashboardData GetAvailableDashboardTemplates()
		{
			using (Dashboard dashboard = new Dashboard(config))
			{
				return dashboard.GetAvailableDashboardTemplates();
			}
		}

		public DataSet GetDashboardByFields(params string[] columns)
		{
			using (Dashboard dashboard = new Dashboard(config))
			{
				return dashboard.GetDashboardByFields(columns);
			}
		}

		public DataSet GetDashboardByFields(string[] ids, params string[] columns)
		{
			using (Dashboard dashboard = new Dashboard(config))
			{
				return dashboard.GetDashboardByFields(ids, columns);
			}
		}

		public DataSet GetDashboardByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Dashboard dashboard = new Dashboard(config))
			{
				return dashboard.GetDashboardByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetDashboardList()
		{
			using (Dashboard dashboard = new Dashboard(config))
			{
				return dashboard.GetDashboardList();
			}
		}

		public DataSet GetDashboardsByUser(string userID)
		{
			using (Dashboard dashboard = new Dashboard(config))
			{
				return dashboard.GetDashboardsByUser(userID);
			}
		}

		public DataSet GetDashboardComboList()
		{
			using (Dashboard dashboard = new Dashboard(config))
			{
				return dashboard.GetDashboardComboList();
			}
		}
	}
}
