using Micromind.Common.Data;
using System;
using System.Data;
using System.Data.SqlClient;

namespace Micromind.Common.Interfaces
{
	public interface IProductSystem
	{
		string ItemPicDirectory
		{
			get;
			set;
		}

		bool UpdateProduct(ProductData productData);

		bool CreateProduct(ProductData productData);

		ProductData GetProductByID(string productID);

		bool DeleteProduct(string productID);

		byte[] GetItemThumbnailImage(int id, int width, int height);

		DataSet GetProductUnitComboList();

		byte[] GetProductComboList();

		float GetProductQuantity(string productID, string locationID);

		DataSet GetProductQuantityAndCost(string productID, string locationID);

		DataSet GetProductAvailability(string productID, string unitID);

		DataSet GetProductList(bool inActive, bool showZeroBalance, string locationID);

		bool AddProductPhoto(string productID, byte[] image);

		bool RemoveProductPhoto(string productID);

		bool GetProductTransactionsExists(string productID);

		byte[] GetProductThumbnailImage(string productID);

		DataSet GetPriceLevelComboList();

		DataSet GetPriceListNames();

		DataSet GetProductPriceList(string productID, string customerID, string locationID = "", string UnitID = "");

		DataSet GetAllProductPriceList();

		decimal GetProductSalesPrice(string productID, string customerID, string locationID = "", string UnitID = "");

		DataSet GetProductSalesPriceDesc(string productID, string customerID, string unitID);

		decimal GetProductPurchasePrice(string productID);

		DataSet GetInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, bool showitemwithTansactions, bool showinactiveitems);

		DataSet GetInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromJob, string toJob, string fromCostCategory, string toCostCategory, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromJob, string toJob, bool showitemwithTansactions, bool showinactiveitems, bool excludeInventoryTransfer, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetInventoryTransactionLotwiseReport(DateTime from, DateTime to, DateTime fromDate, DateTime toDate, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, bool? production, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetInventoryTransactionLotwiseReport(DateTime from, DateTime to, DateTime fromDate, DateTime toDate, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromJob, string toJob, bool? production);

		bool IsSufficientQuantityOnhand(string productID, string unitID, string locationID, string detailsTableName, string sysDocID, string voucherID, decimal quantity);

		DataSet GetSalesByItemSummaryReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetSalesByItemDetailReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetSalesByItemCategorySummaryReport(DateTime from, DateTime to, string fromCategory, string toCategory);

		DataSet GetProductListReport(string fromProduct, string toProduct, string fromClass, string toClass, string fromCategory, string toCategory, bool showZero, bool showInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetPurchaseByItemSummaryReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetPurchaseByItemDetailReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetPurchaseByItemCategorySummaryReport(DateTime from, DateTime to, string fromCategory, string toCategory);

		DataSet GetProductPriceListReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetProductSinglePriceListReport(string fromProduct, string toProduct, string fromClass, string toClass, string fromCategory, string toCategory, string unitPriceName, bool showInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetProductStockListItemWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetInventoryAgingSummaryReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, string fromBrand, string toBrand, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetMatrixProductStockListReport(string fromItem, string toItem, string fromCategory, string toCategory, bool showZero, bool showImage, bool isInactive);

		DataSet GetProductStockListLocationWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetProductCatalogReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool isInactive, bool showZeroQuantity, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetProductListPOS();

		DataSet GetProductListPOS(string locationID);

		DataSet GetTopProducts(DateTime from, DateTime to, int count);

		DataSet POSGetProductData(string code);

		DataSet GetInventoryLedgerList(string productID, DateTime from, DateTime to, bool excludeInventoryTransfer);

		DataSet GetProductsToReorder();

		DataSet GetProductQuantityAndCostAsOfDate(string productID, string locationID, DateTime date);

		DataSet GetProductQuantityAndCostAsOfDate(string[] productID, string locationID, DateTime date);

		bool UpdateInvoicesWhichRequireCOGSUpdate(string batchRef, DateTime from, DateTime to);

		DataSet GetProductAvailableLotsAndBins(string productID, string locationID, string sysDocID, string voucherID, string vendorID);

		DataSet GetSOProductAvailableLotsAndBins(string productID, string locationID, string sysDocID, string voucherID, string vendorID);

		DataSet GetProductReturnableLotsAndBins(string productID, string locationID, string sysDocID, string voucherID, string customerID, string returnSourceSysDocID, string returnSourceVoucherID);

		bool DocumentHasUsedLots(string sysDocID, string voucherID);

		DataSet GetSalesByItemCustomerSalespersonReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetSalesByItemCustomerSalespersonReport(string group1, string group2, string fields, string joinQuery, DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs);

		DataSet GetSalesByItemCustomerSalespersonReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromSalesperson, string toSalesperson, string fromLocation, string toLocation, string customerIDs, string strGroupBY);

		DataSet GetPendingDNsReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetPickListReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetProductLotWiseAvailability(string productID);

		bool UpdatePreviousTransactionsCOGS(DateTime fromDate, string fromItem, string endItem);

		DataSet GetProductLot(string lotID);

		DataSet GetSalesByItemClassCategorySummaryReport(DateTime from, DateTime to, string fromCategory, string toCategory, string fromClass, string toClass);

		DataSet GetSalesByProductBrandSummaryReport(DateTime from, DateTime to, string fromBrand, string toBrand);

		DataSet GetProductStockListCategoryWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive);

		DataSet GetProductStockListClassWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromBrand, string toBrand, string fromLocation, string toLocation, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive);

