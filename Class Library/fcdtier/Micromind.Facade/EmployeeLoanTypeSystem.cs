using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeLoanTypeSystem : MarshalByRefObject, IEmployeeLoanTypeSystem, IDisposable
	{
		private Config config;

		public EmployeeLoanTypeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeLoanType(EmployeeLoanTypeData data)
		{
			return new EmployeeLoanType(config).InsertEmployeeLoanType(data);
		}

		public bool UpdateEmployeeLoanType(EmployeeLoanTypeData data)
		{
			return UpdateEmployeeLoanType(data, checkConcurrency: false);
		}

		public bool UpdateEmployeeLoanType(EmployeeLoanTypeData data, bool checkConcurrency)
		{
			return new EmployeeLoanType(config).UpdateEmployeeLoanType(data);
		}

		public EmployeeLoanTypeData GetEmployeeLoanType()
		{
			using (EmployeeLoanType employeeLoanType = new EmployeeLoanType(config))
			{
				return employeeLoanType.GetEmployeeLoanType();
			}
		}

		public bool DeleteEmployeeLoanType(string groupID)
		{
			using (EmployeeLoanType employeeLoanType = new EmployeeLoanType(config))
			{
				return employeeLoanType.DeleteEmployeeLoanType(groupID);
			}
		}

		public EmployeeLoanTypeData GetEmployeeLoanTypeByID(string id)
		{
			using (EmployeeLoanType employeeLoanType = new EmployeeLoanType(config))
			{
				return employeeLoanType.GetEmployeeLoanTypeByID(id);
			}
		}

		public DataSet GetEmployeeLoanTypeByFields(params string[] columns)
		{
			using (EmployeeLoanType employeeLoanType = new EmployeeLoanType(config))
			{
				return employeeLoanType.GetEmployeeLoanTypeByFields(columns);
			}
		}

		public DataSet GetEmployeeLoanTypeByFields(string[] ids, params string[] columns)
		{
			using (EmployeeLoanType employeeLoanType = new EmployeeLoanType(config))
			{
				return employeeLoanType.GetEmployeeLoanTypeByFields(ids, columns);
			}
		}

		public DataSet GetEmployeeLoanTypeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeLoanType employeeLoanType = new EmployeeLoanType(config))
			{
				return employeeLoanType.GetEmployeeLoanTypeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeeLoanTypeList()
		{
			using (EmployeeLoanType employeeLoanType = new EmployeeLoanType(config))
			{
				return employeeLoanType.GetEmployeeLoanTypeList();
			}
		}

		public DataSet GetEmployeeLoanTypeComboList()
		{
			using (EmployeeLoanType employeeLoanType = new EmployeeLoanType(config))
			{
				return employeeLoanType.GetEmployeeLoanTypeComboList();
			}
		}
	}
}
