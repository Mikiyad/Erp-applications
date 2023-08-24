using Micromind.ClientLibraries;
using Micromind.Common;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class QuantityTextBox : MMTextBox
	{
		private Container components;

		public QuantityTextBox()
		{
			InitializeComponent();
			base.TextAlign = HorizontalAlignment.Right;
			base.Validating += QuantityTextBox_Validating;
			base.Enter += QuantityTextBox_Enter;
			base.Validated += QuantityTextBox_Validated;
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

		private void QuantityTextBox_Validating(object sender, CancelEventArgs e)
		{
			decimal num = default(decimal);
			if (!(Text.Trim() == ""))
			{
				try
				{
					num = decimal.Parse(Text);
				}
				catch
				{
					ErrorHelper.WarningMessage("Invalid quantity.Please enter a numeric value.");
					e.Cancel = true;
					return;
				}
				if (num > 99999999m)
				{
					ErrorHelper.WarningMessage("Invalid quantity.", "Please enter a smaller amount.");
					e.Cancel = true;
				}
				else if (num < 0m && base.Enabled && !base.ReadOnly)
				{
					ErrorHelper.WarningMessage("Invalid quantity.", "Please enter a positive amount.");
					e.Cancel = true;
				}
				else
				{
					Text = Math.Round(decimal.Parse(Text), Format.QuantityNumberOfFixedDecimals).ToString(Format.QuantityFormat);
				}
			}
		}

		private void QuantityTextBox_Enter(object sender, EventArgs e)
		{
			try
			{
				Text = Validator.RemoveChar(Text, ',');
			}
			catch
			{
			}
		}

		private void QuantityTextBox_Validated(object sender, EventArgs e)
		{
			try
			{
				Text = decimal.Parse(Text).ToString(Format.QuantityFormat);
			}
			catch
			{
			}
		}
	}
}
