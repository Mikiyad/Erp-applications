using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IExternalReportSystem
	{
		bool CreateExternalReport(ExternalReportData smartListData);

		bool UpdateExternalReport(ExternalReportData smartListData);

		ExternalReportData GetExternalReport(string UserID, bool isadmin);

		DataSet GetExternalReportData(string UserID, bool isadmin);

		bool DeleteExternalReport(string ID);

		bool CreateCategory(string categoryName, int parentID);

		DataSet GetCategoryList();

		ExternalReportData GetExternalReportByID(string id);

		DataSet GetExternalReportByFields(params string[] columns);

		DataSet GetExternalReportByFields(string[] ids, params string[] columns);

		DataSet GetExternalReportByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetExternalReportList();

		DataSet GetExternalReportComboList();

		DataSet GetSubExternalReportComboList();

		DataSet GetReportByQuery(string query, DateTime fromDate, DateTime toDate);

		bool UpdateCategory(string categoryName, int parentID, int categoryID);

		bool RenameReport(string reportName, int reportID);

		bool DeleteCategory(string categoryID);

		DataSet GetExternalReportData(string reportID, DateTime fromDate, DateTime toDate, string[] parameters, string[] parameterValues);

		DataSet GetExternalReportCategoryComboList();
	}
}
