using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICompanyDocumentSystem
	{
		bool CreateCompanyDocument(CompanyDocumentData companyDocumentData);

		bool UpdateCompanyDocument(CompanyDocumentData companyDocumentData);

		CompanyDocumentData GetCompanyDocument();

		bool DeleteCompanyDocument(string ID);

		CompanyDocumentData GetCompanyDocumentByID(string id);

		CompanyDocumentData GetCompanyDocumentsByCompanyID(string companyID);

		DataSet GetCompanyDocumentByFields(params string[] columns);

		DataSet GetCompanyDocumentByFields(string[] ids, params string[] columns);

		DataSet GetCompanyDocumentByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCompanyDocumentList();

		DataSet GetCompanyDocumentComboList();
	}
}
