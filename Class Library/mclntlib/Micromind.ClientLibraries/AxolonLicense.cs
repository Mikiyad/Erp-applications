using LicenseManager;

namespace Micromind.ClientLibraries
{
	public static class AxolonLicense
	{
		private static LicenseManagerControl licenseManager = new LicenseManagerControl();

		private static LicenseManagerControl moduleLicenseManager = new LicenseManagerControl();

		public static LicenseManagerControl LicenseManager
		{
			get
			{
				licenseManager.License.VersionMajor = Global.MajorVersion;
				return licenseManager;
			}
		}

		public static LicenseManagerControl ModuleLicenseManager => moduleLicenseManager;
	}
}
