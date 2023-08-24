using System.ComponentModel;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class NonDirtyPanel : Panel
	{
		private IContainer components;

		public NonDirtyPanel()
		{
			InitializeComponent();
		}

		public NonDirtyPanel(IContainer container)
		{
			container.Add(this);
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
		}
	}
}
