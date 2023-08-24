using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Micromind.UISupport
{
	public class MMSDateTimePicker : DateTimePicker
	{
		private IContainer components;

		public new bool Checked
		{
			get
			{
				return base.Checked;
			}
			set
			{
				base.Checked = value;
				OnValueChanged(null);
			}
		}

		public new DateTime Value
		{
			get
			{
				if (Checked)
				{
					return base.Value;
				}
				try
				{
					return DateTime.MinValue;
				}
				catch
				{
					return base.Value;
				}
			}
			set
			{
				try
				{
					base.Value = value;
				}
				catch
				{
				}
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public bool IsNull
		{
			get
			{
				return Checked;
			}
			set
			{
				Checked = value;
			}
		}

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DateTime ValueFrom => new DateTime(Value.Year, Value.Month, Value.Day, 0, 0, 0);

		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public DateTime ValueTo => new DateTime(Value.Year, Value.Month, Value.Day, 23, 59, 59);

		public MMSDateTimePicker()
		{
			InitializeComponent();
			base.ValueChanged += MMSDateTimePicker_ValueChanged;
			SetFormat();
		}

		public MMSDateTimePicker(IContainer container)
		{
			container.Add(this);
			InitializeComponent();
			base.ValueChanged += MMSDateTimePicker_ValueChanged;
			SetFormat();
		}

		private void MMSDateTimePicker_ValueChanged(object sender, EventArgs e)
		{
			SetFormat();
		}

		public void Clear()
		{
			IsNull = true;
			Checked = false;
		}

		private void SetFormat()
		{
			if (Checked)
			{
				base.Format = DateTimePickerFormat.Short;
				return;
			}
			base.Format = DateTimePickerFormat.Custom;
			base.CustomFormat = " ";
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
