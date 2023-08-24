using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IProductPriceBulkUpdateSystem
	{
		bool CreateProductPriceBulkUpdate(ProductPriceBulkUpdateData inventoryAdjustmentData, bool isUpdate);

		ProductPriceBulkUpdateData GetProductPriceBulkUpdateByID(string sysDocID, string voucherID);

		bool DeleteProductPriceBulkUpdate(string sysDocID, string voucherID);

		bool VoidProductPriceBulkUpdate(string sysDocID, string voucherID, bool isVoid);

		DataSet GetProductPriceBulkUpdateToPrint(string sysDocID, string voucherID, bool mergeMatrixItems);

		DataSet GetProductPriceBulkUpdateToPrint(string sysDocID, string[] voucherID, bool mergeMatrixItems);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetOpenQuotesSummary(string vendorID, bool isImport);

		DataSet GetPurchaseComparisonReport(string refNumber);

		DataSet GettProductPriceBulkUpdateList();
	}
}
