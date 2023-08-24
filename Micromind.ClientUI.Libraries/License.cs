using LicenseManager;
using Micromind.ClientLibraries;

namespace Micromind.ClientUI.Libraries
{
	public static class License
	{
		private static LicenseManagerControl licenseManager = new LicenseManagerControl();

		public static LicenseManagerControl LicenseManager
		{
			get
			{
				licenseManager.License.VersionMajor = Global.MajorVersion;
				return licenseManager;
			}
		}
	}
}
