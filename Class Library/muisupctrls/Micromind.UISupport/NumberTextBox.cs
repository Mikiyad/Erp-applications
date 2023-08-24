using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class NumberTextBox : MMTextBox, ICustomReportControl
	{
		private string nullText = "0";

		private decimal maxValue = decimal.MaxValue;

		private decimal minValue = decimal.MinValue;

		private bool allowDecimal = true;

		private Container components;

		private string crFieldName = "";

		private string crKey = "";

		private byte crValueType = 1;

		public new string CustomReportFieldName
		{
			get
			{
				return crFieldName;
			}
			set
			{
				crFieldName = value;
			}
		}

		public new string CustomReportKey
		{
			get
			{
				return crKey;
			}
			set
			{
				crKey = value;
			}
		}

		public new byte CustomReportValueType
		{
			get
			{
				return crValueType;
			}
			set
			{
				crValueType = value;
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

		public NumberTextBox()
		{
			InitializeComponent();
			base.KeyDown += NumberTextBox_KeyDown;
		}

		public new string GetParameterValue()
		{
			if (crValueType == 1)
			{
				return Text;
			}
			if (crFieldName == "")
			{
				return "''=''";
			}
			return crFieldName + " = " + Text;
		}

		private void NumberTextBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (!allowDecimal && e.KeyCode == Keys.OemPeriod)
			{
				e.SuppressKeyPress = true;
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
				if (!base.ReadOnly)
				{
					Text = Format.RemoveChar(Text, ',');
				}
			}
			catch
			{
			}
		}

		private void textBoxNumber_Validated(object sender, EventArgs e)
		{
			try
			{
				if (allowDecimal)
				{
					Text = decimal.Parse(Text, NumberStyles.Any).ToString(Format.QuantityFormat);
				}
				else
				{
					Text = decimal.Parse(Text, NumberStyles.Any).ToString(Format.NumberFormat);
				}
			}
			catch
			{
			}
		}

		private void textBoxNumber_Validating(object sender, CancelEventArgs e)
		{
			decimal num = default(decimal);
			if (Text.Trim() == "")
			{
				Text = nullText;
			}
			else
			{
				try
				{
					num = decimal.Parse(Text, NumberStyles.Any);
					if (num > maxValue || num < minValue)
					{
						ErrorHelper.InformationMessage("Invalid value.", "Please enter a numeric value between following:", minValue.ToString() + " - " + maxValue.ToString());
						e.Cancel = true;
					}
					else if (!allowDecimal)
					{
						Text = Math.Round(num).ToString();
					}
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
