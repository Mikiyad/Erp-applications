using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Data;
using System;
using System.Data;

namespace Micromind.Facade
{
	public sealed class ProductPriceBulkUpdateSystem : MarshalByRefObject, IProductPriceBulkUpdateSystem, IDisposable
	{
		private Config config;

		public ProductPriceBulkUpdateSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public bool CreateProductPriceBulkUpdate(ProductPriceBulkUpdateData data, bool isUpdate)
		{
			return new ProductPriceBulkUpdate(config).InsertUpdateProductPriceBulkUpdate(data, isUpdate);
		}

		public ProductPriceBulkUpdateData GetProductPriceBulkUpdateByID(string sysDocID, string voucherID)
		{
			return new ProductPriceBulkUpdate(config).GetProductPriceBulkUpdateByID(sysDocID, voucherID);
		}

		public bool DeleteProductPriceBulkUpdate(string sysDocID, string voucherID)
		{
			return new ProductPriceBulkUpdate(config).DeleteProductPriceBulkUpdate(sysDocID, voucherID);
		}

		public bool VoidProductPriceBulkUpdate(string sysDocID, string voucherID, bool isVoid)
		{
			return new ProductPriceBulkUpdate(config).VoidProductPriceBulkUpdate(sysDocID, voucherID, isVoid);
		}

		public DataSet GetProductPriceBulkUpdateToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems)
		{
			return new ProductPriceBulkUpdate(config).GetProductPriceBulkUpdateToPrint(sysDocID, voucherID, mergeMatrixItems);
		}

		public DataSet GetProductPriceBulkUpdateToPrint(string sysDocID, string voucherID, bool mergeMatrixItems)
		{
			return new ProductPriceBulkUpdate(config).GetProductPriceBulkUpdateToPrint(sysDocID, new string[1]
			{
				voucherID
			}, mergeMatrixItems);
		}

		public DataSet GetList(DateTime from, DateTime to, bool showVoid)
		{
			return new ProductPriceBulkUpdate(config).GetList(from, to, showVoid);
		}

		public DataSet GetOpenQuotesSummary(string vendorID, bool isImport)
		{
			return new ProductPriceBulkUpdate(config).GetOpenQuotesSummary(vendorID, isImport);
		}

		public DataSet GetPurchaseComparisonReport(string refNumber)
		{
			return new ProductPriceBulkUpdate(config).GetPurchaseComparisonReport(refNumber);
		}

		public DataSet GettProductPriceBulkUpdateList()
		{
			return new ProductPriceBulkUpdate(config).GettProductPriceBulkUpdateList();
		}
	}
}
