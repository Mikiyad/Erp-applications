using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class WebDashboardSystem : MarshalByRefObject, IWebDashboardSystem, IDisposable
	{
		private Config config;

		public WebDashboardSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateWebDashboard(WebDashboardData data)
		{
			return new WebDashboard(config).InsertWebDashboard(data);
		}

		public bool UpdateWebDashboard(WebDashboardData data)
		{
			return UpdateWebDashboard(data, checkConcurrency: false);
		}

		public bool UpdateWebDashboard(WebDashboardData data, bool checkConcurrency)
		{
			return new WebDashboard(config).UpdateWebDashboard(data);
		}

		public bool SaveLayout(string dashboardID, string layout)
		{
			return new WebDashboard(config).SaveLayout(dashboardID, layout);
		}

		public bool SaveZoneLayout(string dashboardID, string zoneLayout)
		{
			return new WebDashboard(config).SaveZoneLayout(dashboardID, zoneLayout);
		}

		public WebDashboardData GetWebDashboard()
		{
			using (WebDashboard webDashboard = new WebDashboard(config))
			{
				return webDashboard.GetWebDashboard();
			}
		}

		public bool DeleteWebDashboard(string id, string userID)
		{
			using (WebDashboard webDashboard = new WebDashboard(config))
			{
				return webDashboard.DeleteWebDashboard(id, userID);
			}
		}

		public WebDashboardData GetWebDashboardByID(string id, string userID)
		{
			using (WebDashboard webDashboard = new WebDashboard(config))
			{
				return webDashboard.GetWebDashboardByID(id, userID);
			}
		}

		public DataSet GetWebDashboardByFields(params string[] columns)
		{
			using (WebDashboard webDashboard = new WebDashboard(config))
			{
				return webDashboard.GetWebDashboardByFields(columns);
			}
		}

		public DataSet GetWebDashboardByFields(string[] ids, params string[] columns)
		{
			using (WebDashboard webDashboard = new WebDashboard(config))
			{
				return webDashboard.GetWebDashboardByFields(ids, columns);
			}
		}

		public DataSet GetWebDashboardByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (WebDashboard webDashboard = new WebDashboard(config))
			{
				return webDashboard.GetWebDashboardByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetWebDashboardList()
		{
			using (WebDashboard webDashboard = new WebDashboard(config))
			{
				return webDashboard.GetWebDashboardList();
			}
		}

		public DataSet GetWebDashboardsByUser(string userID)
		{
			using (WebDashboard webDashboard = new WebDashboard(config))
			{
				return webDashboard.GetWebDashboardsByUser(userID);
			}
		}

		public DataSet GetWebDashboardComboList()
		{
			using (WebDashboard webDashboard = new WebDashboard(config))
			{
				return webDashboard.GetWebDashboardComboList();
			}
		}
	}
}
