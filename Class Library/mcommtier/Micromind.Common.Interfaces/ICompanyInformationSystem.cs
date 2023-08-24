using Micromind.Common.Data;
using Micromind.Common.Libraries;
using System;
using System.Data;
using System.IO;

namespace Micromind.Common.Interfaces
{
	public interface ICompanyInformationSystem
	{
		string GetCompanyName();

		byte GetFiscalStartMonth();

		DateTime GetFiscalStartDate();

		CompanyInformationData GetCompanyInformation();

		bool SaveLogo(int id, string imageName, Stream stream);

		Stream GetLogo(int id);

		bool UseLogo(int id);

		bool UpdateClosingBookDate(DateTime closingDate, string closingPassword);

		DateTime GetClosingBookDate();

		string GetClosingBookPassword();

		bool UpdateCompanyInformation(CompanyInformationData data);

		bool UpdateCompanyOptions(CompanyInformationData data);

		bool UpdateEmailConfig(CompanyInformationData data);

		CompanyInformationData GetEmailConfig(CompanyEmailConfigTypes type);

		DataSet GetCompanyPreferences();

		bool CanChangeBaseCurrency();

		bool AddLogo(byte[] image);

		bool RemoveLogo();

		byte[] GetLogoThumbnailImage();

		ApplicationUpdateConfig GetCurrentAxolonServerVersion();

		string GetClientUpdatePath();

		bool ClosePeriod(DateTime date, DateTime dateInventory, string remarks);

		DataSet GetLastClosingPeriod();

		bool UnlockPeriod(int id);

		DataSet GetInstalledModules();

		bool AddModule(string moduleKey);

		bool DeleteModule(string moduleKey);

		bool ConfigureSchedulerAgent(string userID, string password, decimal intervalMinutes, decimal emailInterval, DateTime maintenanceTime, bool isActive);

		DataSet GetSchedulerAgentInfo();

		bool ExecuteScheduledJobs();

		bool ExecuteSystemMaintenanceJobs();

		DataSet GetFormMenuSubstitutes();

		DateTime GetInitialFiscalYearDate();

		DateTime GetLastFiscalYearDate();
	}
}
