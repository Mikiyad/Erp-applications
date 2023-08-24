using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class DepartmentSystem : MarshalByRefObject, IDepartmentSystem, IDisposable
	{
		private Config config;

		public DepartmentSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateDepartment(DepartmentData data)
		{
			return new Department(config).InsertDepartment(data);
		}

		public bool UpdateDepartment(DepartmentData data)
		{
			return UpdateDepartment(data, checkConcurrency: false);
		}

		public bool UpdateDepartment(DepartmentData data, bool checkConcurrency)
		{
			return new Department(config).UpdateDepartment(data);
		}

		public DepartmentData GetDepartment()
		{
			using (Department department = new Department(config))
			{
				return department.GetDepartment();
			}
		}

		public bool DeleteDepartment(string groupID)
		{
			using (Department department = new Department(config))
			{
				return department.DeleteDepartment(groupID);
			}
		}

		public DepartmentData GetDepartmentByID(string id)
		{
			using (Department department = new Department(config))
			{
				return department.GetDepartmentByID(id);
			}
		}

		public DataSet GetDepartmentByFields(params string[] columns)
		{
			using (Department department = new Department(config))
			{
				return department.GetDepartmentByFields(columns);
			}
		}

		public DataSet GetDepartmentByFields(string[] ids, params string[] columns)
		{
			using (Department department = new Department(config))
			{
				return department.GetDepartmentByFields(ids, columns);
			}
		}

		public DataSet GetDepartmentByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Department department = new Department(config))
			{
				return department.GetDepartmentByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetDepartmentList()
		{
			using (Department department = new Department(config))
			{
				return department.GetDepartmentList();
			}
		}

		public DataSet GetDepartmentComboList()
		{
			using (Department department = new Department(config))
			{
				return department.GetDepartmentComboList();
			}
		}
	}
}
