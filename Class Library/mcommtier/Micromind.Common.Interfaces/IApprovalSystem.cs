using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IApprovalSystem
	{
		bool CreateApproval(ApprovalData approvalData);

		bool UpdateApproval(ApprovalData approvalData);

		ApprovalData GetApproval();

		bool DeleteApproval(ApprovalTypes approvalType, string approvalID);

		ApprovalData GetApprovalByID(ApprovalTypes approvalType, string id);

		DataSet GetApprovalByFields(params string[] columns);

		DataSet GetApprovalByFields(string[] ids, params string[] columns);

		DataSet GetApprovalByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetApprovalList();

		DataSet GetVerificationList();

		DataSet GetApprovalComboList(ApprovalTypes approvalType);

		bool ApproveTask(int taskID, string tableName, string idColumnName);

		bool ApproveTaskVerification(int taskID, string tableName, string idColumnName);

		bool RejectTask(int taskID, string tableName, string idColumnName);

		DataSet GetUserApprovalsWithPendingTasks(ApprovalTypes approvalType);

		DataSet GetUserPendingApprovalTasks(ApprovalTypes approvalType, string approvalID);

		byte GetApprovalTaskStatusByID(int taskID);

		DataSet GetTransactionApprovalDetail(SysDocTypes sysDocType, string sysDocID, string voucherID);

		DataSet GetCardApprovalDetail(DataComboType cardType, string cardID);

		DoubleString GetTableName(int objectType, int objectID);

		int GetUserPendingTasksCount();
	}
}
