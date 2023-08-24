using System.ComponentModel;
using System.Drawing;
using System.Resources;
using System.Windows.Forms;

namespace Micromind.DataControls
{
	public class TenancyContractStatusComboBox : ComboBox
	{
		private Container components;

		public int SelectedID
		{
			get
			{
				return SelectedIndex + 1;
			}
			set
			{
				if (value <= 0)
				{
					SelectedIndex = -1;
				}
				else
				{
					SelectedIndex = value - 1;
				}
			}
		}

		public TenancyContractStatusComboBox()
		{
			InitializeComponent();
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

		public void LoadData()
		{
			base.Items.Clear();
			base.Items.Add("Active");
			base.Items.Add("Cancelled");
			base.Items.Add("Expired");
		}

		public void Clear()
		{
			if (base.Items.Count > 0)
			{
				SelectedIndex = 0;
			}
			else
			{
				SelectedIndex = -1;
			}
		}
	}
}
