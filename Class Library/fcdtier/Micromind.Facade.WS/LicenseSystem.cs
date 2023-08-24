using Micromind.Common.Data.WS;
using Micromind.Common.Interfaces.WS;
using Micromind.Data;
using System;

namespace Micromind.Facade.WS
{
	public class LicenseSystem : MarshalByRefObject, ILicenseSystem
	{
		private Config config;

		public LicenseSystem(Config config)
		{
			this.config = config;
		}

		public bool CereateLicense(LicenseData licenseData)
		{
			using (Licenses licenses = new Licenses(config))
			{
				return licenses.InsertLicense(licenseData);
			}
		}

		public LicenseData GetLicenses()
		{
			using (Licenses licenses = new Licenses(config))
			{
				return licenses.GetLicenses();
			}
		}

		public LicenseData GetLicenseByID(int licenseID)
		{
			using (Licenses licenses = new Licenses(config))
			{
				return licenses.GetLicenseByID(licenseID);
			}
		}

		public bool ExistLicenseByMachineID(string licenseCode)
		{
			using (Licenses licenses = new Licenses(config))
			{
				return licenses.ExistLicenseByMachineID(licenseCode);
			}
		}

		public string GetLicenseIDByMachineID(string licenseCode)
		{
			using (Licenses licenses = new Licenses(config))
			{
				return licenses.GetLicenseIDByMachineID(licenseCode);
			}
		}

		public string GetLicenseMachineIDByID(int id)
		{
			using (Licenses licenses = new Licenses(config))
			{
				return licenses.GetLicenseMachineIDByID(id);
			}
		}

		public int GetNumberOfUsesByKey(string key)
		{
			using (Licenses licenses = new Licenses(config))
			{
				return licenses.GetNumberOfUsesByKey(key);
			}
		}
	}
}
