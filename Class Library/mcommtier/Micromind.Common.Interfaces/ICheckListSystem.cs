using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICheckListSystem
	{
		bool CreateCheckList(CheckListData checkListData);

		bool UpdateCheckList(CheckListData checkListData);

		CheckListData GetCheckList();

		bool DeleteCheckList(CheckListTypes checkListType, string checkListID);

		CheckListData GetCheckListByID(CheckListTypes checkListType, string id);

		DataSet GetCheckListByFields(params string[] columns);

		DataSet GetCheckListByFields(string[] ids, params string[] columns);

		DataSet GetCheckListByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCheckListList();

		DataSet GetCheckListComboList(CheckListTypes checkListType);

		bool ApproveTask(int taskID, string tableName, string idColumnName);

		bool RejectTask(int taskID, string tableName, string idColumnName);

		DataSet GetUserCheckListsWithPendingTasks(CheckListTypes checkListType);

		DataSet GetUserPendingCheckListTasks(CheckListTypes checkListType, string checkListID);

		byte GetCheckListTaskStatusByID(int taskID);

		DataSet GetTransactionCheckListDetail(SysDocTypes sysDocType, string sysDocID, string voucherID);

		DataSet GetCardCheckListDetail(DataComboType cardType, string cardID);

		DoubleString GetTableName(int objectType, int objectID);

		bool CloseTask(string taskID, string remarks);
	}
}
