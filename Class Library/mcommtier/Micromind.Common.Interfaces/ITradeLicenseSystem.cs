using Micromind.Common.Data;
using System.Data;

namespace Micromind.Common.Interfaces
{
	public interface ITradeLicenseSystem
	{
		bool CreateTradeLicense(TradeLicenseData tradeLicenseData);

		bool UpdateTradeLicense(TradeLicenseData tradeLicenseData);

		TradeLicenseData GetTradeLicense();

		bool DeleteTradeLicense(string ID);

		TradeLicenseData GetTradeLicenseByID(string id);

		DataSet GetTradeLicenseByFields(params string[] columns);

		DataSet GetTradeLicenseByFields(string[] ids, params string[] columns);

		DataSet GetTradeLicenseByFields(string[] ids, bool isInactive, params string[] columns);

		DataSet GetTradeLicenseList();

		DataSet GetTradeLicenseComboList();
	}
}
