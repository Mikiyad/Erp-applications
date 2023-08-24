using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IStoreProductSystem
	{
		bool CreateStoreProduct(int storeID, int productID, float unitsInStock, float reorderLevel, out StoreProductData storeProductData);

		bool CreateStoreProduct(StoreProductData storeProductData);

		bool UpdateStoreProduct(StoreProductData storeProductData);

		bool Transfer(StoreProductTransferData storeProductTransferData);

		bool Adjust(StoreProductData storeProductData);

		bool Adjust(StoreProductData storeProductData, string closingPassword);

		DataSet GetProducts();

		DataSet GetProducts(params int[] productsID);

		DataSet GetProductsByStoreByFields(int storeID, params string[] columns);

		DataSet GetProductsByStore(int storeID, bool combinedUnitsInStock);

		DataSet GetUnitsInStockByStore(int storeID, bool combinedUnitsInStock);

		float GetUnitsInStockByStore(int storeID, int productID, bool combinedUnitsInStock);

		DataSet GetNonStoreProducts(int storeID);

		float GetCombinedOnHandProducts(int productID);

		DataSet GetCombinedOnHandProducts();

		DataSet GetStoreTransferTransaction(int transferID);

		DataSet GetInventoryTransaction(int inventoryTransactionID);

		StoreProductData GetLastInventoryTransaction(int storeID, int productID, DateTime transactionDate);

		DataSet GetInventoryStockStatus(int storeID);

		DataSet GetInventoryStockStatus();

		DataSet GetItemsInStock(int storeID, int[] items);

		DataSet GetInventoryStockStatus(int[] itemID, int storeID, DateTime date);

		DataSet GetInventoryStockStatus(int[] itemsID, int[] storesID, DateTime date);

		DataSet GetInventoryStockStatusByContainer(int[] itemsID, int[] containersID, DateTime date);

		bool UpdateAdjustedStoreProduct(int inventoryTransactionID, float newQuantity, decimal adjustmentCost, DateTime adjustmentDate, string updateDescription);

		int GetGLIDByInventoryTransactionID(int inventoryTransactionID);

		bool AdjustNewProduct(object storeID, int productID, float newQuantity, decimal cost, DateTime date, string description);

		bool AdjustNewProduct(object storeID, int productID, float newQuantity, decimal cost, DateTime date, string description, string closingPassword);

		int GetNewProductTransactionID(int productID);

		DataSet GetInventoryStockStatus(int[] storesID, bool isInactive, DateTime date);

		DataSet GetInventoryStockStatus(bool isInactive, int[] itemsID, int[] storesID);

		DataSet GetInventoryStockStatus(bool isInactive, int[] itemsID, int[] storesID, params string[] columns);

		string GetAutoTransferNumber();
	}
}
