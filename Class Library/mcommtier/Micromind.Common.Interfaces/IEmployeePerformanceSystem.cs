using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeePerformanceSystem
	{
		bool CreatePerformance(EmployeePerformanceData PerformanceData);

		bool UpdatePerformance(EmployeePerformanceData PerformanceData);

		EmployeePerformanceData GetPerformance();

		bool DeletePerformance(string ID, string SysDocID);

		EmployeePerformanceData GetPerformanceByID(string sysDocID, string id);

		DataSet GetPerformanceByFields(params string[] columns);

		DataSet GetPerformanceByFields(string[] ids, params string[] columns);

		DataSet GetPerformanceByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPerformanceList();

		DataSet GetPerformanceComboList();

		DataSet GetEmployeePerformanceList(string EmployeeID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetEmployeePerformanceToPrint(string sysDocID, string voucherID);
	}
}
