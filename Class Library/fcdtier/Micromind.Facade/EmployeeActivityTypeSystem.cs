using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeActivityTypeSystem : MarshalByRefObject, IEmployeeActivityTypeSystem, IDisposable
	{
		private Config config;

		public EmployeeActivityTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateActivityType(EmployeeActivityTypeData data)
		{
			return new EmployeeActivityType(config).InsertActivityType(data);
		}

		public bool UpdateActivityType(EmployeeActivityTypeData data)
		{
			return UpdateActivityType(data, checkConcurrency: false);
		}

		public bool UpdateActivityType(EmployeeActivityTypeData data, bool checkConcurrency)
		{
			return new EmployeeActivityType(config).UpdateActivityType(data);
		}

		public EmployeeActivityTypeData GetActivityType()
		{
			using (EmployeeActivityType employeeActivityType = new EmployeeActivityType(config))
			{
				return employeeActivityType.GetActivityType();
			}
		}

		public bool DeleteActivityType(string groupID)
		{
			using (EmployeeActivityType employeeActivityType = new EmployeeActivityType(config))
			{
				return employeeActivityType.DeleteActivityType(groupID);
			}
		}

		public EmployeeActivityTypeData GetActivityTypeByID(string id)
		{
			using (EmployeeActivityType employeeActivityType = new EmployeeActivityType(config))
			{
				return employeeActivityType.GetActivityTypeByID(id);
			}
		}

		public DataSet GetActivityTypeByFields(params string[] columns)
		{
			using (EmployeeActivityType employeeActivityType = new EmployeeActivityType(config))
			{
				return employeeActivityType.GetActivityTypeByFields(columns);
			}
		}

		public DataSet GetActivityTypeByFields(string[] ids, params string[] columns)
		{
			using (EmployeeActivityType employeeActivityType = new EmployeeActivityType(config))
			{
				return employeeActivityType.GetActivityTypeByFields(ids, columns);
			}
		}

		public DataSet GetActivityTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeActivityType employeeActivityType = new EmployeeActivityType(config))
			{
				return employeeActivityType.GetActivityTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetActivityTypeList()
		{
			using (EmployeeActivityType employeeActivityType = new EmployeeActivityType(config))
			{
				return employeeActivityType.GetActivityTypeList();
			}
		}

		public DataSet GetActivityTypeComboList()
		{
			using (EmployeeActivityType employeeActivityType = new EmployeeActivityType(config))
			{
				return employeeActivityType.GetActivityTypeComboList();
			}
		}
	}
}
