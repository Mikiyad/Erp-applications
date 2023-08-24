using DevExpress.UserSkins;
using Micromind.ClientLibraries;
using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace Micromind.ClientUI.WindowsForms
{
	public class StartupClass
	{
		[STAThread]
		public static void Main()
		{
			BonusSkins.Register();
			try
			{
				CultureInfo currentCulture = Thread.CurrentThread.CurrentCulture;
				Thread.CurrentThread.CurrentCulture = currentCulture;
				Thread.CurrentThread.CurrentUICulture = currentCulture;
			}
			catch
			{
				GlobalRules.CultureName = string.Empty;
			}
			try
			{
				Thread.CurrentThread.TrySetApartmentState(ApartmentState.STA);
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(defaultValue: false);
			}
			catch (ExternalException ex)
			{
				ErrorHelper.ProcessError(ex, "This is an external error from Windows.", "Please send us the error message for further investigation.");
				Application.Exit();
			}
			catch (ApplicationException e)
			{
				ErrorHelper.ProcessError(e, "Please send us the error message for further investigation.");
				Application.Exit();
			}
			catch (Exception e2)
			{
				ErrorHelper.ProcessError(e2, "Please send us the error message for further investigation.");
				Application.Exit();
			}
			try
			{
				if (Global.GetApplicationType() == ApplicationTypes.POSClient)
				{
					Application.Run(new formPOSMain());
				}
				else
				{
					Application.Run(new formMain());
				}
			}
			catch (Exception e3)
			{
				ErrorHelper.ProcessError(e3);
			}
		}
	}
}
