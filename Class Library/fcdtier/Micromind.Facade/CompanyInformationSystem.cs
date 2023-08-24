using Micromind.Common.Data;
using Micromind.Common.Interfaces;
using Micromind.Common.Libraries;
using Micromind.Data;
using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Micromind.Facade
{
	public sealed class CompanyInformationSystem : MarshalByRefObject, ICompanyInformationSystem, IDisposable
	{
		private Config config;

		public string LogoPicDirectory
		{
			get
			{
				string result = "Logo Images";
				using (Settings settings = new Settings(config))
				{
					try
					{
						object data = settings.GetData("E4C9AB0B1E954fe8A4839E8540290204I", "E4C9AB0B1E954fe8A4839E8540290205N", "E4C9AB0B1E954fe8A4839E8540290203K", "LogoPicDirectory");
						if (data == null)
						{
							return result;
						}
						if (data.ToString().Length <= 0)
						{
							return result;
						}
						result = data.ToString();
						return result;
					}
					catch
					{
						return result;
					}
				}
			}
			set
			{
				using (Settings settings = new Settings(config))
				{
					try
					{
						settings.SaveSetting("E4C9AB0B1E954fe8A4839E8540290204I", "E4C9AB0B1E954fe8A4839E8540290205N", "E4C9AB0B1E954fe8A4839E8540290203K", "LogoPicDirectory", value);
					}
					catch
					{
						throw;
					}
				}
			}
		}

		private string LogoDirectory
		{
			get
			{
				string result = "Logo Images";
				using (Settings settings = new Settings(config))
				{
					try
					{
						object data = settings.GetData("E4C9AB0B1E954fe8A4839E8540290204I", "E4C9AB0B1E954fe8A4839E8540290205N", "E4C9AB0B1E954fe8A4839E8540290203K", "LogoPicDirectory");
						if (data == null)
						{
							return result;
						}
						if (data.ToString().Length <= 0)
						{
							return result;
						}
						result = data.ToString();
						return result;
					}
					catch
					{
						return result;
					}
				}
			}
		}

		public CompanyInformationSystem(Config config)
		{
			this.config = config;
		}

		public void Dispose()
		{
		}

		public string GetCompanyName()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetCompanyName();
			}
		}

		public byte GetFiscalStartMonth()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetFiscalStartMonth();
			}
		}

		public DateTime GetFiscalStartDate()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetFiscalStartDate();
			}
		}

		public CompanyInformationData GetCompanyInformation()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetCompanyInformation();
			}
		}

		public CompanyInformationData GetEmailConfig(CompanyEmailConfigTypes type)
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetEmailConfig(type);
			}
		}

		public bool SaveLogo(int id, string imageName, Stream stream)
		{
			return true;
		}

		public Stream GetLogo(int id)
		{
			Stream result = null;
			try
			{
				string text = "";
				if (text.Length > 0)
				{
					try
					{
						Image image = Image.FromFile(text);
						result = new MemoryStream();
						image.Save(result, ImageFormat.Bmp);
						return result;
					}
					catch
					{
						return null;
					}
				}
				return result;
			}
			catch
			{
				return null;
			}
		}

		private bool IsImageFileExist(string imageName)
		{
			string text = "";
			try
			{
				text = Process.GetCurrentProcess().MainModule.FileName;
				int length = text.LastIndexOf("\\");
				text = text.Substring(0, length) + "\\" + LogoDirectory;
				text = text + "\\" + imageName;
			}
			catch
			{
				throw;
			}
			return File.Exists(text);
		}

		private string GetUniqueImageName(string imageName)
		{
			int num = 1;
			string text = imageName;
			char newChar = ' ';
			char[] invalidPathChars = Path.GetInvalidPathChars();
			foreach (char oldChar in invalidPathChars)
			{
				text = text.Replace(oldChar, newChar);
			}
			checked
			{
				while (IsImageFileExist(text))
				{
					num++;
					text = ((imageName.IndexOf(".") < 0) ? (imageName + num.ToString()) : (imageName.Substring(0, imageName.IndexOf(".")) + num.ToString() + "." + imageName.Substring(imageName.IndexOf(".") + 1, imageName.Length - imageName.IndexOf(".") - 1)));
				}
				text = Path.GetFileNameWithoutExtension(text);
				return text + ".jpg";
			}
		}

		public bool UseLogo(int id)
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.UseLogo(id);
			}
		}

		public bool UpdateClosingBookDate(DateTime closingDate, string closingPassword)
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.UpdateClosingBookDate(closingDate, closingPassword);
			}
		}

		public DateTime GetClosingBookDate()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetClosingBookDate();
			}
		}

		public string GetClosingBookPassword()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetClosingBookPassword();
			}
		}

		public bool UpdateCompanyInformation(CompanyInformationData data)
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.UpdateCompanyInformation(data);
			}
		}

		public bool UpdateEmailConfig(CompanyInformationData data)
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.UpdateEmailConfig(data);
			}
		}

		public bool UpdateCompanyOptions(CompanyInformationData data)
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.UpdateCompanyOptions(data);
			}
		}

		public DataSet GetCompanyPreferences()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetCompanyPreferences();
			}
		}

		public bool CanChangeBaseCurrency()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.CanChangeBaseCurrency();
			}
		}

		public bool AddLogo(byte[] image)
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.AddLogo(image);
			}
		}

		public bool RemoveLogo()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.RemoveLogo();
			}
		}

		public byte[] GetLogoThumbnailImage()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetLogoThumbnailImage();
			}
		}

		public ApplicationUpdateConfig GetCurrentAxolonServerVersion()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetCurrentAxolonServerVersion();
			}
		}

		public string GetClientUpdatePath()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetClientUpdatePath();
			}
		}

		public bool ClosePeriod(DateTime date, DateTime dateInventory, string remarks)
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.ClosePeriod(date, dateInventory, remarks);
			}
		}

		public DataSet GetLastClosingPeriod()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetLastClosingPeriod();
			}
		}

		public bool UnlockPeriod(int id)
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.UnlockPeriod(id);
			}
		}

		public DataSet GetInstalledModules()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetInstalledModules();
			}
		}

		public bool AddModule(string moduleKey)
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.AddModule(moduleKey);
			}
		}

		public bool DeleteModule(string moduleKey)
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.DeleteModule(moduleKey);
			}
		}

		public bool ConfigureSchedulerAgent(string userID, string password, decimal intervalMinutes, decimal emailInterval, DateTime maintenanceTime, bool isActive)
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.ConfigureSchedulerAgent(userID, password, intervalMinutes, emailInterval, maintenanceTime, isActive);
			}
		}

		public DataSet GetSchedulerAgentInfo()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetSchedulerAgentInfo();
			}
		}

		public bool ExecuteScheduledJobs()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.ExecuteScheduledJobs();
			}
		}

		public bool ExecuteSystemMaintenanceJobs()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.ExecuteSystemMaintenanceJobs();
			}
		}

		public DataSet GetFormMenuSubstitutes()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetFormMenuSubstitutes();
			}
		}

		public DateTime GetInitialFiscalYearDate()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetInitialFiscalYearDate();
			}
		}

		public DateTime GetLastFiscalYearDate()
		{
			using (CompanyInformations companyInformations = new CompanyInformations(config))
			{
				return companyInformations.GetLastFiscalYearDate();
			}
		}
	}
}
