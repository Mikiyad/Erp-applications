using System.ComponentModel;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class MTextBox : TextBox
	{
		private Container components;

		public MTextBox()
		{
			try
			{
				InitializeComponent();
			}
			catch
			{
			}
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing && components != null)
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private void InitializeComponent()
		{
			components = new System.ComponentModel.Container();
		}
	}
}
