using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISkillSystem
	{
		bool CreateSkill(SkillData skillData);

		bool UpdateSkill(SkillData skillData);

		SkillData GetSkill();

		bool DeleteSkill(string ID);

		SkillData GetSkillByID(string id);

		DataSet GetSkillByFields(params string[] columns);

		DataSet GetSkillByFields(string[] ids, params string[] columns);

		DataSet GetSkillByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetSkillList();

		DataSet GetSkillComboList();
	}
}
