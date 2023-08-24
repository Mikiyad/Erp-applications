using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class CreditLimitReviewSystem : MarshalByRefObject, ICreditLimitReviewSystem, IDisposable
	{
		private Config config;

		public CreditLimitReviewSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateCreditLimitReview(CreditLimitReviewData data, bool isUpdate)
		{
			return new CreditLimitReview(config).InsertUpdateCreditLimitReview(data, isUpdate);
		}

		public CreditLimitReviewData GetCreditLimitReview()
		{
			using (CreditLimitReview creditLimitReview = new CreditLimitReview(config))
			{
				return creditLimitReview.GetCreditLimitReview();
			}
		}

		public bool DeleteCreditLimitReview(string ID)
		{
			using (CreditLimitReview creditLimitReview = new CreditLimitReview(config))
			{
				return creditLimitReview.DeleteCreditLimitReview(ID);
			}
		}

		public CreditLimitReviewData GetCreditLimitReviewByID(string sysDocID, string id)
		{
			using (CreditLimitReview creditLimitReview = new CreditLimitReview(config))
			{
				return creditLimitReview.GetCreditLimitReviewByID(sysDocID, id);
			}
		}

		public CreditLimitReviewData GetCreditLimitReviewIndividuals(string id)
		{
			using (CreditLimitReview creditLimitReview = new CreditLimitReview(config))
			{
				return creditLimitReview.GetCreditLimitReviewIndividuals(id);
			}
		}

		public CreditLimitReviewData GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new CreditLimitReview(config).GetList(from, to, showVoid);
		}

		public DataSet GetList(string customerID)
		{
			return new CreditLimitReview(config).GetList(customerID);
		}

		public DataSet GetCreditLimitReviewByFields(params string[] columns)
		{
			using (CreditLimitReview creditLimitReview = new CreditLimitReview(config))
			{
				return creditLimitReview.GetCreditLimitreviewByFields(columns);
			}
		}

		public DataSet GetCreditLimitReviewByFields(string[] ids, params string[] columns)
		{
			using (CreditLimitReview creditLimitReview = new CreditLimitReview(config))
			{
				return creditLimitReview.GetCreditLimitreviewByFields(ids, columns);
			}
		}

		public DataSet GetCreditLimitReviewByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (CreditLimitReview creditLimitReview = new CreditLimitReview(config))
			{
				return creditLimitReview.GetCreditLimitreviewByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetCreditLimitReviewList()
		{
			using (CreditLimitReview creditLimitReview = new CreditLimitReview(config))
			{
				return creditLimitReview.GetCreditLimitreviewList();
			}
		}

		public DataSet GetCreditLimitReviewComboList()
		{
			using (CreditLimitReview creditLimitReview = new CreditLimitReview(config))
			{
				return creditLimitReview.GetCreditLimitreviewComboList();
			}
		}

		public string GetNextDocumentNumber(string sysDocID)
		{
			using (CreditLimitReview creditLimitReview = new CreditLimitReview(config))
			{
				return creditLimitReview.GetNextDocumentNumber(sysDocID);
			}
		}

		public CreditLimitReviewData GetCustomerIndividualsByID(string id)
		{
			using (CreditLimitReview creditLimitReview = new CreditLimitReview(config))
			{
				return creditLimitReview.GetCustomerIndividualsByID(id);
			}
		}
	}
}
