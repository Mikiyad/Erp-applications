using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class BudgetingSystem : MarshalByRefObject, IBudgetingSystem, IDisposable
	{
		private Config config;

		public BudgetingSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateBugeting(BudgetingData budgetingData, bool isUpdate)
		{
			return new Budgeting(config).InsertUpdateBudgeting(budgetingData, isUpdate);
		}

		public bool UpdateBudgeting(BudgetingData budgetingData)
		{
			return new Budgeting(config).InsertUpdateBudgeting(budgetingData, isUpdate: true);
		}

		public bool DeleteBudgeting(string sysDocID, string voucherID)
		{
			using (Budgeting budgeting = new Budgeting(config))
			{
				return budgeting.DeleteBudgeting(sysDocID, voucherID);
			}
		}

		public bool VoidBudgeting(string sysDocID, string voucherID, bool isVoid)
		{
			using (Budgeting budgeting = new Budgeting(config))
			{
				return budgeting.VoidBudgeting(sysDocID, voucherID, isVoid);
			}
		}

		public BudgetingData GetBudgetingByID(string sysDocID, string voucherID)
		{
			using (Budgeting budgeting = new Budgeting(config))
			{
				return budgeting.GetJournalVoucherByID(sysDocID, voucherID);
			}
		}

		public DataSet GetBudgetingReport(DateTime from, DateTime to, string fromLocation, string toLocation, bool isVoid)
		{
			using (Budgeting budgeting = new Budgeting(config))
			{
				return budgeting.GetJournalReport(from, to, fromLocation, toLocation, isVoid);
			}
		}

		public DataSet GetBudgetingToPrint(string sysDocID, string[] voucherID)
		{
			return new Budgeting(config).GetBudgetingToPrint(sysDocID, voucherID);
		}

		public DataSet GetBudgetingToPrint(string sysDocID, string voucherID)
		{
			return new Budgeting(config).GetBudgetingToPrint(sysDocID, new string[1]
			{
				voucherID
			});
		}

		public DataSet GetBudgetingList(DateTime from, DateTime to, bool showVoid)
		{
			return new Budgeting(config).GetBudgetingList(from, to, showVoid);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new Budgeting(config).GetList(from, to, showVoid);
		}
	}
}
