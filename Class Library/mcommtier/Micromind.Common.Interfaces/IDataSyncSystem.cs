using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IDataSyncSystem
	{
		bool CreateDataSync(DataSyncData data, bool isUpdate);

		DataSyncData GetDataSync();

		DataSyncData GetDataSyncByID(string code);

		bool DeleteDataSync(string ID);

		DataSet GetDataSyncByFields(params string[] columns);

		DataSet GetDataSyncByFields(string[] ids, params string[] columns);

		DataSet GetDataSyncByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetDataSyncList();

		DataSet GetDataSyncComboList();

		DataSet GetDataSyncBySales(string docType, string code);

		DataSet GetDataSyncByCollections(string paymentType, string code);

		DataSet GetDataSyncByStockTransfer(string transferType, string code);

		DataSet GetDataSyncCustomers(string code);

		DataSet GetDataSyncItems(string code);

		bool UpdateLastSyncTime(string recType, string code);

		DataSet GetConnected(string code);

		DataSet GetDataSyncNewCustomers(string code);

		DataSet GetSyncList();
	}
}
