using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class LegalActivitySystem : MarshalByRefObject, ILegalActivitySystem, IDisposable
	{
		private Config config;

		public LegalActivitySystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateLegalActivity(LegalActivityData data)
		{
			return new LegalActivity(config).InsertUpdateLegalActivity(data, isUpdate: false);
		}

		public bool UpdateLegalActivity(LegalActivityData data)
		{
			return UpdateLegalActivity(data, checkConcurrency: true);
		}

		public bool UpdateLegalActivity(LegalActivityData data, bool checkConcurrency)
		{
			return new LegalActivity(config).InsertUpdateLegalActivity(data, isUpdate: true);
		}

		public LegalActivityData GetLegalActivity()
		{
			using (LegalActivity legalActivity = new LegalActivity(config))
			{
				return legalActivity.GetLegalActivity();
			}
		}

		public bool DeleteLegalActivity(string sysDocID, string voucherID)
		{
			using (LegalActivity legalActivity = new LegalActivity(config))
			{
				return legalActivity.DeleteLegalActivity(sysDocID, voucherID);
			}
		}

		public DataSet GetLeaglActivityToPrint(string sysDocID, string voucherID)
		{
			return new LegalActivity(config).GetLeaglActivityToPrint(sysDocID, voucherID);
		}

		public LegalActivityData GetLegalActivityByID(string sysDocID, string voucherID, string AnalysisID)
		{
			using (LegalActivity legalActivity = new LegalActivity(config))
			{
				return legalActivity.GetLegalActivityByID(sysDocID, voucherID, AnalysisID);
			}
		}

		public LegalActivityData GetCustomerLegalActivityByID(string sysDocID, string voucherID)
		{
			using (LegalActivity legalActivity = new LegalActivity(config))
			{
				return legalActivity.GetCustomerActivityByID(sysDocID, voucherID);
			}
		}

		public DataSet GetLegalActivityList(DateTime from, DateTime to)
		{
			using (LegalActivity legalActivity = new LegalActivity(config))
			{
				return legalActivity.GetLegalActivityList(CRMRelatedTypes.LegalActivity, "", "", from, to);
			}
		}

		public DataSet GetCustomerLegalActivityList(DateTime from, DateTime to)
		{
			using (LegalActivity legalActivity = new LegalActivity(config))
			{
				return legalActivity.GetCustomerActivityList(CRMRelatedTypes.Customer, "", from, to);
			}
		}

		public DataSet GetLegalActivityListByLeadID(CRMRelatedTypes leadType, string SysDocID, string leadID, DateTime from, DateTime to)
		{
			using (LegalActivity legalActivity = new LegalActivity(config))
			{
				return legalActivity.GetLegalActivityListByLeadID(leadType, SysDocID, leadID, from, to);
			}
		}

		public DataSet GetLegalActivityComboList()
		{
			using (LegalActivity legalActivity = new LegalActivity(config))
			{
				return legalActivity.GetLegalActivityComboList();
			}
		}

		public DataSet GetPendingCasesReport(DateTime fromDate, DateTime ToDate, string FromCustomer, string ToCumstomer, string StatusID)
		{
			return new LegalActivity(config).GetPendingCasesReport(fromDate, ToDate, FromCustomer, ToCumstomer, StatusID);
		}

		public DataSet GetCaseHistoryReport(DateTime fromDate, DateTime ToDate, string FromCustomer, string ToCumstomer, string FromLawyer, string ToLawyer, string fileNumber, string sysDociD, string VoucherID)
		{
			return new LegalActivity(config).GetCaseHistoryReport(fromDate, ToDate, FromCustomer, ToCumstomer, FromLawyer, ToLawyer, fileNumber, sysDociD, VoucherID);
		}

		public DataSet GetCaseLawyerTrackReport(DateTime fromDate, DateTime ToDate, string FromLawyer, string ToLawyer)
		{
			return new LegalActivity(config).GetCaseLawyerTrackReport(fromDate, ToDate, FromLawyer, ToLawyer);
		}
	}
}