		bool AllocateItemsToLot(string[] productIDs);

		decimal GetJobBOMProductPurchasePrice(string productID);

		decimal GetJobBOMLabourCost(string productID);

		DataSet GetSalesManDueReport(DateTime fromDate, DateTime toDate, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string customerIDs);

		decimal GetLastSaleTransationByCustomerID(string productID, string customerID);

		DataSet GetSaleProductByID(string productID, string customerID);

		bool GetItemExistsinCategory(string productID, string customerID);

		DataSet GetsufficientQuantityforPackage(string productID, string customerID);

		byte[] GetItemFeatures(string productID);

		DataSet GetPackageID(string productID);

		DataSet GetW3PLInventoryTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showitemwithTansactions, bool showinactiveitems, string fromCustomer, string toCustomer, string fromCusClass, string toCusClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs);

		DataSet GetW3PLProductStockListLocationWiseReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive, string fromCustomer, string toCustomer, string fromCusClass, string toCusClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs);

		DataSet GetW3PLInventoryAgingSummaryReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, DateTime asOfDate, bool showZero, bool isInactive, string fromCustomer, string toCustomer, string fromCusClass, string toCusClass, string fromGroup, string toGroup, string fromArea, string toArea, string fromCountry, string toCountry, string customerIDs);

		DataSet GetPurchaseByItemVendorBuyerReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string vendorIDs);

		DataSet GetPurchaseByInventoryItemVendorBuyerReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string vendorIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetPurchaseSubContractByItemVendorBuyerReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromBuyer, string toBuyer, string fromLocation, string toLocation, string fromJob, string toJob);

		DataSet GetProjectSubContractOrderReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string fromBuyer, string toBuyer, string fromJob, string toJob);

		bool SetFlag(string productID, byte flagID);

		DataSet GetProductComboRowByID(string idField);

		DataSet GetProducts();

		DataSet GetProductsForCombo();

		DataSet GetProductsForItemTransaction();

		bool IsHoldSaleonProduct(string productID);

		DataSet GetProductSalesPurchasePriceList(string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory);

		DataSet GetProductPartsDetail(string ProductID);

		byte[] GetProductPartsList();

		DataSet GetProductUnitDetails(string productID, string unitID, string LocationID = "");

		DataSet GetSalesProfitabilityReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetSalesProfitabilityReportSummary(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetSalesProfitabilityItemWiseReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromCustomer, string toCustomer, string fromCustomerClass, string toCustomerClass, string fromCustomerGroup, string toCustomerGroup, string fromCustomerArea, string toCustomerArea, string fromCustomerCountry, string toCustomerCountry, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry, string fromLocation, string toLocation, string customerIDs, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin);

		DataSet GetTaxDetailsReport(DateTime fromDate, DateTime toDate);

		DataSet GetItemVendorList(string fromItem, string toItem, string fromItemClass, string toItemClass, string fromItemCategory, string toItemCategory, string fromBrand, string toBrand, string fromManufacturer, string toManufacturer, string fromOrigin, string toOrigin, string fromStyle, string toStyle, string fromVendor, string toVendor, string fromVendorClass, string toVendorClass, string fromVendorGroup, string toVendorGroup, string vendorIDs);

		DataSet GetMonthlySalesPivotReport(string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOfDate, DateTime asOfDate, bool showZero, bool isInactive);

		DataSet GetSalesPurchaseAnalysisPivotReport(DateTime fromDate, DateTime toDate, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin, string fromBrand, string toBrand);

		string GenerateProductID(ProductData pdata);

		DataSet GetAvailbleProductBin(bool IsBinOnly, string binID);

		DataSet GetProductsByCatgeory(string categoryID);

		DataSet GetMonthlySalesPivotReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOf, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry);

		DataSet GetMonthlySalesPivotReportByCatgory(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOf, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry);

		DataSet GetMonthlySalesPivotReportMore(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, string fromLocation, string toLocation, bool isAsOf, DateTime asOfDate, bool showZero, bool isInactive, string fromManufacturer, string toManufacturer, string fromStyle, string toStyle, string fromOrigin, string toOrigin, string fromSalesperson, string toSalesperson, string fromSalespersonDivision, string toSalespersonDivision, string fromSalespersonGroup, string toSalespersonGroup, string fromSalespersonArea, string toSalespersonArea, string fromSalespersonCountry, string toSalespersonCountry);

		decimal GetProductLastCost(string productID, DateTime date);

		DataSet GetProductLotDetails(string product, string location, string lotIDFrom, string lotIDTo);

		bool UpdateLotDetails(DataSet currentData, SqlTransaction sqlTransaction);

		bool UpdateLotReceivingDetails(DataSet currentData, SqlTransaction sqlTransaction);

		decimal GetProductCostwithMultiUnit(string productID, string unitID, string locationID);

		bool IsProductExist(string productID);

		bool IsExistProductTransaction(string productID);

		DataSet GetProductUnits(string productID);
	}
}
