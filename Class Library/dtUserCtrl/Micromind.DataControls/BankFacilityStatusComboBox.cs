using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class BankFacilityStatusComboBox : SingleColumnComboBox
	{
		private Container components;

		public override int SelectedID
		{
			get
			{
				if (SelectedIndex < 0)
				{
					return -1;
				}
				return SelectedIndex + 1;
			}
			set
			{
				if (value < 0)
				{
					SelectedIndex = -1;
				}
				else
				{
					SelectedIndex = value - 1;
				}
			}
		}

		public BankFacilityStatusComboBox()
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
			base.Name = "BankFacilityGroupComboBox";
			base.Size = new System.Drawing.Size(192, 20);
		}

		public override void LoadData()
		{
			base.Items.Clear();
			base.Items.Add("Active");
			base.Items.Add("Hold");
			base.Items.Add("Closed");
		}
	}
}
