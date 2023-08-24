using Micromind.Common.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISalesForecastingSystem
	{
		bool CreateSalesForecasting(SalesForecastingData inventoryAdjustmentData, bool isUpdate);

		SalesForecastingData GetSalesForecastingByID(string sysDocID, string voucherID);

		bool DeleteSalesForecasting(string sysDocID, string voucherID);

		bool VoidSalesForecasting(string sysDocID, string voucherID, bool isVoid);

		DataSet GetOpenOrdersSummary(string customerID, bool isExport);

		DataSet GetOpenOrderListReport();

		DataSet GetSalesForecastingToPrint(string sysDocID, string voucherID);

		DataSet GetSalesForecastingToPrint(string sysDocID, string[] voucherID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		bool OrderHasShippedQuantity(string sysDocID, string voucherNumber);

		DataSet GetPRoductLocationData(string productIDs);

		DataSet GetProductLocWiseData(DateTime from, DateTime to, List<Tuple<string, decimal>> list, string LocIDs);

		DataSet GetProductForecastData(DateTime from, DateTime to, List<Tuple<string, decimal>> list, string LocIDs, int mnths);
	}
}
