using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ITaxGroupSystem
	{
		bool CreateTaxGroup(TaxGroupData expenseCodeData);

		bool UpdateTaxGroup(TaxGroupData expenseCodeData);

		TaxGroupData GetTaxGroup();

		bool DeleteTaxGroup(string ID);

		TaxGroupData GetTaxGroupByID(string id);

		DataSet GetTaxGroupList();

		DataSet GetTaxClassList(string ID);

		DataSet GetTaxGroupComboList();

		DataSet GetTaxGroupDetailList();

		DataSet GetTaxDatabasedonproductID(string TaxID, string BasedonID);

		DataSet GetTaxDatabasedonvendorID(string TaxID, string BasedonID);

		DataSet GetTaxDatabasedonCustomerID(string TaxID, string BasedonID);

		DataSet GetTaxDatabasedonGroupID(string GroupID);
	}
}
