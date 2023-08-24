using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class PercentTextBox : MMTextBox
	{
		private Container components;

		public PercentTextBox()
		{
			InitializeComponent();
			base.TextAlign = HorizontalAlignment.Right;
			base.Validating += AmountTextBox_Validating;
			base.Validated += AmountTextBox_Validated;
			base.Enter += AmountTextBox_Enter;
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

		private void AmountTextBox_Validating(object sender, CancelEventArgs e)
		{
			if (Text.Trim() == "")
			{
				Clear();
				return;
			}
			decimal d;
			try
			{
				d = decimal.Parse(Text);
			}
			catch
			{
				ErrorHelper.WarningMessage("Invalid amount.Please enter a numeric value.");
				e.Cancel = true;
				return;
			}
			try
			{
				if (d < 0m)
				{
					ErrorHelper.WarningMessage("Invalid amount.", "Please enter a positive number.");
					e.Cancel = true;
				}
				else if (d > 100m)
				{
					ErrorHelper.WarningMessage("Please enter a numeric value between 0 and 100.");
					e.Cancel = true;
				}
				else if (PublicFunctions.GetNumberOfDecimals(d.ToString()) > 2)
				{
					Text = Math.Round(decimal.Parse(Text), 2).ToString();
				}
			}
			catch
			{
			}
		}

		private void AmountTextBox_Validated(object sender, EventArgs e)
		{
		}

		private void AmountTextBox_Enter(object sender, EventArgs e)
		{
		}
	}
}
