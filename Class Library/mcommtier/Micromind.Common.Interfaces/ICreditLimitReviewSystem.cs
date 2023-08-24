using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICreditLimitReviewSystem
	{
		bool CreateCreditLimitReview(CreditLimitReviewData CreditLimitReviewData, bool isUpdate);

		CreditLimitReviewData GetCreditLimitReview();

		CreditLimitReviewData GetList(DateTime from, DateTime to, bool showVoid);

		CreditLimitReviewData GetCustomerIndividualsByID(string id);

		DataSet GetList(string id);

		bool DeleteCreditLimitReview(string ID);

		CreditLimitReviewData GetCreditLimitReviewByID(string sysDocID, string id);

		CreditLimitReviewData GetCreditLimitReviewIndividuals(string Customerid);

		string GetNextDocumentNumber(string sysDocID);

		DataSet GetCreditLimitReviewByFields(params string[] columns);

		DataSet GetCreditLimitReviewByFields(string[] ids, params string[] columns);

		DataSet GetCreditLimitReviewByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCreditLimitReviewList();

		DataSet GetCreditLimitReviewComboList();
	}
}
