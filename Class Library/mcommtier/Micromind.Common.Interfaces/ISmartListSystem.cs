using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISmartListSystem
	{
		bool CreateSmartList(SmartListData smartListData);

		bool UpdateSmartList(SmartListData smartListData);

		SmartListData GetSmartList();

		bool DeleteSmartList(string ID);

		bool CreateCategory(string categoryName, int parentID);

		int CreateCategory(string categoryName, string parentID);

		DataSet GetCategoryList();

		DataSet GetCategoryListByID(int Id);

		SmartListData GetSmartListByID(string id);

		DataSet GetSmartListByFields(params string[] columns);

		DataSet GetSmartListByFields(string[] ids, params string[] columns);

		DataSet GetSmartListByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetSmartListList();

		DataSet GetSmartListComboList();

		DataSet GetSubSmartListComboList();

		DataSet GetReportByQuery(string query, DateTime fromDate, DateTime toDate);

		bool UpdateCategory(string categoryName, int parentID, int categoryID);

		bool UpdateCategory(string categoryName, int parentID, int categoryID, int rowIndex);

		bool RenameReport(string reportName, int reportID);

		bool DeleteCategory(string categoryID);

		byte[] GetSmartListData(string reportID, DateTime fromDate, DateTime toDate, string[] parameters, string[] parameterValues);

		DataSet GetSmartListCategoryComboList();

		bool UpdateSmartListIndex(string categoryID, int index);
	}
}
