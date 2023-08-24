using Micromind.ClientLibraries;
using System;
using System.Data.SqlClient;

namespace Micromind.ClientUI.Configurations
{
	public sealed class CommonTasks
	{
		private CommonTasks()
		{
		}

		internal static void LoginDatabase(string databaseName, string companyName, string userID, bool autoConnect)
		{
			try
			{
				using (DatabaseLoginForm databaseLoginForm = new DatabaseLoginForm())
				{
					databaseLoginForm.ConnectAutomatically = autoConnect;
					databaseLoginForm.LoginDatabase(databaseName, companyName, userID, "");
					databaseLoginForm.ShowDialog();
					_ = 1;
				}
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}

		internal static void RestoreCompany()
		{
			try
			{
				using (RestoreDatabaseForm restoreDatabaseForm = new RestoreDatabaseForm())
				{
					restoreDatabaseForm.ShowDialog();
					if (!(restoreDatabaseForm.Database.DatabaseName == "") && restoreDatabaseForm.Database.ConnectDatabase)
					{
						LoginDatabase(restoreDatabaseForm.Database.DatabaseName, "", restoreDatabaseForm.Database.userName, autoConnect: false);
					}
				}
			}
			catch (SqlException ex)
			{
				ErrorHelper.ProcessError(ex);
			}
			catch (Exception e)
			{
				ErrorHelper.ProcessError(e);
			}
		}
	}
}
