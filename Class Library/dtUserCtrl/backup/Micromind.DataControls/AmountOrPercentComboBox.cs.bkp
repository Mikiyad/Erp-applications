using Infragistics.Win;
using Infragistics.Win.UltraWinEditors;
using System.ComponentModel;
using System.Drawing;

namespace Micromind.DataControls
{
	public class AmountOrPercentComboBox : UltraComboEditor
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

		public AmountOrPercentComboBox()
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
			base.Name = "AmountOrPercentComboBox";
			base.Size = new System.Drawing.Size(192, 20);
			ResumeLayout(false);
		}

		public void LoadData()
		{
			Items.Clear();
			Items.Add("Percent");
			Items.Add("Amount");
		}
	}
}
