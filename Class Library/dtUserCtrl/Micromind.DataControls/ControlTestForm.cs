using System.ComponentModel;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ControlTestForm : Form
	{
		private IContainer components;

		public ControlTestForm()
		{
			InitializeComponent();
		}

		public static void Main()
		{
			Application.Run(new ControlTestForm());
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
			Text = "ControlTestForm";
		}
	}
}
