using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICompanyDivisionSystem
	{
		bool CreateDivision(CompanyDivisionData CompanyDivisionData);

		bool UpdateDivision(CompanyDivisionData CompanyDivisionData);

		CompanyDivisionData GetDivision();

		bool DeleteDivision(string ID);

		CompanyDivisionData GetDivisionByID(string id);

		DataSet GetDivisionByFields(params string[] columns);

		DataSet GetDivisionByFields(string[] ids, params string[] columns);

		DataSet GetDivisionByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetDivisionList();

		DataSet GetDivisionComboList();
	}
}
