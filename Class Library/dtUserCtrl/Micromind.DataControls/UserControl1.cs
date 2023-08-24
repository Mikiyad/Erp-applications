using Micromind.Common.Data;
using System.ComponentModel;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class UserControl1 : UserControl
	{
		private UDFTypes type;

		private IContainer components;

		public UserControl1()
		{
			InitializeComponent();
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
			base.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
		}
	}
}
