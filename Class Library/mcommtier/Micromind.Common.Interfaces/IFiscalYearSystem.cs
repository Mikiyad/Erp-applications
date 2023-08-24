using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IFiscalYearSystem
	{
		bool CreateFiscalYear(FiscalYearData fiscalYearData);

		bool UpdateFiscalYear(FiscalYearData fiscalYearData);

		FiscalYearData GetFiscalYear();

		bool DeleteFiscalYear(string ID);

		FiscalYearData GetFiscalYearByID(string id);

		DataSet GetFiscalYearByFields(params string[] columns);

		DataSet GetFiscalYearByFields(string[] ids, params string[] columns);

		DataSet GetFiscalYearByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetFiscalYearList();

		DataSet GetFiscalYearComboList();

		int CanCloseFiscalYear(string fiscalYearID);

		bool CloseFiscalYear(string fiscalYearID, GLData closingData, string retainedEarningAccountID);

		bool ReopenFiscalYear(string fiscalYearID);

		bool CreateSummaryTable(string sysDocID, string voucherID, string fiscalYearID);

		int GetPendingDeliveryNotesCount(DateTime toDate);

		int GetPendingGRNCount(DateTime toDate);

		int GetPendingTransferCount(DateTime toDate);

		int GetNegativeStockCount(DateTime toDate);

		bool IsTBCorrect(DateTime toDate);

		int GetUnbalancedJournalsCount(DateTime toDate);

		int GetUncostedItemsCount(DateTime toDate);
	}
}
