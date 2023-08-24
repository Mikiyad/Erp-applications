using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class EmployeeSkillSystem : MarshalByRefObject, IEmployeeSkillSystem, IDisposable
	{
		private Config config;

		public EmployeeSkillSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateEmployeeSkill(EmployeeSkillData data)
		{
			return new EmployeeSkills(config).InsertEmployeeSkill(data);
		}

		public bool UpdateEmployeeSkill(EmployeeSkillData data)
		{
			return UpdateEmployeeSkill(data, checkConcurrency: false);
		}

		public bool UpdateEmployeeSkill(EmployeeSkillData data, bool checkConcurrency)
		{
			return new EmployeeSkills(config).UpdateEmployeeSkill(data);
		}

		public EmployeeSkillData GetEmployeeSkill()
		{
			using (EmployeeSkills employeeSkills = new EmployeeSkills(config))
			{
				return employeeSkills.GetEmployeeSkill();
			}
		}

		public bool DeleteEmployeeSkill(string groupID)
		{
			using (EmployeeSkills employeeSkills = new EmployeeSkills(config))
			{
				return employeeSkills.DeleteEmployeeSkill(groupID);
			}
		}

		public EmployeeSkillData GetEmployeeSkillByID(string id)
		{
			using (EmployeeSkills employeeSkills = new EmployeeSkills(config))
			{
				return employeeSkills.GetEmployeeSkillByID(id);
			}
		}

		public EmployeeSkillData GetEmployeeSkillsByEmployeeID(string employeeID)
		{
			using (EmployeeSkills employeeSkills = new EmployeeSkills(config))
			{
				return employeeSkills.GetEmployeeSkillsByEmployeeID(employeeID);
			}
		}

		public DataSet GetEmployeeSkillByFields(params string[] columns)
		{
			using (EmployeeSkills employeeSkills = new EmployeeSkills(config))
			{
				return employeeSkills.GetEmployeeSkillByFields(columns);
			}
		}

		public DataSet GetEmployeeSkillByFields(string[] ids, params string[] columns)
		{
			using (EmployeeSkills employeeSkills = new EmployeeSkills(config))
			{
				return employeeSkills.GetEmployeeSkillByFields(ids, columns);
			}
		}

		public DataSet GetEmployeeSkillByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (EmployeeSkills employeeSkills = new EmployeeSkills(config))
			{
				return employeeSkills.GetEmployeeSkillByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetEmployeeSkillList()
		{
			using (EmployeeSkills employeeSkills = new EmployeeSkills(config))
			{
				return employeeSkills.GetEmployeeSkillList();
			}
		}

		public DataSet GetEmployeeSkillComboList()
		{
			using (EmployeeSkills employeeSkills = new EmployeeSkills(config))
			{
				return employeeSkills.GetEmployeeSkillComboList();
			}
		}
	}
}
