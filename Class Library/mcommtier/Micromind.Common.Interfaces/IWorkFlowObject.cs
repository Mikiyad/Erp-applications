using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IWorkFlowObject
	{
		DataSet GetPendingApprovalList(string approvalID);
	}
}
