using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ITaxSystem
	{
		bool CreateTax(TaxData expenseCodeData);

		bool UpdateTax(TaxData expenseCodeData);

		TaxData GetTax();

		bool DeleteTax(string ID);

		TaxData GetTaxByID(string id);

		DataSet GetTaxList();

		DataSet GetTaxClassList(string ID);

		DataSet GetTaxComboList();
	}
}
