using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SkillSystem : MarshalByRefObject, ISkillSystem, IDisposable
	{
		private Config config;

		public SkillSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSkill(SkillData data)
		{
			return new Skill(config).InsertSkill(data);
		}

		public bool UpdateSkill(SkillData data)
		{
			return UpdateSkill(data, checkConcurrency: false);
		}

		public bool UpdateSkill(SkillData data, bool checkConcurrency)
		{
			return new Skill(config).UpdateSkill(data);
		}

		public SkillData GetSkill()
		{
			using (Skill skill = new Skill(config))
			{
				return skill.GetSkill();
			}
		}

		public bool DeleteSkill(string groupID)
		{
			using (Skill skill = new Skill(config))
			{
				return skill.DeleteSkill(groupID);
			}
		}

		public SkillData GetSkillByID(string id)
		{
			using (Skill skill = new Skill(config))
			{
				return skill.GetSkillByID(id);
			}
		}

		public DataSet GetSkillByFields(params string[] columns)
		{
			using (Skill skill = new Skill(config))
			{
				return skill.GetSkillByFields(columns);
			}
		}

		public DataSet GetSkillByFields(string[] ids, params string[] columns)
		{
			using (Skill skill = new Skill(config))
			{
				return skill.GetSkillByFields(ids, columns);
			}
		}

		public DataSet GetSkillByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Skill skill = new Skill(config))
			{
				return skill.GetSkillByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetSkillList()
		{
			using (Skill skill = new Skill(config))
			{
				return skill.GetSkillList();
			}
		}

		public DataSet GetSkillComboList()
		{
			using (Skill skill = new Skill(config))
			{
				return skill.GetSkillComboList();
			}
		}
	}
}
