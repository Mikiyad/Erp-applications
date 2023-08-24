using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IStoreSystem
	{
		int CreateStore(string storeName, string address, string city, string country, string postalCode, string phone, bool isShop, bool isWarehouse, string notes);

		int CreateStore(string storeName);

		int CreateStore(StoreData storeData);

		bool UpdateStore(StoreData storeData);

		bool CreateUpdateStoreBatch(DataSet listData, bool checkConcurrency);

		StoreData GetStoreByID(int storeID);

		StoreData GetAllWarehouses();

		StoreData GetAllShops();

		StoreData GetStores();

		bool DeleteStore(int storeID);

		DataSet GetStoresByFields(params string[] columns);

		DataSet GetStoresByFields(bool isInactive, params string[] columns);

		DataSet GetStoresByFields(int[] storesID, params string[] columns);

		bool ActivateStore(object storeID, bool activate);

		bool ExistStoreName(string storeName);

		string GetStoreName(int id);

		bool RenameStore(int storeID, string newName);

		bool ChangeStoreType(int storeID, StoreTypes storeType);
	}
}
