using Micromind.Common.Data.WS;

namespace Micromind.Common.Interfaces.WS
{
	public interface ILicenseSystem
	{
		bool CereateLicense(LicenseData licenseData);

		LicenseData GetLicenses();

		LicenseData GetLicenseByID(int licenseID);

		bool ExistLicenseByMachineID(string licenseCode);

		string GetLicenseIDByMachineID(string licenseCode);

		string GetLicenseMachineIDByID(int id);

		int GetNumberOfUsesByKey(string key);
	}
}
