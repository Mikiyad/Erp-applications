using System;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Drawing;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class flatDatePicker : DateTimePicker
	{
		private bool isRequired;

		private IContainer components;

		[Description("Determines if the filed is required.")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		[DefaultValue(false)]
		[Browsable(true)]
		public bool IsRequired
		{
			get
			{
				return isRequired;
			}
			set
			{
				isRequired = value;
			}
		}

		public static DateTime LongDBDateTimeMin => SqlDateTime.MinValue.Value;

		public static DateTime LongDBDateTimeMax => SqlDateTime.MaxValue.Value;

		public flatDatePicker()
		{
			InitializeComponent();
			base.Value = DateTime.Now;
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
			SuspendLayout();
			base.Size = new System.Drawing.Size(232, 20);
			base.Validating += new System.ComponentModel.CancelEventHandler(flatDatePicker_Validating);
			ResumeLayout(false);
		}

		private void flatDatePicker_Validating(object sender, CancelEventArgs e)
		{
			if (!(Text.Trim() == string.Empty))
			{
				try
				{
					DateTime t = DateTime.Parse(Text);
					if (t > LongDBDateTimeMax)
					{
						MessageBox.Show("'" + Text + "' is not a valid date.Please enter a valid date.\nDate must be between '" + LongDBDateTimeMin.ToShortDateString() + "' and '" + LongDBDateTimeMax.ToShortDateString() + "'", "Date", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						e.Cancel = true;
						Focus();
					}
					if (t < LongDBDateTimeMin)
					{
						MessageBox.Show("'" + Text + "' is not a valid date.Please enter a valid date.\nDate must be between '" + LongDBDateTimeMin.ToShortDateString() + "' and '" + LongDBDateTimeMax.ToShortDateString() + "'", "Date", MessageBoxButtons.OK, MessageBoxIcon.Hand);
						e.Cancel = true;
						Focus();
					}
				}
				catch
				{
					MessageBox.Show("'" + Text + "' is not a valid date.Please enter a valid date.\nDate must be between '" + LongDBDateTimeMin.ToShortDateString() + "' and '" + LongDBDateTimeMax.ToShortDateString() + "'", "Date", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					e.Cancel = true;
					Focus();
				}
			}
		}
	}
}
