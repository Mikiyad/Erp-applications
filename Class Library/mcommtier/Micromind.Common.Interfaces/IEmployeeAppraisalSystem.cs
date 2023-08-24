using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface IEmployeeAppraisalSystem
	{
		bool CreatePosition(EmployeeAppraisalData positionData);

		bool UpdatePosition(EmployeeAppraisalData positionData);

		EmployeeAppraisalData GetPosition();

		bool DeletePosition(string ID, string SysDocID);

		EmployeeAppraisalData GetPositionByID(string sysDocID, string id);

		DataSet GetPositionByFields(params string[] columns);

		DataSet GetPositionByFields(string[] ids, params string[] columns);

		DataSet GetPositionByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetPositionList();

		DataSet GetPositionComboList();

		DataSet GetEmployeeKPIList(string EmployeeID);

		DataSet GetList(DateTime from, DateTime to, bool showVoid);

		DataSet GetEmployeeAppraisalToPrint(string sysDocID, string voucherID);
	}
}
