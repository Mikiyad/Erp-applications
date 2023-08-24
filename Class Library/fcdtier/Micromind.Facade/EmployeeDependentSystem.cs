using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeDependentSystem : MarshalByRefObject, IEmployeeDependentSystem, IDisposable
	{
		private Config config;

		public EmployeeDependentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeDependent(EmployeeDependentData data)
		{
			return new EmployeeDependentes(config).InsertEmployeeDependent(data);
		}

		public bool UpdateEmployeeDependent(EmployeeDependentData data)
		{
			return UpdateEmployeeDependent(data, checkConcurrency: false);
		}

		public bool UpdateEmployeeDependent(EmployeeDependentData data, bool checkConcurrency)
		{
			return new EmployeeDependentes(config).UpdateEmployeeDependent(data);
		}

		public EmployeeDependentData GetEmployeeDependent()
		{
			using (EmployeeDependentes employeeDependentes = new EmployeeDependentes(config))
			{
				return employeeDependentes.GetEmployeeDependent();
			}
		}

		public bool DeleteEmployeeDependent(string dependentID, string employeeID)
		{
			using (EmployeeDependentes employeeDependentes = new EmployeeDependentes(config))
			{
				return employeeDependentes.DeleteEmployeeDependent(dependentID, employeeID);
			}
		}

		public EmployeeDependentData GetEmployeeDependentByID(string employeeID, string dependentID)
		{
			using (EmployeeDependentes employeeDependentes = new EmployeeDependentes(config))
			{
				return employeeDependentes.GetEmployeeDependentByID(employeeID, dependentID);
			}
		}

		public DataSet GetEmployeeDependentByFields(params string[] columns)
		{
			using (EmployeeDependentes employeeDependentes = new EmployeeDependentes(config))
			{
				return employeeDependentes.GetEmployeeDependentByFields(columns);
			}
		}

		public DataSet GetEmployeeDependentByFields(string[] ids, params string[] columns)
		{
			using (EmployeeDependentes employeeDependentes = new EmployeeDependentes(config))
			{
				return employeeDependentes.GetEmployeeDependentByFields(ids, columns);
			}
		}

		public DataSet GetEmployeeDependentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeDependentes employeeDependentes = new EmployeeDependentes(config))
			{
				return employeeDependentes.GetEmployeeDependentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeeDependentList()
		{
			using (EmployeeDependentes employeeDependentes = new EmployeeDependentes(config))
			{
				return employeeDependentes.GetEmployeeDependentList();
			}
		}
	}
}
