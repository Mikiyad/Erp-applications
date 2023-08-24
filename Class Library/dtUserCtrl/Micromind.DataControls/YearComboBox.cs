using Micromind.ClientLibraries;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class YearComboBox : SingleColumnComboBox
	{
		private Container components;

		public override int SelectedID
		{
			get
			{
				if (Text == "")
				{
					return -1;
				}
				return Convert.ToInt32(Text);
			}
			set
			{
				if (value > 0)
				{
					Text = value.ToString();
				}
				else
				{
					Text = "";
				}
			}
		}

		public YearComboBox()
		{
			InitializeComponent();
			base.DropDownStyle = ComboBoxStyle.DropDown;
			base.Validating += YearComboBox_Validating;
		}

		private void YearComboBox_Validating(object sender, CancelEventArgs e)
		{
			if (!(Text == ""))
			{
				int result = 0;
				if (!int.TryParse(Text, out result) || result > 2099 || result < 2000)
				{
					ErrorHelper.WarningMessage("Please enter a numeric value between 2000 and 2099");
					e.Cancel = true;
				}
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
			new System.Resources.ResourceManager(typeof(Micromind.DataControls.AccountTypesComboBox));
			SuspendLayout();
			base.Name = "ItemTypesComboBox";
			base.Size = new System.Drawing.Size(192, 20);
		}

		public override void LoadData()
		{
			base.Items.Clear();
			base.Items.Add(DateTime.Today.Year - 3);
			base.Items.Add(DateTime.Today.Year - 2);
			base.Items.Add(DateTime.Today.Year - 1);
			base.Items.Add(DateTime.Today.Year);
			base.Items.Add(DateTime.Today.Year + 1);
			base.Items.Add(DateTime.Today.Year + 2);
			base.Items.Add(DateTime.Today.Year + 3);
			base.Items.Add(DateTime.Today.Year + 4);
			base.Items.Add(DateTime.Today.Year + 5);
		}
	}
}
