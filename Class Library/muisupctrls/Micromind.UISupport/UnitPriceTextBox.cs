using Micromind.ClientLibraries;
using Micromind.Common;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class UnitPriceTextBox : MMTextBox
	{
		private Container components;

		public UnitPriceTextBox()
		{
			InitializeComponent();
			base.TextAlign = HorizontalAlignment.Right;
			base.Validating += UnitPriceTextBox_Validating;
			base.Validated += UnitPriceTextBox_Validated;
			base.Enter += UnitPriceTextBox_Enter;
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

		private void UnitPriceTextBox_Validating(object sender, CancelEventArgs e)
		{
			decimal num = default(decimal);
			if (Text.Trim() == "")
			{
				return;
			}
			try
			{
				num = decimal.Parse(Text);
			}
			catch
			{
				ErrorHelper.WarningMessage("Invalid amount.", "Please enter a positive numeric value.");
				e.Cancel = true;
				return;
			}
			if (num > new decimal(999999999999L))
			{
				ErrorHelper.WarningMessage("Invalid amount.", "Please enter a smaller amount.");
				e.Cancel = true;
				return;
			}
			if (PublicFunctions.GetNumberOfDecimals(Text) > 5)
			{
				Text = Math.Round(decimal.Parse(Text), 5).ToString(Format.UnitPriceFormat);
			}
			if (num > decimal.MaxValue)
			{
				ErrorHelper.WarningMessage("Amount is larger than the maximum allowed.");
				e.Cancel = true;
			}
			else if (num < 0m)
			{
				ErrorHelper.WarningMessage("Invalid value.", "Please enter a positive amount.");
				e.Cancel = true;
			}
		}

		private void UnitPriceTextBox_Validated(object sender, EventArgs e)
		{
			try
			{
				Text = decimal.Parse(Text).ToString(Format.UnitPriceFormat);
			}
			catch
			{
			}
		}

		private void UnitPriceTextBox_Enter(object sender, EventArgs e)
		{
			try
			{
				Text = Validator.RemoveChar(Text, ',');
			}
			catch
			{
			}
		}
	}
}
