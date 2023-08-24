using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeePerformanceSystem : MarshalByRefObject, IEmployeePerformanceSystem, IDisposable
	{
		private Config config;

		public EmployeePerformanceSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreatePerformance(EmployeePerformanceData data)
		{
			return new EmployeePerformance(config).InsertPerformance(data);
		}

		public bool UpdatePerformance(EmployeePerformanceData data)
		{
			return UpdatePerformance(data, checkConcurrency: false);
		}

		public bool UpdatePerformance(EmployeePerformanceData data, bool checkConcurrency)
		{
			return new EmployeePerformance(config).UpdatePerformance(data);
		}

		public EmployeePerformanceData GetPerformance()
		{
			using (EmployeePerformance employeePerformance = new EmployeePerformance(config))
			{
				return employeePerformance.GetPerformance();
			}
		}

		public bool DeletePerformance(string groupID, string SysDocid)
		{
			using (EmployeePerformance employeePerformance = new EmployeePerformance(config))
			{
				return employeePerformance.DeletePerformance(groupID, SysDocid);
			}
		}

		public EmployeePerformanceData GetPerformanceByID(string sysDocID, string id)
		{
			using (EmployeePerformance employeePerformance = new EmployeePerformance(config))
			{
				return employeePerformance.GetPerformanceByID(sysDocID, id);
			}
		}

		public DataSet GetPerformanceByFields(params string[] columns)
		{
			using (EmployeePerformance employeePerformance = new EmployeePerformance(config))
			{
				return employeePerformance.GetPerformanceByFields(columns);
			}
		}

		public DataSet GetPerformanceByFields(string[] ids, params string[] columns)
		{
			using (EmployeePerformance employeePerformance = new EmployeePerformance(config))
			{
				return employeePerformance.GetPerformanceByFields(ids, columns);
			}
		}

		public DataSet GetPerformanceByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeePerformance employeePerformance = new EmployeePerformance(config))
			{
				return employeePerformance.GetPerformanceByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetPerformanceList()
		{
			using (EmployeePerformance employeePerformance = new EmployeePerformance(config))
			{
				return employeePerformance.GetPerformanceList();
			}
		}

		public DataSet GetPerformanceComboList()
		{
			using (EmployeePerformance employeePerformance = new EmployeePerformance(config))
			{
				return employeePerformance.GetPerformanceComboList();
			}
		}

		public DataSet GetEmployeePerformanceList(string EmployeeID)
		{
			using (EmployeePerformance employeePerformance = new EmployeePerformance(config))
			{
				return employeePerformance.GetEmployeePerfromanceList(EmployeeID);
			}
		}

		public DataSet GetList(DateTime fromDate, DateTime toDate, bool Isvoid)
		{
			return new EmployeePerformance(config).GetList(fromDate, toDate, showVoid: true);
		}

		public DataSet GetEmployeePerformanceToPrint(string sysDocID, string voucherID)
		{
			return new EmployeePerformance(config).GetEmployeePerformanceToPrint(sysDocID, voucherID);
		}
	}
}
