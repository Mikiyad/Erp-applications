using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class FiscalYearSystem : MarshalByRefObject, IFiscalYearSystem, IDisposable
	{
		private Config config;

		public FiscalYearSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateFiscalYear(FiscalYearData data)
		{
			return new FiscalYear(config).InsertFiscalYear(data);
		}

		public bool UpdateFiscalYear(FiscalYearData data)
		{
			return UpdateFiscalYear(data, checkConcurrency: false);
		}

		public bool UpdateFiscalYear(FiscalYearData data, bool checkConcurrency)
		{
			return new FiscalYear(config).UpdateFiscalYear(data);
		}

		public bool CreateSummaryTable(string sysDocID, string voucherID, string fiscalYearID)
		{
			return new FiscalYear(config).CreateSummaryTable(sysDocID, voucherID, fiscalYearID);
		}

		public FiscalYearData GetFiscalYear()
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.GetFiscalYear();
			}
		}

		public bool DeleteFiscalYear(string groupID)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.DeleteFiscalYear(groupID);
			}
		}

		public FiscalYearData GetFiscalYearByID(string id)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.GetFiscalYearByID(id);
			}
		}

		public DataSet GetFiscalYearByFields(params string[] columns)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.GetFiscalYearByFields(columns);
			}
		}

		public DataSet GetFiscalYearByFields(string[] ids, params string[] columns)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.GetFiscalYearByFields(ids, columns);
			}
		}

		public DataSet GetFiscalYearByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.GetFiscalYearByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetFiscalYearList()
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.GetFiscalYearList();
			}
		}

		public DataSet GetFiscalYearComboList()
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.GetFiscalYearComboList();
			}
		}

		public int CanCloseFiscalYear(string fiscalYearID)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.CanCloseFiscalYear(fiscalYearID);
			}
		}

		public bool CloseFiscalYear(string fiscalYearID, GLData closingData, string retainedEarningAccountID)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.CloseFiscalYear(fiscalYearID, closingData, retainedEarningAccountID);
			}
		}

		public bool ReopenFiscalYear(string fiscalYearID)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.ReopenFiscalYear(fiscalYearID);
			}
		}

		public int GetPendingDeliveryNotesCount(DateTime toDate)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.GetPendingDeliveryNotesCount(toDate);
			}
		}

		public int GetPendingGRNCount(DateTime toDate)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.GetPendingGRNCount(toDate);
			}
		}

		public int GetPendingTransferCount(DateTime toDate)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.GetPendingTransferCount(toDate);
			}
		}

		public int GetNegativeStockCount(DateTime toDate)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.GetNegativeStockCount(toDate);
			}
		}

		public bool IsTBCorrect(DateTime toDate)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.IsTBCorrect(toDate);
			}
		}

		public int GetUnbalancedJournalsCount(DateTime toDate)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.GetUnbalancedJournalsCount(toDate);
			}
		}

		public int GetUncostedItemsCount(DateTime toDate)
		{
			using (FiscalYear fiscalYear = new FiscalYear(config))
			{
				return fiscalYear.GetUncostedItemsCount(toDate);
			}
		}
	}
}
