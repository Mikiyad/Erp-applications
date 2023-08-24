using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeSkillSystem
	{
		bool CreateEmployeeSkill(EmployeeSkillData employeeSkillData);

		bool UpdateEmployeeSkill(EmployeeSkillData employeeSkillData);

		EmployeeSkillData GetEmployeeSkill();

		bool DeleteEmployeeSkill(string ID);

		EmployeeSkillData GetEmployeeSkillByID(string id);

		EmployeeSkillData GetEmployeeSkillsByEmployeeID(string employeeID);

		DataSet GetEmployeeSkillByFields(params string[] columns);

		DataSet GetEmployeeSkillByFields(string[] ids, params string[] columns);

		DataSet GetEmployeeSkillByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetEmployeeSkillList();

		DataSet GetEmployeeSkillComboList();
	}
}
