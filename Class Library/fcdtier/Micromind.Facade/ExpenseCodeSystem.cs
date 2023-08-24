using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ExpenseCodeSystem : MarshalByRefObject, IExpenseCodeSystem, IDisposable
	{
		private Config config;

		public ExpenseCodeSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateExpenseCode(ExpenseCodeData data)
		{
			return new ExpenseCode(config).InsertExpenseCode(data);
		}

		public bool UpdateExpenseCode(ExpenseCodeData data)
		{
			return UpdateExpenseCode(data, checkConcurrency: false);
		}

		public bool UpdateExpenseCode(ExpenseCodeData data, bool checkConcurrency)
		{
			return new ExpenseCode(config).UpdateExpenseCode(data);
		}

		public ExpenseCodeData GetExpenseCode()
		{
			using (ExpenseCode expenseCode = new ExpenseCode(config))
			{
				return expenseCode.GetExpenseCode();
			}
		}

		public bool DeleteExpenseCode(string groupID)
		{
			using (ExpenseCode expenseCode = new ExpenseCode(config))
			{
				return expenseCode.DeleteExpenseCode(groupID);
			}
		}

		public ExpenseCodeData GetExpenseCodeByID(string id)
		{
			using (ExpenseCode expenseCode = new ExpenseCode(config))
			{
				return expenseCode.GetExpenseCodeByID(id);
			}
		}

		public DataSet GetExpenseCodeByFields(params string[] columns)
		{
			using (ExpenseCode expenseCode = new ExpenseCode(config))
			{
				return expenseCode.GetExpenseCodeByFields(columns);
			}
		}

		public DataSet GetExpenseCodeByFields(string[] ids, params string[] columns)
		{
			using (ExpenseCode expenseCode = new ExpenseCode(config))
			{
				return expenseCode.GetExpenseCodeByFields(ids, columns);
			}
		}

		public DataSet GetExpenseCodeByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (ExpenseCode expenseCode = new ExpenseCode(config))
			{
				return expenseCode.GetExpenseCodeByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetExpenseCodeList()
		{
			using (ExpenseCode expenseCode = new ExpenseCode(config))
			{
				return expenseCode.GetExpenseCodeList();
			}
		}

		public DataSet GetExpenseCodeComboList()
		{
			using (ExpenseCode expenseCode = new ExpenseCode(config))
			{
				return expenseCode.GetExpenseCodeComboList();
			}
		}
	}
}
