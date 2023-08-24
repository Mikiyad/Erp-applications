using Micromind.ClientLibraries;
using Micromind.Common;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class AmountTextBox : MMTextBox
	{
		private Container components;

		private bool allowNegative;

		private string nullText = "0";

		private decimal maxValue = decimal.MaxValue;

		private decimal minValue = decimal.MinValue;

		private bool allowDecimal = true;

		[DefaultValue(false)]
		public bool AllowNegative
		{
			get
			{
				return allowNegative;
			}
			set
			{
				allowNegative = value;
			}
		}

		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				decimal result = default(decimal);
				if (value != "")
				{
					decimal.TryParse(value, NumberStyles.Any, null, out result);
					base.Text = result.ToString(Format.TotalAmountFormat);
				}
				else
				{
					base.Text = "";
				}
			}
		}

		public decimal MaxValue
		{
			get
			{
				return maxValue;
			}
			set
			{
				maxValue = value;
			}
		}

		public decimal MinValue
		{
			get
			{
				return minValue;
			}
			set
			{
				minValue = value;
			}
		}

		public string NullText
		{
			get
			{
				return nullText;
			}
			set
			{
				nullText = value;
			}
		}

		public decimal Value
		{
			get
			{
				decimal result = default(decimal);
				decimal.TryParse(Text, out result);
				return result;
			}
			set
			{
				Text = value.ToString(Format.TotalAmountFormat);
			}
		}

		public bool AllowDecimal
		{
			get
			{
				return allowDecimal;
			}
			set
			{
				allowDecimal = value;
			}
		}

		public AmountTextBox()
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
				d = decimal.Parse(Text, NumberStyles.Any);
			}
			catch
			{
				ErrorHelper.WarningMessage("Invalid amount.Please enter a numeric value.");
				e.Cancel = true;
				return;
			}
			try
			{
				if (!base.ReadOnly && d < 0m && !AllowNegative)
				{
					ErrorHelper.WarningMessage("Invalid amount.", "Please enter a positive number.");
					e.Cancel = true;
				}
				else if (d > new decimal(999999999999L))
				{
					ErrorHelper.WarningMessage("The number is larger than the maximum allowed.");
					e.Cancel = true;
				}
				else if (PublicFunctions.GetNumberOfDecimals(d.ToString()) > 2)
				{
					Text = Math.Round(decimal.Parse(Text, NumberStyles.Any), Global.CurDecimalPoints).ToString(Format.TotalAmountFormat);
				}
			}
			catch
			{
			}
		}

		private void AmountTextBox_Validated(object sender, EventArgs e)
		{
			try
			{
				Text = decimal.Parse(Text, NumberStyles.Any).ToString(Format.TotalAmountFormat);
			}
			catch
			{
			}
		}

		private void AmountTextBox_Enter(object sender, EventArgs e)
		{
			try
			{
				if (!base.ReadOnly)
				{
					Text = Validator.RemoveChar(Text, ',');
				}
			}
			catch
			{
			}
		}

		public void SetZero()
		{
			Text = 0.ToString(Format.TotalAmountFormat);
		}
	}
}
