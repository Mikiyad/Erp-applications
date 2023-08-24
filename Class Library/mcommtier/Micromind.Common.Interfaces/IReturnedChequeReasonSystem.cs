using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IReturnedChequeReasonSystem
	{
		bool CreateReturnedChequeReason(ReturnedChequeReasonData returnedChequeReasonData);

		bool UpdateReturnedChequeReason(ReturnedChequeReasonData returnedChequeReasonData);

		ReturnedChequeReasonData GetReturnedChequeReason();

		bool DeleteReturnedChequeReason(string ID);

		ReturnedChequeReasonData GetReturnedChequeReasonByID(string id);

		DataSet GetReturnedChequeReasonByFields(params string[] columns);

		DataSet GetReturnedChequeReasonByFields(string[] ids, params string[] columns);

		DataSet GetReturnedChequeReasonByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetReturnedChequeReasonList();

		DataSet GetReturnedChequeReasonComboList();
	}
}
