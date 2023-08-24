using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Micromind.Facade
{
	public sealed class LegalActionSystem : MarshalByRefObject, ILegalActionSystem, IDisposable
	{
		private Config config;

		public LegalActionSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateLegalAction(LegalActionData data)
		{
			return new LegalAction(config).InsertUpdateLegalAction(data, isUpdate: false);
		}

		public bool UpdateLegalAction(LegalActionData data)
		{
			return UpdateLegalAction(data, checkConcurrency: true);
		}

		public bool UpdateLegalAction(LegalActionData data, bool checkConcurrency)
		{
			return new LegalAction(config).InsertUpdateLegalAction(data, isUpdate: true);
		}

		public LegalActionData GetLegalAction()
		{
			using (LegalAction legalAction = new LegalAction(config))
			{
				return legalAction.GetLegalAction();
			}
		}

		public bool DeleteLegalAction(string sysDocID, string voucherID)
		{
			using (LegalAction legalAction = new LegalAction(config))
			{
				return legalAction.DeleteLegalAction(sysDocID, voucherID);
			}
		}

		public DataSet GetLeaglActivityToPrint(string sysDocID, string voucherID)
		{
			return new LegalAction(config).GetLeaglActivityToPrint(sysDocID, voucherID);
		}

		public LegalActionData GetLegalActionByID(string sysDocID, string voucherID, string AnalysisID)
		{
			using (LegalAction legalAction = new LegalAction(config))
			{
				return legalAction.GetLegalActionByID(sysDocID, voucherID, AnalysisID);
			}
		}

		public LegalActionData GetLegalActionExpenseByID(string sysDocID, string voucherID, string AnalysisID)
		{
			using (LegalAction legalAction = new LegalAction(config))
			{
				return legalAction.GetLegalActionExpenseByID(sysDocID, voucherID, AnalysisID);
			}
		}

		public LegalActionData GetCustomerLegalActionByID(string sysDocID, string voucherID)
		{
			using (LegalAction legalAction = new LegalAction(config))
			{
				return legalAction.GetCustomerActivityByID(sysDocID, voucherID);
			}
		}

		public DataSet GetLegalActionList(DateTime from, DateTime to)
		{
			using (LegalAction legalAction = new LegalAction(config))
			{
				return legalAction.GetLegalActionList(CRMRelatedTypes.LegalAction, "", from, to);
			}
		}

		public DataSet GetLegalActionReportList(DateTime from, DateTime to)
		{
			using (LegalAction legalAction = new LegalAction(config))
			{
				return legalAction.GetLegalActionReportList(CRMRelatedTypes.LegalAction, "", from, to);
			}
		}

		public DataSet GetCustomerLegalActionList(DateTime from, DateTime to)
		{
			using (LegalAction legalAction = new LegalAction(config))
			{
				return legalAction.GetCustomerActivityList(CRMRelatedTypes.Customer, "", from, to);
			}
		}

		public DataSet GetLegalActionListByLeadID(CRMRelatedTypes leadType, string leadID, DateTime from, DateTime to)
		{
			using (LegalAction legalAction = new LegalAction(config))
			{
				return legalAction.GetLegalActionListByLeadID(leadType, leadID, from, to);
			}
		}

		public DataSet GetLegalActionComboList()
		{
			using (LegalAction legalAction = new LegalAction(config))
			{
				return legalAction.GetLegalActionComboList();
			}
		}

		public DataSet GetPendingCasesReport(DateTime fromDate, DateTime ToDate, string FromCustomer, string ToCumstomer, string FromClass, string ToClass, string FromGroup, string ToGroup, string StatusID)
		{
			return new LegalAction(config).GetPendingCasesReport(fromDate, ToDate, FromCustomer, ToCumstomer, FromClass, ToClass, FromGroup, ToGroup, StatusID);
		}

		public DataSet GetCaseHistoryReport(DateTime fromDate, DateTime ToDate, string FromCustomer, string ToCumstomer, string FromClass, string ToClass, string FromGroup, string ToGroup, string FromLawyer, string ToLawyer, string fileNumber)
		{
			return new LegalAction(config).GetCaseHistoryReport(fromDate, ToDate, FromCustomer, ToCumstomer, FromClass, ToClass, FromGroup, ToGroup, FromLawyer, ToLawyer, fileNumber);
		}

		public DataSet GetCaseLawyerTrackReport(DateTime fromDate, DateTime ToDate, string FromLawyer, string ToLawyer)
		{
			return new LegalAction(config).GetCaseLawyerTrackReport(fromDate, ToDate, FromLawyer, ToLawyer);
		}

		public DataSet GetLegalDocIDList(string SysDocID)
		{
			using (LegalAction legalAction = new LegalAction(config))
			{
				return legalAction.GetLegalDocIDList(SysDocID);
			}
		}

		public DataSet GetLegalActionHistory(List<Tuple<string, string>> list)
		{
			using (LegalAction legalAction = new LegalAction(config))
			{
				return legalAction.GetLegalActionHistory(list);
			}
		}
	}
}
