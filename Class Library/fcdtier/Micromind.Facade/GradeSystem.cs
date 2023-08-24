using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class GradeSystem : MarshalByRefObject, IGradeSystem, IDisposable
	{
		private Config config;

		public GradeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateGrade(GradeData data)
		{
			return new Grade(config).InsertGrade(data);
		}

		public bool UpdateGrade(GradeData data)
		{
			return UpdateGrade(data, checkConcurrency: false);
		}

		public bool UpdateGrade(GradeData data, bool checkConcurrency)
		{
			return new Grade(config).UpdateGrade(data);
		}

		public GradeData GetGrade()
		{
			using (Grade grade = new Grade(config))
			{
				return grade.GetGrade();
			}
		}

		public bool DeleteGrade(string groupID)
		{
			using (Grade grade = new Grade(config))
			{
				return grade.DeleteGrade(groupID);
			}
		}

		public GradeData GetGradeByID(string id)
		{
			using (Grade grade = new Grade(config))
			{
				return grade.GetGradeByID(id);
			}
		}

		public DataSet GetGradeByFields(params string[] columns)
		{
			using (Grade grade = new Grade(config))
			{
				return grade.GetGradeByFields(columns);
			}
		}

		public DataSet GetGradeByFields(string[] ids, params string[] columns)
		{
			using (Grade grade = new Grade(config))
			{
				return grade.GetGradeByFields(ids, columns);
			}
		}

		public DataSet GetGradeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Grade grade = new Grade(config))
			{
				return grade.GetGradeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetGradeList()
		{
			using (Grade grade = new Grade(config))
			{
				return grade.GetGradeList();
			}
		}

		public DataSet GetGradeComboList()
		{
			using (Grade grade = new Grade(config))
			{
				return grade.GetGradeComboList();
			}
		}
	}
}
