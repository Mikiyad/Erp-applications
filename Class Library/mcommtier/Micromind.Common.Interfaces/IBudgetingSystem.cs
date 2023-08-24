using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IBudgetingSystem
	{
		bool CreateBugeting(BudgetingData budgetingData, bool isUpdate);

		bool UpdateBudgeting(BudgetingData budgetingData);

		bool DeleteBudgeting(string sysDocID, string journalID);

		bool VoidBudgeting(string sysDocID, string journalID, bool isVoid);

		BudgetingData GetBudgetingByID(string sysDocID, string voucherID);

		DataSet GetBudgetingReport(DateTime from, DateTime to, string fromLocationID, string toLocationID, bool showVoid);

		DataSet GetBudgetingToPrint(string sysDocID, string[] voucherID);

		DataSet GetBudgetingToPrint(string sysDocID, string voucherID);

		DataSet GetBudgetingList(DateTime from, DateTime to, bool showVoid);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);
	}
}
