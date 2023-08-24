using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Collections.Generic;
using System.Data;

namespace Micromind.Facade
{
	public sealed class SalesForecastingSystem : MarshalByRefObject, ISalesForecastingSystem, IDisposable
	{
		private Config config;

		public SalesForecastingSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateSalesForecasting(SalesForecastingData data, bool isUpdate)
		{
			return new SalesForecasting(config).InsertUpdateSalesForecasting(data, isUpdate);
		}

		public SalesForecastingData GetSalesForecastingByID(string sysDocID, string voucherID)
		{
			return new SalesForecasting(config).GetSalesForecastingByID(sysDocID, voucherID);
		}

		public bool DeleteSalesForecasting(string sysDocID, string voucherID)
		{
			return new SalesForecasting(config).DeleteSalesForecasting(sysDocID, voucherID);
		}

		public bool VoidSalesForecasting(string sysDocID, string voucherID, bool isVoid)
		{
			return new SalesForecasting(config).VoidSalesForecasting(sysDocID, voucherID, isVoid);
		}

		public DataSet GetOpenOrdersSummary(string customerID, bool isExport)
		{
			return new SalesForecasting(config).GetOpenOrdersSummary(customerID, isExport);
		}

		public DataSet GetOpenOrderListReport()
		{
			return new SalesForecasting(config).GetOpenOrderListReport();
		}

		public DataSet GetSalesForecastingToPrint(string sysDocID, string voucherID)
		{
			return new SalesForecasting(config).GetSalesForecastingToPrint(sysDocID, voucherID);
		}

		public DataSet GetSalesForecastingToPrint(string sysDocID, string[] voucherID)
		{
			return new SalesForecasting(config).GetSalesForecastingToPrint(sysDocID, voucherID);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new SalesForecasting(config).GetList(from, to, showVoid);
		}

		public DataSet GetPRoductLocationData(string productIDs)
		{
			return new SalesForecasting(config).GetPRoductLocationData(productIDs);
		}

		public DataSet GetProductLocWiseData(DateTime from, DateTime to, List<Tuple<string, decimal>> list, string LocIDs)
		{
			return new SalesForecasting(config).GetProductLocWiseData(from, to, list, LocIDs);
		}

		public DataSet GetProductForecastData(DateTime from, DateTime to, List<Tuple<string, decimal>> list, string LocIDs, int mnths)
		{
			return new SalesForecasting(config).GetProductForecastData(from, to, list, LocIDs, mnths);
		}

		public bool OrderHasShippedQuantity(string sysDocID, string voucherNumber)
		{
			return new SalesForecasting(config).OrderHasShippedQuantity(sysDocID, voucherNumber, null);
		}
	}
}
