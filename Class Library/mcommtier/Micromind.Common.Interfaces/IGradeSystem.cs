using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IGradeSystem
	{
		bool CreateGrade(GradeData gradeData);

		bool UpdateGrade(GradeData gradeData);

		GradeData GetGrade();

		bool DeleteGrade(string ID);

		GradeData GetGradeByID(string id);

		DataSet GetGradeByFields(params string[] columns);

		DataSet GetGradeByFields(string[] ids, params string[] columns);

		DataSet GetGradeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetGradeList();

		DataSet GetGradeComboList();
	}
}
