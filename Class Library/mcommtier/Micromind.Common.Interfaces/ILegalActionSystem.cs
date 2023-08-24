using Micromind.Common.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ILegalActionSystem
	{
		bool CreateLegalAction(LegalActionData crmactivityData);

		bool UpdateLegalAction(LegalActionData crmactivityData);

		LegalActionData GetLegalAction();

		bool DeleteLegalAction(string sysDocID, string voucherID);

		LegalActionData GetLegalActionByID(string sysDocID, string voucherID, string AnalysisID);

		LegalActionData GetLegalActionExpenseByID(string sysDocID, string voucherID, string AnalysisID);

		LegalActionData GetCustomerLegalActionByID(string sysDocID, string voucherID);

		DataSet GetLeaglActivityToPrint(string sysDocID, string VoucherID);

		DataSet GetLegalActionList(DateTime from, DateTime to);

		DataSet GetCustomerLegalActionList(DateTime from, DateTime to);

		DataSet GetLegalActionListByLeadID(CRMRelatedTypes leadType, string leadID, DateTime from, DateTime to);

		DataSet GetLegalActionComboList();

		DataSet GetPendingCasesReport(DateTime fromDate, DateTime ToDate, string FromCustomer, string ToCumstomer, string FromClass, string ToClass, string FromGroup, string ToGroup, string StatusID);

		DataSet GetCaseHistoryReport(DateTime fromDate, DateTime ToDate, string FromCustomer, string ToCumstomer, string FromClass, string ToClass, string FromGroup, string ToGroup, string FromLawyer, string ToLawyer, string fileNumber);

		DataSet GetCaseLawyerTrackReport(DateTime fromDate, DateTime ToDate, string FromLawyer, string ToLawyer);

		DataSet GetLegalActionReportList(DateTime from, DateTime to);

		DataSet GetLegalDocIDList(string SysDocID);

		DataSet GetLegalActionHistory(List<Tuple<string, string>> list);
	}
}
