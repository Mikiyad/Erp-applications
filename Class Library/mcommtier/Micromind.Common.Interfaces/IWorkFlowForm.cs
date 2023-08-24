namespace Micromind.Common.Interfaces
{
	public interface IWorkFlowForm
	{
		void SetApprovalStatus();

		void ShowForApproval(string sysDocID, string voucherID, int approvalTaskID);
	}
}
