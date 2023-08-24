using WIA;

namespace Micromind.ClientUI
{
	public class Scanner
	{
		private Device oDevice;

		private Item oItem;

		private CommonDialogClass dlg;

		public Scanner()
		{
			dlg = new CommonDialogClass();
			oDevice = dlg.ShowSelectDevice(WiaDeviceType.ScannerDeviceType, AlwaysSelectDevice: true);
		}

		public void Scann()
		{
			dlg.ShowAcquisitionWizard(oDevice);
		}
	}
}
