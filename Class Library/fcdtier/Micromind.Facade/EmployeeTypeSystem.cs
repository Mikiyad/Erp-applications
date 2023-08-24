using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeTypeSystem : MarshalByRefObject, IEmployeeTypeSystem, IDisposable
	{
		private Config config;

		public EmployeeTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeType(EmployeeTypeData data)
		{
			return new EmployeeType(config).InsertEmployeeType(data);
		}

		public bool UpdateEmployeeType(EmployeeTypeData data)
		{
			return UpdateEmployeeType(data, checkConcurrency: false);
		}

		public bool UpdateEmployeeType(EmployeeTypeData data, bool checkConcurrency)
		{
			return new EmployeeType(config).UpdateEmployeeType(data);
		}

		public EmployeeTypeData GetEmployeeType()
		{
			using (EmployeeType employeeType = new EmployeeType(config))
			{
				return employeeType.GetEmployeeType();
			}
		}

		public bool DeleteEmployeeType(string typeID)
		{
			using (EmployeeType employeeType = new EmployeeType(config))
			{
				return employeeType.DeleteEmployeeType(typeID);
			}
		}

		public EmployeeTypeData GetEmployeeTypeByID(string id)
		{
			using (EmployeeType employeeType = new EmployeeType(config))
			{
				return employeeType.GetEmployeeTypeByID(id);
			}
		}

		public DataSet GetEmployeeTypeByFields(params string[] columns)
		{
			using (EmployeeType employeeType = new EmployeeType(config))
			{
				return employeeType.GetEmployeeTypeByFields(columns);
			}
		}

		public DataSet GetEmployeeTypeByFields(string[] ids, params string[] columns)
		{
			using (EmployeeType employeeType = new EmployeeType(config))
			{
				return employeeType.GetEmployeeTypeByFields(ids, columns);
			}
		}

		public DataSet GetEmployeeTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeType employeeType = new EmployeeType(config))
			{
				return employeeType.GetEmployeeTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeeTypeList()
		{
			using (EmployeeType employeeType = new EmployeeType(config))
			{
				return employeeType.GetEmployeeTypeList();
			}
		}

		public DataSet GetEmployeeTypeComboList()
		{
			using (EmployeeType employeeType = new EmployeeType(config))
			{
				return employeeType.GetEmployeeTypeComboList();
			}
		}
	}
}
