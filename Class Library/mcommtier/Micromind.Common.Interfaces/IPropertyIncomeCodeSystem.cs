using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IPropertyIncomeCodeSystem
	{
		bool CreatePropertyIncomeCode(PropertyIncomeCodeData IncomeCodeData);

		bool UpdatePropertyIncomeCode(PropertyIncomeCodeData IncomeCodeData);

		PropertyIncomeCodeData GetPropertyIncomeCode();

		bool DeletePropertyIncomeCode(string ID);

		PropertyIncomeCodeData GetPropertyIncomeCodeByID(string id);

		DataSet GetPropertyIncomeCodeList();

		DataSet GetPropertyIncomeCodeComboList();
	}
}
