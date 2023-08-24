using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICustomReportSystem
	{
		bool CreateCustomReport(CustomReportData customReportData);

		bool UpdateCustomReport(CustomReportData customReportData);

		bool CreateUpdateCustomReportBatch(DataSet listData, bool checkConcurrency);

		CustomReportData GetCustomReports();

		DataSet GetCustomReportList();

		bool DeleteCustomReport(string customReportID);

		DataSet GetCustomReportsByFields(params string[] columns);

		DataSet GetCustomReportsByFields(string[] customReportsID, params string[] columns);

		DataSet GetCustomReportsByFields(string[] customReportsID, bool isInactive, params string[] columns);

		CustomReportData GetCustomReportByID(string customReportID);

		bool ExistCustomReport(string customReportName);

		DataSet GetCustomReportComboList();

		DataSet GetTableSchema(string query);

		DataSet GetCustomReportData(string reportID, string[] parameters, string[] parameterValues);

		bool SaveLayout(string reportID, byte[] layout, int formWidth, int formHeight);

		DataSet GetCustomData(string query);
	}
}
