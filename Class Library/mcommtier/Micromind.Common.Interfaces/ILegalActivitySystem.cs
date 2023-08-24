using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ILegalActivitySystem
	{
		bool CreateLegalActivity(LegalActivityData crmactivityData);

		bool UpdateLegalActivity(LegalActivityData crmactivityData);

		LegalActivityData GetLegalActivity();

		bool DeleteLegalActivity(string sysDocID, string voucherID);

		LegalActivityData GetLegalActivityByID(string sysDocID, string voucherID, string AnalysisID);

		LegalActivityData GetCustomerLegalActivityByID(string sysDocID, string voucherID);

		DataSet GetLeaglActivityToPrint(string sysDocID, string VoucherID);

		DataSet GetLegalActivityList(DateTime from, DateTime to);

		DataSet GetCustomerLegalActivityList(DateTime from, DateTime to);

		DataSet GetLegalActivityListByLeadID(CRMRelatedTypes leadType, string sysDocID, string leadID, DateTime from, DateTime to);

		DataSet GetLegalActivityComboList();

		DataSet GetPendingCasesReport(DateTime fromDate, DateTime ToDate, string FromCustomer, string ToCumstomer, string StatusID);

		DataSet GetCaseHistoryReport(DateTime fromDate, DateTime ToDate, string FromCustomer, string ToCumstomer, string FromLawyer, string ToLawyer, string fileNumber, string SysDcoID, string VoucherID);

		DataSet GetCaseLawyerTrackReport(DateTime fromDate, DateTime ToDate, string FromLawyer, string ToLawyer);
	}
}
