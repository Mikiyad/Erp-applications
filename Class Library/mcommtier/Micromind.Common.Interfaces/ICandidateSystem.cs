using Micromind.Common.Data;
using System;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICandidateSystem
	{
		bool CreateCandidate(CandidateData areaData);

		bool UpdateCandidate(CandidateData areaData);

		CandidateData GetCandidate();

		bool DeleteCandidate(string ID);

		CandidateData GetCandidateByID(string id);

		bool UpdateCandidateSalaryDetails(DataSet data);

		DataSet GetCandidateSalaryDetailsByCandidateID(string candidateID);

		DataSet GetCandidateByFields(params string[] columns);

		DataSet GetCandidateByFields(string[] ids, params string[] columns);

		DataSet GetCandidateByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetCandidateList();

		DataSet GetAppointmentList();

		DataSet GetCandidateComboList();

		DataSet GetCandidateBriefInfo(string candidateID);

		DataSet GetCandidateBalanceSummary(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showZeroBalance);

		DataSet GetCandidateBalanceDetailReport(DateTime from, DateTime to, string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showZeroBalance);

		DataSet GetCandidateListReport(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive);

		DataSet GetCandidateProfileReport(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive);

		DataSet GetCandidateAppointmentDetails(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive);

		DataSet GetCandidateActivityReport(string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, bool showInactive);

		DataSet GetCandidateLeaveReport(DateTime from, DateTime to, string fromCandidate, string toCandidate, string fromDepartment, string toDepartment, string fromLocation, string toLocation, string fromLeave, string toLeave, LeaveApprovalType approvalType);

		bool AddCandidatePhoto(string candidateID, byte[] image);

		bool RemoveCandidatePhoto(string candidateID);

		byte[] GetCandidateThumbnailImage(string candidateID);

		DataSet GetAgentComboList();

		string GetNextSequenceNumber(string tableName, string fieldName);

		bool CancelCandidate(CandidateData data);

		bool IsEmployee(string employeeNo);
	}
}
