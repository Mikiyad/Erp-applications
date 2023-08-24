using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalespersonSystem : MarshalByRefObject, ISalespersonSystem, IDisposable
	{
		private Config config;

		public SalespersonSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalesperson(SalespersonData data)
		{
			return new Salesperson(config).InsertSalesperson(data);
		}

		public bool UpdateSalesperson(SalespersonData data)
		{
			return UpdateSalesperson(data, checkConcurrency: false);
		}

		public bool UpdateSalesperson(SalespersonData data, bool checkConcurrency)
		{
			return new Salesperson(config).UpdateSalesperson(data);
		}

		public SalespersonData GetSalesperson()
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalesperson();
			}
		}

		public bool DeleteSalesperson(string groupID)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.DeleteSalesperson(groupID);
			}
		}

		public SalespersonData GetSalespersonByID(string id)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalespersonByID(id);
			}
		}

		public DataSet GetSalespersonByFields(params string[] columns)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalespersonByFields(columns);
			}
		}

		public DataSet GetSalespersonByFields(string[] ids, params string[] columns)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalespersonByFields(ids, columns);
			}
		}

		public DataSet GetSalespersonByFields(string[] ids, bool isInactive, params string[] columns)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalespersonByFields(ids, isInactive, columns);
			}
		}

		public DataSet GetSalespersonList()
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalespersonList();
			}
		}

		public DataSet GetSalespersonComboList()
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalespersonComboList();
			}
		}

		public DataSet GetSalesBySalespersonByReportToReport(DateTime from, DateTime to, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalesBySalespersonByReportToReport(from, to, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry);
			}
		}

		public DataSet GetSalesBySalespersonSummaryReport(DateTime from, DateTime to, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalesBySalespersonSummaryReport(from, to, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry);
			}
		}

		public DataSet GetSalesBySalespersonDetailReport(DateTime from, DateTime to, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalesBySalespersonDetailReport(from, to, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry);
			}
		}

		public DataSet GetSalesPersonCommissionDetailReport(DateTime fromDate, DateTime toDate, string fromBrand, string toBrand, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, decimal commissionPercent)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalesPersonCommissionDetailReport(fromDate, toDate, fromBrand, toBrand, fromCategory, toCategory, fromLocation, toLocation, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, commissionPercent);
			}
		}

		public DataSet GetSalesPersonCommissionSummaryReport(DateTime fromDate, DateTime toDate, string fromBrand, string toBrand, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, decimal commissionPercent)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalesPersonCommissionSummaryReport(fromDate, toDate, fromBrand, toBrand, fromCategory, toCategory, fromLocation, toLocation, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, commissionPercent);
			}
		}

		public DataSet GetSalesPersonCommissionItemIncludedReport(DateTime fromDate, DateTime toDate, string fromBrand, string toBrand, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, decimal commissionPercent)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalesPersonCommissionItemIncludedReport(fromDate, toDate, fromBrand, toBrand, fromCategory, toCategory, fromLocation, toLocation, fromSalesperson, toSalesperson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry, commissionPercent);
			}
		}

		public DataSet GetSalesByMainCategory(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs, string fromSalesPerson, string toSalesPerson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalesByMainCategory(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, customerIDs, fromSalesPerson, toSalesPerson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry);
			}
		}

		public DataSet GetSalesByMainCategorySummary(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs, string fromSalesPerson, string toSalesPerson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalesByMainCategorySummary(from, to, fromCustomer, toCustomer, fromClass, toClass, fromGroup, toGroup, fromArea, toArea, fromCountry, toCountry, customerIDs, fromSalesPerson, toSalesPerson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry);
			}
		}

		public DataSet GetSalesComparisonDetailReport(DateTime from, DateTime to, string fromSalesPerson, string toSalesPerson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalesComparisonDetailReport(from, to, fromSalesPerson, toSalesPerson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry);
			}
		}

		public DataSet GetSalesComparisonSummaryReport(DateTime from, DateTime to, string fromSalesPerson, string toSalesPerson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry)
		{
			using (Salesperson salesperson = new Salesperson(config))
			{
				return salesperson.GetSalesComparisonSummaryReport(from, to, fromSalesPerson, toSalesPerson, fromSalespersonDivision, toSalespersonDivision, fromSalespersonGroup, toSalespersonGroup, fromSalespersonArea, toSalespersonArea, fromSalespersonCountry, toSalespersonCountry);
			}
		}
	}
}
