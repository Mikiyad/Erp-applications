using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IFixedAssetSystem
	{
		bool CreateAsset(FixedAssetData assetData);

		bool UpdateAsset(FixedAssetData assetData);

		FixedAssetData GetAsset();

		bool DeleteAsset(string ID);

		FixedAssetData GetAssetByID(string id);

		FixedAssetData GetAssetByClassID(string id);

		DataSet GetAssetByFields(params string[] columns);

		DataSet GetAssetByFields(string[] ids, params string[] columns);

		DataSet GetAssetByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetAssetList();

		DataSet GetAssetComboList();

		bool InsertFixedAssetDepSchedule(string assetID);

		DataSet GetAssetDepSchedule(string assetID);

		DataSet CalculateDepreciation(int year, int month, string assetID);

		DataSet GetAssetListReport(string fromCustomer, string toCustomer, string fromClass, string toClass, string fromGroup, string toGroup, string fromArea, string toArea, string FromDivision, string ToDivision, string FromDepartment, string ToDepartment, string FromCountry, string ToCountry, string FromCompany, string ToCompany, bool showInactive);

		DataSet GetAssetPurchaseDetailReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory);

		DataSet GetAssetSaleDetailReport(DateTime from, DateTime to, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory);

		DataSet GetAssetTransactionReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showitemwithTansactions, bool showinactiveitems);

		DataSet GetAssetDepreciationReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showitemwithTansactions, bool showinactiveitems);

		DataSet GetAssetTransferReport(DateTime from, DateTime to, string fromWarehouse, string toWarehouse, string fromItem, string toItem, string fromClass, string toClass, string fromCategory, string toCategory, bool showitemwithTansactions, bool showinactiveitems);

		DataSet GetAssetList(string fromAsset, string toAsset, string fromClass, string toClass, string fromGroup, string toGroup, string fromLocation, string toLocation);
	}
}
