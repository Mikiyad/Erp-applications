using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class MonthComboBox : SingleColumnComboBox
	{
		private bool isMonthNumber;

		private Container components;

		public bool IsMonthNumbers
		{
			get
			{
				return isMonthNumber;
			}
			set
			{
				isMonthNumber = value;
			}
		}

		public new int SelectedID
		{
			get
			{
				if (SelectedIndex <= 0)
				{
					return -1;
				}
				return SelectedIndex;
			}
			set
			{
				if (value <= 0)
				{
					SelectedIndex = 0;
				}
				else
				{
					SelectedIndex = value;
				}
			}
		}

		public MonthComboBox()
		{
			InitializeComponent();
			base.DropDownStyle = ComboBoxStyle.DropDownList;
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
			base.Items.Add("N/A");
			if (isMonthNumber)
			{
				base.Items.Add("1");
				base.Items.Add("2");
				base.Items.Add("3");
				base.Items.Add("4");
				base.Items.Add("5");
				base.Items.Add("6");
				base.Items.Add("7");
				base.Items.Add("8");
				base.Items.Add("9");
				base.Items.Add("10");
				base.Items.Add("11");
				base.Items.Add("12");
			}
			else
			{
				base.Items.Add("January");
				base.Items.Add("February");
				base.Items.Add("March");
				base.Items.Add("April");
				base.Items.Add("May");
				base.Items.Add("June");
				base.Items.Add("July");
				base.Items.Add("August");
				base.Items.Add("September");
				base.Items.Add("October");
				base.Items.Add("November");
				base.Items.Add("December");
			}
		}
	}
}
