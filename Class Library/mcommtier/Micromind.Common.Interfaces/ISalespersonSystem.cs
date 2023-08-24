using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalespersonSystem
	{
		bool CreateSalesperson(SalespersonData salespersonData);

		bool UpdateSalesperson(SalespersonData salespersonData);

		SalespersonData GetSalesperson();

		bool DeleteSalesperson(string ID);

		SalespersonData GetSalespersonByID(string id);

		DataSet GetSalespersonByFields(params string[] columns);

		DataSet GetSalespersonByFields(string[] ids, params string[] columns);

		DataSet GetSalespersonByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetSalespersonList();

		DataSet GetSalespersonComboList();

		DataSet GetSalesBySalespersonByReportToReport(DateTime from, DateTime to, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry);

		DataSet GetSalesBySalespersonSummaryReport(DateTime from, DateTime to, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry);

		DataSet GetSalesBySalespersonDetailReport(DateTime from, DateTime to, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry);

		DataSet GetSalesPersonCommissionDetailReport(DateTime fromDate, DateTime toDate, string fromBrand, string toBrand, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, decimal commissionPercent);

		DataSet GetSalesPersonCommissionSummaryReport(DateTime fromDate, DateTime toDate, string fromBrand, string toBrand, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, decimal commissionPercent);

		DataSet GetSalesPersonCommissionItemIncludedReport(DateTime fromDate, DateTime toDate, string fromBrand, string toBrand, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, decimal commissionPercent);

		DataSet GetSalesByMainCategory(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs, string fromSalesPerson, string toSalesPerson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry);

		DataSet GetSalesByMainCategorySummary(DateTime from, DateTime to, string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs, string fromSalesPerson, string toSalesPerson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry);

		DataSet GetSalesComparisonDetailReport(DateTime from, DateTime to, string fromSalesPerson, string toSalesPerson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry);

		DataSet GetSalesComparisonSummaryReport(DateTime from, DateTime to, string fromSalesPerson, string toSalesPerson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry);
	}
}
