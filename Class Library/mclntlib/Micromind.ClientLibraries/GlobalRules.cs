using Micromind.Common.Data;
using Micromind.Securities;
using System;
using System.Collections;
using System.Data;

using System.Windows.Forms;

namespace Micromind.ClientLibraries
{
	
	
	public sealed class GlobalRules
	{
		private static int daysAllowed;

		private static UILanguages uiLanguage;

		private static ConnectionTypes connectionType;

		private static bool isLicenseExpired;

		private static Licenses licenses;

		public static ArrayList extendedModules;

		private static string cultureName;

		private static RulesReader rulesReader;

		public static ConnectionTypes ConnectionType
		{
			get
			{
				return connectionType;
			}
			set
			{
				connectionType = value;
			}
		}

		public static string DefaultServerInstanceName => "Micromind";

		public static bool IsTrial => AxolonLicense.LicenseManager.License.IsTrial;

		public static bool IsMultiUser => Global.IsMultiUser;

		public static bool IsBeta
		{
			get
			{
				if (AxolonLicense.LicenseManager.License.UserData == "1")
				{
					return true;
				}
				return false;
			}
		}

		public static int Edition => AxolonLicense.LicenseManager.License.EditionID;

		public static bool IsMultiLingual => true;

		public static UILanguages UILanguage
		{
			get
			{
				return uiLanguage;
			}
			set
			{
				uiLanguage = value;
			}
		}

		public static string CultureName
		{
			get
			{
				return cultureName;
			}
			set
			{
				cultureName = value;
			}
		}

		public static string LanguageName
		{
			get
			{
				switch (uiLanguage)
				{
				case UILanguages.English:
					return "English";
				case UILanguages.Farsi:
					return "Farsi";
				default:
					return "English";
				}
			}
		}

		public static RightToLeft LocalizedRightToLeft
		{
			get
			{
				switch (uiLanguage)
				{
				case UILanguages.English:
					return RightToLeft.No;
				case UILanguages.Farsi:
					return RightToLeft.No;
				default:
					return RightToLeft.No;
				}
			}
		}

		public static string EditionText
		{
			get
			{
				if (Edition == Editions.Enterprise)
				{
					return "Enterprise";
				}
				if (Edition == Editions.Professional)
				{
					return "Professional";
				}
				if (Edition == Editions.Basic)
				{
					return "Basic";
				}
				if (Edition == Editions.Standard)
				{
					return "Standard";
				}
				if (Edition == Editions.Premium)
				{
					return "Premier";
				}
				throw new ApplicationException("This edition is not specified.");
			}
		}

		static GlobalRules()
		{
			daysAllowed = 31;
			uiLanguage = UILanguages.English;
			connectionType = ConnectionTypes.LocalServer;
			isLicenseExpired = false;
			licenses = new Licenses();
			extendedModules = null;
			cultureName = string.Empty;
			rulesReader = new RulesReader();
		}

		private static void LoadExtendedModules()
		{
			try
			{
				DataSet installedModules = Factory.CompanyInformationSystem.GetInstalledModules();
				extendedModules = new ArrayList();
				foreach (DataRow row in installedModules.Tables[0].Rows)
				{
					string key = row["ModuleKey"].ToString();
					AxolonLicense.ModuleLicenseManager.SetKey(key);
					int productID = AxolonLicense.ModuleLicenseManager.License.ProductID;
					if (productID >= 11)
					{
						_ = 40;
					}
					extendedModules.Add(productID);
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		public static bool IsModuleAvailable(AxolonModules module)
		{
			if (extendedModules == null)
			{
				LoadExtendedModules();
			}
			if (extendedModules == null || extendedModules.Count == 0)
			{
				return false;
			}
			if (extendedModules.Contains((int)module))
			{
				return true;
			}
			return false;
		}

		internal static bool IsFeatureAllowedByEdition(EditionFeatures feature)
		{
			if (Edition == Editions.Enterprise)
			{
				return true;
			}
			if (Edition == Editions.Premium)
			{
				return true;
			}
			if (Edition == Editions.Professional)
			{
				return true;
			}
			if (Edition == Editions.Standard)
			{
				switch (feature)
				{
				case EditionFeatures.MultiCurrency:
				case EditionFeatures.LandedCost:
				case EditionFeatures.Assembly:
				case EditionFeatures.Kit:
				case EditionFeatures.JobCosting:
				case EditionFeatures.MultiUnit:
				case EditionFeatures.AdvancedSecurity:
				case EditionFeatures.LetterOfCredits:
				case EditionFeatures.AccountAnalysis:
				case EditionFeatures.ImportDocuments:
				case EditionFeatures.HRPayroll:
				case EditionFeatures.Consignment:
				case EditionFeatures.BasicManufacturing:
				case EditionFeatures.AdvancedManufacturing:
				case EditionFeatures.MatrixInventory:
				case EditionFeatures.FixedAsset:
				case EditionFeatures.PivotReport:
				case EditionFeatures.LotTracking:
				case EditionFeatures.Warehouse3PL:
					return false;
				default:
					return true;
				}
			}
			if (Edition == Editions.Basic)
			{
				switch (feature)
				{
				case EditionFeatures.MultiCurrency:
				case EditionFeatures.JobCosting:
				case EditionFeatures.AdvancedSecurity:
				case EditionFeatures.MultiLocation:
				case EditionFeatures.LetterOfCredits:
				case EditionFeatures.AccountAnalysis:
				case EditionFeatures.ImportDocuments:
				case EditionFeatures.HRPayroll:
				case EditionFeatures.Consignment:
				case EditionFeatures.BasicManufacturing:
				case EditionFeatures.AdvancedManufacturing:
				case EditionFeatures.MatrixInventory:
				case EditionFeatures.FixedAsset:
				case EditionFeatures.PivotReport:
				case EditionFeatures.LotTracking:
				case EditionFeatures.Warehouse3PL:
					return false;
				default:
					return true;
				}
			}
			return false;
		}

		public static bool IsFeatureAllowed(EditionFeatures feature)
		{
			return IsFeatureAllowedByEdition(feature);
		}

		public static bool IsCorrectServerName(string serverName)
		{
			return true;
		}
	}
}
