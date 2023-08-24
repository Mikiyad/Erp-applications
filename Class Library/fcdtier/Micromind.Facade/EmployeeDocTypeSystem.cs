using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeDocTypeSystem : MarshalByRefObject, IEmployeeDocTypeSystem, IDisposable
	{
		private Config config;

		public EmployeeDocTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeDocType(EmployeeDocTypeData data)
		{
			return new EmployeeDocType(config).InsertEmployeeDocType(data);
		}

		public bool UpdateEmployeeDocType(EmployeeDocTypeData data)
		{
			return UpdateEmployeeDocType(data, checkConcurrency: false);
		}

		public bool UpdateEmployeeDocType(EmployeeDocTypeData data, bool checkConcurrency)
		{
			return new EmployeeDocType(config).UpdateEmployeeDocType(data);
		}

		public EmployeeDocTypeData GetEmployeeDocType()
		{
			using (EmployeeDocType employeeDocType = new EmployeeDocType(config))
			{
				return employeeDocType.GetEmployeeDocType();
			}
		}

		public bool DeleteEmployeeDocType(string groupID)
		{
			using (EmployeeDocType employeeDocType = new EmployeeDocType(config))
			{
				return employeeDocType.DeleteEmployeeDocType(groupID);
			}
		}

		public EmployeeDocTypeData GetEmployeeDocTypeByID(string id)
		{
			using (EmployeeDocType employeeDocType = new EmployeeDocType(config))
			{
				return employeeDocType.GetEmployeeDocTypeByID(id);
			}
		}

		public DataSet GetEmployeeDocTypeByFields(params string[] columns)
		{
			using (EmployeeDocType employeeDocType = new EmployeeDocType(config))
			{
				return employeeDocType.GetEmployeeDocTypeByFields(columns);
			}
		}

		public DataSet GetEmployeeDocTypeByFields(string[] ids, params string[] columns)
		{
			using (EmployeeDocType employeeDocType = new EmployeeDocType(config))
			{
				return employeeDocType.GetEmployeeDocTypeByFields(ids, columns);
			}
		}

		public DataSet GetEmployeeDocTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeDocType employeeDocType = new EmployeeDocType(config))
			{
				return employeeDocType.GetEmployeeDocTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeeDocTypeList()
		{
			using (EmployeeDocType employeeDocType = new EmployeeDocType(config))
			{
				return employeeDocType.GetEmployeeDocTypeList();
			}
		}

		public DataSet GetEmployeeDocTypeComboList()
		{
			using (EmployeeDocType employeeDocType = new EmployeeDocType(config))
			{
				return employeeDocType.GetEmployeeDocTypeComboList();
			}
		}
	}
}
