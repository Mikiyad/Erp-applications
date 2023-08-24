using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ICLTokenSystem
	{
		bool CreateCLToken(CLTokenData clTokenData);

		bool UpdateCLToken(CLTokenData clTokenData);

		CLTokenData GetCLToken();

		bool DeleteCLToken(string ID);

		CLTokenData GetCLTokenByID(string id);

		bool RequestCLToken(CLTokenData clTokenData);

		DataSet GetCLTokenList();

		DataSet GetCLTokenComboList();
	}
}
