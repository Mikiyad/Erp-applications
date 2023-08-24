using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ISecuritySystem
	{
		bool CreateSecurity(SecurityData securityData, string userID, string groupID);

		DataSet GetSecurityDataByID(string userID, bool isGroupLevel);

		SecurityData GetUserSecurityData(string userID);

		string GetUserEmployeeID(string userID);

		DataSet GetReportSecurityDataByID(string userID, bool isGroupLevel, bool IsFullView);
	}
}
