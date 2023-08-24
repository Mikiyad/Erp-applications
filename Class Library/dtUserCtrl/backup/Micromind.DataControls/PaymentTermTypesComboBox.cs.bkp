using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using System.ComponentModel;
using System.Drawing;

namespace Micromind.DataControls
{
	public class PaymentTermTypesComboBox : UltraComboEditor
	{
		private Container components;

		public string SelectedID
		{
			get
			{
				if (base.SelectedIndex < 0)
				{
					return "";
				}
				return (base.SelectedIndex + 1).ToString();
			}
			set
			{
				if (value == "")
				{
					base.SelectedIndex = -1;
				}
				else
				{
					try
					{
						base.SelectedIndex = int.Parse(value) - 1;
					}
					catch
					{
						base.SelectedIndex = -1;
					}
				}
			}
		}

		public PaymentTermTypesComboBox()
		{
			InitializeComponent();
			DropDownStyle = DropDownStyle.DropDownList;
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
			base.Name = "PaymentTermTypesComboBox";
			base.Size = new System.Drawing.Size(192, 20);
			ResumeLayout(false);
		}

		public void LoadData()
		{
			Items.Clear();
			Items.Add("After Invoice Date");
			Items.Add("After End of Month");
			Items.Add("After Order Date");
			Items.Add("After ATD");
			Items.Add("After Packing List");
			Items.Add("Before ETA");
			Items.Add("After ETA");
			Items.Add("After BL Date");
			Items.Add("After GRN Date");
			Items.Add("After Delivery Date");
		}
	}
}
