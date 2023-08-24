using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICompanyDocTypeSystem
	{
		bool CreateCompanyDocType(CompanyDocTypeData typeData);

		bool UpdateCompanyDocType(CompanyDocTypeData typeData);

		CompanyDocTypeData GetCompanyDocType();

		bool DeleteCompanyDocType(string ID);

		CompanyDocTypeData GetCompanyDocTypeByID(string id);

		DataSet GetCompanyDocTypeByFields(params string[] columns);

		DataSet GetCompanyDocTypeByFields(string[] ids, params string[] columns);

		DataSet GetCompanyDocTypeByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCompanyDocTypeList();

		DataSet GetCompanyDocTypeComboList();
	}
}
