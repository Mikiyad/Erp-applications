using DevExpress.XtraEditors;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class ScanTextBox : TextEdit
	{
		private IContainer components;

		public event EventHandler EnterPressed;

		public ScanTextBox()
		{
			InitializeComponent();
			Init();
		}

		public ScanTextBox(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			Init();
		}

		private void Init()
		{
			base.Properties.NullValuePrompt = "Scan an item here";
			base.KeyPress += ScanTextBox_KeyPress;
		}

		private void ScanTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				this.EnterPressed(sender, e);
			}
		}

		public void Clear()
		{
			Text = "";
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
