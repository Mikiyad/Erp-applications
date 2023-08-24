using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeGroupSystem : MarshalByRefObject, IEmployeeGroupSystem, IDisposable
	{
		private Config config;

		public EmployeeGroupSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeGroup(EmployeeGroupData data)
		{
			return new EmployeeGroup(config).InsertEmployeeGroup(data);
		}

		public bool UpdateEmployeeGroup(EmployeeGroupData data)
		{
			return UpdateEmployeeGroup(data, checkConcurrency: false);
		}

		public bool UpdateEmployeeGroup(EmployeeGroupData data, bool checkConcurrency)
		{
			return new EmployeeGroup(config).UpdateEmployeeGroup(data);
		}

		public EmployeeGroupData GetEmployeeGroup()
		{
			using (EmployeeGroup employeeGroup = new EmployeeGroup(config))
			{
				return employeeGroup.GetEmployeeGroup();
			}
		}

		public bool DeleteEmployeeGroup(string groupID)
		{
			using (EmployeeGroup employeeGroup = new EmployeeGroup(config))
			{
				return employeeGroup.DeleteEmployeeGroup(groupID);
			}
		}

		public EmployeeGroupData GetEmployeeGroupByID(string id)
		{
			using (EmployeeGroup employeeGroup = new EmployeeGroup(config))
			{
				return employeeGroup.GetEmployeeGroupByID(id);
			}
		}

		public DataSet GetEmployeeGroupByFields(params string[] columns)
		{
			using (EmployeeGroup employeeGroup = new EmployeeGroup(config))
			{
				return employeeGroup.GetEmployeeGroupByFields(columns);
			}
		}

		public DataSet GetEmployeeGroupByFields(string[] ids, params string[] columns)
		{
			using (EmployeeGroup employeeGroup = new EmployeeGroup(config))
			{
				return employeeGroup.GetEmployeeGroupByFields(ids, columns);
			}
		}

		public DataSet GetEmployeeGroupByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeGroup employeeGroup = new EmployeeGroup(config))
			{
				return employeeGroup.GetEmployeeGroupByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeeGroupList()
		{
			using (EmployeeGroup employeeGroup = new EmployeeGroup(config))
			{
				return employeeGroup.GetEmployeeGroupList();
			}
		}

		public DataSet GetEmployeeGroupComboList()
		{
			using (EmployeeGroup employeeGroup = new EmployeeGroup(config))
			{
				return employeeGroup.GetEmployeeGroupComboList();
			}
		}
	}
}
