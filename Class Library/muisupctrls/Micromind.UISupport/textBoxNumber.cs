using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class textBoxNumber : MMTextBox
	{
		private Container components;

		public textBoxNumber()
		{
			InitializeComponent();
			base.TextAlign = HorizontalAlignment.Right;
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
			base.Validating += new System.ComponentModel.CancelEventHandler(textBoxNumber_Validating);
			base.Validated += new System.EventHandler(textBoxNumber_Validated);
			base.Enter += new System.EventHandler(textBoxNumber_Enter);
		}

		private void textBoxNumber_Load(object sender, EventArgs e)
		{
		}

		private void textBoxNumber_Enter(object sender, EventArgs e)
		{
			try
			{
				Text = Format.RemoveChar(Text, ',');
			}
			catch
			{
			}
		}

		private void textBoxNumber_Validated(object sender, EventArgs e)
		{
			try
			{
				Text = decimal.Parse(Text).ToString(Format.QuantityFormat);
			}
			catch
			{
			}
		}

		private void textBoxNumber_Validating(object sender, CancelEventArgs e)
		{
			if (!(Text.Trim() == ""))
			{
				try
				{
					decimal.Parse(Text);
				}
				catch
				{
					MessageBox.Show(this, "Invalid number.Please enter a valid number.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					e.Cancel = true;
				}
			}
		}
	}
}
