using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ExternalReportSystem : MarshalByRefObject, IExternalReportSystem, IDisposable
	{
		private Config config;

		public ExternalReportSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateExternalReport(ExternalReportData data)
		{
			return new ExternalReport(config).InsertExternalReport(data);
		}

		public bool UpdateExternalReport(ExternalReportData data)
		{
			return UpdateExternalReport(data, checkConcurrency: false);
		}

		public bool UpdateExternalReport(ExternalReportData data, bool checkConcurrency)
		{
			return new ExternalReport(config).UpdateExternalReport(data);
		}

		public ExternalReportData GetExternalReport(string UserID, bool isadmin)
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.GetExternalReport(UserID, isadmin);
			}
		}

		public DataSet GetExternalReportData(string UserID, bool isadmin)
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.GetExternalReportData(UserID, isadmin);
			}
		}

		public bool DeleteExternalReport(string groupID)
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.DeleteExternalReport(groupID);
			}
		}

		public bool CreateCategory(string categoryName, int parentID)
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.CreateCategory(categoryName, parentID);
			}
		}

		public ExternalReportData GetExternalReportByID(string id)
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.GetExternalReportByID(id);
			}
		}

		public DataSet GetExternalReportByFields(params string[] columns)
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.GetExternalReportByFields(columns);
			}
		}

		public DataSet GetExternalReportByFields(string[] ids, params string[] columns)
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.GetExternalReportByFields(ids, columns);
			}
		}

		public DataSet GetExternalReportByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.GetExternalReportByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetSubExternalReportComboList()
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.GetSubExternalReportComboList();
			}
		}

		public DataSet GetCategoryList()
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.GetCategoryList();
			}
		}

		public DataSet GetExternalReportList()
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.GetExternalReportList();
			}
		}

		public DataSet GetExternalReportComboList()
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.GetExternalReportComboList();
			}
		}

		public DataSet GetReportByQuery(string query, DateTime fromDate, DateTime toDate)
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.GetReportByQuery(query, fromDate, toDate);
			}
		}

		public bool UpdateCategory(string categoryName, int parentID, int categoryID)
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.UpdateCategory(categoryName, parentID, categoryID);
			}
		}

		public bool RenameReport(string reportName, int reportID)
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.RenameReport(reportName, reportID);
			}
		}

		public bool DeleteCategory(string categoryID)
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.DeleteCategory(categoryID);
			}
		}

		public DataSet GetExternalReportCategoryComboList()
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.GetExternalReportCategoryComboList();
			}
		}

		public DataSet GetExternalReportData(string reportID, DateTime fromDate, DateTime toDate, string[] parameters, string[] parameterValues)
		{
			using (ExternalReport externalReport = new ExternalReport(config))
			{
				return externalReport.GetExternalReportData(reportID, fromDate, toDate, parameters, parameterValues);
			}
		}
	}
}
