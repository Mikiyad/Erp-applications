using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IExpenseCodeSystem
	{
		bool CreateExpenseCode(ExpenseCodeData expenseCodeData);

		bool UpdateExpenseCode(ExpenseCodeData expenseCodeData);

		ExpenseCodeData GetExpenseCode();

		bool DeleteExpenseCode(string ID);

		ExpenseCodeData GetExpenseCodeByID(string id);

		DataSet GetExpenseCodeList();

		DataSet GetExpenseCodeComboList();
	}
}
